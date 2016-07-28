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
    public interface IScBizWorkRepository : IRepository<VcBizWork>
    {
        VcBizWork Insert(VcBizWork scBizWork);
        Task<IList<VcBizWork>> GetBizWorksAsync(Expression<Func<VcBizWork, bool>> where);

        //Task<IPagedList<VcBizWork>> GetPagedListBizWorksAsync(int page, int pageSize, int mngComSn,
        //    string excutorId = null, int bizWorkYear = 0);

        Task<IList<VcBizWork>> GetBizWorkAsync(Expression<Func<VcBizWork, bool>> where);
        Task<VcBizWork> GetBizWorkByLoginIdAsync(Expression<Func<VcBizWork, bool>> where);
    }


    public class ScBizWorkRepository : RepositoryBase<VcBizWork>, IScBizWorkRepository
    {
        public ScBizWorkRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public VcBizWork Insert(VcBizWork scBizWork)
        {
            return DbContext.VcBizWorks.Add(scBizWork);
        }

        public async Task<IList<VcBizWork>> GetBizWorksAsync(Expression<Func<VcBizWork, bool>> where)
        {
            return await DbContext.VcBizWorks.Include("VcCompMappings").Include("VcUsrInfo").Where(where).ToListAsync();
        }

        // 기간에 대한 테이블은 VC_NUM_MNG
        //public async Task<IPagedList<VcBizWork>> GetPagedListBizWorksAsync(int page, int pageSize, int mngComSn,
        //    string excutorId = null, int bizWorkYear = 0)
        //{
        //    if (string.IsNullOrEmpty(excutorId))
        //    {
        //        return
        //            await
        //                DbContext.VcBizWorks.Include("VcCompMappings")
        //                    .Include("VcUsrInfo")
        //                    //.Where(bw => bw.MngCompSn == mngComSn && bw.Status == "N")
        //                    .Where(
        //                        bw =>
        //                            bizWorkYear == 0
        //                                ? bw.BizWorkStDt.Value.Year > 0
        //                                : bw.BizWorkStDt.Value.Year <= bizWorkYear &&
        //                                  bw.BizWorkEdDt.Value.Year >= bizWorkYear)
        //                    .OrderByDescending(bw => bw.BizWorkSn).ToPagedListAsync(page, pageSize);
        //    }
        //    return
        //        await
        //            DbContext.VcBizWorks.Include("VcCompMappings")
        //                .Include("VcUsrInfo")
        //                //.Where(bw => bw.MngCompSn == mngComSn && bw.Status == "N" && bw.ExecutorId == excutorId)
        //                .Where(
        //                    bw =>
        //                        bizWorkYear == 0
        //                            ? bw.BizWorkStDt.Value.Year > 0
        //                            : bw.BizWorkStDt.Value.Year <= bizWorkYear &&
        //                              bw.BizWorkEdDt.Value.Year >= bizWorkYear)
        //                .OrderByDescending(bw => bw.BizWorkSn).ToPagedListAsync(page, pageSize);
        //}

        public async Task<IList<VcBizWork>> GetBizWorkAsync(Expression<Func<VcBizWork, bool>> where)
        {
            return
                await
                    //DbContext.VcBizWorks.Include("VcCompMappings").Include("VcUsrInfo").Where(where).SingleOrDefaultAsync();
                    DbContext.VcBizWorks.Where(where).ToListAsync();
        }

        public async Task<VcBizWork> GetBizWorkByLoginIdAsync(Expression<Func<VcBizWork, bool>> where)
        {
            return await DbContext.VcBizWorks.Include("VcUsrInfo").Where(where).SingleOrDefaultAsync();
        }
    }
}