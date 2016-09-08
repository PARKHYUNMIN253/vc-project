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
    public interface IVcIfNumInfoRepository : IRepository<VcIfNumInfo>
    {
        void insert(VcIfNumInfo vcIfNumInfo);
        Task<VcIfNumInfo> inertToAsync(VcIfNumInfo vcIfNumInfo);
        //Task<IList<VcIfNumInfo>> getNumSn(string numSn);
        Task<VcIfNumInfo> getVcIfNumInfoByInfId(string infId);

        // getVcIfNumInfoByInfId
        VcIfNumInfo getTest(Expression<Func<VcIfNumInfo, bool>> where);
    }

    public class VcIfNumInfoRepository : RepositoryBase<VcIfNumInfo>, IVcIfNumInfoRepository
    {
        public VcIfNumInfoRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public void insert(VcIfNumInfo vcIfNumInfo)
        {
            DbContext.VcIfNumInfoes.Add(vcIfNumInfo);
        }

        public async Task<VcIfNumInfo> inertToAsync(VcIfNumInfo vcIfNumInfo)
        {
            return await Task.Run(() => DbContext.VcIfNumInfoes.Add(vcIfNumInfo));
        }
        //public async Task<IList<VcIfNumInfo>> getNumSn(string numSn)
        //{
        //    return await DbContext.VcIfNumInfoes.Where(bw => bw.NumSn == numSn).ToListAsync();
        //}

        // infid로 해당 테이블 객체 조회
        public async Task<VcIfNumInfo> getVcIfNumInfoByInfId(string infId)
        {
            return await DbContext.VcIfNumInfoes.Where(bw => bw.InfId == infId).SingleOrDefaultAsync();
        }

        public VcIfNumInfo getTest(Expression<Func<VcIfNumInfo, bool>> where)
        {

            return DbContext.VcIfNumInfoes.Where(where).SingleOrDefault();

        }
    }
}
