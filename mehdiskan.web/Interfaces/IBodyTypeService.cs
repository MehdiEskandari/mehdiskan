using mehdiskan.web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mehdiskan.web.Interfaces
{
    public interface IBodyTypeService
    {
        #region Add new BodyType
        BodyType AddBodyType(BodyType bodyType);
        Task<BodyType> AddBodyTypeAsync(BodyType bodyType);
        #endregion

        #region Get bodyTypes
        List<BodyType> GetBodyTypes();
        Task<List<BodyType>> GetBodyTypesAsync();
        #endregion

        #region Get BodyType by id
        BodyType GetBodyTypeById(int bodyTypeId);
        Task<BodyType> GetBodyTypeByIdAsync(int bodyTypeId);
        #endregion

        #region Update BodyType
        BodyType UpdateBodyType(BodyType bodyType);
        Task<BodyType> UpdateBodyTypeAsync(BodyType bodyType);
        #endregion

        #region Remove BodyType
        void RemoveBodyType(int bodyTypeId);
        Task RemoveBodyTypeAsync(int bodyTypeId);
        #endregion

        #region BodyTypesCount
        int BodyTypesCount();
        Task<int> BodyTypesCountAsync();
        #endregion
    }
}
