using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BizOneShot.Light.Dao.Infrastructure;
using BizOneShot.Light.Dao.Repositories;
using BizOneShot.Light.Models.DareModels;
using BizOneShot.Light.Models.WebModels;

namespace BizOneShot.Light.Services
{
    public interface IScUsrService : IBaseService
    {
        //IEnumerable<FaqViewModel> GetFaqs(string searchType = null, string keyword = null);

        //Task<bool> ChkLoginId(string loginId);
        Task<VcUsrInfo> getScUsrByLoginId(string loginId);
        Task<VcUsrInfo> SelectScUsr(string loginId);

        Task<VcUsrInfo> selectScUsrByTcms(string tcmsLoginKey);
        //Task<VcUsrInfo> getScUsrByCompSn(int compSn);
        //Task<VcUsrInfo> SelectMentorInfo(string loginId);
        void ModifyScUsr(VcUsrInfo scUsr);

        Task<int> AddCompanyUserAsync(VcCompInfo scCompInfo, VcUsrInfo scUsr, SHUSER_SyUser syUser);
        Task<int> AddBizManagerAsync(VcCompInfo scCompInfo);
        Task<int> AddBizManagerMemberAsync(VcUsrInfo scUsr);
        Task<IList<VcUsrInfo>> GetBizManagerAsync();
        Task<IList<VcUsrInfo>> GetBizManagerByComNameAsync(string keyword = null);
        Task<IList<VcUsrInfo>> GetExpertManagerAsync();
        Task<IList<VcUsrInfo>> GetExpertManagerAsync(string bizMngSn = null, string expertType = null);
        //Task<IList<ScUsr>> GetMentorListAsync(int mngCompSn);
        Task<VcUsrInfo> SelectScUsr(string loginId, string registrationNo);
        int UpdatePassword(SHUSER_SyUser syUser);
        int UserPasswordReset(VcUsrInfo scUsr);

        Task<VcBaInfo> getBaInfoById(string loginId);
        Task<VcMentorInfo> getMentorInfoById(string mentorId);
        Task<VcMentorInfo> getMentorInfoBySn(string mentorSn);
        
        //IF TABLE 에서 usrinfo insert
        void insertUsrforIf(VcUsrInfo scUsr);
        Task<VcUsrInfo> SelectScUsrforIf(string loginId);
        Task<VcMentorInfo> getMentorInfo(string loginid);
        Task<int> AddMentorInfo(VcMentorInfo vcMentorInfo);

        // add Loy
        Task<int> insertTcmsInfo(VcTcmsInfo vcTcmsInfo);
        Task<int> insertNumMngInfo(VcNumMngInfo vcNumMngInfo);

        // Tcms Select
        Task<VcTcmsInfo> getTcmsInfo(int tcmsLoginKey);
        Task<VcNumMngInfo> getNumInfoAsync(string numSn);


        // api - vcusrinfo 값 넣어줄 때 사용
        void insertVcUsrInfo(VcUsrInfo vcUsrInfo);

        void updateNumInfo (VcNumMngInfo vcNumMngInfo);
        void insertMentorInfo(VcMentorInfo vcMentorInfo);


        // TCMS_LOGIN_KEY로 해당 USR 정보 조회
        Task<VcUsrInfo> getUsrInfoByTcmsKey(int tcmsLoginKey);

        // numinfo들 중 
        Task<VcNumMngInfo> getNumInfoOneAsync();

        // ba에 매핑된 mentor 조회
        Task<IList<VcMentorInfo>> baGetMappingMentor(int baSn);
    }


    public class VcUsrService : IScUsrService
    {
        private readonly IDareUnitOfWork dareUnitOfWork;
        private readonly IScCompInfoRepository scCompInfoRespository;
        private readonly IScMentorMappingRepository scMentorMappingRepository;
        private readonly IScUsrRepository scUsrRespository;

        private readonly ISyUserRepository syUserRespository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IVcMentorInfoRepository vcMentorInfoRepository;

        // add loy
        private readonly IVcTcmsInfoRepository vcTcmsInfoRepository;
        private readonly IVcNumMngInfoRepository vcNumMngInfoRepository;

