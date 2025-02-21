using System.ComponentModel.DataAnnotations;

namespace CityInfo.API.Models.RequestDto
{
    public class POIUpdateDto
    {
        [Required(ErrorMessage ="Please provide a name for point of interest")]
        [MaxLength(50)]
        public string Name { get; set; } = "";
    }
}
