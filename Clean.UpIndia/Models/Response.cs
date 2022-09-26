using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clean.UpIndia.Models
{
    [Table("Responses")]
    public class Response
    {
        [Key]
        [Required(ErrorMessage = "{0} cannot be empty!!!!")]
        [Display(Name = "Complaint Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Required Name!!! ")]
        [Display(Name = "Name of Person")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Complaint Date")]
        public DateTime DateTime { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "{0} cannot be more than {1} characters!!!")]
        [Display(Name = "Complaint message")]
        public string Message { get; set; }
    }
}
