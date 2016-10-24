using System;
using System.Threading.Tasks;
using BizOneShot.Light.Dao.Infrastructure;
using BizOneShot.Light.Dao.Repositories;
using BizOneShot.Light.Models.WebModels;
using System.Collections.Generic;

namespace BizOneShot.Light.Services
{
    public interface IScFileInfoService : IBaseService
    {
        void ModifyFileInfo(ScFileInfo scFileInfo);
        Task<ScFileInfo> getFileInfoByFileSn(int fileSn);
        ScFileInfo getFileInfoByFileSnNA(int fileSn);

        Task<IList<ScFileInfo>> getFileInfoByFileSnList(int fileSn);
    }


    public class ScFileInfoService : IScFileInfoService
    {
        private readonly IScFileInfoRepository scFileInfoRepository;
        private readonly IUnitOfWork unitOfWork;

        public ScFileInfoService(IScFileInfoRepository scFileInfoRepository, IUnitOfWork unitOfWork)
        {
            this.scFileInfoRepository = scFileInfoRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<ScFileInfo> getFileInfoByFileSn(int fileSn)
        {
            return await scFileInfoRepository.getFileInfoByFileSn(fileSn);
        }

        public ScFileInfo getFileInfoByFileSnNA(int fileSn)
        {
            return scFileInfoRepository.getFileInfoByFileSnNA(fileSn);
        }

        public void ModifyFileInfo(ScFileInfo scFileInfo)
        {
            scFileInfoRepository.Update(scFileInfo);
        }
        public async Task<IList<ScFileInfo>> getFileInfoByFileSnList(int fileSn)
        {
            return await scFileInfoRepository.getFileInfoByFileSnList(fileSn);
        }


        #region SaveDbContext

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