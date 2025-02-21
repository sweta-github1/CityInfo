using System.ComponentModel.DataAnnotations;

namespace CityInfo.API.Models.ResponseDto
{
    public class CityDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "You should provide a name value.")]
        [MaxLength(50)]
        public string Name { get; set; } = "";

        [MaxLength(200)]
        public string? Description { get; set; }

        public List<PointOfInterestDto> PointsOfInterest { get; set; }
            = [];

        public int NumberOfPointsOfInterest
        {
            get
            {
                return PointsOfInterest.Count;
            }
        }
    }
}
