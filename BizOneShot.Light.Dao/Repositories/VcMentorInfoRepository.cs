using BizOneShot.Light.Dao.Infrastructure;
using BizOneShot.Light.Models.WebModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BizOneShot.Light.Dao.Repositories
{
    public interface IVcMentorInfoRepository : IRepository<VcMentorInfo>
    {
        Task<VcMentorInfo> getMentorInfoById(string mentorId);
        Task<VcMentorInfo> getMentorInfoBySn(string mentorSn);
        VcMentorInfo Insert(VcMentorInfo vcMentorInfo);

        // baSn으로 멘토 info 조회
        Task<IList<VcMentorInfo>> baGetMappingMentor(int baSn);
    }



    public class VcMentorInfoRepository : RepositoryBase<VcMentorInfo>, IVcMentorInfoRepository
    {
        public VcMentorInfoRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public async Task<VcMentorInfo> getMentorInfoById(string mentorId)
        {
            var id = int.Parse(mentorId);
            return await DbContext.VcMentorInfoes.Where(bw => bw.TcmsLoginKey == id).SingleOrDefaultAsync();

        }

        public async Task<VcMentorInfo> getMentorInfoBySn(string mentorSn)
        {
            var convertMentorSn = int.Parse(mentorSn);
            return await DbContext.VcMentorInfoes.Where(bw => bw.MentorSn == convertMentorSn).SingleOrDefaultAsync();

        }

        public VcMentorInfo Insert(VcMentorInfo vcMentorInfo)
        {
            return DbContext.VcMentorInfoes.Add(vcMentorInfo);
        }

        public async Task<IList<VcMentorInfo>> baGetMappingMentor(int baSn)
        {
            return await DbContext.VcMentorInfoes.Where(bw => bw.BaSn == baSn).ToListAsync();
        }
    }
}