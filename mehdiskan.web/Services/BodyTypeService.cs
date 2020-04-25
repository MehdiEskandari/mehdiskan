using mehdiskan.web.Data;
using mehdiskan.web.Interfaces;
using mehdiskan.web.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mehdiskan.web.Services
{
    public class BodyTypeService : IBodyTypeService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<BodyTypeService> _logger;
        public BodyTypeService(ApplicationDbContext context, ILogger<BodyTypeService> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Add new BodyType to database
        /// </summary>
        /// <param name="bodyType"></param>
        /// <returns>bodyType or Null</returns>
        #region Add new BodyType
        public BodyType AddBodyType(BodyType bodyType)
        {
            try
            {
                _context.BodyTypes.Add(bodyType);
                _context.SaveChanges();

                return bodyType;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.StackTrace}\n{ex.Message}");
                return null;
            }
        }

        public async Task<BodyType> AddBodyTypeAsync(BodyType bodyType)
        {
            try
            {
                await _context.BodyTypes.AddAsync(bodyType);
                await _context.SaveChangesAsync();

                return bodyType;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.StackTrace}\n{ex.Message}");
                return null;
            }
        }
        #endregion

        /// <summary>
        /// Get BodyType by Id from database
        /// </summary>
        /// <param name="bodyTypeId"></param>
        /// <returns></returns>
        #region Get BodyType by Id
        public BodyType GetBodyTypeById(int bodyTypeId) => _context.BodyTypes.SingleOrDefault(e=> e.BodyTypeId == bodyTypeId);

        public async Task<BodyType> GetBodyTypeByIdAsync(int bodyTypeId) => await _context.BodyTypes.SingleOrDefaultAsync(e =>e.BodyTypeId == bodyTypeId);

        #endregion


        /// <summary>
        /// Get all BodyTypes 
        /// </summary>
        /// <returns></returns>
        #region Get all BodyType
        public List<BodyType> GetBodyTypes() => _context.BodyTypes.ToList();
        public async Task<List<BodyType>> GetBodyTypesAsync() => await _context.BodyTypes.ToListAsync();
        #endregion

        /// <summary>
        /// Remove the BodyType from database
        /// </summary>
        /// <param name="bodyTypeId"></param>
        #region Remove BodyType
        public void RemoveBodyType(int bodyTypeId)
        {
            var bodyType = GetBodyTypeById(bodyTypeId);
            _context.BodyTypes.Remove(bodyType);
            _context.SaveChanges();
        }

        public async Task RemoveBodyTypeAsync(int bodyTypeId)
        {
            var bodyType = await GetBodyTypeByIdAsync(bodyTypeId);
            _context.BodyTypes.Remove(bodyType);
            await _context.SaveChangesAsync();
        }
        #endregion

        /// <summary>
        /// Update the BodyTypes data from database
        /// </summary>
        /// <param name="bodyType"></param>
        /// <returns></returns>
        #region Update BodyType
        public BodyType UpdateBodyType(BodyType bodyType)
        {
            try
            {
                _context.BodyTypes.Update(bodyType);
                _context.SaveChanges();

                return bodyType;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.StackTrace}\n{ex.Message}");
                return null;
            }
        }

        public async Task<BodyType> UpdateBodyTypeAsync(BodyType bodyType)
        {
            try
            {
                _context.BodyTypes.Update(bodyType);
                await _context.SaveChangesAsync();

                return bodyType;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.StackTrace}\n{ex.Message}");
                return null;
            }
        }
        #endregion

        /// <summary>
        /// BodyTypes Count
        /// </summary>
        /// <returns></returns>
        #region BodyType Count

        public int BodyTypesCount() => _context.BodyTypes.Count();

        public async Task<int> BodyTypesCountAsync()=> await _context.BodyTypes.CountAsync();
        #endregion

    }
}
