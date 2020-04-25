using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mehdiskan.web.Interfaces;
using mehdiskan.web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace mehdiskan.web.Pages.Admin.BodyTypes
{
    public class DeleteModel : Microsoft.AspNetCore.Mvc.RazorPages.PageModel
    {
        private readonly IBodyTypeService _bodyTypeService;


        // step 1: add constructor
        public DeleteModel(IBodyTypeService bodyTypeService)
        {
            // step 2: inject ibodytype service
            _bodyTypeService = bodyTypeService;
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

        public async Task<IActionResult> OnPostAsync(int id)
        {
            // success
            TempData["Success"] = "BodyType successfully removed.";
            await _bodyTypeService.RemoveBodyTypeAsync(id);

            return RedirectToPage("Index");

        }
    }
}
