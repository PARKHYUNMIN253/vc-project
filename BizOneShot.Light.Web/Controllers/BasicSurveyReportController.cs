using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using BizOneShot.Light.Models.ViewModels;
using BizOneShot.Light.Models.WebModels;
using BizOneShot.Light.Services;
using BizOneShot.Light.Web.ComLib;
using PagedList;
using BizOneShot.Light.Models.DareModels;
using System.Data.SqlClient;

namespace BizOneShot.Light.Web.Controllers
{
    [UserAuthorize(Order = 1)]
    public class BasicSurveyReportController : BaseController
    {
        private readonly IQuesCompInfoService quesCompInfoService;
        private readonly IQuesResult1Service quesResult1Service;
        private readonly IQuesResult2Service quesResult2Service;
        private readonly IQuesMasterService quesMasterService;
        private readonly IScCompMappingService scCompMappingService;
        private readonly IScBizWorkService scBizWorkService;
        private readonly IScMentorMappingService scMentorMappingService;
        private readonly IVcCompInfoService vcCompInfoService;
        private readonly IRptMasterService rptMasterService;
        private readonly IRptMentorCommentService rptMentorCommentService;
        private readonly IRptCheckListService rptCheckListService;
        private readonly IRptMngCodeService rptMngCodeService;
        private readonly IRptMngCommentService rptMngCommentService;
        private readonly ISboFinancialIndexTService sboFinancialIndexTService;
        private readonly IScCavService scCavService;
        private readonly IScMakService scMakService;
        private readonly IScFinancialIndexTService scFinancialIndexTService;
        private readonly IScUsrService scUsrService;
        private readonly IScCompanyFinanceService scCompanyFinanceService;
        private readonly IProcMngService procMngService;

        public BasicSurveyReportController(
            IScCompMappingService scCompMappingService,
            IQuesCompInfoService quesCompInfoService,
            IScMentorMappingService scMentorMappingService,
            IRptCheckListService rptCheckListService,
            IRptMasterService rptMasterService,
            IRptMentorCommentService rptMentorCommentService,
            IQuesResult1Service quesResult1Service,
            IQuesResult2Service quesResult2Service,
            IQuesMasterService quesMasterService,
            IScBizWorkService scBizWorkService,
            IRptMngCodeService rptMngCodeService,
            IRptMngCommentService rptMngCommentService,
            ISboFinancialIndexTService sboFinancialIndexTService,
            IVcCompInfoService vcCompInfoService,
            IScCavService scCavService,
            IScMakService scMakService,
            IScFinancialIndexTService scFinancialIndexTService,
            IScUsrService scUsrService,
            IScCompanyFinanceService scCompanyFinanceService,
            IProcMngService procMngService
            )
        {
            this.scCompMappingService = scCompMappingService;
            this.quesCompInfoService = quesCompInfoService;
            this.scMentorMappingService = scMentorMappingService;
            this.rptCheckListService = rptCheckListService;
            this.rptMasterService = rptMasterService;
            this.rptMentorCommentService = rptMentorCommentService;
            this.quesResult1Service = quesResult1Service;
            this.quesMasterService = quesMasterService;
            this.quesResult2Service = quesResult2Service;
            this.scBizWorkService = scBizWorkService;
            this.rptMngCodeService = rptMngCodeService;
            this.rptMngCommentService = rptMngCommentService;
            this.sboFinancialIndexTService = sboFinancialIndexTService;
            this.vcCompInfoService = vcCompInfoService;
            this.scCavService = scCavService;
            this.scMakService = scMakService;
            this.scFinancialIndexTService = scFinancialIndexTService;
            this.scUsrService = scUsrService;
            this.scCompanyFinanceService = scCompanyFinanceService;
            this.procMngService = procMngService;
        }

        // GET: BasicSurveyReport
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult BasicSurveyReportPrint(BasicSurveyReportViewModel paramModel)
        {
            ViewBag.naviLeftMenu = Global.Report;
            return View(paramModel);
        }

        // 첫 페이지 추가 커버 부분
        //public async Task<ActionResult> LogoCover(BasicSurveyReportViewModel paramModel)
        //{
        //    ViewBag.naviLeftMenu = Global.Report;

        //    if (paramModel.CompSn == 0 || paramModel.NumSn == 0)
        //    {
        //        return View(paramModel);
        //    }

        //    var scCompMapping = await scCompMappingService.GetCompMappingAsync(paramModel.NumSn, paramModel.CompSn);

        //    paramModel.CompNm = scCompMapping.ScCompInfo.CompNm;
        //    paramModel.BizWorkNm = scCompMapping.ScBizWork.BizWorkNm;

        //    ViewBag.paramModel = paramModel;

        //    return View(paramModel);

        //}

        //public async Task<ActionResult> Cover(BasicSurveyReportViewModel paramModel)
        //{
        //    ViewBag.naviLeftMenu = Global.Report;
        //    ViewBag.paramModel = paramModel;

        //    if (paramModel.CompSn == 0 || paramModel.NumSn == 0)
        //    {
        //        return View(paramModel);
        //    }

        //    var scCompMapping = await scCompMappingService.GetCompMappingAsync(paramModel.NumSn, paramModel.CompSn);

        //    paramModel.CompNm = scCompMapping.ScCompInfo.CompNm;
        //    paramModel.BizWorkNm = scCompMapping.ScBizWork.BizWorkNm;

        //    ViewBag.NumSn = paramModel.NumSn;
        //    ViewBag.CompSn = paramModel.CompSn;
        //    ViewBag.BizWorkYear = paramModel.BizWorkYear;
        //    ViewBag.Status = paramModel.Status;
        //    ViewBag.QuestionSn = paramModel.QuestionSn;

        //    return View(paramModel);

        //}

        public async Task<ActionResult> CompanyInfo(BasicSurveyReportViewModel paramModel)
        {
            // P, T, C


            ViewBag.naviLeftMenu = Global.Report;
            ViewBag.paramModel = paramModel;

            string cacheId = this.ControllerContext.RouteData.Values["action"].ToString() + paramModel.QuestionSn.ToString() + "_";
            string methodCacheId = cacheId + "methodCache";

            QuesCompanyInfoViewModel viewModel = new QuesCompanyInfoViewModel();

            bool cacheStatus = false;

            Dictionary<string, object> methodCache = (Dictionary<string, object>)this.HttpContext.Cache[methodCacheId];

            if (methodCache != null && methodCache.Count > 0)
            {
                cacheStatus = true;
            }
            else
            {
                methodCache = new Dictionary<string, object>();
            }

            if (cacheStatus == true)
            {
                ViewBag.paramModel = methodCache["paramModel"];
                ViewBag.NumSn = ViewBag.paramModel.NumSn;
                ViewBag.CompSn = ViewBag.paramModel.CompSn;
                ViewBag.BizWorkYear = ViewBag.paramModel.BizWorkYear;
                ViewBag.Status = ViewBag.paramModel.Status;
                ViewBag.QuestionSn = ViewBag.paramModel.QuestionSn;
                return View(methodCache["viewModel"]);
            }



            //if (paramModel.Status == "T")
            //{
            //    var rptMaster = await rptMasterService.GetRptMasterAsync(paramModel.QuestionSn, paramModel.CompSn, paramModel.BizWorkYear);
            //    rptMaster.Status = "P";
            //    await rptMasterService.SaveDbContextAsync();
            //}

            // 0415 - 임시저장 수정 
            if (paramModel.Status == "T")
            {
                var rptMaster = await rptMasterService.GetRptMasterAsync(paramModel.QuestionSn, paramModel.CompSn, paramModel.BizWorkYear);
                rptMaster.Status = "P";
                paramModel.Status = "P";
                ViewBag.Status = paramModel.Status;
                await rptMasterService.SaveDbContextAsync();
            }

            var quesCompInfo = await quesCompInfoService.GetQuesCompInfoAsync(paramModel.QuestionSn);
            var quesCompInfoView = Mapper.Map<QuesCompanyInfoViewModel>(quesCompInfo);
            if (quesCompInfoView.PublishDt == "0001-01-01")
                quesCompInfoView.PublishDt = null;

            ViewBag.paramModel = paramModel;

            ViewBag.NumSn = paramModel.NumSn;
            ViewBag.CompSn = paramModel.CompSn;
            ViewBag.BizWorkYear = paramModel.BizWorkYear;
            ViewBag.Status = paramModel.Status;
            ViewBag.QuestionSn = paramModel.QuestionSn;

            methodCache["paramModel"] = paramModel;
            methodCache["viewModel"] = quesCompInfoView;
            //methodCache["scCompMapping"] = scCompMapping;
            //methodCache["CavYear"] = ViewBag.CavYear;
            //methodCache["PageNum"] = rptpagelist1[0].PageNum;
            this.HttpContext.Cache.Add(methodCacheId, methodCache, null, DateTime.Now.AddMinutes(180D),
                System.Web.Caching.Cache.NoSlidingExpiration,
                System.Web.Caching.CacheItemPriority.High, null);

            return View(quesCompInfoView);

        }

        public async Task<ActionResult> OverallSummaryCover(BasicSurveyReportViewModel paramModel)
        {
            ViewBag.naviLeftMenu = Global.Report;
            ViewBag.paramModel = paramModel;
            //보고서 상단 회사명 가져오기


            var vcCompInfoObj = await vcCompInfoService.getVcCompInfoByCompSn(paramModel.CompSn);

            paramModel.CompNm = vcCompInfoObj.CompNm;

            //var scCompMapping = await scCompMappingService.GetCompMappingAsync(paramModel.NumSn, paramModel.CompSn);
            //paramModel.CompNm = scCompMapping.ScCompInfo.CompNm;

            ViewBag.NumSn = paramModel.NumSn;
            ViewBag.CompSn = paramModel.CompSn;
            ViewBag.BizWorkYear = paramModel.BizWorkYear;
            ViewBag.Status = paramModel.Status;
            ViewBag.QuestionSn = paramModel.QuestionSn;

            return View(paramModel);
        }

        // * 그래프 나오는 부분
        public async Task<ActionResult> OverallSummary(BasicSurveyReportViewModel paramModel)
        {
            ViewBag.naviLeftMenu = Global.Report;
            ViewBag.paramModel = paramModel;
            ViewBag.CompNm = paramModel.CompNm;


            Dictionary<string, object> cachedViewBag;
            OverallSummaryViewModel cachedViewModel;
            bool cacheStatus = false;
            string cacheId = "OverallSummary_" + paramModel.QuestionSn.ToString() + "_";
            cachedViewBag = (Dictionary<string, object>)this.HttpContext.Cache[cacheId + "ViewBag"];
            cachedViewModel = (OverallSummaryViewModel)this.HttpContext.Cache[cacheId + "ViewModel"];

            if (cachedViewBag != null && cachedViewModel != null && cachedViewBag.Count > 0) // 있으면 flag를 true로
            {
                cacheStatus = true;
            }
            if (cachedViewBag == null) cachedViewBag = new Dictionary<string, object>();    // 없으면 객체 새로 할당


            if (cacheStatus == true) // 있어서 true면 cachedViewBag 값들을 해당 ViewBag에 넣고 cachedViewModel을 던져서 마무리
            {
                ViewBag.NumSn = cachedViewBag["NumSn"]; // NumSn으로 교체
                ViewBag.CompNm = cachedViewBag["CompNm"];
                ViewBag.CompSn = cachedViewBag["CompSn"];
                ViewBag.BizWorkYear = cachedViewBag["BizWorkYear"];
                ViewBag.Status = cachedViewBag["Status"];
                ViewBag.QuestionSn = cachedViewBag["QuestionSn"];
                ViewBag.NullCheck = cachedViewBag["NullCheck"];
                return View(cachedViewModel);
            }


            OverallSummaryViewModel viewModel = new OverallSummaryViewModel();
            viewModel.CommentList = new List<CommentViewModel>();

            ReportUtil reportUtil = new ReportUtil(
                scBizWorkService,
                quesResult1Service,
                quesResult2Service,
                quesMasterService,
                sboFinancialIndexTService,
                scCavService,
                scCompanyFinanceService,
                rptMasterService,
                scFinancialIndexTService,
                procMngService
            );

            // 해당기업 변수들
            double basicCapa = 0.0;  //해당기업 기초역량
            double mkt = 0.0;        //해당기업 기술경영 마케팅관리
            double hrMng = 0.0;      //해당기업 인적자원관리
            double financeMng = 0.0; //해당기업 재무관리
            double workProductivity = 0.0; //해당기업 1인당 노동생산성
            double salesEarning = 0.0;     //해당기업 매출영업이익률
            double current = 0.0;    //해당기업 유동비율

            SqlParameter numSn = new SqlParameter("NUM_SN", paramModel.NumSn);
            object[] parameters = new object[] { numSn };


            //1) 현재 사업에 참여한 업체 평균
            var curBizWork = await scBizWorkService.GetBizWorkByBizWorkSn(int.Parse(paramModel.NumSn)); // BizWorkSn을 기준으로 현재 있는 사업들 객체 가져오기

            var curBizWorkObj = await procMngService.getBizWorkInfoByBizWorkSn(parameters);

            // Dictionary 객체
            Dictionary<string, double> dicBizInHrMng = new Dictionary<string, double>();        //인적자원관리
            Dictionary<string, double> dicBizInFinanceMng = new Dictionary<string, double>();   //재무관리
            Dictionary<string, double> dicBizInMkt = new Dictionary<string, double>();          //기술경영마케팅
            Dictionary<string, double> dicBizInBasicCpas = new Dictionary<string, double>();    //기초역량
            Dictionary<string, decimal> dicSales = new Dictionary<string, decimal>();           //매출액
            Dictionary<string, decimal> dicValueadded = new Dictionary<string, decimal>();      //부가가치
            Dictionary<string, decimal> dicMaterrial = new Dictionary<string, decimal>();       //재료비
            Dictionary<string, decimal> dicQtEmp = new Dictionary<string, decimal>();           //종업원수
            Dictionary<string, decimal> dicOperatingErning = new Dictionary<string, decimal>(); //영업이익
            Dictionary<string, decimal> dicCurrentAsset = new Dictionary<string, decimal>();    //유동자산
            Dictionary<string, decimal> dicCurrentLiability = new Dictionary<string, decimal>();//유동부채

            {
                //var compMappings = curBizWork.ScCompMappings.Where(sc => sc.Status == "A");  // 기업 중 승인된 기업만 가져오기
                foreach (var compMapping in curBizWorkObj) // 승인된 기업들 전체를 foreach
                {
                    int compSn = 0;

                    //문진표 작성내역 조회
                    //var quesMaster = await quesMasterService.GetQuesMasterAsync(compMapping.ScCompInfo.RegistrationNo, paramModel.BizWorkYear);
                    var quesMaster = await quesMasterService.GetQuesMasterAsync(compMapping.REGISTRATION_NO, paramModel.BizWorkYear);
                    if (quesMaster == null)
                    {
                        continue;
                    }

                    // 조직만족도 객체
                    var quesResult2s = await quesResult2Service.GetQuesResult2sAsync(quesMaster.QuestionSn, "A1A202"); // 조직만족도 코드 : A1A202
                    // 총직원은 문진표상에 총인원을 가지고 온다.
                    var totalEmp = quesResult2s.SingleOrDefault(i => i.QuesCheckList.DetailCd == "A1A20201");
                    //참여기업 변수
                    double bizInHrMng, bizInMkt, bizInBasicCapa, bizInFinance;

                    /*SHUSER_SboFinancialIndexT sboFinacialIndexT = new SHUSER_SboFinancialIndexT();*/  // 재무정보 조회를 위한 빈 객체 선언

                    compSn = compMapping.COMP_SN;
                    //int companySn = compMapping.ScCompInfo.CompSn;
                    //VcUsrInfo companyUserInfo = await scUsrService.getScUsrByCompSn(companySn);
                    //int CompSn = companyUserInfo.CompSn;
                    //string UseErp = companyUserInfo.UseErp;

                    // 재무정보 객체 할당, 결국 필요한 것은 scFinancialIndexT
                    var scFinancialIndexT = await scFinancialIndexTService.getScFinancialIndexTAsync(compSn, paramModel.BizWorkYear.ToString());
                    if (scFinancialIndexT == null)
                    {
                        continue;
                    }

                    #region 미사용
                    // 현재 ERP는 무조건 미사용으로 처리되므로 useErp값이 1일 경우의 처리는 아래 주석과 같다
                    //if (UseErp == "1")
                    //{
                    //    sboFinacialIndexT = await sboFinancialIndexTService.GetSHUSER_SboFinancialIndexT(compMapping.ScCompInfo.RegistrationNo, ConfigurationManager.AppSettings["CorpCode"], ConfigurationManager.AppSettings["BizCode"], paramModel.BizWorkYear.ToString());
                    //    if (sboFinacialIndexT == null)
                    //    {
                    //        continue;
                    //    }
                    //}
                    //else
                    //{
                    //    var scFinancialIndexT = await scFinancialIndexTService.getScFinancialIndexTAsync(CompSn, paramModel.BizWorkYear.ToString());
                    //    if (scFinancialIndexT == null) continue;
                    //    sboFinacialIndexT = ConvertMap(scFinancialIndexT);
                    //}
                    #endregion

                    //sboFinacialIndexT.QtEmp = decimal.Parse(totalEmp.D452); // 인원수는     
                    scFinancialIndexT.QtEmp = decimal.Parse(totalEmp.D452); // 인원수는     

                    bizInHrMng = await reportUtil.GetHumanResourceMng(quesMaster.QuestionSn);
                    bizInMkt = await reportUtil.GetTechMng(quesMaster.QuestionSn, scFinancialIndexT); // 원래는 sboFinancialIndexT
                    bizInBasicCapa = await reportUtil.GetOverAllManagementTotalPoint(quesMaster.QuestionSn);
                    bizInFinance = await reportUtil.GetFinanceMng(quesMaster.QuestionSn, scFinancialIndexT); // 원래는 sboFinancialIndexT

                    //해당기업을 찾아 점수를 별도로 저장한다.
                    if (quesMaster.QuestionSn == paramModel.QuestionSn)
                    {
                        basicCapa = bizInBasicCapa;
                        mkt = bizInMkt;
                        hrMng = bizInHrMng;
                        financeMng = bizInFinance;

                        if (scFinancialIndexT.QtEmp != 0)
                        {
                            workProductivity = Math.Truncate(Convert.ToDouble((scFinancialIndexT.ValueAdded / decimal.Parse(totalEmp.D452)) / 1000));
                        }
                        if (scFinancialIndexT.CurrentSale != 0)
                        {
                            salesEarning = Math.Round(Convert.ToDouble((scFinancialIndexT.OperatingEarning / scFinancialIndexT.CurrentSale) * 100), 1);
                        }

                        if (scFinancialIndexT.CurrentLiability != 0)
                        {
                            current = Math.Round(Convert.ToDouble((scFinancialIndexT.CurrentAsset / scFinancialIndexT.CurrentLiability) * 100), 1);
                        }
                    }

                    dicBizInHrMng.Add(compMapping.REGISTRATION_NO, bizInHrMng);
                    dicBizInMkt.Add(compMapping.REGISTRATION_NO, bizInMkt);
                    dicBizInBasicCpas.Add(compMapping.REGISTRATION_NO, bizInBasicCapa);
                    dicBizInFinanceMng.Add(compMapping.REGISTRATION_NO, bizInFinance);

                    dicSales.Add(compMapping.REGISTRATION_NO, scFinancialIndexT.CurrentSale.Value); // sbo
                    dicValueadded.Add(compMapping.REGISTRATION_NO, scFinancialIndexT.ValueAdded.Value);   //sbo // 0226 추가
                    dicMaterrial.Add(compMapping.REGISTRATION_NO, scFinancialIndexT.MaterialCost.Value); // sbo
                    dicQtEmp.Add(compMapping.REGISTRATION_NO, decimal.Parse(totalEmp.D452));
                    dicOperatingErning.Add(compMapping.REGISTRATION_NO, scFinancialIndexT.OperatingEarning.Value); // sbo
                    dicCurrentAsset.Add(compMapping.REGISTRATION_NO, scFinancialIndexT.CurrentAsset.Value); // sbo
                    dicCurrentLiability.Add(compMapping.REGISTRATION_NO, scFinancialIndexT.CurrentLiability.Value); // sbo
                }
            }

            //1-A. 사업평균 점수 계산
            double totalPoint = 0;

            double bizInHrMngSum = dicBizInHrMng.Values.Sum();
            double bizInMktSum = dicBizInMkt.Values.Sum();
            double bizInBasicCpas = dicBizInBasicCpas.Values.Sum();
            double bizInFinanceMng = dicBizInFinanceMng.Values.Sum();

            totalPoint = totalPoint + bizInHrMngSum;
            totalPoint = totalPoint + bizInMktSum;
            totalPoint = totalPoint + bizInBasicCpas;
            totalPoint = totalPoint + bizInFinanceMng;
            viewModel.AvgTotalPoint = (dicBizInHrMng.Count == 0) ? 0 : Math.Round(totalPoint / dicBizInHrMng.Count, 1);

            //1-B. 해당 기업의 기초역량 점수 계산 
            double companyPoint = 0;
            companyPoint = basicCapa + mkt + hrMng + financeMng;
            viewModel.CompanyPoint = Math.Round(companyPoint, 1);

            //2. 경영역량 총괄 화살표
            viewModel.BizCapaType = ReportHelper.GetArrowTypeA(companyPoint);

            //3. 인적자원관리 화살표(해당기업)
            viewModel.HRMngType = ReportHelper.GetArrowTypeB(hrMng);

            //4. 기술경영, 마케팅 화살표(해당기업)
            viewModel.MarketingType = ReportHelper.GetArrowTypeC(mkt);

            //5. 기초역량 화살표(해당기업)
            viewModel.BasicCapaType = ReportHelper.GetArrowTypeD(basicCapa);

            //6. 조직문화도 화살표  -------------> 해당 페이지 개발 후 적용 해야함.
            var orgDivided = await rptMentorCommentService.getRptMentorCommentAsync(paramModel.QuestionSn, paramModel.NumSn, paramModel.BizWorkYear, "02030901");

            if (orgDivided != null)
            {
                if (orgDivided.Comment == "" || orgDivided.Comment == null) orgDivided.Comment = "1";
                viewModel.OrgType = ReportHelper.GetArrowTypeE(int.Parse(orgDivided.Comment));
            }
            else
            {
                viewModel.OrgType = "C";
            }
            //7. 고객의수, 상품의질 화살표 -------------> 해당 페이지 개발 후 적용 해야함.
            var custMng = await rptMentorCommentService.getRptMentorCommentAsync(paramModel.QuestionSn, paramModel.NumSn, paramModel.BizWorkYear, "02022112");
            if (custMng != null)
            {
                if (custMng.Comment == "" || custMng.Comment == null) custMng.Comment = "1";
                viewModel.CustMngType = ReportHelper.GetArrowTypeE(int.Parse(custMng.Comment));
            }
            else
            {
                viewModel.CustMngType = "C";
            }
            //8. 전반적 제도 및 규정관리체계 화살표 -------------> 해당 페이지 개발 후 적용 해야함.02033128
            var rool = await rptMentorCommentService.getRptMentorCommentAsync(paramModel.QuestionSn, paramModel.NumSn, paramModel.BizWorkYear, "02033128");

            if (rool != null)
            {
                if (rool.Comment == "" || rool.Comment == null) rool.Comment = "1";
                viewModel.RoolType = ReportHelper.GetArrowTypeE(int.Parse(rool.Comment));

            }
            else
            {
                viewModel.RoolType = "C";
            }

            //9. 조직역량-인적자원관리 해당기업 점수
            OverallSummaryPointViewModel orgCapa = new OverallSummaryPointViewModel();

            var regCav = await scCavService.getMaxYear();
            var regCavDetail = scCavService.GetCavAsync(regCav);

            orgCapa.CompanyPoint = Math.Round(hrMng, 1);
            //12. 조직역량-인적자원관리 참여기업 평균 점수
            orgCapa.AvgBizInCompanyPoint = Math.Round(dicBizInHrMng.Values.Average(), 1);
            //15. 조직역량-인적자원관리 전체평균 점수
            orgCapa.AvgTotalPoint = Math.Round((dicBizInHrMng.Values.Sum() + 810.00) / (dicBizInHrMng.Count + 100), 1);
            //18. 조직역량-1인당노동생산성 해당기업점수
            orgCapa.CompanyPoint2 = workProductivity;
            //21. 조직역량-1인당노동생산성 참여기업 평균
            /*orgCapa.AvgBizInCompanyPoint2 = (dicQtEmp.Values.Sum() == 0) ? 0 : Math.Truncate(Convert.ToDouble(((dicSales.Values.Sum() - dicValueadded.Values.Sum()) / dicQtEmp.Values.Sum()) / 1000));*/  // 부가가치로 바꿈 0226
            orgCapa.AvgBizInCompanyPoint2 = (dicQtEmp.Values.Sum() == 0) ? 0 : Math.Truncate(Convert.ToDouble(((dicValueadded.Values.Sum()) / dicQtEmp.Values.Sum()) / 1000));  // 부가가치만 적용 0303
            //24. 조직역량-1인당노동생산성 전체 평균
            /*orgCapa.AvgTotalPoint2 = Math.Truncate(Convert.ToDouble((((dicSales.Values.Sum() + 307076158152) - (dicValueadded.Values.Sum() + 191352188304)) / (dicQtEmp.Values.Sum() + 1964)) / 1000));*/   // 부가가치로 바꿈 0226
            //orgCapa.AvgTotalPoint2 = Math.Truncate(Convert.ToDouble((((dicValueadded.Values.Sum() + 191352188304)) / (dicQtEmp.Values.Sum() + 1964)) / 1000)); // 부가가치만 적용 0303
            orgCapa.AvgTotalPoint2 = Math.Truncate(Convert.ToDouble((((dicValueadded.Values.Sum() + 51774550474)) / (dicQtEmp.Values.Sum() + 1533)) / 1000)); // 0411 - phm - 산식 기초자료값 변경
            //27. 조직역량-1인당노동생산성 중소기업평균
            var pointCavLp = regCavDetail.CavLp;
            orgCapa.AvgSMCompanyPoint = Decimal.Round(pointCavLp, 1);

            viewModel.OrgCapa = orgCapa;

            //10. 상품화역량-기술경영 마케팅관리 해당기업 점수
            OverallSummaryPointViewModel prductionCapa = new OverallSummaryPointViewModel();

            prductionCapa.CompanyPoint = Math.Round(mkt, 1);
            //13. 상품화역량-기술경영 마케팅관리 참여기업 평균 점수
            prductionCapa.AvgBizInCompanyPoint = Math.Round(dicBizInMkt.Values.Average(), 1);
            //16. 상품화역량-기술경영 마케팅관리 전체평균 점수
            prductionCapa.AvgTotalPoint = Math.Round((dicBizInMkt.Values.Sum() + 1920.0) / (dicBizInMkt.Count + 100), 1);
            //19. 상품화역량-매출영업이익률 해당기업 점수
            prductionCapa.CompanyPoint2 = salesEarning;
            //22. 상품화역량-매출영업이익률 참여기업 평균
            prductionCapa.AvgBizInCompanyPoint2 = (dicSales.Values.Sum() == 0) ? 0 : Math.Round(Convert.ToDouble((dicOperatingErning.Values.Sum() / dicSales.Values.Sum()) * 100), 1);
            //25. 상품화역량-매출영업이익률 전체평균
            prductionCapa.AvgTotalPoint2 = Math.Round(Convert.ToDouble(((dicOperatingErning.Values.Sum() + 15961070936) / (dicSales.Values.Sum() + 307076158152)) * 100), 1);
            //28. 상품화역량-매출영업이익률 중소기업평균
            var pointCavOp = regCavDetail.CavOp;
            prductionCapa.AvgSMCompanyPoint = Decimal.Round(pointCavOp, 1); //중소기업평균 매출영업이익률
            viewModel.ProductionCapa = prductionCapa;

            //11. 위험관리역량-기초역량 해당기업 점수
            OverallSummaryPointViewModel riskMngCapa = new OverallSummaryPointViewModel();
            riskMngCapa.CompanyPoint = Math.Round(basicCapa, 1);
            //14. 위험관리역량-기초역량 참여기업 평균 점수
            riskMngCapa.AvgBizInCompanyPoint = Math.Round(dicBizInBasicCpas.Values.Average(), 1);
            //17. 위험관리역량-기초역량 전체평균 점수
            riskMngCapa.AvgTotalPoint = Math.Round((dicBizInBasicCpas.Values.Sum() + 630.00) / (dicBizInBasicCpas.Count + 100), 1);
            //20. 위험관리역량-유동비율 해당기업 점수
            riskMngCapa.CompanyPoint2 = current;
            //23. 위험관리역량-유동비율 참여기업평균 점수
            riskMngCapa.AvgBizInCompanyPoint2 = (dicCurrentLiability.Values.Sum() == 0) ? 0 : Math.Round(Convert.ToDouble((dicCurrentAsset.Values.Sum() / dicCurrentLiability.Values.Sum()) * 100), 1);
            //26. 위험관리역량-유동비율 전체평균 점수
            riskMngCapa.AvgTotalPoint2 = Math.Round(Convert.ToDouble(((dicCurrentAsset.Values.Sum() + 129006100528) / (dicCurrentLiability.Values.Sum() + 77693292830)) * 100), 1);
            //29. 위험관리역량-유동비율 중소기업평균 점수
            var pointCavCr = regCavDetail.CavCr;
            riskMngCapa.AvgSMCompanyPoint = Decimal.Round(pointCavCr, 1);//중소기업평균 유동비율
            viewModel.RiskMngCapa = riskMngCapa;


            #region 멘토 작성내용 조회

            var comments = await rptMentorCommentService.GetRptMentorCommentListAsync(paramModel.QuestionSn, paramModel.NumSn, paramModel.BizWorkYear, "04");
            //조직역량->조직분화도
            var comment0 = comments.SingleOrDefault(i => i.DetailCd == "01010401");
            viewModel.CommentList.Add(ReportHelper.MakeCommentViewModel(paramModel, "01010401", comment0));

            // 상품화역량 -> 고객의수, 상품의 질 및 마케팅 수준
            var comment1 = comments.SingleOrDefault(i => i.DetailCd == "01010402");
            viewModel.CommentList.Add(ReportHelper.MakeCommentViewModel(paramModel, "01010402", comment1));

            // 위험관리역량 -> 제무회계 관리체계 및 제도수준
            var comment2 = comments.SingleOrDefault(i => i.DetailCd == "01010403");
            viewModel.CommentList.Add(ReportHelper.MakeCommentViewModel(paramModel, "01010403", comment2));


            var comment3 = comments.SingleOrDefault(i => i.DetailCd == "01010404");
            viewModel.CommentList.Add(ReportHelper.MakeCommentViewModel(paramModel, "01010404", comment3));

            var comment4 = comments.SingleOrDefault(i => i.DetailCd == "01010405");
            viewModel.CommentList.Add(ReportHelper.MakeCommentViewModel(paramModel, "01010405", comment4));

            var comment5 = comments.SingleOrDefault(i => i.DetailCd == "01010406");
            viewModel.CommentList.Add(ReportHelper.MakeCommentViewModel(paramModel, "01010406", comment5));


            var comment6 = comments.SingleOrDefault(i => i.DetailCd == "01010407");
            viewModel.CommentList.Add(ReportHelper.MakeCommentViewModel(paramModel, "01010407", comment6));

            var comment7 = comments.SingleOrDefault(i => i.DetailCd == "01010408");
            viewModel.CommentList.Add(ReportHelper.MakeCommentViewModel(paramModel, "01010408", comment7));

            var comment8 = comments.SingleOrDefault(i => i.DetailCd == "01010409");
            viewModel.CommentList.Add(ReportHelper.MakeCommentViewModel(paramModel, "01010409", comment8));


            #endregion


            // 보고서 상단 회사명 가져오기 --------------------------------------------------------------------------------------------------
            //var scCompMapping = await scCompMappingService.GetCompMappingAsync(paramModel.NumSn, paramModel.CompSn);
            //paramModel.CompNm = scCompMapping.ScCompInfo.CompNm;

            ViewBag.CompNm = paramModel.CompNm;
            ViewBag.paramModel = paramModel;
            ViewBag.NumSn = paramModel.NumSn;
            ViewBag.CompSn = paramModel.CompSn;
            ViewBag.BizWorkYear = paramModel.BizWorkYear;
            ViewBag.Status = paramModel.Status;
            ViewBag.QuestionSn = paramModel.QuestionSn;
            //ViewBag.NullCheck = paramModel.NullCheck;

            //var rptquestion = await quesMasterService.GetQuesMasterAsync(paramModel.QuestionSn);

            //var a = ReportHelper.SelectCommentViewModel(paramModel.QuestionSn, paramModel.NumSn, rptquestion.BasicYear, "01010401",comment0.Comment);
            //var b = ReportHelper.SelectCommentViewModel(paramModel.QuestionSn, paramModel.NumSn, rptquestion.BasicYear, "01010402", comment1.Comment);
            //var c = ReportHelper.SelectCommentViewModel(paramModel.QuestionSn, paramModel.NumSn, rptquestion.BasicYear, "01010403", comment2.Comment);

            //if (a.NullCheck.Equals("N") || b.NullCheck.Equals("N") || c.NullCheck.Equals("N"))
            //{
            //    paramModel.NullCheck = "N";
            //}
            //else
            //{
            //    paramModel.NullCheck = "Y";
            //}

            //if(paramModel.Status == "C")
            //{ 
            cachedViewBag.Add("LeftMenu", ViewBag.LeftMenu);
            cachedViewBag.Add("paramMomdel", ViewBag.paramModel);
            cachedViewBag.Add("CompNm", ViewBag.CompNm);
            cachedViewBag.Add("CompSn", ViewBag.CompSn);
            cachedViewBag.Add("NumSn", ViewBag.NumSn);  // NumSn으로 교체
            cachedViewBag.Add("BizWorkYear", ViewBag.BizWorkYear);
            cachedViewBag.Add("Status", ViewBag.Status);
            cachedViewBag.Add("QuestionSn", ViewBag.QuestionSn);
            cachedViewBag.Add("NullCheck", ViewBag.NullCheck);

            this.HttpContext.Cache.Insert(cacheId + "ViewBag", cachedViewBag);
            this.HttpContext.Cache.Insert(cacheId + "ViewModel", viewModel);
            //}

            return View(viewModel);

        }



