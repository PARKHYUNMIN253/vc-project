using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BizOneShot.Light.Dao.Infrastructure;
using BizOneShot.Light.Dao.Repositories;
using BizOneShot.Light.Models.ViewModels;
using BizOneShot.Light.Models.WebModels;
using PagedList;

namespace BizOneShot.Light.Services
{
    public interface IVcSatCheckService : IBaseService
    {
        Task<int> AddCheckAasync(VcSatCheck vsc);
        void ModifySat(VcSatCheck VcSatCheck);
        Task<VcSatCheck> getVcSatCheckBySatSN(int satSn);
        void Insert(VcSatCheck vsc);
    }

    public class VcSatCheckService : IVcSatCheckService
    {
        private readonly IVcSatCheckRepository VcSatCheckRepository;
        private readonly IUnitOfWork unitOfWork;

        public VcSatCheckService(IVcSatCheckRepository VcSatCheckRepository, IUnitOfWork unitOfWork)
        {
            this.VcSatCheckRepository = VcSatCheckRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<int> AddCheckAasync(VcSatCheck vsc)
        {
            var maskSat = VcSatCheckRepository.insert(vsc);
            if (maskSat == null)
            {
                return -1;
            }
            return await SaveDbContextAsync();
        }


        public void ModifySat(VcSatCheck VcSatCheck)
        {
            VcSatCheckRepository.Update(VcSatCheck);
        }

        public async Task<VcSatCheck> getVcSatCheckBySatSN(int satSn)
        {
            return await VcSatCheckRepository.getVcSatCheckBySatSN(satSn);
        }

        public void Insert(VcSatCheck obj)
        {
            VcSatCheckRepository.insert(obj);
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