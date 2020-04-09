using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace mehdiskan.web.Models
{
    public class Contact
    {
        [Key]
        public int ContactId { get; set; }

        [Required][StringLength(12)]
        public string PhoneNumber { get; set; }
        
        [Required][StringLength(120)]
        public string Title { get; set; }

        [Required][StringLength(750)]
        public string Description { get; set; }

        public bool IsCheck { get; set; }

        [Required]
        public DateTime SubmitDate { get; set; }


    }
}
