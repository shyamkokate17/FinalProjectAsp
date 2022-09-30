using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Xml.Linq;
using System;

namespace Clean.UpIndia.Models
{
    [Table("PublicToilets")]
    public class PublicToilet
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Toilet Id")]
        public int ToiletId { get; set; }

        [Required(ErrorMessage = "{0} cannot be empty!")]
        [Column(TypeName = "varchar(50)")]
        [Display(Name = "Open Hours")]
        public string OpenHours { get; set; }

        [Required]
        [DefaultValue(true)]
        [Display(Name = "Is Available?")]
        public bool IsAvailable { get; set; } = true;

        [Display(Name = "Rating")]
        [Range(1,10,ErrorMessage ="Rating must be below 10")]
        public int Rating { get; set; }



        #region Navigation Properties to the Localities Model

        [Required]
        [Display(Name = "Locality Name")]
        public int LocalityId { get; set; }

        [ForeignKey(nameof(PublicToilet.LocalityId))]
        public Locality Localities { get; set; }

        #endregion
    }
}