        [HttpPost]
        public async Task<ActionResult> OverallSummary(OverallSummaryViewModel viewModel, BasicSurveyReportViewModel paramModel)
        {
            // NON ERP : NOT TARGET

            string cacheId = "OverallSummary_" + paramModel.QuestionSn.ToString() + "_";
            string ViewBagCacheId = cacheId + "ViewBag";
            string ViewModelCacheId = cacheId + "ViewModel";

            this.HttpContext.Cache.Remove(ViewBagCacheId);
            this.HttpContext.Cache.Remove(ViewModelCacheId);

            ViewBag.naviLeftMenu = Global.Report;
            var comments = await rptMentorCommentService.GetRptMentorCommentListAsync(paramModel.QuestionSn, paramModel.NumSn, paramModel.BizWorkYear, "04");

            foreach (var item in viewModel.CommentList)
            {
                var comment = comments.SingleOrDefault(i => i.DetailCd == item.DetailCd);
                if (comment == null)
                {
                    rptMentorCommentService.Insert(ReportHelper.MakeRptMentorcomment(item, paramModel));
                }
                else
                {
                    comment.Comment = item.Comment;
                }
            }

            await rptMentorCommentService.SaveDbContextAsync();

            if (viewModel.SubmitType == "T")
            {
                return RedirectToAction("OverallSummary", "BasicSurveyReport", new { NumSn = paramModel.NumSn, CompSn = paramModel.CompSn, BizWorkYear = paramModel.BizWorkYear, Status = paramModel.Status, QuestionSn = paramModel.QuestionSn });
            }
            else
            {
                return RedirectToAction("OverallResultCover", "BasicSurveyReport", new { NumSn = paramModel.NumSn, CompSn = paramModel.CompSn, BizWorkYear = paramModel.BizWorkYear, Status = paramModel.Status, QuestionSn = paramModel.QuestionSn });
            }
        }

        public async Task<ActionResult> OverallResultCover(BasicSurveyReportViewModel paramModel)
        {
            ViewBag.naviLeftMenu = Global.Report;
            //ViewBag.paramModel = paramModel;

            ViewBag.CompNm = paramModel.CompNm;
            ViewBag.paramModel = paramModel;
            ViewBag.NumSn = paramModel.NumSn;
            ViewBag.CompSn = paramModel.CompSn;
            ViewBag.BizWorkYear = paramModel.BizWorkYear;
            ViewBag.Status = paramModel.Status;
            ViewBag.QuestionSn = paramModel.QuestionSn;
            //ViewBag.NullCheck = paramModel.NullCheck;
            //보고서 상단 회사명 가져오기
            //var scCompMapping = await scCompMappingService.GetCompMappingAsync(paramModel.NumSn, paramModel.CompSn);
            //paramModel.CompNm = scCompMapping.ScCompInfo.CompNm;

            var vcCompInfoObj = await vcCompInfoService.getVcCompInfoByCompSn(paramModel.CompSn);
            paramModel.CompNm = vcCompInfoObj.CompNm;

            return View(paramModel);
        }


        public async Task<ActionResult> OrgHR01(BasicSurveyReportViewModel paramModel)
        {
            string cacheId = "OrgHR01" + paramModel.QuestionSn.ToString() + "_";
            string methodCacheId = cacheId + "methodCache";
            // NON ERP : Completed
            OrgHR01ViewModel viewModel = new OrgHR01ViewModel();
            OrgHR01ViewModel viewModel2 = new OrgHR01ViewModel(); // orghr02를 위한 viewmodel

            List<object> viewList = new List<object>();

            ViewBag.naviLeftMenu = Global.Report;
            ViewBag.CompNm = paramModel.CompNm;
            ViewBag.paramModel = paramModel;
            ViewBag.NumSn = paramModel.NumSn;
            ViewBag.CompSn = paramModel.CompSn;
            ViewBag.BizWorkYear = paramModel.BizWorkYear;
            ViewBag.Status = paramModel.Status;
            ViewBag.QuestionSn = paramModel.QuestionSn;
            //ViewBag.NullCheck = paramModel.NullCheck;

            bool cacheStatus = false;
            Dictionary<string, object> methodCache = (Dictionary<string, object>)this.HttpContext.Cache[methodCacheId];

            if (methodCache != null && methodCache.Count > 0) // 있다, flag true로 할당
            {
                cacheStatus = true;
            }
            else // 없다, 새 객체 할당
            {
                methodCache = new Dictionary<string, object>();
            }

            if (cacheStatus == true)
            {
                ViewBag.paramModel = methodCache["paramModel"];
                ViewBag.CompNm = ViewBag.paramModel.CompNm;
                return View(methodCache["viewList"]);
            }




            ReportUtil reportUtil = new ReportUtil(
                scBizWorkService,
                quesResult1Service,
                quesResult2Service,
                quesMasterService,
                sboFinancialIndexTService,
                scCavService,
                scCompanyFinanceService,
                rptMasterService,
                scFinancialIndexTService,
                procMngService
            );

            LocalReportUtil localReportUtil = new LocalReportUtil(
                scBizWorkService,
                quesResult1Service,
                quesResult2Service,
                quesMasterService,
                sboFinancialIndexTService,
                scFinancialIndexTService,
                scCavService,
                scUsrService,
                scCompanyFinanceService,
                rptMasterService,
                procMngService
            );

            //viewModel.CheckList = await localReportUtil.getGrowthStepPointCheckList(paramModel, "A1D101");
            viewModel.CheckList = await reportUtil.getGrowthStepPointCheckList(paramModel, "A1D101");

            //검토결과 데이터 생성
            var listRptMentorComment = await rptMentorCommentService.GetRptMentorCommentListAsync(paramModel.QuestionSn, paramModel.NumSn, paramModel.BizWorkYear, "06");

            //레포트 체크리스트
            var enumRptCheckList = await rptCheckListService.GetRptCheckListBySmallClassCd("06");

            //CommentList 채우기
            var CommentList = ReportHelper.MakeCommentViewModel(enumRptCheckList, listRptMentorComment);

            viewModel.CommentList = CommentList;
            ViewBag.paramModel = paramModel;

            //보고서 상단 회사명 가져오기
            //var scCompMapping = await scCompMappingService.GetCompMappingAsync(paramModel.NumSn, paramModel.CompSn);
            //paramModel.CompNm = scCompMapping.ScCompInfo.CompNm;

            var vcCompInfoObj = await vcCompInfoService.getVcCompInfoByCompSn(paramModel.CompSn);
            paramModel.CompNm = vcCompInfoObj.CompNm;

            // orghr02 -------------------------------------------

            viewModel2.CheckList = await localReportUtil.getGrowthStepPointCheckList(paramModel, "A1D102");

            var listRptMentorComment2 = await rptMentorCommentService.GetRptMentorCommentListAsync(paramModel.QuestionSn, paramModel.NumSn, paramModel.BizWorkYear, "07");

            //레포트 체크리스트
            var enumRptCheckList2 = await rptCheckListService.GetRptCheckListBySmallClassCd("07");

            //CommentList 채우기
            var CommentList2 = ReportHelper.MakeCommentViewModel(enumRptCheckList2, listRptMentorComment2);

            viewModel2.CommentList = CommentList;

            // list모델에 객체 채우기 --------------------------
            viewList.Add(viewModel);
            viewList.Add(viewModel2);


            methodCache["paramModel"] = paramModel;
            methodCache["viewList"] = viewList;
            this.HttpContext.Cache.Insert(methodCacheId, methodCache);

            // view에 사용될 viewList 돌려주기 -----------------
            return View(viewList);
        }

        [HttpPost]
        public async Task<ActionResult> OrgHR01(OrgHR01ViewModel viewModel1, OrgHR01ViewModel viewModel2, BasicSurveyReportViewModel paramModel)
        {
            string cacheId = "OrgHR01" + paramModel.QuestionSn.ToString() + "_";
            //Dictionary<string, object> methodCache = new Dictionary<string, object>() ;
            string methodCacheId = cacheId + "methodCache";

            this.HttpContext.Cache.Remove(methodCacheId);

            // NON ERP : NOT TARGET
            ViewBag.naviLeftMenu = Global.Report;
            OrgHR01ViewModel viewModel = viewModel1;
            var listRptMentorComment = await rptMentorCommentService.GetRptMentorCommentListAsync(paramModel.QuestionSn, paramModel.NumSn, paramModel.BizWorkYear, "06");

            foreach (var item in viewModel.CommentList)
            {
                var comment = listRptMentorComment.SingleOrDefault(i => i.DetailCd == item.DetailCd);
                if (comment == null)
                {
                    rptMentorCommentService.Insert(ReportHelper.MakeRptMentorcomment(item, paramModel));
                }
                else
                {
                    comment.Comment = item.Comment;
                }
            }
            await rptMentorCommentService.SaveDbContextAsync();

            if (viewModel.SubmitType == "T")
            {
                return RedirectToAction("OrgHR01", "BasicSurveyReport", new { NumSn = paramModel.NumSn, CompSn = paramModel.CompSn, BizWorkYear = paramModel.BizWorkYear, Status = paramModel.Status, QuestionSn = paramModel.QuestionSn });
            }
            else
            {
                return RedirectToAction("OrgProductivity", "BasicSurveyReport", new { NumSn = paramModel.NumSn, CompSn = paramModel.CompSn, BizWorkYear = paramModel.BizWorkYear, Status = paramModel.Status, QuestionSn = paramModel.QuestionSn });
            }
        }

