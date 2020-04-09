using mehdiskan.web.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace mehdiskan.web.Data
{
    public class EyeColor
    {
        [Key]
        public int EyeColorId { get; set; }

        [Required][StringLength(120)]
        public string Name { get; set; }

        #region Relations - Navigation Properties
        public ICollection<Pet> Pets { get; set; }
        #endregion
    }
}
