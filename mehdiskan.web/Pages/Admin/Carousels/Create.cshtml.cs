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
    public class CreateModel : PageModel
    {
        private readonly ICarouselService _carouselService;
        private readonly ILogger<CreateModel> _logger;

        // step 1: add constructor
        public CreateModel(ICarouselService carouselService, ILogger<CreateModel> logger)
        {
            // step 2: inject iCarouselService 
            _carouselService = carouselService;
            _logger = logger;
        }

        // step 3: create property
        [BindProperty]
        public Carousel Carousel { get; set; }
        public IFormFile Image { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                // admin inputs is valid
                if (await _carouselService.AddCarouselAsync(Carousel, Image) != null)
                {
                    TempData["Success"] = "New carousel successfully added.";

                    return RedirectToPage("Index");
                }
                else
                {
                    // db failure
                    TempData["Message"] = "database error, contact the developer to fix the error.";

                    _logger.LogError($"Carousel {nameof(CreateModel)} database error.");

                    return Page();
                }
            }

            // invalid inputs
            TempData["Message"] = "Your inputs is not valid.";

            _logger.LogError($"Carousel {nameof(CreateModel)} invalid input.");

            return Page();

        }
    }
}
