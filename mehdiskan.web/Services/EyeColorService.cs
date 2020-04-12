using mehdiskan.web.Data;
using mehdiskan.web.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mehdiskan.web.Services
{
    public class EyeColorService : IEyeColorService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<EyeColorService> _logger;
        public EyeColorService(ApplicationDbContext context, ILogger<EyeColorService> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Add new EyeColor to database
        /// </summary>
        /// <param name="eyeColor"></param>
        /// <returns></returns>
        #region Add new EyeColor
        public EyeColor AddEyeColor(EyeColor eyeColor)
        {
            try
            {
                _context.EyeColors.Add(eyeColor);
                _context.SaveChanges();


                return eyeColor;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.StackTrace}\n{ex.Message}");
                return null;
            }
        }

        public async Task<EyeColor> AddEyeColorAsync(EyeColor eyeColor)
        {
            try
            {
                await _context.EyeColors.AddAsync(eyeColor);
                await _context.SaveChangesAsync();


                return eyeColor;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.StackTrace}\n{ex.Message}");
                return null;
            }
        }

        #endregion

        /// <summary>
        /// Get all eyeColors.
        /// </summary>
        /// <returns></returns>
        #region Get EyeColors
        public List<EyeColor> GetAllEyeColor() => _context.EyeColors.ToList();
        public async Task<List<EyeColor>> GetAllEyeColorAsync() => await _context.EyeColors.ToListAsync();
        #endregion

        /// <summary>
        /// Get EyeColor by id from database.
        /// </summary>
        /// <param name="eyeColorId"></param>
        /// <returns></returns>
        #region Get EyeColor by id
        public EyeColor GetEyeColorById(int eyeColorId) => _context.EyeColors.SingleOrDefault(g => g.EyeColorId == eyeColorId);
        public async Task<EyeColor> GetEyeColorByIdAsync(int eyeColorId) => await _context.EyeColors.SingleOrDefaultAsync(g => g.EyeColorId == eyeColorId);
        #endregion

        /// <summary>
        /// Remove EyeColor from database
        /// </summary>
        /// <param name="eyeColorId"></param>
        #region Remove EyeColor
        public void RemoveEyeColor(int eyeColorId)
        {
            var EyeColor = GetEyeColorById(eyeColorId);

            _context.EyeColors.Remove(EyeColor);
            _context.SaveChanges();
        }

        public async Task RemoveEyeColorAsync(int eyeColorId)
        {
            var EyeColor = await GetEyeColorByIdAsync(eyeColorId);
            

            _context.EyeColors.Remove(EyeColor);
            await _context.SaveChangesAsync();
        }
        #endregion

        /// <summary>
        /// Update database
        /// </summary>
        /// <param name="eyeColor"></param>
        /// <returns></returns>
        #region Update EyeColor
        public EyeColor UpdateEyeColor(EyeColor eyeColor)
        {
            try
            {
                _context.EyeColors.Update(eyeColor);
                _context.SaveChanges();

                return eyeColor;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.StackTrace}\n{ex.Message}");
                return null;
            }
        }

        public async Task<EyeColor> UpdateEyeColorAsync(EyeColor eyeColor)
        {
            try
            {
                _context.EyeColors.Update(eyeColor);
                await _context.SaveChangesAsync();

                return eyeColor;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.StackTrace}\n{ex.Message}");
                return null;
            }
        }
        #endregion

        /// <summary>
        /// EyeColors Count.
        /// </summary>
        /// <returns></returns>
        #region EyeColor Count

        public int EyeColorsCount() => _context.EyeColors.Count();

        public async Task<int> EyeColorsCountAsync() => await _context.EyeColors.CountAsync();
        #endregion


    }
}
