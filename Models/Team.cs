namespace BodyaBet.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? BaseYear { get; set; }
        public int CountryId { get; set; }
        public virtual Country? Country { get; set; }
    }
}
