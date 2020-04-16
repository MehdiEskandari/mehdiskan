﻿using mehdiskan.web.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace mehdiskan.web.Interfaces
{
    interface ICarouselService
    {
        #region Add new carousel
        Carousel AddCarousel(Carousel carousel, IFormFile carouselFile);
        Task<Carousel> AddCarouselAsync(Carousel carousel, IFormFile carouselFile);
        #endregion

        #region Get carousels
        IEnumerable<Carousel> GetCarousels();
        Task<IEnumerable<Carousel>> GetCarouselsAsync();
        #endregion

        #region Get carousel by id
        Carousel GetCarouselById(int carouselId);
        Task<Carousel> GetCarouselByIdAsync(int carouselId);
        #endregion

        #region Update carousel
        Carousel UpdateCarousel(Carousel carousel, IFormFile carouselFile);
        Task<Carousel> UpdateCarouselAsync(Carousel carousel, IFormFile carouselFile);
        #endregion

        #region Remove carousel
        void RemoveCarousel(int carouselId);
        Task RemoveCarouselAsync(int carouselId);
        #endregion

    }
}
