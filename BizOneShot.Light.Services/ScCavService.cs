using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BizOneShot.Light.Dao.Infrastructure;
using BizOneShot.Light.Dao.Repositories;
using BizOneShot.Light.Models.WebModels;
using PagedList;

namespace BizOneShot.Light.Services
{
    public interface IScCavService : IBaseService
    {
        Task<Decimal> AddCavAsync(ScCav scCav);
        //Task<ScCav> GetCavinfoAsync(int year);
        //    Task<IList<ScCav>> GetCavListAsync();
        IList<ScCav> GetCavList(int year);
        void ModifyScCav(ScCav scCav);
        ScCav GetCavAsync(int year);
        Task<IList<ScCav>> GetCavListAsync(int year);
        Task<IList<ScCav>> GetCavListLastYear(int maxYear);
        Task<IPagedList<ScCav>> GetPagedListCAVAsync(int page, int pageSize, int CavDt);
        Task<int> getMaxYear();

    }

    public class ScCavService : IScCavService
    {
        private readonly IScCavRepository scCavRepository;
        private readonly IUnitOfWork unitOfWork;

        public ScCavService(IScCavRepository scCavRespository, IUnitOfWork unitOfWork)
        {
            this.scCavRepository = scCavRespository;
            this.unitOfWork = unitOfWork;
        }
        public async Task<Decimal> AddCavAsync(ScCav scCav)
        {
            var rstScCav = await scCavRepository.insert(scCav);

            if (rstScCav == null)
            {
                return -1;
            }
            return await SaveDbContextAsync();
        }

        //public async Task<ScCav> GetCavinfoAsync(int year)
        //{
        //    return await scCavRespository.GetCavinfoById(
        //        scr => scr.CavDt == year);
        //}

        public IList<ScCav> GetCavList(int year)
        {
            //year = 2014;
            var listScUsrTask = scCavRepository.GetcavlistById(year);
            return listScUsrTask;
        }

        public async Task<IList<ScCav>> GetCavListAsync(int year)
        {
            return await scCavRepository.GetCavListByIdAsync(year);
        }

        public async Task<IList<ScCav>> GetCavListLastYear(int maxYear)
        {
            return await scCavRepository.GetCavListLastYear(maxYear);
        }

        public ScCav GetCavAsync(int year)
        {
            var scCav = scCavRepository.GetCavById(year);
            return scCav;
        }

        public async Task<IPagedList<ScCav>> GetPagedListCAVAsync(int page, int pageSize, int CavDt)
        {
            return await scCavRepository.GetPagedListCAVAsync(page, pageSize, CavDt);
        }

        public async Task<int> getMaxYear()
        {
            return await scCavRepository.getMaxYear();
        }


        public void SaveDbContext()
        {
            unitOfWork.Commit();
        }

        public async Task<int> SaveDbContextAsync()
        {
            return await unitOfWork.CommitAsync();
        }

        public void ModifyScCav(ScCav scCav) 
        {
            scCavRepository.Update(scCav);
        }
    }
}
