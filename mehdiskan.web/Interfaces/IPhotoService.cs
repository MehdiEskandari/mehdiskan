using mehdiskan.web.Models;
using mehdiskan.web.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mehdiskan.web.Interfaces
{
    public interface IPhotoService
    {
        #region Add new photo
        Photo AddPhoto(Photo photo, IFormFile imageFile);
        Task<Photo> AddPhotoAsync(Photo photo, IFormFile imageFile);
        #endregion

        #region Get photos
        PhotoPagingViewModel GetPhotos(int pageNumber, int pageSize);
        Task<PhotoPagingViewModel> GetPhotosAsync(int pageNumber, int pageSize);
        #endregion

        #region Get photo by id
        Photo GetPhotoById(int photoId);
        Task<Photo> GetPhotoByIdAsync(int photoId);
        #endregion

        #region PetSelectListItem
        List<SelectListItem> GetSelectListItems();
        Task<List<SelectListItem>> GetSelectListItemsAsync();
        #endregion

        #region Update photo
        Photo UpdatePhoto(Photo photo, IFormFile imageFile);
        Task<Photo> UpdatePhotoAsync(Photo photo, IFormFile imageFile);
        #endregion

        #region Remove photo
        void RemovePhoto(int photoId);
        Task RemovePhotoAsync(int photoId);
        #endregion

        #region Search photo
        List<Photo> SearchPhoto(string alt);
        Task<List<Photo>> SearchPhotoAsync(string alt);
        #endregion

        #region Photo count
        int PhotosCount();
        Task<int> PhotosCountAsync();
        #endregion


    }
}
