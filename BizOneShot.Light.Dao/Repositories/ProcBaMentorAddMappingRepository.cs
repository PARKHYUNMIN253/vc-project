using BizOneShot.Light.Dao.Infrastructure;
using BizOneShot.Light.Models.WebModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizOneShot.Light.Dao.Repositories
{
    public interface IProcBaMentorAddMappingRepository : IRepository<ProcBaMentorAddMappingReturnModel>
    {
        Task<IList<ProcBaMentorAddMappingReturnModel>> getBaMentorNonMapping(object[] parameters);
    }

    public class ProcBaMentorAddMappingRepository : RepositoryBase<ProcBaMentorAddMappingReturnModel>, IProcBaMentorAddMappingRepository
    {
        public ProcBaMentorAddMappingRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public async Task<IList<ProcBaMentorAddMappingReturnModel>> getBaMentorNonMapping(object[] parameters)
        {
            return await DbContext.Database.SqlQuery<ProcBaMentorAddMappingReturnModel>("proc_baMentorAddMapping @LOGIN_ID", parameters).ToListAsync();
        }
    }
}
