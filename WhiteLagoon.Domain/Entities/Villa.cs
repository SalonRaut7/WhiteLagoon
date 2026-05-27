using System.ComponentModel.DataAnnotations;

namespace WhiteLagoon.Domain.Entities
{
    public class Villa
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public required string Name { get; set; }
        [MaxLength(500)]
        public string? Description { get; set; }
        [Display(Name = "Price per Night")]
        [Range(10,10000)]
        public double Price { get; set; }
        [Range(100,5000)]
        public int Sqft { get; set; }
        [Range(1,10)]
        public int Occupancy { get; set; }
        [Display(Name = "Image URL")]
        public string? ImageUrl { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}