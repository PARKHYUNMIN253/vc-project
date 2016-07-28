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
    public interface IQuesWriterService : IBaseService
    {
        Task<int> insertQuesWriterAsync(QuesWriter quesWriter);
    }


    public class QuesWriterServcie : IQuesWriterService
    {
        private readonly IQuesWriterRepository _quesWriterRepository;
        private readonly IUnitOfWork _unitOfWork;

        public QuesWriterServcie(IQuesWriterRepository _quesWriterRepository, IUnitOfWork _unitOfWork)
        {
            this._quesWriterRepository = _quesWriterRepository;
            this._unitOfWork = _unitOfWork;
        }

        public async Task<int> insertQuesWriterAsync(QuesWriter quesWriter)
        {
            var result = await _quesWriterRepository.insertQuesWriterAsync(quesWriter);

            if (result == null)
            {
                return -1;
            }
            return await SaveDbContextAsync();
        }

        public void SaveDbContext()
        {
            _unitOfWork.Commit();
        }

        public async Task<int> SaveDbContextAsync()
        {
            return await _unitOfWork.CommitAsync();
        }
    }
}
