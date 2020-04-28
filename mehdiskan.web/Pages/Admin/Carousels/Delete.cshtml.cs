using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mehdiskan.web.Interfaces;
using mehdiskan.web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace mehdiskan.web.Pages.Admin.Carousels
{
    public class DeleteModel : PageModel
    {
        private readonly ICarouselService _carouselService;
        private readonly ILogger<DeleteModel> _logger;

        // step 1: add constructor
        public DeleteModel(ICarouselService carouselService, ILogger<DeleteModel> logger)
        {
            // step 2: inject iCarouselService 
            _carouselService = carouselService;
            _logger = logger;
        }

        // step 3: create property
        [BindProperty]
        public Carousel Carousel { get; set; }


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
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


        public async Task<IActionResult> OnPostAsync(int id)
        {

            TempData["Success"] = "Carousel successfully Removed.";
            await _carouselService.RemoveCarouselAsync(id);
            
            return RedirectToPage("Index");
        }
    }
}
