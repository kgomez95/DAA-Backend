namespace DAA.Constants.Databases
{
    public class DbViewsValues
    {
        public class VideoGameScores
        {
            public const string VIEW_NAME = "VW_VIDEOGAMESCORE";

            public const string ID = "Id";
            public const string VIDEO_GAME_NAME = "VideoGameName";
            public const string PLATFORM_NAME = "PlatformName";
            public const string SCORE = "Score";
        }

        public class VideoGames
        {
            public const string VIEW_NAME = "VW_VIDEOGAMES";

            public const string VIDEO_GAME_ID = "VideoGameId";
            public const string VIDEO_GAME_NAME = "VideoGameName";
            public const string VIDEO_GAME_DESCRIPTION = "VideoGameDescription";
            public const string VIDEO_GAME_PRICE = "VideoGamePrice";
            public const string VIDEO_GAME_SCORE = "VideoGameScore";
            public const string VIDEO_GAME_RELEASE_DATE = "VideoGameReleaseDate";
            public const string PLATFORM_ID = "PlatformId";
            public const string PLATFORM_NAME = "PlatformName";
            public const string PLATFORM_COMPANY = "PlatformCompany";
            public const string PLATFORM_PRICE = "PlatformPrice";
            public const string PLATFORM_RELEASE_DATE = "PlatformReleaseDate";
        }

        public class Platforms
        {
            public const string VIEW_NAME = "VW_PLATFORMS";

            public const string ID = "Id";
            public const string NAME = "Name";
            public const string COMPANY = "Company";
            public const string PRICE = "Price";
            public const string RELEASE_DATE = "ReleaseDate";
        }
    }
}
