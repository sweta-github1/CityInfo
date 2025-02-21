using System.ComponentModel.DataAnnotations;

namespace CityInfo.API.Models.RequestDto
{
    public class CityCreationDto
    {
        [Required(ErrorMessage ="Please provide a city name")]
        [MaxLength(50)]
        public string Name { get; set; } = "";

        [MaxLength(200)]
        public string Description { get; set; } = "";
    }
}
