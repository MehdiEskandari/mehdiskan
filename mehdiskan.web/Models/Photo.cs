﻿using System.ComponentModel.DataAnnotations;

namespace mehdiskan.web.Models
{
    public class Photo
    {
        [Key]
        public int PhotoId { get; set; }

        [Required][StringLength(75)]
        public string PhotoName { get; set; }

        [Required]
        public int PetId { get; set; }

        #region Relations - Navigation Properties
        public Pet Pet { get; set; }
        #endregion
    }

}
