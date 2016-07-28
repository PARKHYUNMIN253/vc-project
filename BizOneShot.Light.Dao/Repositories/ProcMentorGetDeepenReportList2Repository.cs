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
    public interface IProcMentorGetDeepenReportList2Repository : IRepository<ProcMentorGetDeepenReportList2ReturnModel>
    {
        Task<IList<ProcMentorGetDeepenReportList2ReturnModel>> MentorGetDeepenReportList(object[] parameters);
    }

    public class ProcMentorGetDeepenReportList2Repository : RepositoryBase<ProcMentorGetDeepenReportList2ReturnModel>, IProcMentorGetDeepenReportList2Repository
    {
        public ProcMentorGetDeepenReportList2Repository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public async Task<IList<ProcMentorGetDeepenReportList2ReturnModel>> MentorGetDeepenReportList(object[] parameters)
        {
            return await DbContext.Database.SqlQuery<ProcMentorGetDeepenReportList2ReturnModel>("proc_mentorGetDeepenReportList2 @LOGIN_ID", parameters).ToListAsync();
        }
    }
}
