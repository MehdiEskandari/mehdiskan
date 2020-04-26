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
    public class IndexModel : PageModel
    {
        private readonly IPatternService _patternService;

        public IndexModel(IPatternService patternService)
        {
            _patternService = patternService;
        }

        // step 3: define properties
        public List<Pattern> Patterns { get; private set; }
        public int PatternCount { get; set; }

        public async Task OnGetAsync()
        {
            Patterns = await _patternService.GetPatternsAsync();
            PatternCount = await _patternService.PatternsCountAsync();
        }
    }
}
