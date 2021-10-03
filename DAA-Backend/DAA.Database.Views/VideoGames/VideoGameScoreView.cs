namespace DAA.Database.Views.VideoGames
{
    public class VideoGameScoreView
    {
        public int Id { get; set; }
        public string VideoGameName { get; set; }
        public string PlatformName { get; set; }
        public decimal Score { get; set; }
    }
}
