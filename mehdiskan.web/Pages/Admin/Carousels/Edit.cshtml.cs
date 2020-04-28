using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mehdiskan.web.Interfaces;
using mehdiskan.web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace mehdiskan.web.Pages.Admin.Carousels
{
    public class EditModel : PageModel
    {
        private readonly ICarouselService _carouselService;
        private readonly ILogger<EditModel> _logger;

        // step 1: add constructor
        public EditModel(ICarouselService carouselService, ILogger<EditModel> logger)
        {
            // step 2: inject iCarouselService 
            _carouselService = carouselService;
            _logger = logger;
        }

        // step 3: create property
        [BindProperty]
        public Carousel Carousel { get; set; }
        public IFormFile Image { get; set; }


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if(id == null)
            {
                return BadRequest();
            }

            Carousel = await _carouselService.GetCarouselByIdAsync(id.Value);

            if (Carousel == null)
            {
                return NotFound();
            }

            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                // admin inputs is valid
                if (await _carouselService.UpdateCarouselAsync(Carousel, Image) != null)
                {
                    TempData["Success"] = "carousel successfully Update.";

                    return RedirectToPage("Index");
                }
                else
                {
                    // db failure
                    TempData["Message"] = "database error, contact the developer to fix the error.";

                    _logger.LogError($"Carousel {nameof(EditModel)} database error.");

                    return Page();
                }
            }

            // invalid inputs
            TempData["Message"] = "Your inputs is not valid.";

            _logger.LogError($"Carousel {nameof(EditModel)} invalid input.");

            return Page();

        }
    }
}
