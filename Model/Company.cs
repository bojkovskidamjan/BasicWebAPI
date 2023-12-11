using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BasicWebAPI.Model
{
    public class Company
    {
        [Key]
        public int CompanyId { get; set; }

        public string CompanyName { get; set; }

        [JsonIgnore]
        public ICollection<Contact> Contacts { get; set; }

    }
}
