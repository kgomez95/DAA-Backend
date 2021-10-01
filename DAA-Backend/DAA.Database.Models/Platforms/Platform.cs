using DAA.Database.Models.VideoGames;
using System;
using System.Collections.Generic;

namespace DAA.Database.Models.Platforms
{
    public class Platform : BaseAuditable
    {
        public string Name { get; set; }
        public string Company { get; set; }
        public decimal Price { get; set; }
        public DateTime ReleaseDate { get; set; }

        public IList<VideoGame> VideoGames { get; set; }
    }
}
