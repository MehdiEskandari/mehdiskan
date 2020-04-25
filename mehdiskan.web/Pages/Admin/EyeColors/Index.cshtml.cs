using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mehdiskan.web.Interfaces;
using mehdiskan.web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace mehdiskan.web.Pages.Admin.EyeColors
{
    public class IndexModel : PageModel
    {
        private readonly IEyeColorService _eyeColorService;

        // step 1: add constructor
        public IndexModel(IEyeColorService eyeColorService)
        {
            // step 2: inject iEyeColor service
            _eyeColorService = eyeColorService;
        }

        // step 3: Create Property
        public List<EyeColour> EyeColors { get; private set; }
        public int EyeColorsCount { get; set; }



        public async Task OnGet()
        {
            // stepp 4: feed EyeColor property with data in database.
            EyeColors = await _eyeColorService.GetAllEyeColorAsync();
            EyeColorsCount = await _eyeColorService.EyeColorsCountAsync();

        }
    }
}
