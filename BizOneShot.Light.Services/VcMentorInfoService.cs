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
    public interface IVcMentorInfoSerivce : IBaseService
    {
        Task<VcMentorInfo> getVcMentorInfoByMentorSn(string mentorSn);
        Task<IList<VcMentorInfo>> getVcMentorInfoListByBaSn(int baSn);
    }

    public class VcMentorInfoService : IVcMentorInfoSerivce
    {
        private readonly IVcMentorInfoRepository _vcMentorInfoRepository;
        private readonly IUnitOfWork _unitOfWork;

        public VcMentorInfoService(IVcMentorInfoRepository _vcMentorInfoRepository, IUnitOfWork _unitOfWork)
        {
            this._vcMentorInfoRepository = _vcMentorInfoRepository;
            this._unitOfWork = _unitOfWork;
        }

        public async Task<VcMentorInfo> getVcMentorInfoByMentorSn(string mentorSn)
        {
            return await _vcMentorInfoRepository.getMentorInfoBySn(mentorSn);
        }

        public void SaveDbContext()
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveDbContextAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IList<VcMentorInfo>> getVcMentorInfoListByBaSn(int baSn)
        {
            return await _vcMentorInfoRepository.baGetMappingMentor(baSn);
        }
    }
}
