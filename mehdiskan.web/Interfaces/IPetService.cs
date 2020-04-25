﻿using mehdiskan.web.Models;
using mehdiskan.web.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mehdiskan.web.Interfaces
{
    public interface IPetService
    {
        #region Add new pet
        Pet AddPet(Pet pet, List<IFormFile> petPhotos);
        Task<Pet> AddPetAsync(Pet pet, List<IFormFile> petPhotos);
        #endregion

        #region Get pets

        PetPagingViewModel GetAllPets(int pageNumber, int pageSize);

        Task<PetPagingViewModel> GetAllPetsAsync(int pageNumber, int pageSize);

        #endregion

        #region Get pet by id

        Pet GetPetById(int petId);

        Task<Pet> GetPetByIdAsync(int petId);

        #endregion

        #region Update pet

        Pet UpdatePet(Pet pet, List<IFormFile> petPhotos);

        Task<Pet> UpdatePetAsync(Pet pet, List<IFormFile> petPhotos);

        #endregion

        #region Remove pet
        void RemovePet(int petId);

        Task RemovePetAsync(int petId);
        #endregion

        #region Search pet
        PetPagingViewModel SearchPets(string title, string code, int pageNumber, int pageSize);

        Task<PetPagingViewModel> SearchPetsAsync(string title, string code, int pageNumber, int pageSize);
        #endregion

        #region Pets count
        int PetsCount();
        Task<int> PetsCountAsync();
        #endregion
    }
}
