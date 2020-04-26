using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mehdiskan.web.Interfaces;
using mehdiskan.web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace mehdiskan.web.Pages.Admin.Patterns
{
    public class DeleteModel : PageModel
    {
        private readonly IPatternService _patternServicee;

        public DeleteModel(IPatternService patternService)
        {
            _patternServicee = patternService;
        }

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

        public async Task<IActionResult> OnPostAsync(int id)
        {
            // success
            TempData["Success"] = "Pattern successfully removed.";
            await _patternServicee.RemovePatternAsync(id);

            return RedirectToPage("Index");

        }
    }
}