        public VcUsrService(IScUsrRepository scUsrRespository
            , IUnitOfWork unitOfWork
            , ISyUserRepository syUserRespository
            , IDareUnitOfWork dareUnitOfWork
            , IScCompInfoRepository scCompInfoRespository
            , IScMentorMappingRepository scMentorMappingRepository
            , IScUsrRepository scUserRepository
            , IVcMentorInfoRepository vcMentorInfoRepository
            , IVcTcmsInfoRepository vcTcmsInfoRepository
            , IVcNumMngInfoRepository vcNumMngInfoRepository)
        {
            this.scUsrRespository = scUsrRespository;
            this.unitOfWork = unitOfWork;
            this.syUserRespository = syUserRespository;
            this.dareUnitOfWork = dareUnitOfWork;
            this.scCompInfoRespository = scCompInfoRespository;
            this.scMentorMappingRepository = scMentorMappingRepository;
            this.scUsrRespository = scUsrRespository;
            this.vcMentorInfoRepository = vcMentorInfoRepository;
            this.vcTcmsInfoRepository = vcTcmsInfoRepository;
            this.vcNumMngInfoRepository = vcNumMngInfoRepository;
        }

        //public async Task<bool> ChkLoginId(string loginId)
        //{
        //    IEnumerable<VcCompInfo> listScUsrTask = null;
        //    listScUsrTask = await scUsrRespository.GetManyAsync(usr => usr.LoginId == loginId);

        //    if (listScUsrTask.Count() > 0)
        //        return false;
            
        //    // 다래 임시 주석
        //    //IEnumerable<SHUSER_SyUser> listSyUserTask = null;
        //    //listSyUserTask = await syUserRespository.GetManyAsync(usr => usr.IdUser == loginId);

        //    //if (listSyUserTask.Count() > 0)
        //    //    return false;

        //    return true;
        //}


        public async Task<VcUsrInfo> SelectScUsr(string loginId)
        {
            var iLoginId = int.Parse(loginId);
            var scUsr = await scUsrRespository.GetAsync(sc =>
                            sc.TcmsLoginKey == iLoginId);

            return scUsr;
        }

        public async Task<VcUsrInfo> selectScUsrByTcms(string tcmsLoginKey)
        {
            int loginKey = int.Parse(tcmsLoginKey);
            return await scUsrRespository.GetAsync(sc => sc.TcmsLoginKey == loginKey);
        }

        public async Task<VcUsrInfo> SelectScUsr(string loginId, string registrationNo)
        {
            var scUsr =
                await
                    scUsrRespository.GetAsync(
                        sc =>
                            sc.LoginId == loginId);

            return scUsr;
        }


        public async Task<VcUsrInfo> SelectScUsrforIf(string loginId)
        {
            var scUsr = await scUsrRespository.getUsrInfo(loginId);

            return scUsr;
        }

        public void ModifyScUsr(VcUsrInfo scUsr)
        {
            scUsrRespository.Update(scUsr);
        }


        public async Task<int> AddCompanyUserAsync(VcCompInfo scCompInfo, VcUsrInfo scUsr, SHUSER_SyUser syUser)
        {
            //var rstScUsr = scUsrRespository.Insert(scUsr);
            //scCompInfo.
            var rstScCompInfo = scCompInfoRespository.Insert(scCompInfo);      // vcCompInfoRepository
            //var rstSyUser = syUserRespository.Insert(syUser);

            //if (rstScCompInfo == null || rstSyUser != 1)
            //{
            //    return -1;
            //}

            if( rstScCompInfo == null)
            {
                return -1;
            }

            var rst = await SaveDbContextAsync();

            return rst;
            //if (rst != 1)
            //    return rst;

            //return await SaveDareDbContextAsync();
        }


        public async Task<int> AddBizManagerMemberAsync(VcUsrInfo scUsr)
        {
            var rstScUsr = scUsrRespository.Insert(scUsr);

            if (rstScUsr == null)
            {
                return -1;
            }
            return await SaveDbContextAsync();
        }

