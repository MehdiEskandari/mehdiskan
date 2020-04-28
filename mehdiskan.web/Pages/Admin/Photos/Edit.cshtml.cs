using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mehdiskan.web.Interfaces;
using mehdiskan.web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace mehdiskan.web.Pages.Admin.Photos
{
    public class EditModel : PageModel
    {
        private readonly IPhotoService _photoService;
        private readonly ILogger<EditModel> _logger;

        // step 1: create a constructor
        public EditModel(IPhotoService photoService,
            ILogger<EditModel> logger)
        {
            // step 2: inject message service
            _photoService = photoService;
            _logger = logger;
        }

        [BindProperty]
        public Photo Photo { get; set; }

        public SelectList PetsSelectList { get; set; }

        public IFormFile Image { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            Photo = await _photoService.GetPhotoByIdAsync(id.Value);

            await FillPetsSelectListItem();

            if (Photo == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                if (await _photoService.UpdatePhotoAsync(Photo, Image) != null)
                {
                    // success
                    TempData["Success"] = "Photo successfully updated.";

                    return RedirectToPage("Index");
                }
                else
                {
                    // db failure
                    ViewData["Message"] = "Database error, contact the developer to fix the error.";

                    _logger.LogError($"Photo {nameof(EditModel)} database error.");

                    await FillPetsSelectListItem();

                    return Page();
                }
            }

            // wrong inputs
            ViewData["Message"] = "Your inputs is not valid.";

            _logger.LogError($"Photo {nameof(EditModel)} database error.");

            await FillPetsSelectListItem();

            return Page();
        }

        private async Task FillPetsSelectListItem()
        {
            PetsSelectList = new SelectList(await _photoService.GetSelectListItemsAsync(), "Value", "Text");
        }
    }
}
