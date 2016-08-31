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
    public interface IVcBaInfoService : IBaseService
    {
        Task<VcBaInfo> getVcBaInfoById(string loginId);
        Task<VcBaInfo> getVcBaInfoByKey(int TcmsKey);
        Task<int> AddBaInfo(VcBaInfo vcBaInfo);
        void UpdateBaInfo(VcBaInfo vcBaInfo);
        void insertVcBaInfo(VcBaInfo vcBaInfo);
        // loy 0627
        Task<IList<VcBaInfo>> getVcBaInfoByLoginKey(int tcmsLoginKey);
        Task<VcBaInfo> getVcBaInfoByBaSn(int baSn);
    }

    public class VcBaInfoService : IVcBaInfoService
    {
        private readonly IVcBaInfoRepository vcBaInfoRepository;
        private readonly IUnitOfWork unitOfWork;

        public VcBaInfoService(IVcBaInfoRepository vcBaInfoRepository, IUnitOfWork unitOfWork)
        {
            this.vcBaInfoRepository = vcBaInfoRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<VcBaInfo> getVcBaInfoById(string loginId)
        {
            var id = int.Parse(loginId);
            return await vcBaInfoRepository.getVcBaInfoById(vb => vb.TcmsLoginKey == id);
        }

        public async Task<IList<VcBaInfo>> getVcBaInfoByLoginKey(int tcmsLoginKey)
        {
            return await vcBaInfoRepository.getVcBaInfoByLoginKey(vb => vb.TcmsLoginKey == tcmsLoginKey);
        }

        public void SaveDbContext()
        {
            unitOfWork.Commit();
        }

        public async Task<int> SaveDbContextAsync()
        {
            return await unitOfWork.CommitAsync();
        }

        public async Task<int> AddBaInfo(VcBaInfo vcBaInfo)
        {
            var addba = vcBaInfoRepository.insertToAsync(vcBaInfo);
            return await SaveDbContextAsync();
        }

        public async Task<VcBaInfo> getVcBaInfoByKey(int TcmsKey)
        {
            return await vcBaInfoRepository.getVcBaInfoById(vb => vb.TcmsLoginKey == TcmsKey);
        }

        public async Task<VcBaInfo> getVcBaInfoByBaSn(int baSn)
        {
            return await vcBaInfoRepository.getVcBaInfoByBaSn(vb => vb.BaSn == baSn);
        }

        public void UpdateBaInfo(VcBaInfo vcBaInfo)
        {
            vcBaInfoRepository.Update(vcBaInfo);
        }

        public void insertVcBaInfo(VcBaInfo vcBaInfo)
        {
            vcBaInfoRepository.Add(vcBaInfo);
            SaveDbContext();
        }

        
    }
}
