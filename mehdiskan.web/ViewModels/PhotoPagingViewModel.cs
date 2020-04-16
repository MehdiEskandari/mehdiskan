using mehdiskan.web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mehdiskan.web.ViewModels
{
    public class PhotoPagingViewModel
    {
        public List<Photo> Photos { get; set; }

        public int PageCount { get; set; }

        public int PageNumber { get; set; }
    }
}
