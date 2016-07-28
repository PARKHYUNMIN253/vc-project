using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BizOneShot.Light.Dao.Infrastructure;
using BizOneShot.Light.Dao.Repositories;
using BizOneShot.Light.Models.WebModels;
using PagedList;

namespace BizOneShot.Light.Services
{
    public interface IScBizWorkService : IBaseService
    {
        //IEnumerable<FaqViewModel> GetFaqs(string searchType = null, string keyword = null);
        //Task<IList<VcBizWork>> GetBizWorkList(int comSn, string excutorId = null);
        //Task<IList<VcBizWork>> GetBizWorkList(int mngComSn, string excutorId = null, int bizWorkYear = 0);

        //Task<IPagedList<VcBizWork>> GetPagedListBizWorkList(int page, int pageSize, int mngComSn,
        //    string excutorId = null, int bizWorkYear = 0);

        //Task<IList<VcBizWork>> GetEndBizWorkList(DateTime endDateTiem);
        //Task<IList<VcCompInfo>> GetBizWorkComList(int bizWorkSn);
        //Task<IPagedList<VcCompInfo>> GetPagedListBizWorkComList(int bizWorkSn, int page, int pageSize);
        //Task<IList<VcBizWork>> GetBizWorkListByBizWorkNm(int comSn, string query);
        //Task<VcBizWork> GetBizWorkByloginId(string loginId);
        Task<IList<VcBizWork>> GetBizWorkByBizWorkSn(int bizWorkSn);
        VcBizWork Insert(VcBizWork scBizWork);
        
        
        Task<int> AddBizWorkAsync(VcBizWork scBizWork);
        
    }

    public class VcBizWorkService : IScBizWorkService
    {
        private readonly IScBizWorkRepository scBizWorkRespository;
        private readonly IScCompMappingRepository scCompMappingRepository;
        private readonly IUnitOfWork unitOfWork;

        public VcBizWorkService(IScBizWorkRepository scBizWorkRespository,
            IScCompMappingRepository scCompMappingRepository, IUnitOfWork unitOfWork)
        {
            this.scBizWorkRespository = scBizWorkRespository;
            this.scCompMappingRepository = scCompMappingRepository;
            this.unitOfWork = unitOfWork;
        }


        //public async Task<IList<VcBizWork>> GetBizWorkList(int mngComSn, string excutorId = null, int bizWorkYear = 0)
        //{
        //    if (string.IsNullOrEmpty(excutorId))
        //    {
        //        var scBizWorks =
        //            await scBizWorkRespository.GetBizWorksAsync(bw => bw.MngCompSn == mngComSn && bw.Status == "N");
        //        scBizWorks.Where(
        //            bw =>
        //                bizWorkYear == 0
        //                    ? bw.BizWorkStDt.Value.Year > 0
        //                    : bw.BizWorkStDt.Value.Year <= bizWorkYear && bw.BizWorkEdDt.Value.Year >= bizWorkYear);

        //        //return scBizWorks.OrderByDescending(bw => bw.BizWorkStDt).OrderBy(bw => bw.BizWorkNm).ToList();
        //        return scBizWorks.OrderBy(bw => bw.BizWorkNm).OrderByDescending(bw => bw.BizWorkStDt).ToList();
        //    }
        //    else
        //    {
        //        var scBizWorks =
        //            await
        //                scBizWorkRespository.GetBizWorksAsync(
        //                    bw => bw.MngCompSn == mngComSn && bw.Status == "N" && bw.ExecutorId == excutorId);
        //        scBizWorks.Where(
        //            bw =>
        //                bizWorkYear == 0
        //                    ? bw.BizWorkStDt.Value.Year > 0
        //                    : bw.BizWorkStDt.Value.Year <= bizWorkYear && bw.BizWorkEdDt.Value.Year >= bizWorkYear);

        //        //return scBizWorks.OrderByDescending(bw => bw.BizWorkStDt).OrderBy(bw => bw.BizWorkNm).ToList();
        //        return scBizWorks.OrderBy(bw => bw.BizWorkNm).OrderByDescending(bw => bw.BizWorkStDt).ToList();
        //    }
        //}

        //public async Task<IPagedList<VcBizWork>> GetPagedListBizWorkList(int page, int pageSize, int mngComSn,
        //    string excutorId = null, int bizWorkYear = 0)
        //{
        //    return
        //        await scBizWorkRespository.GetPagedListBizWorksAsync(page, pageSize, mngComSn, excutorId, bizWorkYear);
        //}

        //public async Task<IList<VcBizWork>> GetEndBizWorkList(DateTime endDateTiem)
        //{
        //    var scBizWorks =
        //        await scBizWorkRespository.GetBizWorksAsync(bw => bw.BizWorkEdDt < endDateTiem && bw.Status == "N");
        //    return scBizWorks.OrderByDescending(bw => bw.BizWorkSn).ToList();
        //}

        //public async Task<IList<VcCompInfo>> GetBizWorkComList(int bizWorkSn)
        //{
        //    var scBizWorks = await scCompMappingRepository.GetCompanysAsync(bw => bw.BizWorkSn == bizWorkSn);
        //    return scBizWorks.OrderByDescending(sc => sc.CompNm).ToList();
        //}

        //// 해당 사업에 매핑되어있는 전체 기업
        //public async Task<IPagedList<VcCompInfo>> GetPagedListBizWorkComList(int bizWorkSn, int page, int pageSize)
        //{
        //    return
        //        await scCompMappingRepository.GetPagedListCompanysAsync(bw => bw.BizWorkSn == bizWorkSn, page, pageSize);
        //    //return scBizWorks.OrderByDescending(sc => sc.CompNm).ToList();
        //}

        //public async Task<IList<VcBizWork>> GetBizWorkListByBizWorkNm(int mngComSn, string query)
        //{
        //    var scBizWorks =
        //        await
        //            scBizWorkRespository.GetBizWorksAsync(bw => bw.MngCompSn == mngComSn && bw.BizWorkNm.Contains(query));
        //    //return scBizWorks.OrderByDescending(bw => bw.BizWorkSn).ToList();
        //    return scBizWorks.OrderBy(bw => bw.BizWorkNm).OrderByDescending(bw => bw.BizWorkStDt).ToList();
        //}

        public async Task<IList<VcBizWork>> GetBizWorkByBizWorkSn(int bizWorkSn)
        {
            var scBizWork = await scBizWorkRespository.GetBizWorkAsync(bw => bw.BizWorkSn == bizWorkSn);
            return scBizWork;
        }

        //public async Task<VcBizWork> GetBizWorkByloginId(string loginId)
        //{
        //    var scBizWork = await scBizWorkRespository.GetBizWorkByLoginIdAsync(bw => bw.ExecutorId == loginId);
        //    return scBizWork;
        //}

        public VcBizWork Insert(VcBizWork scBizWork)
        {
            return scBizWorkRespository.Insert(scBizWork);
        }

        public async Task<int> AddBizWorkAsync(VcBizWork scBizWork)
        {
            var rstScUsr = scBizWorkRespository.Insert(scBizWork);

            if (rstScUsr == null)
            {
                return -1;
            }
            return await SaveDbContextAsync();
        }

        public void SaveDbContext()
        {
            unitOfWork.Commit();
        }

        public async Task<int> SaveDbContextAsync()
        {
            return await unitOfWork.CommitAsync();
        }
    }
}