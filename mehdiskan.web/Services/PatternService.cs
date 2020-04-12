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
    public class PatternService:IPatternService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<PatternService> _logger;
        public PatternService(ApplicationDbContext context, ILogger<PatternService> logger)
        {
            _context = context;
            _logger = logger;
        }
        /// <summary>
        /// Add new pattern
        /// </summary>
        /// <param name="pattern"></param>
        /// <returns></returns>
        #region Add Pattern
        public Pattern AddPattern(Pattern pattern)
        {
            try
            {
                _context.Patterns.Add(pattern);
                _context.SaveChanges();

                return pattern;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.StackTrace}\n{ex.Message}");
                return null;
            }
        }

        public async Task<Pattern> AddPatternAsync(Pattern pattern)
        {
            try
            {
                await _context.Patterns.AddAsync(pattern);
                await _context.SaveChangesAsync();

                return pattern;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.StackTrace}\n{ex.Message}");
                return null;
            }
        }
        #endregion

        /// <summary>
        /// Get Pattern by id
        /// </summary>
        /// <param name="patternId"></param>
        /// <returns></returns>
        #region Get Pattern by Id
        public Pattern GetPatternById(int patternId) => _context.Patterns.SingleOrDefault(g => g.PatternId == patternId);

        public async Task<Pattern> GetPatternByIdAsync(int patternId) => await _context.Patterns.SingleOrDefaultAsync(g => g.PatternId == patternId);

        #endregion

        /// <summary>
        /// Get all Pattern
        /// </summary>
        /// <returns></returns>
        #region Get Patterns
        public List<Pattern> GetPatterns() => _context.Patterns.ToList();

        public async Task<List<Pattern>> GetPatternsAsync() => await _context.Patterns.ToListAsync();
        #endregion

        /// <summary>
        /// Remove Pattern from database
        /// </summary>
        /// <param name="patternId"></param>
        #region Remove Pattern
        public void RemovePattern(int patternId)
        {
            try
            {
                var Pattern = GetPatternById(patternId);
                _context.Patterns.Remove(Pattern);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.StackTrace}\n{ex.Message}");
            }
        }

        public async Task RemovePatternAsync(int patternId)
        {
            try
            {
                var Pattern = await GetPatternByIdAsync(patternId);
                _context.Patterns.Remove(Pattern);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.StackTrace}\n{ex.Message}");
            }
        }
        #endregion

        /// <summary>
        /// Update Pattern from database
        /// </summary>
        /// <param name="pattern"></param>
        /// <returns></returns>
        #region Update Pattern
        public Pattern UpdatePattern(Pattern pattern)
        {
            try
            {
                _context.Patterns.Update(pattern);
                _context.SaveChanges();

                return pattern;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.StackTrace}\n{ex.Message}");

                return null;
            }
        }

        public async Task<Pattern> UpdatePatternAsync(Pattern pattern)
        {
            try
            {
                _context.Patterns.Update(pattern);
                await _context.SaveChangesAsync();

                return pattern;
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
