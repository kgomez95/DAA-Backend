using System;

namespace DAA.Database.ModelsDTO
{
    public class BaseAuditableDTO
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
