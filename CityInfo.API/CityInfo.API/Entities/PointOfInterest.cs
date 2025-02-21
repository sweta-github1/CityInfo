using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CityInfo.API.Entities
{
    public class PointOfInterest(string name)
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "You should provide a name value.")]
        [MaxLength(50)]
        public string? Name { get; set; } = name;

        [ForeignKey("CityId")]
        public City? City { get; set; }
        public int CityId { get; set; }
    }
}