        public async Task<ActionResult> OrgHR02(BasicSurveyReportViewModel paramModel)
        {
            // NON ERP : Completed
            ViewBag.naviLeftMenu = Global.Report;
            ViewBag.paramModel = paramModel;
            OrgHR01ViewModel viewModel = new OrgHR01ViewModel();

            //ReportUtil reportUtil = new ReportUtil(scBizWorkService, quesResult1Service, quesResult2Service, quesMasterService, sboFinancialIndexTService, scCavService);

            LocalReportUtil localReportUtil = new LocalReportUtil(scBizWorkService, quesResult1Service, quesResult2Service, quesMasterService, sboFinancialIndexTService, scFinancialIndexTService, scCavService, scUsrService, scCompanyFinanceService, rptMasterService, procMngService);
            viewModel.CheckList = await localReportUtil.getGrowthStepPointCheckList(paramModel, "A1D102");

            //검토결과 데이터 생성
            var listRptMentorComment = await rptMentorCommentService.GetRptMentorCommentListAsync(paramModel.QuestionSn, paramModel.NumSn, paramModel.BizWorkYear, "07");

            //레포트 체크리스트
            var enumRptCheckList = await rptCheckListService.GetRptCheckListBySmallClassCd("07");

            //CommentList 채우기
            var CommentList = ReportHelper.MakeCommentViewModel(enumRptCheckList, listRptMentorComment);

            viewModel.CommentList = CommentList;

            ViewBag.CompNm = paramModel.CompNm;
            ViewBag.paramModel = paramModel;
            ViewBag.NumSn = paramModel.NumSn;
            ViewBag.CompSn = paramModel.CompSn;
            ViewBag.BizWorkYear = paramModel.BizWorkYear;
            ViewBag.Status = paramModel.Status;
            ViewBag.QuestionSn = paramModel.QuestionSn;
            //ViewBag.NullCheck = paramModel.NullCheck;

            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> OrgHR02(OrgHR01ViewModel viewModel, BasicSurveyReportViewModel paramModel)
        {
            // NON ERP : NOT TARGET
            ViewBag.naviLeftMenu = Global.Report;


            var listRptMentorComment = await rptMentorCommentService.GetRptMentorCommentListAsync(paramModel.QuestionSn, paramModel.NumSn, paramModel.BizWorkYear, "07");

            foreach (var item in viewModel.CommentList)
            {
                var comment = listRptMentorComment.SingleOrDefault(i => i.DetailCd == item.DetailCd);
                if (comment == null)
                {
                    rptMentorCommentService.Insert(ReportHelper.MakeRptMentorcomment(item, paramModel));
                }
                else
                {
                    comment.Comment = item.Comment;
                }
            }
            await rptMentorCommentService.SaveDbContextAsync();

            if (viewModel.SubmitType == "T")
            {
                return RedirectToAction("OrgHR02", "BasicSurveyReport", new { NumSn = paramModel.NumSn, CompSn = paramModel.CompSn, BizWorkYear = paramModel.BizWorkYear, Status = paramModel.Status, QuestionSn = paramModel.QuestionSn });
            }
            else
            {
                return RedirectToAction("OrgProductivity", "BasicSurveyReport", new { NumSn = paramModel.NumSn, CompSn = paramModel.CompSn, BizWorkYear = paramModel.BizWorkYear, Status = paramModel.Status, QuestionSn = paramModel.QuestionSn });
            }
        }

        public async Task<ActionResult> OrgProductivity(BasicSurveyReportViewModel paramModel)
        {
            // NON ERP : Completed
            ViewBag.naviLeftMenu = Global.Report;
            ViewBag.CompNm = paramModel.CompNm;
            ViewBag.paramModel = paramModel;
            ViewBag.NumSn = paramModel.NumSn;
            ViewBag.CompSn = paramModel.CompSn;
            ViewBag.BizWorkYear = paramModel.BizWorkYear;
            ViewBag.Status = paramModel.Status;
            ViewBag.QuestionSn = paramModel.QuestionSn;
            //ViewBag.NullCheck = paramModel.NullCheck;

            string cacheId = "OrgProductivity" + paramModel.QuestionSn.ToString() + "_";
            string methodCacheId = cacheId + "methodCache";

            Boolean cacheStatus = false;

            Dictionary<string, object> methodCache = (Dictionary<string, object>)this.HttpContext.Cache[methodCacheId];

            if (methodCache != null && methodCache.Count > 0)
            {
                cacheStatus = true;
            }
            else
            {
                methodCache = new Dictionary<string, object>();
            }

            if (cacheStatus == true)
            {
                ViewBag.paramModel = methodCache["paramModel"];
                ViewBag.CavYear = methodCache["CavYear"];
                return View(methodCache["viewModel"]);
            }

            ReportUtil reportUtil = new ReportUtil(scBizWorkService, quesResult1Service, quesResult2Service, quesMasterService, sboFinancialIndexTService, scCavService, scCompanyFinanceService, rptMasterService, scFinancialIndexTService, procMngService);

            OrgProductivityViewModel viewModel = new OrgProductivityViewModel();
            viewModel.CheckList = new List<CheckListViewModel>();
            viewModel.Productivity = new BarChartViewModel();
            viewModel.Activity = new BarChartViewModel();


            SqlParameter numSn = new SqlParameter("NUM_SN", paramModel.NumSn);
            object[] parameters = new object[] { numSn };

            var curBizWorkObj = await procMngService.getBizWorkInfoByBizWorkSn(parameters);


            //1) 현재 사업에 참여한 업체 평균
            var curBizWork = await scBizWorkService.GetBizWorkByBizWorkSn(int.Parse(paramModel.NumSn));

            Dictionary<string, decimal> dicSales = new Dictionary<string, decimal>();
            Dictionary<string, decimal> dicMaterrial = new Dictionary<string, decimal>();
            Dictionary<string, decimal> dicValueadded = new Dictionary<string, decimal>();
            Dictionary<string, decimal> dicQtEmp = new Dictionary<string, decimal>();
            Dictionary<string, decimal> dicTotalAsset = new Dictionary<string, decimal>();

            {
                //var compMappings = curBizWork.ScCompMappings.Where(sc => sc.Status == "A");
                foreach (var compMapping in curBizWorkObj)
                {
                    //문진표 작성내역 조회
                    var quesMaster = await quesMasterService.GetQuesOgranAnalysisAsync(compMapping.REGISTRATION_NO, paramModel.BizWorkYear);
                    if (quesMaster == null)
                    {
                        continue;
                    }

                    //다래 재무정보 조회해야 함.

                    SHUSER_SboFinancialIndexT sboFinacialIndexT = new SHUSER_SboFinancialIndexT();

                    //int companySn = compMapping.ScCompInfo.CompSn;
                    //VcUsrInfo companyUserInfo = await scUsrService.getScUsrByCompSn(companySn);
                    //int CompSn = companyUserInfo.CompSn;
                    //string UseErp = companyUserInfo.UseErp;


                    //if (UseErp == "1")
                    //{
                    //    sboFinacialIndexT = await sboFinancialIndexTService.GetSHUSER_SboFinancialIndexT(compMapping.ScCompInfo.RegistrationNo, ConfigurationManager.AppSettings["CorpCode"], ConfigurationManager.AppSettings["BizCode"], paramModel.BizWorkYear.ToString());
                    //    if (sboFinacialIndexT == null)
                    //    {
                    //        continue;
                    //    }
                    //}
                    //else
                    //{
                    var scFinancialIndexT = await scFinancialIndexTService.getScFinancialIndexTAsync(compMapping.COMP_SN, paramModel.BizWorkYear.ToString());
                    if (scFinancialIndexT == null) continue;

                    sboFinacialIndexT = ConvertMap(scFinancialIndexT);
                    //}

                    // A1A202 : 조직만족도
                    var quesResult2s = await quesResult2Service.GetQuesResult2sAsync(quesMaster.QuestionSn, "A1A202");
                    // 총직원은 다래 재무정보가 아닌 문진표상에 총인원을 가지고 온다.
                    var totalEmp = quesResult2s.SingleOrDefault(i => i.QuesCheckList.DetailCd == "A1A20201");

                    //중소기업 평균 값 가져오는 부분
                    int workYear = paramModel.BizWorkYear - 1;

                    var regCav = await scCavService.getMaxYear();
                    var regCavDetail = scCavService.GetCavAsync(regCav);
                    ViewBag.CavYear = regCavDetail.CavDt;
                    //해당기업을 찾아 점수를 별도로 저장한다.
                    if (quesMaster.QuestionSn == paramModel.QuestionSn)
                    {
                        // 0226 산식
                        //viewModel.Productivity.Dividend = Math.Truncate(Convert.ToDouble((sboFinacialIndexT.CurrentSale.Value - sboFinacialIndexT.ValueAdded.Value) / 1000));

                        // 0302 변경 -> 부가가치만 산식에 넣는다
                        viewModel.Productivity.Dividend = Math.Truncate(Convert.ToDouble((sboFinacialIndexT.ValueAdded.Value) / 1000));
                        viewModel.Productivity.Divisor = Math.Round(Convert.ToDouble(totalEmp.D452), 0);
                        viewModel.Productivity.Result = (viewModel.Productivity.Divisor == 0) ? 0 : Math.Truncate(viewModel.Productivity.Dividend / viewModel.Productivity.Divisor);
                        viewModel.Productivity.Company = viewModel.Productivity.Result;
                        viewModel.Productivity.AvgSMCompany = Math.Round(Convert.ToDecimal(regCavDetail.CavLp / 1000),1); //중소기업평균 노동생산성

                        viewModel.Activity.Dividend = Math.Truncate(Convert.ToDouble(sboFinacialIndexT.CurrentSale.Value / 1000));
                        viewModel.Activity.Divisor = Math.Truncate(Convert.ToDouble(sboFinacialIndexT.TotalAsset.Value / 1000));
                        viewModel.Activity.Result = (viewModel.Activity.Divisor == 0) ? 0 : Math.Round((viewModel.Activity.Dividend / viewModel.Activity.Divisor) * 100, 1);
                        viewModel.Activity.Company = viewModel.Activity.Result;
                        viewModel.Activity.AvgSMCompany = regCavDetail.CavTat;  //중소기업평균 총자산회전율
                    }

                    dicSales.Add(compMapping.REGISTRATION_NO, sboFinacialIndexT.CurrentSale.Value);
                    dicMaterrial.Add(compMapping.REGISTRATION_NO, sboFinacialIndexT.MaterialCost.Value);
                    dicValueadded.Add(compMapping.REGISTRATION_NO, sboFinacialIndexT.ValueAdded.Value);
                    dicQtEmp.Add(compMapping.REGISTRATION_NO, decimal.Parse(totalEmp.D452)); // 이 부분
                    dicTotalAsset.Add(compMapping.REGISTRATION_NO, sboFinacialIndexT.TotalAsset.Value);
                }
            }

            //부가가치노동생산성

            //viewModel.Productivity.AvgBizInCompany = (dicQtEmp.Values.Sum() == 0) ? 0 : Math.Truncate(Convert.ToDouble(dicSales.Values.Sum() - dicValueadded.Values.Sum() / dicQtEmp.Values.Sum() / 1000)); // 참여기업평균 0226 수정
            /*viewModel.Productivity.AvgBizInCompany =  (dicQtEmp.Values.Sum() == 0) ? 0 : Math.Truncate(Convert.ToDouble(((dicSales.Values.Sum() - dicValueadded.Values.Sum()) / dicQtEmp.Values.Sum()) / 1000));*/  // 부가가치로 바꿈 0226
            viewModel.Productivity.AvgBizInCompany = (dicQtEmp.Values.Sum() == 0) ? 0 : Math.Truncate(Convert.ToDouble(((dicValueadded.Values.Sum()) / dicQtEmp.Values.Sum()) / 1000)); // 부가가치만 사용 0303

            // 전체평균
            /*viewModel.Productivity.AvgTotal = Math.Truncate(Convert.ToDouble((((dicSales.Values.Sum() + 307076158152 - (dicValueadded.Values.Sum()) + 191352188304)) / (dicQtEmp.Values.Sum() + 1964)) / 1000));*/ // 전체평균 0226 수정
            /*viewModel.Productivity.AvgTotal = Math.Truncate(Convert.ToDouble((((dicValueadded.Values.Sum() + 191352188304)) / (dicQtEmp.Values.Sum() + 1964)) / 1000));*/    // 부가가치만 사용 0303
            viewModel.Productivity.AvgTotal = Math.Truncate(Convert.ToDouble((((dicValueadded.Values.Sum() + 51774550474)) / (dicQtEmp.Values.Sum() + 1533)) / 1000)); // 0411 - phm - 기초자료값 변경


            //총자산회전률 평균값 계산
            viewModel.Activity.AvgBizInCompany = (dicTotalAsset.Values.Sum() == 0) ? 0 : Math.Round(Convert.ToDouble(dicSales.Values.Sum() / dicTotalAsset.Values.Sum() * 100), 1);
            viewModel.Activity.AvgTotal = Math.Round(Convert.ToDouble((dicSales.Values.Sum() + 307076158152) / (dicTotalAsset.Values.Sum() + 287322762537) * 100), 1);

            //검토결과 데이터 생성
            var listRptMentorComment = await rptMentorCommentService.GetRptMentorCommentListAsync(paramModel.QuestionSn, paramModel.NumSn, paramModel.BizWorkYear, "08");

            //레포트 체크리스트
            var enumRptCheckList = await rptCheckListService.GetRptCheckListBySmallClassCd("08");

            //CommentList 채우기
            var CommentList = ReportHelper.MakeCommentViewModel(enumRptCheckList, listRptMentorComment);

            viewModel.CommentList = CommentList;

            //보고서 상단 회사명 가져오기
            //var scCompMapping = await scCompMappingService.GetCompMappingAsync(paramModel.NumSn, paramModel.CompSn);
            //paramModel.CompNm = scCompMapping.ScCompInfo.CompNm;

            ViewBag.CompNm = paramModel.CompNm;
            ViewBag.paramModel = paramModel;


            // cache 들어올 자리


            methodCache["paramModel"] = paramModel;
            methodCache["viewModel"] = viewModel;
            methodCache["CavYear"] = ViewBag.CavYear;
            this.HttpContext.Cache.Insert(methodCacheId, methodCache);

            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> OrgProductivity(OrgProductivityViewModel viewModel, BasicSurveyReportViewModel paramModel)
        {
            string cacheId = "OrgProductivity" + paramModel.QuestionSn.ToString() + "_";
            string methodCacheId = cacheId + "methodCache";
            this.HttpContext.Cache.Remove(methodCacheId);

            // NON ERP : NOT TARGET
            ViewBag.naviLeftMenu = Global.Report;

            var listRptMentorComment = await rptMentorCommentService.GetRptMentorCommentListAsync(paramModel.QuestionSn, paramModel.NumSn, paramModel.BizWorkYear, "08");

            foreach (var item in viewModel.CommentList)
            {
                var comment = listRptMentorComment.SingleOrDefault(i => i.DetailCd == item.DetailCd);
                if (comment == null)
                {
                    rptMentorCommentService.Insert(ReportHelper.MakeRptMentorcomment(item, paramModel));
                }
                else
                {
                    comment.Comment = item.Comment;
                }
            }
            await rptMentorCommentService.SaveDbContextAsync();

            if (viewModel.SubmitType == "T")
            {
                return RedirectToAction("OrgProductivity", "BasicSurveyReport", new { NumSn = paramModel.NumSn, CompSn = paramModel.CompSn, BizWorkYear = paramModel.BizWorkYear, Status = paramModel.Status, QuestionSn = paramModel.QuestionSn });
            }
            else
            {
                return RedirectToAction("OrgDivided", "BasicSurveyReport", new { NumSn = paramModel.NumSn, CompSn = paramModel.CompSn, BizWorkYear = paramModel.BizWorkYear, Status = paramModel.Status, QuestionSn = paramModel.QuestionSn });
            }
        }

        public async Task<ActionResult> OrgDivided(BasicSurveyReportViewModel paramModel)
        {
            // NON ERP : Completed
            ViewBag.naviLeftMenu = Global.Report;
            ViewBag.paramModel = paramModel;

            string cacheId = "OrgDivided" + paramModel.QuestionSn.ToString() + "_";
            string methodCacheId = cacheId + "methodCache";

            ReportUtil reportUtil = new ReportUtil(scBizWorkService, quesResult1Service, quesResult2Service, quesMasterService, sboFinancialIndexTService, scCavService, scCompanyFinanceService, rptMasterService, scFinancialIndexTService, procMngService);

            var viewModel = new OrgDividedViewModel();

            bool cacheStatus = false;

            Dictionary<string, object> methodCache = (Dictionary<string, object>)this.HttpContext.Cache[methodCacheId];

            if (methodCache != null && methodCache.Count > 0)
            {
                cacheStatus = true;
            }
            else
            {
                methodCache = new Dictionary<string, object>();
            }

            if (cacheStatus == true)
            {
                ViewBag.paramModel = methodCache["paramModel"];
                return View(methodCache["viewModel"]);
            }



            //1) 현재 사업에 참여한 업체 평균
            //var curBizWork = await scBizWorkService.GetBizWorkByBizWorkSn(int.Parse(paramModel.NumSn));

            SqlParameter numSn = new SqlParameter("NUM_SN", paramModel.NumSn);
            object[] parameters = new object[] { numSn };

            var curBizWorkObj = await procMngService.getBizWorkInfoByBizWorkSn(parameters);

            Dictionary<string, int> dicManagement = new Dictionary<string, int>();
            Dictionary<string, int> dicProduce = new Dictionary<string, int>();
            Dictionary<string, int> dicRnd = new Dictionary<string, int>();
            Dictionary<string, int> dicSalse = new Dictionary<string, int>();

            {
                //var compMappings = curBizWork.ScCompMappings.Where(sc => sc.Status == "A");
                foreach (var compMapping in curBizWorkObj)
                {
                    int compSn = 0;
                    //문진표 작성내역 조회
                    var quesMaster = await quesMasterService.GetQuesOgranAnalysisAsync(compMapping.REGISTRATION_NO, paramModel.BizWorkYear);
                    if (quesMaster == null)
                    {
                        continue;
                    }

                    SHUSER_SboFinancialIndexT sboFinacialIndexT;

                    //int companySn = compMapping.ScCompInfo.CompSn;
                    //VcUsrInfo companyUserInfo = await scUsrService.getScUsrByCompSn(companySn);
                    //int CompSn = companyUserInfo.CompSn;

                    compSn = compMapping.COMP_SN;
                    //string UseErp = companyUserInfo.UseErp;

                    //if (UseErp == "1")
                    //{
                    //    sboFinacialIndexT = await sboFinancialIndexTService.GetSHUSER_SboFinancialIndexT(compMapping.ScCompInfo.RegistrationNo, ConfigurationManager.AppSettings["CorpCode"], ConfigurationManager.AppSettings["BizCode"], paramModel.BizWorkYear.ToString());
                    //    if (sboFinacialIndexT == null)
                    //    {
                    //        continue;
                    //    }
                    //}
                    //else
                    //{
                    var scFinancialIndexT = await scFinancialIndexTService.getScFinancialIndexTAsync(compSn, paramModel.BizWorkYear.ToString());
                    if (scFinancialIndexT == null) continue;

                    sboFinacialIndexT = ConvertMap(scFinancialIndexT);
                    //}

                    foreach (var item in quesMaster.QuesOgranAnalysis)
                    {
                        var cnt = item.ChiefCount + item.OfficerCount + item.StaffCount + item.BeginnerCount;

                        //기획관리
                        if (item.DeptCd == "M")
                        {
                            dicManagement.Add(compMapping.REGISTRATION_NO, cnt.Value);

                            if (quesMaster.QuestionSn == paramModel.QuestionSn)
                            {
                                viewModel.Management = Mapper.Map<OrgEmpCompositionViewModel>(item);
                            }
                        }
                        //생산 / 생산관리
                        else if (item.DeptCd == "P")
                        {
                            dicProduce.Add(compMapping.REGISTRATION_NO, cnt.Value);
                            if (quesMaster.QuestionSn == paramModel.QuestionSn)
                            {
                                viewModel.Produce = Mapper.Map<OrgEmpCompositionViewModel>(item);
                            }
                        }

                        //연구개발/연구지원
                        else if (item.DeptCd == "R")
                        {
                            dicRnd.Add(compMapping.REGISTRATION_NO, cnt.Value);
                            if (quesMaster.QuestionSn == paramModel.QuestionSn)
                            {
                                viewModel.RND = Mapper.Map<OrgEmpCompositionViewModel>(item);
                            }
                        }

                        //마케팅기획/판매영업
                        else if (item.DeptCd == "S")
                        {
                            dicSalse.Add(compMapping.REGISTRATION_NO, cnt.Value);
                            if (quesMaster.QuestionSn == paramModel.QuestionSn)
                            {
                                viewModel.Salse = Mapper.Map<OrgEmpCompositionViewModel>(item);
                            }
                        }
                    }
                }
            }

            viewModel.StaffSumCount = viewModel.Management.StaffCount + viewModel.Produce.StaffCount + viewModel.RND.StaffCount + viewModel.Salse.StaffCount;

            viewModel.ChiefSumCount = viewModel.Management.ChiefCount + viewModel.Produce.ChiefCount + viewModel.RND.ChiefCount + viewModel.Salse.ChiefCount;

            viewModel.OfficerSumCount = viewModel.Management.OfficerCount + viewModel.Produce.OfficerCount + viewModel.RND.OfficerCount + viewModel.Salse.OfficerCount;

            viewModel.BeginnerSumCount = viewModel.Management.BeginnerCount + viewModel.Produce.BeginnerCount + viewModel.RND.BeginnerCount + viewModel.Salse.BeginnerCount;

            viewModel.TotalSumCount = viewModel.StaffSumCount + viewModel.ChiefSumCount + viewModel.OfficerSumCount + viewModel.BeginnerSumCount;
            //평균값생성

            double companyTotalCnt = viewModel.TotalSumCount.Value;
            double bizInCompanyTotalCnt = dicProduce.Values.Sum() + dicRnd.Values.Sum() + dicSalse.Values.Sum() + dicManagement.Values.Sum();

            //기획관리 평균값 생성
            if (viewModel.TotalSumCount == 0)
            {
                viewModel.Management.Company = 0;
                viewModel.Produce.Company = 0;
                viewModel.RND.Company = 0;
                viewModel.Salse.Company = 0;
                viewModel.CompanySum = 0;
            }
            else
            {
                viewModel.Management.Company = Math.Round(viewModel.Management.PartialSum / companyTotalCnt * 100, 1);
                viewModel.Produce.Company = Math.Round(viewModel.Produce.PartialSum / companyTotalCnt * 100, 1);
                viewModel.RND.Company = Math.Round(viewModel.RND.PartialSum / companyTotalCnt * 100, 1);
                viewModel.Salse.Company = Math.Round(viewModel.Salse.PartialSum / companyTotalCnt * 100, 1);
                viewModel.CompanySum = 100;
            }

            if ((dicProduce.Values.Sum() + dicRnd.Values.Sum() + dicSalse.Values.Sum() + dicManagement.Values.Sum()) == 0)
            {
                viewModel.Management.AvgBizInCompany = 0;
                viewModel.Produce.AvgBizInCompany = 0;
                viewModel.RND.AvgBizInCompany = 0;
                viewModel.Salse.AvgBizInCompany = 0;
                viewModel.AvgBizInCompanySum = 0;
            }
            else
            {
                viewModel.Management.AvgBizInCompany = Math.Round(dicManagement.Values.Sum() / bizInCompanyTotalCnt * 100, 1);
                viewModel.Produce.AvgBizInCompany = Math.Round(dicProduce.Values.Sum() / bizInCompanyTotalCnt * 100, 1);
                viewModel.RND.AvgBizInCompany = Math.Round(dicRnd.Values.Sum() / bizInCompanyTotalCnt * 100, 1);
                viewModel.Salse.AvgBizInCompany = Math.Round(dicSalse.Values.Sum() / bizInCompanyTotalCnt * 100, 1);
                viewModel.AvgBizInCompanySum = 100;
            }

            viewModel.Management.AvgTotal = Math.Round((dicManagement.Values.Sum() + 330) / (bizInCompanyTotalCnt + 1966) * 100, 1);
            viewModel.Produce.AvgTotal = Math.Round((dicProduce.Values.Sum() + 865) / (bizInCompanyTotalCnt + 1966) * 100, 1);
            viewModel.RND.AvgTotal = Math.Round((dicRnd.Values.Sum() + 463) / (bizInCompanyTotalCnt + 1966) * 100, 1);
            viewModel.Salse.AvgTotal = Math.Round((dicSalse.Values.Sum() + 308) / (bizInCompanyTotalCnt + 1966) * 100, 1);
            viewModel.AvgTotalSum = 100;

            //검토결과 데이터 생성
            var listRptMentorComment = await rptMentorCommentService.GetRptMentorCommentListAsync(paramModel.QuestionSn, paramModel.NumSn, paramModel.BizWorkYear, "09");

            //레포트 체크리스트
            var enumRptCheckList = await rptCheckListService.GetRptCheckListBySmallClassCd("09");

            //CommentList 채우기
            var CommentList = ReportHelper.MakeCommentViewModel(enumRptCheckList, listRptMentorComment);

            viewModel.CommentList = CommentList;

            //보고서 상단 회사명 가져오기
            //var scCompMapping = await scCompMappingService.GetCompMappingAsync(paramModel.NumSn, paramModel.CompSn);
            //paramModel.CompNm = scCompMapping.ScCompInfo.CompNm;

            ViewBag.CompNm = paramModel.CompNm;
            ViewBag.paramModel = paramModel;

            methodCache["paramModel"] = paramModel;
            methodCache["viewModel"] = viewModel;
            //methodCache["CavYear"] = ViewBag.CavYear;
            //methodCache["PageNum"] = rptpagelist1[0].PageNum;
            this.HttpContext.Cache.Insert(methodCacheId, methodCache);

            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> OrgDivided(OrgHR01ViewModel viewModel, BasicSurveyReportViewModel paramModel)
        {
            string cacheId = "OrgDivided" + paramModel.QuestionSn.ToString() + "_";
            string methodCacheId = cacheId + "methodCache";

            this.HttpContext.Cache.Remove(methodCacheId);
            this.HttpContext.Cache.Remove("OverallSummary_" + paramModel.QuestionSn.ToString() + "_ViewBag");
            this.HttpContext.Cache.Remove("OverallSummary_" + paramModel.QuestionSn.ToString() + "_ViewModel");

            // NON ERP : Not target
            ViewBag.naviLeftMenu = Global.Report;

            var listRptMentorComment = await rptMentorCommentService.GetRptMentorCommentListAsync(paramModel.QuestionSn, paramModel.NumSn, paramModel.BizWorkYear, "09");

            foreach (var item in viewModel.CommentList)
            {
                var comment = listRptMentorComment.SingleOrDefault(i => i.DetailCd == item.DetailCd);
                if (comment == null)
                {
                    rptMentorCommentService.Insert(ReportHelper.MakeRptMentorcomment(item, paramModel));
                }
                else
                {
                    comment.Comment = item.Comment;
                }
            }
            await rptMentorCommentService.SaveDbContextAsync();

            if (viewModel.SubmitType == "T")
            {
                return RedirectToAction("OrgDivided", "BasicSurveyReport", new { NumSn = paramModel.NumSn, CompSn = paramModel.CompSn, BizWorkYear = paramModel.BizWorkYear, Status = paramModel.Status, QuestionSn = paramModel.QuestionSn });
            }
            else
            {
                return RedirectToAction("RndCost", "BasicSurveyReport", new { NumSn = paramModel.NumSn, CompSn = paramModel.CompSn, BizWorkYear = paramModel.BizWorkYear, Status = paramModel.Status, QuestionSn = paramModel.QuestionSn });
            }
        }

        public async Task<ActionResult> RndCost(BasicSurveyReportViewModel paramModel)
        {
            // NON ERP : Done
            int compSn = 0;

            ViewBag.naviLeftMenu = Global.Report;
            ViewBag.paramModel = paramModel;

            ReportUtil reportUtil = new ReportUtil(scBizWorkService, quesResult1Service, quesResult2Service, quesMasterService, sboFinancialIndexTService, scCavService, scCompanyFinanceService, rptMasterService, scFinancialIndexTService, procMngService);

            var viewModel1 = new RndCostViewModel();
            viewModel1.value = new CheckListViewModel();
            viewModel1.percent = new CheckListViewModel();

            var viewModel2 = new RndEmpViewModel();
            viewModel2.rndEmpRatio = new CheckListViewModel();
            viewModel2.rndEmpLevelRatio = new CheckListViewModel();

            RiskMgmtViewModel viewModel3 = new RiskMgmtViewModel();

            List<object> viewList = new List<object>();

            string cacheId = "RndCost" + paramModel.QuestionSn.ToString() + "_";
            string methodCacheId = cacheId + "methodCache";

            Boolean cacheStatus = false;
            //string cacheId = "OverallSummary_" + paramModel.QuestionSn.ToString() + "_";
            Dictionary<string, object> methodCache = (Dictionary<string, object>)this.HttpContext.Cache[methodCacheId];

            if (methodCache != null && methodCache.Count > 0)
            {
                cacheStatus = true;
            }
            else
            {
                methodCache = new Dictionary<string, object>();
            }

            if (cacheStatus == true)
            {
                ViewBag.paramModel = methodCache["paramModel"];
                //ViewBag.NullCheck = ViewBag.paramModel.NullCheck;
                //ViewBag.PageNum = methodCache["PageNum"];
                ViewBag.CompNm = ViewBag.paramModel.CompNm;
                //ViewBag.CavYear = methodCache["CavYear"];
                return View(methodCache["viewList"]);
            }

            //1) 현재 사업에 참여한 업체 평균
            //사업참여 기업들의 레벨(창업보육, 보육성장, 자립성장) 분류
            Dictionary<int, decimal> dicStartUpRnd = new Dictionary<int, decimal>();
            Dictionary<int, decimal> dicGrowthRnd = new Dictionary<int, decimal>();
            Dictionary<int, decimal> dicIndependentRnd = new Dictionary<int, decimal>();
            Dictionary<int, decimal> dicStartUpSales = new Dictionary<int, decimal>();
            Dictionary<int, decimal> dicGrowthSales = new Dictionary<int, decimal>();
            Dictionary<int, decimal> dicIndependentSales = new Dictionary<int, decimal>();


            Dictionary<int, int> dicStartUpRndEmp = new Dictionary<int, int>();
            Dictionary<int, int> dicGrowthRndEmp = new Dictionary<int, int>();
            Dictionary<int, int> dicIndependentRndEmp = new Dictionary<int, int>();
            Dictionary<int, int> dicStartUpTotalEmp = new Dictionary<int, int>();
            Dictionary<int, int> dicGrowthTotalEmp = new Dictionary<int, int>();
            Dictionary<int, int> dicIndependentTotalEmp = new Dictionary<int, int>();
            Dictionary<int, int> dicStartUpHighRnd = new Dictionary<int, int>();
            Dictionary<int, int> dicGrowthHighRnd = new Dictionary<int, int>();
            Dictionary<int, int> dicIndependentHighRnd = new Dictionary<int, int>();


            var curBizWork = await scBizWorkService.GetBizWorkByBizWorkSn(int.Parse(paramModel.NumSn));

            SqlParameter numSn = new SqlParameter("NUM_SN", paramModel.NumSn);
            object[] parameters = new object[] { numSn };

            var curBizWorkObj = await procMngService.getBizWorkInfoByBizWorkSn(parameters); // 두 loop에서 공통으로 쓰일 curBizWorkObj객체

            {
                //var compMappings = curBizWork.ScCompMappings.Where(sc => sc.Status == "A");
                foreach (var compMapping in curBizWorkObj)
                {
                    var quesMaster = await quesMasterService.GetQuesMasterAsync(compMapping.REGISTRATION_NO, paramModel.BizWorkYear);
                    if (quesMaster == null)
                    {
                        continue;
                    }

                    SHUSER_SboFinancialIndexT sboFinacialIndexT;

                    /* 연구개발 변수 */
                    double preReserchAmt = 0.0;

                    double numerator = 0.0;
                    double denominator = 0.0;

                    compSn = compMapping.COMP_SN;

                    var scFinancialIndexT = await scFinancialIndexTService.getScFinancialIndexTAsync(compSn, paramModel.BizWorkYear.ToString());
                    if (scFinancialIndexT == null) continue;

                    sboFinacialIndexT = ConvertMap(scFinancialIndexT);

                    // 연구개발
                    var rpt = rptMasterService.GetRptMasterByQuestionSn(paramModel.QuestionSn);
                    var pre = scCompanyFinanceService.getScCompanyFinance(compSn, int.Parse(sboFinacialIndexT.Year) - 1);

                    if (pre != null)
                    {
                        preReserchAmt = Convert.ToDouble(pre.CiOrdevexp + pre.CiResearch + pre.McRndexp);
                    }

                    numerator = (Convert.ToDouble(sboFinacialIndexT.ReserchAmt.Value) + preReserchAmt) / 2.0;
                    denominator = Convert.ToDouble(sboFinacialIndexT.CurrentSale.Value);

                    if (quesMaster.QuestionSn == paramModel.QuestionSn) // 해당기업
                    {
                        viewModel1.value.Company = Math.Truncate(numerator / 1000).ToString();  // **************** 연구개발비 평균이라고 구하는 부분은 없다

                        if (denominator == 0 && numerator != 0)
                        {
                            viewModel1.percent.Company = "-";
                        }
                        else
                        {
                            viewModel1.percent.Company = (denominator == 0) ? "0" : Math.Round((numerator / denominator) * 100, 1).ToString();
                        }
                    }

                    var point = await reportUtil.GetCompanyTotalPoint(quesMaster.QuestionSn, ConvertToScMap(sboFinacialIndexT));

                    if (point >= 0 && point <= 50)
                    {
                        dicStartUpRnd.Add(compMapping.COMP_SN, Convert.ToDecimal(numerator));
                        dicStartUpSales.Add(compMapping.COMP_SN, sboFinacialIndexT.CurrentSale.Value);
                    }
                    else if (point > 50 && point <= 75)
                    {
                        dicGrowthRnd.Add(compMapping.COMP_SN, Convert.ToDecimal(numerator));
                        dicGrowthSales.Add(compMapping.COMP_SN, sboFinacialIndexT.CurrentSale.Value);
                    }
                    else
                    {
                        dicIndependentRnd.Add(compMapping.COMP_SN, Convert.ToDecimal(numerator));
                        dicIndependentSales.Add(compMapping.COMP_SN, sboFinacialIndexT.CurrentSale.Value);
                    }
                }
            }

            viewModel1.value.StartUpAvg = Math.Truncate(((dicStartUpRnd.Values.Sum() + 4646684614) / (dicStartUpRnd.Count + 50)) / 1000).ToString();
            viewModel1.percent.StartUpAvg = Math.Round(((dicStartUpRnd.Values.Sum() + 4646684614) / (dicStartUpSales.Values.Sum() + 74895469963) * 100), 1).ToString();

            viewModel1.value.GrowthAvg = Math.Truncate(((dicGrowthRnd.Values.Sum() + 8987123190) / (dicGrowthRnd.Count + 46)) / 1000).ToString();
            viewModel1.percent.GrowthAvg = Math.Round(((dicGrowthRnd.Values.Sum() + 8987123190) / (dicGrowthSales.Values.Sum() + 273649178539) * 100), 1).ToString();

            viewModel1.value.IndependentAvg = Math.Truncate(((dicIndependentRnd.Values.Sum() + 2274961982) / (dicIndependentRnd.Count + 4)) / 1000).ToString();
            viewModel1.percent.IndependentAvg = Math.Round(((dicIndependentRnd.Values.Sum() + 2274961982) / (dicIndependentSales.Values.Sum() + 15298509650) * 100), 1).ToString();

            if (dicIndependentRnd.Count + dicStartUpRnd.Count + dicGrowthRnd.Count != 0)
            {
                viewModel1.value.BizInCompanyAvg = Math.Truncate(((dicIndependentRnd.Values.Sum() + dicGrowthRnd.Values.Sum() + dicStartUpRnd.Values.Sum()) / (dicIndependentRnd.Count + dicStartUpRnd.Count + dicGrowthRnd.Count)) / 1000).ToString();
            }
            else
            {
                viewModel1.value.BizInCompanyAvg = 0 + "";
            }

            if (dicIndependentSales.Values.Sum() + dicGrowthSales.Values.Sum() + dicStartUpSales.Values.Sum() != 0)
            {
                viewModel1.percent.BizInCompanyAvg = Math.Round(((dicIndependentRnd.Values.Sum() + dicGrowthRnd.Values.Sum() + dicStartUpRnd.Values.Sum()) / (dicIndependentSales.Values.Sum() + dicGrowthSales.Values.Sum() + dicStartUpSales.Values.Sum()) * 100), 1).ToString();
            }
            else
            {
                viewModel1.percent.BizInCompanyAvg = 0 + "";
            }

            viewModel1.value.TotalAvg = Math.Truncate(((dicIndependentRnd.Values.Sum() + dicGrowthRnd.Values.Sum() + dicStartUpRnd.Values.Sum() + 15908769785) / (dicIndependentRnd.Count + dicStartUpRnd.Count + dicGrowthRnd.Count + 100)) / 1000).ToString();
            viewModel1.percent.TotalAvg = Math.Round(((dicIndependentRnd.Values.Sum() + dicGrowthRnd.Values.Sum() + dicStartUpRnd.Values.Sum() + 15908769785) / (dicIndependentSales.Values.Sum() + dicGrowthSales.Values.Sum() + dicStartUpSales.Values.Sum() + 363843158152) * 100), 1).ToString();

            //검토결과 데이터 생성
            var listRptMentorComment = await rptMentorCommentService.GetRptMentorCommentListAsync(paramModel.QuestionSn, paramModel.NumSn, paramModel.BizWorkYear, "10");

            //레포트 체크리스트
            var enumRptCheckList = await rptCheckListService.GetRptCheckListBySmallClassCd("10");

            //CommentList 채우기
            var CommentList = ReportHelper.MakeCommentViewModel(enumRptCheckList, listRptMentorComment);

            viewModel1.CommentList = CommentList;
            // RndCost 종료...


            // RndEmp 구하기 ---------------------------------------------------------------------------------------------
            {
                //var compMappings = curBizWork.ScCompMappings.Where(sc => sc.Status == "A");
                foreach (var compMapping in curBizWorkObj)
                {
                    var quesMaster = await quesMasterService.GetQuesMasterAsync(compMapping.REGISTRATION_NO, paramModel.BizWorkYear);
                    if (quesMaster == null)
                    {
                        continue;
                    }

                    SHUSER_SboFinancialIndexT sboFinacialIndexT;
                    //int companySn = compMapping.ScCompInfo.CompSn;
                    //VcUsrInfo companyUserInfo = await scUsrService.getScUsrByCompSn(companySn);
                    //int CompSn = companyUserInfo.CompSn;
                    //string UseErp = companyUserInfo.UseErp;

                    //if (UseErp == "1")
                    //{
                    //    sboFinacialIndexT = await sboFinancialIndexTService.GetSHUSER_SboFinancialIndexT(compMapping.ScCompInfo.RegistrationNo, ConfigurationManager.AppSettings["CorpCode"], ConfigurationManager.AppSettings["BizCode"], paramModel.BizWorkYear.ToString());
                    //    if (sboFinacialIndexT == null)
                    //    {
                    //        continue;
                    //    }
                    //}
                    //else
                    //{
                    var scFinancialIndexT = await scFinancialIndexTService.getScFinancialIndexTAsync(compMapping.COMP_SN, paramModel.BizWorkYear.ToString());
                    if (scFinancialIndexT == null) continue;

                    sboFinacialIndexT = ConvertMap(scFinancialIndexT);
                    //}

                    //연구개발 인력의 비율
                    var quesResult2sEmpRate = await quesResult2Service.GetQuesResult2sAsync(quesMaster.QuestionSn, "A1B102");
                    //전체임직원수
                    var TotalEmp = quesResult2sEmpRate.SingleOrDefault(i => i.QuesCheckList.DetailCd == "A1B10202");
                    //연구개발인력
                    var RndEmp = quesResult2sEmpRate.SingleOrDefault(i => i.QuesCheckList.DetailCd == "A1B10201");

                    //연구개발 인력의 능력
                    var quesResult2sEmpCapa = await quesResult2Service.GetQuesResult2sAsync(quesMaster.QuestionSn, "A1B103");
                    //박사급
                    var DoctorEmp = quesResult2sEmpCapa.SingleOrDefault(i => i.QuesCheckList.DetailCd == "A1B10301");
                    //석사급
                    var MasterEmp = quesResult2sEmpCapa.SingleOrDefault(i => i.QuesCheckList.DetailCd == "A1B10302");

                    TotalEmp.D = (int.Parse(TotalEmp.D) + 1) + "";  // 0419 현재 대표자를 포함시키는 1 값이 추가되어 있지 않으므로, 계산시 직접 1을 더해준다

                    if (quesMaster.QuestionSn == paramModel.QuestionSn)
                    {
                        // 연구개발 인력의 비율 0226 수정 
                        if (TotalEmp.D == "0") // 0419 -> 위의 대표자 1명은 무조건 총 인원수에 포함되므로 사실상 필요 없는 분기가 된다
                        {
                            viewModel2.rndEmpRatio.Company = "-";
                        }
                        else
                        {
                            viewModel2.rndEmpRatio.Company = Math.Round((double.Parse(RndEmp.D) / double.Parse(TotalEmp.D) * 100), 1).ToString();

                            if (double.IsNaN(double.Parse(viewModel2.rndEmpRatio.Company)))
                            {
                                viewModel2.rndEmpRatio.Company = "-";
                            }
                        }
                        // 연구개발 인력 중 석사급 이상 비율
                        if (RndEmp.D != "0")    // 연구개발 인력이 0이 아닌지 검사
                        {
                            if (int.Parse(MasterEmp.D) != 0 || int.Parse(DoctorEmp.D) != 0) // 석사 또는 박사의 숫자가 0이 아닌지 검사
                            {
                                if (int.Parse(RndEmp.D) == 0) // 그런데 전체 인원 수는 0이다
                                {
                                    viewModel2.rndEmpLevelRatio.Company = "-";   // 즉 말이 안되므로 - 표시
                                }
                                else // 계산이 가능하다, 그렇다면 계산
                                {
                                    viewModel2.rndEmpLevelRatio.Company = Math.Round(((double.Parse(DoctorEmp.D) + double.Parse(MasterEmp.D)) / double.Parse(RndEmp.D) * 100), 1).ToString();
                                    if (double.IsNaN(double.Parse(viewModel2.rndEmpLevelRatio.Company))) // 그런데 혹시나 나누어진 값이 Nan이면 - 
                                    {
                                        viewModel2.rndEmpLevelRatio.Company = "-";
                                    }
                                }
                            }
                            else
                            {
                                viewModel2.rndEmpLevelRatio.Company = "0";
                            }
                        }
                        else // 전체 연구개발인력수가 0이다
                        {
                            viewModel2.rndEmpLevelRatio.Company = "0";
                        }
                    }

                    //종합점수 조회하여 분류별로 딕셔너리 저장
                    var point = await reportUtil.GetCompanyTotalPoint(quesMaster.QuestionSn, ConvertToScMap(sboFinacialIndexT));

                    if (point >= 0 && point <= 50)
                    {
                        dicStartUpRndEmp.Add(compMapping.COMP_SN, int.Parse(RndEmp.D));
                        dicStartUpTotalEmp.Add(compMapping.COMP_SN, int.Parse(TotalEmp.D));
                        dicStartUpHighRnd.Add(compMapping.COMP_SN, int.Parse(DoctorEmp.D) + int.Parse(MasterEmp.D));
                    }
                    else if (point > 50 && point <= 75)
                    {
                        dicGrowthRndEmp.Add(compMapping.COMP_SN, int.Parse(RndEmp.D));
                        dicGrowthTotalEmp.Add(compMapping.COMP_SN, int.Parse(TotalEmp.D));
                        dicGrowthHighRnd.Add(compMapping.COMP_SN, int.Parse(DoctorEmp.D) + int.Parse(MasterEmp.D));
                    }
                    else
                    {
                        dicIndependentRndEmp.Add(compMapping.COMP_SN, int.Parse(RndEmp.D));
                        dicIndependentTotalEmp.Add(compMapping.COMP_SN, int.Parse(TotalEmp.D));
                        dicIndependentHighRnd.Add(compMapping.COMP_SN, int.Parse(DoctorEmp.D) + int.Parse(MasterEmp.D));
                    }
                }
            }

            viewModel2.rndEmpRatio.StartUpAvg = Math.Round(((dicStartUpRndEmp.Values.Sum() + 152.0) / (dicStartUpTotalEmp.Values.Sum() + 623) * 100), 1).ToString();
            viewModel2.rndEmpLevelRatio.StartUpAvg = Math.Round(((dicStartUpHighRnd.Values.Sum() + 77.0) / (dicStartUpRndEmp.Values.Sum() + 152) * 100), 1).ToString();

            viewModel2.rndEmpRatio.GrowthAvg = Math.Round(((dicGrowthRndEmp.Values.Sum() + 244.0) / (dicGrowthTotalEmp.Values.Sum() + 1143) * 100), 1).ToString();
            viewModel2.rndEmpLevelRatio.GrowthAvg = Math.Round(((dicGrowthHighRnd.Values.Sum() + 132.0) / (dicGrowthRndEmp.Values.Sum() + 244) * 100), 1).ToString();

            viewModel2.rndEmpRatio.IndependentAvg = Math.Round(((dicIndependentRndEmp.Values.Sum() + 39.0) / (dicIndependentTotalEmp.Values.Sum() + 117) * 100), 1).ToString();
            viewModel2.rndEmpLevelRatio.IndependentAvg = Math.Round(((dicIndependentHighRnd.Values.Sum() + 22.0) / (dicIndependentRndEmp.Values.Sum() + 39) * 100), 1).ToString();


            if (dicStartUpTotalEmp.Values.Sum() + dicGrowthTotalEmp.Values.Sum() + dicIndependentTotalEmp.Values.Sum() == 0)
            {
                viewModel2.rndEmpRatio.BizInCompanyAvg = "0";
            }
            else
            {
                viewModel2.rndEmpRatio.BizInCompanyAvg = Math.Round(((dicStartUpRndEmp.Values.Sum() + dicGrowthRndEmp.Values.Sum() + dicIndependentRndEmp.Values.Sum() + 0.0) / (dicStartUpTotalEmp.Values.Sum() + dicGrowthTotalEmp.Values.Sum() + dicIndependentTotalEmp.Values.Sum()) * 100), 1).ToString();
            }

            if (dicStartUpRndEmp.Values.Sum() + dicGrowthRndEmp.Values.Sum() + dicIndependentRndEmp.Values.Sum() == 0)
            {
                viewModel2.rndEmpLevelRatio.BizInCompanyAvg = "0";
            }
            else
            {
                viewModel2.rndEmpLevelRatio.BizInCompanyAvg = Math.Round(((dicStartUpHighRnd.Values.Sum() + dicGrowthHighRnd.Values.Sum() + dicIndependentHighRnd.Values.Sum() + 0.0) / (dicStartUpRndEmp.Values.Sum() + dicGrowthRndEmp.Values.Sum() + dicIndependentRndEmp.Values.Sum()) * 100), 1).ToString();
            }

            viewModel2.rndEmpRatio.TotalAvg = Math.Round(((dicStartUpRndEmp.Values.Sum() + dicGrowthRndEmp.Values.Sum() + dicIndependentRndEmp.Values.Sum() + 435.0) / (dicStartUpTotalEmp.Values.Sum() + dicGrowthTotalEmp.Values.Sum() + dicIndependentTotalEmp.Values.Sum() + 1883) * 100), 1).ToString();
            viewModel2.rndEmpLevelRatio.TotalAvg = Math.Round(((dicStartUpHighRnd.Values.Sum() + dicGrowthHighRnd.Values.Sum() + dicIndependentHighRnd.Values.Sum() + 231.0) / (dicStartUpRndEmp.Values.Sum() + dicGrowthRndEmp.Values.Sum() + dicIndependentRndEmp.Values.Sum() + 435) * 100), 1).ToString();

            int workYear = paramModel.BizWorkYear - 1;

            var regCav = await scCavService.getMaxYear();
            var regCavDetail = scCavService.GetCavAsync(regCav);
            var pointCavSmc = regCavDetail.CavSmc;

            viewModel2.rndEmpRatio.SMCompanyAvg = Decimal.Round(pointCavSmc, 1); //중소기업평균 연구개발인력의 비율

            //검토결과 데이터 생성
            var listRptMentorComment2 = await rptMentorCommentService.GetRptMentorCommentListAsync(paramModel.QuestionSn, paramModel.NumSn, paramModel.BizWorkYear, "11");

            //레포트 체크리스트
            var enumRptCheckList2 = await rptCheckListService.GetRptCheckListBySmallClassCd("11");

            //CommentList 채우기
            var CommentList2 = ReportHelper.MakeCommentViewModel(enumRptCheckList, listRptMentorComment);

            viewModel2.CommentList = CommentList;
            // RndEmp 종료...




            //
            viewModel3.CheckList = await reportUtil.getGrowthStepPointCheckList(paramModel, "A1B104");

            //검토결과 데이터 생성
            var listRptMentorComment3 = await rptMentorCommentService.GetRptMentorCommentListAsync(paramModel.QuestionSn, paramModel.NumSn, paramModel.BizWorkYear, "12");

            //레포트 체크리스트
            var enumRptCheckList3 = await rptCheckListService.GetRptCheckListBySmallClassCd("12");

            //CommentList 채우기
            var CommentList3 = ReportHelper.MakeCommentViewModel(enumRptCheckList3, listRptMentorComment3);

            viewModel3.CommentList = CommentList3;
            // ..........


            viewList.Add(viewModel1);
            viewList.Add(viewModel2);
            viewList.Add(viewModel3);

            ViewBag.CompNm = paramModel.CompNm;
            ViewBag.paramModel = paramModel;

            // 캐시 저장 부분

            methodCache["paramModel"] = paramModel;
            methodCache["viewList"] = viewList;
            //methodCache["scCompMapping"] = scCompMapping;
            //methodCache["CavYear"] = ViewBag.CavYear;
            //methodCache["PageNum"] = rptpagelist1[0].PageNum;
            this.HttpContext.Cache.Insert(methodCacheId, methodCache);

            return View(viewList);
        }

        [HttpPost]
        public async Task<ActionResult> RndCost(OrgHR01ViewModel viewModel, BasicSurveyReportViewModel paramModel)
        {
            string cacheId = "RndCost" + paramModel.QuestionSn.ToString() + "_";
            string methodCacheId = cacheId + "methodCache";

            this.HttpContext.Cache.Remove(methodCacheId);

            ViewBag.naviLeftMenu = Global.Report;

            var listRptMentorComment = await rptMentorCommentService.GetRptMentorCommentListAsync(paramModel.QuestionSn, paramModel.NumSn, paramModel.BizWorkYear, "10");

            foreach (var item in viewModel.CommentList)
            {
                var comment = listRptMentorComment.SingleOrDefault(i => i.DetailCd == item.DetailCd);
                if (comment == null)
                {
                    rptMentorCommentService.Insert(ReportHelper.MakeRptMentorcomment(item, paramModel));
                }
                else
                {
                    comment.Comment = item.Comment;
                }
            }
            await rptMentorCommentService.SaveDbContextAsync();

            if (viewModel.SubmitType == "T")
            {
                return RedirectToAction("RndCost", "BasicSurveyReport", new { NumSn = paramModel.NumSn, CompSn = paramModel.CompSn, BizWorkYear = paramModel.BizWorkYear, Status = paramModel.Status, QuestionSn = paramModel.QuestionSn });
            }
            else
            {
                return RedirectToAction("ProductivityResult", "BasicSurveyReport", new { NumSn = paramModel.NumSn, CompSn = paramModel.CompSn, BizWorkYear = paramModel.BizWorkYear, Status = paramModel.Status, QuestionSn = paramModel.QuestionSn });
            }
        }

        public async Task<ActionResult> RndEmp(BasicSurveyReportViewModel paramModel)
        {
            // NON ERP : Done
            ViewBag.naviLeftMenu = Global.Report;
            ViewBag.paramModel = paramModel;

            ReportUtil reportUtil = new ReportUtil(scBizWorkService, quesResult1Service, quesResult2Service, quesMasterService, sboFinancialIndexTService, scCavService, scCompanyFinanceService, rptMasterService, scFinancialIndexTService, procMngService);

            var viewModel = new RndEmpViewModel();
            viewModel.rndEmpRatio = new CheckListViewModel();
            viewModel.rndEmpLevelRatio = new CheckListViewModel();

            //1) 현재 사업에 참여한 업체 평균
            //사업참여 기업들의 레벨(창업보육, 보육성장, 자립성장) 분류
            Dictionary<int, int> dicStartUpRndEmp = new Dictionary<int, int>();
            Dictionary<int, int> dicGrowthRndEmp = new Dictionary<int, int>();
            Dictionary<int, int> dicIndependentRndEmp = new Dictionary<int, int>();
            Dictionary<int, int> dicStartUpTotalEmp = new Dictionary<int, int>();
            Dictionary<int, int> dicGrowthTotalEmp = new Dictionary<int, int>();
            Dictionary<int, int> dicIndependentTotalEmp = new Dictionary<int, int>();
            Dictionary<int, int> dicStartUpHighRnd = new Dictionary<int, int>();
            Dictionary<int, int> dicGrowthHighRnd = new Dictionary<int, int>();
            Dictionary<int, int> dicIndependentHighRnd = new Dictionary<int, int>();

            var curBizWork = await scBizWorkService.GetBizWorkByBizWorkSn(int.Parse(paramModel.NumSn));
            {
                //var compMappings = curBizWork.ScCompMappings.Where(sc => sc.Status == "A");
                //foreach (var compMapping in compMappings)
                //{
                //    var quesMaster = await quesMasterService.GetQuesMasterAsync(compMapping.ScCompInfo.RegistrationNo, paramModel.BizWorkYear);
                //    if (quesMaster == null)
                //    {
                //        continue;
                //    }

                //    SHUSER_SboFinancialIndexT sboFinacialIndexT;
                //    int companySn = compMapping.ScCompInfo.CompSn;
                //    VcUsrInfo companyUserInfo = await scUsrService.getScUsrByCompSn(companySn);
                //    int CompSn = companyUserInfo.CompSn;
                //    //string UseErp = companyUserInfo.UseErp;

                //    //if (UseErp == "1")
                //    //{
                //    //    sboFinacialIndexT = await sboFinancialIndexTService.GetSHUSER_SboFinancialIndexT(compMapping.ScCompInfo.RegistrationNo, ConfigurationManager.AppSettings["CorpCode"], ConfigurationManager.AppSettings["BizCode"], paramModel.BizWorkYear.ToString());
                //    //    if (sboFinacialIndexT == null)
                //    //    {
                //    //        continue;
                //    //    }
                //    //}
                //    //else
                //    //{
                //        var scFinancialIndexT = await scFinancialIndexTService.getScFinancialIndexTAsync(CompSn, paramModel.BizWorkYear.ToString());
                //        if (scFinancialIndexT == null) continue;

                //        sboFinacialIndexT = ConvertMap(scFinancialIndexT);
                //    //}

                //    //연구개발 인력의 비율
                //    var quesResult2sEmpRate = await quesResult2Service.GetQuesResult2sAsync(quesMaster.QuestionSn, "A1B102");
                //    //전체임직원수
                //    var TotalEmp = quesResult2sEmpRate.SingleOrDefault(i => i.QuesCheckList.DetailCd == "A1B10202");
                //    //연구개발인력
                //    var RndEmp = quesResult2sEmpRate.SingleOrDefault(i => i.QuesCheckList.DetailCd == "A1B10201");

                //    //연구개발 인력의 능력
                //    var quesResult2sEmpCapa = await quesResult2Service.GetQuesResult2sAsync(quesMaster.QuestionSn, "A1B103");
                //    //박사급
                //    var DoctorEmp = quesResult2sEmpCapa.SingleOrDefault(i => i.QuesCheckList.DetailCd == "A1B10301");
                //    //석사급
                //    var MasterEmp = quesResult2sEmpCapa.SingleOrDefault(i => i.QuesCheckList.DetailCd == "A1B10302");

                //    TotalEmp.D = (int.Parse(TotalEmp.D) + 1) + "";  // 0419 현재 대표자를 포함시키는 1 값이 추가되어 있지 않으므로, 계산시 직접 1을 더해준다

                //    if (quesMaster.QuestionSn == paramModel.QuestionSn)
                //    {
                //        // 연구개발 인력의 비율 0226 수정 
                //        if (TotalEmp.D == "0") // 0419 -> 위의 대표자 1명은 무조건 총 인원수에 포함되므로 사실상 필요 없는 분기가 된다
                //        {
                //            viewModel.rndEmpRatio.Company = "-";
                //        }
                //        else
                //        {
                //            viewModel.rndEmpRatio.Company = Math.Round((double.Parse(RndEmp.D) / double.Parse(TotalEmp.D) * 100), 1).ToString();

                //            if (double.IsNaN(double.Parse(viewModel.rndEmpRatio.Company)))
                //            {
                //                viewModel.rndEmpRatio.Company = "-";
                //            }
                //        }
                //        // 연구개발 인력 중 석사급 이상 비율
                //        if (RndEmp.D != "0")    // 연구개발 인력이 0이 아닌지 검사
                //        {
                //            if (int.Parse(MasterEmp.D) != 0 || int.Parse(DoctorEmp.D) != 0) // 석사 또는 박사의 숫자가 0이 아닌지 검사
                //            {
                //                if (int.Parse(RndEmp.D) == 0) // 그런데 전체 인원 수는 0이다
                //                {
                //                    viewModel.rndEmpLevelRatio.Company = "-";   // 즉 말이 안되므로 - 표시
                //                }
                //                else // 계산이 가능하다, 그렇다면 계산
                //                {
                //                    viewModel.rndEmpLevelRatio.Company = Math.Round(((double.Parse(DoctorEmp.D) + double.Parse(MasterEmp.D)) / double.Parse(RndEmp.D) * 100), 1).ToString();
                //                    if (double.IsNaN(double.Parse(viewModel.rndEmpLevelRatio.Company))) // 그런데 혹시나 나누어진 값이 Nan이면 - 
                //                    {
                //                        viewModel.rndEmpLevelRatio.Company = "-";
                //                    }
                //                }
                //            }
                //            else
                //            {
                //                viewModel.rndEmpLevelRatio.Company = "0";
                //            }
                //        }
                //        else // 전체 연구개발인력수가 0이다
                //        {
                //            viewModel.rndEmpLevelRatio.Company = "0";
                //        }
                //    }

                //    //종합점수 조회하여 분류별로 딕셔너리 저장
                //    var point = await reportUtil.GetCompanyTotalPoint(quesMaster.QuestionSn, ConvertToScMap(sboFinacialIndexT));

                //    if (point >= 0 && point <= 50)
                //    {
                //        dicStartUpRndEmp.Add(compMapping.CompSn, int.Parse(RndEmp.D));
                //        dicStartUpTotalEmp.Add(compMapping.CompSn, int.Parse(TotalEmp.D));
                //        dicStartUpHighRnd.Add(compMapping.CompSn, int.Parse(DoctorEmp.D) + int.Parse(MasterEmp.D));
                //    }
                //    else if (point > 50 && point <= 75)
                //    {
                //        dicGrowthRndEmp.Add(compMapping.CompSn, int.Parse(RndEmp.D));
                //        dicGrowthTotalEmp.Add(compMapping.CompSn, int.Parse(TotalEmp.D));
                //        dicGrowthHighRnd.Add(compMapping.CompSn, int.Parse(DoctorEmp.D) + int.Parse(MasterEmp.D));
                //    }
                //    else
                //    {
                //        dicIndependentRndEmp.Add(compMapping.CompSn, int.Parse(RndEmp.D));
                //        dicIndependentTotalEmp.Add(compMapping.CompSn, int.Parse(TotalEmp.D));
                //        dicIndependentHighRnd.Add(compMapping.CompSn, int.Parse(DoctorEmp.D) + int.Parse(MasterEmp.D));
                //    }
                //}
            }


            viewModel.rndEmpRatio.StartUpAvg = Math.Round(((dicStartUpRndEmp.Values.Sum() + 152.0) / (dicStartUpTotalEmp.Values.Sum() + 623) * 100), 1).ToString();
            viewModel.rndEmpLevelRatio.StartUpAvg = Math.Round(((dicStartUpHighRnd.Values.Sum() + 77.0) / (dicStartUpRndEmp.Values.Sum() + 152) * 100), 1).ToString();

            viewModel.rndEmpRatio.GrowthAvg = Math.Round(((dicGrowthRndEmp.Values.Sum() + 244.0) / (dicGrowthTotalEmp.Values.Sum() + 1143) * 100), 1).ToString();
            viewModel.rndEmpLevelRatio.GrowthAvg = Math.Round(((dicGrowthHighRnd.Values.Sum() + 132.0) / (dicGrowthRndEmp.Values.Sum() + 244) * 100), 1).ToString();

            viewModel.rndEmpRatio.IndependentAvg = Math.Round(((dicIndependentRndEmp.Values.Sum() + 39.0) / (dicIndependentTotalEmp.Values.Sum() + 117) * 100), 1).ToString();
            viewModel.rndEmpLevelRatio.IndependentAvg = Math.Round(((dicIndependentHighRnd.Values.Sum() + 22.0) / (dicIndependentRndEmp.Values.Sum() + 39) * 100), 1).ToString();


            if (dicStartUpTotalEmp.Values.Sum() + dicGrowthTotalEmp.Values.Sum() + dicIndependentTotalEmp.Values.Sum() == 0)
            {
                viewModel.rndEmpRatio.BizInCompanyAvg = "0";
            }
            else
            {
                viewModel.rndEmpRatio.BizInCompanyAvg = Math.Round(((dicStartUpRndEmp.Values.Sum() + dicGrowthRndEmp.Values.Sum() + dicIndependentRndEmp.Values.Sum() + 0.0) / (dicStartUpTotalEmp.Values.Sum() + dicGrowthTotalEmp.Values.Sum() + dicIndependentTotalEmp.Values.Sum()) * 100), 1).ToString();
            }

            if (dicStartUpRndEmp.Values.Sum() + dicGrowthRndEmp.Values.Sum() + dicIndependentRndEmp.Values.Sum() == 0)
            {
                viewModel.rndEmpLevelRatio.BizInCompanyAvg = "0";
            }
            else
            {
                viewModel.rndEmpLevelRatio.BizInCompanyAvg = Math.Round(((dicStartUpHighRnd.Values.Sum() + dicGrowthHighRnd.Values.Sum() + dicIndependentHighRnd.Values.Sum() + 0.0) / (dicStartUpRndEmp.Values.Sum() + dicGrowthRndEmp.Values.Sum() + dicIndependentRndEmp.Values.Sum()) * 100), 1).ToString();
            }

            viewModel.rndEmpRatio.TotalAvg = Math.Round(((dicStartUpRndEmp.Values.Sum() + dicGrowthRndEmp.Values.Sum() + dicIndependentRndEmp.Values.Sum() + 435.0) / (dicStartUpTotalEmp.Values.Sum() + dicGrowthTotalEmp.Values.Sum() + dicIndependentTotalEmp.Values.Sum() + 1883) * 100), 1).ToString();
            viewModel.rndEmpLevelRatio.TotalAvg = Math.Round(((dicStartUpHighRnd.Values.Sum() + dicGrowthHighRnd.Values.Sum() + dicIndependentHighRnd.Values.Sum() + 231.0) / (dicStartUpRndEmp.Values.Sum() + dicGrowthRndEmp.Values.Sum() + dicIndependentRndEmp.Values.Sum() + 435) * 100), 1).ToString();

            int workYear = paramModel.BizWorkYear - 1;

            var regCav = await scCavService.getMaxYear();
            var regCavDetail = scCavService.GetCavAsync(regCav);
            var pointCavSmc = regCavDetail.CavSmc;

            viewModel.rndEmpRatio.SMCompanyAvg = Decimal.Round(pointCavSmc, 1); //중소기업평균 연구개발인력의 비율

            //검토결과 데이터 생성
            var listRptMentorComment = await rptMentorCommentService.GetRptMentorCommentListAsync(paramModel.QuestionSn, paramModel.NumSn, paramModel.BizWorkYear, "11");

            //레포트 체크리스트
            var enumRptCheckList = await rptCheckListService.GetRptCheckListBySmallClassCd("11");

            //CommentList 채우기
            var CommentList = ReportHelper.MakeCommentViewModel(enumRptCheckList, listRptMentorComment);

            viewModel.CommentList = CommentList;
            //보고서 상단 회사명 가져오기
            //var scCompMapping = await scCompMappingService.GetCompMappingAsync(paramModel.NumSn, paramModel.CompSn);
            //paramModel.CompNm = scCompMapping.ScCompInfo.CompNm;

            ViewBag.CompNm = paramModel.CompNm;

            ViewBag.paramModel = paramModel;
            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> RndEmp(OrgHR01ViewModel viewModel, BasicSurveyReportViewModel paramModel)
        {
            // NON ERP : Not target
            ViewBag.naviLeftMenu = Global.Report;

            var listRptMentorComment = await rptMentorCommentService.GetRptMentorCommentListAsync(paramModel.QuestionSn, paramModel.NumSn, paramModel.BizWorkYear, "11");

            foreach (var item in viewModel.CommentList)
            {
                var comment = listRptMentorComment.SingleOrDefault(i => i.DetailCd == item.DetailCd);
                if (comment == null)
                {
                    rptMentorCommentService.Insert(ReportHelper.MakeRptMentorcomment(item, paramModel));
                }
                else
                {
                    comment.Comment = item.Comment;
                }
            }
            await rptMentorCommentService.SaveDbContextAsync();

            if (viewModel.SubmitType == "T")
            {
                return RedirectToAction("RndEmp", "BasicSurveyReport", new { NumSn = paramModel.NumSn, CompSn = paramModel.CompSn, BizWorkYear = paramModel.BizWorkYear, Status = paramModel.Status, QuestionSn = paramModel.QuestionSn });
            }
            else
            {
                return RedirectToAction("ProductivityCommercialize", "BasicSurveyReport", new { NumSn = paramModel.NumSn, CompSn = paramModel.CompSn, BizWorkYear = paramModel.BizWorkYear, Status = paramModel.Status, QuestionSn = paramModel.QuestionSn });
            }
        }

        public async Task<ActionResult> ProductivityResult(BasicSurveyReportViewModel paramModel)
        {
            ViewBag.naviLeftMenu = Global.Report;
            ViewBag.paramModel = paramModel;

            string cacheId = "ProductivityResult" + paramModel.QuestionSn.ToString() + "_";
            string methodCacheId = cacheId + "methodCache";

            Boolean cacheStatus = false;
            Dictionary<string, object> methodCache = (Dictionary<string, object>)this.HttpContext.Cache[methodCacheId];

            if (methodCache != null && methodCache.Count > 0)
            {
                cacheStatus = true;
            }
            else
            {
                methodCache = new Dictionary<string, object>();
            }

            if (cacheStatus == true)
            {
                ViewBag.paramModel = methodCache["paramModel"];
                //ViewBag.NullCheck = ViewBag.paramModel.NullCheck;
                //ViewBag.PageNum = methodCache["PageNum"];
                ViewBag.CompNm = ViewBag.paramModel.CompNm;
                //ViewBag.CavYear = methodCache["CavYear"];
                return View(methodCache["viewList"]);   // 사전에 저장됐던 viewList로 view 생성
            }

            ReportUtil reportUtil = new ReportUtil(scBizWorkService, quesResult1Service, quesResult2Service, quesMasterService, sboFinancialIndexTService, scCavService, scCompanyFinanceService, rptMasterService, scFinancialIndexTService, procMngService);
            LocalReportUtil localReportUtil = new LocalReportUtil(scBizWorkService, quesResult1Service, quesResult2Service, quesMasterService, sboFinancialIndexTService, scFinancialIndexTService, scCavService, scUsrService, scCompanyFinanceService, rptMasterService, procMngService);

            var viewModel1 = new ProductivityResultViewModel();
            viewModel1.BizResultCnt = new CheckListViewModel();
            viewModel1.BizResultPoint = new CheckListViewModel();

            RiskMgmtViewModel viewModel2 = new RiskMgmtViewModel();

            RiskMgmtViewModel viewModel3 = new RiskMgmtViewModel();

            List<object> viewList = new List<object>();

            //1) 현재 사업에 참여한 업체 평균
            //사업참여 기업들의 레벨(창업보육, 보육성장, 자립성장) 분류
            Dictionary<int, int> dicStartUpCnt = new Dictionary<int, int>();
            Dictionary<int, int> dicGrowthCnt = new Dictionary<int, int>();
            Dictionary<int, int> dicIndependentCnt = new Dictionary<int, int>();
            Dictionary<int, double> dicStartUpPoint = new Dictionary<int, double>();
            Dictionary<int, double> dicGrowthPoint = new Dictionary<int, double>();
            Dictionary<int, double> dicIndependentPoint = new Dictionary<int, double>();


            SqlParameter numSn = new SqlParameter("NUM_SN", paramModel.NumSn);
            object[] parameters = new object[] { numSn };

            var curBizWorkObj = await procMngService.getBizWorkInfoByBizWorkSn(parameters);


            var curBizWork = await scBizWorkService.GetBizWorkByBizWorkSn(int.Parse(paramModel.NumSn));
            {
                //var compMappings = curBizWork.ScCompMappings.Where(sc => sc.Status == "A");
                foreach (var compMapping in curBizWorkObj)
                {
                    var quesMaster = await quesMasterService.GetQuesMasterAsync(compMapping.REGISTRATION_NO, paramModel.BizWorkYear);
                    if (quesMaster == null)
                    {
                        continue;
                    }

                    //다래 재무정보 유무 체크하는 로직 추가해야함.(문진표정보, 재무정보가 있어야 보고서 생성가능.)
                    //다래 재무정보 조회해야 함.
                    SHUSER_SboFinancialIndexT sboFinacialIndexT;
                    //int companySn = compMapping.ScCompInfo.CompSn;
                    //VcUsrInfo companyUserInfo = await scUsrService.getScUsrByCompSn(companySn);
                    //int CompSn = companyUserInfo.CompSn;
                    //string UseErp = companyUserInfo.UseErp;

                    //if (UseErp == "1")
                    //{
                    //    sboFinacialIndexT = await sboFinancialIndexTService.GetSHUSER_SboFinancialIndexT(compMapping.ScCompInfo.RegistrationNo, ConfigurationManager.AppSettings["CorpCode"], ConfigurationManager.AppSettings["BizCode"], paramModel.BizWorkYear.ToString());
                    //    if (sboFinacialIndexT == null)
                    //    {
                    //        continue;
                    //    }
                    //}
                    //else
                    //{
                    var scFinancialIndexT = await scFinancialIndexTService.getScFinancialIndexTAsync(compMapping.COMP_SN, paramModel.BizWorkYear.ToString());
                    if (scFinancialIndexT == null) continue;

                    sboFinacialIndexT = ConvertMap(scFinancialIndexT);
                    //}

                    // A1B105 : 사업화실적
                    var quesResult2s = await quesResult2Service.GetQuesResult2sAsync(quesMaster.QuestionSn, "A1B105");
                    var BizResultCnt = quesResult2s.SingleOrDefault(i => i.QuesCheckList.DetailCd == "A1B10502");

                    if (quesMaster.QuestionSn == paramModel.QuestionSn)
                    {
                        viewModel1.BizResultCnt.Company = (int.Parse(BizResultCnt.D) + int.Parse(BizResultCnt.D451) + int.Parse(BizResultCnt.D452)).ToString();

                        double avg = (int.Parse(BizResultCnt.D) + int.Parse(BizResultCnt.D451) + int.Parse(BizResultCnt.D452)) / 3.0;
                        viewModel1.BizResultPoint.Company = Math.Round(ReportHelper.CalcPoint(ReportHelper.GetCodeTypeE(avg), 4), 1).ToString();
                    }


                    //종합점수 조회하여 분류별로 딕셔너리 저장
                    var point = await reportUtil.GetCompanyTotalPoint(quesMaster.QuestionSn, ConvertToScMap(sboFinacialIndexT));

                    if (point >= 0 && point <= 50)
                    {
                        dicStartUpCnt.Add(compMapping.COMP_SN, (int.Parse(BizResultCnt.D) + int.Parse(BizResultCnt.D451) + int.Parse(BizResultCnt.D452)));
                    }
                    else if (point > 50 && point <= 75)
                    {
                        dicGrowthCnt.Add(compMapping.COMP_SN, (int.Parse(BizResultCnt.D) + int.Parse(BizResultCnt.D451) + int.Parse(BizResultCnt.D452)));
                    }
                    else
                    {
                        dicIndependentCnt.Add(compMapping.COMP_SN, (int.Parse(BizResultCnt.D) + int.Parse(BizResultCnt.D451) + int.Parse(BizResultCnt.D452)));
                    }
                }
            }

            //참업보육 평균
            viewModel1.BizResultCnt.StartUpAvg = Math.Round((dicStartUpCnt.Values.Sum() + 146) / (dicStartUpCnt.Count + 50.0), 1).ToString();
            double startUpSum = 0;
            foreach (var item in dicStartUpCnt.Values)
            {
                startUpSum = startUpSum + ReportHelper.CalcPoint(ReportHelper.GetCodeTypeE(item / 3.0), 4);
            }
            viewModel1.BizResultPoint.StartUpAvg = Math.Round((startUpSum + 11) / (dicStartUpCnt.Count + 50.0), 1).ToString();
            //보육성장 평균
            viewModel1.BizResultCnt.GrowthAvg = Math.Round((dicGrowthCnt.Values.Sum() + 169) / (dicGrowthCnt.Count + 46.0), 1).ToString();
            double growthSum = 0;
            foreach (var item in dicGrowthCnt.Values)
            {
                growthSum = growthSum + ReportHelper.CalcPoint(ReportHelper.GetCodeTypeE(item / 3.0), 4);
            }
            viewModel1.BizResultPoint.GrowthAvg = Math.Round((growthSum + 16) / (dicGrowthCnt.Count + 46.0), 1).ToString();
            //자립성장 평균
            viewModel1.BizResultCnt.IndependentAvg = Math.Round((dicIndependentCnt.Values.Sum() + 28) / (dicIndependentCnt.Count + 4.0), 1).ToString();

            double independentSum = 0;

            foreach (var item in dicIndependentCnt.Values)
            {
                independentSum = independentSum + ReportHelper.CalcPoint(ReportHelper.GetCodeTypeE(item / 3.0), 4);
            }

            viewModel1.BizResultPoint.IndependentAvg = Math.Round((independentSum + 4) / (dicIndependentCnt.Count + 4.0), 1).ToString();
            //참여기업 평균
            viewModel1.BizResultCnt.BizInCompanyAvg = Math.Round((dicIndependentCnt.Values.Sum() + dicStartUpCnt.Values.Sum() + dicGrowthCnt.Values.Sum() + 0.0) / (dicIndependentCnt.Count + dicStartUpCnt.Count + dicGrowthCnt.Count), 1).ToString();
            viewModel1.BizResultPoint.BizInCompanyAvg = Math.Round((independentSum + startUpSum + growthSum) / (dicIndependentCnt.Count + dicGrowthCnt.Count + dicStartUpCnt.Count + 0.0), 1).ToString();

            //전체 평균
            viewModel1.BizResultCnt.TotalAvg = Math.Round((dicIndependentCnt.Values.Sum() + dicStartUpCnt.Values.Sum() + dicGrowthCnt.Values.Sum() + 343.0) / (dicIndependentCnt.Count + dicStartUpCnt.Count + dicGrowthCnt.Count + 100.0), 1).ToString();
            viewModel1.BizResultPoint.TotalAvg = Math.Round((independentSum + startUpSum + growthSum + 31) / (dicIndependentCnt.Count + dicGrowthCnt.Count + dicStartUpCnt.Count + 100.0), 1).ToString();

            //검토결과 데이터 생성
            var listRptMentorComment = await rptMentorCommentService.GetRptMentorCommentListAsync(paramModel.QuestionSn, paramModel.NumSn, paramModel.BizWorkYear, "13");

            //레포트 체크리스트
            var enumRptCheckList = await rptCheckListService.GetRptCheckListBySmallClassCd("13");

            //CommentList 채우기
            var CommentList = ReportHelper.MakeCommentViewModel(enumRptCheckList, listRptMentorComment);

            viewModel1.CommentList = CommentList;
            // ProductivityResult 종료...


            // ProductivityMgmtFacility 시작
            viewModel2.CheckList = await reportUtil.getGrowthStepPointCheckList(paramModel, "A1B106");

            var listRptMentorComment2 = await rptMentorCommentService.GetRptMentorCommentListAsync(paramModel.QuestionSn, paramModel.NumSn, paramModel.BizWorkYear, "14");

            //레포트 체크리스트
            var enumRptCheckList2 = await rptCheckListService.GetRptCheckListBySmallClassCd("14");

            //CommentList 채우기
            var CommentList2 = ReportHelper.MakeCommentViewModel(enumRptCheckList2, listRptMentorComment2);

            viewModel2.CommentList = CommentList2;
            // ProductivityMgmtFacility 종료 ... 


            // ProductivityProcessControl 시작
            viewModel3.CheckList = await reportUtil.getGrowthStepPointCheckList(paramModel, "A1B107");


            //검토결과 데이터 생성
            var listRptMentorComment3 = await rptMentorCommentService.GetRptMentorCommentListAsync(paramModel.QuestionSn, paramModel.NumSn, paramModel.BizWorkYear, "15");

            //레포트 체크리스트
            var enumRptCheckList3 = await rptCheckListService.GetRptCheckListBySmallClassCd("15");

            //CommentList 채우기
            var CommentList3 = ReportHelper.MakeCommentViewModel(enumRptCheckList3, listRptMentorComment3);

            viewModel3.CommentList = CommentList3;
            // ProductivityProcessControl 종료...

            viewList.Add(viewModel1);
            viewList.Add(viewModel2);
            viewList.Add(viewModel3);

            methodCache["paramModel"] = paramModel;
            methodCache["viewList"] = viewList;
            this.HttpContext.Cache.Insert(methodCacheId, methodCache);


            ViewBag.CompNm = paramModel.CompNm;
            ViewBag.paramModel = paramModel;
            return View(viewList);
        }

        [HttpPost]
        public async Task<ActionResult> ProductivityResult(OrgHR01ViewModel viewModel, BasicSurveyReportViewModel paramModel)
        {
            // NON ERP : Not target
            ViewBag.naviLeftMenu = Global.Report;

            string cacheId = "ProductivityResult" + paramModel.QuestionSn.ToString() + "_";
            string methodCacheId = cacheId + "methodCache";

            this.HttpContext.Cache.Remove(methodCacheId);

            var listRptMentorComment = await rptMentorCommentService.GetRptMentorCommentListAsync(paramModel.QuestionSn, paramModel.NumSn, paramModel.BizWorkYear, "13");

            foreach (var item in viewModel.CommentList)
            {
                var comment = listRptMentorComment.SingleOrDefault(i => i.DetailCd == item.DetailCd);
                if (comment == null)
                {
                    rptMentorCommentService.Insert(ReportHelper.MakeRptMentorcomment(item, paramModel));
                }
                else
                {
                    comment.Comment = item.Comment;
                }
            }
            await rptMentorCommentService.SaveDbContextAsync();

            if (viewModel.SubmitType == "T")
            {
                return RedirectToAction("ProductivityResult", "BasicSurveyReport", new { NumSn = paramModel.NumSn, CompSn = paramModel.CompSn, BizWorkYear = paramModel.BizWorkYear, Status = paramModel.Status, QuestionSn = paramModel.QuestionSn });
            }
            else
            {
                return RedirectToAction("ProductivityQC", "BasicSurveyReport", new { NumSn = paramModel.NumSn, CompSn = paramModel.CompSn, BizWorkYear = paramModel.BizWorkYear, Status = paramModel.Status, QuestionSn = paramModel.QuestionSn });
            }
        }

        public async Task<ActionResult> ProductivityProfitability(BasicSurveyReportViewModel paramModel)
        {
            //NON ERP : Done
            ViewBag.naviLeftMenu = Global.Report;
            ViewBag.paramModel = paramModel;

            string cacheId = this.ControllerContext.RouteData.Values["action"].ToString() + paramModel.QuestionSn.ToString() + "_";
            string methodCacheId = cacheId + "methodCache";

            Boolean cacheStatus = false;
            //string cacheId = "OverallSummary_" + paramModel.QuestionSn.ToString() + "_";
            Dictionary<string, object> methodCache = (Dictionary<string, object>)this.HttpContext.Cache[methodCacheId];

            if (methodCache != null && methodCache.Count > 0)
            {
                cacheStatus = true;
            }
            else
            {
                methodCache = new Dictionary<string, object>();
            }

            if (cacheStatus == true)
            {
                ViewBag.paramModel = methodCache["paramModel"];
                //ViewBag.NullCheck = ViewBag.paramModel.NullCheck;
                //ViewBag.PageNum = methodCache["PageNum"];
                //ViewBag.CompNm = ViewBag.paramModel.CompNm;
                ViewBag.CavYear = methodCache["CavYear"];
                return View(methodCache["viewModel"]);
            }

            ReportUtil reportUtil = new ReportUtil(scBizWorkService, quesResult1Service, quesResult2Service, quesMasterService, sboFinancialIndexTService, scCavService, scCompanyFinanceService, rptMasterService, scFinancialIndexTService, procMngService);

            ProductivityProfitabilityViewModel viewModel = new ProductivityProfitabilityViewModel();
            viewModel.Profitability = new BarChartViewModel();
            viewModel.Growth = new BarChartViewModel();

            //1) 현재 사업에 참여한 업체 평균
            var curBizWork = await scBizWorkService.GetBizWorkByBizWorkSn(int.Parse(paramModel.NumSn));

            //당기매출액
            Dictionary<string, decimal> dicSales = new Dictionary<string, decimal>();
            //영업이익
            Dictionary<string, decimal> dicOperatingEarning = new Dictionary<string, decimal>();
            //전기매출액
            Dictionary<string, decimal> dicPrevSales = new Dictionary<string, decimal>();

            SqlParameter numSn = new SqlParameter("NUM_SN", paramModel.NumSn);
            object[] parameters = new object[] { numSn };

            var curBizWorkObj = await procMngService.getBizWorkInfoByBizWorkSn(parameters);
            {
                //var compMappings = curBizWork.ScCompMappings.Where(sc => sc.Status == "A");
                foreach (var compMapping in curBizWorkObj)
                {
                    //문진표 작성내역 조회
                    var quesMaster = await quesMasterService.GetQuesOgranAnalysisAsync(compMapping.REGISTRATION_NO, paramModel.BizWorkYear);
                    if (quesMaster == null)
                    {
                        continue;
                    }

                    SHUSER_SboFinancialIndexT sboFinacialIndexT;
                    //int companySn = compMapping.ScCompInfo.CompSn;
                    //VcUsrInfo companyUserInfo = await scUsrService.getScUsrByCompSn(companySn);
                    //int CompSn = companyUserInfo.CompSn;
                    //string UseErp = companyUserInfo.UseErp;

                    //if (UseErp == "1")
                    //{
                    //    sboFinacialIndexT = await sboFinancialIndexTService.GetSHUSER_SboFinancialIndexT(compMapping.ScCompInfo.RegistrationNo, ConfigurationManager.AppSettings["CorpCode"], ConfigurationManager.AppSettings["BizCode"], paramModel.BizWorkYear.ToString());
                    //    if (sboFinacialIndexT == null)
                    //    {
                    //        continue;
                    //    }
                    //}
                    //else
                    //{
                    var scFinancialIndexT = await scFinancialIndexTService.getScFinancialIndexTAsync(compMapping.COMP_SN, paramModel.BizWorkYear.ToString());
                    if (scFinancialIndexT == null) continue;

                    sboFinacialIndexT = ConvertMap(scFinancialIndexT);
                    //}

                    //중소기업 평균값 가져오는 부분
                    var regCav = await scCavService.getMaxYear();
                    var regCavDetail = scCavService.GetCavAsync(regCav);
                    ViewBag.CavYear = regCavDetail.CavDt;
                    //해당기업을 찾아 점수를 별도로 저장한다.
                    if (quesMaster.QuestionSn == paramModel.QuestionSn)
                    {

                        viewModel.Profitability.Dividend = Math.Truncate(Convert.ToDouble(sboFinacialIndexT.OperatingEarning.Value / 1000));
                        viewModel.Profitability.Divisor = Math.Truncate(Convert.ToDouble(sboFinacialIndexT.CurrentSale.Value / 1000));
                        viewModel.Profitability.Result = (sboFinacialIndexT.CurrentSale.Value == 0) ? 0 : Math.Round(Convert.ToDouble(sboFinacialIndexT.OperatingEarning.Value / sboFinacialIndexT.CurrentSale.Value * 100), 1);
                        viewModel.Profitability.Company = viewModel.Profitability.Result;
                        viewModel.Profitability.AvgSMCompany = regCavDetail.CavOp;

                        viewModel.Growth.Dividend = Math.Truncate(Convert.ToDouble((sboFinacialIndexT.CurrentSale.Value - sboFinacialIndexT.PrevSale.Value) / 1000));
                        viewModel.Growth.Divisor = Math.Truncate(Convert.ToDouble(sboFinacialIndexT.PrevSale.Value / 1000));
                        viewModel.Growth.Result = (sboFinacialIndexT.PrevSale.Value == 0) ? 0 : Math.Round(Convert.ToDouble((sboFinacialIndexT.CurrentSale.Value - sboFinacialIndexT.PrevSale.Value) / sboFinacialIndexT.PrevSale.Value * 100), 1);
                        viewModel.Growth.Company = viewModel.Growth.Result;
                        viewModel.Growth.AvgSMCompany = regCavDetail.CavSg;
                    }

                    dicSales.Add(compMapping.REGISTRATION_NO, sboFinacialIndexT.CurrentSale.Value);
                    dicOperatingEarning.Add(compMapping.REGISTRATION_NO, sboFinacialIndexT.OperatingEarning.Value);
                    dicPrevSales.Add(compMapping.REGISTRATION_NO, sboFinacialIndexT.PrevSale.Value);

                }
            }

            //평균값 계산
            viewModel.Profitability.AvgBizInCompany = (dicSales.Values.Sum() == 0) ? 0 : Math.Round(Convert.ToDouble(dicOperatingEarning.Values.Sum() / dicSales.Values.Sum() * 100), 1);
            viewModel.Profitability.AvgTotal = Math.Round(Convert.ToDouble((dicOperatingEarning.Values.Sum() + 15961070936) / (dicSales.Values.Sum() + 307076158152) * 100), 1);

            viewModel.Growth.AvgBizInCompany = (dicPrevSales.Values.Sum() == 0) ? 0 : Math.Round(Convert.ToDouble((dicSales.Values.Sum() - dicPrevSales.Values.Sum()) / dicPrevSales.Values.Sum() * 100), 1);
            viewModel.Growth.AvgTotal = Math.Round(Convert.ToDouble(((dicSales.Values.Sum() - dicPrevSales.Values.Sum()) + 41562026522) / (dicPrevSales.Values.Sum() + 265426804812) * 100), 1);


            //검토결과 데이터 생성
            var listRptMentorComment = await rptMentorCommentService.GetRptMentorCommentListAsync(paramModel.QuestionSn, paramModel.NumSn, paramModel.BizWorkYear, "19");

            //레포트 체크리스트
            var enumRptCheckList = await rptCheckListService.GetRptCheckListBySmallClassCd("19");

            //CommentList 채우기
            var CommentList = ReportHelper.MakeCommentViewModel(enumRptCheckList, listRptMentorComment);

            viewModel.CommentList = CommentList;
            //보고서 상단 회사명 가져오기
            //var scCompMapping = await scCompMappingService.GetCompMappingAsync(paramModel.NumSn, paramModel.CompSn);
            //paramModel.CompNm = scCompMapping.ScCompInfo.CompNm;

            methodCache["paramModel"] = paramModel;
            methodCache["viewModel"] = viewModel;
            //methodCache["scCompMapping"] = scCompMapping;
            methodCache["CavYear"] = ViewBag.CavYear;
            //methodCache["PageNum"] = rptpagelist1[0].PageNum;
            this.HttpContext.Cache.Insert(methodCacheId, methodCache);

            ViewBag.CompNm = paramModel.CompNm;
            ViewBag.paramModel = paramModel;
            return View(viewModel);

        }

        [HttpPost]
        public async Task<ActionResult> ProductivityProfitability(OrgProductivityViewModel viewModel, BasicSurveyReportViewModel paramModel)
        {
            // NON ERP : Not target
            ViewBag.naviLeftMenu = Global.Report;

            string cacheId = this.ControllerContext.RouteData.Values["action"].ToString() + paramModel.QuestionSn.ToString() + "_";
            string methodCacheId = cacheId + "methodCache";

            this.HttpContext.Cache.Remove(methodCacheId);

            var listRptMentorComment = await rptMentorCommentService.GetRptMentorCommentListAsync(paramModel.QuestionSn, paramModel.NumSn, paramModel.BizWorkYear, "19");

            foreach (var item in viewModel.CommentList)
            {
                var comment = listRptMentorComment.SingleOrDefault(i => i.DetailCd == item.DetailCd);
                if (comment == null)
                {
                    rptMentorCommentService.Insert(ReportHelper.MakeRptMentorcomment(item, paramModel));
                }
                else
                {
                    comment.Comment = item.Comment;
                }
            }
            await rptMentorCommentService.SaveDbContextAsync();

            if (viewModel.SubmitType == "T")
            {
                return RedirectToAction("ProductivityProfitability", "BasicSurveyReport", new { NumSn = paramModel.NumSn, CompSn = paramModel.CompSn, BizWorkYear = paramModel.BizWorkYear, Status = paramModel.Status, QuestionSn = paramModel.QuestionSn });
            }
            else
            {
                return RedirectToAction("ProductivityValueChain", "BasicSurveyReport", new { NumSn = paramModel.NumSn, CompSn = paramModel.CompSn, BizWorkYear = paramModel.BizWorkYear, Status = paramModel.Status, QuestionSn = paramModel.QuestionSn });
            }
        }

        public async Task<ActionResult> RiskMgmtOrgSatisfaction(BasicSurveyReportViewModel paramModel)  // 조직 만족도
        {
            // NON ERP : Done
            ViewBag.naviLeftMenu = Global.Report;
            ViewBag.paramModel = paramModel;

            ReportUtil reportUtil = new ReportUtil(scBizWorkService, quesResult1Service, quesResult2Service, quesMasterService, sboFinancialIndexTService, scCavService, scCompanyFinanceService, rptMasterService, scFinancialIndexTService, procMngService);

            var viewModel = new RiskMgmtOrgSatisfactionViewModel();
            viewModel.orgSatisfaction = new CheckListViewModel();

            //1) 현재 사업에 참여한 업체 평균
            //사업참여 기업들의 레벨(창업보육, 보육성장, 자립성장) 분류
            Dictionary<int, int> dicStartUpTotal = new Dictionary<int, int>();
            Dictionary<int, int> dicStartUpMove = new Dictionary<int, int>();

            Dictionary<int, int> dicGrowthTotal = new Dictionary<int, int>();
            Dictionary<int, int> dicGrowthMove = new Dictionary<int, int>();

            Dictionary<int, int> dicIndependentTotal = new Dictionary<int, int>();
            Dictionary<int, int> dicIndependentMove = new Dictionary<int, int>();

            var curBizWork = await scBizWorkService.GetBizWorkByBizWorkSn(int.Parse(paramModel.NumSn));
            {
                //var compMappings = curBizWork.ScCompMappings.Where(sc => sc.Status == "A");
                //foreach (var compMapping in compMappings)
                //{
                //    var quesMaster = await quesMasterService.GetQuesMasterAsync(compMapping.ScCompInfo.RegistrationNo, paramModel.BizWorkYear);
                //    if (quesMaster == null)
                //    {
                //        continue;
                //    }

                //    //다래 재무정보 유무 체크하는 로직 추가해야함.(문진표정보, 재무정보가 있어야 보고서 생성가능.)
                //    //다래 재무정보 조회해야 함.

                //    SHUSER_SboFinancialIndexT sboFinacialIndexT;
                //    int companySn = compMapping.ScCompInfo.CompSn;
                //    VcUsrInfo companyUserInfo = await scUsrService.getScUsrByCompSn(companySn);
                //    int CompSn = companyUserInfo.CompSn;
                //    //string UseErp = companyUserInfo.UseErp;

                //    //if (UseErp == "1")
                //    //{
                //    //    sboFinacialIndexT = await sboFinancialIndexTService.GetSHUSER_SboFinancialIndexT(compMapping.ScCompInfo.RegistrationNo, ConfigurationManager.AppSettings["CorpCode"], ConfigurationManager.AppSettings["BizCode"], paramModel.BizWorkYear.ToString());
                //    //    if (sboFinacialIndexT == null)
                //    //    {
                //    //        continue;
                //    //    }
                //    //}
                //    //else
                //    //{
                //        var scFinancialIndexT = await scFinancialIndexTService.getScFinancialIndexTAsync(CompSn, paramModel.BizWorkYear.ToString());
                //        if (scFinancialIndexT == null) continue;

                //        sboFinacialIndexT = ConvertMap(scFinancialIndexT);
                //    //}

                //    // A1A202 : 조직만족도
                //    var quesResult2s = await quesResult2Service.GetQuesResult2sAsync(quesMaster.QuestionSn, "A1A202");
                //    //총직원
                //    var totalEmp = quesResult2s.SingleOrDefault(i => i.QuesCheckList.DetailCd == "A1A20201");
                //    //이직직원
                //    var moveEmp = quesResult2s.SingleOrDefault(i => i.QuesCheckList.DetailCd == "A1A20202");

                //    // 해당기업 계산 부분
                //    if (quesMaster.QuestionSn == paramModel.QuestionSn)
                //    {
                //        if (double.Parse(totalEmp.D451) == 0) // 2015년의 총 직원수가 0이면
                //        {
                //            viewModel.orgSatisfaction.Company = "-"; // 계산 불가
                //        }
                //        else // 2015년의 총 직원수가 0이 아니면
                //        {
                //            viewModel.orgSatisfaction.Company = Math.Round(Convert.ToDouble((int.Parse(moveEmp.D451) / double.Parse(totalEmp.D451)) * 100), 1).ToString(); // 우선 계산

                //            if (double.IsNaN(double.Parse(viewModel.orgSatisfaction.Company))) // 계산된 값이 nan 
                //            {
                //                viewModel.orgSatisfaction.Company = "-";
                //            }
                //        }
                //    }

                //    //종합점수 조회하여 분류별로 딕셔너리 저장
                //    var point = await reportUtil.GetCompanyTotalPoint(quesMaster.QuestionSn, ConvertToScMap(sboFinacialIndexT));

                //    if (point >= 0 && point <= 50)
                //    {
                //        if (totalEmp.D451 == null) totalEmp.D451 = "0";
                //        if (moveEmp.D451 == null) moveEmp.D451 = "0";
                //        dicStartUpTotal.Add(compMapping.CompSn, int.Parse(totalEmp.D451));
                //        dicStartUpMove.Add(compMapping.CompSn, int.Parse(moveEmp.D451));
                //    }
                //    else if (point > 50 && point <= 75)
                //    {
                //        if (totalEmp.D451 == null) totalEmp.D451 = "0";
                //        if (moveEmp.D451 == null) moveEmp.D451 = "0";
                //        dicGrowthTotal.Add(compMapping.CompSn, int.Parse(totalEmp.D451));
                //        dicGrowthMove.Add(compMapping.CompSn, int.Parse(moveEmp.D451));
                //    }
                //    else
                //    {
                //        if (totalEmp.D451 == null) totalEmp.D451 = "0";
                //        if (moveEmp.D451 == null) moveEmp.D451 = "0";
                //        dicIndependentTotal.Add(compMapping.CompSn, int.Parse(totalEmp.D451));
                //        dicIndependentMove.Add(compMapping.CompSn, int.Parse(moveEmp.D451));
                //    }
                //}
            }

            viewModel.orgSatisfaction.StartUpAvg = Math.Round(Convert.ToDouble(((dicStartUpMove.Values.Sum() + 100.0) / (dicStartUpTotal.Values.Sum() + 694)) * 100), 1).ToString();

            viewModel.orgSatisfaction.GrowthAvg = Math.Round(Convert.ToDouble(((dicGrowthMove.Values.Sum() + 185.0) / (dicGrowthTotal.Values.Sum() + 1172)) * 100), 1).ToString();

            viewModel.orgSatisfaction.IndependentAvg = Math.Round(Convert.ToDouble(((dicIndependentMove.Values.Sum() + 12.0) / (dicIndependentTotal.Values.Sum() + 116)) * 100), 1).ToString();

            viewModel.orgSatisfaction.BizInCompanyAvg = Math.Round(Convert.ToDouble(((dicStartUpMove.Values.Sum() + dicGrowthMove.Values.Sum() + dicIndependentMove.Values.Sum() + 0.0) / (dicStartUpTotal.Values.Sum() + dicGrowthTotal.Values.Sum() + dicIndependentTotal.Values.Sum())) * 100), 1).ToString();

            viewModel.orgSatisfaction.TotalAvg = Math.Round(Convert.ToDouble(((dicStartUpMove.Values.Sum() + dicGrowthMove.Values.Sum() + dicIndependentMove.Values.Sum() + 297.0) / (dicStartUpTotal.Values.Sum() + dicGrowthTotal.Values.Sum() + dicIndependentTotal.Values.Sum() + 1982)) * 100), 1).ToString();


            //검토결과 데이터 생성
            var listRptMentorComment = await rptMentorCommentService.GetRptMentorCommentListAsync(paramModel.QuestionSn, paramModel.NumSn, paramModel.BizWorkYear, "28");

            //레포트 체크리스트
            var enumRptCheckList = await rptCheckListService.GetRptCheckListBySmallClassCd("28");

            //CommentList 채우기
            var CommentList = ReportHelper.MakeCommentViewModel(enumRptCheckList, listRptMentorComment);

            viewModel.CommentList = CommentList;

            ViewBag.CompNm = paramModel.CompNm;
            ViewBag.paramModel = paramModel;
            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> RiskMgmtOrgSatisfaction(OrgHR01ViewModel viewModel, BasicSurveyReportViewModel paramModel)
        {
            ViewBag.naviLeftMenu = Global.Report;

            var listRptMentorComment = await rptMentorCommentService.GetRptMentorCommentListAsync(paramModel.QuestionSn, paramModel.NumSn, paramModel.BizWorkYear, "28");

            foreach (var item in viewModel.CommentList)
            {
                var comment = listRptMentorComment.SingleOrDefault(i => i.DetailCd == item.DetailCd);
                if (comment == null)
                {
                    rptMentorCommentService.Insert(ReportHelper.MakeRptMentorcomment(item, paramModel));
                }
                else
                {
                    comment.Comment = item.Comment;
                }
            }
            await rptMentorCommentService.SaveDbContextAsync();

            if (viewModel.SubmitType == "T")
            {
                return RedirectToAction("RiskMgmtOrgSatisfaction", "BasicSurveyReport", new { NumSn = paramModel.NumSn, CompSn = paramModel.CompSn, BizWorkYear = paramModel.BizWorkYear, Status = paramModel.Status, QuestionSn = paramModel.QuestionSn });
            }
            else
            {
                return RedirectToAction("RiskMgmtITSystem", "BasicSurveyReport", new { NumSn = paramModel.NumSn, CompSn = paramModel.CompSn, BizWorkYear = paramModel.BizWorkYear, Status = paramModel.Status, QuestionSn = paramModel.QuestionSn });
            }
        }

        public async Task<ActionResult> RiskMgmtLiquidity(BasicSurveyReportViewModel paramModel)
        {
            // NON ERP : Done
            ViewBag.naviLeftMenu = Global.Report;
            ViewBag.paramModel = paramModel;

            string cacheId = this.ControllerContext.RouteData.Values["action"].ToString() + paramModel.QuestionSn.ToString() + "_";
            string methodCacheId = cacheId + "methodCache";

            Boolean cacheStatus = false;
            //string cacheId = "OverallSummary_" + paramModel.QuestionSn.ToString() + "_";
            Dictionary<string, object> methodCache = (Dictionary<string, object>)this.HttpContext.Cache[methodCacheId];

            if (methodCache != null && methodCache.Count > 0)
            {
                cacheStatus = true;
            }
            else
            {
                methodCache = new Dictionary<string, object>();
            }

            if (cacheStatus == true)
            {
                ViewBag.paramModel = methodCache["paramModel"];
                //ViewBag.NullCheck = ViewBag.paramModel.NullCheck;
                //ViewBag.PageNum = methodCache["PageNum"];
                //ViewBag.CompNm = ViewBag.paramModel.CompNm;
                ViewBag.CavYear = methodCache["CavYear"];
                return View(methodCache["viewModel"]);
            }

            ReportUtil reportUtil = new ReportUtil(scBizWorkService, quesResult1Service, quesResult2Service, quesMasterService, sboFinancialIndexTService, scCavService, scCompanyFinanceService, rptMasterService, scFinancialIndexTService, procMngService);

            RiskMgmtLiquidityViewModel viewModel = new RiskMgmtLiquidityViewModel();
            viewModel.Liquidity = new BarChartViewModel();
            viewModel.Stability = new BarChartViewModel();

            //1) 현재 사업에 참여한 업체 평균
            var curBizWork = await scBizWorkService.GetBizWorkByBizWorkSn(int.Parse(paramModel.NumSn));

            SqlParameter numSn = new SqlParameter("NUM_SN", paramModel.NumSn);
            object[] parameters = new object[] { numSn };
            var curBizWorkObj = await procMngService.getBizWorkInfoByBizWorkSn(parameters);

            //유동자산
            Dictionary<string, decimal> dicCurrentAsset = new Dictionary<string, decimal>();
            //유동부채
            Dictionary<string, decimal> dicCurrentLiability = new Dictionary<string, decimal>();
            //부채총계
            Dictionary<string, decimal> dicTotalLiability = new Dictionary<string, decimal>();
            //자본총계
            Dictionary<string, decimal> dicTotalCapital = new Dictionary<string, decimal>();

            {
                //var compMappings = curBizWork.ScCompMappings.Where(sc => sc.Status == "A");
                foreach (var compMapping in curBizWorkObj)
                {
                    //문진표 작성내역 조회
                    var quesMaster = await quesMasterService.GetQuesOgranAnalysisAsync(compMapping.REGISTRATION_NO, paramModel.BizWorkYear);
                    if (quesMaster == null)
                    {
                        continue;
                    }

                    SHUSER_SboFinancialIndexT sboFinacialIndexT;
                    //int companySn = compMapping.ScCompInfo.CompSn;
                    //VcUsrInfo companyUserInfo = await scUsrService.getScUsrByCompSn(companySn);
                    //int CompSn = companyUserInfo.CompSn;
                    //string UseErp = companyUserInfo.UseErp;

                    //if (UseErp == "1")
                    //{
                    //    sboFinacialIndexT = await sboFinancialIndexTService.GetSHUSER_SboFinancialIndexT(compMapping.ScCompInfo.RegistrationNo, ConfigurationManager.AppSettings["CorpCode"], ConfigurationManager.AppSettings["BizCode"], paramModel.BizWorkYear.ToString());
                    //    if (sboFinacialIndexT == null)
                    //    {
                    //        continue;
                    //    }
                    //}
                    //else
                    //{
                    var scFinancialIndexT = await scFinancialIndexTService.getScFinancialIndexTAsync(compMapping.COMP_SN, paramModel.BizWorkYear.ToString());
                    if (scFinancialIndexT == null) continue;

                    sboFinacialIndexT = ConvertMap(scFinancialIndexT);
                    // }
                    //중소기업 평균값 가져오는 부분
                    var regCav = await scCavService.getMaxYear();
                    var regCavDetail = scCavService.GetCavAsync(regCav);
                    ViewBag.CavYear = regCavDetail.CavDt;
                    //해당기업을 찾아 점수를 별도로 저장한다.
                    if (quesMaster.QuestionSn == paramModel.QuestionSn)
                    {

                        viewModel.Liquidity.Dividend = Math.Truncate(Convert.ToDouble(sboFinacialIndexT.CurrentAsset.Value / 1000));
                        viewModel.Liquidity.Divisor = Math.Truncate(Convert.ToDouble(sboFinacialIndexT.CurrentLiability.Value / 1000));
                        viewModel.Liquidity.Result = (sboFinacialIndexT.CurrentLiability.Value == 0) ? 0 : Math.Round(Convert.ToDouble(sboFinacialIndexT.CurrentAsset.Value / sboFinacialIndexT.CurrentLiability.Value * 100), 1);
                        viewModel.Liquidity.Company = viewModel.Liquidity.Result;
                        viewModel.Liquidity.AvgSMCompany = regCavDetail.CavCr; //중소기업평균 유동비율

                        viewModel.Stability.Dividend = Math.Truncate(Convert.ToDouble(sboFinacialIndexT.TotalLiability.Value / 1000));
                        viewModel.Stability.Divisor = Math.Truncate(Convert.ToDouble(sboFinacialIndexT.TotalCapital.Value / 1000));
                        viewModel.Stability.Result = (sboFinacialIndexT.TotalCapital.Value == 0) ? 0 : Math.Round(Convert.ToDouble(sboFinacialIndexT.TotalLiability.Value / sboFinacialIndexT.TotalCapital.Value * 100), 1);
                        viewModel.Stability.Company = viewModel.Stability.Result;
                        viewModel.Stability.AvgSMCompany = regCavDetail.CavDebt; //중소기업평균 부채비율
                    }

                    //dicCurrentAsset.Add(compMapping.ScCompInfo.RegistrationNo, sboFinacialIndexT.CurrentSale.Value);
                    //dicCurrentLiability.Add(compMapping.ScCompInfo.RegistrationNo, sboFinacialIndexT.OperatingEarning.Value);
                    //dicTotalLiability.Add(compMapping.ScCompInfo.RegistrationNo, sboFinacialIndexT.PrevSale.Value);
                    //dicTotalCapital.Add(compMapping.ScCompInfo.RegistrationNo, sboFinacialIndexT.PrevSale.Value);
                    dicCurrentAsset.Add(compMapping.REGISTRATION_NO, sboFinacialIndexT.CurrentAsset.Value);
                    dicCurrentLiability.Add(compMapping.REGISTRATION_NO, sboFinacialIndexT.CurrentLiability.Value);
                    dicTotalLiability.Add(compMapping.REGISTRATION_NO, sboFinacialIndexT.TotalLiability.Value);
                    dicTotalCapital.Add(compMapping.REGISTRATION_NO, sboFinacialIndexT.TotalCapital.Value);

                }
            }

            //평균값 계산
            viewModel.Liquidity.AvgBizInCompany = (dicCurrentLiability.Values.Sum() == 0) ? 0 : Math.Round(Convert.ToDouble(dicCurrentAsset.Values.Sum() / dicCurrentLiability.Values.Sum() * 100), 1);
            viewModel.Liquidity.AvgTotal = Math.Round(Convert.ToDouble((dicCurrentAsset.Values.Sum() + 129006100528) / (dicCurrentLiability.Values.Sum() + 77693292830) * 100), 1);

            viewModel.Stability.AvgBizInCompany = (dicTotalCapital.Values.Sum() == 0) ? 0 : Math.Round(Convert.ToDouble(dicTotalLiability.Values.Sum() / dicTotalCapital.Values.Sum() * 100), 1);
            viewModel.Stability.AvgTotal = Math.Round(Convert.ToDouble((dicTotalLiability.Values.Sum() + 170285912240) / (dicTotalCapital.Values.Sum() + 115832509297) * 100), 1);


            //검토결과 데이터 생성
            var listRptMentorComment = await rptMentorCommentService.GetRptMentorCommentListAsync(paramModel.QuestionSn, paramModel.NumSn, paramModel.BizWorkYear, "30");

            //레포트 체크리스트
            var enumRptCheckList = await rptCheckListService.GetRptCheckListBySmallClassCd("30");

            //CommentList 채우기
            var CommentList = ReportHelper.MakeCommentViewModel(enumRptCheckList, listRptMentorComment);

            viewModel.CommentList = CommentList;
            //보고서 상단 회사명 가져오기
            //var scCompMapping = await scCompMappingService.GetCompMappingAsync(paramModel.NumSn, paramModel.CompSn);
            //paramModel.CompNm = scCompMapping.ScCompInfo.CompNm;

            methodCache["paramModel"] = paramModel;
            methodCache["viewModel"] = viewModel;
            methodCache["CavYear"] = ViewBag.CavYear;
            //methodCache["PageNum"] = rptpagelist1[0].PageNum;
            this.HttpContext.Cache.Insert(methodCacheId, methodCache);

            ViewBag.CompNm = paramModel.CompNm;
            ViewBag.paramModel = paramModel;
            return View(viewModel);

        }

        [HttpPost]
        public async Task<ActionResult> RiskMgmtLiquidity(OrgProductivityViewModel viewModel, BasicSurveyReportViewModel paramModel)
        {
            string cacheId = this.ControllerContext.RouteData.Values["action"].ToString() + paramModel.QuestionSn.ToString() + "_";
            string methodCacheId = cacheId + "methodCache";


            this.HttpContext.Cache.Remove(methodCacheId);

            //NON ERP : Not Target
            ViewBag.naviLeftMenu = Global.Report;

            var listRptMentorComment = await rptMentorCommentService.GetRptMentorCommentListAsync(paramModel.QuestionSn, paramModel.NumSn, paramModel.BizWorkYear, "30");

            foreach (var item in viewModel.CommentList)
            {
                var comment = listRptMentorComment.SingleOrDefault(i => i.DetailCd == item.DetailCd);
                if (comment == null)
                {
                    rptMentorCommentService.Insert(ReportHelper.MakeRptMentorcomment(item, paramModel));
                }
                else
                {
                    comment.Comment = item.Comment;
                }
            }
            await rptMentorCommentService.SaveDbContextAsync();

            if (viewModel.SubmitType == "T")
            {
                return RedirectToAction("RiskMgmtLiquidity", "BasicSurveyReport", new { NumSn = paramModel.NumSn, CompSn = paramModel.CompSn, BizWorkYear = paramModel.BizWorkYear, Status = paramModel.Status, QuestionSn = paramModel.QuestionSn });
            }
            else
            {
                return RedirectToAction("RiskMgmtEvalProfession", "BasicSurveyReport", new { NumSn = paramModel.NumSn, CompSn = paramModel.CompSn, BizWorkYear = paramModel.BizWorkYear, Status = paramModel.Status, QuestionSn = paramModel.QuestionSn });
            }
        }


        #region 2. 기초역량 검토 종합결과

        //P12 2.상품화역량 - 사업화역량
        public async Task<ActionResult> ProductivityCommercialize(BasicSurveyReportViewModel paramModel)
        {

            //NON ERP : Done
            ViewBag.naviLeftMenu = Global.Report;
            ViewBag.paramModel = paramModel;

            LocalReportUtil reportUtil = new LocalReportUtil(scBizWorkService, quesResult1Service, quesResult2Service, quesMasterService, sboFinancialIndexTService, scFinancialIndexTService, scCavService, scUsrService, scCompanyFinanceService, rptMasterService, procMngService);

            RiskMgmtViewModel viewModel = new RiskMgmtViewModel();
            viewModel.CheckList = await reportUtil.getGrowthStepPointCheckList(paramModel, "A1B104");


            //검토결과 데이터 생성
            var listRptMentorComment = await rptMentorCommentService.GetRptMentorCommentListAsync(paramModel.QuestionSn, paramModel.NumSn, paramModel.BizWorkYear, "12");

            //레포트 체크리스트
            var enumRptCheckList = await rptCheckListService.GetRptCheckListBySmallClassCd("12");

            //CommentList 채우기
            var CommentList = ReportHelper.MakeCommentViewModel(enumRptCheckList, listRptMentorComment);

            viewModel.CommentList = CommentList;
            //보고서 상단 회사명 가져오기
            //var scCompMapping = await scCompMappingService.GetCompMappingAsync(paramModel.NumSn, paramModel.CompSn);
            //paramModel.CompNm = scCompMapping.ScCompInfo.CompNm;

            ViewBag.CompNm = paramModel.CompNm;

            ViewBag.paramModel = paramModel;
            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> ProductivityCommercialize(OrgHR01ViewModel viewModel, BasicSurveyReportViewModel paramModel)
        {
            //NON ERP : NOT TARGET
            ViewBag.naviLeftMenu = Global.Report;

            var listRptMentorComment = await rptMentorCommentService.GetRptMentorCommentListAsync(paramModel.QuestionSn, paramModel.NumSn, paramModel.BizWorkYear, "12");

            foreach (var item in viewModel.CommentList)
            {
                var comment = listRptMentorComment.SingleOrDefault(i => i.DetailCd == item.DetailCd);
                if (comment == null)
                {
                    rptMentorCommentService.Insert(ReportHelper.MakeRptMentorcomment(item, paramModel));
                }
                else
                {
                    comment.Comment = item.Comment;
                }
            }
            await rptMentorCommentService.SaveDbContextAsync();

            if (viewModel.SubmitType == "T")
            {
                return RedirectToAction("ProductivityCommercialize", "BasicSurveyReport", new { NumSn = paramModel.NumSn, CompSn = paramModel.CompSn, BizWorkYear = paramModel.BizWorkYear, Status = paramModel.Status, QuestionSn = paramModel.QuestionSn });
            }
            else
            {
                return RedirectToAction("ProductivityResult", "BasicSurveyReport", new { NumSn = paramModel.NumSn, CompSn = paramModel.CompSn, BizWorkYear = paramModel.BizWorkYear, Status = paramModel.Status, QuestionSn = paramModel.QuestionSn });
            }
        }


        //P14 2.상품화역량 - 생산설비의 운영체계 및 관리
        public async Task<ActionResult> ProductivityMgmtFacility(BasicSurveyReportViewModel paramModel)
        {
            //NON ERP : Done
            ViewBag.naviLeftMenu = Global.Report;
            ViewBag.paramModel = paramModel;

            //ReportUtil reportUtil = new ReportUtil(scBizWorkService, quesResult1Service, quesResult2Service, quesMasterService, sboFinancialIndexTService, scCavService);
            LocalReportUtil reportUtil = new LocalReportUtil(scBizWorkService, quesResult1Service, quesResult2Service, quesMasterService, sboFinancialIndexTService, scFinancialIndexTService, scCavService, scUsrService, scCompanyFinanceService, rptMasterService, procMngService);

            RiskMgmtViewModel viewModel = new RiskMgmtViewModel();
            viewModel.CheckList = await reportUtil.getGrowthStepPointCheckList(paramModel, "A1B106");


            //검토결과 데이터 생성
            var listRptMentorComment = await rptMentorCommentService.GetRptMentorCommentListAsync(paramModel.QuestionSn, paramModel.NumSn, paramModel.BizWorkYear, "14");

            //레포트 체크리스트
            var enumRptCheckList = await rptCheckListService.GetRptCheckListBySmallClassCd("14");

            //CommentList 채우기
            var CommentList = ReportHelper.MakeCommentViewModel(enumRptCheckList, listRptMentorComment);

            viewModel.CommentList = CommentList;
            //보고서 상단 회사명 가져오기
            //var scCompMapping = await scCompMappingService.GetCompMappingAsync(paramModel.NumSn, paramModel.CompSn);
            //paramModel.CompNm = scCompMapping.ScCompInfo.CompNm;

            ViewBag.CompNm = paramModel.CompNm;
            ViewBag.paramModel = paramModel;
            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> ProductivityMgmtFacility(OrgHR01ViewModel viewModel, BasicSurveyReportViewModel paramModel)
        {
            // NON ERP : Done
            ViewBag.naviLeftMenu = Global.Report;

            var listRptMentorComment = await rptMentorCommentService.GetRptMentorCommentListAsync(paramModel.QuestionSn, paramModel.NumSn, paramModel.BizWorkYear, "14");

            foreach (var item in viewModel.CommentList)
            {
                var comment = listRptMentorComment.SingleOrDefault(i => i.DetailCd == item.DetailCd);
                if (comment == null)
                {
                    rptMentorCommentService.Insert(ReportHelper.MakeRptMentorcomment(item, paramModel));
                }
                else
                {
                    comment.Comment = item.Comment;
                }
            }
            await rptMentorCommentService.SaveDbContextAsync();

            if (viewModel.SubmitType == "T")
            {
                return RedirectToAction("ProductivityMgmtFacility", "BasicSurveyReport", new { NumSn = paramModel.NumSn, CompSn = paramModel.CompSn, BizWorkYear = paramModel.BizWorkYear, Status = paramModel.Status, QuestionSn = paramModel.QuestionSn });
            }
            else
            {
                return RedirectToAction("ProductivityProcessControl", "BasicSurveyReport", new { NumSn = paramModel.NumSn, CompSn = paramModel.CompSn, BizWorkYear = paramModel.BizWorkYear, Status = paramModel.Status, QuestionSn = paramModel.QuestionSn });
            }
        }

        //P15 2.상품화역량 - 공정관리
        public async Task<ActionResult> ProductivityProcessControl(BasicSurveyReportViewModel paramModel)
        {
            //NON ERP : Done

            ViewBag.naviLeftMenu = Global.Report;
            ViewBag.paramModel = paramModel;

            //ReportUtil reportUtil = new ReportUtil(scBizWorkService, quesResult1Service, quesResult2Service, quesMasterService, sboFinancialIndexTService, scCavService);
            LocalReportUtil reportUtil = new LocalReportUtil(scBizWorkService, quesResult1Service, quesResult2Service, quesMasterService, sboFinancialIndexTService, scFinancialIndexTService, scCavService, scUsrService, scCompanyFinanceService, rptMasterService, procMngService);


            RiskMgmtViewModel viewModel = new RiskMgmtViewModel();
            viewModel.CheckList = await reportUtil.getGrowthStepPointCheckList(paramModel, "A1B107");


            //검토결과 데이터 생성
            var listRptMentorComment = await rptMentorCommentService.GetRptMentorCommentListAsync(paramModel.QuestionSn, paramModel.NumSn, paramModel.BizWorkYear, "15");

            //레포트 체크리스트
            var enumRptCheckList = await rptCheckListService.GetRptCheckListBySmallClassCd("15");

            //CommentList 채우기
            var CommentList = ReportHelper.MakeCommentViewModel(enumRptCheckList, listRptMentorComment);

            viewModel.CommentList = CommentList;
            //보고서 상단 회사명 가져오기
            //var scCompMapping = await scCompMappingService.GetCompMappingAsync(paramModel.NumSn, paramModel.CompSn);
            //paramModel.CompNm = scCompMapping.ScCompInfo.CompNm;

            ViewBag.CompNm = paramModel.CompNm;
            ViewBag.paramModel = paramModel;
            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> ProductivityProcessControl(OrgHR01ViewModel viewModel, BasicSurveyReportViewModel paramModel)
        {
            //NON ERP : NOT TARGET

            ViewBag.naviLeftMenu = Global.Report;

            var listRptMentorComment = await rptMentorCommentService.GetRptMentorCommentListAsync(paramModel.QuestionSn, paramModel.NumSn, paramModel.BizWorkYear, "15");

            foreach (var item in viewModel.CommentList)
            {
                var comment = listRptMentorComment.SingleOrDefault(i => i.DetailCd == item.DetailCd);
                if (comment == null)
                {
                    rptMentorCommentService.Insert(ReportHelper.MakeRptMentorcomment(item, paramModel));
                }
                else
                {
                    comment.Comment = item.Comment;
                }
            }
            await rptMentorCommentService.SaveDbContextAsync();

            if (viewModel.SubmitType == "T")
            {
                return RedirectToAction("ProductivityProcessControl", "BasicSurveyReport", new { NumSn = paramModel.NumSn, CompSn = paramModel.CompSn, BizWorkYear = paramModel.BizWorkYear, Status = paramModel.Status, QuestionSn = paramModel.QuestionSn });
            }
            else
            {
                return RedirectToAction("ProductivityQC", "BasicSurveyReport", new { NumSn = paramModel.NumSn, CompSn = paramModel.CompSn, BizWorkYear = paramModel.BizWorkYear, Status = paramModel.Status, QuestionSn = paramModel.QuestionSn });
            }
        }

        //P16 2.상품화역량 - 품질관리
        public async Task<ActionResult> ProductivityQC(BasicSurveyReportViewModel paramModel)
        {
            //NON ERP : Done
            ViewBag.naviLeftMenu = Global.Report;
            ViewBag.paramModel = paramModel;

            string cacheId = this.ControllerContext.RouteData.Values["action"].ToString() + paramModel.QuestionSn.ToString() + "_";
            string methodCacheId = cacheId + "methodCache";

            Boolean cacheStatus = false;
            //string cacheId = "OverallSummary_" + paramModel.QuestionSn.ToString() + "_";
            Dictionary<string, object> methodCache = (Dictionary<string, object>)this.HttpContext.Cache[methodCacheId];

            if (methodCache != null && methodCache.Count > 0)
            {
                cacheStatus = true;
            }
            else
            {
                methodCache = new Dictionary<string, object>();
            }

            if (cacheStatus == true)
            {
                ViewBag.paramModel = methodCache["paramModel"];
                //ViewBag.NullCheck = ViewBag.paramModel.NullCheck;
                //ViewBag.PageNum = methodCache["PageNum"];
                //ViewBag.CavYear = methodCache["CavYear"];
                return View(methodCache["viewList"]);
            }

            //ReportUtil reportUtil = new ReportUtil(scBizWorkService, quesResult1Service, quesResult2Service, quesMasterService, sboFinancialIndexTService, scCavService);
            LocalReportUtil reportUtil = new LocalReportUtil(scBizWorkService, quesResult1Service, quesResult2Service, quesMasterService, sboFinancialIndexTService, scFinancialIndexTService, scCavService, scUsrService, scCompanyFinanceService, rptMasterService, procMngService);


            RiskMgmtViewModel viewModel1 = new RiskMgmtViewModel();
            viewModel1.CheckList = await reportUtil.getGrowthStepPointCheckList(paramModel, "A1B108");

            RiskMgmtViewModel viewModel2 = new RiskMgmtViewModel();
            viewModel2.CheckList = await reportUtil.getGrowthStepPointCheckList(paramModel, "A1C101");

            List<object> viewList = new List<object>();


            //검토결과 데이터 생성
            var listRptMentorComment = await rptMentorCommentService.GetRptMentorCommentListAsync(paramModel.QuestionSn, paramModel.NumSn, paramModel.BizWorkYear, "16");

            //레포트 체크리스트
            var enumRptCheckList = await rptCheckListService.GetRptCheckListBySmallClassCd("16");

            //CommentList 채우기
            var CommentList = ReportHelper.MakeCommentViewModel(enumRptCheckList, listRptMentorComment);

            viewModel1.CommentList = CommentList;


            //검토결과 데이터 생성
            var listRptMentorComment2 = await rptMentorCommentService.GetRptMentorCommentListAsync(paramModel.QuestionSn, paramModel.NumSn, paramModel.BizWorkYear, "17");

            //레포트 체크리스트
            var enumRptCheckList2 = await rptCheckListService.GetRptCheckListBySmallClassCd("17");

            //CommentList 채우기
            var CommentList2 = ReportHelper.MakeCommentViewModel(enumRptCheckList2, listRptMentorComment2);

            viewModel2.CommentList = CommentList2;

            viewList.Add(viewModel1);
            viewList.Add(viewModel2);

            methodCache["paramModel"] = paramModel;
            methodCache["viewList"] = viewList;
            this.HttpContext.Cache.Insert(methodCacheId, methodCache);

            ViewBag.CompNm = paramModel.CompNm;
            ViewBag.paramModel = paramModel;

            return View(viewList);
        }

        [HttpPost]
        public async Task<ActionResult> ProductivityQC(OrgHR01ViewModel viewModel, BasicSurveyReportViewModel paramModel)
        {
            //NON ERP : NOT TARGET
            string cacheId = this.ControllerContext.RouteData.Values["action"].ToString() + paramModel.QuestionSn.ToString() + "_";
            string methodCacheId = cacheId + "methodCache";
            this.HttpContext.Cache.Remove(methodCacheId);

            ViewBag.naviLeftMenu = Global.Report;

            var listRptMentorComment = await rptMentorCommentService.GetRptMentorCommentListAsync(paramModel.QuestionSn, paramModel.NumSn, paramModel.BizWorkYear, "16");

            foreach (var item in viewModel.CommentList)
            {
                var comment = listRptMentorComment.SingleOrDefault(i => i.DetailCd == item.DetailCd);
                if (comment == null)
                {
                    rptMentorCommentService.Insert(ReportHelper.MakeRptMentorcomment(item, paramModel));
                }
                else
                {
                    comment.Comment = item.Comment;
                }
            }
            await rptMentorCommentService.SaveDbContextAsync();

            if (viewModel.SubmitType == "T")
            {
                return RedirectToAction("ProductivityQC", "BasicSurveyReport", new { NumSn = paramModel.NumSn, CompSn = paramModel.CompSn, BizWorkYear = paramModel.BizWorkYear, Status = paramModel.Status, QuestionSn = paramModel.QuestionSn });
            }
            else
            {
                return RedirectToAction("ProductivityMgmtCustomer", "BasicSurveyReport", new { NumSn = paramModel.NumSn, CompSn = paramModel.CompSn, BizWorkYear = paramModel.BizWorkYear, Status = paramModel.Status, QuestionSn = paramModel.QuestionSn });
            }
        }

        //P17 2.상품화역량 - 마케팅 전략의 수립 및 실행
        public async Task<ActionResult> ProductivityMgmtMarketing(BasicSurveyReportViewModel paramModel)
        {
            //NON ERP : Done

            ViewBag.naviLeftMenu = Global.Report;
            ViewBag.paramModel = paramModel;

            //ReportUtil reportUtil = new ReportUtil(scBizWorkService, quesResult1Service, quesResult2Service, quesMasterService, sboFinancialIndexTService, scCavService);
            LocalReportUtil reportUtil = new LocalReportUtil(scBizWorkService, quesResult1Service, quesResult2Service, quesMasterService, sboFinancialIndexTService, scFinancialIndexTService, scCavService, scUsrService, scCompanyFinanceService, rptMasterService, procMngService);


            RiskMgmtViewModel viewModel = new RiskMgmtViewModel();
            viewModel.CheckList = await reportUtil.getGrowthStepPointCheckList(paramModel, "A1C101");

            //검토결과 데이터 생성
            var listRptMentorComment = await rptMentorCommentService.GetRptMentorCommentListAsync(paramModel.QuestionSn, paramModel.NumSn, paramModel.BizWorkYear, "17");

            //레포트 체크리스트
            var enumRptCheckList = await rptCheckListService.GetRptCheckListBySmallClassCd("17");

            //CommentList 채우기
            var CommentList = ReportHelper.MakeCommentViewModel(enumRptCheckList, listRptMentorComment);

            viewModel.CommentList = CommentList;
            //보고서 상단 회사명 가져오기
            //var scCompMapping = await scCompMappingService.GetCompMappingAsync(paramModel.NumSn, paramModel.CompSn);
            //paramModel.CompNm = scCompMapping.ScCompInfo.CompNm;

            ViewBag.CompNm = paramModel.CompNm;
            ViewBag.paramModel = paramModel;
            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> ProductivityMgmtMarketing(OrgHR01ViewModel viewModel, BasicSurveyReportViewModel paramModel)
        {
            //NON ERP : NOT TARGET

            ViewBag.naviLeftMenu = Global.Report;

            var listRptMentorComment = await rptMentorCommentService.GetRptMentorCommentListAsync(paramModel.QuestionSn, paramModel.NumSn, paramModel.BizWorkYear, "17");

            foreach (var item in viewModel.CommentList)
            {
                var comment = listRptMentorComment.SingleOrDefault(i => i.DetailCd == item.DetailCd);
                if (comment == null)
                {
                    rptMentorCommentService.Insert(ReportHelper.MakeRptMentorcomment(item, paramModel));
                }
                else
                {
                    comment.Comment = item.Comment;
                }
            }
            await rptMentorCommentService.SaveDbContextAsync();

            if (viewModel.SubmitType == "T")
            {
                return RedirectToAction("ProductivityMgmtMarketing", "BasicSurveyReport", new { NumSn = paramModel.NumSn, CompSn = paramModel.CompSn, BizWorkYear = paramModel.BizWorkYear, Status = paramModel.Status, QuestionSn = paramModel.QuestionSn });
            }
            else
            {
                return RedirectToAction("ProductivityMgmtCustomer", "BasicSurveyReport", new { NumSn = paramModel.NumSn, CompSn = paramModel.CompSn, BizWorkYear = paramModel.BizWorkYear, Status = paramModel.Status, QuestionSn = paramModel.QuestionSn });
            }
        }

        //P18 2.상품화역량 - 고객관리
        public async Task<ActionResult> ProductivityMgmtCustomer(BasicSurveyReportViewModel paramModel)
        {
            // NON ERP : Done
            ViewBag.naviLeftMenu = Global.Report;
            ViewBag.paramModel = paramModel;

            string cacheId = this.ControllerContext.RouteData.Values["action"].ToString() + paramModel.QuestionSn.ToString() + "_";
            string methodCacheId = cacheId + "methodCache";

            Boolean cacheStatus = false;
            //string cacheId = "OverallSummary_" + paramModel.QuestionSn.ToString() + "_";
            Dictionary<string, object> methodCache = (Dictionary<string, object>)this.HttpContext.Cache[methodCacheId];

            if (methodCache != null && methodCache.Count > 0)
            {
                cacheStatus = true;
            }
            else
            {
                methodCache = new Dictionary<string, object>();
            }

            if (cacheStatus == true)
            {
                ViewBag.paramModel = methodCache["paramModel"];
                return View(methodCache["viewList"]);
            }

            //ReportUtil reportUtil = new ReportUtil(scBizWorkService, quesResult1Service, quesResult2Service, quesMasterService, sboFinancialIndexTService, scCavService);
            LocalReportUtil reportUtil = new LocalReportUtil(scBizWorkService, quesResult1Service, quesResult2Service, quesMasterService, sboFinancialIndexTService, scFinancialIndexTService, scCavService, scUsrService, scCompanyFinanceService, rptMasterService, procMngService);


            RiskMgmtViewModel viewModel = new RiskMgmtViewModel();
            viewModel.CheckList = await reportUtil.getGrowthStepPointCheckList(paramModel, "A1C102");


            //검토결과 데이터 생성
            var listRptMentorComment = await rptMentorCommentService.GetRptMentorCommentListAsync(paramModel.QuestionSn, paramModel.NumSn, paramModel.BizWorkYear, "18");

            //레포트 체크리스트
            var enumRptCheckList = await rptCheckListService.GetRptCheckListBySmallClassCd("18");

            //CommentList 채우기
            var CommentList = ReportHelper.MakeCommentViewModel(enumRptCheckList, listRptMentorComment);

            viewModel.CommentList = CommentList;
            //보고서 상단 회사명 가져오기
            //var scCompMapping = await scCompMappingService.GetCompMappingAsync(paramModel.NumSn, paramModel.CompSn);
            //paramModel.CompNm = scCompMapping.ScCompInfo.CompNm;


            methodCache["paramModel"] = paramModel;
            methodCache["viewList"] = viewModel;
            this.HttpContext.Cache.Insert(methodCacheId, methodCache);

            ViewBag.CompNm = paramModel.CompNm;
            ViewBag.paramModel = paramModel;
            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> ProductivityMgmtCustomer(OrgHR01ViewModel viewModel, BasicSurveyReportViewModel paramModel)
        {
            //NON ERP : NOT TARGET

            string cacheId = this.ControllerContext.RouteData.Values["action"].ToString() + paramModel.QuestionSn.ToString() + "_";
            string methodCacheId = cacheId + "methodCache";
            this.HttpContext.Cache.Remove(methodCacheId);

            ViewBag.naviLeftMenu = Global.Report;

            var listRptMentorComment = await rptMentorCommentService.GetRptMentorCommentListAsync(paramModel.QuestionSn, paramModel.NumSn, paramModel.BizWorkYear, "18");

            foreach (var item in viewModel.CommentList)
            {
                var comment = listRptMentorComment.SingleOrDefault(i => i.DetailCd == item.DetailCd);
                if (comment == null)
                {
                    rptMentorCommentService.Insert(ReportHelper.MakeRptMentorcomment(item, paramModel));
                }
                else
                {
                    comment.Comment = item.Comment;
                }
            }
            await rptMentorCommentService.SaveDbContextAsync();

            if (viewModel.SubmitType == "T")
            {
                return RedirectToAction("ProductivityMgmtCustomer", "BasicSurveyReport", new { NumSn = paramModel.NumSn, CompSn = paramModel.CompSn, BizWorkYear = paramModel.BizWorkYear, Status = paramModel.Status, QuestionSn = paramModel.QuestionSn });
            }
            else
            {
                return RedirectToAction("ProductivityProfitability", "BasicSurveyReport", new { NumSn = paramModel.NumSn, CompSn = paramModel.CompSn, BizWorkYear = paramModel.BizWorkYear, Status = paramModel.Status, QuestionSn = paramModel.QuestionSn });
            }
        }


        // p20 2.상품화 역량 -  역량별 검토결과 - 타깃고객검토
        public async Task<ActionResult> ProductivityTargetCustomer(BasicSurveyReportViewModel paramModel)
        {
            // NON ERP : NOT TARGET

            ViewBag.naviLeftMenu = Global.Report;
            ViewBag.paramModel = paramModel;

            RiskMgmtViewModel viewModel = new RiskMgmtViewModel();

            //검토결과 데이터 생성
            var listRptMentorComment = await rptMentorCommentService.GetRptMentorCommentListAsync(paramModel.QuestionSn, paramModel.NumSn, paramModel.BizWorkYear, "20");

            //레포트 체크리스트
            var enumRptCheckList = await rptCheckListService.GetRptCheckListBySmallClassCd("20");

            //CommentList 채우기
            var CommentList = ReportHelper.MakeCommentViewModel(enumRptCheckList.Where(cl => cl.Type == "C"), listRptMentorComment.Where(rmc => rmc.RptCheckList.Type == "C").ToList());

            //체크박스 List 채우기
            var ChekcBoxList = ReportHelper.MakeCheckBoxViewModel(enumRptCheckList.Where(cl => cl.Type == "B"), listRptMentorComment.Where(rmc => rmc.RptCheckList.Type == "B").ToList());

            viewModel.CommentList = CommentList;
            viewModel.CheckBoxList = ChekcBoxList;

            ViewBag.paramModel = paramModel;

            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> ProductivityTargetCustomer(BasicSurveyReportViewModel paramModel, RiskMgmtViewModel viewModel)
        {
            //NON ERP : NOT TARGET

            ViewBag.naviLeftMenu = Global.Report;

            var listRptMentorComment = await rptMentorCommentService.GetRptMentorCommentListAsync(paramModel.QuestionSn, paramModel.NumSn, paramModel.BizWorkYear, "20");

            foreach (var item in viewModel.CommentList)
            {
                var comment = listRptMentorComment.SingleOrDefault(i => i.DetailCd == item.DetailCd);
                if (comment == null)
                {
                    rptMentorCommentService.Insert(ReportHelper.MakeRptMentorcomment(item, paramModel));
                }
                else
                {
                    comment.Comment = item.Comment;
                }
            }

            foreach (var item in viewModel.CheckBoxList)
            {
                var comment = listRptMentorComment.SingleOrDefault(i => i.DetailCd == item.DetailCd);
                if (comment == null)
                {
                    rptMentorCommentService.Insert(ReportHelper.MakeRptMentorcomment(item, paramModel));
                }
                else
                {
                    comment.Comment = item.CheckVal.ToString();
                }
            }

            await rptMentorCommentService.SaveDbContextAsync();

            if (viewModel.SubmitType == "T") //임시저장
            {
                return RedirectToAction("ProductivityTargetCustomer", "BasicSurveyReport", new { NumSn = paramModel.NumSn, CompSn = paramModel.CompSn, BizWorkYear = paramModel.BizWorkYear, Status = paramModel.Status, QuestionSn = paramModel.QuestionSn });
            }
            else
            {
                return RedirectToAction("ProductivityValueChain", "BasicSurveyReport", new { NumSn = paramModel.NumSn, CompSn = paramModel.CompSn, BizWorkYear = paramModel.BizWorkYear, Status = paramModel.Status, QuestionSn = paramModel.QuestionSn });
            }
        }

        // p21 2.상품화 역량 -  역량별 검토결과 - 상품화구조 Check
        public async Task<ActionResult> ProductivityValueChain(BasicSurveyReportViewModel paramModel)
        {
            //NON ERP : NOT TARGET
            ViewBag.naviLeftMenu = Global.Report;
            ViewBag.paramModel = paramModel;

            string cacheId = this.ControllerContext.RouteData.Values["action"].ToString() + paramModel.QuestionSn.ToString() + "_";
            string methodCacheId = cacheId + "methodCache";

            RiskMgmtViewModel viewModel1 = new RiskMgmtViewModel();
            ProductivityRelationViewModel viewModel2 = new ProductivityRelationViewModel();

            List<object> viewList = new List<object>();

            Boolean cacheStatus = false;
            //string cacheId = "OverallSummary_" + paramModel.QuestionSn.ToString() + "_";
            Dictionary<string, object> methodCache = (Dictionary<string, object>)this.HttpContext.Cache[methodCacheId];

            if (methodCache != null && methodCache.Count > 0)
            {
                cacheStatus = true;
            }
            else
            {
                methodCache = new Dictionary<string, object>();
            }

            if (cacheStatus == true)
            {
                ViewBag.paramModel = methodCache["paramModel"];
                //ViewBag.NullCheck = ViewBag.paramModel.NullCheck;
                //ViewBag.PageNum = methodCache["PageNum"];
                //ViewBag.CompNm = ViewBag.paramModel.CompNm;
                //ViewBag.CavYear = methodCache["CavYear"];
                return View(methodCache["viewList"]);
            }

            //검토결과 데이터 생성
            var listRptMentorComment = await rptMentorCommentService.GetRptMentorCommentListAsync(paramModel.QuestionSn, paramModel.NumSn, paramModel.BizWorkYear, "21");

            //레포트 체크리스트
            var enumRptCheckList = await rptCheckListService.GetRptCheckListBySmallClassCd("21");

            //CommentList 채우기
            var CommentList = ReportHelper.MakeCommentViewModel(enumRptCheckList.Where(cl => cl.Type == "C"), listRptMentorComment.Where(rmc => rmc.RptCheckList.Type == "C").ToList());

            //체크박스 List 채우기
            var ChekcBoxList = ReportHelper.MakeCheckBoxViewModel(enumRptCheckList.Where(cl => cl.Type == "B"), listRptMentorComment.Where(rmc => rmc.RptCheckList.Type == "B").ToList());

            viewModel1.CommentList = CommentList;
            viewModel1.CheckBoxList = ChekcBoxList;
            // .............

            //검토결과 데이터 생성
            var listRptMentorComment2 = await rptMentorCommentService.GetRptMentorCommentListAsync(paramModel.QuestionSn, paramModel.NumSn, paramModel.BizWorkYear, "22");

            //레포트 체크리스트
            var enumRptCheckList2 = await rptCheckListService.GetRptCheckListBySmallClassCd("22");

            //CommentList 채우기
            var CommentList2 = ReportHelper.MakeCommentViewModel(enumRptCheckList2.Where(cl => cl.Type == "C"), listRptMentorComment2.Where(rmc => rmc.RptCheckList.Type == "C").ToList());


            //SCP 입력값 가져오는 로직 있어야함(테이블도 생성해야함)
            //검토결과 데이터 생성
            var listRptMngComment = await rptMngCommentService.GetRptMngCommentListAsync(paramModel.BizWorkYear, "22");

            //레포트 체크리스트
            var enumRptMngCode = await rptMngCodeService.GetRptMngCodeBySmallClassCd("22");

            //상품화 역량 시장현황 CommentList 채우기
            var MakComment = await scMakService.getMaxYear();
            var MakCommentDetail = scMakService.GetMakAsync(MakComment);

            viewModel2.CommentList = CommentList2;
            viewModel2.MakCommentDetail = MakCommentDetail;

            viewList.Add(viewModel1);
            viewList.Add(viewModel2);

            methodCache["paramModel"] = paramModel;
            methodCache["viewList"] = viewList;
            //methodCache["scCompMapping"] = scCompMapping;
            //methodCache["CavYear"] = ViewBag.CavYear;
            //methodCache["PageNum"] = rptpagelist1[0].PageNum;
            this.HttpContext.Cache.Insert(methodCacheId, methodCache);


            ViewBag.paramModel = paramModel;
            ViewBag.CompNm = paramModel.CompNm;
            return View(viewList);
        }

        [HttpPost]
        public async Task<ActionResult> ProductivityValueChain(BasicSurveyReportViewModel paramModel, RiskMgmtViewModel viewModel1, ProductivityRelationViewModel viewModel2)
        {
            //NON ERP : NOT TARGET
            this.HttpContext.Cache.Remove("OverallSummary_" + paramModel.QuestionSn.ToString() + "_ViewBag");
            this.HttpContext.Cache.Remove("OverallSummary_" + paramModel.QuestionSn.ToString() + "_ViewModel");
            string cacheId = this.ControllerContext.RouteData.Values["action"].ToString() + paramModel.QuestionSn.ToString() + "_";
            string methodCacheId = cacheId + "methodCache";
            this.HttpContext.Cache.Remove(methodCacheId);

            ViewBag.naviLeftMenu = Global.Report;

            // 1페이지 내용에 대한 저장 
            RiskMgmtViewModel viewModel = viewModel1;

            var listRptMentorComment = await rptMentorCommentService.GetRptMentorCommentListAsync(paramModel.QuestionSn, paramModel.NumSn, paramModel.BizWorkYear, "21");

            foreach (var item in viewModel.CommentList)
            {
                var comment = listRptMentorComment.SingleOrDefault(i => i.DetailCd == item.DetailCd);
                if (comment == null)
                {
                    rptMentorCommentService.Insert(ReportHelper.MakeRptMentorcomment(item, paramModel));
                }
                else
                {
                    comment.Comment = item.Comment;
                }
            }

            foreach (var item in viewModel.CheckBoxList)
            {
                var comment = listRptMentorComment.SingleOrDefault(i => i.DetailCd == item.DetailCd);
                if (comment == null)
                {
                    rptMentorCommentService.Insert(ReportHelper.MakeRptMentorcomment(item, paramModel));
                }
                else
                {
                    comment.Comment = item.CheckVal.ToString();
                }
            }

            await rptMentorCommentService.SaveDbContextAsync();
            // 1페이지 것 저장 완료 .... 

            // 2페이지 내용에 대한 저장
            var listRptMentorComment2 = await rptMentorCommentService.GetRptMentorCommentListAsync(paramModel.QuestionSn, paramModel.NumSn, paramModel.BizWorkYear, "22");

            foreach (var item in viewModel2.CommentList)
            {
                var comment = listRptMentorComment2.SingleOrDefault(i => i.DetailCd == item.DetailCd);
                if (comment == null)
                {
                    rptMentorCommentService.Insert(ReportHelper.MakeRptMentorcomment(item, paramModel));
                }
                else
                {
                    comment.Comment = item.Comment;
                }
            }
            try
            {
                await rptMentorCommentService.SaveDbContextAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            // 2페이지 완료 ....

            if (viewModel.SubmitType == "T") //임시저장
            {
                return RedirectToAction("ProductivityValueChain", "BasicSurveyReport", new { NumSn = paramModel.NumSn, CompSn = paramModel.CompSn, BizWorkYear = paramModel.BizWorkYear, Status = paramModel.Status, QuestionSn = paramModel.QuestionSn });
            }
            else
            {
                return RedirectToAction("ProductivityRelation2", "BasicSurveyReport", new { NumSn = paramModel.NumSn, CompSn = paramModel.CompSn, BizWorkYear = paramModel.BizWorkYear, Status = paramModel.Status, QuestionSn = paramModel.QuestionSn });
            }

        }

        // p22 2.상품화 역량 -  역량별 검토결과 - 제품생산.판매 관계망검토
        public async Task<ActionResult> ProductivityRelation(BasicSurveyReportViewModel paramModel)
        {
            ViewBag.naviLeftMenu = Global.Report;
            ViewBag.paramModel = paramModel;

            ProductivityRelationViewModel viewModel = new ProductivityRelationViewModel();

            //검토결과 데이터 생성
            var listRptMentorComment = await rptMentorCommentService.GetRptMentorCommentListAsync(paramModel.QuestionSn, paramModel.NumSn, paramModel.BizWorkYear, "22");

            //레포트 체크리스트
            var enumRptCheckList = await rptCheckListService.GetRptCheckListBySmallClassCd("22");

            //CommentList 채우기
            var CommentList = ReportHelper.MakeCommentViewModel(enumRptCheckList.Where(cl => cl.Type == "C"), listRptMentorComment.Where(rmc => rmc.RptCheckList.Type == "C").ToList());


            //SCP 입력값 가져오는 로직 있어야함(테이블도 생성해야함)
            //검토결과 데이터 생성
            var listRptMngComment = await rptMngCommentService.GetRptMngCommentListAsync(paramModel.BizWorkYear, "22");

            //레포트 체크리스트
            var enumRptMngCode = await rptMngCodeService.GetRptMngCodeBySmallClassCd("22");

            //상품화 역량 시장현황 CommentList 채우기
            var MakComment = await scMakService.getMaxYear();
            var MakCommentDetail = scMakService.GetMakAsync(MakComment);

            viewModel.CommentList = CommentList;
            viewModel.MakCommentDetail = MakCommentDetail;
            //보고서 상단 회사명 가져오기
            //var scCompMapping = await scCompMappingService.GetCompMappingAsync(paramModel.NumSn, paramModel.CompSn);
            //paramModel.CompNm = scCompMapping.ScCompInfo.CompNm;

            ViewBag.CompNm = paramModel.CompNm;
            ViewBag.paramModel = paramModel;

            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> ProductivityRelation(BasicSurveyReportViewModel paramModel, ProductivityRelationViewModel viewModel)
        {
            ViewBag.naviLeftMenu = Global.Report;

            var listRptMentorComment = await rptMentorCommentService.GetRptMentorCommentListAsync(paramModel.QuestionSn, paramModel.NumSn, paramModel.BizWorkYear, "22");

            foreach (var item in viewModel.CommentList)
            {
                var comment = listRptMentorComment.SingleOrDefault(i => i.DetailCd == item.DetailCd);

                if (comment == null)
                {
                    rptMentorCommentService.Insert(ReportHelper.MakeRptMentorcomment(item, paramModel));
                }

                else
                {
                    comment.Comment = item.Comment;
                }
            }
            try
            {
                await rptMentorCommentService.SaveDbContextAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (viewModel.SubmitType == "T") //임시저장
            {
                return RedirectToAction("ProductivityRelation", "BasicSurveyReport", new { NumSn = paramModel.NumSn, CompSn = paramModel.CompSn, BizWorkYear = paramModel.BizWorkYear, Status = paramModel.Status, QuestionSn = paramModel.QuestionSn });
            }
            else
            {
                return RedirectToAction("ProductivityRelation2", "BasicSurveyReport", new { NumSn = paramModel.NumSn, CompSn = paramModel.CompSn, BizWorkYear = paramModel.BizWorkYear, Status = paramModel.Status, QuestionSn = paramModel.QuestionSn });
            }
        }

        // p23 2.상품화 역량 -  역량별 검토결과 - 제품생산.판매 관계망검토
        public async Task<ActionResult> ProductivityRelation2(BasicSurveyReportViewModel paramModel)
        {
            ViewBag.naviLeftMenu = Global.Report;
            ViewBag.paramModel = paramModel;

            string cacheId = this.ControllerContext.RouteData.Values["action"].ToString() + paramModel.QuestionSn.ToString() + "_";
            string methodCacheId = cacheId + "methodCache";

            ProductivityRelationViewModel viewModel = new ProductivityRelationViewModel();

            Boolean cacheStatus = false;
            //string cacheId = "OverallSummary_" + paramModel.QuestionSn.ToString() + "_";
            Dictionary<string, object> methodCache = (Dictionary<string, object>)this.HttpContext.Cache[methodCacheId];

            if (methodCache != null && methodCache.Count > 0)
            {
                cacheStatus = true;
            }
            else
            {
                methodCache = new Dictionary<string, object>();
            }

            if (cacheStatus == true)
            {
                ViewBag.paramModel = methodCache["paramModel"];
                //ViewBag.NullCheck = ViewBag.paramModel.NullCheck;
                //ViewBag.PageNum = methodCache["PageNum"];
                //ViewBag.CompNm = ViewBag.paramModel.CompNm;
                //ViewBag.CavYear = methodCache["CavYear"];
                return View(methodCache["viewModel"]);
            }

            //검토결과 데이터 생성
            var listRptMentorComment = await rptMentorCommentService.GetRptMentorCommentListAsync(paramModel.QuestionSn, paramModel.NumSn, paramModel.BizWorkYear, "23");

            //레포트 체크리스트
            var enumRptCheckList = await rptCheckListService.GetRptCheckListBySmallClassCd("23");

            //CommentList 채우기
            var CommentList = ReportHelper.MakeCommentViewModel(enumRptCheckList.Where(cl => cl.Type == "C"), listRptMentorComment.Where(rmc => rmc.RptCheckList.Type == "C").ToList());


            //SCP 입력값 가져오는 로직 있어야함(테이블도 생성해야함)

            viewModel.CommentList = CommentList;

            ViewBag.paramModel = paramModel;
            //보고서 상단 회사명 가져오기
            //var scCompMapping = await scCompMappingService.GetCompMappingAsync(paramModel.NumSn, paramModel.CompSn);
            //paramModel.CompNm = scCompMapping.ScCompInfo.CompNm;
            methodCache["paramModel"] = paramModel;
            methodCache["viewModel"] = viewModel;
            //methodCache["scCompMapping"] = scCompMapping;
            //methodCache["CavYear"] = ViewBag.CavYear;
            //methodCache["PageNum"] = rptpagelist1[0].PageNum;
            this.HttpContext.Cache.Insert(methodCacheId, methodCache);

            ViewBag.CompNm = paramModel.CompNm;
            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> ProductivityRelation2(BasicSurveyReportViewModel paramModel, ProductivityRelationViewModel viewModel)
        {
            ViewBag.naviLeftMenu = Global.Report;

            string cacheId = this.ControllerContext.RouteData.Values["action"].ToString() + paramModel.QuestionSn.ToString() + "_";
            string methodCacheId = cacheId + "methodCache";
            this.HttpContext.Cache.Remove(methodCacheId);

            var listRptMentorComment = await rptMentorCommentService.GetRptMentorCommentListAsync(paramModel.QuestionSn, paramModel.NumSn, paramModel.BizWorkYear, "23");

            foreach (var item in viewModel.CommentList)
            {
                var comment = listRptMentorComment.SingleOrDefault(i => i.DetailCd == item.DetailCd);
                if (comment == null)
                {
                    rptMentorCommentService.Insert(ReportHelper.MakeRptMentorcomment(item, paramModel));
                }
                else
                {
                    comment.Comment = item.Comment;
                }
            }
            try
            {
                await rptMentorCommentService.SaveDbContextAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (viewModel.SubmitType == "T") //임시저장
            {
                return RedirectToAction("ProductivityRelation2", "BasicSurveyReport", new { NumSn = paramModel.NumSn, CompSn = paramModel.CompSn, BizWorkYear = paramModel.BizWorkYear, Status = paramModel.Status, QuestionSn = paramModel.QuestionSn });
            }
            else
            {
                return RedirectToAction("RiskMgmtVisionStrategy", "BasicSurveyReport", new { NumSn = paramModel.NumSn, CompSn = paramModel.CompSn, BizWorkYear = paramModel.BizWorkYear, Status = paramModel.Status, QuestionSn = paramModel.QuestionSn });
            }
        }


        //P24 3.위험관리역량 - [CEO역량]경영목표 및 전략
        public async Task<ActionResult> RiskMgmtVisionStrategy(BasicSurveyReportViewModel paramModel)
        {
            //NON ERP : Done
            ViewBag.naviLeftMenu = Global.Report;
            ViewBag.paramModel = paramModel;

            string cacheId = this.ControllerContext.RouteData.Values["action"].ToString() + paramModel.QuestionSn.ToString() + "_";
            string methodCacheId = cacheId + "methodCache";

            Boolean cacheStatus = false;
            //string cacheId = "OverallSummary_" + paramModel.QuestionSn.ToString() + "_";
            Dictionary<string, object> methodCache = (Dictionary<string, object>)this.HttpContext.Cache[methodCacheId];

            if (methodCache != null && methodCache.Count > 0)
            {
                cacheStatus = true;
            }
            else
            {
                methodCache = new Dictionary<string, object>();
            }

            if (cacheStatus == true)
            {
                ViewBag.paramModel = methodCache["paramModel"];
                //ViewBag.NullCheck = ViewBag.paramModel.NullCheck;
                //ViewBag.PageNum = methodCache["PageNum"];
                //ViewBag.CompNm = ViewBag.paramModel.CompNm;
                //ViewBag.CavYear = methodCache["CavYear"];
                return View(methodCache["viewList"]);
            }

            LocalReportUtil reportUtil = new LocalReportUtil(scBizWorkService, quesResult1Service, quesResult2Service, quesMasterService, sboFinancialIndexTService, scFinancialIndexTService, scCavService, scUsrService, scCompanyFinanceService, rptMasterService, procMngService);

            RiskMgmtViewModel viewModel = new RiskMgmtViewModel();
            viewModel.CheckList = await reportUtil.getGrowthStepPointCheckList(paramModel, "A1A101");

            RiskMgmtViewModel viewModel2 = new RiskMgmtViewModel();
            viewModel2.CheckList = await reportUtil.getGrowthStepPointCheckList(paramModel, "A1A102");

            RiskMgmtViewModel viewModel3 = new RiskMgmtViewModel();
            viewModel3.CheckList = await reportUtil.getGrowthStepPointCheckList(paramModel, "A1A103");

            List<object> viewList = new List<object>();

            //
            //검토결과 데이터 생성
            var listRptMentorComment = await rptMentorCommentService.GetRptMentorCommentListAsync(paramModel.QuestionSn, paramModel.NumSn, paramModel.BizWorkYear, "24");

            //레포트 체크리스트
            var enumRptCheckList = await rptCheckListService.GetRptCheckListBySmallClassCd("24");

            //CommentList 채우기
            var CommentList = ReportHelper.MakeCommentViewModel(enumRptCheckList, listRptMentorComment);

            viewModel.CommentList = CommentList;
            // ..................


            //
            //검토결과 데이터 생성
            var listRptMentorComment2 = await rptMentorCommentService.GetRptMentorCommentListAsync(paramModel.QuestionSn, paramModel.NumSn, paramModel.BizWorkYear, "25");

            //레포트 체크리스트
            var enumRptCheckList2 = await rptCheckListService.GetRptCheckListBySmallClassCd("25");

            //CommentList 채우기
            var CommentList2 = ReportHelper.MakeCommentViewModel(enumRptCheckList2, listRptMentorComment2);

            viewModel2.CommentList = CommentList2;
            // ...........................

            //
            //검토결과 데이터 생성
            var listRptMentorComment3 = await rptMentorCommentService.GetRptMentorCommentListAsync(paramModel.QuestionSn, paramModel.NumSn, paramModel.BizWorkYear, "26");

            //레포트 체크리스트
            var enumRptCheckList3 = await rptCheckListService.GetRptCheckListBySmallClassCd("26");

            //CommentList 채우기
            var CommentList3 = ReportHelper.MakeCommentViewModel(enumRptCheckList3, listRptMentorComment3);

            viewModel3.CommentList = CommentList3;
            // ...........................



            viewList.Add(viewModel);
            viewList.Add(viewModel2);
            viewList.Add(viewModel3);

            methodCache["paramModel"] = paramModel;
            methodCache["viewList"] = viewList;
            //methodCache["scCompMapping"] = scCompMapping;
            //methodCache["CavYear"] = ViewBag.CavYear;
            //methodCache["PageNum"] = rptpagelist1[0].PageNum;
            this.HttpContext.Cache.Insert(methodCacheId, methodCache);

            ViewBag.CompNm = paramModel.CompNm;
            ViewBag.paramModel = paramModel;
            return View(viewList);
        }

        [HttpPost]
        public async Task<ActionResult> RiskMgmtVisionStrategy(OrgHR01ViewModel viewModel, BasicSurveyReportViewModel paramModel)
        {
            // NON ERP : Not target
            ViewBag.naviLeftMenu = Global.Report;

            string cacheId = this.ControllerContext.RouteData.Values["action"].ToString() + paramModel.QuestionSn.ToString() + "_";
            string methodCacheId = cacheId + "methodCache";
            this.HttpContext.Cache.Remove(methodCacheId);

            var listRptMentorComment = await rptMentorCommentService.GetRptMentorCommentListAsync(paramModel.QuestionSn, paramModel.NumSn, paramModel.BizWorkYear, "24");

            foreach (var item in viewModel.CommentList)
            {
                var comment = listRptMentorComment.SingleOrDefault(i => i.DetailCd == item.DetailCd);
                if (comment == null)
                {
                    rptMentorCommentService.Insert(ReportHelper.MakeRptMentorcomment(item, paramModel));
                }
                else
                {
                    comment.Comment = item.Comment;
                }
            }
            await rptMentorCommentService.SaveDbContextAsync();

            if (viewModel.SubmitType == "T")
            {
                return RedirectToAction("RiskMgmtVisionStrategy", "BasicSurveyReport", new { NumSn = paramModel.NumSn, CompSn = paramModel.CompSn, BizWorkYear = paramModel.BizWorkYear, Status = paramModel.Status, QuestionSn = paramModel.QuestionSn });
            }
            else
            {
                return RedirectToAction("RiskMgmtWorkingEnv", "BasicSurveyReport", new { NumSn = paramModel.NumSn, CompSn = paramModel.CompSn, BizWorkYear = paramModel.BizWorkYear, Status = paramModel.Status, QuestionSn = paramModel.QuestionSn });
            }
        }

        //P25 3.위험관리역량 - [CEO역량]경영자의 리더쉽
        public async Task<ActionResult> RiskMgmtLeadership(BasicSurveyReportViewModel paramModel)
        {
            // NON ERP : DONE
            ViewBag.naviLeftMenu = Global.Report;
            ViewBag.paramModel = paramModel;

            LocalReportUtil reportUtil = new LocalReportUtil(scBizWorkService, quesResult1Service, quesResult2Service, quesMasterService, sboFinancialIndexTService, scFinancialIndexTService, scCavService, scUsrService, scCompanyFinanceService, rptMasterService, procMngService);

            RiskMgmtViewModel viewModel = new RiskMgmtViewModel();
            viewModel.CheckList = await reportUtil.getGrowthStepPointCheckList(paramModel, "A1A102");

            // 
            //검토결과 데이터 생성
            var listRptMentorComment = await rptMentorCommentService.GetRptMentorCommentListAsync(paramModel.QuestionSn, paramModel.NumSn, paramModel.BizWorkYear, "25");

            //레포트 체크리스트
            var enumRptCheckList = await rptCheckListService.GetRptCheckListBySmallClassCd("25");

            //CommentList 채우기
            var CommentList = ReportHelper.MakeCommentViewModel(enumRptCheckList, listRptMentorComment);

            viewModel.CommentList = CommentList;
            // ......................................

            ViewBag.CompNm = paramModel.CompNm;
            ViewBag.paramModel = paramModel;
            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> RiskMgmtLeadership(OrgHR01ViewModel viewModel, BasicSurveyReportViewModel paramModel)
        {
            //NON ERP : NOT TARGET
            ViewBag.naviLeftMenu = Global.Report;

            var listRptMentorComment = await rptMentorCommentService.GetRptMentorCommentListAsync(paramModel.QuestionSn, paramModel.NumSn, paramModel.BizWorkYear, "25");

            foreach (var item in viewModel.CommentList)
            {
                var comment = listRptMentorComment.SingleOrDefault(i => i.DetailCd == item.DetailCd);
                if (comment == null)
                {
                    rptMentorCommentService.Insert(ReportHelper.MakeRptMentorcomment(item, paramModel));
                }
                else
                {
                    comment.Comment = item.Comment;
                }
            }
            await rptMentorCommentService.SaveDbContextAsync();

            if (viewModel.SubmitType == "T")
            {
                return RedirectToAction("RiskMgmtLeadership", "BasicSurveyReport", new { NumSn = paramModel.NumSn, CompSn = paramModel.CompSn, BizWorkYear = paramModel.BizWorkYear, Status = paramModel.Status, QuestionSn = paramModel.QuestionSn });
            }
            else
            {
                return RedirectToAction("RiskMgmtRelCEO", "BasicSurveyReport", new { NumSn = paramModel.NumSn, CompSn = paramModel.CompSn, BizWorkYear = paramModel.BizWorkYear, Status = paramModel.Status, QuestionSn = paramModel.QuestionSn });
            }
        }

        //P26 3.위험관리역량 - 경영목표의 신뢰성
        public async Task<ActionResult> RiskMgmtRelCEO(BasicSurveyReportViewModel paramModel)
        {
            // NON ERP : Not target
            ViewBag.naviLeftMenu = Global.Report;
            ViewBag.paramModel = paramModel;

            LocalReportUtil reportUtil = new LocalReportUtil(scBizWorkService, quesResult1Service, quesResult2Service, quesMasterService, sboFinancialIndexTService, scFinancialIndexTService, scCavService, scUsrService, scCompanyFinanceService, rptMasterService, procMngService);

            RiskMgmtViewModel viewModel = new RiskMgmtViewModel();
            viewModel.CheckList = await reportUtil.getGrowthStepPointCheckList(paramModel, "A1A103");


            //검토결과 데이터 생성
            var listRptMentorComment = await rptMentorCommentService.GetRptMentorCommentListAsync(paramModel.QuestionSn, paramModel.NumSn, paramModel.BizWorkYear, "26");

            //레포트 체크리스트
            var enumRptCheckList = await rptCheckListService.GetRptCheckListBySmallClassCd("26");

            //CommentList 채우기
            var CommentList = ReportHelper.MakeCommentViewModel(enumRptCheckList, listRptMentorComment);

            viewModel.CommentList = CommentList;
            //보고서 상단 회사명 가져오기
            //var scCompMapping = await scCompMappingService.GetCompMappingAsync(paramModel.NumSn, paramModel.CompSn);
            //paramModel.CompNm = scCompMapping.ScCompInfo.CompNm;

            ViewBag.CompNm = paramModel.CompNm;
            ViewBag.paramModel = paramModel;
            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> RiskMgmtRelCEO(OrgHR01ViewModel viewModel, BasicSurveyReportViewModel paramModel)
        {
            //NON ERP : NOT TARGET
            ViewBag.naviLeftMenu = Global.Report;

            var listRptMentorComment = await rptMentorCommentService.GetRptMentorCommentListAsync(paramModel.QuestionSn, paramModel.NumSn, paramModel.BizWorkYear, "26");

            foreach (var item in viewModel.CommentList)
            {
                var comment = listRptMentorComment.SingleOrDefault(i => i.DetailCd == item.DetailCd);
                if (comment == null)
                {
                    rptMentorCommentService.Insert(ReportHelper.MakeRptMentorcomment(item, paramModel));
                }
                else
                {
                    comment.Comment = item.Comment;
                }
            }
            await rptMentorCommentService.SaveDbContextAsync();

            if (viewModel.SubmitType == "T")
            {
                return RedirectToAction("RiskMgmtRelCEO", "BasicSurveyReport", new { NumSn = paramModel.NumSn, CompSn = paramModel.CompSn, BizWorkYear = paramModel.BizWorkYear, Status = paramModel.Status, QuestionSn = paramModel.QuestionSn });
            }
            else
            {
                return RedirectToAction("RiskMgmtWorkingEnv", "BasicSurveyReport", new { NumSn = paramModel.NumSn, CompSn = paramModel.CompSn, BizWorkYear = paramModel.BizWorkYear, Status = paramModel.Status, QuestionSn = paramModel.QuestionSn });
            }
        }

        //P27 3.위험관리역량 - 근로환경
        public async Task<ActionResult> RiskMgmtWorkingEnv(BasicSurveyReportViewModel paramModel)
        {
            ViewBag.naviLeftMenu = Global.Report;
            ViewBag.paramModel = paramModel;

            string cacheId = this.ControllerContext.RouteData.Values["action"].ToString() + paramModel.QuestionSn.ToString() + "_";
            string methodCacheId = cacheId + "methodCache";

            Boolean cacheStatus = false;
            //string cacheId = "OverallSummary_" + paramModel.QuestionSn.ToString() + "_";
            Dictionary<string, object> methodCache = (Dictionary<string, object>)this.HttpContext.Cache[methodCacheId];

            if (methodCache != null && methodCache.Count > 0)
            {
                cacheStatus = true;
            }
            else
            {
                methodCache = new Dictionary<string, object>();
            }

            if (cacheStatus == true)
            {
                ViewBag.paramModel = methodCache["paramModel"];
                //ViewBag.NullCheck = ViewBag.paramModel.NullCheck;
                //ViewBag.PageNum = methodCache["PageNum"];
                //ViewBag.CompNm = ViewBag.paramModel.CompNm;
                //ViewBag.CavYear = methodCache["CavYear"];
                return View(methodCache["viewList"]);
            }

            ReportUtil reportUtil = new ReportUtil(scBizWorkService, quesResult1Service, quesResult2Service, quesMasterService, sboFinancialIndexTService, scCavService, scCompanyFinanceService, rptMasterService, scFinancialIndexTService, procMngService);
            LocalReportUtil localReportUtil = new LocalReportUtil(scBizWorkService, quesResult1Service, quesResult2Service, quesMasterService, sboFinancialIndexTService, scFinancialIndexTService, scCavService, scUsrService, scCompanyFinanceService, rptMasterService, procMngService);

            RiskMgmtViewModel viewModel = new RiskMgmtViewModel();
            viewModel.CheckList = await localReportUtil.getGrowthStepPointCheckList(paramModel, "A1A201");

            var viewModel2 = new RiskMgmtOrgSatisfactionViewModel();
            viewModel2.orgSatisfaction = new CheckListViewModel();

            RiskMgmtViewModel viewModel3 = new RiskMgmtViewModel();
            viewModel3.CheckList = await localReportUtil.getGrowthStepPointCheckList(paramModel, "A1A203");

            //1) 현재 사업에 참여한 업체 평균
            //사업참여 기업들의 레벨(창업보육, 보육성장, 자립성장) 분류
            Dictionary<int, int> dicStartUpTotal = new Dictionary<int, int>();
            Dictionary<int, int> dicStartUpMove = new Dictionary<int, int>();

            Dictionary<int, int> dicGrowthTotal = new Dictionary<int, int>();
            Dictionary<int, int> dicGrowthMove = new Dictionary<int, int>();

            Dictionary<int, int> dicIndependentTotal = new Dictionary<int, int>();
            Dictionary<int, int> dicIndependentMove = new Dictionary<int, int>();

            var curBizWork = await scBizWorkService.GetBizWorkByBizWorkSn(int.Parse(paramModel.NumSn));

            List<object> listView = new List<object>();

            SqlParameter numSn = new SqlParameter("NUM_SN", paramModel.NumSn);
            object[] parameters = new object[] { numSn };
            var curBizWorkObj = await procMngService.getBizWorkInfoByBizWorkSn(parameters);


            //
            //검토결과 데이터 생성
            var listRptMentorComment = await rptMentorCommentService.GetRptMentorCommentListAsync(paramModel.QuestionSn, paramModel.NumSn, paramModel.BizWorkYear, "27");

            //레포트 체크리스트
            var enumRptCheckList = await rptCheckListService.GetRptCheckListBySmallClassCd("27");

            //CommentList 채우기
            var CommentList = ReportHelper.MakeCommentViewModel(enumRptCheckList, listRptMentorComment);

            viewModel.CommentList = CommentList;
            viewModel.CommentList = CommentList;
            // ..............................................

            //
            {
                //var compMappings = curBizWork.ScCompMappings.Where(sc => sc.Status == "A");
                foreach (var compMapping in curBizWorkObj)
                {
                    var quesMaster = await quesMasterService.GetQuesMasterAsync(compMapping.REGISTRATION_NO, paramModel.BizWorkYear);
                    if (quesMaster == null)
                    {
                        continue;
                    }

                    //다래 재무정보 유무 체크하는 로직 추가해야함.(문진표정보, 재무정보가 있어야 보고서 생성가능.)
                    //다래 재무정보 조회해야 함.

                    SHUSER_SboFinancialIndexT sboFinacialIndexT;
                    //int companySn = compMapping.ScCompInfo.CompSn;
                    //VcUsrInfo companyUserInfo = await scUsrService.getScUsrByCompSn(companySn);
                    //int CompSn = companyUserInfo.CompSn;
                    //string UseErp = companyUserInfo.UseErp;

                    //if (UseErp == "1")
                    //{
                    //    sboFinacialIndexT = await sboFinancialIndexTService.GetSHUSER_SboFinancialIndexT(compMapping.ScCompInfo.RegistrationNo, ConfigurationManager.AppSettings["CorpCode"], ConfigurationManager.AppSettings["BizCode"], paramModel.BizWorkYear.ToString());
                    //    if (sboFinacialIndexT == null)
                    //    {
                    //        continue;
                    //    }
                    //}
                    //else
                    //{
                    var scFinancialIndexT = await scFinancialIndexTService.getScFinancialIndexTAsync(compMapping.COMP_SN, paramModel.BizWorkYear.ToString());
                    if (scFinancialIndexT == null) continue;

                    sboFinacialIndexT = ConvertMap(scFinancialIndexT);
                    //}

                    // A1A202 : 조직만족도
                    var quesResult2s = await quesResult2Service.GetQuesResult2sAsync(quesMaster.QuestionSn, "A1A202");
                    //총직원
                    var totalEmp = quesResult2s.SingleOrDefault(i => i.QuesCheckList.DetailCd == "A1A20201");
                    //이직직원
                    var moveEmp = quesResult2s.SingleOrDefault(i => i.QuesCheckList.DetailCd == "A1A20202");

                    // 해당기업 계산 부분
                    if (quesMaster.QuestionSn == paramModel.QuestionSn)
                    {
                        if (double.Parse(totalEmp.D451) == 0) // 2015년의 총 직원수가 0이면
                        {
                            viewModel2.orgSatisfaction.Company = "-"; // 계산 불가
                        }
                        else // 2015년의 총 직원수가 0이 아니면
                        {
                            viewModel2.orgSatisfaction.Company = Math.Round(Convert.ToDouble((int.Parse(moveEmp.D451) / double.Parse(totalEmp.D451)) * 100), 1).ToString(); // 우선 계산

                            if (double.IsNaN(double.Parse(viewModel2.orgSatisfaction.Company))) // 계산된 값이 nan 
                            {
                                viewModel2.orgSatisfaction.Company = "-";
                            }
                        }
                    }

                    //종합점수 조회하여 분류별로 딕셔너리 저장
                    var point = await reportUtil.GetCompanyTotalPoint(quesMaster.QuestionSn, ConvertToScMap(sboFinacialIndexT));

                    if (point >= 0 && point <= 50)
                    {
                        if (totalEmp.D451 == null) totalEmp.D451 = "0";
                        if (moveEmp.D451 == null) moveEmp.D451 = "0";
                        dicStartUpTotal.Add(compMapping.COMP_SN, int.Parse(totalEmp.D451));
                        dicStartUpMove.Add(compMapping.COMP_SN, int.Parse(moveEmp.D451));
                    }
                    else if (point > 50 && point <= 75)
                    {
                        if (totalEmp.D451 == null) totalEmp.D451 = "0";
                        if (moveEmp.D451 == null) moveEmp.D451 = "0";
                        dicGrowthTotal.Add(compMapping.COMP_SN, int.Parse(totalEmp.D451));
                        dicGrowthMove.Add(compMapping.COMP_SN, int.Parse(moveEmp.D451));
                    }
                    else
                    {
                        if (totalEmp.D451 == null) totalEmp.D451 = "0";
                        if (moveEmp.D451 == null) moveEmp.D451 = "0";
                        dicIndependentTotal.Add(compMapping.COMP_SN, int.Parse(totalEmp.D451));
                        dicIndependentMove.Add(compMapping.COMP_SN, int.Parse(moveEmp.D451));
                    }
                }
            }

            viewModel2.orgSatisfaction.StartUpAvg = Math.Round(Convert.ToDouble(((dicStartUpMove.Values.Sum() + 100.0) / (dicStartUpTotal.Values.Sum() + 694)) * 100), 1).ToString();

            viewModel2.orgSatisfaction.GrowthAvg = Math.Round(Convert.ToDouble(((dicGrowthMove.Values.Sum() + 185.0) / (dicGrowthTotal.Values.Sum() + 1172)) * 100), 1).ToString();

            viewModel2.orgSatisfaction.IndependentAvg = Math.Round(Convert.ToDouble(((dicIndependentMove.Values.Sum() + 12.0) / (dicIndependentTotal.Values.Sum() + 116)) * 100), 1).ToString();

            viewModel2.orgSatisfaction.BizInCompanyAvg = Math.Round(Convert.ToDouble(((dicStartUpMove.Values.Sum() + dicGrowthMove.Values.Sum() + dicIndependentMove.Values.Sum() + 0.0) / (dicStartUpTotal.Values.Sum() + dicGrowthTotal.Values.Sum() + dicIndependentTotal.Values.Sum())) * 100), 1).ToString();

            viewModel2.orgSatisfaction.TotalAvg = Math.Round(Convert.ToDouble(((dicStartUpMove.Values.Sum() + dicGrowthMove.Values.Sum() + dicIndependentMove.Values.Sum() + 297.0) / (dicStartUpTotal.Values.Sum() + dicGrowthTotal.Values.Sum() + dicIndependentTotal.Values.Sum() + 1982)) * 100), 1).ToString();

            //검토결과 데이터 생성
            var listRptMentorComment2 = await rptMentorCommentService.GetRptMentorCommentListAsync(paramModel.QuestionSn, paramModel.NumSn, paramModel.BizWorkYear, "28");

            //레포트 체크리스트
            var enumRptCheckList2 = await rptCheckListService.GetRptCheckListBySmallClassCd("28");

            //CommentList 채우기
            var CommentList2 = ReportHelper.MakeCommentViewModel(enumRptCheckList2, listRptMentorComment2);

            viewModel2.CommentList = CommentList2;
            // ..............................................


            //
            //검토결과 데이터 생성
            var listRptMentorComment3 = await rptMentorCommentService.GetRptMentorCommentListAsync(paramModel.QuestionSn, paramModel.NumSn, paramModel.BizWorkYear, "29");

            //레포트 체크리스트
            var enumRptCheckList3 = await rptCheckListService.GetRptCheckListBySmallClassCd("29");

            //CommentList 채우기
            var CommentList3 = ReportHelper.MakeCommentViewModel(enumRptCheckList3, listRptMentorComment3);

            viewModel3.CommentList = CommentList3;
            // ..............................................

            listView.Add(viewModel);
            listView.Add(viewModel2);
            listView.Add(viewModel3);

            methodCache["paramModel"] = paramModel;
            methodCache["viewList"] = listView;
            this.HttpContext.Cache.Insert(methodCacheId, methodCache);

            ViewBag.CompNm = paramModel.CompNm;
            ViewBag.paramModel = paramModel;
            return View(listView);
        }

        [HttpPost]
        public async Task<ActionResult> RiskMgmtWorkingEnv(OrgHR01ViewModel viewModel1, BasicSurveyReportViewModel paramModel)
        {
            ViewBag.naviLeftMenu = Global.Report;

            string cacheId = this.ControllerContext.RouteData.Values["action"].ToString() + paramModel.QuestionSn.ToString() + "_";
            string methodCacheId = cacheId + "methodCache";
            this.HttpContext.Cache.Remove(methodCacheId);

            OrgHR01ViewModel viewModel = viewModel1;

            var listRptMentorComment = await rptMentorCommentService.GetRptMentorCommentListAsync(paramModel.QuestionSn, paramModel.NumSn, paramModel.BizWorkYear, "27");

            foreach (var item in viewModel.CommentList)
            {
                var comment = listRptMentorComment.SingleOrDefault(i => i.DetailCd == item.DetailCd);
                if (comment == null)
                {
                    rptMentorCommentService.Insert(ReportHelper.MakeRptMentorcomment(item, paramModel));
                }
                else
                {
                    comment.Comment = item.Comment;
                }
            }
            await rptMentorCommentService.SaveDbContextAsync();

            if (viewModel.SubmitType == "T")
            {
                return RedirectToAction("RiskMgmtWorkingEnv", "BasicSurveyReport", new { NumSn = paramModel.NumSn, CompSn = paramModel.CompSn, BizWorkYear = paramModel.BizWorkYear, Status = paramModel.Status, QuestionSn = paramModel.QuestionSn });
            }
            else
            {
                return RedirectToAction("RiskMgmtLiquidity", "BasicSurveyReport", new { NumSn = paramModel.NumSn, CompSn = paramModel.CompSn, BizWorkYear = paramModel.BizWorkYear, Status = paramModel.Status, QuestionSn = paramModel.QuestionSn });
            }
        }



        //P29 3.위험관리역량 - 정보시스템활용
        public async Task<ActionResult> RiskMgmtITSystem(BasicSurveyReportViewModel paramModel)
        {
            ViewBag.naviLeftMenu = Global.Report;
            ViewBag.paramModel = paramModel;

            LocalReportUtil reportUtil = new LocalReportUtil(scBizWorkService, quesResult1Service, quesResult2Service, quesMasterService, sboFinancialIndexTService, scFinancialIndexTService, scCavService, scUsrService, scCompanyFinanceService, rptMasterService, procMngService);

            RiskMgmtViewModel viewModel = new RiskMgmtViewModel();
            viewModel.CheckList = await reportUtil.getGrowthStepPointCheckList(paramModel, "A1A203");


            //검토결과 데이터 생성
            var listRptMentorComment = await rptMentorCommentService.GetRptMentorCommentListAsync(paramModel.QuestionSn, paramModel.NumSn, paramModel.BizWorkYear, "29");

            //레포트 체크리스트
            var enumRptCheckList = await rptCheckListService.GetRptCheckListBySmallClassCd("29");

            //CommentList 채우기
            var CommentList = ReportHelper.MakeCommentViewModel(enumRptCheckList, listRptMentorComment);

            viewModel.CommentList = CommentList;

            ViewBag.CompNm = paramModel.CompNm;
            ViewBag.paramModel = paramModel;
            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> RiskMgmtITSystem(OrgHR01ViewModel viewModel, BasicSurveyReportViewModel paramModel)
        {
            ViewBag.naviLeftMenu = Global.Report;

            var listRptMentorComment = await rptMentorCommentService.GetRptMentorCommentListAsync(paramModel.QuestionSn, paramModel.NumSn, paramModel.BizWorkYear, "29");

            foreach (var item in viewModel.CommentList)
            {
                var comment = listRptMentorComment.SingleOrDefault(i => i.DetailCd == item.DetailCd);
                if (comment == null)
                {
                    rptMentorCommentService.Insert(ReportHelper.MakeRptMentorcomment(item, paramModel));
                }
                else
                {
                    comment.Comment = item.Comment;
                }
            }
            await rptMentorCommentService.SaveDbContextAsync();

            if (viewModel.SubmitType == "T")
            {
                return RedirectToAction("RiskMgmtITSystem", "BasicSurveyReport", new { NumSn = paramModel.NumSn, CompSn = paramModel.CompSn, BizWorkYear = paramModel.BizWorkYear, Status = paramModel.Status, QuestionSn = paramModel.QuestionSn });
            }
            else
            {
                return RedirectToAction("RiskMgmtLiquidity", "BasicSurveyReport", new { NumSn = paramModel.NumSn, CompSn = paramModel.CompSn, BizWorkYear = paramModel.BizWorkYear, Status = paramModel.Status, QuestionSn = paramModel.QuestionSn });
            }
        }


        //31p 3.위험관리 역량 - 31p. 전문가 평가
        public async Task<ActionResult> RiskMgmtEvalProfession(BasicSurveyReportViewModel paramModel)
        {
            ViewBag.naviLeftMenu = Global.Report;
            ViewBag.paramModel = paramModel;

            string cacheId = this.ControllerContext.RouteData.Values["action"].ToString() + paramModel.QuestionSn.ToString() + "_";
            string methodCacheId = cacheId + "methodCache";

            Boolean cacheStatus = false;
            //string cacheId = "OverallSummary_" + paramModel.QuestionSn.ToString() + "_";
            Dictionary<string, object> methodCache = (Dictionary<string, object>)this.HttpContext.Cache[methodCacheId];

            if (methodCache != null && methodCache.Count > 0)
            {
                cacheStatus = true;
            }
            else
            {
                methodCache = new Dictionary<string, object>();
            }

            if (cacheStatus == true)
            {
                ViewBag.paramModel = methodCache["paramModel"];
                //ViewBag.NullCheck = ViewBag.paramModel.NullCheck;
                //ViewBag.PageNum = methodCache["PageNum"];
                ViewBag.CompNm = ViewBag.paramModel.CompNm;
                //ViewBag.CavYear = methodCache["CavYear"];
                return View(methodCache["viewModel"]);
            }


            RiskMgmtViewModel viewModel = new RiskMgmtViewModel();

            //검토결과 데이터 생성
            var listRptMentorComment = await rptMentorCommentService.GetRptMentorCommentListAsync(paramModel.QuestionSn, paramModel.NumSn, paramModel.BizWorkYear, "31");

            //레포트 체크리스트
            var enumRptCheckList = await rptCheckListService.GetRptCheckListBySmallClassCd("31");

            //CommentList 채우기
            var CommentList = ReportHelper.MakeCommentViewModel(enumRptCheckList.Where(cl => cl.Type == "C"), listRptMentorComment.Where(rmc => rmc.RptCheckList.Type == "C").ToList());

            //체크박스 List 채우기
            var ChekcBoxList = ReportHelper.MakeCheckBoxViewModel(enumRptCheckList.Where(cl => cl.Type == "B"), listRptMentorComment.Where(rmc => rmc.RptCheckList.Type == "B").ToList());

            viewModel.CommentList = CommentList;
            viewModel.CheckBoxList = ChekcBoxList;

            ViewBag.paramModel = paramModel;
            //보고서 상단 회사명 가져오기
            //var scCompMapping = await scCompMappingService.GetCompMappingAsync(paramModel.NumSn, paramModel.CompSn);
            //paramModel.CompNm = scCompMapping.ScCompInfo.CompNm;
            methodCache["paramModel"] = paramModel;
            methodCache["viewModel"] = viewModel;
            //methodCache["CavYear"] = ViewBag.CavYear;
            //methodCache["PageNum"] = rptpagelist1[0].PageNum;
            this.HttpContext.Cache.Insert(methodCacheId, methodCache);

            ViewBag.CompNm = paramModel.CompNm;
            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> RiskMgmtEvalProfession(BasicSurveyReportViewModel paramModel, RiskMgmtViewModel viewModel)
        {
            ViewBag.naviLeftMenu = Global.Report;
            string cacheId = this.ControllerContext.RouteData.Values["action"].ToString() + paramModel.QuestionSn.ToString() + "_";
            string methodCacheId = cacheId + "methodCache";
            this.HttpContext.Cache.Remove(methodCacheId);

            this.HttpContext.Cache.Remove("OverallSummary_" + paramModel.QuestionSn.ToString() + "_ViewBag");
            this.HttpContext.Cache.Remove("OverallSummary_" + paramModel.QuestionSn.ToString() + "_ViewModel");

            var listRptMentorComment = await rptMentorCommentService.GetRptMentorCommentListAsync(paramModel.QuestionSn, paramModel.NumSn, paramModel.BizWorkYear, "31");

            foreach (var item in viewModel.CommentList)
            {
                var comment = listRptMentorComment.SingleOrDefault(i => i.DetailCd == item.DetailCd);
                if (comment == null)
                {
                    rptMentorCommentService.Insert(ReportHelper.MakeRptMentorcomment(item, paramModel));
                }
                else
                {
                    comment.Comment = item.Comment;
                }
            }

            foreach (var item in viewModel.CheckBoxList)
            {
                var comment = listRptMentorComment.SingleOrDefault(i => i.DetailCd == item.DetailCd);
                if (comment == null)
                {
                    rptMentorCommentService.Insert(ReportHelper.MakeRptMentorcomment(item, paramModel));
                }
                else
                {
                    comment.Comment = item.CheckVal.ToString();
                }
            }


            await rptMentorCommentService.SaveDbContextAsync();


            if (viewModel.SubmitType == "T") //임시저장
            {
                return RedirectToAction("RiskMgmtEvalProfession", "BasicSurveyReport", new { NumSn = paramModel.NumSn, CompSn = paramModel.CompSn, BizWorkYear = paramModel.BizWorkYear, Status = paramModel.Status, QuestionSn = paramModel.QuestionSn });
            }
            else
            {
                return RedirectToAction("GrowthRoadMapCover", "BasicSurveyReport", new { NumSn = paramModel.NumSn, CompSn = paramModel.CompSn, BizWorkYear = paramModel.BizWorkYear, Status = paramModel.Status, QuestionSn = paramModel.QuestionSn });
            }
        }

        #endregion

        #region 3. 성장 로드맵제안
        //32p
        public async Task<ActionResult> GrowthRoadMapCover(BasicSurveyReportViewModel paramModel)
        {
            ViewBag.naviLeftMenu = Global.Report;
            ViewBag.paramModel = paramModel;
            //보고서 상단 회사명 가져오기
            //var scCompMapping = await scCompMappingService.GetCompMappingAsync(paramModel.NumSn, paramModel.CompSn);
            //paramModel.CompNm = scCompMapping.ScCompInfo.CompNm;
            var vcCompInfoObj = await vcCompInfoService.getVcCompInfoByCompSn(paramModel.CompSn);
            paramModel.CompNm = vcCompInfoObj.CompNm;

            return View(paramModel);

        }


        //p33 유형별 성장전략
        public async Task<ActionResult> GrowthStrategyType(BasicSurveyReportViewModel paramModel)
        {
            ViewBag.naviLeftMenu = Global.Report;
            ViewBag.paramModel = paramModel;

            GrowthStrategyViewModel viewModel = new GrowthStrategyViewModel();

            //검토결과 데이터 생성
            var listRptMentorComment = await rptMentorCommentService.GetRptMentorCommentListAsync(paramModel.QuestionSn, paramModel.NumSn, paramModel.BizWorkYear, "33");

            //레포트 체크리스트
            var enumRptCheckList = await rptCheckListService.GetRptCheckListBySmallClassCd("33");

            //CommentList 채우기
            var CommentList = ReportHelper.MakeCommentViewModel(enumRptCheckList, listRptMentorComment);


            viewModel.CommentList = CommentList;

            ViewBag.paramModel = paramModel;
            //보고서 상단 회사명 가져오기
            //var scCompMapping = await scCompMappingService.GetCompMappingAsync(paramModel.NumSn, paramModel.CompSn);
            //paramModel.CompNm = scCompMapping.ScCompInfo.CompNm;

            ViewBag.CompNm = paramModel.CompNm;
            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> GrowthStrategyType(BasicSurveyReportViewModel paramModel, GrowthStrategyViewModel viewModel)
        {
            ViewBag.naviLeftMenu = Global.Report;

            var listRptMentorComment = await rptMentorCommentService.GetRptMentorCommentListAsync(paramModel.QuestionSn, paramModel.NumSn, paramModel.BizWorkYear, "33");

            foreach (var item in viewModel.CommentList)
            {
                var comment = listRptMentorComment.SingleOrDefault(i => i.DetailCd == item.DetailCd);
                if (comment == null)
                {
                    rptMentorCommentService.Insert(ReportHelper.MakeRptMentorcomment(item, paramModel));
                }
                else
                {
                    comment.Comment = item.Comment;
                }
            }


            await rptMentorCommentService.SaveDbContextAsync();


            if (viewModel.SubmitType == "T") //임시저장
            {
                return RedirectToAction("GrowthStrategyType", "BasicSurveyReport", new { NumSn = paramModel.NumSn, CompSn = paramModel.CompSn, BizWorkYear = paramModel.BizWorkYear, Status = paramModel.Status, QuestionSn = paramModel.QuestionSn });
            }
            else
            {
                return RedirectToAction("GrowthCapabilityProposal", "BasicSurveyReport", new { NumSn = paramModel.NumSn, CompSn = paramModel.CompSn, BizWorkYear = paramModel.BizWorkYear, Status = paramModel.Status, QuestionSn = paramModel.QuestionSn });
            }
        }

        //p34 단계 성장전략
        public async Task<ActionResult> GrowthStrategyStep(BasicSurveyReportViewModel paramModel)
        {
            ViewBag.naviLeftMenu = Global.Report;
            ViewBag.paramModel = paramModel;

            GrowthStrategyViewModel viewModel = new GrowthStrategyViewModel();

            //검토결과 데이터 생성
            var listRptMentorComment = await rptMentorCommentService.GetRptMentorCommentListAsync(paramModel.QuestionSn, paramModel.NumSn, paramModel.BizWorkYear, "34");

            //레포트 체크리스트
            var enumRptCheckList = await rptCheckListService.GetRptCheckListBySmallClassCd("34");

            //CommentList 채우기
            var CommentList = ReportHelper.MakeCommentViewModel(enumRptCheckList, listRptMentorComment);

            //보고서 상단 회사명 가져오기
            //var scCompMapping = await scCompMappingService.GetCompMappingAsync(paramModel.NumSn, paramModel.CompSn);
            //paramModel.CompNm = scCompMapping.ScCompInfo.CompNm;

            ViewBag.CompNm = paramModel.CompNm;
            viewModel.CommentList = CommentList;
            ViewBag.paramModel = paramModel;

            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> GrowthStrategyStep(BasicSurveyReportViewModel paramModel, GrowthStrategyViewModel viewModel)
        {
            ViewBag.naviLeftMenu = Global.Report;

            var listRptMentorComment = await rptMentorCommentService.GetRptMentorCommentListAsync(paramModel.QuestionSn, paramModel.NumSn, paramModel.BizWorkYear, "34");

            foreach (var item in viewModel.CommentList)
            {
                var comment = listRptMentorComment.SingleOrDefault(i => i.DetailCd == item.DetailCd);
                if (comment == null)
                {
                    rptMentorCommentService.Insert(ReportHelper.MakeRptMentorcomment(item, paramModel));
                }
                else
                {
                    comment.Comment = item.Comment;
                }
            }


            await rptMentorCommentService.SaveDbContextAsync();


            if (viewModel.SubmitType == "T") //임시저장
            {
                return RedirectToAction("GrowthStrategyStep", "BasicSurveyReport", new { NumSn = paramModel.NumSn, CompSn = paramModel.CompSn, BizWorkYear = paramModel.BizWorkYear, Status = paramModel.Status, QuestionSn = paramModel.QuestionSn });
            }
            else
            {
                return RedirectToAction("GrowthCapabilityProposal", "BasicSurveyReport", new { NumSn = paramModel.NumSn, CompSn = paramModel.CompSn, BizWorkYear = paramModel.BizWorkYear, Status = paramModel.Status, QuestionSn = paramModel.QuestionSn });
            }
        }

        //p35 역량강화제안
        public async Task<ActionResult> GrowthCapabilityProposal(BasicSurveyReportViewModel paramModel)
        {
            ViewBag.naviLeftMenu = Global.Report;
            ViewBag.paramModel = paramModel;

            GrowthStrategyViewModel viewModel = new GrowthStrategyViewModel();

            //검토결과 데이터 생성
            var listRptMentorComment = await rptMentorCommentService.GetRptMentorCommentListAsync(paramModel.QuestionSn, paramModel.NumSn, paramModel.BizWorkYear, "35");

            //레포트 체크리스트
            var enumRptCheckList = await rptCheckListService.GetRptCheckListBySmallClassCd("35");

            //CommentList 채우기
            var CommentList = ReportHelper.MakeCommentViewModel(enumRptCheckList, listRptMentorComment);

            //보고서 상단 회사명 가져오기
            //var scCompMapping = await scCompMappingService.GetCompMappingAsync(paramModel.NumSn, paramModel.CompSn);
            //paramModel.CompNm = scCompMapping.ScCompInfo.CompNm;

            ViewBag.CompNm = paramModel.CompNm;
            viewModel.CommentList = CommentList;
            ViewBag.paramModel = paramModel;

            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> GrowthCapabilityProposal(BasicSurveyReportViewModel paramModel, GrowthStrategyViewModel viewModel)
        {
            ViewBag.naviLeftMenu = Global.Report;

            var listRptMentorComment = await rptMentorCommentService.GetRptMentorCommentListAsync(paramModel.QuestionSn, paramModel.NumSn, paramModel.BizWorkYear, "35");

            foreach (var item in viewModel.CommentList)
            {
                var comment = listRptMentorComment.SingleOrDefault(i => i.DetailCd == item.DetailCd);
                if (comment == null)
                {
                    rptMentorCommentService.Insert(ReportHelper.MakeRptMentorcomment(item, paramModel));
                }
                else
                {
                    comment.Comment = item.Comment;
                }
            }

            //await rptMentorCommentService.SaveDbContextAsync();

            if (viewModel.SubmitType == "T") //임시저장
            {
                await rptMentorCommentService.SaveDbContextAsync();
                return RedirectToAction("GrowthTotalProposal", "BasicSurveyReport", new { NumSn = paramModel.NumSn, CompSn = paramModel.CompSn, BizWorkYear = paramModel.BizWorkYear, Status = paramModel.Status, QuestionSn = paramModel.QuestionSn });
            }
            else
            {
                //return RedirectToAction("CapacityCompGoal", "BasicSurveyReport", new { BizWorkSn = paramModel.NumSn, CompSn = paramModel.CompSn, BizWorkYear = paramModel.BizWorkYear, Status = paramModel.Status, QuestionSn = paramModel.QuestionSn });
                var rptMater = await rptMasterService.GetRptMasterAsync(paramModel.QuestionSn, paramModel.CompSn, paramModel.BizWorkYear);
                rptMater.Status = "C";
                rptMasterService.ModifyRptMaster(rptMater);

                await rptMentorCommentService.SaveDbContextAsync();
                return RedirectToAction("BasicSurveyReport", "Report", new { area = "Mentor" });
            }

            //if (viewModel.SubmitType == "T") //임시저장
            //{
            //    return RedirectToAction("GrowthCapabilityProposal", "BasicSurveyReport", new { NumSn = paramModel.NumSn, CompSn = paramModel.CompSn, BizWorkYear = paramModel.BizWorkYear, Status = paramModel.Status, QuestionSn = paramModel.QuestionSn });
            //}
            //else
            //{
            //    return RedirectToAction("GrowthTotalProposal", "BasicSurveyReport", new { NumSn = paramModel.NumSn, CompSn = paramModel.CompSn, BizWorkYear = paramModel.BizWorkYear, Status = paramModel.Status, QuestionSn = paramModel.QuestionSn });
            //}
        }

        //p36 회사핵심내용
        public async Task<ActionResult> GrowthTotalProposal(BasicSurveyReportViewModel paramModel)
        {
            ViewBag.naviLeftMenu = Global.Report;
            ViewBag.paramModel = paramModel;

            GrowthStrategyViewModel viewModel = new GrowthStrategyViewModel();

            //검토결과 데이터 생성
            var listRptMentorComment = await rptMentorCommentService.GetRptMentorCommentListAsync(paramModel.QuestionSn, paramModel.NumSn, paramModel.BizWorkYear, "36");

            //레포트 체크리스트
            var enumRptCheckList = await rptCheckListService.GetRptCheckListBySmallClassCd("36");

            //CommentList 채우기
            var CommentList = ReportHelper.MakeCommentViewModel(enumRptCheckList, listRptMentorComment);

            //보고서 상단 회사명 가져오기
            //var scCompMapping = await scCompMappingService.GetCompMappingAsync(paramModel.NumSn, paramModel.CompSn);
            //paramModel.CompNm = scCompMapping.ScCompInfo.CompNm;

            ViewBag.CompNm = paramModel.CompNm;
            viewModel.CommentList = CommentList;
            ViewBag.paramModel = paramModel;

            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> GrowthTotalProposal(BasicSurveyReportViewModel paramModel, GrowthStrategyViewModel viewModel)
        {
            ViewBag.naviLeftMenu = Global.Report;

            var listRptMentorComment = await rptMentorCommentService.GetRptMentorCommentListAsync(paramModel.QuestionSn, paramModel.NumSn, paramModel.BizWorkYear, "36");

            foreach (var item in viewModel.CommentList)
            {
                var comment = listRptMentorComment.SingleOrDefault(i => i.DetailCd == item.DetailCd);
                if (comment == null)
                {
                    rptMentorCommentService.Insert(ReportHelper.MakeRptMentorcomment(item, paramModel));
                }
                else
                {
                    comment.Comment = item.Comment;
                }
            }

            if (viewModel.SubmitType == "T") //임시저장
            {
                await rptMentorCommentService.SaveDbContextAsync();
                return RedirectToAction("GrowthTotalProposal", "BasicSurveyReport", new { NumSn = paramModel.NumSn, CompSn = paramModel.CompSn, BizWorkYear = paramModel.BizWorkYear, Status = paramModel.Status, QuestionSn = paramModel.QuestionSn });
            }
            else
            {
                //return RedirectToAction("CapacityCompGoal", "BasicSurveyReport", new { BizWorkSn = paramModel.NumSn, CompSn = paramModel.CompSn, BizWorkYear = paramModel.BizWorkYear, Status = paramModel.Status, QuestionSn = paramModel.QuestionSn });
                var rptMater = await rptMasterService.GetRptMasterAsync(paramModel.QuestionSn, paramModel.CompSn, paramModel.BizWorkYear);
                rptMater.Status = "C";
                rptMasterService.ModifyRptMaster(rptMater);

                await rptMentorCommentService.SaveDbContextAsync();
                return RedirectToAction("BasicSurveyReport", "Report", new { area = "Mentor" });
            }

        }
        #endregion

        [HttpPost]
        public async Task<JsonResult> CheckFinanceData(int CompSn, int BasicYear)
        {
            //var scUsr = await scUsrService.getScUsrByCompSn(CompSn);
            var compInfo = await vcCompInfoService.getVcCompInfoByCompSn(CompSn);

            //if (scUsr.UseErp.Equals("1"))
            //{
            //    //다래 재무정보 조회해야 함.
            //    var sboFinacialIndexT = await sboFinancialIndexTService.GetSHUSER_SboFinancialIndexT(compInfo.RegistrationNo, ConfigurationManager.AppSettings["CorpCode"], ConfigurationManager.AppSettings["BizCode"], BasicYear.ToString());
            //    if (sboFinacialIndexT != null)
            //    {
            //        return Json(new { result = true });
            //    }
            //    else
            //    {
            //        return Json(new { result = false });
            //    }
            //}
            //else
            //{
            var scFinancialIndexT = await scFinancialIndexTService.getScFinancialIndexTAsync(CompSn, (BasicYear - 1).ToString());   // 이전년도? 재무데이터를 가져와야 하므로
            if (scFinancialIndexT != null)
            {
                return Json(new { result = true });
            }
            else
            {
                return Json(new { result = false });
            }
            //}
        }

        public ScFinancialIndexT ConvertToScMap(SHUSER_SboFinancialIndexT sboFinancialIndexT)
        {
            var scFinancialIndexT = new ScFinancialIndexT();
            scFinancialIndexT.CurrentAsset = sboFinancialIndexT.CurrentAsset;
            scFinancialIndexT.CurrentEarning = sboFinancialIndexT.CurrentEarning;
            scFinancialIndexT.CurrentLiability = sboFinancialIndexT.CurrentLiability;
            scFinancialIndexT.CurrentSale = sboFinancialIndexT.CurrentSale;
            scFinancialIndexT.InterstCost = sboFinancialIndexT.InterstCost;
            scFinancialIndexT.InventoryAsset = sboFinancialIndexT.InventoryAsset;
            scFinancialIndexT.MaterialCost = sboFinancialIndexT.MaterialCost;
            scFinancialIndexT.NonOperEar = sboFinancialIndexT.NonOperEar;
            scFinancialIndexT.OperatingEarning = sboFinancialIndexT.OperatingEarning;
            scFinancialIndexT.PrevEarning = sboFinancialIndexT.PrevEarning;
            scFinancialIndexT.PrevSale = sboFinancialIndexT.PrevSale;
            scFinancialIndexT.QtEmp = sboFinancialIndexT.QtEmp;
            scFinancialIndexT.ReserchAmt = sboFinancialIndexT.ReserchAmt;
            scFinancialIndexT.SalesCredit = sboFinancialIndexT.SalesCredit;
            scFinancialIndexT.TotalAsset = sboFinancialIndexT.TotalAsset;
            scFinancialIndexT.TotalCapital = sboFinancialIndexT.TotalCapital;
            scFinancialIndexT.TotalLiability = sboFinancialIndexT.TotalLiability;
            scFinancialIndexT.ValueAdded = sboFinancialIndexT.ValueAdded;
            scFinancialIndexT.Year = sboFinancialIndexT.Year;
            return scFinancialIndexT;
        }


        public SHUSER_SboFinancialIndexT ConvertMap(ScFinancialIndexT scFinancialIndexT)
        {
            var sboFinancialIndexT = new SHUSER_SboFinancialIndexT();

            sboFinancialIndexT.CurrentAsset = scFinancialIndexT.CurrentAsset;
            sboFinancialIndexT.CurrentEarning = scFinancialIndexT.CurrentEarning;
            sboFinancialIndexT.CurrentLiability = scFinancialIndexT.CurrentLiability;
            sboFinancialIndexT.CurrentSale = scFinancialIndexT.CurrentSale;
            sboFinancialIndexT.InterstCost = scFinancialIndexT.InterstCost;
            sboFinancialIndexT.InventoryAsset = scFinancialIndexT.InventoryAsset;
            sboFinancialIndexT.MaterialCost = scFinancialIndexT.MaterialCost;
            sboFinancialIndexT.NonOperEar = scFinancialIndexT.NonOperEar;
            sboFinancialIndexT.OperatingEarning = scFinancialIndexT.OperatingEarning;
            sboFinancialIndexT.PrevEarning = scFinancialIndexT.PrevEarning;
            sboFinancialIndexT.PrevSale = scFinancialIndexT.PrevSale;
            sboFinancialIndexT.QtEmp = scFinancialIndexT.QtEmp;
            sboFinancialIndexT.ReserchAmt = scFinancialIndexT.ReserchAmt;
            sboFinancialIndexT.SalesCredit = scFinancialIndexT.SalesCredit;
            sboFinancialIndexT.TotalAsset = scFinancialIndexT.TotalAsset;
            sboFinancialIndexT.TotalCapital = scFinancialIndexT.TotalCapital;
            sboFinancialIndexT.TotalLiability = scFinancialIndexT.TotalLiability;
            sboFinancialIndexT.ValueAdded = scFinancialIndexT.ValueAdded;
            sboFinancialIndexT.Year = scFinancialIndexT.Year;

            return sboFinancialIndexT;
        }
        //다른 멘토의견 가져오기
        //public async Task<ActionResult> RptCommentPop(string bizWorkSn, string questionSn, string detailCode, string compSn)
        //{
        //    var mentorId = Session[Global.LoginID].ToString();

        //    var rptCommentPop = await rptMentorCommentService.GetRptMentorCommentSelectAsync(detailCode, int.Parse(compSn), int.Parse(bizWorkSn));
        //    var regCommentViewModels = Mapper.Map<List<SelectMentorCommentViewModel>>(rptCommentPop);

        //    return View(regCommentViewModels);

        //}
        //reportcover
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
            //paramModel.BizWorkNm = scCompMapping.ScBizWork.BizWorkNm;'

            ViewBag.CompNm = paramModel.CompNm;
            ViewBag.paramModel = paramModel;
            ViewBag.NumSn = paramModel.NumSn;
            ViewBag.CompSn = paramModel.CompSn;
            ViewBag.BizWorkYear = paramModel.BizWorkYear;
            ViewBag.Status = paramModel.Status;
            ViewBag.QuestionSn = paramModel.QuestionSn;

            return View(paramModel);

        }

        // 새로운 page 3개 추가 
        public async Task<ActionResult> CapacityCompGoal(BasicSurveyReportViewModel paramModel)
        {
            ViewBag.naviLeftMenu = Global.Report;
            ViewBag.paramModel = paramModel;

            if (paramModel.CompSn == 0 || paramModel.NumSn == "0")
            {
                return View(paramModel);
            }

            //var scCompMapping = await scCompMappingService.GetCompMappingAsync(paramModel.NumSn, paramModel.CompSn);
            //paramModel.CompNm = scCompMapping.ScCompInfo.CompNm;
            //paramModel.BizWorkNm = scCompMapping.ScBizWork.BizWorkNm;

            ViewBag.paramModel = paramModel;

            return View(paramModel);
        }

        public async Task<ActionResult> CapacityDiffStartUp(BasicSurveyReportViewModel paramModel)
        {
            ViewBag.naviLeftMenu = Global.Report;
            ViewBag.paramModel = paramModel;

            if (paramModel.CompSn == 0 || paramModel.NumSn == "0")
            {
                return View(paramModel);
            }

            //var scCompMapping = await scCompMappingService.GetCompMappingAsync(paramModel.NumSn, paramModel.CompSn);

            //paramModel.CompNm = scCompMapping.ScCompInfo.CompNm;
            //paramModel.BizWorkNm = scCompMapping.ScBizWork.BizWorkNm;

            ViewBag.paramModel = paramModel;

            return View(paramModel);
        }

        public async Task<ActionResult> CapacityCompGrowth(BasicSurveyReportViewModel paramModel)
        {
            ViewBag.naviLeftMenu = Global.Report;
            ViewBag.paramModel = paramModel;

            GrowthStrategyViewModel viewModel = new GrowthStrategyViewModel();

            if (paramModel.CompSn == 0 || paramModel.NumSn == "0")
            {
                return View(paramModel);
            }

            //var scCompMapping = await scCompMappingService.GetCompMappingAsync(paramModel.NumSn, paramModel.CompSn);

            //paramModel.CompNm = scCompMapping.ScCompInfo.CompNm;
            //paramModel.BizWorkNm = scCompMapping.ScBizWork.BizWorkNm;

            ViewBag.paramModel = paramModel;

            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> CapacityCompGrowth(BasicSurveyReportViewModel paramModel, GrowthStrategyViewModel viewModel)
        {
            ViewBag.naviLeftMenu = Global.Report;

            var listRptMentorComment = await rptMentorCommentService.GetRptMentorCommentListAsync(paramModel.QuestionSn, paramModel.NumSn, paramModel.BizWorkYear, "36");

            if (viewModel.SubmitType == "T") //임시저장
            {
                await rptMentorCommentService.SaveDbContextAsync();
                return RedirectToAction("CapacityCompGrowth", "BasicSurveyReport", new { NumSn = paramModel.NumSn, CompSn = paramModel.CompSn, BizWorkYear = paramModel.BizWorkYear, Status = paramModel.Status, QuestionSn = paramModel.QuestionSn });
            }
            else
            {
                var rptMater = await rptMasterService.GetRptMasterAsync(paramModel.QuestionSn, paramModel.CompSn, paramModel.BizWorkYear);
                rptMater.Status = "C";
                rptMasterService.ModifyRptMaster(rptMater);

                await rptMentorCommentService.SaveDbContextAsync();
                return RedirectToAction("BasicSurveyReport", "Report", new { area = "Mentor" });
            }

        }

        //[HttpPost]
        //public async Task<JsonResult> RptNullCheckJson(int ques, int biz, int year)
        //{
        //    ViewBag.naviLeftMenu = Global.Report;
        //    //navi
        //    var rptquestion = await quesMasterService.GetQuesMasterAsync(ques);
        //    var rptpagelist1 = await rptPageViewService.GetRptPageViewListAsync(ques, biz, year);

        //    int[] pages = new int[] { 3, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 20, 22, 23, 24, 25, 26, 27, 28, 29 };//24page
        //    string[] nCheckArray = new string[30];
        //    int i = 0;
        //    string Nochange = "N";

        //    if (rptpagelist1.Count == 0)
        //    {
        //        for (int k = 0; k < pages.Length; k++)
        //        {
        //            nCheckArray[pages[k]] = "N";
        //        }
        //    }

        //    for (int j = 0; j < pages.Length; j++)
        //    {
        //        while (i < rptpagelist1.Count)
        //        {
        //            if (rptpagelist1[i].PageNum == pages[j])
        //            {
        //                if (rptpagelist1[i].Comment == null)
        //                {
        //                    nCheckArray[pages[j]] = "N";
        //                    i++;
        //                }
        //                else
        //                {
        //                    if (nCheckArray[pages[j]] == Nochange)
        //                    {
        //                        i++;
        //                    }
        //                    else
        //                    {
        //                        nCheckArray[pages[j]] = "Y";
        //                        i++;
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                break;
        //            }
        //        }
        //    }
        //    ViewBag.nCheckArray = nCheckArray;
        //    return Json(nCheckArray, JsonRequestBehavior.AllowGet);
        //}


        public async Task<ActionResult> FinanceStatements(BasicSurveyReportViewModel paramModel)
        {
            ViewBag.naviLeftMenu = Global.Report;

            FinanceCheckViewModel viewModel = new FinanceCheckViewModel();

            viewModel.QuestionSn = paramModel.QuestionSn;
            viewModel.Status = paramModel.Status;
            viewModel.BasicYear = paramModel.BizWorkYear;

            var maxYear = await scCompanyFinanceService.getMaxYearCompanyFinanceAsync(paramModel.CompSn); // 해당 기업이 가지고 있는 최고 년도를 가져오기

            //var quesMaster = await quesMasterService.GetQuesMasterAsync(paramModel.QuestionSn);

            var cur = await scCompanyFinanceService.getScCompanyFinanceAsync(paramModel.CompSn, maxYear);
            var pre = await scCompanyFinanceService.getScCompanyFinanceAsync(paramModel.CompSn, maxYear - 1);

            if (cur != null)
            {
                var curModel = Mapper.Map<FinanceCompositionViewModel>(cur);
                viewModel.Current = curModel;
            }

            if (pre != null)
            {
                var preModel = Mapper.Map<FinanceCompositionViewModel>(pre);
                viewModel.Previous = preModel;
            }

            return View(viewModel);
        }

        public async Task<ActionResult> FinanceIncomeStatements(BasicSurveyReportViewModel paramModel)
        {
            ViewBag.naviLeftMenu = Global.Report;

            FinanceCheckViewModel viewModel = new FinanceCheckViewModel();

            //var quesMaster = await quesMasterService.GetQuesMasterAsync(paramModel.QuestionSn);

            viewModel.QuestionSn = paramModel.QuestionSn;
            viewModel.Status = paramModel.Status;
            viewModel.BasicYear = paramModel.BizWorkYear;

            var maxYear = await scCompanyFinanceService.getMaxYearCompanyFinanceAsync(paramModel.CompSn); // 해당 기업이 가지고 있는 최고 년도를 가져오기

            //var quesMaster = await quesMasterService.GetQuesMasterAsync(paramModel.QuestionSn);

            var cur = await scCompanyFinanceService.getScCompanyFinanceAsync(paramModel.CompSn, maxYear);
            var pre = await scCompanyFinanceService.getScCompanyFinanceAsync(paramModel.CompSn, maxYear - 1);

            if (cur != null)
            {
                var curModel = Mapper.Map<FinanceCompositionViewModel>(cur);
                viewModel.Current = curModel;
            }

            if (pre != null)
            {
                var preModel = Mapper.Map<FinanceCompositionViewModel>(pre);
                viewModel.Previous = preModel;
            }

            return View(viewModel);
        }

        public async Task<ActionResult> ManufacturingCostStatements(BasicSurveyReportViewModel paramModel)
        {
            ViewBag.naviLeftMenu = Global.Report;

            FinanceCheckViewModel viewModel = new FinanceCheckViewModel();

            //var quesMaster = await quesMasterService.GetQuesMasterAsync(paramModel.QuestionSn);

            viewModel.QuestionSn = paramModel.QuestionSn;
            viewModel.Status = paramModel.Status;
            viewModel.BasicYear = paramModel.BizWorkYear;

            var maxYear = await scCompanyFinanceService.getMaxYearCompanyFinanceAsync(paramModel.CompSn); // 해당 기업이 가지고 있는 최고 년도를 가져오기

            //var quesMaster = await quesMasterService.GetQuesMasterAsync(paramModel.QuestionSn);

            var cur = await scCompanyFinanceService.getScCompanyFinanceAsync(paramModel.CompSn, maxYear);
            var pre = await scCompanyFinanceService.getScCompanyFinanceAsync(paramModel.CompSn, maxYear - 1);

            if (cur != null)
            {
                var curModel = Mapper.Map<FinanceCompositionViewModel>(cur);
                viewModel.Current = curModel;
            }

            if (pre != null)
            {
                var preModel = Mapper.Map<FinanceCompositionViewModel>(pre);
                viewModel.Previous = preModel;
            }

            return View(viewModel);
        }
    }
}