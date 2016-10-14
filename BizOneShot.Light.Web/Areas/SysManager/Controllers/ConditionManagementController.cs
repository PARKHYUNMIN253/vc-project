using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using BizOneShot.Light.Models.ViewModels;
using BizOneShot.Light.Services;
using BizOneShot.Light.Web.ComLib;
using PagedList;
using BizOneShot.Light.Models.WebModels;
using BizOneShot.Light.Dao.Repositories;
using System.Data.SqlClient;

namespace BizOneShot.Light.Web.Areas.SysManager.Controllers
{
    public class ConditionManagementController : BaseController
    {
        private readonly IVcCompInfoService vcCompInfoService;
        private readonly IVcMentorMappingService vcMentorMappingService;
        private readonly ICompBaMngService compBaMngService;
        private readonly ITcmsCompStatusSelectViewService tcmsCompStatusSelectViewService;
        private readonly IProcMngService procBaMentorMapping;
        private readonly IScUsrService vcUsrInfoService;
        private readonly IVcLastReportNSatService _VcLastReportNSatService;

        public ConditionManagementController(IVcCompInfoService vcCompInfoService,
            IVcMentorMappingService vcMentorMappingService,
            ICompBaMngService compBaMngService,
            ITcmsCompStatusSelectViewService tcmsCompStatusSelectViewService,
            IProcMngService procBaMentorMapping,
            IScUsrService vcUsrInfoService,
            IVcLastReportNSatService _VcLastReportNSatService
            )
        {
            this.vcCompInfoService = vcCompInfoService;
            this.vcMentorMappingService = vcMentorMappingService;
            this.compBaMngService = compBaMngService;
            this.tcmsCompStatusSelectViewService = tcmsCompStatusSelectViewService;
            this.procBaMentorMapping = procBaMentorMapping;
            this.vcUsrInfoService = vcUsrInfoService;
            this._VcLastReportNSatService = _VcLastReportNSatService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> CompStateManage(TcmsCompStatusSelectViewModel paramModel, string curPage)
        {
            ViewBag.naviLeftMenu = Global.CompMng;

            int pagingSize = int.Parse(ConfigurationManager.AppSettings["PagingSize"]);
            int pageSize = int.Parse(ConfigurationManager.AppSettings["PagingSize"]);

            var listPageView = await tcmsCompStatusSelectViewService.getTcmsByCompStatus("B");

            var selectCompViews = Mapper.Map<List<TcmsCompStatusSelectViewModel>>(listPageView);
            string[] nCheckArray = new string[listPageView.Count];

            if (listPageView.Count > 0)
            {
                for (int i = 0; i < nCheckArray.Length; i++)
                {
                    var obj = await vcMentorMappingService.GetCompSnForTcms(Convert.ToString(listPageView[i].CompSn), Convert.ToString(listPageView[i].BaSn), listPageView[i].NumSn, listPageView[i].SubNumSn, listPageView[i].ConCode);
                    if (obj == null)
                    {
                        nCheckArray[i] = null;
                    }
                    else
                    {
                        selectCompViews[i].MentorSn = Convert.ToString(obj.MentorSn); //mentorsn 저장
                        var mentorid = await vcUsrInfoService.getMentorInfoBySn(selectCompViews[i].MentorSn);
                        var mentorname = await vcUsrInfoService.selectScUsrByTcms(Convert.ToString(mentorid.TcmsLoginKey));
                        selectCompViews[i].Name = mentorname.Name; //mentorname 저장
                        var lastreport = await _VcLastReportNSatService.getSatSn(Convert.ToString(selectCompViews[i].CompSn), selectCompViews[i].NumSn, selectCompViews[i].SubNumSn, selectCompViews[i].MentorSn, selectCompViews[i].ConCode);
                        if (lastreport == null)
                        {
                            continue;
                        }
                        else
                        {
                            selectCompViews[i].SatSn = lastreport.SatSn;
                            selectCompViews[i].SatisfactionGrade = lastreport.SatisfactionGrade ?? default(int);
                            selectCompViews[i].ConStatus = lastreport.ConStatus;
                        }
                    }

                }

            }

            ViewBag.nCheckArray = nCheckArray;
            var searchBy = new List<SelectListItem>(){
                new SelectListItem { Value = "0", Text = "기업명", Selected = true },
                new SelectListItem { Value = "1", Text = "전문기관(BA)명" },
            };
            ViewBag.SelectList = searchBy;

            return View(new StaticPagedList<TcmsCompStatusSelectViewModel>(selectCompViews.ToPagedList(1, pageSize), 1, pagingSize, selectCompViews.Count));
        }

        [HttpPost]
        public async Task<ActionResult> CompStateManage(string SelectList, string Query, string curPage)
        {
            ViewBag.naviLeftMenu = Global.CompMng;

            var searchBy = new List<SelectListItem>(){
                new SelectListItem { Value = "0", Text = "기업명", Selected = true },
                new SelectListItem { Value = "1", Text = "전문기관(BA)명" },
            };
            ViewBag.SelectList = searchBy;

            int pagingSize = int.Parse(ConfigurationManager.AppSettings["PagingSize"]);
            int pageSize = int.Parse(ConfigurationManager.AppSettings["PagingSize"]);

            var listPageView = await tcmsCompStatusSelectViewService.GetListViewsAsync(SelectList, Query);

            var selectCompViews =
               Mapper.Map<List<TcmsCompStatusSelectViewModel>>(listPageView);
            string[] nCheckArray = new string[listPageView.Count];

            if (listPageView.Count > 0)
            {
                for (int i = 0; i < nCheckArray.Length; i++)
                {
                    var obj = await vcMentorMappingService.GetCompSnForTcms(Convert.ToString(listPageView[i].CompSn), Convert.ToString(listPageView[i].BaSn), listPageView[i].NumSn, listPageView[i].SubNumSn, listPageView[i].ConCode);
                    if (obj == null)
                    {
                        nCheckArray[i] = null;
                    }
                    else
                    {
                        selectCompViews[i].MentorSn = Convert.ToString(obj.MentorSn); //mentorsn 저장
                        var mentorid = await vcUsrInfoService.getMentorInfoBySn(selectCompViews[i].MentorSn);
                        var mentorname = await vcUsrInfoService.selectScUsrByTcms(Convert.ToString(mentorid.TcmsLoginKey));
                        selectCompViews[i].Name = mentorname.Name; //mentorname 저장
                        var lastreport = await _VcLastReportNSatService.getSatSn(Convert.ToString(selectCompViews[i].CompSn), selectCompViews[i].NumSn, selectCompViews[i].SubNumSn, selectCompViews[i].MentorSn, selectCompViews[i].ConCode);
                        if (lastreport == null)
                        {
                            continue;
                        }
                        else
                        {
                            selectCompViews[i].SatSn = lastreport.SatSn;
                            selectCompViews[i].SatisfactionGrade = lastreport.SatisfactionGrade ?? default(int);
                            selectCompViews[i].ConStatus = lastreport.ConStatus;
                        }
                    }

                }

            }
            return View(new StaticPagedList<TcmsCompStatusSelectViewModel>(selectCompViews.ToPagedList(int.Parse(curPage), pagingSize), int.Parse(curPage), pagingSize, selectCompViews.Count));
        }

        public async Task<ActionResult> BaStateManage(TcmsCompStatusSelectView paramModel, string curPage)
        {
            ViewBag.naviLeftMenu = Global.CompMng;

            int pagingSize = int.Parse(ConfigurationManager.AppSettings["PagingSize"]);
            int pageSize = int.Parse(ConfigurationManager.AppSettings["PagingSize"]);

            var listPageView = await tcmsCompStatusSelectViewService.getTcmsByBaStatus("B");

            var selectCompViews = Mapper.Map<List<TcmsCompStatusSelectViewModel>>(listPageView);
            string[] nCheckArray = new string[listPageView.Count];
            //var testview = await vcMentorMappingService.getVcMentorMappingByCompSn(listPageView[i].CompSn);
            if (listPageView.Count > 0)
            {
                for (int i = 0; i < nCheckArray.Length; i++)
                {
                    var obj = await vcMentorMappingService.GetCompSnForTcms(Convert.ToString(listPageView[i].CompSn), Convert.ToString(listPageView[i].BaSn), listPageView[i].NumSn, listPageView[i].SubNumSn, listPageView[i].ConCode);
                    if (obj == null)
                    {
                        nCheckArray[i] = null;
                    }
                    else
                    {
                        selectCompViews[i].MentorSn = Convert.ToString(obj.MentorSn); //mentorsn 저장
                        var mentorid = await vcUsrInfoService.getMentorInfoBySn(selectCompViews[i].MentorSn);
                        var mentorname = await vcUsrInfoService.selectScUsrByTcms(Convert.ToString(mentorid.TcmsLoginKey));
                        selectCompViews[i].Name = mentorname.Name; //mentorname 저장
                    }

                }

            }
            var searchBy = new List<SelectListItem>(){
                new SelectListItem { Value = "1", Text = "전문기관(BA)명", Selected = true },
                new SelectListItem { Value = "0", Text = "기업명"},
            };
            ViewBag.SelectList = searchBy;

            return View(new StaticPagedList<TcmsCompStatusSelectViewModel>(selectCompViews.ToPagedList(1, pageSize), 1, pagingSize, selectCompViews.Count));
        }

        [HttpPost]
        public async Task<ActionResult> BaStateManage(string SelectList, string Query, string curPage)
        {
            ViewBag.naviLeftMenu = Global.CompMng;

            var searchBy = new List<SelectListItem>(){
                new SelectListItem { Value = "1", Text = "전문기관(BA)명", Selected = true },
                new SelectListItem { Value = "0", Text = "기업명"},
            };
            ViewBag.SelectList = searchBy;

            int pagingSize = int.Parse(ConfigurationManager.AppSettings["PagingSize"]);
            int pageSize = int.Parse(ConfigurationManager.AppSettings["PagingSize"]);

            var listPageView = await tcmsCompStatusSelectViewService.GetListViewsAsync(SelectList, Query);

            var selectCompViews =
               Mapper.Map<List<TcmsCompStatusSelectViewModel>>(listPageView);
            string[] nCheckArray = new string[listPageView.Count];

            if (listPageView.Count > 0)
            {
                for (int i = 0; i < nCheckArray.Length; i++)
                {
                    var obj = await vcMentorMappingService.GetCompSnForTcms(Convert.ToString(listPageView[i].CompSn), Convert.ToString(listPageView[i].BaSn), listPageView[i].NumSn, listPageView[i].SubNumSn, listPageView[i].ConCode);
                    if (obj == null)
                    {
                        nCheckArray[i] = null;
                    }
                    else
                    {
                        selectCompViews[i].MentorSn = Convert.ToString(obj.MentorSn); //mentorsn 저장
                        var mentorid = await vcUsrInfoService.getMentorInfoBySn(selectCompViews[i].MentorSn);
                        var mentorname = await vcUsrInfoService.selectScUsrByTcms(Convert.ToString(mentorid.TcmsLoginKey));
                        selectCompViews[i].Name = mentorname.Name; //mentorname 저장
                        var lastreport = await _VcLastReportNSatService.getSatSn(Convert.ToString(selectCompViews[i].CompSn), selectCompViews[i].NumSn, selectCompViews[i].SubNumSn, selectCompViews[i].MentorSn, selectCompViews[i].ConCode);
                        if (lastreport == null)
                        {
                            continue;
                        }
                        else
                        {
                            selectCompViews[i].SatSn = lastreport.SatSn;
                            selectCompViews[i].SatisfactionGrade = lastreport.SatisfactionGrade ?? default(int);
                        }
                    }

                }

            }
            return View(new StaticPagedList<TcmsCompStatusSelectViewModel>(selectCompViews.ToPagedList(int.Parse(curPage), pagingSize), int.Parse(curPage), pagingSize, selectCompViews.Count));
        }
    }
}