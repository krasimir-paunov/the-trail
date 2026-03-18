namespace TheTrail.Domain.Common
{
    public static class ValidationConstants
    {
        public static class Era
        {
            public const int NameMaxLength = 100;
            public const int DescriptionMaxLength = 1000;
            public const int CoverImageUrlMaxLength = 500;
            public const int ColorThemeMaxLength = 50;
        }

        public static class Chapter
        {
            public const int TitleMaxLength = 200;
            public const int SubtitleMaxLength = 300;
            public const int CoverImageUrlMaxLength = 500;
            public const int EstimatedMinutesMin = 1;
            public const int EstimatedMinutesMax = 120;
        }

        public static class Quiz
        {
            public const int PassMarkPercentMin = 1;
            public const int PassMarkPercentMax = 100;
        }

        public static class Question
        {
            public const int TextMaxLength = 500;
            public const int OptionMaxLength = 200;
        }

        public static class Collectible
        {
            public const int NameMaxLength = 100;
            public const int DescriptionMaxLength = 500;
            public const int ArtworkUrlMaxLength = 500;
        }

        public static class Badge
        {
            public const int NameMaxLength = 100;
            public const int DescriptionMaxLength = 500;
            public const int ArtworkUrlMaxLength = 500;
        }

        public static class User
        {
            public const int DisplayNameMaxLength = 50;
            public const int AvatarUrlMaxLength = 500;
        }
    }
}