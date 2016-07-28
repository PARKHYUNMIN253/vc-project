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
    public interface IRptMasterRepository : IRepository<RptMaster>
    {
        RptMaster Insert(RptMaster rptMaster);
        Task<IList<RptMaster>> GetRptMastersAsync(Expression<Func<RptMaster, bool>> where);

        Task<IPagedList<RptMaster>> GetRptMasters(int page, int pageSize, string mentorID, int basicYear, int bizWorkSn,
            int compSn, string status);

        Task<IPagedList<RptMaster>> GetRptMastersForBizManager(int page, int pageSize, string executorId, int basicYear,
            int bizWorkSn, int compSn, string status);

        Task<IPagedList<RptMaster>> GetRptMastersForSysManager(int page, int pageSize, int bizWorkSn, int mngCompSn,
            string status);

        //Task<IPagedList<RptMaster>> GetRptMastersForExpert(int page, int pageSize, string expertId, int bizWorkSn,
        //    int mngCompSn, string status);

        Task<RptMaster> GetRptMasterAsync(Expression<Func<RptMaster, bool>> where);

        RptMaster GetRptMasterByQuestionSn(Expression<Func<RptMaster, bool>> where);

        // 05.22 phm 메소드 추가
        // area : company
        Task<RptMaster> getRptMasterByCompSnBasicYear(Expression<Func<RptMaster, bool>> where);
    }


    public class RptMasterRepository : RepositoryBase<RptMaster>, IRptMasterRepository
    {
        public RptMasterRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public RptMaster Insert(RptMaster rptMaster)
        {
            return DbContext.RptMasters.Add(rptMaster);
        }

        public async Task<IList<RptMaster>> GetRptMastersAsync(Expression<Func<RptMaster, bool>> where)
        {
            return await DbContext.RptMasters.Include("VcBizWork").Include("VcCompInfo").Where(where).ToListAsync();
        }

        public async Task<RptMaster> GetRptMasterAsync(Expression<Func<RptMaster, bool>> where)
        {
            return
                await
                    //DbContext.RptMasters.Include("VcBizWork").Include("VcCompInfo").Where(where).SingleOrDefaultAsync();
                    DbContext.RptMasters.Where(where).SingleOrDefaultAsync();
        }

        public async Task<IPagedList<RptMaster>> GetRptMasters(int page, int pageSize, string mentorID, int basicYear,
            int bizWorkSn, int compSn, string status)
        {
            //return await DbContext.RptMasters
            //    .Include(rm => rm.ScBizWork)
            //    .Include(rm => rm.ScCompInfo)
            //    .Where(rm => rm.MentorId == mentorID && rm.BasicYear == basicYear && rm.Status.Contains(status) && rm.Status != "D")
            //    .Where(rm => bizWorkSn == 0 ? rm.BizWorkSn > 0 : rm.BizWorkSn == bizWorkSn)
            //    .Where(rm => compSn == 0 ? rm.CompSn > 0 : rm.CompSn == compSn)
            //    .OrderByDescending(rm => rm.RegDt)
            //    .AsNoTracking().ToPagedListAsync(page, pageSize);

            var rptMasters = DbContext.RptMasters
                             .Where(rm => rm.MentorId == mentorID && rm.BasicYear == basicYear && rm.Status.Contains(status) && rm.Status != "D")
                             .Where(rm => bizWorkSn == 0 ? int.Parse(rm.NumSn) > 0 : int.Parse(rm.NumSn) == bizWorkSn)
                             .Where(rm => compSn == 0 ? rm.CompSn > 0 : rm.CompSn == compSn);

            // 조인 테스트
            //var joinList = from a in rptMasters
            //               join c in DbContext.VcCompMappings on new {  a.CompSn } equals new { c.CompSn }
            //               select a;


            //return await joinList.Include("VcBizWork").Include("VcCompInfo")
            //       .OrderByDescending(rm => rm.RegDt)
            //       .AsNoTracking().ToPagedListAsync(page, pageSize);

            return await DbContext.RptMasters.OrderByDescending(rm => rm.RegDt).ToPagedListAsync(page, pageSize);
        }

        public async Task<IPagedList<RptMaster>> GetRptMastersForBizManager(int page, int pageSize, string executorId,
            int basicYear, int bizWorkSn, int compSn, string status)
        {
            if (executorId == null)
            {
                return await DbContext.RptMasters
                    //.Include(rm => rm.VcBizWork)
                    .Include(rm => rm.VcCompInfo)
                    //.Where(rm => rm.ScBizWork.MngCompSn == compSn && rm.Status == status)
                    .Where(rm => bizWorkSn == 0 ? int.Parse(rm.NumSn) > 0 : int.Parse(rm.NumSn) == bizWorkSn)
                    .Where(rm => basicYear == 0 ? rm.BasicYear > 0 : rm.BasicYear == basicYear)
                    .OrderByDescending(rm => rm.RegDt)
                    .AsNoTracking().ToPagedListAsync(page, pageSize);
            }
            return await DbContext.RptMasters
                //.Include(rm => rm.VcBizWork)
                .Include(rm => rm.VcCompInfo)
                //.Where(
                //    rm =>
                //        rm.ScBizWork.MngCompSn == compSn && rm.ScBizWork.ExecutorId == executorId && rm.Status == status)
                .Where(rm => bizWorkSn == 0 ? int.Parse(rm.NumSn) > 0 : int.Parse(rm.NumSn) == bizWorkSn)
                .Where(rm => basicYear == 0 ? rm.BasicYear > 0 : rm.BasicYear == basicYear)
                .OrderByDescending(rm => rm.RegDt)
                .AsNoTracking().ToPagedListAsync(page, pageSize);
        }


        public async Task<IPagedList<RptMaster>> GetRptMastersForSysManager(int page, int pageSize, int bizWorkSn,
            int mngCompSn, string status)
        {
            return await DbContext.RptMasters
                //.Include(rm => rm.VcBizWork)
                .Include(rm => rm.VcCompInfo)
                .Where(rm => rm.Status == status)
                //.Where(rm => mngCompSn == 0 ? rm.VcBizWork.MngCompSn > 0 : rm.VcBizWork.MngCompSn == mngCompSn)
                .Where(rm => bizWorkSn == 0 ? int.Parse(rm.NumSn) > 0 : int.Parse(rm.NumSn) == bizWorkSn)
                .OrderByDescending(rm => rm.RegDt)
                .AsNoTracking().ToPagedListAsync(page, pageSize);
        }

        // 전문가 보고서
        //public async Task<IPagedList<RptMaster>> GetRptMastersForExpert(int page, int pageSize, string expertId,
        //    int bizWorkSn, int mngCompSn, string status)
        //{
        //    var listRptMatsers = await DbContext.ScExpertMappings
        //        .Where(em => em.ExpertId == expertId)
        //        .Select(sm => sm.ScBizWork)
        //        .Select(tt => tt.RptMasters).AsNoTracking().ToListAsync();

        //    IList<RptMaster> rptMaterView = new List<RptMaster>();
        //    foreach (var rptMasters in listRptMatsers)
        //    {
        //        foreach (var rptMaster in rptMasters)
        //        {
        //            rptMaterView.Add(rptMaster);
        //        }
        //    }

        //    var rptMaters = rptMaterView.Where(em => em.Status == status)
        //        .Where(rm => mngCompSn == 0 ? rm.ScBizWork.MngCompSn > 0 : rm.ScBizWork.MngCompSn == mngCompSn)
        //        .Where(rm => bizWorkSn == 0 ? rm.BizWorkSn > 0 : rm.BizWorkSn == bizWorkSn)
        //        .OrderByDescending(rm => rm.RegDt)
        //        .ToPagedList(page, pageSize);

        //    return rptMaters;
        //}

        public RptMaster GetRptMasterByQuestionSn(Expression<Func<RptMaster, bool>> where)
        {
            return DbContext.RptMasters.Where(where).SingleOrDefault();
        }

        // 05.22 phm 메소드 추가
        // area : company
        public async Task<RptMaster> getRptMasterByCompSnBasicYear(Expression<Func<RptMaster, bool>> where)
        {
            return await DbContext.RptMasters.Where(where).SingleOrDefaultAsync();
        }
    }
}