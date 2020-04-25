using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mehdiskan.web.Interfaces;
using mehdiskan.web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace mehdiskan.web.Pages.Admin.EyeColors
{
    public class CreateModel : PageModel
    {
        private readonly IEyeColorService _eyeColorService;

        // step 1: add constructor
        public CreateModel(IEyeColorService eyeColorService)
        {
            // step 2: inject ibodytype service
            _eyeColorService = eyeColorService;
        }

        // step 3: create bodytype property
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
                    return Page();
                }
            }

            // invalid inputs by admin
            ViewData["Message"] = "Your inputs is not valid.";
            return Page();

        }
    }
}
