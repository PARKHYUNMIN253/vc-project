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
using System.Data.SqlClient;
using BizOneShot.Light.Util.Helper;
using System.IO;

namespace BizOneShot.Light.Web.Areas.SysManager.Controllers
{
    [UserAuthorize(Order = 1)]
    [MenuAuthorize(Roles = UserType.SysManager, Order = 2)]
    public class ReportController : BaseController
    {
        private readonly IScBizWorkService scBizWorkService;
        private readonly IScCompMappingService scCompMappingService;
        private readonly IRptMasterService rptMasterService;
        private readonly IScUsrService scUsrService;
        private readonly IScCavService scCavService;
        private readonly IScMakService scMakService;
        private readonly ITcmsCompStatusSelectViewService tcmsCompStatusSelectViewService;
        private readonly IVcCompInfoService vcCompInfoService;
        private readonly IVcMentorMappingService vcMentorMappingService;
        private readonly IScMentoringTotalReportService _scMentoringTotalReportService;
        private readonly IScUsrService vcUsrInfoService;
        private readonly IVcLastReportNSatService _VcLastReportNSatService;
        private readonly IScMentoringReportService _scMentoringReportService;
        private readonly ITcmsMentoringReportSelectViewService tcmsMentoringReportSelectViewService;

        public ReportController(IScBizWorkService scBizWorkService
            , IRptMasterService rptMasterService
            , IScUsrService scUsrService
            , IScCompMappingService scCompMappingService
            , IScCavService scCavService
            , IScMakService scMakService
            , ITcmsCompStatusSelectViewService tcmsCompStatusSelectViewService
            , IVcCompInfoService vcCompInfoService
            , IScUsrService vcUsrInfoService
            , IScMentoringTotalReportService _scMentoringTotalReportService
            , IVcLastReportNSatService vcLastReportNSatService
            , IVcMentorMappingService vcMentorMappingService
            , ITcmsMentoringReportSelectViewService tcmsMentoringReportSelectViewService
            , IScMentoringReportService scMentoringReportService
            )
        {
            this.scBizWorkService = scBizWorkService;
            this.rptMasterService = rptMasterService;
            this.scUsrService = scUsrService;
            this.scCompMappingService = scCompMappingService;
            this.scCavService = scCavService;
            this.scMakService = scMakService;
            this.tcmsCompStatusSelectViewService = tcmsCompStatusSelectViewService;
            this.vcCompInfoService = vcCompInfoService;
            this.vcUsrInfoService = vcUsrInfoService;
            this._scMentoringTotalReportService = _scMentoringTotalReportService;
            this._VcLastReportNSatService = vcLastReportNSatService;
            this.vcMentorMappingService = vcMentorMappingService;
            this.tcmsMentoringReportSelectViewService = tcmsMentoringReportSelectViewService;
            this._scMentoringReportService = scMentoringReportService;
        }

        // GET: SysManager/Report
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult RegCompanyAverage()
        {
            //RegCompanyAverageViewModel rcav = new RegCompanyAverageViewModel();
            ViewBag.LeftMenu = Global.Report;
            return View();
        }


