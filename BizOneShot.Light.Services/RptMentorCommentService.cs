using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BizOneShot.Light.Dao.Infrastructure;
using BizOneShot.Light.Dao.Repositories;
using BizOneShot.Light.Models.WebModels;

namespace BizOneShot.Light.Services
{
    public interface IRptMentorCommentService : IBaseService
    {
        RptMentorComment Insert(RptMentorComment rptMentorComment);
        Task<int> AddRptMentorCommentAsync(RptMentorComment rptMentorComment);

        Task<IList<RptMentorComment>> GetRptMentorCommentListAsync(int qustionSn, string numSn, int basicYear, string pageCode);

        Task<RptMentorComment> GetRptMentorCommentAsync(int qustionSn, int bizWorkSn, int? basicYear, string detailCode);
        //Task<IList<RptMentorComment>> GetSelectRptMentorCommentListAsync( int bizWorkSn, int basicYear, string comment);
        Task<RptMentorComment> getRptMentorCommentAsync(int questionSn, string numSn, int? basicYear, string detailCode);


        //Task<IList<RptMentorComment>> GetRptMentorCommentSelectAsync( string detailCode, int compSn, int bizWorkSn);
    }


    public class RptMentorCommentService : IRptMentorCommentService
    {
        private readonly IRptMentorCommentRepository rptMentorCommentRepository;
        private readonly IUnitOfWork unitOfWork;

        public RptMentorCommentService(IRptMentorCommentRepository rptMentorCommentRepository, IUnitOfWork unitOfWork)
        {
            this.rptMentorCommentRepository = rptMentorCommentRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<IList<RptMentorComment>> GetRptMentorCommentListAsync(int qustionSn, string numSn,
            int basicYear, string pageCode)
        {
            var rptMentorComments =
                await
                    rptMentorCommentRepository.GetRptMentorCommentsAsync(
                        rm =>
                            rm.QuestionSn == qustionSn && rm.NumSn == numSn && rm.BasicYear == basicYear &&
                            rm.RptCheckList.SmallClassCd == pageCode);

            return rptMentorComments.OrderBy(rm => rm.DetailCd).ToList();
        }

        public async Task<RptMentorComment> GetRptMentorCommentAsync(int qustionSn, int bizWorkSn, int? basicYear,
            string detailCode)
        {
            var rptMentorComment =
                await
                    rptMentorCommentRepository.GetRptMentorCommentAsync(
                        rm =>
                            rm.QuestionSn == qustionSn 
                            && int.Parse(rm.NumSn) == bizWorkSn 
                            && rm.BasicYear == basicYear 
                            && rm.DetailCd == detailCode);
            return rptMentorComment;
        }

        public RptMentorComment Insert(RptMentorComment rptMentorComment)
        {
            return rptMentorCommentRepository.Insert(rptMentorComment);
        }


        public async Task<int> AddRptMentorCommentAsync(RptMentorComment rptMentorComment)
        {
            var result = rptMentorCommentRepository.Insert(rptMentorComment);

            if (result == null)
            {
                return -1;
            }
            return await SaveDbContextAsync();
        }

        public void SaveDbContext()
        {
            unitOfWork.Commit();
        }

        public async Task<int> SaveDbContextAsync()
        {
            return await unitOfWork.CommitAsync();
        }

        //다른 멘토의견 가져오기
        //public async Task<IList<RptMentorComment>> GetRptMentorCommentSelectAsync( string detailCode, int compSn, int bizWorkSn)
        //{
        //    var rptMentorComment = 
        //        await
        //            rptMentorCommentRepository.GetSelectRptMentorCommentsAsync(rm => rm.DetailCd == detailCode && rm.RptMaster.CompSn == compSn && rm.VcBizWork.BizWorkSn == bizWorkSn );
            
        //    return rptMentorComment;
        //}

        public async Task<RptMentorComment> getRptMentorCommentAsync(int questionSn, string numSn, int? basicYear, string detailCode)
        {
            return await rptMentorCommentRepository.getRptMentorCommentAsync(rm => rm.QuestionSn == questionSn && rm.NumSn == numSn && rm.BasicYear == basicYear && rm.DetailCd == detailCode);
        }
    }
}