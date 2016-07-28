using BizOneShot.Light.Dao.Infrastructure;
using BizOneShot.Light.Models.WebModels;
using PagedList;
using PagedList.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BizOneShot.Light.Dao.Repositories
{
    public interface ITcmsCompStatusSelectViewRepository : IRepository<TcmsCompStatusSelectView>
    {
        Task<IList<TcmsCompStatusSelectView>> getTcmsByCompStatus(Expression<Func<TcmsCompStatusSelectView, bool>> where);
        Task<IPagedList<TcmsCompStatusSelectView>> GetPagedListAsync(Expression<Func<TcmsCompStatusSelectView, bool>> where, int page, int pageSize);
        Task<IPagedList<TcmsCompStatusSelectView>> GetPagedListCompForTcms(string usrType, int page, int pageSize);
        //Task<IList<TcmsCompStatusSelectView>> GetListCompForTcms(string usrType);
        Task<IPagedList<TcmsCompStatusSelectView>> GetPagedListBaForTcms(string usrType, int page, int pageSize);
        //Task<IPagedList<TcmsCompStatusSelectView>> GetPagedListCompSnForTcms(string compSn, int page, int pageSize);
    }
    public class TcmsCompStatusSelectViewRepository : RepositoryBase<TcmsCompStatusSelectView>, ITcmsCompStatusSelectViewRepository
    {
        public TcmsCompStatusSelectViewRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public async Task<IList<TcmsCompStatusSelectView>> getTcmsByCompStatus(Expression<Func<TcmsCompStatusSelectView, bool>> where)
        {
            return await DbContext.TcmsCompStatusSelectViews.Where(where).ToListAsync();
        }
        public async Task<IPagedList<TcmsCompStatusSelectView>> GetPagedListAsync(Expression<Func<TcmsCompStatusSelectView, bool>> where, int page,
            int pageSize)
        {
            return
                await
                    DbContext.TcmsCompStatusSelectViews/*.Include("ScQcl")*/
                        .Where(where)
                        //.Where(ut => ut.MappingCompSn != null)
                        //.Where(ut => ut.MappingMentorSn != null)
                        .OrderByDescending(sf => sf.UsrType)
                        .ToPagedListAsync(page, pageSize);
            ;
        }

        public async Task<IPagedList<TcmsCompStatusSelectView>> GetPagedListCompForTcms(string usrType, int page, int pageSize)
        {
            return await DbContext.TcmsCompStatusSelectViews.Where(ut => ut.UsrType == usrType)
                //.Where(ut => ut.MappingCompSn != null)
                //.Where(ut => ut.MappingMentorSn != null)
                .OrderByDescending(ut => ut.CompSn)
                .ToPagedListAsync(page, pageSize);
        }
        public async Task<IPagedList<TcmsCompStatusSelectView>> GetPagedListBaForTcms(string usrType, int page, int pageSize)
        {
            return await DbContext.TcmsCompStatusSelectViews.Where(ut => ut.UsrType == usrType)
                //.Where(ut => ut.MappingCompSn != null)
                //.Where(ut => ut.MappingMentorSn != null)
                .OrderByDescending(ut => ut.BaSn)
                .ToPagedListAsync(page, pageSize);
        }



        //public async Task<IPagedList<TcmsCompStatusSelectView>> GetPagedListCompSnForTcms(string compSn, int page, int pageSize)
        //{
        //    return await DbContext.TcmsCompStatusSelectViews.Where(ut => ut.CompSn == Int32.Parse(compSn)).OrderByDescending(ut => ut.UsrType).ToPagedListAsync(page, pageSize);
        //}


    }
}
