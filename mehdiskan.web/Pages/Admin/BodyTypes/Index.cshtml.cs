

using mehdiskan.web.Interfaces;
using mehdiskan.web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace mehdiskan.web.Pages.Admin.BodyTypes
{
    public class IndexModel : PageModel
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
