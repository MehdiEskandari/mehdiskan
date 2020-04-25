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
    public class IndexModel : Microsoft.AspNetCore.Mvc.RazorPages.PageModel
    {
        private readonly IBodyTypeService _bodyTypeService;

        // step 1: add constuctor
        public IndexModel(IBodyTypeService bodyTypeService)
        {
            // step 2: inject ibodytype servive
            _bodyTypeService = bodyTypeService;
        }

        // step 3: create property
        public List<BodyType> BodyTypes { get; private set; }
        public int BodyTypesCount { get; set; }

        public async Task OnGet()
        {
            // step 4: feed BodyTypes property with data in database.
            BodyTypes = await _bodyTypeService.GetBodyTypesAsync();
            BodyTypesCount = await _bodyTypeService.BodyTypesCountAsync();
        }
    }
}
