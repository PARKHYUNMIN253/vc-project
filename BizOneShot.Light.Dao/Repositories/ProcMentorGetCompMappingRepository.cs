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
    public interface IProcMentorGetCompMappingRepository : IRepository<ProcMentorGetCompMappingReturnModel>
    {
        Task<IList<ProcMentorGetCompMappingReturnModel>> getCompMapping(object[] parameters);
    }

    public class ProcMentorGetCompMappingRepository : RepositoryBase<ProcMentorGetCompMappingReturnModel>, IProcMentorGetCompMappingRepository
    {
        public ProcMentorGetCompMappingRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public async Task<IList<ProcMentorGetCompMappingReturnModel>> getCompMapping(object[] parameters)
        {
            return await DbContext.Database.SqlQuery<ProcMentorGetCompMappingReturnModel>("proc_mentorGetCompMapping @LOGIN_ID", parameters).ToListAsync();
        }
    }
}