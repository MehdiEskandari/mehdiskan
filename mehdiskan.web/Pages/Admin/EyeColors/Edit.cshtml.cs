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
    public class EditModel : PageModel
    {
        private readonly IEyeColorService _eyeColorService;

        public EditModel(IEyeColorService eyeColorService)
        {
            _eyeColorService = eyeColorService;
        }

        [BindProperty]
        public EyeColour EyeColor { get; set; }


        public async Task<IActionResult> OnGetAsync(int? id)
        {

            if (id ==null)
            {
                return BadRequest();
            }

            EyeColor = await _eyeColorService.GetEyeColorByIdAsync(id.Value);

            if (EyeColor == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                if (await _eyeColorService.UpdateEyeColorAsync(EyeColor) != null)
                {
                    // success
                    TempData["Success"] = "EyeColor Successfully edited.";
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
            ViewData["Message"] = "your inputs is not valid.";
            return Page();
        }


    }
}
