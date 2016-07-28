using BizOneShot.Light.Dao.Infrastructure;
using BizOneShot.Light.Models.WebModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizOneShot.Light.Dao.Repositories
{
    public interface IProcBaGetReportListRepository : IRepository<ProcBaGetReportListReturnModel>
    {
        Task<IList<ProcBaGetReportListReturnModel>> baGetReportList(object[] parameters);
    }

    public class ProcBaGetReportListRepository : RepositoryBase<ProcBaGetReportListReturnModel>, IProcBaGetReportListRepository
    {
        public ProcBaGetReportListRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }


        public async Task<IList<ProcBaGetReportListReturnModel>> baGetReportList(object[] parameters)
        {
            return await DbContext.Database.SqlQuery<ProcBaGetReportListReturnModel>("proc_baGetReportList @LOGIN_ID", parameters).ToListAsync();
        }
    }
}
