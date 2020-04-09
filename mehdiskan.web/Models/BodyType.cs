using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace mehdiskan.web.Models
{
    public class BodyType
    {
        [Key]
        public int BodyTypeId { get; set; }

        [Required]
        [StringLength(120)]
        public string Title { get; set; }

        #region Relation - Navigation Properties

        public ICollection<Pet> Pets { get; set; }

        #endregion
    }
}
