using BizOneShot.Light.Models.WebModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BizOneShot.Light.Dao.Infrastructure;

namespace BizOneShot.Light.Dao.Repositories
{
    public interface IProcBaGetMentorRepository : IRepository<ProcBaMentorMappingReturnModel>
    {
        Task<IList<ProcBaMentorMappingReturnModel>> getBaMentorMapping(object[] parameters);
        
    }

    public class ProcBaMentorMappingRepository : RepositoryBase<ProcBaMentorMappingReturnModel>, IProcBaGetMentorRepository
    {
        public ProcBaMentorMappingRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public async Task<IList<ProcBaMentorMappingReturnModel>> getBaMentorMapping(object[] parameters)
        {
            return await DbContext.Database.SqlQuery<ProcBaMentorMappingReturnModel>("proc_baMentorMapping @LOGIN_ID", parameters).ToListAsync();
        }

      
    }
}
