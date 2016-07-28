using BizOneShot.Light.Dao.Infrastructure;
using BizOneShot.Light.Dao.Repositories;
using BizOneShot.Light.Models.WebModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using PagedList;

namespace BizOneShot.Light.Services
{
    public interface ITcmsCompStatusSelectViewService : IBaseService
    {
        Task<IList<TcmsCompStatusSelectView>> getTcmsByCompStatus(string usrType);
        Task<IList<TcmsCompStatusSelectView>> getTcmsByWriteBa(string writeyn);
        Task<IList<TcmsCompStatusSelectView>> getTcmsByBaStatus(string usrType);
        Task<IList<TcmsCompStatusSelectView>> getTcmsByCompNm(string CompNm);
        Task<IPagedList<TcmsCompStatusSelectView>> GetPagedListViewsAsync(int page, int pageSize, string searchType = null, string keyword = null);
        Task<IList<TcmsCompStatusSelectView>> GetListViewsAsync(string searchType = null, string keyword = null);
        Task<IPagedList<TcmsCompStatusSelectView>> GetpageListbyComp(string usrType, int page, int pageSize);
        Task<IPagedList<TcmsCompStatusSelectView>> GetpageListbyBa(string usrType, int page, int pageSize);
        Task<IList<TcmsCompStatusSelectView>> GetRptListViewsAsync(string searchType = null, string keyword = null);
    }


    public class TcmsCompStatusSelectViewService : ITcmsCompStatusSelectViewService
    {
        private readonly ITcmsCompStatusSelectViewRepository tcmsCompStatusSelectViewRepository;
        private readonly IUnitOfWork unitOfWork;

        public TcmsCompStatusSelectViewService(ITcmsCompStatusSelectViewRepository tcmsCompStatusSelectViewRepository, IUnitOfWork unitOfWork)
        {
            this.tcmsCompStatusSelectViewRepository = tcmsCompStatusSelectViewRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<IList<TcmsCompStatusSelectView>> getTcmsByCompNm(string CompNm)
        {
            return await tcmsCompStatusSelectViewRepository.getTcmsByCompStatus(cm => cm.CompNm == CompNm);
        }

        public async Task<IPagedList<TcmsCompStatusSelectView>> GetPagedListViewsAsync(int page, int pageSize, string searchType = null,
            string keyword = null)
        {
            if (string.IsNullOrEmpty(searchType) || string.IsNullOrEmpty(keyword))
            {
                return await tcmsCompStatusSelectViewRepository.GetPagedListCompForTcms("B", page, pageSize);
            }
            if (searchType.Equals("0")) // keyword가 포함된 기업명 검색 
            {
                return
                    await
                        tcmsCompStatusSelectViewRepository.GetPagedListAsync(
                            cm => cm.CompNm.Contains(keyword), page,
                            pageSize);
            }
            if (searchType.Equals("1")) // keyword가 포함된 BA명 검색 
            {
                return
                    await
                        tcmsCompStatusSelectViewRepository.GetPagedListAsync(
                            bm => bm.BaNm.Contains(keyword), page,
                            pageSize);
            }

            return await tcmsCompStatusSelectViewRepository.GetPagedListCompForTcms("B", page, pageSize);
        }

        public async Task<IList<TcmsCompStatusSelectView>> GetListViewsAsync(string searchType = null, string keyword = null)
        {
            if (string.IsNullOrEmpty(searchType) || string.IsNullOrEmpty(keyword))
            {
                return await tcmsCompStatusSelectViewRepository.getTcmsByCompStatus(ut => ut.UsrType == "B");
            }
            if (searchType.Equals("0")) // keyword가 포함된 기업명 검색 
            {
                return
                    await
                        tcmsCompStatusSelectViewRepository.getTcmsByCompStatus(
                            cm => cm.CompNm.Contains(keyword));
            }
            if (searchType.Equals("1")) // keyword가 포함된 BA명 검색 
            {
                return
                    await
                        tcmsCompStatusSelectViewRepository.getTcmsByCompStatus(
                            bm => bm.BaNm.Contains(keyword));
            }

            return await tcmsCompStatusSelectViewRepository.getTcmsByCompStatus(ut => ut.UsrType == "B");
        }
        public async Task<IList<TcmsCompStatusSelectView>> GetRptListViewsAsync(string searchType = null, string keyword = null)
        {
            if (string.IsNullOrEmpty(searchType) || string.IsNullOrEmpty(keyword))
            {
                return await tcmsCompStatusSelectViewRepository.getTcmsByCompStatus(ut => ut.UsrType == "B" && ut.WriteYn == "Y");
            }
            if (searchType.Equals("0")) // keyword가 포함된 기업명 검색 
            {
                return
                    await
                        tcmsCompStatusSelectViewRepository.getTcmsByCompStatus(
                            cm => cm.CompNm.Contains(keyword) && cm.WriteYn == "Y");
            }
            if (searchType.Equals("1")) // keyword가 포함된 BA명 검색 
            {
                return
                    await
                        tcmsCompStatusSelectViewRepository.getTcmsByCompStatus(
                            bm => bm.BaNm.Contains(keyword) && bm.WriteYn == "Y");
            }

            return await tcmsCompStatusSelectViewRepository.getTcmsByCompStatus(ut => ut.UsrType == "B" && ut.WriteYn == "Y");
        }
        public async Task<IList<TcmsCompStatusSelectView>> getTcmsByCompStatus(string usrType)
        {
            return await tcmsCompStatusSelectViewRepository.getTcmsByCompStatus(vb => vb.UsrType == usrType);
        }
        public async Task<IList<TcmsCompStatusSelectView>> getTcmsByWriteBa(string writeyn)
        {
            return await tcmsCompStatusSelectViewRepository.getTcmsByCompStatus(vb => vb.WriteYn == writeyn);
        }
        public async Task<IList<TcmsCompStatusSelectView>> getTcmsByBaStatus(string usrType)
        {
            return await tcmsCompStatusSelectViewRepository.getTcmsByCompStatus(vb => vb.UsrType == usrType);
        }

        public async Task<IPagedList<TcmsCompStatusSelectView>> GetpageListbyComp(string usrType, int page, int pageSize)
        {
            return await tcmsCompStatusSelectViewRepository.GetPagedListCompForTcms(usrType, page, pageSize);
        }
        public async Task<IPagedList<TcmsCompStatusSelectView>> GetpageListbyBa(string usrType, int page, int pageSize)
        {
            return await tcmsCompStatusSelectViewRepository.GetPagedListBaForTcms(usrType, page, pageSize);
        }
        public void SaveDbContext()
        {
            unitOfWork.Commit();
        }

        public async Task<int> SaveDbContextAsync()
        {
            return await unitOfWork.CommitAsync();
        }





        //public async Task<IPagedList<TcmsCompStatusSelectView>> GetpageListbyCompSn(string compSn, int page, int pageSize)
        //{
        //    return await tcmsCompStatusSelectViewRepository.GetPagedListCompSnForTcms(compSn, page, pageSize);
        //}

    }
}
