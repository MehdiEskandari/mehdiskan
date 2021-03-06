using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mehdiskan.web.Interfaces;
using mehdiskan.web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace mehdiskan.web.Pages.Admin.EyeColors
{
    public class CreateModel : PageModel
    {
        private readonly IEyeColorService _eyeColorService;
        private readonly ILogger<CreateModel> _logger;

        // step 1: add constructor
        public CreateModel(IEyeColorService eyeColorService, ILogger<CreateModel> logger)
        {
            // step 2: inject ieyeColor service
            _eyeColorService = eyeColorService;
            _logger = logger;
        }

        // step 3: create eyeColor property
        [BindProperty]
        public EyeColour EyeColor { get; set; }


        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                // admin inputs is valid
                if (await _eyeColorService.AddEyeColorAsync(EyeColor) != null)
                {
                    // success
                    TempData["Success"] = "New EyeColor successfully added.";
                    return RedirectToPage("Index");
                }
                else
                {
                    // db failure
                    ViewData["Message"] = "Database error, contact the developer to fix the error.";

                    _logger.LogError($"EyeColor {nameof(CreateModel)} database error.");

                    return Page();
                }
            }

            // invalid inputs by admin
            ViewData["Message"] = "Your inputs is not valid.";

            _logger.LogError($"EyeColor {nameof(CreateModel)} invalid inputs.");

            return Page();

        }
    }
}
