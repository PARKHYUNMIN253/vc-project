using BizOneShot.Light.Dao.Infrastructure;
using BizOneShot.Light.Models.WebModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizOneShot.Light.Dao.Repositories
{
    public interface IProcBaGetReportCompInfoRepository : IRepository<ProcBaGetReportCompInfoReturnModel>
    {
        Task<IList<ProcBaGetReportCompInfoReturnModel>> getBaReportCompMapping(object[] parameters);
    }

    public class ProcBaGetReportCompInfoRepository : RepositoryBase<ProcBaGetReportCompInfoReturnModel>, IProcBaGetReportCompInfoRepository
    {
        public ProcBaGetReportCompInfoRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }


        public async Task<IList<ProcBaGetReportCompInfoReturnModel>> getBaReportCompMapping(object[] parameters)
        {
            return await DbContext.Database.SqlQuery<ProcBaGetReportCompInfoReturnModel>("proc_baGetReportCompInfo @LOGIN_ID", parameters).ToListAsync();
        }
    }
}
