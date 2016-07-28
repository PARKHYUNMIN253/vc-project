using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BizOneShot.Light.Dao.Infrastructure;
using BizOneShot.Light.Models.WebModels;

namespace BizOneShot.Light.Dao.Repositories
{
    public interface IScMentorMappingRepository : IRepository<VcMentorMapping>
    {
        Task<IList<VcMentorMapping>> GetMentorMappingsInScUsrAsync(Expression<Func<VcMentorMapping, bool>> where);
        Task<IList<VcMentorMapping>> GetMentorMappingsAsync(Expression<Func<VcMentorMapping, bool>> where);
        Task<VcMentorMapping> GetMentorMappingAsync(Expression<Func<VcMentorMapping, bool>> where);
    }


    public class ScMentorMappingRepository : RepositoryBase<VcMentorMapping>, IScMentorMappingRepository
    {
        public ScMentorMappingRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }


        public async Task<IList<VcMentorMapping>> GetMentorMappingsInScUsrAsync(
            Expression<Func<VcMentorMapping, bool>> where)
        {
            return await DbContext.VcMentorMappings
                //.Include(mm => mm.ScUsr)   추가해야함 2016-0512
                .Where(where).ToListAsync();
        }

        public async Task<IList<VcMentorMapping>> GetMentorMappingsAsync(Expression<Func<VcMentorMapping, bool>> where)
        {
            return await DbContext.VcMentorMappings
                .Include("VcBizWork")
                .Include("VcUsrInfo")
                //.Include("VcUsrInfo.ScUsrResume.ScFileInfo")
                .Where(where).ToListAsync();
        }

        public async Task<VcMentorMapping> GetMentorMappingAsync(Expression<Func<VcMentorMapping, bool>> where)
        {
            return
                await
                    DbContext.VcMentorMappings.Include("VcBizWork")
                        .Include("VcUsrInfo")
                        //.Include("ScUsr.ScUsrResume.ScFileInfo")
                        .Where(where)
                        .SingleAsync();
        }
    }
}