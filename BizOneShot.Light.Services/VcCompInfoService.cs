using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BizOneShot.Light.Dao.Infrastructure;
using BizOneShot.Light.Dao.Repositories;
using BizOneShot.Light.Models.WebModels;

namespace BizOneShot.Light.Services
{
    public interface IVcCompInfoService : IBaseService
    {
        Task<VcCompInfo> getVcCompInfoById(string loginId);
        Task<VcCompInfo> getVcCompInfoByKey(int TcmsKey);
        Task<VcCompInfo> getVcCompInfoByCompSn(int compSn);
        Task<IList<VcUsrInfo>> getVcCompInfoForSearchId(string registationNo);

        Task<IList<VcCompInfo>> GetVcCompInfoByRegistationNo(string registationNo);

        Task<VcCompMapping> getCompMappingInfo(string compSn);
        Task<VcCompMapping> getCompMappingInfos(string compSn, string baSn);
        Task<int> AddCompInfo(VcCompInfo compinfo);
        Task<VcCompInfo> getcompinfoByrsn(string registationNo);

        //add loy
        Task<int> insertCompMapping(VcCompMapping vcCompMapping);
        Task<VcCompMapping> getCompMappingByCpMpCode(string cpMpCode);

        // check add loy
        Task<IList<VcCompInfo>> getVcCompInfoByLoginKey(int tcmsLoginKey);
        void insertVcCompInfo(VcCompInfo vcCompInfo);

        //compMapping List 가져오기
        Task<IList<VcCompMapping>> getCompMappingInfosList(int compSn, int baSn);
        // compSn 던져서 compMapping List 가져오기
        Task<IList<VcCompMapping>> getCompMappingForSn(int compSn);
        // 보고서 작성자 확인
        Task<IList<VcCompMapping>> getReportWriterList(int compSn, int baSn, string numSn);

        // BA 보고서 작성자 확인
        Task<VcCompMapping> checkBaWriter(int compSn, int baSn, string numSn, string conCode);

        // BA에 매핑된 기업& 멘토 List 가져오기 
        Task<IList<VcCompMapping>> baGetCompMentorMappingList(int baSn);

        // ba 보고서 작성자 확인 List
        Task<IList<VcCompMapping>> checkBaWriterList(int compSn, int baSn, string numSn);

    }


    public class VcCompInfoService : IVcCompInfoService
    {
        private readonly IVcCompInfoRepository vcCompInfoRespository;
        private readonly IUnitOfWork unitOfWork;

        private readonly IVcCompMappingRepository vcCompMappingRepository;

        public VcCompInfoService(IVcCompInfoRepository vcCompInfoRespository
            , IUnitOfWork unitOfWork
            , IVcCompMappingRepository vcCompMappingRepository)
        {
            this.vcCompInfoRespository = vcCompInfoRespository;
            this.unitOfWork = unitOfWork;
            this.vcCompMappingRepository = vcCompMappingRepository;
        }

        //  methods.... 
        public async Task<VcCompInfo> getVcCompInfoById(string loginId)
        {
            var iLoginId = int.Parse(loginId);
            return await vcCompInfoRespository.getVcCompInfoById(vc => vc.TcmsLoginKey == iLoginId);
        }
        public async Task<VcCompInfo> getcompinfoByrsn(string registationNo)
        {
            var rns = await vcCompInfoRespository.getCompInfo(registationNo);
            return rns;
        }
        public async Task<VcCompInfo> getVcCompInfoByCompSn(int compSn)
        {
            return await vcCompInfoRespository.getVcCompInfoByCompSn(vc => vc.CompSn == compSn);
        }

        public async Task<IList<VcUsrInfo>> getVcCompInfoForSearchId(string registationNo)
        {
            //var scCompInfo = await vcCompInfoRespository.GetAsync(scr => scr.RegistrationNo == registationNo);

            //if (scCompInfo == null)
            //    return new List<VcUsrInfo>();
            //return scCompInfo.VcUsrInfo.ToList();
            return null;
        }

        public async Task<IList<VcCompInfo>> GetVcCompInfoByRegistationNo(string registationNo)
        {
            var scCompInfos = await vcCompInfoRespository.GetManyAsync(scr => scr.RegistrationNo == registationNo);
            return scCompInfos.ToList();
        }

        public async Task<VcCompMapping> getCompMappingInfo(string compSn)
        {
            return await vcCompMappingRepository.getCompMappingInfo(compSn);
        }

        public async Task<VcCompMapping> getCompMappingInfos(string compSn, string baSn)
        {
            return await vcCompMappingRepository.getCompMappingInfos(compSn, baSn);
        }

        public async Task<int> insertCompMapping(VcCompMapping vcCompMapping)
        {
            var result = await vcCompMappingRepository.InsertToAsync(vcCompMapping);

            return await SaveDbContextAsync();
        }

        public async Task<VcCompMapping> getCompMappingByCpMpCode(string cpMpCode)
        {
            return await vcCompMappingRepository.getCompMappingByCpMpCode(cpMpCode);
        }

        public async Task<IList<VcCompInfo>> getVcCompInfoByLoginKey(int tcmsLoginKey)
        {

            return await vcCompInfoRespository.getVcCompInfoByLoginKey(vc => vc.TcmsLoginKey == tcmsLoginKey);
        }

        public async Task<IList<VcCompMapping>> getCompMappingInfosList(int compSn, int baSn)
        {
            return await vcCompMappingRepository.getCompMappingInfosList(compSn, baSn);
        }

        public async Task<IList<VcCompMapping>> getReportWriterList(int compSn, int baSn, string numSn)
        {
            return await vcCompMappingRepository.getReportWriterList(compSn, baSn, numSn);
        }

        public async Task<VcCompMapping> checkBaWriter(int compSn, int baSn, string numSn, string conCode)
        {
            return await vcCompMappingRepository.checkBaWriter(compSn, baSn, numSn, conCode);
        }
        
        public async Task<IList<VcCompMapping>> baGetCompMentorMappingList(int baSn)
        {
            return await vcCompMappingRepository.baGetCompMentorMappingList(baSn);
        }

        public async Task<IList<VcCompMapping>> checkBaWriterList(int compSn, int baSn, string numSn)
        {
            return await vcCompMappingRepository.checkBaWriterList(compSn, baSn, numSn);
        }


        public void SaveDbContext()
        {
            unitOfWork.Commit();
        }

        public async Task<int> SaveDbContextAsync()
        {
            return await unitOfWork.CommitAsync();
        }

        public async Task<int> AddCompInfo(VcCompInfo compinfo)
        {
            var addcomp = vcCompInfoRespository.insertToAsync(compinfo);

            return await SaveDbContextAsync();
        }

        public async Task<VcCompInfo> getVcCompInfoByKey(int TcmsKey)
        {
            return await vcCompInfoRespository.getVcCompInfoById(vc => vc.TcmsLoginKey == TcmsKey);
        }
        public void insertVcCompInfo(VcCompInfo vcCompInfo)
        {
            vcCompInfoRespository.Add(vcCompInfo);
            SaveDbContext();
        }

        public async Task<IList<VcCompMapping>> getCompMappingForSn(int compSn)
        {
            return await vcCompMappingRepository.getCompMappingForSn(compSn);
        }
    }
}