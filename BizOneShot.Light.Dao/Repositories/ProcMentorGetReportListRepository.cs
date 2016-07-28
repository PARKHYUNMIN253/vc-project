using BizOneShot.Light.Dao.Infrastructure;
using BizOneShot.Light.Models.WebModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizOneShot.Light.Dao.Repositories
{
    public interface IProcMentorGetReportListRepository : IRepository<ProcMentorGetReportListReturnModel>
    {
        Task<IList<ProcMentorGetReportListReturnModel>> getMentorReportList(object[] parameters);
    }

    public class ProcMentorGetReportListRepository : RepositoryBase<ProcMentorGetReportListReturnModel>, IProcMentorGetReportListRepository
    {
        public ProcMentorGetReportListRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }


        public async Task<IList<ProcMentorGetReportListReturnModel>> getMentorReportList(object[] parameters)
        {
            return await DbContext.Database.SqlQuery<ProcMentorGetReportListReturnModel>("proc_mentorGetReportList @LOGIN_ID", parameters).ToListAsync();
        }

    }
}
