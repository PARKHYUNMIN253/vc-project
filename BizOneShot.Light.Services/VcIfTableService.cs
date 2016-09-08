using BizOneShot.Light.Dao.Infrastructure;
using BizOneShot.Light.Dao.Repositories;
using BizOneShot.Light.Models.WebModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizOneShot.Light.Services
{
    public interface IVcIfTableService : IBaseService
    {
        void insertVcIfNumInfo(VcIfNumInfo vcIfNumInfo);
        void insertVcIfTcmsInfo(VcIfTcmsInfo vcIfTcmsInfo);
        void insertVcIfCompMapping(VcIfCompMapping vcIfCompMapping);
        void insertVcIfQuesCompInfo(VcIfQuesCompInfo vcIfQuesCompInfo);
        //test
        Task<int> insertVcIfUsrInfo(VcIfUsrInfo vcIfUsrInfo);
        Task<int> insertVcIfCompInfo(VcIfCompInfo vcIfCompInfo);
        Task<int> insertVcIfBaInfo(VcIfBaInfo vcIfBaInfo);
        Task<int> insertVcIfMentorInfo(VcIfMentorInfo vcIfMentorInfo);

        // add loy
        Task<int> insertQuesCompInfoForIf(VcIfQuesCompInfo vcIfQuesCompInfo);
        Task<int> insertCompMappingForIf(VcIfCompMapping vcIfCompMapping);
        Task<int> insertTcmsInfoForIf(VcIfTcmsInfo vcTcmsInfo);
        Task<int> insertNumMngForIf(VcIfNumInfo vcIfNumMngInfo);
        //Task<IList<VcIfNumInfo>> GetNumInfoAsync(string numSn);

        // 해당 객체 조회용 method
        Task<VcIfUsrInfo> getVcIfUsrInfoByInfId(string infId);

        // 해당 객체 조회용 method add loy
        Task<VcIfNumInfo> getVcIfNumInfoByInfId(string infId);
        Task<VcIfTcmsInfo> getVcIfTcmsInfoByInfId(string infId);
        Task<VcIfCompMapping> getVcIfCompMappingByInfId(string infId);
        Task<VcIfCompInfo> getVcIfCompInfoByInfId(string infId);
        Task<VcIfBaInfo> getVcIfBaInfoInfId(string InfId);
        Task<VcIfMentorInfo> getVcIfMentorInfId(string InfId);
        Task<VcIfQuesCompInfo> getVcIfQuesCompInfoByInfId(string infId); // 8번

        // TEST 동기식
        VcIfNumInfo getTest(string infId);
    }

    public class VcIfTableService : IVcIfTableService
    {
        private readonly IVcIfUsrInfoRepository _vcIfUsrInfoRepository;
        private readonly IVcIfCompInfoRepository _vcIfCompInfoRepository;
        private readonly IVcIfBaInfoRepository _vcIfBaInfoRepository;
        private readonly IVcIfMentorInfoRepository _vcIfMentorInfoRepository;
        private readonly IVcIfNumInfoRepository _vcIfNumInfoRepository;
        private readonly IVcIfTcmsInfoRepository _vcIfTcmsInfoRepository;
        private readonly IVcIfCompMappingRepository _vcIfCompMappingRepository;
        private readonly IVcIfQuesCompInfoRepository _vcIfQuesCompInfoRepository;

        private readonly IUnitOfWork unitOfWork;

        public VcIfTableService(
            IVcIfUsrInfoRepository _vcIfUsrInfoRepository,
            IVcIfCompInfoRepository _vcIfCompInfoRepository,
            IVcIfBaInfoRepository _vcIfBaInfoRepository,
            IVcIfMentorInfoRepository _vcIfMentorInfoRepository,
            IVcIfNumInfoRepository _vcIfNumInfoRepository,
            IVcIfTcmsInfoRepository _vcIfTcmsInfoRepository,
            IVcIfCompMappingRepository _vcIfCompMappingRepository,
            IVcIfQuesCompInfoRepository _vcIfQuesCompInfoRepository,
            IUnitOfWork unitOfWork
            )
        {
            this._vcIfUsrInfoRepository = _vcIfUsrInfoRepository;
            this._vcIfCompInfoRepository = _vcIfCompInfoRepository;
            this._vcIfBaInfoRepository = _vcIfBaInfoRepository;
            this._vcIfMentorInfoRepository = _vcIfMentorInfoRepository;
            this._vcIfNumInfoRepository = _vcIfNumInfoRepository;
            this._vcIfTcmsInfoRepository = _vcIfTcmsInfoRepository;
            this._vcIfCompMappingRepository = _vcIfCompMappingRepository;
            this._vcIfQuesCompInfoRepository = _vcIfQuesCompInfoRepository;
            this.unitOfWork = unitOfWork;
        }

        // # 1. 
        public async Task<int> insertVcIfUsrInfo(VcIfUsrInfo vcIfUsrInfo)
        {
            var result = await _vcIfUsrInfoRepository.insertToAsync(vcIfUsrInfo); 
            return await SaveDbContextAsync();
        }
        // # 2. 
        public async Task<int> insertVcIfCompInfo(VcIfCompInfo vcIfCompInfo)
        {
            var result = await _vcIfCompInfoRepository.insertComp(vcIfCompInfo);
            return await SaveDbContextAsync();
        }
        // # 3. 
        public async Task<int> insertVcIfBaInfo(VcIfBaInfo vcIfBaInfo)
        {
            var result = await _vcIfBaInfoRepository.insertBa(vcIfBaInfo);
            return await SaveDbContextAsync();
        }
        // # 4. 
        public async Task<int> insertVcIfMentorInfo(VcIfMentorInfo vcIfMentorInfo)
        {
            var result = await _vcIfMentorInfoRepository.InsertMentor(vcIfMentorInfo);
            return await SaveDbContextAsync();
        }
        // # 5.
        public async void insertVcIfNumInfo(VcIfNumInfo vcIfNumInfo)
        {
            _vcIfNumInfoRepository.Add(vcIfNumInfo);
            await SaveDbContextAsync();
        }
        // # 6.
        public async void insertVcIfTcmsInfo(VcIfTcmsInfo vcIfTcmsInfo)
        {
            _vcIfTcmsInfoRepository.Add(vcIfTcmsInfo);
            await SaveDbContextAsync();
        }
        // # 7.
        public async void insertVcIfCompMapping(VcIfCompMapping vcIfCompMapping)
        {
            _vcIfCompMappingRepository.Add(vcIfCompMapping);
            await SaveDbContextAsync();
        }
        // # 8.
        public async void insertVcIfQuesCompInfo(VcIfQuesCompInfo vcIfQuesCompInfo)
        {
            _vcIfQuesCompInfoRepository.Add(vcIfQuesCompInfo);
            await SaveDbContextAsync();
        }

        // loy add insert IfTable
        public async Task<int> insertQuesCompInfoForIf(VcIfQuesCompInfo vcIfQuesCompInfo)
        {
            var result = await _vcIfQuesCompInfoRepository.inertToAsync(vcIfQuesCompInfo);

            return await SaveDbContextAsync();
        }

        public async Task<int> insertCompMappingForIf(VcIfCompMapping vcIfCompMapping)
        {
            var result = await _vcIfCompMappingRepository.inertToAsync(vcIfCompMapping);

            return await SaveDbContextAsync();
        }

        public async Task<int> insertTcmsInfoForIf(VcIfTcmsInfo vcTcmsInfo)
        {
            var result = await _vcIfTcmsInfoRepository.inertToAsync(vcTcmsInfo);

            return await SaveDbContextAsync();
        }

        public async Task<int> insertNumMngForIf(VcIfNumInfo vcIfNumMngInfo)
        {
            var result = await _vcIfNumInfoRepository.inertToAsync(vcIfNumMngInfo);

            return await SaveDbContextAsync();
        }

        // 중복값 check
        //public async Task<IList<VcIfNumInfo>> GetNumInfoAsync(string numSn)
        //{
        //    var result = await _vcIfNumInfoRepository.getNumSn(numSn);
        //    return result;
        //}



        // vc_if_usr_info 테이블 객체 조회
        public async Task<VcIfUsrInfo> getVcIfUsrInfoByInfId(string infId)
        {
            return await _vcIfUsrInfoRepository.getVcIfUsrInfoByInfId(infId);
        }

        // vc_if_num_info 테이블 객체 조회
        public async Task<VcIfNumInfo> getVcIfNumInfoByInfId(string infId)
        {
            return await _vcIfNumInfoRepository.getVcIfNumInfoByInfId(infId);
        }

        public async Task<VcIfTcmsInfo> getVcIfTcmsInfoByInfId(string infId)
        {
            return await _vcIfTcmsInfoRepository.getVcIfTcmsInfoByInfId(infId);
        }

        public async Task<VcIfCompMapping> getVcIfCompMappingByInfId(string infId)
        {
            return await _vcIfCompMappingRepository.getVcIfCompMappingByInfId(infId);
        }

        public async Task<VcIfCompInfo> getVcIfCompInfoByInfId(string infId)
        {
            return await _vcIfCompInfoRepository.getVcIfCompInfoByInfId(infId);
        }

        public async Task<VcIfBaInfo> getVcIfBaInfoInfId(string InfId)
        {
            return await _vcIfBaInfoRepository.getVcIfBaInfoByInfId(InfId);
        }
        public async Task<VcIfMentorInfo> getVcIfMentorInfId(string InfId)
        {
            return await _vcIfMentorInfoRepository.getVcIfMentorInfId(InfId);
        }


        public async Task<VcIfQuesCompInfo> getVcIfQuesCompInfoByInfId(string infId)
        {
            return await _vcIfQuesCompInfoRepository.getVcIfQuesCompInfoByInfId(infId);
        }


        public VcIfNumInfo getTest(string infId)
        {
            return _vcIfNumInfoRepository.getTest(bw => bw.InfId == infId);
        }



        #region saveDbContext
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
