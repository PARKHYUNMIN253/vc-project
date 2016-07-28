using BizOneShot.Light.Dao.Infrastructure;
using BizOneShot.Light.Dao.Repositories;
using BizOneShot.Light.Models.WebModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace BizOneShot.Light.Services
{
    public interface IQuesMasterService : IBaseService
    {
        Task<IList<QuesMaster>> GetQuesMastersAsync(string registrationNo);
        QuesMaster GetQuesMaster(int questionSn);
        Task<QuesMaster> GetQuesMasterAsync(int questionSn);
        Task<QuesMaster> GetQuesMasterAsync(string registrationNo, int basicYear);
        Task<QuesMaster> GetQuesMasterAsyncPro(string registrationNo, int basicYear);
        Task<QuesMaster> AddQuesMasterAsync(QuesMaster quesMaster);
        Task<QuesMaster> GetQuesCompInfoAsync(int questionSn);
        Task<QuesMaster> GetQuesCompExtentionAsync(int questionSn);
        Task<QuesMaster> GetQuesResult1Async(int questionSn);
        Task<QuesMaster> GetQuesResult2Async(int questionSn);
        Task<QuesMaster> GetQuesOgranAnalysisAsync(int questionSn);
        Task<QuesMaster> GetQuesOgranAnalysisAsync(string registrationNo, int basicYear);
        

        // add loy
        Task<IList<QuesMaster>> GetQuesMasterAsyncList(int questionSn);
        Task<int> InsertQuest(QuesMaster quesMaster);
        void insertQuescomp(QuesMaster quesMaster);

        void insertQuesWriters(QuesWriter quesWriter);

        Task<int> insertQuesWriter(QuesWriter quesWriter);
        Task<QuesWriter> getQuesWriter(int questionSn);
    }

    public class QuesMasterService : IQuesMasterService
    {
        private readonly IQuesMasterRepository _quesMasterRepository;
        private readonly IUnitOfWork unitOfWork;

        // 보고서 작성자 조회 추가
        private readonly IQuesWriterRepository _quesWriterRepository;

        public QuesMasterService(IQuesMasterRepository _quesMasterRepository, IUnitOfWork unitOfWork, IQuesWriterRepository _quesWriterRepository)
        {
            this._quesMasterRepository = _quesMasterRepository;
            this.unitOfWork = unitOfWork;
            this._quesWriterRepository = _quesWriterRepository;
        }

        public QuesMaster GetQuesMaster(int questionSn)
        {
            return _quesMasterRepository.GetQuesMaster(
                        qm => qm.QuestionSn == questionSn && qm.Status != "D"
                   );
        }

        public async Task<IList<QuesMaster>> GetQuesMastersAsync(string registrationNo)
        {
            var listQuesMaster =
                await
                    _quesMasterRepository.GetQuesMastersAsync(
                        qm => qm.RegistrationNo == registrationNo && qm.Status != "D");
            return listQuesMaster.OrderByDescending(qm => qm.BasicYear).ToList();
        }

        public async Task<QuesMaster> GetQuesMasterAsync(int questionSn)
        {
            var quesMaster =
                await _quesMasterRepository.GetQuesMasterAsync(qm => qm.QuestionSn == questionSn && qm.Status != "D");
            return quesMaster;
        }

        public async Task<QuesMaster> GetQuesMasterAsync(string registrationNo, int basicYear)
        {
            var quesMaster =
                await
                    _quesMasterRepository.GetQuesMasterAsync(
                        qm => qm.RegistrationNo == registrationNo && qm.BasicYear == basicYear && qm.Status == "C");
            return quesMaster;
        }

        // add Loy
        public async Task<QuesMaster> GetQuesMasterAsyncPro(string registrationNo, int basicYear)
        {
            var quesMaster =
                await
                _quesMasterRepository.GetQuesMasterAsync(
                    qm => qm.RegistrationNo == registrationNo && qm.BasicYear == basicYear && qm.Status == "P");
            return quesMaster;
        }

        public async Task<QuesMaster> GetQuesCompInfoAsync(int questionSn)
        {
            var quesMaster =
                await _quesMasterRepository.GetQuesCompInfoAsync(qm => qm.QuestionSn == questionSn && qm.Status != "D");
            return quesMaster;
        }

        public async Task<QuesMaster> GetQuesCompExtentionAsync(int questionSn)
        {
            var quesMaster =
                await
                    _quesMasterRepository.GetQuesCompExtentionAsync(
                        qm => qm.QuestionSn == questionSn && qm.Status != "D");
            return quesMaster;
        }

        public async Task<QuesMaster> GetQuesResult1Async(int questionSn)
        {
            var quesMaster =
                await _quesMasterRepository.GetQuesResult1Async(qm => qm.QuestionSn == questionSn && qm.Status != "D");
            return quesMaster;
        }

        public async Task<QuesMaster> GetQuesResult2Async(int questionSn)
        {
            var quesMaster =
                await _quesMasterRepository.GetQuesResult2Async(qm => qm.QuestionSn == questionSn && qm.Status != "D");
            return quesMaster;
        }

        public async Task<QuesMaster> GetQuesOgranAnalysisAsync(int questionSn)
        {
            var quesMaster =
                await
                    _quesMasterRepository.GetQuesOgranAnalysisAsync(
                        qm => qm.QuestionSn == questionSn && qm.Status != "D");
            return quesMaster;
        }

        public async Task<QuesMaster> GetQuesOgranAnalysisAsync(string registrationNo, int basicYear)
        {
            var quesMaster =
                await
                    _quesMasterRepository.GetQuesOgranAnalysisAsync(
                        qm => qm.RegistrationNo == registrationNo && qm.BasicYear == basicYear && qm.Status == "C");
            return quesMaster;
        }

        public async Task<QuesMaster> AddQuesMasterAsync(QuesMaster quesMaster)
        {
            //var rstScUsr = scUsrRespository.Insert(scUsr);
            //scCompInfo.
            var rstQuesMaster = _quesMasterRepository.Insert(quesMaster);

            if (rstQuesMaster != null)
            {
                await SaveDbContextAsync();
            }

            return rstQuesMaster;
        }

        public async Task<IList<QuesMaster>> GetQuesMasterAsyncList(int questionSn)
        {
            return await _quesMasterRepository.GetQuesMasterAsyncList(questionSn);
        }


        public async Task<int> InsertQuest(QuesMaster quesMaster)
        {
            var rstQuesMaster = _quesMasterRepository.insertQues(quesMaster);

            return await SaveDbContextAsync();
        }

        public async Task<int> insertQuesWriter(QuesWriter quesWriter)
        {
            var result = _quesMasterRepository.InsertWriter(quesWriter);
            return await SaveDbContextAsync();
        }

        public async Task<QuesWriter> getQuesWriter(int questionSn)
        {
            return await _quesWriterRepository.getQuesWriter(questionSn);
        }

        #region SaveContext

        public void SaveDbContext()
        {
            unitOfWork.Commit();
        }

        public async Task<int> SaveDbContextAsync()
        {
            return await unitOfWork.CommitAsync();
        }

        public void insertQuescomp(QuesMaster quesMaster)
        {
            _quesMasterRepository.Add(quesMaster);
            SaveDbContext();
        }

        public void insertQuesWriters(QuesWriter quesWriter)
        {
            _quesWriterRepository.Add(quesWriter);
            SaveDbContext();
        }

        #endregion
    }
}