using System.ComponentModel.DataAnnotations;

namespace BodyaBet.Models
{
    public class Player
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        public string Name { get; set; }
        public int CountryId { get; set; }
        public int? TeamId { get; set; }
        public virtual Country? Country { get; set; }
        public virtual Team? Team { get; set; }

    }
}
