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
    public interface IScCompMappingService : IBaseService
    {
        //Task<IList<VcCompMapping>> GetCompMappingListByMentorId(string mentorId = null, string status = null);
        //Task<IList<VcCompMapping>> GetCompMappingListByMentorId(string mentorId, string status = "A", int bizWorkSn = 0, int bizWorkYear = 0);
        //Task<IList<VcCompMapping>> GetCompMappingListByCompSn(int compSn, string status = "A", int bizWorkSn = 0, int bizWorkYear = 0);
        //Task<IList<VcCompInfo>> GetBizWorkComList(int mngComSn, string excutorId = null, int bizWorkSn = 0, int bizWorkYear = 0);
        //Task<IPagedList<VcCompMapping>> GetPagedListCompMappingsAsync(int page, int pageSize, int compSn, int bizWorkSn = 0, string status = null, string compNm = null);
        //Task<IPagedList<VcCompMapping>> GetPagedListCompMappingsAsync(int page, int pageSize, int compSn, string excutorId = null, int bizWorkSn = 0);
        //Task<IPagedList<VcCompMapping>> GetPagedListCompMappingsForBasicReportAsync(int page, int pageSize, int compSn = 0, string excutorId = null, int bizWorkSn = 0);
        //Task<IList<VcCompMapping>> GetCompMappingsAsync(int compSn, int bizWorkSn = 0, string status = null, string compNm = null);
        //Task<VcCompMapping> GetCompMappingAsync(int bizWorkSn, int compSn);
        //Task<VcCompMapping> GetCompMappingAsync(int compSn, string status = null);
        //Task<IList<VcCompMapping>> GetExpertCompMappingsAsync(string expertId, int bizWorkSn = 0, string comName = null);
        //Task<IList<VcCompMapping>> GetExpertCompMappingsAsync(string expertId, int bizWorkSn = 0, int compSn = 0);
        //Task<IList<VcCompMapping>> GetExpertCompMappingsForPopupAsync(string expertId, string query);
        //Task<IList<VcCompMapping>> GetCompMappingAsync(int bizWorkSn);
        //Task<IList<VcCompMapping>> GetCompMappingsForCompanyAsync(int compSn);
        //Task CancelApproveCompMapping(IList<string> listCompSn);
        //Task CompMappingCancelApprove(int bizWorkSn, int compSn);



        //Task<IList<VcCompMapping>> GetCompMappingListByMentorId(string mentorId, string status = "A", int bizWorkSn = 0, int bizWorkYear = 0);
    }

    public class ScCompMappingService : IScCompMappingService
    {
        private readonly IScCompMappingRepository scCompMappingRepository;
        private readonly IUnitOfWork unitOfWork;

        public ScCompMappingService(IScCompMappingRepository scCompMappingRepository, IUnitOfWork unitOfWork)
        {
            this.scCompMappingRepository = scCompMappingRepository;
            this.unitOfWork = unitOfWork;
        }


        //public async Task<IList<VcCompMapping>> GetExpertCompMappingsForPopupAsync(string expertId, string query)
        //{
        //    IEnumerable<VcCompMapping> listScCompMappingTask = null;


        //    listScCompMappingTask = await scCompMappingRepository.GetExpertCompanysForPopupAsync(expertId, query);
        //    return listScCompMappingTask.OrderByDescending(scm => scm.RegDt).ToList();
        //}

        //public async Task<VcCompMapping> GetCompMappingAsync(int bizWorkSn, int compSn)
        //{
        //    var scCompMapping =
        //        await
        //            scCompMappingRepository.GetCompMappingAsync(
        //                scm => scm.NumSn = bizWorkSn && scm.CompSn = compSn);

        //    return scCompMapping;
        //}


        //public async Task CompMappingCancelApprove(int bizWorkSn, int compSn)
        //{
        //    var scCompMapping =
        //        await
        //            scCompMappingRepository.GetCompMappingAsync(
        //                scm => scm.NumSn = bizWorkSn && scm.CompSn = compSn);

        //    scCompMapping.Status = "R";
        //    scCompMappingRepository.Update(scCompMapping);
        //}

        //public async Task CancelApproveCompMapping(IList<string> listCompSn)
        //{
        //    foreach (var compSn in listCompSn)
        //    {
        //        var array = compSn.Split('#');
        //        await Task.Run(() => CompMappingCancelApprove(int.Parse(array[1]), int.Parse(array[0])));
        //    }

        //    await SaveDbContextAsync();
        //}


        //public async Task<IList<VcCompMapping>> GetCompMappingAsync(int bizWorkSn)
        //{
        //    var scCompMapping = await scCompMappingRepository.GetCompMappingsAsync(scm => int.Parse(scm.NumSn) = bizWorkSn);

        //    return scCompMapping;
        //}


        ////사업참여기업의 매핑정보를 가져옴
        //public async Task<VcCompMapping> GetCompMappingAsync(int compSn, string status = null)
        //{
        //    if (string.IsNullOrEmpty(status))
        //    {
        //        var scCompMapping = await scCompMappingRepository.GetCompMappingAsync(scm => scm.NumSn = Convert.ToString(compSn));

        //        return scCompMapping;
        //    }
        //    else
        //    {
        //        var scCompMapping =
        //            await
        //                scCompMappingRepository.GetCompMappingAsync(scm => scm.CompSn = Convert.ToString(compSn);

        //        return scCompMapping;
        //    }
        //}

        //public async Task<IPagedList<VcCompMapping>> GetPagedListCompMappingsAsync(int page, int pageSize, int compSn,
        //    int bizWorkSn = 0, string status = null, string compNm = null)
        //{ 
        //    return
        //        await
        //            scCompMappingRepository.GetPagedListCompMappingsAsync(page, pageSize, compSn, bizWorkSn, status,
        //                compNm);
        //}

        //public async Task<IPagedList<VcCompMapping>> GetPagedListCompMappingsAsync(int page, int pageSize, int compSn, string excutorId = null, int bizWorkSn = 0)
        //{
        //    return await scCompMappingRepository.GetPagedListCompMappingsAsync(page, pageSize, compSn, excutorId, bizWorkSn);
        //}

        //public async Task<IPagedList<VcCompMapping>> GetPagedListCompMappingsForBasicReportAsync(int page, int pageSize, int compSn = 0, string excutorId = null, int bizWorkSn = 0)
        //{
        //    return await scCompMappingRepository.GetPagedListCompMappingsForBasicReportAsync(page, pageSize, compSn, excutorId, bizWorkSn);
        //}


        //public async Task<IList<VcCompMapping>> GetCompMappingsAsync(int compSn, int bizWorkSn = 0, string status = null, string compNm = null)
        //{
        //    IEnumerable<VcCompMapping> listScCompMappingTask = null;


        //    if ((bizWorkSn == 0) && string.IsNullOrEmpty(status) && string.IsNullOrEmpty(compNm)) //000
        //    {
        //        listScCompMappingTask =
        //            await
        //                scCompMappingRepository.GetCompMappingsAsync(
        //                    scm => scm.ScBizWork.ScCompInfo.CompSn == compSn && scm.Status != "D");
        //        return listScCompMappingTask.OrderByDescending(scm => scm.RegDt).ToList();
        //    }
        //    if ((bizWorkSn == 0) && string.IsNullOrEmpty(status) && !string.IsNullOrEmpty(compNm)) //001
        //    {
        //        listScCompMappingTask =
        //            await
        //                scCompMappingRepository.GetCompMappingsAsync(
        //                    scm =>
        //                        scm.ScBizWork.ScCompInfo.CompSn == compSn && scm.ScCompInfo.CompNm.Contains(compNm) &&
        //                        scm.Status != "D");
        //        return listScCompMappingTask.OrderByDescending(scm => scm.RegDt).ToList();
        //    }
        //    if ((bizWorkSn == 0) && !string.IsNullOrEmpty(status) && string.IsNullOrEmpty(compNm)) //010
        //    {
        //        listScCompMappingTask =
        //            await
        //                scCompMappingRepository.GetCompMappingsAsync(
        //                    scm => scm.ScBizWork.ScCompInfo.CompSn == compSn && scm.Status == status);
        //        return listScCompMappingTask.OrderByDescending(scm => scm.RegDt).ToList();
        //    }
        //    if ((bizWorkSn == 0) && !string.IsNullOrEmpty(status) && !string.IsNullOrEmpty(compNm)) //011
        //    {
        //        listScCompMappingTask =
        //            await
        //                scCompMappingRepository.GetCompMappingsAsync(
        //                    scm =>
        //                        scm.ScBizWork.ScCompInfo.CompSn == compSn && scm.Status == status &&
        //                        scm.ScCompInfo.CompNm.Contains(compNm));
        //        return listScCompMappingTask.OrderByDescending(scm => scm.RegDt).ToList();
        //    }
        //    if ((bizWorkSn != 0) && string.IsNullOrEmpty(status) && string.IsNullOrEmpty(compNm)) //100
        //    {
        //        listScCompMappingTask =
        //            await
        //                scCompMappingRepository.GetCompMappingsAsync(
        //                    scm =>
        //                        scm.ScBizWork.ScCompInfo.CompSn == compSn && scm.Status != "D" &&
        //                        scm.BizWorkSn == bizWorkSn);
        //        return listScCompMappingTask.OrderByDescending(scm => scm.RegDt).ToList();
        //    }
        //    if ((bizWorkSn != 0) && string.IsNullOrEmpty(status) && !string.IsNullOrEmpty(compNm)) //101
        //    {
        //        listScCompMappingTask =
        //            await
        //                scCompMappingRepository.GetCompMappingsAsync(
        //                    scm =>
        //                        scm.ScBizWork.ScCompInfo.CompSn == compSn && scm.Status != "D" &&
        //                        scm.BizWorkSn == bizWorkSn && scm.ScCompInfo.CompNm.Contains(compNm));
        //        return listScCompMappingTask.OrderByDescending(scm => scm.RegDt).ToList();
        //    }
        //    if ((bizWorkSn != 0) && !string.IsNullOrEmpty(status) && string.IsNullOrEmpty(compNm)) //110
        //    {
        //        listScCompMappingTask =
        //            await
        //                scCompMappingRepository.GetCompMappingsAsync(
        //                    scm =>
        //                        scm.ScBizWork.ScCompInfo.CompSn == compSn && scm.Status == status &&
        //                        scm.BizWorkSn == bizWorkSn);
        //        return listScCompMappingTask.OrderByDescending(scm => scm.RegDt).ToList();
        //    }
        //    listScCompMappingTask =
        //        await
        //            scCompMappingRepository.GetCompMappingsAsync(
        //                scm =>
        //                    scm.ScBizWork.ScCompInfo.CompSn == compSn && scm.Status == status &&
        //                    scm.BizWorkSn == bizWorkSn && scm.ScCompInfo.CompNm.Contains(compNm));
        //        return listScCompMappingTask.OrderByDescending(scm => scm.RegDt).ToList();
        //    }


        //public async Task<IList<VcCompMapping>> GetExpertCompMappingsAsync(string expertId, int bizWorkSn = 0,
        //    string comName = null)
        //{
        //    IEnumerable<VcCompMapping> listScCompMappingTask = null;


        //    if (!string.IsNullOrEmpty(expertId) && bizWorkSn == 0 && string.IsNullOrEmpty(comName)) //100
        //    {
        //        listScCompMappingTask = await scCompMappingRepository.GetExpertCompanysAsync(expertId);
        //        return listScCompMappingTask.OrderByDescending(scm => scm.RegDt).ToList();
        //    }
        //    if (!string.IsNullOrEmpty(expertId) && (bizWorkSn == 0) && !string.IsNullOrEmpty(comName)) //001
        //    {
        //        listScCompMappingTask = await scCompMappingRepository.GetExpertCompanysAsync(expertId, comName);
        //        return listScCompMappingTask.OrderByDescending(scm => scm.RegDt).ToList();
        //    }
        //    if ((bizWorkSn != 0) && string.IsNullOrEmpty(comName)) //010
        //    {
        //        listScCompMappingTask =
        //            await
        //                scCompMappingRepository.GetExpertCompanysAsync(
        //                    scm => scm.Status == "A" && scm.ScBizWork.BizWorkSn == bizWorkSn);
        //        return listScCompMappingTask.OrderByDescending(scm => scm.RegDt).ToList();
        //    }
        //    if ((bizWorkSn != 0) && !string.IsNullOrEmpty(comName)) //010
        //    {
        //        listScCompMappingTask =
        //            await
        //                scCompMappingRepository.GetExpertCompanysAsync(
        //                    scm =>
        //                        scm.Status == "A" && scm.ScBizWork.BizWorkSn == bizWorkSn &&
        //                        scm.ScCompInfo.CompNm.Contains(comName));
        //        return listScCompMappingTask.OrderByDescending(scm => scm.RegDt).ToList();
        //    }
        //        listScCompMappingTask = await scCompMappingRepository.GetExpertCompanysAsync(expertId);
        //        return listScCompMappingTask.OrderByDescending(scm => scm.RegDt).ToList();
        //}

        //public async Task<IList<VcCompMapping>> GetExpertCompMappingsAsync(string expertId, int bizWorkSn = 0, int compSn = 0)
        //{
        //    var date = DateTime.Now.Date;

        //    var listScCompMapping = await scCompMappingRepository.GetExpertCompanysAsync(expertId);

        //    return listScCompMapping
        //            .Where(cmp => bizWorkSn == 0 ? cmp.ScBizWork.BizWorkSn > 0 : cmp.ScBizWork.BizWorkSn == bizWorkSn)
        //            .Where(cmp => compSn == 0 ? cmp.CompSn > 0 : cmp.CompSn == compSn)
        //            .ToList();
        //}

        //public async Task<IList<VcCompMapping>> GetCompMappingListByMentorId(string mentorId = null,
        //    string status = null)
        //{
        //    if (!string.IsNullOrEmpty(mentorId) && !string.IsNullOrEmpty(status))
        //    {
        //        return
        //            await
        //                scCompMappingRepository.GetCompMappingsAsync(
        //                    cmp => cmp.MentorId == mentorId && cmp.Status == status);
        //    }
        //    if (!string.IsNullOrEmpty(mentorId) && string.IsNullOrEmpty(status))
        //    {
        //        return await scCompMappingRepository.GetCompMappingsAsync(cmp => cmp.MentorId == mentorId);
        //    }
        //    if (string.IsNullOrEmpty(mentorId) && !string.IsNullOrEmpty(status))
        //    {
        //        return await scCompMappingRepository.GetCompMappingsAsync(cmp => cmp.Status == status);
        //    }
        //    return await scCompMappingRepository.GetCompMappingsAsync(cmp => cmp.BizWorkSn > 0);
        //}

        //// 멘토가 담당하는 기업 list
        //public async Task<IList<VcCompMapping>> GetCompMappingListByMentorId(string mentorId, string status = "A",
        //    int bizWorkSn = 0, int bizWorkYear = 0)
        //{
        //    var date = DateTime.Now.Date;

        //    var listScCompMapping =
        //        await
        //            scCompMappingRepository.GetCompMappingsAsync(cmp => cmp.MentorId == mentorId && cmp.Status == status);

        //    return listScCompMapping.Where(cmp => cmp.Status == "A").ToList();

        //    if (bizWorkYear == 0)
        //    {
        //        return listScCompMapping.Where(cmp => cmp.ScBizWork.BizWorkEdDt.Value > date)
        //            .Where(cmp => bizWorkSn == 0 ? cmp.ScBizWork.BizWorkSn > 0 : cmp.ScBizWork.BizWorkSn == bizWorkSn)
        //            .ToList();
        //    }
        //        return listScCompMapping.Where(cmp => cmp.ScBizWork.BizWorkEdDt.Value > date)
        //            .Where(cmp => bizWorkSn == 0 ? cmp.ScBizWork.BizWorkSn > 0 : cmp.ScBizWork.BizWorkSn == bizWorkSn)
        //        .Where(
        //            cmp =>
        //                cmp.ScBizWork.BizWorkStDt.Value.Year <= bizWorkYear &&
        //                cmp.ScBizWork.BizWorkEdDt.Value.Year >= bizWorkYear).ToList();
        //}

        //public async Task<IList<ScCompMapping>> GetCompMappingPagedListByMentorId(string mentorId, string status = "A",
        //    int bizWorkSn = 0, int bizWorkYear = 0)
        //{
        //    var date = DateTime.Now.Date;

        //    var listScCompMapping =
        //        await
        //            scCompMappingRepository.GetCompMappingsAsync(cmp => cmp.MentorId == mentorId && cmp.Status == status);

        //    if (bizWorkYear == 0)
        //    {
        //        return listScCompMapping.Where(cmp => cmp.ScBizWork.BizWorkEdDt.Value > date)
        //            .Where(cmp => bizWorkSn == 0 ? cmp.ScBizWork.BizWorkSn > 0 : cmp.ScBizWork.BizWorkSn == bizWorkSn)
        //            .ToList();
        //    }
        //        return listScCompMapping.Where(cmp => cmp.ScBizWork.BizWorkEdDt.Value > date)
        //            .Where(cmp => bizWorkSn == 0 ? cmp.ScBizWork.BizWorkSn > 0 : cmp.ScBizWork.BizWorkSn == bizWorkSn)
        //        .Where(
        //            cmp =>
        //                cmp.ScBizWork.BizWorkStDt.Value.Year <= bizWorkYear &&
        //                cmp.ScBizWork.BizWorkEdDt.Value.Year >= bizWorkYear).ToList();
        //}

        //public async Task<IList<VcCompMapping>> GetCompMappingListByCompSn(int compSn, string status = "A",
        //    int bizWorkSn = 0, int bizWorkYear = 0)
        //{
        //    var date = DateTime.Now.Date;

        //    var listScCompMapping =
        //        await scCompMappingRepository.GetCompMappingsAsync(cmp => cmp.CompSn = compSn && cmp.Status == status);

        //    if (bizWorkYear == 0)
        //    {
        //        return listScCompMapping.Where(cmp => cmp.ScBizWork.BizWorkEdDt.Value > date)
        //            .Where(cmp => bizWorkSn == 0 ? cmp.ScBizWork.BizWorkSn > 0 : cmp.ScBizWork.BizWorkSn == bizWorkSn)
        //            .ToList();
        //    }
        //        return listScCompMapping.Where(cmp => cmp.ScBizWork.BizWorkEdDt.Value > date)
        //            .Where(cmp => bizWorkSn == 0 ? cmp.ScBizWork.BizWorkSn > 0 : cmp.ScBizWork.BizWorkSn == bizWorkSn)
        //        .Where(
        //            cmp =>
        //                cmp.ScBizWork.BizWorkStDt.Value.Year <= bizWorkYear &&
        //                cmp.ScBizWork.BizWorkEdDt.Value.Year >= bizWorkYear).ToList();
        //}

        //public async Task<IList<VcCompMapping>> GetCompMappingsForCompanyAsync(int compSn)
        //{
        //    var listScCompMapping =
        //        await scCompMappingRepository.GetCompMappingsAsync(cmp => cmp.CompSn = compSn && cmp.Status == "A");
        //    return listScCompMapping;
        //}


        //public async Task<IList<VcCompInfo>> GetBizWorkComList(int mngComSn, string excutorId = null, int bizWorkSn = 0,
        //    int bizWorkYear = 0)
        //{
        //    if (string.IsNullOrEmpty(excutorId))
        //    {
        //        var listScCompMapping =
        //            await
        //                scCompMappingRepository.GetCompMappingsAsync(
        //                    cmp => cmp.ScBizWork.MngCompSn == mngComSn && cmp.Status == "A");
        //        var scCompInfos =
        //            listScCompMapping.Where(cmp => bizWorkSn == 0 ? cmp.BizWorkSn > 0 : cmp.BizWorkSn == bizWorkSn)
        //                .Where(
        //                    cmp =>
        //                        bizWorkYear == 0
        //                            ? cmp.ScBizWork.BizWorkStDt.Value.Year > 0
        //                            : cmp.ScBizWork.BizWorkStDt.Value.Year <= bizWorkYear &&
        //                              cmp.ScBizWork.BizWorkEdDt.Value.Year >= bizWorkYear)
        //             .Select(cmp => cmp.ScCompInfo).Distinct();

        //        return scCompInfos.OrderByDescending(sc => sc.CompNm).ToList();
        //    }
        //    else
        //    {
        //        var listScCompMapping =
        //            await
        //                scCompMappingRepository.GetCompMappingsAsync(
        //                    cmp =>
        //                        cmp.ScBizWork.MngCompSn == mngComSn && cmp.ScBizWork.ExecutorId == excutorId &&
        //                        cmp.Status == "A");
        //        var scCompInfos =
        //            listScCompMapping.Where(cmp => bizWorkSn == 0 ? cmp.BizWorkSn > 0 : cmp.BizWorkSn == bizWorkSn)
        //                .Where(
        //                    cmp =>
        //                        bizWorkYear == 0
        //                            ? cmp.ScBizWork.BizWorkStDt.Value.Year > 0
        //                            : cmp.ScBizWork.BizWorkStDt.Value.Year <= bizWorkYear &&
        //                              cmp.ScBizWork.BizWorkEdDt.Value.Year >= bizWorkYear)
        //                            .Select(cmp => cmp.ScCompInfo).Distinct();

        //        return scCompInfos.OrderByDescending(sc => sc.CompNm).ToList();
        //    }
        //}

        //멘토가 담당하는 기업 list
        //public async Task<IList<VcCompMapping>> GetCompMappingListByMentorId(string mentorId, string status = "A",
        //    int bizWorkSn = 0, int bizWorkYear = 0)
        //{
        //    var date = DateTime.Now.Date;

        //    var listScCompMapping =
        //        await
        //            scCompMappingRepository.GetCompMappingsAsync(cmp => cmp.MentorId == mentorId && cmp.Status == status);

        //    return listScCompMapping.Where(cmp => cmp.Status == "A").ToList();
        //}

        #region SaveContext

        public void SaveDbContext()
        {
            unitOfWork.Commit();
        }

        public async Task<int> SaveDbContextAsync()
        {
            return await unitOfWork.CommitAsync();
        }

        #endregion

    }
}