        public async Task<int> AddBizManagerAsync(VcCompInfo scCompInfo)
        {
            //var rstScUsr = scUsrRespository.Insert(scUsr);
            //scCompInfo.
            var rstScCompInfo = scCompInfoRespository.Insert(scCompInfo);

            if (rstScCompInfo == null)
            {
                return -1;
            }
            return await SaveDbContextAsync();
        }


        public async Task<IList<VcUsrInfo>> GetBizManagerAsync()
        {
            IEnumerable<VcUsrInfo> listScUsrTask = null;

            listScUsrTask =
                await
                    scUsrRespository.GetManyAsync(
                        usr => usr.UsrType == "B" );
            return listScUsrTask.OrderByDescending(usr => usr.RegDt).ToList();
        }


        public async Task<IList<VcUsrInfo>> GetBizManagerByComNameAsync(string keyword = null)
        {
            IEnumerable<VcUsrInfo> listScUsrTask = null;

            listScUsrTask =
                await
                    scUsrRespository.GetManyAsync(
                        usr =>
                            usr.UsrType == "B" /*&& usr.VcCompInfoes.CompNm.Contains(keyword)*/);

            return listScUsrTask.OrderByDescending(usr => usr.RegDt).ToList();
        }


        public async Task<IList<VcUsrInfo>> GetExpertManagerAsync()
        {
            IEnumerable<VcUsrInfo> listScUsrTask = null;

            listScUsrTask = await scUsrRespository.GetManyAsync(usr => usr.UsrType == "P");
            return listScUsrTask.OrderByDescending(usr => usr.RegDt).ToList();
        }


        public async Task<IList<VcUsrInfo>> GetExpertManagerAsync(string bizMngSn = null, string expertType = null)
        {
            IEnumerable<VcUsrInfo> listScUsrTask = null;


            if ((string.IsNullOrEmpty(bizMngSn) && string.IsNullOrEmpty(expertType)) ||
                ((bizMngSn == "0") && string.IsNullOrEmpty(expertType)))
            {
                listScUsrTask = await scUsrRespository.GetManyAsync(usr => usr.UsrType == "P");
                return listScUsrTask.OrderByDescending(usr => usr.RegDt).ToList();
            }
            //if ((bizMngSn != "0") && string.IsNullOrEmpty(expertType))
            //{
            //    listScUsrTask =
            //        await
            //            scUsrRespository.GetManyAsync(
            //                usr =>
            //                    usr.UsrType == "P" && usr.VcCompInfoes.CompSn == int.Parse(bizMngSn));

            //    return listScUsrTask.OrderByDescending(usr => usr.RegDt).ToList();
            //}
            //if ((bizMngSn == "0") && !string.IsNullOrEmpty(expertType))
            //{
            //    listScUsrTask =
            //        await
            //            scUsrRespository.GetManyAsync(
            //                usr =>  usr.UsrType == "P" );
            //    return listScUsrTask.OrderByDescending(usr => usr.RegDt).ToList();
            //}
            //listScUsrTask =
            //    await
            //        scUsrRespository.GetManyAsync(
            //            usr =>
            //                usr.UsrType == "P" && usr.VcCompInfo.CompSn == int.Parse(bizMngSn));

            return listScUsrTask.OrderByDescending(usr => usr.RegDt).ToList();
        }

        //public async Task<IList<ScUsr>> GetMentorListAsync(int mngCompSn)
        //{
        //    IEnumerable<ScMentorMappiing> listScUsrTask = null;

        //    listScUsrTask = await scMentorMappingRepository.GetManyAsync(mmp => mmp.Status == "N" && mmp.ScUsr.UsrType == "M" && mmp.ScCompInfo.CompSn == mngCompSn);
        //    return listScUsrTask.Select(mmp => mmp.ScUsr).OrderByDescending(usr => usr.RegDt).ToList();
        //}

        public int UpdatePassword(SHUSER_SyUser syUser)
        {
            var rstSyUser = syUserRespository.UpdatePassword(syUser);
            return rstSyUser;
        }

        public int UserPasswordReset(VcUsrInfo scUsr)
        {
            var rstScUser = scUsrRespository.UserPasswordReset(scUsr);
            return rstScUser;
        }


