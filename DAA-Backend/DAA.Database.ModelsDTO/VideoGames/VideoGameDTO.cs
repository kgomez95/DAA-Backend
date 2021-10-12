using DAA.Database.ModelsDTO.Platforms;
using System;

namespace DAA.Database.ModelsDTO.VideoGames
{
    [Serializable]
    public class VideoGameDTO : BaseAuditableDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Score { get; set; }
        public DateTime ReleaseDate { get; set; }

        public int PlatformId { get; set; }
        public PlatformDTO Platform { get; set; }
    }
}
