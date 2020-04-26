
using mehdiskan.web.Interfaces;
using mehdiskan.web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace mehdiskan.web.Pages.Admin.BodyTypes
{
    public class EditModel : PageModel
    {
        private readonly IBodyTypeService _bodyTypeService;
        private readonly ILogger<EditModel> _logger;

        // step 1: add constructor
        public EditModel(IBodyTypeService bodyTypeService, ILogger<EditModel> logger)
        {
            // step 2: inject ibodytype service
            _bodyTypeService = bodyTypeService;
            _logger = logger;
        }

        // step 3: create bodytype property
        [BindProperty]
        public BodyType BodyType { get; set; }

        // step 4: get bodytype by id  
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            BodyType = await _bodyTypeService.GetBodyTypeByIdAsync(id.Value);

            if (BodyType == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        { 
            if (ModelState.IsValid)
            {
                // admin inputs is valid
                if (await _bodyTypeService.UpdateBodyTypeAsync(BodyType) != null)
                {
                    // success
                    TempData["Success"] = "BodyType successfully edited.";
                    return RedirectToPage("Index");
                }
                else
                {
                    // db failure
                    ViewData["Message"] = "Database error, contact the developer to fix the error.";

                    _logger.LogError($"BodyType {nameof(EditModel)} database error.");

                    return Page();
                }
            }

            // invalid inputs by admin
            ViewData["Message"] = "Your inputs is not valid.";

            _logger.LogError($"BodyType {nameof(EditModel)} invalid input.");

            return Page();

        }
    }
}
