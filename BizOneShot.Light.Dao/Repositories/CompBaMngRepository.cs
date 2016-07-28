using BizOneShot.Light.Dao.Infrastructure;
using BizOneShot.Light.Models.WebModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizOneShot.Light.Dao.Repositories
{
    public interface ICompBaMngRepository : IRepository<ProcCompMentorMappingReturnModel>
    {
        Task<IList<ProcCompMentorMappingReturnModel>> getCompMentorMapping(object[] parameters);
    }

    public class CompBaMngRepository : RepositoryBase<ProcCompMentorMappingReturnModel>, ICompBaMngRepository
    {
        // constructor...
        public CompBaMngRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        // methods...
        public async Task<IList<ProcCompMentorMappingReturnModel>> getCompMentorMapping(object[] parameters)
        {
            return await
                DbContext.Database.SqlQuery<ProcCompMentorMappingReturnModel>("proc_compMentorMapping @COMP_SN", parameters).ToListAsync();
        }
    }
}
