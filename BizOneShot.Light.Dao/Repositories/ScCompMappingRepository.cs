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
    public interface IScCompMappingRepository : IRepository<VcCompMapping>
    {
        //Task<IPagedList<VcCompMapping>> GetPagedListCompMappingsAsync(int page, int pageSize, int compSn,
        //    int bizWorkSn = 0, string status = null, string compNm = null);
        //Task<IPagedList<VcCompMapping>> GetPagedListCompMappingsAsync(int page, int pageSize, int compSn, string excutorId = null, int bizWorkSn = 0);
        //Task<IPagedList<VcCompMapping>> GetPagedListCompMappingsForBasicReportAsync(int page, int pageSize, int compSn, string excutorId = null, int bizWorkSn = 0);

        Task<IList<VcCompMapping>> GetCompMappingsAsync(Expression<Func<VcCompMapping, bool>> where);
        Task<VcCompMapping> GetCompMappingAsync(Expression<Func<VcCompMapping, bool>> where);

        
        //Task<IList<VcCompInfo>> GetCompanysAsync(Expression<Func<VcCompMapping, bool>> where);
        //Task<IPagedList<VcCompInfo>> GetPagedListCompanysAsync(Expression<Func<VcCompMapping, bool>> where, int page, int pageSize);
        //Task<IList<VcCompMapping>> GetExpertCompanysAsync(string loginId, string comName = null);
        //Task<IList<VcCompMapping>> GetExpertCompanysAsync(Expression<Func<VcCompMapping, bool>> where);
        //Task<IList<VcCompMapping>> GetExpertCompanysForPopupAsync(string expertId, string query);

    }


    public class ScCompMappingRepository : RepositoryBase<VcCompMapping>, IScCompMappingRepository
    {
        public ScCompMappingRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
           
        // VC_COMP_MAPPING에 대해서 FOREIGN KEY 추가
        //public async Task<IPagedList<VcCompMapping>> GetPagedListCompMappingsAsync(int page, int pageSize, int compSn,
        //    int bizWorkSn = 0, string status = null, string compNm = null)
        //{
        //    return await DbContext.VcCompMappings
        //        .Include("VcCompInfo")
        //        .Include("VcBizWork")
        //        .Include("VcUsrInfo")
        //        .Include("VcBizWork.VcCompInfo")
        //        .Include("VcBizWork.VcUsrInfo")
        //        //.Where(scm => scm.ScBizWork.ScCompInfo.CompSn == compSn && scm.Status != "D")
        //        .Where(scm => bizWorkSn == 0 ? int.Parse(scm.NumSn) > bizWorkSn : scm.NumSn = Convert.ToString(bizWorkSn))
        //        .Where(scm => string.IsNullOrEmpty(status) ? scm.Status != "D" : scm.Status == status)
        //        .Where(
        //            scm =>
        //                string.IsNullOrEmpty(compNm)
        //                    ? scm.VcCompInfo.CompNm != null
        //                    : scm.vcCompInfo.CompNm.Contains(compNm))
        //        .OrderByDescending(scm => scm.RegDt).ToPagedListAsync(page, pageSize);
        //}

        //public async Task<IPagedList<VcCompMapping>> GetPagedListCompMappingsAsync(int page, int pageSize, int compSn, string excutorId = null, int bizWorkSn = 0)
        //{

        //    return await DbContext.VcCompMappings
        //        .Include("VcCompInfo")
        //        .Include("VcBizWork")
        //        .Where(scm => scm.Status != "D")
        //        .Where(scm => string.IsNullOrEmpty(excutorId) ? scm.ScBizWork.ExecutorId != null : scm.ScBizWork.ExecutorId == excutorId)
        //        .Where(scm => compSn == 0 ? scm.ScBizWork.MngCompSn > compSn : scm.ScBizWork.MngCompSn == compSn)
        //        .Where(scm => bizWorkSn == 0 ? scm.BizWorkSn > bizWorkSn : scm.BizWorkSn == bizWorkSn)
        //        .OrderByDescending(scm => scm.RegDt).ToPagedListAsync(page, pageSize);

        //}

        //public async Task<IPagedList<VcCompMapping>> GetPagedListCompMappingsForBasicReportAsync(int page, int pageSize, int compSn, string excutorId = null, int bizWorkSn = 0)
        //{

        //    return await DbContext.VcCompMappings
        //        .Include("VcCompInfo")
        //        .Include("VcBizWork")
        //        .Where(scm => scm.Status != "D")
        //        .Where(scm => compSn == 0 ? scm.ScBizWork.MngCompSn > compSn : scm.ScBizWork.MngCompSn == compSn)
        //        .Where(scm => string.IsNullOrEmpty(excutorId) ? scm.ScBizWork.ExecutorId != null : scm.ScBizWork.ExecutorId == excutorId)
        //        .Where(scm => bizWorkSn == 0 ? scm.BizWorkSn > bizWorkSn : scm.BizWorkSn == bizWorkSn)
        //        .Where(scm => scm.ScCompInfo.RptMasters.Where(rm => rm.Status == "C" && scm.BizWorkSn == rm.BizWorkSn).Count() > 0)
        //        .OrderByDescending(scm => scm.RegDt).ToPagedListAsync(page, pageSize);

        //}

        // 멘토가 담당하는 기업 list
        public async Task<IList<VcCompMapping>> GetCompMappingsAsync(Expression<Func<VcCompMapping, bool>> where)
        {
            return
                await
                    DbContext.VcCompMappings.Include("VcCompInfo")
                        .Include("VcBizWork")
                        .Include("VcUsrInfo")
                        .Include("VcBizWork.VcCompInfo")
                        .Include("VcBizWork.VcUsrInfo")
                        .Where(where)
                        .ToListAsync();
        }



        public async Task<VcCompMapping> GetCompMappingAsync(Expression<Func<VcCompMapping, bool>> where)
        {
            return
                await
                    DbContext.VcCompMappings.Include("VcCompInfo")
                        .Include("VcBizWork")
                        .Include("VcUsrInfo")
                        .Include("VcBizWork.VcCompInfo")
                        .Include("VcBizWork.VcUsrInfo")
                        .Where(where)
                        .SingleOrDefaultAsync();
        }

        //public async Task<IList<VcCompInfo>> GetCompanysAsync(Expression<Func<VcCompMapping, bool>> where)
        //{
        //    return
        //        await
        //            DbContext.VcCompMappings.Include("VcCompMappings")
        //                .Include("VcUsr")
        //                .Where(where)
        //                .Select(bw => bw.)
        //                .Include("VcUsrsInfo")
        //                .ToListAsync();
        //}

        //// 해당 사업에 매핑되어있는 전체 기업
        //public async Task<IPagedList<VcCompInfo>> GetPagedListCompanysAsync(Expression<Func<VcCompMapping, bool>> where,
        //    int page, int pageSize)
        //{
        //    return await DbContext.VcCompMappings
        //        .Include("ScCompMappings")
        //        .Include("ScUsr")
        //        .Where(where)
        //        //.Where(bw => bw.Status == "A")
        //        .Select(bw => bw.VcCompInfo)
        //        .Include("ScUsrs")
        //        .OrderByDescending(sc => sc.CompNm)
        //        .ToPagedListAsync(page, pageSize);
        //}


        //public async Task<IList<VcCompMapping>> GetExpertCompanysAsync(Expression<Func<VcCompMapping, bool>> where)
        //{
        //    return
        //        await
        //            DbContext.VcCompMappings.Include("ScCompInfo")
        //                .Include("ScCompInfo.ScUsrs")
        //                .Where(where)
        //                .ToListAsync();
        //}

        //public async Task<IList<VcCompMapping>> GetExpertCompanysAsync(string expertId, string comName = null)
        //{
        //    if (string.IsNullOrEmpty(comName))
        //    { 
        //        var joinList = from a in DbContext.VcCompMappings
        //                       join c in DbContext.ScExpertMappings on a.BizWorkSn equals c.BizWorkSn
        //            where c.ExpertId == expertId && a.Status == "A"
        //                       select a;

        //        return await joinList.Include("ScCompInfo").Include("ScCompInfo.ScUsrs").ToListAsync();
        //    }
        //    else
        //    {
        //        var joinList = from a in DbContext.VcCompMappings
        //                       join c in DbContext.ScExpertMappings on a.BizWorkSn equals c.BizWorkSn
        //            where c.ExpertId == expertId && a.Status == "A" && a.ScCompInfo.CompNm.Contains(comName)
        //                       select a;

        //        return await joinList.Include("ScCompInfo").Include("ScCompInfo.ScUsrs").ToListAsync();
        //    }
        //}

        //public async Task<IList<VcCompMapping>> GetExpertCompanysForPopupAsync(string expertId, string query)
        //{
        //    var joinList = from a in DbContext.VcCompMappings
        //                   join c in DbContext.ScExpertMappings on a.BizWorkSn equals c.BizWorkSn
        //        where
        //            c.ExpertId == expertId && a.Status == "A" &&
        //            (a.ScCompInfo.CompNm.Contains(query) || a.ScCompInfo.RegistrationNo.Contains(query))
        //                   select a;

        //    return await joinList.Include("ScCompInfo").Include("ScCompInfo.ScUsrs").ToListAsync();
        //}

    }
}
