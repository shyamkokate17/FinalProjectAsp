using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace Clean.UpIndia.Models

{
    [Table("Events")]
    public class Event
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Event ID")]
        public int EventId { get; set; }

        [Required(ErrorMessage = "{0} Cannot be blank!!!")]
        [StringLength(20, ErrorMessage = "{0} cannot be more than {1} characters!!!")]
        [Display(Name = "Event Name")]
        public string EventName { get; set; }

        [Required]
        [StringLength(1000, ErrorMessage = "{0} cannot be empty")]
        [Display(Name = "Event Description")]
        public string EventDescription { get; set; }


        [Required]
        [Display(Name = "Event Date")]
        public DateTime EventDate { get; set; }



        [Display(Name = "Locality Name")]
        public int LocalityId { get; set; }
        [ForeignKey(nameof(Event.LocalityId))]

        public Locality Locality { get; set; }



        #region Navigation properties to the transaction Model 
        // public ICollection<Event> Events { get; set; }

        public ICollection<Volunteer> Volunteers { get; set; }


        #endregion
    }
}
