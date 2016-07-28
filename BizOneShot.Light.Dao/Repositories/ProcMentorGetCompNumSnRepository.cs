using BizOneShot.Light.Dao.Infrastructure;
using BizOneShot.Light.Models.WebModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizOneShot.Light.Dao.Repositories
{
    public interface IProcMentorGetCompNumSnRepository : IRepository<ProcMentorGetCompNumSnReturnModel>
    {
        Task<IList<ProcMentorGetCompNumSnReturnModel>> getMentorCompNumSn(object[] parameters);
    }

    public class ProcMentorGetCompNumSnRepository : RepositoryBase<ProcMentorGetCompNumSnReturnModel>, IProcMentorGetCompNumSnRepository
    {
        public ProcMentorGetCompNumSnRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }


        public async Task<IList<ProcMentorGetCompNumSnReturnModel>> getMentorCompNumSn(object[] parameters)
        {
            return await DbContext.Database.SqlQuery<ProcMentorGetCompNumSnReturnModel>("proc_mentorGetCompNumSn @COMP_SN, @LOGIN_ID", parameters).ToListAsync();
        }

    }
}