        //public async Task<VcUsrInfo> getScUsrByCompSn(int compSn)
        //{
        //    var obj = await scUsrRespository.getScUsrByCompSn(sc => sc.CompSn == compSn);
        //    return obj;
        //}

        public async Task<VcBaInfo> getBaInfoById (string loginId)
        {
            var baInfo = await scUsrRespository.getBaInfo(loginId);
            return baInfo;
        }

        public async Task<VcMentorInfo> getMentorInfoById(string mentorId)
        {
            return await vcMentorInfoRepository.getMentorInfoById(mentorId);
           
        }

        

        public async Task<VcMentorInfo> getMentorInfoBySn(string mentorSn)
        {
            return await vcMentorInfoRepository.getMentorInfoBySn(mentorSn);

        }

        public async void insertUsrforIf(VcUsrInfo scUsr)
        {
            scUsrRespository.Add(scUsr);
            await SaveDbContextAsync();
        }

        public async Task<VcMentorInfo> getMentorInfo(string loginid)
        {
            return await vcMentorInfoRepository.getMentorInfoById(loginid);
        }

        public async Task<int> AddMentorInfo(VcMentorInfo vcMentorInfo)
        {
            var addmentor = vcMentorInfoRepository.Insert(vcMentorInfo);

            if (addmentor == null)
            {
                return -1;
            }
            return await SaveDbContextAsync();
        }

        public async Task<int> insertTcmsInfo(VcTcmsInfo vcTcmsInfo)
        {
            var result = await vcTcmsInfoRepository.inertToAsync(vcTcmsInfo);

            return await SaveDbContextAsync();
        }

        public async Task<int> insertNumMngInfo(VcNumMngInfo vcNumMngInfo)
        {
            var result = await vcNumMngInfoRepository.inertToAsync(vcNumMngInfo);
            return await SaveDbContextAsync();
        }

        public async Task<VcTcmsInfo> getTcmsInfo(int tcmsLoginKey)
        {
            var result = await vcTcmsInfoRepository.getTcmsInfo(tcmsLoginKey);
            return result;
        }

        public async Task<VcNumMngInfo> getNumInfoAsync(string numSn)
        {
            var result = await vcNumMngInfoRepository.getNumInfoAsync(numSn);
            return result;
        }

        public void insertVcUsrInfo(VcUsrInfo vcUsrInfo)
        {
            scUsrRespository.Add(vcUsrInfo);
            SaveDbContext();
        }

        public void updateNumInfo(VcNumMngInfo vcNumMngInfo)
        {
            vcNumMngInfoRepository.Update(vcNumMngInfo);
        }

        // TCMS_LOGIN_KEY로 유저 정보 조회
        public async Task<VcUsrInfo> getUsrInfoByTcmsKey(int tcmsLoginKey)
        {
            return await scUsrRespository.getUsrInfoByTcmsKey(tcmsLoginKey);
        }

        // baSn으로 mentorInfo 조회
        public async Task<IList<VcMentorInfo>> baGetMappingMentor(int baSn)
        {
            return await vcMentorInfoRepository.baGetMappingMentor(baSn);
        }

        public void SaveDbContext()
        {
            unitOfWork.Commit();
        }

        public async Task<int> SaveDbContextAsync()
        {
            return await unitOfWork.CommitAsync();
        }

        public async Task<int> SaveDareDbContextAsync()
        {
            return await dareUnitOfWork.CommitAsync();
        }

        public void SaveDareDbContext()
        {
            dareUnitOfWork.Commit();
        }

        public void insertMentorInfo(VcMentorInfo vcMentorInfo)
        {
            vcMentorInfoRepository.Add(vcMentorInfo);
            SaveDbContext();
        }

        public async Task<VcNumMngInfo> getNumInfoOneAsync()
        {
            return await vcNumMngInfoRepository.getNumInfoOneAsync();
        }

        public async Task<VcUsrInfo> getScUsrByLoginId(string loginId)
        {
            return await scUsrRespository.getUsrInfo(loginId);
        }
    }
}