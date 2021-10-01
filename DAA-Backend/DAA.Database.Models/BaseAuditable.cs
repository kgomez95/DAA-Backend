using System;

namespace DAA.Database.Models
{
    public class BaseAuditable
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
