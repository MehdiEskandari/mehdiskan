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
    public class DeleteModel : PageModel
    {
        private readonly IEyeColorService _eyeColorService;


        // step 1: add constructor
        public DeleteModel(IEyeColorService eyeColorService)
        {
            // step 2: inject ieyeColor service
            _eyeColorService = eyeColorService;
        }


        // step 3: create eyeColor property
        [BindProperty]
        public EyeColour EyeColor { get; set; }

        // step 4: get eyeColor by id  
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
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

        public async Task<IActionResult> OnPostAsync(int id)
        {
            // success
            TempData["Success"] = "EyeColor successfully removed.";
            await _eyeColorService.RemoveEyeColorAsync(id);

            return RedirectToPage("Index");

        }
    }
}
