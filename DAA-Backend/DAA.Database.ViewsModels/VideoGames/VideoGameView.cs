using System;

namespace DAA.Database.Views.VideoGames
{
    public class VideoGameView
    {
        public int VideoGameId { get; set; }
        public string VideoGameName { get; set; }
        public string VideoGameDescription { get; set; }
        public decimal VideoGamePrice { get; set; }
        public decimal VideoGameScore { get; set; }
        public DateTime VideoGameReleaseDate { get; set; }
        public int PlatformId { get; set; }
        public string PlatformName { get; set; }
        public string PlatformCompany { get; set; }
        public decimal PlatformPrice { get; set; }
        public DateTime PlatformReleaseDate { get; set; }
    }
}
