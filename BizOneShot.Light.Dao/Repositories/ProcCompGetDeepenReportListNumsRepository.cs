using BizOneShot.Light.Dao.Infrastructure;
using BizOneShot.Light.Models.WebModels;
using PagedList;
using PagedList.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace BizOneShot.Light.Dao.Repositories
{
    public interface IProcCompGetDeepenReportListNumsRepository : IRepository<ProcCompGetDeepenReportListNumsReturnModel>
    {
        IPagedList<ProcCompGetDeepenReportListNumsReturnModel> getDeepenReportListNums(object[] parameters, int page, int pageSize);
    }
    public class ProcCompGetDeepenReportListNumsRepository : RepositoryBase<ProcCompGetDeepenReportListNumsReturnModel>, IProcCompGetDeepenReportListNumsRepository
    {
        public ProcCompGetDeepenReportListNumsRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IPagedList<ProcCompGetDeepenReportListNumsReturnModel> getDeepenReportListNums(object[] parameters, int page, int pageSize)
        {
            return
                DbContext.Database.SqlQuery<ProcCompGetDeepenReportListNumsReturnModel>
                ("proc_compGetDeepenReportListNums @COMP_SN, @NUM_SN, @SUB_NUM_SN", parameters)
                .OrderByDescending(obj => obj.TOTAL_REPORT_SN)
                .ToPagedList(page, pageSize);
        }
    }
}
