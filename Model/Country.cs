using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BasicWebAPI.Model
{
    public class Country
    {
        [Key]
        public int CountryId { get; set; }

        public string CountryName { get; set; }

        [JsonIgnore]
        public ICollection<Contact> Contacts { get; set; }
    }
}
