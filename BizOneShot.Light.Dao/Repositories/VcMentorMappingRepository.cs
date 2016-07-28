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
    public interface IVcMentorMappingRepository : IRepository<VcMentorMapping>
    {
        Task<IList<VcMentorMapping>> getVcMentorMappingByCompSn(Expression<Func<VcMentorMapping, bool>> where);
        Task<VcMentorMapping> getVcMentorMappingSingleByCompSn(Expression<Func<VcMentorMapping, bool>> where);
        Task<VcMentorMapping> GetCompSnForTcms(string compSn , string baSn, string numSn, string subSn, string code);
        Task<VcMentorMapping> Insert(VcMentorMapping vcMentor);
        Task<VcMentorMapping> getMentorMappingInfo(string mentorSn);
        Task<IList<VcMentorMapping>> getMentorMappingInfoList(string compSn, string mentorSn);

        // conCodeInfo getConcodeInfo
        Task<VcMentorMapping> getConcodeInfo(int compSn, int baSn, int mentorSn, string numSn, string subNumSn);
        Task<IList<VcMentorMapping>>getCheckMapping(int compSn, int baSn, string numSn);

        // 매핑된 기업 & 멘토있는지 확인
        Task<IList<VcMentorMapping>> checkCompMentorMapping(int compSn, int baSn, string numSn, string subNumSn, string conCode);

        // 기업 conCode 매핑 홧인
        Task<IList<VcMentorMapping>> checkCompConCodeMapping(int compSn, string conCode);
    }



    public class VcMentorMappingRepository : RepositoryBase<VcMentorMapping>, IVcMentorMappingRepository
    {
        public VcMentorMappingRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public async Task<IList<VcMentorMapping>> getVcMentorMappingByCompSn(Expression<Func<VcMentorMapping, bool>> where)
        {
            return await DbContext.VcMentorMappings.Where(where).ToListAsync();
        }

        public async Task<VcMentorMapping> Insert(VcMentorMapping vcMentor)
        {
            return await Task.Run(() => DbContext.VcMentorMappings.Add(vcMentor));
        }


        public async Task<VcMentorMapping> getMentorMappingInfo(string mentorSn)
        {
            var convertMentorSn = int.Parse(mentorSn);
            return await DbContext.VcMentorMappings.Where(bw => bw.MentorSn == convertMentorSn).SingleOrDefaultAsync();
        }

        public async Task<IList<VcMentorMapping>> getMentorMappingInfoList(string compSn, string mentorSn)
        {
            var convertCompSn = int.Parse(compSn);
            var convertMentorSn = int.Parse(mentorSn);

            return await DbContext.VcMentorMappings
                .Where(bw => bw.CompSn == convertCompSn)
                .Where(bw => bw.MentorSn == convertMentorSn)
                .ToListAsync();
        }

        public async Task<VcMentorMapping> GetCompSnForTcms(string compSn, string baSn, string numSn, string subSn, string code)
        {
            var convertCompSn = int.Parse(compSn);
            var convertBaSn = int.Parse(baSn);

            return await DbContext.VcMentorMappings
                .Where(mm => mm.CompSn == convertCompSn && mm.BaSn == convertBaSn && mm.NumSn == numSn && mm.SubNumSn == subSn && mm.ConCode == code).SingleOrDefaultAsync();
        }

        public async Task<VcMentorMapping> getConcodeInfo(int compSn, int baSn, int mentorSn, string numSn, string subNumSn)
        {
            return await DbContext.VcMentorMappings.Where(bw => bw.CompSn == compSn)
                .Where(bw => bw.BaSn == baSn)
                .Where(bw => bw.MentorSn == mentorSn)
                .Where(bw => bw.NumSn == numSn)
                .Where(bw => bw.SubNumSn == subNumSn)
                .SingleOrDefaultAsync();
        }

        public async Task<IList<VcMentorMapping>> getCheckMapping(int compSn, int baSn, string numSn)
        {
            return await DbContext.VcMentorMappings
                .Where(bw => bw.CompSn == compSn)
                .Where(bw => bw.BaSn == baSn)
                .Where(bw => bw.NumSn == numSn)
                .ToListAsync();
        }

        public async Task<VcMentorMapping> getVcMentorMappingSingleByCompSn(Expression<Func<VcMentorMapping, bool>> where)
        {
            return await DbContext.VcMentorMappings.Where(where).SingleOrDefaultAsync();
        }

        public async Task<IList<VcMentorMapping>> checkCompMentorMapping(int compSn, int baSn, string numSn, string subNumSn, string conCode)
        {
            return await DbContext.VcMentorMappings
                .Where(bw => bw.CompSn == compSn)
                .Where(bw => bw.BaSn == baSn)
                .Where(bw => bw.NumSn == numSn)
                .Where(bw => bw.SubNumSn == subNumSn)
                .Where(bw => bw.ConCode == conCode)
                .ToListAsync();
        }

        public async Task<IList<VcMentorMapping>> checkCompConCodeMapping(int compSn, string conCode)
        {
            return await DbContext.VcMentorMappings
                .Where(bw => bw.CompSn == compSn)
                .Where(bw => bw.ConCode == conCode)
                .ToListAsync();
        }
    }
}
