using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mehdiskan.web.Interfaces;
using mehdiskan.web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace mehdiskan.web.Pages.Admin.Photos
{
    public class IndexModel : PageModel
    {
        private readonly IPhotoService _photoService;

        // step 1: create a constructor
        public IndexModel(IPhotoService photoService)
        {
            // step 2: inject photo service
            _photoService = photoService;
        }

        // step 3: strong-binding
        public PhotoPagingViewModel PhotoPagingVM { get; private set; }

        public int PhotosCount { get; private set; }

        public async Task OnGetAsync(int pageNumber = 1, int pageSize = 32)
        {
            // step 4: food PhotoPaingVM and PhotosCount
            PhotoPagingVM = await _photoService.GetPhotosAsync(pageNumber, pageSize);
            PhotosCount = await _photoService.PhotosCountAsync();
        }
    }
}
