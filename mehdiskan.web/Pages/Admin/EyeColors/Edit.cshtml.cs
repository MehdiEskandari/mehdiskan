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
    public class EditModel : PageModel
    {
        private readonly IEyeColorService _eyeColorService;
        private readonly ILogger<CreateModel> _logger;

        // step 1: add constructor
        public EditModel(IEyeColorService eyeColorService, ILogger<CreateModel> logger)
        {
            // step 2: inject ieyeColor service
            _eyeColorService = eyeColorService;
            _logger = logger;
        }

        // step 3: create eyeColor property
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

                    _logger.LogError($"EyeColor {nameof(EditModel)} database error.");

                    return Page();
                }
            }

            // invalid inputs by admin
            ViewData["Message"] = "your inputs is not valid.";

            _logger.LogError($"EyeColor {nameof(EditModel)} invalid inputs.");

            return Page();
        }


    }
}
