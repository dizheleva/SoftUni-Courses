namespace CinemaApp.Data.Common
{
    public static class EntityConstants
    {
        public static class MovieConstants
        {
            // Title stores text between 2 and 100 characters
            public const int TitleMinLength = 2;
            public const int TitleMaxLength = 100;

            // Genre stores text between 3 and 50 characters
            public const int GenreMinLength = 3;
            public const int GenreMaxLength = 50;

            // Director stores text between 2 and 100 characters
            public const int DirectorMinLength = 2;
            public const int DirectorMaxLength = 100;

            // Description stores text between 10 and 1000 characters
            public const int DescriptionMinLength = 10;
            public const int DescriptionMaxLength = 1000;

            // Duration should be between 1 and 300 minutes
            public const int DurationMin = 1;
            public const int DurationMax = 300;

            // Maximum allowed length for image URL
            public const int ImageUrlMaxLength = 2048;

            // The format used for the released date
            public const string DateFormat = "yyyy-MM-dd";

            // Error messages

            public const string TitleRequiredMessage = "Title is required.";
            public const string TitleMinLengthMessage = "Title must be at least 2 characters.";
            public const string TitleMaxLengthMessage = "Title cannot exceed 100 characters.";

            public const string GenreRequiredMessage = "Genre is required.";
            public const string GenreMinLengthMessage = "Genre must be at least 3 characters.";
            public const string GenreMaxLengthMessage = "Genre cannot exceed 50 characters.";

            public const string DirectorRequiredMessage = "Director is required.";
            public const string DirectorNameMinLengthMessage = "Director name must be at least 2 characters.";
            public const string DirectorNameMaxLengthMessage = "Director name cannot exceed 100 characters.";

            public const string DescriptionRequiredMessage = "Description is required.";
            public const string DescriptionMinLengthMessage = "Description must be at least 10 characters.";
            public const string DescriptionMaxLengthMessage = "Description cannot exceed 1000 characters.";

            public const string DurationRequiredMessage = "Duration is required.";
            public const string DurationRangeMessage = "Duration must be between 1 and 300 minutes.";

            public const string ReleaseDateRequiredMessage = "Release date is required.";

            public const string ImageUrlMaxLengthMessage = "Image URL cannot exceed 2048 characters.";
        }
    }
}

