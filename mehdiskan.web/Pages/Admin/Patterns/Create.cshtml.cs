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
    public class CreateModel : PageModel
    {
        private readonly IPatternService _patternServicee;
        private readonly ILogger<CreateModel> _logger;


        // step 1: add constructor
        public CreateModel(IPatternService patternService, ILogger<CreateModel> logger)
        {
            // step 2: inject ipattern service
            _patternServicee = patternService;
            _logger = logger;
        }


        // step 3: create pattern property
        [BindProperty]
        public Pattern Pattern { get; set; }


        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                // admin inputs is valid
                if (await _patternServicee.AddPatternAsync(Pattern) != null)
                {
                    // success
                    TempData["Success"] = "New Pattern successfully added.";
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
