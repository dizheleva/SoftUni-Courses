using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Newtonsoft.Json;
using VaporStore.Data.Models;
using VaporStore.Data.Models.Enums;
using VaporStore.DataProcessor.Dto.Export;

namespace VaporStore.DataProcessor
{
	using System;
	using Data;

	public static class Serializer
	{
		public static string ExportGamesByGenres(VaporStoreDbContext context, string[] genreNames)
		{
            var genres = context
                .Genres
                .ToArray()
                .Where(g => genreNames.Contains(g.Name))
                .Select(g => new
                {
                    Id = g.Id,
                    Genre = g.Name,
                    Games = g.Games
                        .Where(p => p.Purchases.Any())
                        .Select(game => new
                        {
                            Id = game.Id,
                            Title = game.Name,
                            Developer = game.Developer.Name,
                            Tags = String.Join(", ", game.GameTags.Select(gt => gt.Tag.Name).ToArray()),
                            Players = game.Purchases.Count
                        })
                        .OrderByDescending(ga => ga.Players)
                        .ThenBy(ga => ga.Id)
                        .ToArray(),
                    TotalPlayers = g.Games.Sum(ga => ga.Purchases.Count)
                })
                .OrderByDescending(g => g.TotalPlayers)
                .ThenBy(g => g.Id)
                .ToArray();

            var json = JsonConvert.SerializeObject(genres, Formatting.Indented);
            return json;
        }

		public static string ExportUserPurchasesByType(VaporStoreDbContext context, string storeType)
		{
            PurchaseType purchaseType = Enum.Parse<PurchaseType>(storeType);

            var users = context.Users
                .ToArray()
                .Where(u => u.Cards.Any(c => c.Purchases.Any()))
                .Select(u => new UserDto
                {
                    Username = u.Username,
                    Purchases = context.Purchases
                        .ToArray()
                        .Where(p => p.Card.User.Username == u.Username && p.Type == purchaseType)
                        .OrderBy(p => p.Date)
                        .Select(p => new PurchaseDto
                        {
                            Card = p.Card.Number,
                            Cvc = p.Card.Cvc,
                            Date = p.Date.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture),
                            Game = new GameDto()
                            {
                                Title = p.Game.Name,
                                Genre = p.Game.Genre.Name,
                                Price = p.Game.Price
                            }
                        })
                        .ToArray(),
                    TotalSpent = context
                        .Purchases
                        .ToArray()
                        .Where(p => p.Card.User.Username == u.Username && p.Type == purchaseType)
                        .Sum(p => p.Game.Price)
                })
                .Where(u => u.Purchases.Length > 0)
                .OrderByDescending(u => u.TotalSpent)
                .ThenBy(u => u.Username)
                .ToArray();

            var xmlSerializer = new XmlSerializer(typeof(UserDto[]), new XmlRootAttribute("Users"));
            var textWriter = new StringWriter();
            var ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            xmlSerializer.Serialize(textWriter, users, ns);

            var xml = textWriter.ToString();
            return xml;
        }
	}
}