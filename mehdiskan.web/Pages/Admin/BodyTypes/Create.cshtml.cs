using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using mehdiskan.web.Models;
using mehdiskan.web.Interfaces;

namespace mehdiskan.web.Pages.Admin.BodyTypes
{
    public class CreateModel : Microsoft.AspNetCore.Mvc.RazorPages.PageModel
    {
        private readonly IBodyTypeService _bodyTypeService;

        // step 1: add constructor
        public CreateModel(IBodyTypeService bodyTypeService)
        {
            // step 2: inject ibodytype service
            _bodyTypeService = bodyTypeService;
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
                    return Page();
                }
            }

            // invalid inputs by admin
            ViewData["Message"] = "Your inputs is not valid.";
            return Page();

        }
    }
}
