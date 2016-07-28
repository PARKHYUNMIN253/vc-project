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
    public interface IProcBaGetDeepenReportRepository : IRepository<ProcBaGetDeepenReportReturnModel>
    {
        Task<IList<ProcBaGetDeepenReportReturnModel>> getBaDeepenReport(object[] parameters);
    }

    public class ProcBaGetDeepenReportRepository : RepositoryBase<ProcBaGetDeepenReportReturnModel>, IProcBaGetDeepenReportRepository
    {
        public ProcBaGetDeepenReportRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public async Task<IList<ProcBaGetDeepenReportReturnModel>> getBaDeepenReport(object[] parameters)
        {
            return await DbContext.Database.SqlQuery<ProcBaGetDeepenReportReturnModel>("proc_baGetDeepenReport @LOGIN_ID", parameters).ToListAsync();
        }
    }
}
