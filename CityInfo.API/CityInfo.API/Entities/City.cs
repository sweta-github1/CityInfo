namespace CityInfo.API.Entities
{
    public class City(string name)
    {
        public int Id { get; set; }
        public string Name { get; set; } = name;
        public string? Description { get; set; }

        public ICollection<PointOfInterest> PointsOfInterest { get; set; }
            = [];
    }
}