        public async Task<ActionResult> BasicSurveyReport(BasicSurveyReportViewModel paramModel, string curPage)
        {
            ViewBag.naviLeftMenu = Global.Report;

            int pagingSize = int.Parse(ConfigurationManager.AppSettings["PagingSize"]);
            int pageSize = int.Parse(ConfigurationManager.AppSettings["PagingSize"]);

            var listPageView = await tcmsCompStatusSelectViewService.getTcmsByWriteBa("Y");

            var selectCompViews = Mapper.Map<List<BasicSurveyReportViewModel>>(listPageView);
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
                        selectCompViews[i].MentorNm = mentorname.Name; //mentorname 저장
                    }

                }

            }
            var searchBy = new List<SelectListItem>(){
                new SelectListItem { Value = "0", Text = "기업명", Selected = true },
                new SelectListItem { Value = "1", Text = "전문기관(BA)명" },
            };
            ViewBag.SelectList = searchBy;
            ViewBag.ListPageView = listPageView;

            return View(new StaticPagedList<BasicSurveyReportViewModel>(selectCompViews.ToPagedList(1, pageSize), 1, pagingSize, selectCompViews.Count));

        }

        [HttpPost]
        public async Task<ActionResult> BasicSurveyReport(string SelectList, string Query, string curPage)
        {
            ViewBag.naviLeftMenu = Global.Report;

            var searchBy = new List<SelectListItem>(){
                new SelectListItem { Value = "0", Text = "기업명", Selected = true },
                new SelectListItem { Value = "1", Text = "전문기관(BA)명" },
            };
            ViewBag.SelectList = searchBy;

            int pagingSize = int.Parse(ConfigurationManager.AppSettings["PagingSize"]);
            int pageSize = int.Parse(ConfigurationManager.AppSettings["PagingSize"]);

            var pagedListView = await tcmsCompStatusSelectViewService.GetRptListViewsAsync(SelectList, Query);
            var resultViews =
               Mapper.Map<List<BasicSurveyReportViewModel>>(pagedListView);
            string[] nCheckArray = new string[pagedListView.Count];
            //var testview = await vcMentorMappingService.getVcMentorMappingByCompSn(listPageView[i].CompSn);
            if (pagedListView.Count > 0)
            {
                for (int i = 0; i < nCheckArray.Length; i++)
                {
                    var obj = await vcMentorMappingService.GetCompSnForTcms(Convert.ToString(pagedListView[i].CompSn), Convert.ToString(pagedListView[i].BaSn), pagedListView[i].NumSn, pagedListView[i].SubNumSn, pagedListView[i].ConCode);
                    if (obj == null)
                    {
                        nCheckArray[i] = null;
                    }
                    else
                    {
                        resultViews[i].MentorSn = Convert.ToString(obj.MentorSn); //mentorsn 저장
                        var mentorid = await vcUsrInfoService.getMentorInfoBySn(resultViews[i].MentorSn);
                        var mentorname = await vcUsrInfoService.selectScUsrByTcms(Convert.ToString(mentorid.TcmsLoginKey));
                        resultViews[i].MentorNm = mentorname.Name; //mentorname 저장
                    }

                }

            }

            return View(new StaticPagedList<BasicSurveyReportViewModel>(resultViews.ToPagedList(int.Parse(curPage), pagingSize), int.Parse(curPage), pagingSize, resultViews.Count));
        }

        public async Task<ActionResult> FinanceReport(string curPage)
        {
            ViewBag.LeftMenu = Global.Report;
            var bizMngList = await scUsrService.GetBizManagerAsync();
            var bizMngDropDown =
                Mapper.Map<List<BizMngDropDownModel>>(bizMngList);

            BizMngDropDownModel title = new BizMngDropDownModel();
            title.CompSn = 0;
            title.CompNm = "사업관리기관 선택";
            bizMngDropDown.Insert(0, title);

            SelectList bizList = new SelectList(bizMngDropDown, "CompSn", "CompNm");


            //사업관리기관 DownDown List Data
            ViewBag.SelectBizWorkMngrList = bizList;
            ViewBag.SelectBizWorkList = ReportHelper.MakeBizWorkList(null);
            return View();
        }
        //미사용
        //[HttpPost]
        //public async Task<ActionResult> FinanceReport(BasicSurveyReportViewModel paramModel, string curPage)
        //{
        //    ViewBag.LeftMenu = Global.Report;
        //    //사업관리기관 DownDown List Data
        //    var bizMngList = await scUsrService.GetBizManagerAsync();
        //    var bizMngDropDown =
        //        Mapper.Map<List<BizMngDropDownModel>>(bizMngList);

        //    BizMngDropDownModel title = new BizMngDropDownModel();
        //    title.CompSn = 0;
        //    title.CompNm = "사업관리기관 선택";
        //    bizMngDropDown.Insert(0, title);

        //    SelectList bizList = new SelectList(bizMngDropDown, "CompSn", "CompNm");
        //    ViewBag.SelectBizWorkMngrList = bizList;

        //    //사업 DropDown List Data
        //    if (paramModel.BizWorkMngr == 0)
        //        ViewBag.SelectBizWorkList = ReportHelper.MakeBizWorkList(null);
        //    else
        //    {
        //        var listScBizWork = await scBizWorkService.GetBizWorkList(paramModel.BizWorkMngr, null, 0);
        //        ViewBag.SelectBizWorkList = ReportHelper.MakeBizWorkList(listScBizWork);
        //    }


        //    //사업별 기업 조회
        //    int pagingSize = int.Parse(ConfigurationManager.AppSettings["PagingSize"]);

        //    var scCompMappings = await scCompMappingService.GetPagedListCompMappingsAsync(int.Parse(curPage ?? "1"), pagingSize, paramModel.BizWorkMngr, null, paramModel.BizWorkSn);

        //    //뷰모델 맵핑
        //    var rptMasterListView = Mapper.Map<List<BasicSurveyReportViewModel>>(scCompMappings.ToList());

        //    return View(new StaticPagedList<BasicSurveyReportViewModel>(rptMasterListView, int.Parse(curPage ?? "1"), pagingSize, scCompMappings.TotalItemCount));

        //}

        //public async Task<ActionResult> BasicSurveyCover(BasicSurveyReportViewModel paramModel)
        //{
        //    ViewBag.LeftMenu = Global.Report;

        //    if (paramModel.CompSn == 0 || paramModel.BizWorkSn == 0)
        //    {
        //        return View(paramModel);
        //    }

        //    var scCompMapping = await scCompMappingService.GetCompMappingAsync(paramModel.BizWorkSn, paramModel.CompSn);


        //    paramModel.CompNm = scCompMapping.ScCompInfo.CompNm;

        //    paramModel.BizWorkNm = scCompMapping.ScBizWork.BizWorkNm;
        //    paramModel.BizWorkYear = scCompMapping.ScCompInfo.RptMasters.Max(rm => rm.BasicYear);

        //    var rptMaster = scCompMapping.ScCompInfo.RptMasters.Where(rm => rm.BasicYear == paramModel.BizWorkYear && rm.BizWorkSn == paramModel.BizWorkSn && rm.CompSn == paramModel.CompSn).SingleOrDefault();

        //    paramModel.QuestionSn = rptMaster.QuestionSn;
        //    paramModel.Status = rptMaster.Status;
        //    paramModel.RegistrationNo = scCompMapping.ScCompInfo.RegistrationNo;

        //    var rptMaters = scCompMapping.ScCompInfo.RptMasters.Where(rm => rm.BizWorkSn == paramModel.BizWorkSn && rm.Status == "C").ToList();

        //    ViewBag.SelectReportYearList = ReportHelper.MakeBasicSurveyYear(rptMaters, paramModel.BizWorkYear);

        //    return View(paramModel);

        //}

        //[HttpPost]
        //public async Task<ActionResult> BasicSurveyCover(BasicSurveyReportViewModel paramModel, int curPage = 0)
        //{
        //    ViewBag.LeftMenu = Global.Report;

        //    if (paramModel.CompSn == 0 || paramModel.BizWorkSn == 0)
        //    {
        //        return View(paramModel);
        //    }

        //    var scCompMapping = await scCompMappingService.GetCompMappingAsync(paramModel.BizWorkSn, paramModel.CompSn);

        //    paramModel.CompNm = scCompMapping.ScCompInfo.CompNm;
        //    paramModel.BizWorkNm = scCompMapping.ScBizWork.BizWorkNm;

        //    var rptMaster = scCompMapping.ScCompInfo.RptMasters.Where(rm => rm.BasicYear == paramModel.BizWorkYear && rm.BizWorkSn == paramModel.BizWorkSn && rm.CompSn == paramModel.CompSn).SingleOrDefault();

        //    paramModel.QuestionSn = rptMaster.QuestionSn;
        //    paramModel.Status = rptMaster.Status;

        //    var rptMaters = scCompMapping.ScCompInfo.RptMasters.Where(rm => rm.BizWorkSn == paramModel.BizWorkSn && rm.Status == "C").ToList();
        //    ViewBag.SelectReportYearList = ReportHelper.MakeBasicSurveyYear(rptMaters, paramModel.BizWorkYear);

        //    return View(paramModel);

        //}

        public async Task<ActionResult> CompanyAverage()
        {
            ViewBag.LeftMenu = Global.Report;
            return View();
        }

        public async Task<JsonResult> CompanyAverageJson(int year) //중소기업평균 값 DB 에서 가져오는 부분
        {
            ViewBag.LeftMenu = Global.Report;

            //var scv = Mapper.Map<ScCav>(cavSn.CavDt);

            var scv = await scCavService.GetCavListAsync(year);

            var ob = Mapper.Map<List<RegCompanyAverageViewModel>>(scv);

            ViewBag.cavSelectList = ReportHelper.cavSelectList(scv);

            var bizList = ReportHelper.cavSelectList(scv);

            return Json(ob, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public async Task<ActionResult> RegCompanyAverage(RegCompanyAverageViewModel rcav) //중소기업평균 값 입력하는 부분
        {
            ViewBag.LeftMenu = Global.Report;

            var scv = Mapper.Map<ScCav>(rcav);

            scv.RegDt = DateTime.Now;
            scv.RegId = Session[Global.LoginID].ToString();

            Decimal result = await scCavService.AddCavAsync(scv);

            if (result != -1)
                return RedirectToAction("CompanyAverage", "Report");

            ModelState.AddModelError("", "중소기업평균 등록 실패.");
            return View(scv);

        }

        public async Task<ActionResult> MarketState()
        {
            ViewBag.LeftMenu = Global.Report;
            return View();

        }

        public async Task<ActionResult> RegMarketState()
        {
            ViewBag.LeftMenu = Global.Report;
            return View();

        }

        [HttpPost]
        public async Task<ActionResult> ModifyCompanyAverage(RegCompanyAverageViewModel rcav)//중소기업 평균 수정
        {
            ViewBag.LeftMenu = Global.Report;

            var usrView = Mapper.Map<ScCav>(rcav);

            usrView.CavDt = rcav.CavDt;
            usrView.CavOp = rcav.CavOp;

            scCavService.ModifyScCav(usrView);
            int result = await scCavService.SaveDbContextAsync();

            if (result != -1)
                return RedirectToAction("CompanyAverage", "Report");

            ModelState.AddModelError("", "중소기업평균 수정 실패");

            return View(usrView);


        }


        public async Task<ActionResult> ModifyCompanyAverage()
        {
            ViewBag.LeftMenu = Global.Report;

            return View();


        }
        public async Task<ActionResult> ModifyMarketState()
        {
            ViewBag.LeftMenu = Global.Report;

            return View();


        }
        [HttpPost]
        public async Task<ActionResult> RegMarketState(RegMarketStateViewModel rmak) //상품화 역량 시장현황 등록 부분
        {
            ViewBag.LeftMenu = Global.Report;

            var mak = Mapper.Map<ScMak>(rmak);

            mak.RegDt = DateTime.Now;
            mak.RegId = Session[Global.LoginID].ToString();

            int result = await scMakService.AddMakAsync(mak);

            if (result != -1)
                return RedirectToAction("MarketState", "Report");

            ModelState.AddModelError("", "상품화시장현황 등록 실패.");
            return View(rmak);
        }
        [HttpPost]
        public async Task<JsonResult> MarketStateJson(int year) //상품화 역량 시장현황 값 DB 에서 가져오는 부분
        {
            ViewBag.LeftMenu = Global.Report;

            //var scv = Mapper.Map<ScCav>(cavSn.CavDt);

            var mak = await scMakService.GetMakListAsync(year);

            var ob = Mapper.Map<List<RegMarketStateViewModel>>(mak);

            ViewBag.makSelectList = ReportHelper.makSelectList(mak);

            var bizList = ReportHelper.makSelectList(mak);

            return Json(ob, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public async Task<ActionResult> ModifyMarketState(RegMarketStateViewModel rmak)//상품화 역량 수정
        {
            ViewBag.LeftMenu = Global.Report;

            var usrView = Mapper.Map<ScMak>(rmak);

            usrView.MakDt = rmak.MakDt;
            usrView.MaksCa = rmak.MaksCa;

            scMakService.ModifyScMak(usrView);
            int result = await scMakService.SaveDbContextAsync();

            if (result != -1)
                return RedirectToAction("MarketState", "Report");

            ModelState.AddModelError("", "중소기업평균 수정 실패");

            return View(usrView);
        }

        public async Task<ActionResult> ReportCover(BasicSurveyReportViewModel paramModel)
        {
            ViewBag.naviLeftMenu = Global.Report;

            ViewBag.paramModel = paramModel;

            if (paramModel.CompSn == 0 || paramModel.NumSn == "0")
            {
                return View(paramModel);
            }

            var vcCompInfoObj = await vcCompInfoService.getVcCompInfoByCompSn(paramModel.CompSn);

            paramModel.CompNm = vcCompInfoObj.CompNm;
            paramModel.BizWorkNm = "";
            //var scCompMapping = await scCompMappingService.GetCompMappingAsync(paramModel.NumSn, paramModel.CompSn);

            //paramModel.CompNm = scCompMapping.ScCompInfo.CompNm;
            //paramModel.BizWorkNm = scCompMapping.ScBizWork.BizWorkNm;
            ViewBag.CompNm = paramModel.CompNm;
            ViewBag.paramModel = paramModel;
            ViewBag.NumSn = paramModel.NumSn;
            ViewBag.CompSn = paramModel.CompSn;
            ViewBag.BizWorkYear = paramModel.BizWorkYear;
            ViewBag.Status = paramModel.Status;
            ViewBag.QuestionSn = paramModel.QuestionSn;
            //ViewBag.NullCheck = paramModel.NullCheck;

            return View(paramModel);

        }

        public async Task<ActionResult> DeepenReport(TcmsCompStatusSelectViewModel paramModel, string curPage)
        {
            ViewBag.naviLeftMenu = Global.Report;

            int pagingSize = int.Parse(ConfigurationManager.AppSettings["PagingSize"]);
            int pageSize = int.Parse(ConfigurationManager.AppSettings["PagingSize"]);

            var listPageView = await tcmsCompStatusSelectViewService.getTcmsByCompStatus("B");

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
                        var lastreport = await _VcLastReportNSatService.getSatSn(Convert.ToString(selectCompViews[i].CompSn), selectCompViews[i].NumSn, selectCompViews[i].SubNumSn, selectCompViews[i].MentorSn, selectCompViews[i].ConCode);
                        if (lastreport == null)
                        {
                            continue;
                        }
                        else
                        {
                            selectCompViews[i].SatSn = lastreport.SatSn;
                            selectCompViews[i].SatisfactionGrade = lastreport.SatisfactionGrade ?? default(int);
                            selectCompViews[i].TotalReportSn = lastreport.TotalReportSn ?? default(int);
                        }
                    }

                }

            }
            var searchBy = new List<SelectListItem>(){
                new SelectListItem { Value = "0", Text = "기업명", Selected = true },
                new SelectListItem { Value = "1", Text = "전문기관(BA)명" },

            };

            ViewBag.DeepenList = paramModel;
            ViewBag.SelectList = searchBy;

            return View(new StaticPagedList<TcmsCompStatusSelectViewModel>(selectCompViews.ToPagedList(1, pageSize), 1, pagingSize, selectCompViews.Count));

        }

        [HttpPost]
        public async Task<ActionResult> DeepenReport(string SelectList, string Query, string curPage)
        {
            ViewBag.naviLeftMenu = Global.Report;

            var searchBy = new List<SelectListItem>(){
                new SelectListItem { Value = "0", Text = "기업명", Selected = true },
                new SelectListItem { Value = "1", Text = "전문기관(BA)명" },
            };
            ViewBag.SelectList = searchBy;

            int pagingSize = int.Parse(ConfigurationManager.AppSettings["PagingSize"]);
            int pageSize = int.Parse(ConfigurationManager.AppSettings["PagingSize"]);

            var pagedListView = await tcmsCompStatusSelectViewService.GetListViewsAsync(SelectList, Query);
            //var obj = pagedListView.Where(x => x.SatSn == x.SatSn).Distinct().ToList(); // 기업 중복제거
            var resultViews =
               Mapper.Map<List<TcmsCompStatusSelectViewModel>>(pagedListView);
            string[] nCheckArray = new string[pagedListView.Count];
            //var testview = await vcMentorMappingService.getVcMentorMappingByCompSn(listPageView[i].CompSn);
            if (pagedListView.Count > 0)
            {
                for (int i = 0; i < nCheckArray.Length; i++)
                {
                    var obj = await vcMentorMappingService.GetCompSnForTcms(Convert.ToString(pagedListView[i].CompSn), Convert.ToString(pagedListView[i].BaSn), pagedListView[i].NumSn, pagedListView[i].SubNumSn, pagedListView[i].ConCode);
                    if (obj == null)
                    {
                        nCheckArray[i] = null;

                    }
                    else
                    {
                        resultViews[i].MentorSn = Convert.ToString(obj.MentorSn); //mentorsn 저장
                        var mentorid = await vcUsrInfoService.getMentorInfoBySn(resultViews[i].MentorSn);
                        var mentorname = await vcUsrInfoService.selectScUsrByTcms(Convert.ToString(mentorid.TcmsLoginKey));
                        resultViews[i].Name = mentorname.Name; //mentorname 저장
                        var lastreport = await _VcLastReportNSatService.getSatSn(Convert.ToString(resultViews[i].CompSn), resultViews[i].NumSn, resultViews[i].SubNumSn, resultViews[i].MentorSn, resultViews[i].ConCode);
                        if (lastreport == null)
                        {
                            continue;
                        }
                        else
                        {
                            resultViews[i].SatSn = lastreport.SatSn;
                            resultViews[i].SatisfactionGrade = lastreport.SatisfactionGrade ?? default(int);
                            resultViews[i].TotalReportSn = lastreport.TotalReportSn ?? default(int);
                        }
                    }

                }

            }
            return View(new StaticPagedList<TcmsCompStatusSelectViewModel>(resultViews.ToPagedList(int.Parse(curPage), pagingSize), int.Parse(curPage), pagingSize, resultViews.Count));
        }

        public async Task<ActionResult> DeepenReportDetail(int totalReportSn, SelectedMentorTotalReportParmModel selectParam)
        {
            ViewBag.naviLeftMenu = Global.Report;

            var scMentoringTotalReport = await _scMentoringTotalReportService.GetMentoringTotalReportById(totalReportSn);

            var listscFileInfo = scMentoringTotalReport.ScMentoringTrFileInfoes.Select(mtfi => mtfi.ScFileInfo).Where(fi => fi.Status == "N");

            var listFileContent =
               Mapper.Map<List<FileContent>>(listscFileInfo);

            var totalReportViewModel =
               Mapper.Map<MentoringTotalReportViewModel>(scMentoringTotalReport);

            totalReportViewModel.FileContents = listFileContent;

            // compSn을 통해 기업명 가져오기
            var compSn = totalReportViewModel.CompSn;
            var compInfo = await vcCompInfoService.getVcCompInfoByCompSn(compSn);

            ViewBag.CompNm = compInfo.CompNm;
            //totalReportViewModel.CompNm = compNm.ToString();

            //검색조건 유지를 위해
            ViewBag.SelectParam = selectParam;

            return View(totalReportViewModel);
        }
        #region 파일 다운로드
        public void DownloadReportFile()
        {
            string fileNm = Request.QueryString["FileNm"];
            string filePath = Request.QueryString["FilePath"];

            string archiveName = fileNm;

            var files = new List<FileContent>();

            var file = new FileContent
            {
                FileNm = fileNm,
                FilePath = filePath
            };
            files.Add(file);

            new FileHelper().DownloadFile(files, archiveName);
        }
        #endregion

        public async Task<ActionResult> MentoringReportList()
        {
            ViewBag.naviLeftMenu = Global.Report;

            int pagingSize = int.Parse(ConfigurationManager.AppSettings["PagingSize"]);
            int pageSize = int.Parse(ConfigurationManager.AppSettings["PagingSize"]);

            var searchBy = new List<SelectListItem>(){
                new SelectListItem { Value = "0", Text = "기업명", Selected = true },
                new SelectListItem { Value = "1", Text = "전문기관(BA)명" },
                new SelectListItem { Value = "2", Text = "멘토명" }
            };


            var allMentoringReport = await tcmsMentoringReportSelectViewService.getMentoringReportInfoes();

            // 뷰모델 매핑
            var viewModel = Mapper.Map<IList<TcmsMentoringReportViewModel>>(allMentoringReport);
            
            ViewBag.SelectList = searchBy;
            return View(new StaticPagedList<TcmsMentoringReportViewModel>(viewModel.ToPagedList(1, pageSize), 1, pagingSize, viewModel.Count));
        }

        [HttpPost]
        public async Task<ActionResult> MentoringReportList(string SelectList, string Query, string curPage)
        {
            ViewBag.naviLeftMenu = Global.Report;

            int pagingSize = int.Parse(ConfigurationManager.AppSettings["PagingSize"]);
            int pageSize = int.Parse(ConfigurationManager.AppSettings["PagingSize"]);

            var searchBy = new List<SelectListItem>(){
                new SelectListItem { Value = "0", Text = "기업명", Selected = true },
                new SelectListItem { Value = "1", Text = "전문기관(BA)명" },
                new SelectListItem { Value = "2", Text = "멘토명" }
            };
            ViewBag.SelectList = searchBy;

            var allMentoringReport = await tcmsMentoringReportSelectViewService.GetListViewsAsync(SelectList, Query);
            
            // 뷰모델 매핑
            var viewModel = Mapper.Map<IList<TcmsMentoringReportViewModel>>(allMentoringReport);

            return View(new StaticPagedList<TcmsMentoringReportViewModel>(viewModel.ToPagedList(int.Parse(curPage), pagingSize), int.Parse(curPage), pagingSize, viewModel.Count));
        }

        public async Task<ActionResult> MentoringReportDetail(int reportSn, SelectedMentorReportParmModel selectParam, string searchType, string CompName)
        {
            ViewBag.naviLeftMenu = Global.Report;

            var scMentoringReport = await _scMentoringReportService.GetMentoringReportById(reportSn);

            //멘토링 사진
            var listscMentoringImageInfo = scMentoringReport.ScMentoringFileInfoes.Where(mtfi => mtfi.Classify == "P").Select(mtfi => mtfi.ScFileInfo).Where(fi => fi.Status == "N");

            //사진추가
            var listMentoringPhotoView =
              Mapper.Map<List<FileContent>>(listscMentoringImageInfo);

            FileHelper fileHelper = new FileHelper();

            foreach (var mentoringPhoto in listMentoringPhotoView)
            {
                mentoringPhoto.FileBase64String = await fileHelper.GetPhotoString(mentoringPhoto.FilePath);
                // resize/
                mentoringPhoto.FileFullPath = await fileHelper.GetPhotoStringFullsize(mentoringPhoto.FilePath);
            }

            //첨부파일
            var listscFileInfo = scMentoringReport.ScMentoringFileInfoes.Where(mtfi => mtfi.Classify == "A").Select(mtfi => mtfi.ScFileInfo).Where(fi => fi.Status == "N");

            var listFileContentView =
               Mapper.Map<List<FileContent>>(listscFileInfo);

            //멘토링 상세 매핑
            var reportViewModel =
               Mapper.Map<MentoringReportViewModel>(scMentoringReport);

            //멘토링상세뷰에 파일정보 추가
            reportViewModel.FileContents = listFileContentView;
            reportViewModel.MentoringPhoto = listMentoringPhotoView;

            //기업명 가져오기
            var getCompNm = await tcmsMentoringReportSelectViewService.GetCompNmByReportSn(reportSn);
            reportViewModel.CompNm = getCompNm.CompNm;

            //검색조건 유지를 위해
            ViewBag.SelectParam = selectParam;
            ViewBag.SearchType = searchType;
            return View(reportViewModel);
        }
    }

}