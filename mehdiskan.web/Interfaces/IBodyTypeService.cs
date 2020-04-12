using mehdiskan.web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mehdiskan.web.Interfaces
{
    interface IBodyTypeService
    {
        #region Add new BodyType
        BodyType AddBodyType(BodyType bodyType);
        Task<BodyType> AddBodyTypeAsync(BodyType bodyType);
        #endregion

        #region Get BodyTypes
        IEnumerable<BodyType> GetAllBodyTypes();
        Task<IEnumerable<BodyType>> GetAllBodyTypesAsync();
        #endregion

        #region Get BodyType by id
        BodyType BodyType(int bodyTypeId);
        Task<BodyType> BodyTypeAsync(int bodyTypeId);
        #endregion

        #region Update BodyType
        BodyType UpdateBodyType(BodyType bodyType);
        Task<BodyType> UpdateBodyTypeAsync(BodyType bodyType);
        #endregion

        #region Remove BodyType
        void RemoveBodyType(int bodyTypeId);
        Task RemoveBodyTypeAsync(int bodyTypeId);

        #endregion

        #region BodyType count
        int BodyTypeCount();
        Task<int> BodyTypeCountAsync();
        #endregion
    }
}
