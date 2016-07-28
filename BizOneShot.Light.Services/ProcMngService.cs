using BizOneShot.Light.Dao.Infrastructure;
using BizOneShot.Light.Dao.Repositories;
using BizOneShot.Light.Models.WebModels;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizOneShot.Light.Services
{
    public interface IProcMngService : IBaseService
    {
        //Task<IList<ProcMentorGetReportListByIdReturnModel>> getMentorReportListById(object[] parameters);
        Task<IList<ProcReportGetBizWorkInfoReturnModel>> getBizWorkInfoByBizWorkSn(object[] parameters);
        Task<IList<ProcMentorGetCompMappingReturnModel>> getCompMapping(object[] parameters);
        Task<IList<ProcBaMentorMappingReturnModel>> getBaMentorMapping(object[] parameters);
        Task<IList<ProcBaGetReportCompInfoReturnModel>> getBaReportCompMapping(object[] parameters);

        Task<IList<ProcMentorGetMentoringReportReturnModel>> getMentoringReport(object[] parameters);
        Task<IList<ProcMentorGetDeepenReportReturnModel>> getDeepenReport(object[] parameters);
        Task<IList<ProcBaMentorAddMappingReturnModel>> getBaMentorNonMapping(object[] parameters);

        Task<IList<ProcMentorGetReportListReturnModel>> getMentorReportList(object[] parameters);
        Task<IList<ProcBaGetMentoringReportReturnModel>> getBaMentoringReport(object[] parameters);
        Task<IList<ProcBaGetDeepenReportReturnModel>> getBaDeepenReport(object[] parameters);

        // phm
        IPagedList<ProcCompGetDeepenReportListNumsReturnModel> getDeepenReportListNums(object[] parameters, int page, int pageSize);
        IPagedList<ProcCompGetDeepenReportListReturnModel> getDeepenReportList2(object[] parameters, int page, int pageSize);
        IList<ProcCompGetDeepenReportListReturnModel> getDeepenReportList3(object[] parameters);

        Task<IList<ProcCompGetDeepenReportListReturnModel>> getDeepenReportListL(object[] parameters);
        Task<IList<ProcMentorGetDeepenReportListReturnModel>> getDeepenReportList(object[] parameters);
        Task<IList<ProcBaCompConCodeReturnModel>> getBaCompConcode(object[] parameters);

        // loy
        Task<IList<ProcMentorGetCompNumSnReturnModel>> getMentorCompNumSn(object[] parameters);

        // 심화보고서 등록시 필요한 정보들 가저오기
        Task<ProcMentorRegDeepenReportReturnModel> getMentorRegDeepenReport(object[] parameters);

        // BA가 기업 & Mentor 매핑할때 필요한 기업 List 가져오기
        Task<IList<ProcBaGetCompSelectListReturnModel>> baGetCompSelectList(object[] parameters);

        // BA가 기업 & Mentor 매핑된 List 가져오기
        Task<IList<ProcBaGetCompMentorMappingListReturnModel>> baGetCompMentorMappingList(object[] parameters);

        // BA가 REPORT LIST가져오는 부분
        Task<IList<ProcBaGetReportListReturnModel>> baGetReportList(object[] parameters);

        // 멘토가 보고서 조회 list
        Task<IList<ProcMentorGetReportList2ReturnModel>> mentorGetReportList2(object[] parameters);

        // 멘토가 심화보고서 조회 list 수정
        Task<IList<ProcMentorGetDeepenReportList2ReturnModel>> MentorGetDeepenReportList(object[] parameters);
    }

    public class ProcMngService : IProcMngService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IProcReportGetBizWorkInfoRepository procReportGetBizWorkInfoRepository;
        private readonly IProcMentorGetCompMappingRepository procMentorGetCompMappingRepository;
        private readonly IProcBaGetMentorRepository procBaGetMentorRepository;
        private readonly IProcBaGetReportCompInfoRepository procBaGetReportCompInfoRepository;

        private readonly IProcMentorGetMentoringReportRepository procMentorGetMentoringReportRepository;
        private readonly IProcMentorGetDeepenReportRepository procMentorGetDeepenRepository;
        private readonly IProcBaMentorAddMappingRepository procBaMentorAddMappingRepository;

        private readonly IProcMentorGetReportListRepository procMentorGetReportListRepository;
        private readonly IProcBaGetMentoringReportRepository procBaGetMentoringReportRepository;
        private readonly IProcBaGetDeepenReportRepository procBaGetDeepenReportRepository;

        // phm
        private readonly IProcCompGetDeepenReportListNumsRepository procCompGetDeepenReportListNumsRepository;
        private readonly IProcCompGetDeepenReportListRepository procCompGetDeepenReportListRepository;
        private readonly IProcMentorGetDeepenReportListRepository procMentorGetDeepenReportListRepository;

        // loy 20160613
        private readonly IProcBaCompConCodeRepository procBaCompConCodeRepository;
        private readonly IProcMentorGetCompNumSnRepository procMentorGetCompNumSnRepository;

        // loy 20160714
        private readonly IProcMentorRegDeepenReportRepository procMentorRegDeepenReportRepository;

        // loy 20160715
        private readonly IProcBaGetCompSelectListRepository procBaGetCompSelectListRepository;

        // loy 20160718
        private readonly IProcBaGetCompMentorMappingListRepository procBaGetCompMentorMappingListRepository;

        // loy 20160721
        private readonly IProcBaGetReportListRepository procBaGetReportListRepository;
        private readonly IProcMentorGetReportList2Repository procMentorGetReportList2Repository;

        // loy 20160727 심화보고서 조회 by mentor
        private readonly IProcMentorGetDeepenReportList2Repository procMentorGetDeepenReportList2Repository;

        public ProcMngService(IUnitOfWork unitOfWork 
            , IProcReportGetBizWorkInfoRepository procReportGetBizWorkInfoRepository
            , IProcMentorGetCompMappingRepository procMentorGetCompMappingRepository
            , IProcBaGetMentorRepository procBaGetMentorRepository
            , IProcBaGetReportCompInfoRepository procBaGetReportCompInfoRepository

            , IProcMentorGetMentoringReportRepository procMentorGetMentoringReportRepository
            , IProcMentorGetDeepenReportRepository procMentorGetDeepenRepository
            , IProcBaMentorAddMappingRepository procBaMentorAddMappingRepository
            , IProcMentorGetReportListRepository procMentorGetReportListRepository
            , IProcBaGetMentoringReportRepository procBaGetMentoringReportRepository
            , IProcBaGetDeepenReportRepository procBaGetDeepenReportRepository
            , IProcCompGetDeepenReportListNumsRepository procCompGetDeepenReportListNumsRepository
            , IProcCompGetDeepenReportListRepository procCompGetDeepenReportListRepository
            , IProcMentorGetDeepenReportListRepository procMentorGetDeepenReportListRepository
            , IProcBaCompConCodeRepository procBaCompConCodeRepository
            , IProcMentorGetCompNumSnRepository procMentorGetCompNumSnRepository
            , IProcMentorRegDeepenReportRepository procMentorRegDeepenReportRepository
            , IProcBaGetCompSelectListRepository procBaGetCompSelectListRepository
            , IProcBaGetCompMentorMappingListRepository procBaGetCompMentorMappingListRepository
            , IProcBaGetReportListRepository procBaGetReportListRepository
            , IProcMentorGetReportList2Repository procMentorGetReportList2Repository
            , IProcMentorGetDeepenReportList2Repository procMentorGetDeepenReportList2Repository
            )
        {
            this.unitOfWork = unitOfWork;
            this.procReportGetBizWorkInfoRepository = procReportGetBizWorkInfoRepository;
            this.procMentorGetCompMappingRepository = procMentorGetCompMappingRepository;
            this.procBaGetMentorRepository = procBaGetMentorRepository;
            this.procBaGetReportCompInfoRepository = procBaGetReportCompInfoRepository;

            this.procMentorGetMentoringReportRepository = procMentorGetMentoringReportRepository;
            this.procMentorGetDeepenRepository = procMentorGetDeepenRepository;
            this.procBaMentorAddMappingRepository = procBaMentorAddMappingRepository;
            this.procMentorGetReportListRepository = procMentorGetReportListRepository;
            this.procBaGetMentoringReportRepository = procBaGetMentoringReportRepository;
            this.procBaGetDeepenReportRepository = procBaGetDeepenReportRepository;

            this.procCompGetDeepenReportListRepository = procCompGetDeepenReportListRepository;
            this.procCompGetDeepenReportListNumsRepository = procCompGetDeepenReportListNumsRepository;
            this.procMentorGetDeepenReportListRepository = procMentorGetDeepenReportListRepository;
            this.procBaCompConCodeRepository = procBaCompConCodeRepository;
            this.procMentorGetCompNumSnRepository = procMentorGetCompNumSnRepository;

            this.procMentorRegDeepenReportRepository = procMentorRegDeepenReportRepository;
            this.procBaGetCompSelectListRepository = procBaGetCompSelectListRepository;

            this.procBaGetCompMentorMappingListRepository = procBaGetCompMentorMappingListRepository;

            this.procBaGetReportListRepository = procBaGetReportListRepository;
            this.procMentorGetReportList2Repository = procMentorGetReportList2Repository;

            this.procMentorGetDeepenReportList2Repository = procMentorGetDeepenReportList2Repository;

        }

        //public async Task<IList<ProcMentorGetReportListByIdReturnModel>> getMentorReportListById(object[] parameters)
        //{
        //    return await procMentorGetReportListByIdRepository.getMentorReportListById(parameters);
        //}

        public async Task<IList<ProcReportGetBizWorkInfoReturnModel>> getBizWorkInfoByBizWorkSn(object[] parameters)
        {
            return await procReportGetBizWorkInfoRepository.getBizWorkInfoByBizWorkSn(parameters);
        }

        public async Task<IList<ProcMentorGetCompMappingReturnModel>> getCompMapping(object[] parameters)
        {
            return await procMentorGetCompMappingRepository.getCompMapping(parameters);
        }

        public async Task<IList<ProcBaMentorMappingReturnModel>> getBaMentorMapping(object[] parameters)
        {
            return await procBaGetMentorRepository.getBaMentorMapping(parameters);
        }

        public async Task<IList<ProcBaGetReportCompInfoReturnModel>> getBaReportCompMapping(object[] parameters)
        {
            return await procBaGetReportCompInfoRepository.getBaReportCompMapping(parameters);
        }

        public async Task<IList<ProcMentorGetMentoringReportReturnModel>> getMentoringReport(object[] parameters)
        {
            return await procMentorGetMentoringReportRepository.getMentoring(parameters);
        }

        public async Task<IList<ProcMentorGetDeepenReportReturnModel>> getDeepenReport(object[] parameters)
        {
            return await procMentorGetDeepenRepository.getDeepenReport(parameters);
        }

        public async Task<IList<ProcBaMentorAddMappingReturnModel>> getBaMentorNonMapping(object[] parameters)
        {
            return await procBaMentorAddMappingRepository.getBaMentorNonMapping(parameters);
        }

        public async Task<IList<ProcMentorGetReportListReturnModel>> getMentorReportList(object[] parameters)
        {
            return await procMentorGetReportListRepository.getMentorReportList(parameters);
        }

        public async Task<IList<ProcBaGetMentoringReportReturnModel>> getBaMentoringReport(object[] parameters)
        {
            return await procBaGetMentoringReportRepository.getBaMentoringReport(parameters);
        }

        public async Task<IList<ProcBaGetDeepenReportReturnModel>> getBaDeepenReport(object[] parameters)
        {
            return await procBaGetDeepenReportRepository.getBaDeepenReport(parameters);
        }

        public async Task<IList<ProcMentorGetDeepenReportListReturnModel>> getDeepenReportList(object[] parameters)
        {
            return await procMentorGetDeepenReportListRepository.getDeepenReportList(parameters);
        }

        // phm inserted
        //public Task<IPagedList<ProcCompGetDeepenReportListReturnModel>> getDeepenReportList(object[] parameters, int page, int pageSize)
        //{
        //    throw new NotImplementedException();
        //}

        public IPagedList<ProcCompGetDeepenReportListNumsReturnModel> getDeepenReportListNums(object[] parameters, int page, int pageSize)
        {
            return procCompGetDeepenReportListNumsRepository.getDeepenReportListNums(parameters, page, pageSize);
        }

        public IPagedList<ProcCompGetDeepenReportListReturnModel> getDeepenReportList2(object[] parameters, int page, int pageSize)
        {
            return procCompGetDeepenReportListRepository.getDeepenReportList(parameters, page, pageSize);
        }

        public IList<ProcCompGetDeepenReportListReturnModel> getDeepenReportList3(object[] parameters)
        {
            return procCompGetDeepenReportListRepository.getDeepenReportListR(parameters);
        }

        public Task<IList<ProcCompGetDeepenReportListReturnModel>> getDeepenReportListL(object[] parameters)
        {
            return procCompGetDeepenReportListRepository.getDeepenReportListL(parameters);
        }

        public async Task<IList<ProcBaCompConCodeReturnModel>> getBaCompConcode(object[] parameters)
        {
            return await procBaCompConCodeRepository.getBaCompConcode(parameters);
        }

        public async Task<IList<ProcMentorGetCompNumSnReturnModel>> getMentorCompNumSn(object[] parameters)
        {
            return await procMentorGetCompNumSnRepository.getMentorCompNumSn(parameters);
        }

        public async Task<ProcMentorRegDeepenReportReturnModel> getMentorRegDeepenReport(object[] parameters)
        {
            return await procMentorRegDeepenReportRepository.getMentorRegDeepenReport(parameters);
        }

        public async Task<IList<ProcBaGetCompSelectListReturnModel>> baGetCompSelectList(object[] parameters)
        {
            return await procBaGetCompSelectListRepository.baGetCompSelectList(parameters);
        }

        public async Task<IList<ProcBaGetCompMentorMappingListReturnModel>> baGetCompMentorMappingList(object[] parameters)
        {
            return await procBaGetCompMentorMappingListRepository.baGetCompMentorMappingList(parameters);
        }

        public async Task<IList<ProcBaGetReportListReturnModel>> baGetReportList(object[] parameters)
        {
            return await procBaGetReportListRepository.baGetReportList(parameters);
        }

        public async Task<IList<ProcMentorGetReportList2ReturnModel>> mentorGetReportList2(object[] parameters)
        {
            return await procMentorGetReportList2Repository.mentorGetReportList2(parameters);
        }

        public async Task<IList<ProcMentorGetDeepenReportList2ReturnModel>> MentorGetDeepenReportList(object[] parameters)
        {
            return await procMentorGetDeepenReportList2Repository.MentorGetDeepenReportList(parameters);
        }

        #region saveDbContexts...
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
