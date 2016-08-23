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
    public interface IVcCompMappingRepository : IRepository<VcCompMapping>
    {
        Task<VcCompMapping> getCompMappingInfo(string CompSn);
        Task<VcCompMapping> getCompMappingInfos(string CompSn, string Basn);
        Task<VcCompMapping> InsertToAsync(VcCompMapping vcCompMapping);
        Task<VcCompMapping> getCompMappingByCpMpCode(string cpMpCode);

        // CompMappingInfo list
        Task<IList<VcCompMapping>> getCompMappingInfosList(int CompSn, int Basn);
        Task<IList<VcCompMapping>> getCompMappingForSn(int CompSn);

        // 보고서 작성자 가져오기
        Task<IList<VcCompMapping>> getReportWriterList(int compSn, int baSn, string numSn);

        // BA 보고서 작성자 가져오기
        Task<VcCompMapping> checkBaWriter(int compSn, int baSn, string numSn, string conCode);

        // Ba와 매핑된 기업 & 멘토 가져오기 
        Task<IList<VcCompMapping>> baGetCompMentorMappingList(int baSn);

        // BA 보고서 작성자 List 확인
        Task<IList<VcCompMapping>> checkBaWriterList(int compSn, int baSn, string numSn);


    }

    public class VcCompMappingRepository : RepositoryBase<VcCompMapping>, IVcCompMappingRepository
    {
        public VcCompMappingRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public async Task<VcCompMapping> getCompMappingInfo(string CompSn)
        {
            var convertCompSn = int.Parse(CompSn);
            return await DbContext.VcCompMappings.Where(bw => bw.CompSn == convertCompSn).SingleOrDefaultAsync();
        }

        public async Task<VcCompMapping> getCompMappingInfos(string CompSn, string BaSn)
        {
            var convertCompSn = int.Parse(CompSn);
            var convertBaSn = int.Parse(BaSn);

            return await DbContext.VcCompMappings
                .Where(bw => bw.CompSn == convertCompSn)
                .Where(bw => bw.BaSn == convertBaSn)
                .SingleOrDefaultAsync();
        }

        public async Task<VcCompMapping> InsertToAsync(VcCompMapping vcCompMapping)
        {
            return await Task.Run(() => DbContext.VcCompMappings.Add(vcCompMapping));
        }

        public async Task<VcCompMapping> getCompMappingByCpMpCode(string cpMpCode)
        {
            return await DbContext.VcCompMappings.Where(bw => bw.CpMpCode == cpMpCode).SingleOrDefaultAsync();
        }
        public async Task<IList<VcCompMapping>> getCompMappingInfosList(int CompSn, int Basn)
        {
            return await DbContext.VcCompMappings.Where(bw => bw.CompSn == CompSn).Where(bw => bw.BaSn ==Basn).ToListAsync();
        }

        public async Task<IList<VcCompMapping>> getReportWriterList(int compSn, int baSn, string numSn)
        {
            return await DbContext.VcCompMappings.Where(bw => bw.CompSn == compSn).Where(bw => bw.BaSn == baSn).Where(bw => bw.NumSn == numSn).ToListAsync();
        }

        public async Task<VcCompMapping> checkBaWriter(int compSn, int baSn, string numSn, string conCode)
        {
            return await DbContext.VcCompMappings
                .Where(bw => bw.CompSn == compSn)
                .Where(bw => bw.BaSn == baSn)
                .Where(bw => bw.NumSn == numSn)
                .Where(bw => bw.ConCode == conCode)
                .SingleOrDefaultAsync();
        }

        public async Task<IList<VcCompMapping>> getCompMappingForSn(int CompSn)
        {
            return await DbContext.VcCompMappings.Where(bw => bw.CompSn == CompSn).ToListAsync();
        }

        public async Task<IList<VcCompMapping>> baGetCompMentorMappingList(int baSn)
        {
            return await DbContext.VcCompMappings.Where(bw => bw.BaSn == baSn).ToListAsync();
        }

        public async Task<IList<VcCompMapping>> checkBaWriterList(int compSn, int baSn, string numSn)
        {
            return await DbContext.VcCompMappings
                .Where(bw => bw.CompSn == compSn)
                .Where(bw => bw.BaSn == baSn)
                .Where(bw => bw.NumSn == numSn)
                .ToListAsync();
        }



    }

}
