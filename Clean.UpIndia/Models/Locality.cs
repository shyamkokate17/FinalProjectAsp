using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System.Text.Json.Serialization;

namespace Clean.UpIndia.Models
{
    [Table("Localities")]
    public class Locality
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Locality Id")]
        public int LocalityId { get; set; }

        [Required(ErrorMessage = "{0} cannot be empty!")]
        [Column(TypeName = "varchar(50)")]
        public string LocalityName { get; set; }



        #region Navigation Properties to the Event Model
        [JsonIgnore]
        public ICollection<Event> Events { get; set; }


        #endregion



        #region Navigation Properties to the PublicToilets Model

        [JsonIgnore]
        public ICollection<PublicToilet> PublicToilets { get; set; }

        #endregion


    }
}
