using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Newtonsoft.Json;
using VaporStore.Data.Models;
using VaporStore.Data.Models.Enums;
using VaporStore.DataProcessor.Dto.Import;

namespace VaporStore.DataProcessor
{
	using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Data;

	public static class Deserializer
	{
        private const string ErrorMessage = "Invalid Data";

        private const string UserWithCardsAdded = "Imported {0} with {1} cards";

        private const string GameAddedMessage = "Added {0} ({1}) with {2} tags";

        private const string PurchaseImported = "Imported {0} for {1}";

        public static string ImportGames(VaporStoreDbContext context, string jsonString)
		{
            var gamesDto = JsonConvert.DeserializeObject<GameImportDto[]>(jsonString);

            var games = new List<Game>();
            var developers = new List<Developer>();
            var genres = new List<Genre>();
            var tags = new List<Tag>();

            var sb = new StringBuilder();

            foreach (var gameImportDto in gamesDto)
            {
                if (!IsValid(gameImportDto) || gameImportDto.Tags.Length ==0)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                DateTime releaseDate;
                bool isReleaseDateValid = DateTime.TryParseExact(gameImportDto.ReleaseDate, "yyyy-MM-dd",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out releaseDate);

                if (!isReleaseDateValid)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                if (gameImportDto.Tags.Length == 0)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var developer = developers.FirstOrDefault(d => d.Name == gameImportDto.Developer);

                if (developer == null)
                {
                    developer = new Developer { Name = gameImportDto.Developer };
                    developers.Add(developer);
                }

                var genre = genres.FirstOrDefault(d => d.Name == gameImportDto.Genre);

                if (genre == null)
                {
                    genre = new Genre() { Name = gameImportDto.Genre };
                    genres.Add(genre);
                }

                var game = new Game()
                {
                    Name = gameImportDto.Name,
                    Price = gameImportDto.Price,
                    ReleaseDate = releaseDate,
                    Developer = developer,
                    Genre = genre
                };

                foreach (var tagName in gameImportDto.Tags)
                {
                    if (String.IsNullOrEmpty(tagName))
                    {
                        continue;
                    }

                    var tag = tags.FirstOrDefault(b => b.Name == tagName);

                    if (tag == null)
                    {
                        tag = new Tag {Name = tagName};
                        tags.Add(tag);
                    }

                    game.GameTags.Add(new GameTag()
                    {
                        Game = game,
                        Tag = tag
                    });
                }

                if (game.GameTags.Count == 0)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                games.Add(game);

                sb.AppendLine(string.Format(GameAddedMessage, game.Name, game.Genre.Name, game.GameTags.Count));
            }

            context.Games.AddRange(games);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportUsers(VaporStoreDbContext context, string jsonString)
        {
            var usersDto = JsonConvert.DeserializeObject<User[]>(jsonString);

            var users = new List<User>();
            var cards = new List<Card>();

            var sb = new StringBuilder();

            foreach (var userDto in usersDto)
            {
                if (!IsValid(userDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var user = users.FirstOrDefault(d => d.FullName == userDto.FullName);

                if (user == null)
                {
                    user = new User()
                    {
                        FullName = userDto.FullName,
                        Username = userDto.Username,
                        Email = userDto.Email,
                        Age = userDto.Age
                    };

                    foreach (var cardDto in userDto.Cards)
                    {
                        var card = cards.FirstOrDefault(d => d.Number == cardDto.Number);

                        if (card == null)
                        {
                            card = new Card()
                            {
                                Number = cardDto.Number,
                                Cvc = cardDto.Cvc,
                                Type = cardDto.Type
                            };

                            if (!IsValid(userDto))
                            {
                                sb.AppendLine(ErrorMessage);
                                continue;
                            }

                            cards.Add(card);
                        }

                        user.Cards.Add(card);

                    }

                    users.Add(user);
                }

                sb.AppendLine(string.Format(UserWithCardsAdded, user.Username, user.Cards.Count));
            }

            context.Cards.AddRange(cards);
            context.Users.AddRange(users);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportPurchases(VaporStoreDbContext context, string xmlString)
		{
            var xmlSerializer = new XmlSerializer(typeof(PurchaseImportDto[]), new XmlRootAttribute("Purchases"));
            var textReader = new StringReader(xmlString);

            var purchasesDto = xmlSerializer.Deserialize(textReader) as PurchaseImportDto[];

            var purchases = new List<Purchase>();

            var sb = new StringBuilder();

            foreach (var purchaseDto in purchasesDto)
            {
                if (!IsValid(purchaseDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var isDateValid = DateTime.TryParseExact(purchaseDto.Date, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out var date);

                if (!isDateValid)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var isTypeValid = Enum.TryParse(purchaseDto.Type, out PurchaseType type);

                if (!isTypeValid)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var card = context.Cards.FirstOrDefault(c => c.Number == purchaseDto.CardNumber);

                if (card == null)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var game = context.Games.FirstOrDefault(c => c.Name == purchaseDto.GameName);

                if (game == null)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var purchase = new Purchase()
                {
                    Type = type, 
                    ProductKey = purchaseDto.ProductKey,
                    Date = date,
                    Card = card,
                    Game = game
                };

                purchases.Add(purchase);

                sb.AppendLine(string.Format(PurchaseImported, purchase.Game.Name, purchase.Card.User.Username));
            }

            context.Purchases.AddRange(purchases);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

		private static bool IsValid(object dto)
		{
			var validationContext = new ValidationContext(dto);
			var validationResult = new List<ValidationResult>();

			return Validator.TryValidateObject(dto, validationContext, validationResult, true);
		}
	}
}