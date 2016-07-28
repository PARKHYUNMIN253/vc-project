using BizOneShot.Light.Dao.Infrastructure;
using BizOneShot.Light.Models.WebModels;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizOneShot.Light.Dao.Repositories
{
    public interface IProcCompGetDeepenReportListRepository : IRepository<ProcCompGetDeepenReportListReturnModel>
    {
        IPagedList<ProcCompGetDeepenReportListReturnModel> getDeepenReportList(object[] parameters, int page, int pageSize);
        IList<ProcCompGetDeepenReportListReturnModel> getDeepenReportListR(object[] parameters);
        Task<IList<ProcCompGetDeepenReportListReturnModel>> getDeepenReportListL(object[] parameters);
    }
    public class ProcCompGetDeepenReportListRepository : RepositoryBase<ProcCompGetDeepenReportListReturnModel>, IProcCompGetDeepenReportListRepository
    {
        public ProcCompGetDeepenReportListRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IPagedList<ProcCompGetDeepenReportListReturnModel> getDeepenReportList(object[] parameters, int page, int pageSize)
        {
            return
                DbContext.Database.SqlQuery<ProcCompGetDeepenReportListReturnModel>
                ("proc_compGetDeepenReportList @COMP_SN", parameters)
                .OrderByDescending(obj => obj.TOTAL_REPORT_SN)
                .ToPagedList(page, pageSize);
        }

        public IList<ProcCompGetDeepenReportListReturnModel> getDeepenReportListR(object[] parameters)
        {
            return
                    DbContext.Database.SqlQuery<ProcCompGetDeepenReportListReturnModel>
                    ("proc_compGetDeepenReportList @COMP_SN", parameters)
                    .OrderByDescending(obj => obj.TOTAL_REPORT_SN)
                    .ToList();
        }

        public async Task<IList<ProcCompGetDeepenReportListReturnModel>> getDeepenReportListL(object[] parameters)
        {
            return await DbContext.Database.SqlQuery<ProcCompGetDeepenReportListReturnModel>("proc_compGetDeepenReportList @COMP_SN", parameters).ToListAsync();
        }
    }
}
