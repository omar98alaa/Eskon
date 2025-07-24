namespace Eskon.Domian.DTOs.CityDTO
{
    public class CityUpdateDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid CountryId { get; set; }
    }
}
