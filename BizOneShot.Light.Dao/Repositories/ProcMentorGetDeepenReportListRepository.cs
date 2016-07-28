using BizOneShot.Light.Dao.Infrastructure;
using BizOneShot.Light.Models.WebModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizOneShot.Light.Dao.Repositories
{
    public interface IProcMentorGetDeepenReportListRepository : IRepository<ProcMentorGetDeepenReportListReturnModel>
    {
        Task<IList<ProcMentorGetDeepenReportListReturnModel>> getDeepenReportList(object[] parameters);
    }

    public class ProcMentorGetDeepenReportListRepository : RepositoryBase<ProcMentorGetDeepenReportListReturnModel>, IProcMentorGetDeepenReportListRepository
    {
        public ProcMentorGetDeepenReportListRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }


        public async Task<IList<ProcMentorGetDeepenReportListReturnModel>> getDeepenReportList(object[] parameters)
        {
            return await DbContext.Database.SqlQuery<ProcMentorGetDeepenReportListReturnModel>("proc_mentorGetDeepenReportList @LOGIN_ID", parameters).ToListAsync();
        }

    }
}
