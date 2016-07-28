using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BizOneShot.Light.Dao.Infrastructure;
using BizOneShot.Light.Models.WebModels;

namespace BizOneShot.Light.Dao.Repositories
{
    public interface IRptMentorCommentRepository : IRepository<RptMentorComment>
    {
        RptMentorComment Insert(RptMentorComment rptMentorComment);
        Task<IList<RptMentorComment>> GetRptMentorCommentsAsync(Expression<Func<RptMentorComment, bool>> where);
        Task<RptMentorComment> GetRptMentorCommentAsync(Expression<Func<RptMentorComment, bool>> where);
        Task<IList<RptMentorComment>> GetSelectRptMentorCommentsAsync(Expression<Func<RptMentorComment, bool>> where);
        Task<RptMentorComment> GetRptMentorCommentSelectAsync(Expression<Func<RptMentorComment, bool>> where);
        Task<RptMentorComment> getRptMentorCommentAsync(Expression<Func<RptMentorComment, bool>> where);
    }


    public class RptMentorCommentRepository : RepositoryBase<RptMentorComment>, IRptMentorCommentRepository
    {
        public RptMentorCommentRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public RptMentorComment Insert(RptMentorComment rptMentorComment)
        {
            return DbContext.RptMentorComments.Add(rptMentorComment);
        }

        public async Task<IList<RptMentorComment>> GetRptMentorCommentsAsync(Expression<Func<RptMentorComment, bool>> where)
        {
            return await DbContext.RptMentorComments.Include("RptCheckList").Where(where).ToListAsync();
        }

        public async Task<RptMentorComment> GetRptMentorCommentAsync(Expression<Func<RptMentorComment, bool>> where)
        {
            return await DbContext.RptMentorComments.Include("RptCheckList").Where(where).SingleOrDefaultAsync();
        }

        //다른 멘토 의견 가져오기
        public async Task<IList<RptMentorComment>> GetSelectRptMentorCommentsAsync(Expression<Func<RptMentorComment, bool>> where)
        {
            return await DbContext.RptMentorComments.Include("RptMaster").Include("VcBizWork").Where(where).ToListAsync();
        }

        public async Task<RptMentorComment> GetRptMentorCommentSelectAsync(Expression<Func<RptMentorComment, bool>> where)
        {
            return await DbContext.RptMentorComments.Include("RptCheckList").Where(where).SingleOrDefaultAsync();
        }

        public async Task<RptMentorComment> getRptMentorCommentAsync(Expression<Func<RptMentorComment, bool>> where)
        {
            return await DbContext.RptMentorComments.Where(where).SingleOrDefaultAsync();
        }
    }
}