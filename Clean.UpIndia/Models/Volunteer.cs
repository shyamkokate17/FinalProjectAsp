using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System;

namespace Clean.UpIndia.Models
{
    [Table("Volunteers")]
    public class Volunteer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Volunteer ID")]
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} Cannot be blank!!!")]
        [Display(Name = "Volunteer Name")]
        [StringLength(60, ErrorMessage = "{0} cannot have more than {1} characters!!! ")]
        [RegularExpression(@"^[A-Za-z]+[\s][A-Za-z]+$", ErrorMessage = "Use only letters!!!")]
        public string Name { get; set; }


        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "{0} cannot be empty!!!")]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Enter only 10 digits!!!")]
        public string PhoneNumber { get; set; }


        [Required(ErrorMessage = "Please enter Email address!!!")]
        [EmailAddress(ErrorMessage = "Invalid EmailAddress")]
        [Display(Name = "Vounteer Email")]
        public string Email { get; set; }



        [Required(ErrorMessage = "{0} cannot be Empty!!!")]
        [StringLength(120)]
        public string Address { get; set; }







        #region Navigation property to Event model

        // public string EventName { get; set; }

        [Display(Name = "Event Name")]
        public int EventId { get; set; }

        [ForeignKey(nameof(Volunteer.EventId))]
        public Event Events { get; set; }


        
        #endregion
    }
}
