using mehdiskan.web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mehdiskan.web.Interfaces
{
    public interface IPatternService
    {
        #region Add Pattern
        Pattern AddPattern(Pattern pattern);
        Task<Pattern> AddPatternAsync(Pattern pattern);

        #endregion

        #region Get Patterns
        List<Pattern> GetPatterns();
        Task<List<Pattern>> GetPatternsAsync();

        #endregion

        #region Get Pattern by id
        Pattern GetPatternById(int patternId);
        Task<Pattern> GetPatternByIdAsync(int patternId);

        #endregion


        #region Update Pattern
        Pattern UpdatePattern(Pattern pattern);
        Task<Pattern> UpdatePatternAsync(Pattern pattern);
        #endregion

        #region Remove Pattern
        void RemovePattern(int patternId);
        Task RemovePatternAsync(int patternId);
        #endregion
    }
}
