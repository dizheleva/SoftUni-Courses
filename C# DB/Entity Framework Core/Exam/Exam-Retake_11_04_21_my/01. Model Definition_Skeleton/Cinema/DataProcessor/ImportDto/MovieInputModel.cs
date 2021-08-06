using System;
using System.ComponentModel.DataAnnotations;
using Cinema.Data.Models.Enums;
using Newtonsoft.Json;

namespace Cinema.DataProcessor.ImportDto
{
    public class MovieInputModel
    {
        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        [JsonProperty("Title")]
        public string Title { get; set; }
        

        [Required]
        [Range(0,9)]
        [JsonProperty("Genre")]
        
        public string Genre { get; set; }

        [Required]
        [JsonProperty("Duration")]
        public string Duration { get; set; }
        

        [Required]
        [Range(1.00,10.00)]
        [JsonProperty("Rating")]
        public double Rating { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        [JsonProperty("Director")]
        public string Director { get; set; }

    }

    public enum GenreTypeConverter
    {
        Action = 0,
        Drama = 1,
        Comedy = 2,
        Crime = 3,
        Western = 4,
        Romance = 5,
        Documentary = 6,
        Children = 7,
        Animation = 8,
        Musical = 9
    }
}

//"Title": "Little Big Man",
//"Genre": "Western",
//"Duration": "01:58:00",
//"Rating": 28,
//"Director": "Duffie Abrahamson"
