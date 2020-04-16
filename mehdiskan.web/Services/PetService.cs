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
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace mehdiskan.web.Services
{
    public class PetService: IPetService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<PetService> _logger;

        public PetService(ApplicationDbContext context, ILogger<PetService> logger)
        {
            _context= context;
            _logger = logger;
        }

        /// <summary>
        /// Add new pet
        /// </summary>
        /// <param name="pet"></param>
        /// <param name="petPhotos"></param>
        /// <returns></returns>
        #region Add new pet


        public Pet AddPet(Pet pet, List<IFormFile> petPhotos)
        {
            try
            {
                _context.Pets.Add(pet);
                _context.SaveChanges();

                foreach (var imageName in UploadProductImages(petPhotos))
                {
                    _context.Photos.Add(new Photo
                    {
                        PetId = pet.PetId,
                        PhotoName = imageName
                    });
                   
                    _context.SaveChanges();
                }

                return pet;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.StackTrace}\n{ex.Message}");

                return null;
            }
        }

        public async Task<Pet> AddPetAsync(Pet pet, List<IFormFile> petPhotos)
        {
            try
            {
                _context.Pets.Add(pet);
                await _context.SaveChangesAsync();

                foreach (var imageName in UploadProductImages(petPhotos))
                {
                    await _context.Photos.AddAsync(new Photo
                    {
                        PetId = pet.PetId,
                        PhotoName = imageName
                    });
                    await _context.SaveChangesAsync();
                }

                

                return pet;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.StackTrace}\n{ex.Message}");

                return null;
            }
        }

        #endregion

        /// <summary>
        /// Get all pets
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        #region Get all pets
        public PetPagingViewModel GetAllPets(int pageNumber = 1, int pageSize = 32)
        {
            IQueryable<Pet> pets = _context.Pets;

            int take = pageSize;
            int skip = (pageNumber - 1) * take;
            int petsCount = pets.Count();

            int pageCount = (int)Math.Ceiling(Decimal.Divide(petsCount, take));

            return new PetPagingViewModel
            {
                Pets = pets
                .Skip(skip)
                .Take(take)
                .Where(p => p.IsDelete == false)
                .OrderByDescending(p => p.PetId).ToList(),
                PageNumber = pageNumber,
                PageCount = pageCount
            };
        }

        public async Task<PetPagingViewModel> GetAllPetsAsync(int pageNumber = 1, int pageSize = 32)
        {
            IQueryable<Pet> pets = _context.Pets;

            int take = pageSize;
            int skip = (pageNumber - 1) * take;
            int petsCount = await pets.CountAsync();

            int pageCount = (int)Math.Ceiling(Decimal.Divide(petsCount, take));

            return new PetPagingViewModel
            {
                Pets = await pets
                .Skip(skip)
                .Take(take)
                .Where(p => p.IsDelete == false)
                .OrderByDescending(p => p.PetId)
                .ToListAsync(),
                PageNumber = pageNumber,
                PageCount = pageCount
            };
        }
        #endregion

        /// <summary>
        /// Get pet by id
        /// </summary>
        /// <param name="petId"></param>
        /// <returns></returns>
        #region Get pet by id
        public Pet GetPetById(int petId) => _context.Pets.SingleOrDefault(p => p.PetId == petId);

        public async Task<Pet> GetPetByIdAsync(int petId) => await _context.Pets.SingleOrDefaultAsync(p => p.PetId == petId);
        #endregion

        /// <summary>
        /// Pets count on database.
        /// </summary>
        /// <returns></returns>
        #region Pets count
        public int PetsCount() => _context.Pets.Where(p => p.IsDelete == false).Count();

        public async Task<int> PetsCountAsync() => await _context.Pets.Where(p => p.IsDelete == false).CountAsync();
        #endregion

        /// <summary>
        /// Remove pet by id from database.
        /// </summary>
        /// <param name="petId"></param>
        #region Remove pet
        public void RemovePet(int petId)
        {
            RemoveProductImages(petId);

            var pet = GetPetById(petId);

            pet.IsDelete = true;

            _context.Pets.Update(pet);
            _context.SaveChanges();
        }

        public async Task RemovePetAsync(int petId)
        {
            RemoveProductImages(petId);

            var pet = await GetPetByIdAsync(petId);

            pet.IsDelete = true;

            _context.Pets.Update(pet);
            await _context.SaveChangesAsync();
        }
        #endregion

        /// <summary>
        /// Search pets based on title and code.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="code"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        #region Search Pets
        public PetPagingViewModel SearchPets(string title, string code, int pageNumber =1, int pageSize=30)
        {
            IQueryable<Pet> pets = _context.Pets.Where(p => p.Title.TextTransform().Contains(title.TextTransform()) || p.Code.TextTransform().Contains(code.TextTransform()));
            int take = pageSize;
            int skip = (pageNumber - 1) * take;
            int contactCount = pets.Count();

            int pageCount = (int)Math.Ceiling(Decimal.Divide(contactCount, take));

            return new PetPagingViewModel
            {
                Pets = pets.Skip(skip).Take(take).OrderByDescending(p => p.PetId).ToList(),
                PageNumber = pageNumber,
                PageCount = pageCount
            };
        }

        public async Task<PetPagingViewModel> SearchPetsAsync(string title, string code, int pageNumber=1, int pageSize=30)
        {
            IQueryable<Pet> pets = _context.Pets.Where(p => p.Title.TextTransform().Contains(title.TextTransform()) || p.Code.TextTransform().Contains(code.TextTransform()));
            int take = pageSize;
            int skip = (pageNumber - 1) * take;
            int contactCount = await pets.CountAsync();

            int pageCount = (int)Math.Ceiling(Decimal.Divide(contactCount, take));

            return new PetPagingViewModel
            {
                Pets = await pets.Skip(skip).Take(take).OrderByDescending(p => p.PetId).ToListAsync(),
                PageNumber = pageNumber,
                PageCount = pageCount
            };
        }
        #endregion

        #region Update Pet

        public Pet UpdatePet(Pet pet, List<IFormFile> petPhotos)
        {
            try
            {
                _context.Pets.Update(pet);
                _context.SaveChanges();

                if (petPhotos != null)
                {
                    foreach (var imageName in UploadProductImages(petPhotos))
                    {
                        _context.Photos.Add(new Photo
                        {
                            PetId = pet.PetId,
                            PhotoName = imageName
                        });
                        _context.SaveChanges();
                    }
                }

                return pet;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.StackTrace}\n{ex.Message}");

                return null;
            }
        }

        public async Task<Pet> UpdatePetAsync(Pet pet, List<IFormFile> petPhotos)
        {
            try
            {
                _context.Pets.Update(pet);
                await _context.SaveChangesAsync();

                if (petPhotos != null)
                {
                    foreach (var imageName in UploadProductImages(petPhotos))
                    {
                        await _context.Photos.AddAsync(new Photo
                        {
                            PetId = pet.PetId,
                            PhotoName = imageName
                        });
                        await _context.SaveChangesAsync();
                    }
                }

                return pet;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.StackTrace}\n{ex.Message}");

                return null;
            }
        }
        #endregion

        #region Helpers

        private List<string> UploadProductImages(List<IFormFile> productImages)
        {
            if (productImages != null)
            {

                List<string> productImagesList = new List<string>();

                foreach (var productImage in productImages)
                {
                    string productImageName = Guid.NewGuid() + Path.GetExtension(productImage.FileName);

                    string productPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/thumbnails/", productImageName);

                    using (var fileStream = new FileStream(productPath, FileMode.Create))
                    {
                        productImage.CopyTo(fileStream);
                    }

                    productImagesList.Add(productImageName);
                }

                return productImagesList;
            }

            return new List<string> { "default.png" };
        }

        private List<string> UpdateAndUploadProductImages(Pet pet, List<IFormFile> productImages)
        {
            List<string> oldProductImages = GetProductImageNames(pet.PetId);

            foreach (var productImage in oldProductImages)
            {
                if (productImage.TextTransform() != "default.png")
                {
                    string productPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/thumbnails/", productImage);

                    if (new FileInfo(productPath).Exists)
                    {
                        new FileInfo(productPath).Delete();
                    }
                }
            }

            List<string> productImagesList = new List<string>();

            foreach (var productImage in productImages)
            {
                string productImageName = Guid.NewGuid() + Path.GetExtension(productImage.FileName);

                string productPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/thumbnails/", productImageName);

                using (var fileStream = new FileStream(productPath, FileMode.Create))
                {
                    productImage.CopyTo(fileStream);
                }

                productImagesList.Add(productImageName);
            }

            return productImagesList;
        }

        public void RemoveProductImages(int petId)
        {
            var oldPetImages = _context.Photos
                .Where(p => p.PetId == petId);

            if (oldPetImages.Count() > 0)
            {
                foreach (var productImage in oldPetImages.Select(p => p.PhotoName))
                {
                    if (productImage != "default.png")
                    {
                        string productPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/thumbnails/", productImage);

                        if (new FileInfo(productPath).Exists)
                        {
                            new FileInfo(productPath).Delete();
                        }
                    }
                }

                foreach (var oldPetImage in oldPetImages)
                {
                    _context.Photos.Remove(oldPetImage);
                }

                _context.SaveChanges();
            }
        }

        private List<string> GetProductImageNames(int petId)
        {
            return _context.Photos
                   .Where(p => p.PetId == petId)
                   .Select(p => p.PhotoName)
                   .ToList();
        }

        #endregion
    }
}
