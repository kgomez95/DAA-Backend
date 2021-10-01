using DAA.Database.Models.Platforms;
using System;

namespace DAA.Database.Models.VideoGames
{
    public class VideoGame : BaseAuditable
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Score { get; set; }
        public DateTime ReleaseDate { get; set; }

        public int PlatformId { get; set; }
        public Platform Platform { get; set; }
    }
}
