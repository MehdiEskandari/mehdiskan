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
    public class CreateModel : PageModel
    {
        private readonly IPhotoService _photoService;
        private readonly ILogger<CreateModel> _logger;

        public CreateModel(IPhotoService photoService, ILogger<CreateModel> logger)
        {
            _photoService = photoService;
            _logger = logger;
        }

        [BindProperty]
        public Photo Photo { get; set; }

        public SelectList PetsSelectList { get; set; }

        public IFormFile Image { get; set; }


        public async Task OnGet()
        {
            await FillPetsSelectListItem();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                // admin inputs is valid

                if (await _photoService.AddPhotoAsync(Photo, Image) != null)
                {
                    // success
                    TempData["Success"] = "New photo successfully added.";

                    return RedirectToPage("Index");
                }
                else
                {
                    // db failure
                    ViewData["Message"] = "Database error, contact the developer to fix the error.";

                    _logger.LogError($"Photo {nameof(CreateModel)} database error.");

                    await FillPetsSelectListItem();

                    return Page();
                }
            }

            // wrong inputs
            ViewData["Message"] = "Your inputs is not valid.";

            _logger.LogError($"Photo {nameof(CreateModel)} database error.");

            await FillPetsSelectListItem();

            return Page();
        }

        private async Task FillPetsSelectListItem()
        {
            PetsSelectList = new SelectList(await _photoService.GetSelectListItemsAsync(), "Value", "Text");
        }

    }
}
