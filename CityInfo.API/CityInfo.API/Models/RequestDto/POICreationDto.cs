using System.ComponentModel.DataAnnotations;

namespace CityInfo.API.Models.RequestDto
{
    public class POICreationDto
    {
        [Required(ErrorMessage ="Please provide POI name")]
        [MaxLength(50)]
        public string Name { get; set; } = "";


    }
}
