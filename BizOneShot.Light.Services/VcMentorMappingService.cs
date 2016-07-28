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
    public interface IVcMentorMappingService : IBaseService
    {
        Task<IList<VcMentorMapping>> getVcMentorMappingByCompSn(string compSn);
        Task<VcMentorMapping> getVcMentorMappingSingleByCompSn(string compSn, string numSn);
        Task<int> AddMentorMappingAsync(VcMentorMapping scUsr);
        Task<VcMentorMapping> getMentorMappingInfo(string mentorSn);
        Task<VcMentorMapping> GetCompSnForTcms(string compSn, string baSn, string numSn, string subSn, string code);
        Task<IList<VcMentorMapping>> getMentorMappingInfoList(string compSn, string mentorSn);

        // getConCode by compSn, baSn, mentorSn, numSn, subNumSn
        Task<VcMentorMapping> getConcodeInfo(int compSn, int baSn, int mentorSn, string numSn, string subNumSn);
        Task<IList<VcMentorMapping>> getCheckMapping(int compSn, int baSn, string numSn);

        //동기식 insert
        void insertMentorMapping(VcMentorMapping vcMentorMapping);

        // 매핑된 기업 & 멘토있는지 확인
        Task<IList<VcMentorMapping>> checkCompMentorMapping(int compSn, int baSn, string numSn, string subNumSn, string conCode);

        // 매핑 확인
        Task<IList<VcMentorMapping>> checkCompConCodeMapping(int compSn, string conCode);

    }

    public class VcMentorMappingService : IVcMentorMappingService
    {
        private readonly IVcMentorMappingRepository vcMentorMappingRepository;
        private readonly IUnitOfWork unitOfWork;

        // constructor...
        public VcMentorMappingService(IVcMentorMappingRepository vcMentorMappingRepository, IUnitOfWork unitOfWork)
        {
            this.vcMentorMappingRepository = vcMentorMappingRepository;
            this.unitOfWork = unitOfWork;
        }

        // methods...
        public async Task<IList<VcMentorMapping>> getVcMentorMappingByCompSn(string compSn)
        {
            var convertCompSn = int.Parse(compSn);

            return await vcMentorMappingRepository.getVcMentorMappingByCompSn(vc => vc.CompSn == convertCompSn);
        }

        public async Task<VcMentorMapping> getMentorMappingInfo(string mentorSn)
        {
            return await vcMentorMappingRepository.getMentorMappingInfo(mentorSn);
        }

        public async Task<int> AddMentorMappingAsync(VcMentorMapping scUsr)
        {
           var rstScUsr = vcMentorMappingRepository.Insert(scUsr);

            if (rstScUsr == null)
            {
                return -1;
            }
            return await SaveDbContextAsync();
        }

        public async Task<IList<VcMentorMapping>> getMentorMappingInfoList(string compSn, string mentorSn)
        {
            return await vcMentorMappingRepository.getMentorMappingInfoList(compSn, mentorSn);
        }

        public async Task<VcMentorMapping> getConcodeInfo(int compSn, int baSn, int mentorSn, string numSn, string subNumSn)
        {
            return await vcMentorMappingRepository.getConcodeInfo(compSn, baSn, mentorSn, numSn, subNumSn);
        }

        public async Task<IList<VcMentorMapping>> getCheckMapping(int compSn, int baSn, string numSn)
        {
            return await vcMentorMappingRepository.getCheckMapping(compSn, baSn, numSn);
        }

        public async Task<VcMentorMapping> GetCompSnForTcms(string compSn, string baSn, string numSn, string subSn, string code)
        {
            return await vcMentorMappingRepository.GetCompSnForTcms(compSn, baSn, numSn, subSn, code);
        }


        public async Task<VcMentorMapping> getVcMentorMappingSingleByCompSn(string compSn, string numSn)
        {
            int iCompSn = int.Parse(compSn);
            return await vcMentorMappingRepository.getVcMentorMappingSingleByCompSn(vmm => vmm.CompSn == iCompSn && vmm.NumSn == numSn && vmm.WriteYn == "Y");
        }

        public async Task<IList<VcMentorMapping>> checkCompMentorMapping(int compSn, int baSn, string numSn, string subNumSn, string conCode)
        {
            return await vcMentorMappingRepository.checkCompMentorMapping(compSn, baSn, numSn, subNumSn, conCode);
        }

        public async Task<IList<VcMentorMapping>> checkCompConCodeMapping(int compSn, string conCode)
        {
            return await vcMentorMappingRepository.checkCompConCodeMapping(compSn, conCode);
        }


        public void insertMentorMapping(VcMentorMapping vcMentorMapping)
        {
            vcMentorMappingRepository.Add(vcMentorMapping);
            SaveDbContext();
        }

        #region saveMethods...
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
