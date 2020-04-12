using mehdiskan.web.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mehdiskan.web.Interfaces
{
    interface IEyeColorService
    {
        #region Add new EyeColor
        EyeColor AddEyeColor(EyeColor eyeColor);
        Task<EyeColor> AddEyeColorAsync(EyeColor eyeColor);
        #endregion

        #region Get All EyeColor
        List<EyeColor> GetAllEyeColor();
        Task<List<EyeColor>> GetAllEyeColorAsync();
        #endregion

        #region Get EyeColor by id
        EyeColor GetEyeColorById(int eyeColorId);
        Task<EyeColor> GetEyeColorByIdAsync(int eyeColorId);
        #endregion

        #region Update EyeColor
        EyeColor UpdateEyeColor(EyeColor eyeColor);
        Task<EyeColor> UpdateEyeColorAsync(EyeColor eyeColor);
        #endregion

        #region Remove EyeColor
        void RemoveEyeColor(int eyeColorId);
        Task RemoveEyeColorAsync(int eyeColorId);
        #endregion

        #region EyeColorCount
        int EyeColorsCount();
        Task<int> EyeColorsCountAsync();
        #endregion

    }
}
