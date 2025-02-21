namespace CityInfo.API.Models.ResponseDto
{
    public class CityWithoutPOI
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string? Description { get; set; }
    }
}
