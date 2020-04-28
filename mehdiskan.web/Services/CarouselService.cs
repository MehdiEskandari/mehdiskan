using mehdiskan.web.Data;
using mehdiskan.web.Helpers;
using mehdiskan.web.Interfaces;
using mehdiskan.web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace mehdiskan.web.Services
{
    public class CarouselService : ICarouselService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CarouselService> _logger;

        public CarouselService(ApplicationDbContext context, ILogger<CarouselService> logger)
        {
            _context = context;
            _logger = logger;
        }


        /// <summary>
        /// Add new carousel to database
        /// </summary>
        /// <param name="carousel"></param>
        /// <param name="carouselFile"></param>
        /// <returns></returns>
        #region Add new carousel
        public Carousel AddCarousel(Carousel carousel, IFormFile carouselFile)
        {
            try
            {
                carousel.ImageName = carouselFile.UploadPhoto("carousels");

                _context.Carousels.Add(carousel);
                _context.SaveChanges();

                return carousel;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.StackTrace}\n{ex.Message}");
                return null;
            }
        }

        public async Task<Carousel> AddCarouselAsync(Carousel carousel, IFormFile carouselFile)
        {
            try
            {
                carousel.ImageName = carouselFile.UploadPhoto("carousels");

                await _context.Carousels.AddAsync(carousel);
                await _context.SaveChangesAsync();

                return carousel;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.StackTrace}\n{ex.Message}");
                return null;
            }
        }

        #endregion


        /// <summary>
        /// Get carousel by id from database.
        /// </summary>
        /// <param name="carouselId"></param>
        /// <returns></returns>
        #region Get carousel by id
        public Carousel GetCarouselById(int carouselId) => _context.Carousels.SingleOrDefault(g => g.CarouselId == carouselId);

        public async Task<Carousel> GetCarouselByIdAsync(int carouselId) => await _context.Carousels.SingleOrDefaultAsync(g => g.CarouselId == carouselId);

        #endregion




        /// <summary>
        /// Get all carousels.
        /// </summary>
        /// <returns></returns>
        #region Get carousels

        public IEnumerable<Carousel> GetCarousels() => _context.Carousels.ToList();

        public async Task<IEnumerable<Carousel>> GetCarouselsAsync() => await _context.Carousels.ToListAsync();
 

        #endregion



        #region Remove carousel

        public void RemoveCarousel(int carouselId)
        {
            var carousel = GetCarouselById(carouselId);

            RemoveDeletedCarousel(carousel);

            _context.Carousels.Remove(carousel);
            _context.SaveChanges();
        }

        public async Task RemoveCarouselAsync(int carouselId)
        {
            var carousel = await GetCarouselByIdAsync(carouselId);

            RemoveDeletedCarousel(carousel);

            _context.Carousels.Remove(carousel);
            await _context.SaveChangesAsync();
        }
        #endregion



        #region Updaate carousel
        public Carousel UpdateCarousel(Carousel carousel, IFormFile carouselFile)
        {
            try
            {
                carousel.ImageName = carouselFile.UpdateUploadedCarouselPhoto(carousel);

                _context.Carousels.Update(carousel);
                _context.SaveChanges();

                return carousel;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.StackTrace}\n{ex.Message}");

                return null;
            }
        }


        public async Task<Carousel> UpdateCarouselAsync(Carousel carousel, IFormFile carouselFile)
        {
            try
            {
                carousel.ImageName = carouselFile.UpdateUploadedCarouselPhoto(carousel);

                _context.Carousels.Update(carousel);
                await _context.SaveChangesAsync();

                return carousel;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.StackTrace}\n{ex.Message}");

                return null;
            }
        }
        #endregion

        #region Carousels Count


        public int CarouselsCount() => _context.Carousels.Count();

        public async Task<int> CarouselsCountAsync() => await _context.Carousels.CountAsync();

        #endregion

        #region Helpper
        private void RemoveDeletedCarousel(Carousel carousel)
        {
            string carouselToDeletePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/carousel", carousel.ImageName);
            if (File.Exists(carouselToDeletePath))
            {
                File.Delete(carouselToDeletePath);
            }
        }
        #endregion
    }
}
