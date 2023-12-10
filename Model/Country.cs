using System.ComponentModel.DataAnnotations;

namespace BasicWebAPI.Model
{
    public class Country
    {
        [Key]
        public int CountryId { get; set; }

        public string CountryName { get; set; }

        public ICollection<Contact> Contacts { get; set; }
    }
}
