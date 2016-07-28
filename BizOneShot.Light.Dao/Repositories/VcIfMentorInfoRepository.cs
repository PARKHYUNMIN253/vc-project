using BizOneShot.Light.Dao.Infrastructure;
using BizOneShot.Light.Models.WebModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizOneShot.Light.Dao.Repositories
{
    public interface IVcIfMentorInfoRepository : IRepository<VcIfMentorInfo>
    {
        void insert(VcIfMentorInfo vcIfMentorInfo);
        Task<VcIfMentorInfo> InsertMentor(VcIfMentorInfo vcIfMentorInfo);
        Task<VcIfMentorInfo> getVcIfMentorInfId(string InfId);
    }

    public class VcIfMentorInfoRepository : RepositoryBase<VcIfMentorInfo>, IVcIfMentorInfoRepository
    {
        public VcIfMentorInfoRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public async Task<VcIfMentorInfo> getVcIfMentorInfId(string InfId)
        {
            return await DbContext.VcIfMentorInfoes.Where(mi => mi.InfId == InfId).SingleOrDefaultAsync();
        }

        public void insert(VcIfMentorInfo vcIfMentorInfo)
        {
            DbContext.VcIfMentorInfoes.Add(vcIfMentorInfo);
        }

        public async Task<VcIfMentorInfo> InsertMentor(VcIfMentorInfo vcIfMentorInfo)
        {
            return await Task.Run(() => DbContext.VcIfMentorInfoes.Add(vcIfMentorInfo));
        }
    }
}
