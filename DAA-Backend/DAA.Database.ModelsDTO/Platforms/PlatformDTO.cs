using DAA.Database.ModelsDTO.VideoGames;
using System;
using System.Collections.Generic;

namespace DAA.Database.ModelsDTO.Platforms
{
    [Serializable]
    public class PlatformDTO : BaseAuditableDTO
    {
        public string Name { get; set; }
        public string Company { get; set; }
        public decimal Price { get; set; }
        public DateTime ReleaseDate { get; set; }

        public IList<VideoGameDTO> VideoGames { get; set; }
    }
}
