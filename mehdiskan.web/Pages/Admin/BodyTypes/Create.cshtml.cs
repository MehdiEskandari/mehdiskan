using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using mehdiskan.web.Models;
using mehdiskan.web.Interfaces;
using Microsoft.Extensions.Logging;

namespace mehdiskan.web.Pages.Admin.BodyTypes
{
    public class CreateModel : PageModel
    {
        private readonly IBodyTypeService _bodyTypeService;
        private readonly ILogger<CreateModel> _logger;

        // step 1: add constructor
        public CreateModel(IBodyTypeService bodyTypeService, ILogger<CreateModel> logger)
        {
            // step 2: inject ibodytype service
            _bodyTypeService = bodyTypeService;
            _logger = logger;
        }

        // step 3: create bodytype property
        [BindProperty]
        public BodyType BodyType { get; set; }


        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                // admin inputs is valid
                if (await _bodyTypeService.AddBodyTypeAsync(BodyType) != null)
                {
                    // success
                    TempData["Success"] = "New BodyType successfully added.";
                    return RedirectToPage("Index");
                }
                else
                {
                    // db failure
                    ViewData["Message"] = "Database error, contact the developer to fix the error.";

                    _logger.LogError($"BodyType {nameof(CreateModel)} database error.");

                    return Page();
                }
            }

            // invalid inputs by admin
            ViewData["Message"] = "Your inputs is not valid.";

            _logger.LogError($"BodyType {nameof(CreateModel)} invalid input");

            return Page();

        }
    }
}
