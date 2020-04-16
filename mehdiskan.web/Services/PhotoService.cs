using mehdiskan.web.Data;
using mehdiskan.web.Helpers;
using mehdiskan.web.Interfaces;
using mehdiskan.web.Models;
using mehdiskan.web.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace mehdiskan.web.Services
{
    public class PhotoService : IPhotoService
    {

        private readonly ApplicationDbContext _context;
        private readonly ILogger<PhotoService> _logger;

        public PhotoService(ApplicationDbContext context, ILogger<PhotoService> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Add new photo
        /// </summary>
        /// <param name="photo"></param>
        /// <param name="imageFile"></param>
        /// <returns></returns>
        #region Add new photo
        public Photo AddPhoto(Photo photo, IFormFile imageFile)
        {
            try
            {
                photo.PhotoName = imageFile.UploadPhoto();

                _context.Photos.Add(photo);
                _context.SaveChanges();

                return photo;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.StackTrace}\n{ex.Message}");
                return null;
            }
        }

        public async Task<Photo> AddPhotoAsync(Photo photo, IFormFile imageFile)
        {
            try
            {
                photo.PhotoName = imageFile.UploadPhoto();

                await _context.Photos.AddAsync(photo);
                await _context.SaveChangesAsync();

                return photo;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.StackTrace}\n{ex.Message}");
                return null;
            }
        }
        #endregion

        /// <summary>
        /// Get photo by id from database.
        /// </summary>
        /// <param name="photoId"></param>
        /// <returns></returns>
        #region Get Photo by id
        public Photo GetPhotoById(int photoId) => _context.Photos.SingleOrDefault(g => g.PhotoId == photoId);

        public async Task<Photo> GetPhotoByIdAsync(int photoId) => await _context.Photos.SingleOrDefaultAsync(g => g.PhotoId == photoId);
        #endregion

        /// <summary>
        /// Get all Photos.
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        #region Get all Photos
        public PhotoPagingViewModel GetPhotos(int pageNumber, int pageSize)
        {
            IQueryable<Photo> photos = _context.Photos;
            int take = pageSize;
            int skip = (pageNumber - 1) * take;
            int contactCount = photos.Count();

            int pageCount = (int)Math.Ceiling(Decimal.Divide(contactCount, take));

            return new PhotoPagingViewModel
            {
                Photos = photos.Skip(skip).Take(take).OrderByDescending(p => p.PhotoId).ToList(), PageNumber = pageNumber, PageCount = pageCount
            };
        }

        public async Task<PhotoPagingViewModel> GetPhotosAsync(int pageNumber, int pageSize)
        {
            IQueryable<Photo> photos = _context.Photos;
            int take = pageSize;
            int skip = (pageNumber - 1) * take;
            int contactCount = await photos.CountAsync();

            int pageCount = (int)Math.Ceiling(Decimal.Divide(contactCount, take));

            return new PhotoPagingViewModel
            {
                Photos = await photos.Skip(skip).Take(take).OrderByDescending(p => p.PhotoId).ToListAsync(),
                PageNumber = pageNumber,
                PageCount = pageCount
            };
        }
        #endregion

        /// <summary>
        /// Remove the photo from database.
        /// </summary>
        /// <param name="photoId"></param>
        #region Remove Photo

        public void RemovePhoto(int photoId)
        {
            var photo = GetPhotoById(photoId);

            photo.RemoveUploadedFile();

            _context.Photos.Remove(photo);
            _context.SaveChanges();
        }

        public async Task RemovePhotoAsync(int photoId)
        {
            var photo = await GetPhotoByIdAsync(photoId);

            photo.RemoveUploadedFile();

            _context.Photos.Remove(photo);
            await _context.SaveChangesAsync();
        }
        #endregion

        /// <summary>
        /// Search photos based on alt
        /// </summary>
        /// <param name="alt"></param>
        /// <returns></returns>
        #region Search Photo

        public List<Photo> SearchPhoto(string alt) => _context.Photos.Where(p => p.PhotoName.TextTransform().Contains(alt.TextTransform())).ToList();

        public async Task<List<Photo>> SearchPhotoAsync(string alt)=> await _context.Photos.Where(p => p.PhotoName.TextTransform().Contains(alt.TextTransform())).ToListAsync();
        #endregion

        /// <summary>
        /// Update the photo's data from database
        /// </summary>
        /// <param name="photo"></param>
        /// <param name="imageFile"></param>
        /// <returns></returns>
        #region Update Photo

        public Photo UpdatePhoto(Photo photo, IFormFile imageFile)
        {
            try
            {
                photo.PhotoName = imageFile.UpdateUploadedPetPhoto(photo);

                _context.Photos.Update(photo);
                _context.SaveChanges();

                return photo;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.StackTrace}\n{ex.Message}");
                return null;
            }
        }

        public async Task<Photo> UpdatePhotoAsync(Photo photo, IFormFile imageFile)
        {
            try
            {
                photo.PhotoName = imageFile.UpdateUploadedPetPhoto(photo);

                _context.Photos.Update(photo);
                await _context.SaveChangesAsync();

                return photo;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.StackTrace}\n{ex.Message}");
                return null;
            }
        }
        #endregion
    }
}
