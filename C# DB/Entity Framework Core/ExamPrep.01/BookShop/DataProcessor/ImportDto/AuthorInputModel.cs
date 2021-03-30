namespace BookShop.DataProcessor.ImportDto
{
    using Newtonsoft.Json;
    using System.ComponentModel.DataAnnotations;

    public class AuthorInputModel
    {
        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string LastName { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]{3}-[0-9]{3}-[0-9]{4}$")]
        public string Phone { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        public AuthorBookInputModel[] Books { get; set; }
    }

    public class AuthorBookInputModel
    {
        [JsonProperty("Id")]
        public int? Id { get; set; }
    }}
