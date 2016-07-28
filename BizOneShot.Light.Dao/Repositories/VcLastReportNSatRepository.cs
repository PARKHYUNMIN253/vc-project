using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BizOneShot.Light.Dao.Infrastructure;
using BizOneShot.Light.Models.WebModels;
using PagedList;
using PagedList.EntityFramework;

namespace BizOneShot.Light.Dao.Repositories
{
    public interface IVcLastReportNSatRepository : IRepository<VcLastReportNSat>
    {
        VcLastReportNSat insert(VcLastReportNSat RnS);
        Task<IList<VcLastReportNSat>> getVcCompInfoById(Expression<Func<VcLastReportNSat, bool>> where);
        Task<VcLastReportNSat> getSatSn(Expression<Func<VcLastReportNSat, bool>> where);
        Task<VcLastReportNSat> checkDeepenSubmit(int compSn, int baSn, int mentorSn);
        Task<IList<VcLastReportNSat>> checkDeepenSubmitByMentor(int compSn, int baSn, string numSn, string subNumSn, string conCode);
        Task<VcLastReportNSat> checkDeepenSubmitByBa(int compSn, int baSn, int mentorSn, string conCode);
    }
    public class VcLastReportNSatRepository : RepositoryBase<VcLastReportNSat>, IVcLastReportNSatRepository
    {
        public VcLastReportNSatRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public async Task<VcLastReportNSat> getSatSn(Expression<Func<VcLastReportNSat, bool>> where)
        {
            return await DbContext.VcLastReportNSats.Where(where).SingleOrDefaultAsync();
        }

        public async Task<IList<VcLastReportNSat>> getVcCompInfoById(Expression<Func<VcLastReportNSat, bool>> where)
        {
            return await DbContext.VcLastReportNSats.Where(where).ToListAsync();
        }

        public VcLastReportNSat insert(VcLastReportNSat RnS)
        {
            return DbContext.VcLastReportNSats.Add(RnS);
        }

        public async Task<VcLastReportNSat> checkDeepenSubmit(int compSn, int baSn, int mentorSn)
        {
            return await DbContext.VcLastReportNSats.Where(bw => bw.CompSn == compSn)
                .Where(bw => bw.BaSn == baSn)
                .Where(bw => bw.MentorSn == mentorSn)
                .SingleOrDefaultAsync();
        }

        public async Task<IList<VcLastReportNSat>> checkDeepenSubmitByMentor(int compSn, int baSn, string numSn, string subNumSn, string conCode)
        {

            return await DbContext.VcLastReportNSats
                .Where(bw => bw.CompSn == compSn)
                .Where(bw => bw.BaSn == baSn)
                .Where(bw => bw.NumSn == numSn)
                .Where(bw => bw.SubNumSn == subNumSn)
                .Where(bw => bw.ConCode == conCode)
                .ToListAsync();
        }

        public async Task<VcLastReportNSat> checkDeepenSubmitByBa(int compSn, int baSn, int mentorSn, string conCode)
        {
            return await DbContext.VcLastReportNSats.Where(bw => bw.CompSn == compSn).Where(bw => bw.BaSn == baSn).Where(bw => bw.MentorSn == mentorSn).Where(bw => bw.ConCode == conCode).SingleOrDefaultAsync();
        }
    }
}
