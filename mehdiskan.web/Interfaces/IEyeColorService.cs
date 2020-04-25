using mehdiskan.web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace mehdiskan.web.Interfaces
{
    public interface IEyeColorService
    {
        #region Add new EyeColor
        EyeColour AddEyeColor(EyeColour eyeColor);
        Task<EyeColour> AddEyeColorAsync(EyeColour eyeColor);
        #endregion

        #region Get All EyeColor
        List<EyeColour> GetAllEyeColor();
        Task<List<EyeColour>> GetAllEyeColorAsync();
        #endregion

        #region Get EyeColor by id
        EyeColour GetEyeColorById(int eyeColorId);
        Task<EyeColour> GetEyeColorByIdAsync(int eyeColorId);
        #endregion

        #region Update EyeColor
        EyeColour UpdateEyeColor(EyeColour eyeColor);
        Task<EyeColour> UpdateEyeColorAsync(EyeColour eyeColor);
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