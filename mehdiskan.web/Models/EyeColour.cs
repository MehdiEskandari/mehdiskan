using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace mehdiskan.web.Models
{
    public class EyeColour
    {
        [Key]
        public int EyeColorId { get; set; }

        [Required]
        [StringLength(120)]
        public string Name { get; set; }

        #region Relations - Navigation Properties

        public ICollection<Pet> Pets { get; set; }

        #endregion
    }
}
