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
    public interface IScMakService : IBaseService
    {
        Task<int> AddMakAsync(ScMak scMak);
        ScMak GetMakAsync(int year);
        Task<IList<ScMak>> GetMakListAsync(int year);
        IList<ScMak> GetMakList(int year);
        Task<int> getMaxYear();
        void ModifyScMak(ScMak scMak);
    }
    public class ScMakService : IScMakService
    {
        private readonly IScMakRepository scMakRepository;
        private readonly IUnitOfWork unitOfWork;

        public ScMakService(IScMakRepository scMakRepository, IUnitOfWork unitOfWork)
        {
            this.scMakRepository = scMakRepository;
            this.unitOfWork = unitOfWork;
        }
        public async Task<int> AddMakAsync(ScMak scMak)
        {
            var makScMak = await scMakRepository.insert(scMak);

            if (makScMak == null)
            {
                return -1;
            }
            return await SaveDbContextAsync();
        }

        public ScMak GetMakAsync(int year)
        {
            var scMak = scMakRepository.GetMakById(year);
            return scMak;
        }

        public IList<ScMak> GetMakList(int year)
        {
            var listScUsrTask = scMakRepository.GetMaklistById(year);
            return listScUsrTask;
        }

        public async Task<IList<ScMak>> GetMakListAsync(int year)
        {
            return await scMakRepository.GetMaklistByIdAsync(year);
        }

        public async Task<int> getMaxYear()
        {
            return await scMakRepository.getMaxYear();
        }

        public void ModifyScMak(ScMak scMak)
        {
            scMakRepository.Update(scMak);
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
