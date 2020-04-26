using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mehdiskan.web.Interfaces;
using mehdiskan.web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace mehdiskan.web.Pages.Admin.Patterns
{
    public class EditModel : PageModel
    {
        private readonly IPatternService _patternServicee;
        private readonly ILogger<EditModel> _logger;

        // step 1: add constructor
        public EditModel(IPatternService patternService, ILogger<EditModel> logger)
        {
            // step 2: inject pattern service
            _patternServicee = patternService;
            _logger = logger;
        }

        // step 3: create pattern property
        [BindProperty]
        public Pattern Pattern { get; set; }


        public async Task<IActionResult> OnGetAsync(int? id)
        {

            if (id == null)
            {
                return BadRequest();
            }

            Pattern = await _patternServicee.GetPatternByIdAsync(id.Value);

            if (Pattern == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                if (await _patternServicee.UpdatePatternAsync(Pattern) != null)
                {
                    // success
                    TempData["Success"] = "Pattern Successfully edited.";
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
