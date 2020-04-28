using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mehdiskan.web.Interfaces;
using mehdiskan.web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace mehdiskan.web.Pages.Admin.Carousels
{
    public class IndexModel : PageModel
    {
        private readonly ICarouselService _carouselService;

        public IndexModel(ICarouselService carouselService)
        {
            _carouselService = carouselService;
        }

        public IEnumerable<Carousel> Carousels { get; private set; }
        public int CarouselCount { get; private set; }

        public async Task OnGetAsync()
        {
            Carousels = await _carouselService.GetCarouselsAsync();
            CarouselCount = await _carouselService.CarouselsCountAsync();


        }
    }
}
