using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BasicWebAPI.Model
{
    public class Contact
    {
        [Key]
        public int ContactId { get; set; }

        public string ContactName { get; set; }

        [ForeignKey("Company")]
        public int CompanyId { get; set; }

        [ForeignKey("Country")]
        public int CountryId { get; set; }

        [JsonIgnore]
        public Company Company { get; set; }

        [JsonIgnore]
        public Country Country { get; set; }
    }
}
