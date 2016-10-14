using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using BizOneShot.Light.Models.ViewModels;
using BizOneShot.Light.Models.WebModels;
using BizOneShot.Light.Services;
using BizOneShot.Light.Web.ComLib;
using System.Data.SqlClient;
using System.Configuration;
using BizOneShot.Light.Util.Helper;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;
using System.Text;
using BizOneShot.Light.Models.CustomModels;

namespace BizOneShot.Light.Web.Areas.Company.Controllers
{
    [UserAuthorize(Order = 1)]
    [MenuAuthorize(Roles = UserType.Company, Order = 2)]
    public class ReportController : BaseController
    {
        private readonly IQuesMasterService _quesMasterService;
        private readonly IQuesCompInfoService _quesCompInfoService;
        private readonly IQuesCheckListService _quesCheckListService;
        private readonly IQuesResult1Service _quesResult1Service;
        private readonly IQuesResult2Service _quesResult2Service;
        private readonly IScUsrService _scUsrService;
        private readonly IScCompMappingService _scCompMappingService;
        private readonly IRptMasterService _rptMasterService;
        private readonly IScCompanyFinanceService _scCompanyFinanceService;
        private readonly IScFinancialIndexTService _scFinancialIndexTService;
        private readonly IVcBaInfoService _vcBaInfoService;
        private readonly IVcCompInfoService _vcCompInfoService;
        private readonly ICompMentorBizSurveyCompleteService _compMentorBizSurveyCompleteService;
        private readonly IVcSatCheckService _VcSatCheckService;
        private readonly IVcLastReportNSatService _VcLastReportNSatService;
        private readonly IVcMentorMappingService vcMentorMappingService;
        private readonly ICompBaMngService compBaMngService;
        private readonly IProcMngService _procMngService;
        private readonly IScMentoringTotalReportService _scMentoringTotalReportService;
        private readonly IScMentoringFileInfoService _scMentoringFileInfoService;
        private readonly IScMentoringTrFileInfoService _scMentoringTrFileInfoService;
        private readonly ITcmsIfSurveyService _tcmsIfSurveyService;
        private readonly IVcMentorInfoSerivce _vcMentorInfoService;

        public ReportController(IQuesMasterService _quesMasterService, 
            IQuesCompInfoService _quesCompInfoService, 
            IScUsrService _scUsrService, 
            IQuesCheckListService _quesCheckListService, 
            IQuesResult1Service _quesResult1Service, 
            IQuesResult2Service _quesResult2Service, 
            IScCompMappingService _scCompMappingService, 
            IRptMasterService _rptMasterService, 
            IScCompanyFinanceService _scCompanyFinanceService, 
            IScFinancialIndexTService _scFinancialIndexTService, 
            IVcBaInfoService _vcBaInfoService,
            IVcCompInfoService _vcCompInfoService,
            ICompMentorBizSurveyCompleteService _compMentorBizSurveyCompleteService,
            IVcSatCheckService _VcSatCheckService,
            IVcLastReportNSatService _VcLastReportNSatService,
            IVcMentorMappingService vcMentorMappingService,
            ICompBaMngService compBaMngService,
            IProcMngService _procMngService,
            IScMentoringTotalReportService _scMentoringTotalReportService,
            IScMentoringFileInfoService _scMentoringFileInfoService,
            IScMentoringTrFileInfoService _scMentoringTrFileInfoService,
            ITcmsIfSurveyService _tcmsIfSurveyService,
            IVcMentorInfoSerivce _vcMentorInfoService)
        {
            this._quesMasterService = _quesMasterService;
            this._scUsrService = _scUsrService;
            this._quesCompInfoService = _quesCompInfoService;
            this._quesCheckListService = _quesCheckListService;
            this._quesResult1Service = _quesResult1Service;
            this._quesResult2Service = _quesResult2Service;
            this._scCompMappingService = _scCompMappingService;
            this._rptMasterService = _rptMasterService;
            this._scCompanyFinanceService = _scCompanyFinanceService;
            this._scFinancialIndexTService = _scFinancialIndexTService;
            this._VcSatCheckService = _VcSatCheckService;
            this._vcBaInfoService = _vcBaInfoService;
            this._vcCompInfoService = _vcCompInfoService;
            this._compMentorBizSurveyCompleteService = _compMentorBizSurveyCompleteService;
            this._VcLastReportNSatService = _VcLastReportNSatService;
            this._procMngService = _procMngService;
            this._scMentoringTotalReportService = _scMentoringTotalReportService;
            this._scMentoringFileInfoService = _scMentoringFileInfoService;
            this._scMentoringTrFileInfoService = _scMentoringTrFileInfoService;
            this.vcMentorMappingService = vcMentorMappingService;
            this._tcmsIfSurveyService = _tcmsIfSurveyService;
            this._vcMentorInfoService = _vcMentorInfoService;
        }

        // GET: Company/Report
        public ActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult> BasicSurvey()
        {
            ViewBag.naviLeftMenu = Global.Report;

            var vcBaInfo = await _vcCompInfoService.getVcCompInfoById(Session[Global.LoginID].ToString());
            var compSn = vcBaInfo.CompSn;

            //var compSn = Session[Global.CompSN];

            //문진표 List Data
            var listQuesMaster = await _quesMasterService.GetQuesMastersAsync(vcBaInfo.RegistrationNo);

            //var scCompMapping = await _scCompMappingService.GetCompMappingAsync(int.Parse(compSn.ToString()), null);

            //ViewBag.scCompMapping = scCompMapping.Status;

            var questionDropDown =
                Mapper.Map<List<QuestionDropDownModel>>(listQuesMaster);

            if (questionDropDown.Count() == 0 || questionDropDown[0].BasicYear < DateTime.Now.Year)
            {
                //사업담당자 일 경우 담당 사업만 조회
                QuestionDropDownModel title = new QuestionDropDownModel();
                title.SnStatus = "#P";
                title.BasicYear = DateTime.Now.Year;
                questionDropDown.Insert(0, title);
            }

            SelectList questionList = new SelectList(questionDropDown, "SnStatus", "BasicYear");
            ViewBag.SelectQuestionList = questionList;

            return View();
        }

        public async Task<ActionResult> Summary01(string questionSn, string status)
        {
            ViewBag.naviLeftMenu = Global.Report;

            if(!string.IsNullOrEmpty(questionSn))
            { 
                var quesMaster = await _quesMasterService.GetQuesMasterAsync(int.Parse(questionSn));
                var quesWriter = quesMaster.QuesWriter;
                var quesMasterView = Mapper.Map<QuesMasterViewModel>(quesMaster);
                var quesWriterView = Mapper.Map<QuesWriterViewModel>(quesWriter);
                quesMasterView.QuesWriter = quesWriterView;

                return View(quesMasterView);
            }
            else
            {
                VcUsrInfo scUsr = await _scUsrService.SelectScUsr(Session[Global.LoginID].ToString());
                var quesMasterViewModel = new QuesMasterViewModel();
                var quesWriterViewModel = new QuesWriterViewModel();
                //quesMasterViewModel.RegistrationNo = scUsr.ScCompInfo.RegistrationNo;
                //quesWriterViewModel.CompNm = scUsr.ScCompInfo.CompNm;
                //quesWriterViewModel.DeptNm = scUsr.DeptNm;
                quesWriterViewModel.Email = scUsr.Email;
                quesWriterViewModel.Name = scUsr.Name;
                quesMasterViewModel.QuesWriter = quesWriterViewModel;
                quesMasterViewModel.Status = status;
                return View(quesMasterViewModel);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Summary01(QuesMasterViewModel quesMasterViewModel)
        {
            ViewBag.naviLeftMenu = Global.Report;
            int questionSn = quesMasterViewModel.QuestionSn;

            if (quesMasterViewModel.QuestionSn > 0)
            {
                var quesMaster = await _quesMasterService.GetQuesMasterAsync(quesMasterViewModel.QuestionSn);

                quesMaster.QuesWriter.CompNm = quesMasterViewModel.QuesWriter.CompNm;
                quesMaster.QuesWriter.Name = quesMasterViewModel.QuesWriter.Name;
                quesMaster.QuesWriter.Position = quesMasterViewModel.QuesWriter.Position;
                quesMaster.QuesWriter.TelNo = quesMasterViewModel.QuesWriter.TelNo;
                quesMaster.QuesWriter.Email = quesMasterViewModel.QuesWriter.Email;
                quesMaster.QuesWriter.UpdDt = DateTime.Now;
                quesMaster.QuesWriter.UpdId = Session[Global.LoginID].ToString();

                if (quesMasterViewModel.SubmitType == "T")
                {
                    quesMaster.SaveStatus = 1;
                }
                else
                {
                    quesMaster.SaveStatus = 2;
                }

                await _quesMasterService.SaveDbContextAsync();
            }
            else
            {
                var quesMaster = new QuesMaster();
                quesMaster.BasicYear = DateTime.Now.Year;
                quesMaster.RegistrationNo = Session[Global.CompRegistrationNo].ToString();
                quesMaster.Status = "P";
                if (quesMasterViewModel.SubmitType == "T")
                {
                    quesMaster.SaveStatus = 1;
                }
                else
                {
                    quesMaster.SaveStatus = 2;
                }

                var quesWriter = Mapper.Map<QuesWriter>(quesMasterViewModel.QuesWriter);
                quesWriter.RegDt = DateTime.Now;
                quesWriter.RegId = Session[Global.LoginID].ToString();
                quesMaster.QuesWriter = quesWriter;

                var saveQuesMaster = await _quesMasterService.AddQuesMasterAsync(quesMaster);
                questionSn = saveQuesMaster.QuestionSn;
            } 

            if (quesMasterViewModel.SubmitType == "T")
            {
                return RedirectToAction("Summary01", "Report", new { @questionSn = questionSn });
            }
            else
            {
                return RedirectToAction("Summary02", "Report", new { @questionSn = questionSn });
            }
        }

        public ActionResult Summary02(string questionSn, string status)
        {
            ViewBag.naviLeftMenu = Global.Report;
            var quesViewModel = new QuesViewModel();
            quesViewModel.QuestionSn = int.Parse(questionSn);
            quesViewModel.Status = status;
            return View(quesViewModel);
        }

        public async Task<ActionResult> CompanyInfo01(string questionSn, string status)
        {
            ViewBag.naviLeftMenu = Global.Report;

            if (string.IsNullOrEmpty(questionSn))
            {
                // 오류 처리해야함.
                return View();
            }

            var quesCompInfo = await _quesCompInfoService.GetQuesCompInfoAsync(int.Parse(questionSn));

            if (quesCompInfo == null)
            {
                VcUsrInfo scUsr = await _scUsrService.SelectScUsr(Session[Global.LoginID].ToString());
                var quesCompInfoViewModel = new QuesCompanyInfoViewModel();
                quesCompInfoViewModel.QuestionSn = int.Parse(questionSn);
                //quesCompInfoViewModel.CompAddr = "(" + scUsr.ScCompInfo.PostNo + ") " + scUsr.ScCompInfo.Addr1 + " " + scUsr.ScCompInfo.Addr2;
                //quesCompInfoViewModel.CompNm = scUsr.ScCompInfo.CompNm;
                //quesCompInfoViewModel.TelNo = scUsr.ScCompInfo.TelNo;
                //quesCompInfoViewModel.Name = scUsr.ScCompInfo.OwnNm;
                //quesCompInfoViewModel.Email = scUsr.ScCompInfo.Email;
                //quesCompInfoViewModel.RegistrationNo = scUsr.ScCompInfo.RegistrationNo;
                quesCompInfoViewModel.Status = status;
                return View(quesCompInfoViewModel);
            }
            else
            {
                var quesCompInfoView = Mapper.Map<QuesCompanyInfoViewModel>(quesCompInfo);
                if (quesCompInfoView.PublishDt == "0001-01-01")
                    quesCompInfoView.PublishDt = null;
                quesCompInfoView.Status = status;
                return View(quesCompInfoView);
            }

        }

        [HttpPost]
        public async Task<ActionResult> CompanyInfo01(QuesCompanyInfoViewModel quesCompanyInfoViewModel)
        {
            ViewBag.naviLeftMenu = Global.Report;
            int questionSn = quesCompanyInfoViewModel.QuestionSn;

            if (quesCompanyInfoViewModel.QuestionSn > 0)
            {
                var quesMaster = await _quesMasterService.GetQuesCompInfoAsync(questionSn);

                if (quesCompanyInfoViewModel.SubmitType == "N")
                {
                    quesMaster.SaveStatus = 3;
                }

                var quesCompInfo = Mapper.Map<QuesCompInfo>(quesCompanyInfoViewModel);
                if (quesMaster.QuesCompInfo == null)
                {
                    quesCompInfo.RegDt = DateTime.Now;
                    //quesCompInfo.RegId = Session[Global.LoginID].ToString();
                }
                else
                {
                    quesCompInfo.RegDt = quesMaster.QuesCompInfo.RegDt;
                    //quesCompInfo.RegId = quesMaster.QuesCompInfo.RegId;
                    quesCompInfo.UpdDt = DateTime.Now;
                    //quesCompInfo.UpdId = Session[Global.LoginID].ToString();
                }
                quesMaster.QuesCompInfo = quesCompInfo;
                await _quesMasterService.SaveDbContextAsync();
            }
            else
            {
                //에러처리 필요
                return View(quesCompanyInfoViewModel);
            }

            if (quesCompanyInfoViewModel.SubmitType == "T")
            {
                return RedirectToAction("CompanyInfo01", "Report", new { @questionSn = questionSn });
            }
            else
            {
                return RedirectToAction("CompanyInfo02", "Report", new { @questionSn = questionSn });
            }
        }

        public async Task<ActionResult> CompanyInfo02(string questionSn, string status)
        {
            ViewBag.naviLeftMenu = Global.Report;

            if (string.IsNullOrEmpty(questionSn))
            {
                // 오류 처리해야함.
                return View();
            }

            var quesMaster = await _quesMasterService.GetQuesCompExtentionAsync(int.Parse(questionSn));

            if (quesMaster.QuesCompExtention == null)
            {
                VcUsrInfo scUsr = await _scUsrService.SelectScUsr(Session[Global.LoginID].ToString());
                var quesCompExtentionViewModel = new QuesCompExtentionViewModel();
                quesCompExtentionViewModel.QuestionSn = int.Parse(questionSn);
                //quesCompExtentionViewModel.PresidentNm = scUsr.ScCompInfo.OwnNm;
                quesCompExtentionViewModel.Status = status;
                return View(quesCompExtentionViewModel);
            }
            else
            {
                var quesCompExtentionView = Mapper.Map<QuesCompExtentionViewModel>(quesMaster.QuesCompExtention);
                quesCompExtentionView.Status = status;
                return View(quesCompExtentionView);
            }
        }

        [HttpPost]
        public async Task<ActionResult> CompanyInfo02(QuesCompExtentionViewModel quesCompExtentionViewModel)
        {
            ViewBag.naviLeftMenu = Global.Report;
            int questionSn = quesCompExtentionViewModel.QuestionSn;

            if (quesCompExtentionViewModel.QuestionSn > 0)
            {
                var quesMaster = await _quesMasterService.GetQuesCompExtentionAsync(questionSn);

                if (quesCompExtentionViewModel.SubmitType == "N")
                {
                    quesMaster.SaveStatus = 4;
                }

                var quesCompExtention = Mapper.Map<QuesCompExtention>(quesCompExtentionViewModel);
                if (quesMaster.QuesCompExtention == null)
                {
                    quesCompExtention.RegDt = DateTime.Now;
                    quesCompExtention.RegId = Session[Global.LoginID].ToString();
                }
                else
                {
                    quesCompExtention.RegDt = quesMaster.QuesCompExtention.RegDt;
                    quesCompExtention.RegId = quesMaster.QuesCompExtention.RegId;
                    quesCompExtention.UpdDt = DateTime.Now;
                    quesCompExtention.UpdId = Session[Global.LoginID].ToString();
                }
                quesMaster.QuesCompExtention = quesCompExtention;
                await _quesMasterService.SaveDbContextAsync();
            }
            else
            {
                //에러처리 필요
                return View(quesCompExtentionViewModel);
            }

            if (quesCompExtentionViewModel.SubmitType == "T")
            {
                return RedirectToAction("CompanyInfo02", "Report", new { @questionSn = questionSn });
            }
            else
            {
                return RedirectToAction("BizCheck01", "Report", new { @questionSn = questionSn });
            }
        }

        public ActionResult BizCheck01(string questionSn, string status)
        {
            ViewBag.naviLeftMenu = Global.Report;

            var viewModel = new QuesViewModel();
            viewModel.QuestionSn = int.Parse(questionSn);
            viewModel.Status = status;
            return View(viewModel);
        }

        public async Task<ActionResult> BizCheck02(string questionSn, string status)
        {
            ViewBag.naviLeftMenu = Global.Report;

            if (string.IsNullOrEmpty(questionSn))
            {
                // 오류 처리해야함.
                return View();
            }

            var bizCheck02 = new BizCheck02ViewModel();

            // A1A101 : 경영목표 및 전략 코드
            var quesResult1sPurpose = await _quesResult1Service.GetQuesResult1sAsync(int.Parse(questionSn), "A1A101");

            if (quesResult1sPurpose.Count() != 5)
            {
                var quesCheckList = await _quesCheckListService.GetQuesCheckListAsync("A1A101");
                var quesCheckListView = Mapper.Map<List<QuesCheckListViewModel>>(quesCheckList);

                foreach (var item in quesCheckListView)
                {
                    item.QuestionSn = int.Parse(questionSn);
                    item.AnsVal = false;
                }

                bizCheck02.BizPurpose = quesCheckListView;
            }
            else
            {
                var quesCheckListView = Mapper.Map<List<QuesCheckListViewModel>>(quesResult1sPurpose);
                bizCheck02.BizPurpose = quesCheckListView;
            }

            // A1A102 : 경영자의 리더쉽 코드
            var quesResult1sLeadership = await _quesResult1Service.GetQuesResult1sAsync(int.Parse(questionSn), "A1A102");

            if (quesResult1sLeadership.Count() != 5)
            {
                var quesCheckList = await _quesCheckListService.GetQuesCheckListAsync("A1A102");
                var quesCheckListView = Mapper.Map<List<QuesCheckListViewModel>>(quesCheckList);

                foreach (var item in quesCheckListView)
                {
                    item.QuestionSn = int.Parse(questionSn);
                    item.AnsVal = false;
                }

                bizCheck02.Leadership = quesCheckListView;
            }
            else
            {
                var quesCheckListView = Mapper.Map<List<QuesCheckListViewModel>>(quesResult1sLeadership);
                bizCheck02.Leadership = quesCheckListView;
            }

            bizCheck02.Status = status;
            bizCheck02.QuestionSn = int.Parse(questionSn);
            return View(bizCheck02);
        }

        [HttpPost]
        public async Task<ActionResult> BizCheck02(BizCheck02ViewModel bizCheck02ViewModel)
        {
            ViewBag.naviLeftMenu = Global.Report;
            int questionSn = bizCheck02ViewModel.QuestionSn;

            if (bizCheck02ViewModel.QuestionSn > 0)
            {
                var quesMaster = await _quesMasterService.GetQuesResult1Async(questionSn);

                if (bizCheck02ViewModel.SubmitType == "N")
                {
                    quesMaster.SaveStatus = 5;
                }

                foreach(var item in bizCheck02ViewModel.BizPurpose)
                {
                    var checkItem = quesMaster.QuesResult1.SingleOrDefault(m => m.QuestionSn == questionSn && m.CheckListSn == item.CheckListSn);
                    if(checkItem == null)
                    {
                        var result1 = new QuesResult1();
                        result1.QuestionSn = questionSn;
                        result1.CheckListSn = item.CheckListSn;
                        result1.AnsVal = item.AnsVal;
                        result1.RegDt = DateTime.Now;
                        result1.RegId = Session[Global.LoginID].ToString();
                        quesMaster.QuesResult1.Add(result1);
                    }
                    else
                    {
                        checkItem.AnsVal = item.AnsVal;
                        checkItem.UpdDt = DateTime.Now;
                        checkItem.UpdId = Session[Global.LoginID].ToString();
                    }
                }

                foreach (var item in bizCheck02ViewModel.Leadership)
                {
                    var checkItem = quesMaster.QuesResult1.SingleOrDefault(m => m.QuestionSn == questionSn && m.CheckListSn == item.CheckListSn);
                    if (checkItem == null)
                    {
                        var result1 = new QuesResult1();
                        result1.QuestionSn = questionSn;
                        result1.CheckListSn = item.CheckListSn;
                        result1.AnsVal = item.AnsVal;
                        result1.RegDt = DateTime.Now;
                        result1.RegId = Session[Global.LoginID].ToString();
                        quesMaster.QuesResult1.Add(result1);
                    }
                    else
                    {
                        checkItem.AnsVal = item.AnsVal;
                        checkItem.UpdDt = DateTime.Now;
                        checkItem.UpdId = Session[Global.LoginID].ToString();
                    }
                }

                await _quesMasterService.SaveDbContextAsync();
            }
            else
            {
                //에러처리 필요
                return View(bizCheck02ViewModel);
            }

            if (bizCheck02ViewModel.SubmitType == "T")
            {
                return RedirectToAction("BizCheck02", "Report", new { @questionSn = questionSn });
            }
            else
            {
                return RedirectToAction("BizCheck03", "Report", new { @questionSn = questionSn });
            }
        }


        public async Task<ActionResult> BizCheck03(string questionSn, string status)
        {
            ViewBag.naviLeftMenu = Global.Report;

            if (string.IsNullOrEmpty(questionSn))
            {
                // 오류 처리해야함.
                return View();
            }

            var bizCheck03 = new BizCheck03ViewModel();

            // A1A103 : 경영자의 신뢰성
            var quesResult1s = await _quesResult1Service.GetQuesResult1sAsync(int.Parse(questionSn), "A1A103");

            if (quesResult1s.Count() != 4)
            {
                var quesCheckList = await _quesCheckListService.GetQuesCheckListAsync("A1A103");
                var quesCheckListView = Mapper.Map<List<QuesCheckListViewModel>>(quesCheckList);

                foreach (var item in quesCheckListView)
                {
                    item.QuestionSn = int.Parse(questionSn);
                    item.AnsVal = false;
                }

                bizCheck03.LeaderReliability = quesCheckListView;
            }
            else
            {
                var quesCheckListView = Mapper.Map<List<QuesCheckListViewModel>>(quesResult1s);
                bizCheck03.LeaderReliability = quesCheckListView;
            }

            bizCheck03.Status = status;
            bizCheck03.QuestionSn = int.Parse(questionSn);
            return View(bizCheck03);
        }

        [HttpPost]
        public async Task<ActionResult> BizCheck03(BizCheck03ViewModel bizCheck03ViewModel)
        {
            ViewBag.naviLeftMenu = Global.Report;
            int questionSn = bizCheck03ViewModel.QuestionSn;

            if (bizCheck03ViewModel.QuestionSn > 0)
            {
                var quesMaster = await _quesMasterService.GetQuesResult1Async(questionSn);

                if (bizCheck03ViewModel.SubmitType == "N")
                {
                    quesMaster.SaveStatus = 6;
                }

                foreach (var item in bizCheck03ViewModel.LeaderReliability)
                {
                    var checkItem = quesMaster.QuesResult1.SingleOrDefault(m => m.QuestionSn == questionSn && m.CheckListSn == item.CheckListSn);
                    if (checkItem == null)
                    {
                        var result1 = new QuesResult1();
                        result1.QuestionSn = questionSn;
                        result1.CheckListSn = item.CheckListSn;
                        result1.AnsVal = item.AnsVal;
                        result1.RegDt = DateTime.Now;
                        result1.RegId = Session[Global.LoginID].ToString();
                        quesMaster.QuesResult1.Add(result1);
                    }
                    else
                    {
                        checkItem.AnsVal = item.AnsVal;
                        checkItem.UpdDt = DateTime.Now;
                        checkItem.UpdId = Session[Global.LoginID].ToString();
                    }
                }

                await _quesMasterService.SaveDbContextAsync();
            }
            else
            {
                //에러처리 필요
                return View(bizCheck03ViewModel);
            }

            if (bizCheck03ViewModel.SubmitType == "T")
            {
                return RedirectToAction("BizCheck03", "Report", new { @questionSn = questionSn });
            }
            else
            {
                return RedirectToAction("BizCheck04", "Report", new { @questionSn = questionSn });
            }
        }


        public async Task<ActionResult> BizCheck04(string questionSn, string status)
        {
            ViewBag.naviLeftMenu = Global.Report;

            if (string.IsNullOrEmpty(questionSn))
            {
                // 오류 처리해야함.
                return View();
            }

            var bizCheck04 = new BizCheck04ViewModel();

            // A1A201 : 근로환경
            var quesResult1s = await _quesResult1Service.GetQuesResult1sAsync(int.Parse(questionSn), "A1A201");

            if (quesResult1s.Count() != 6)
            {
                var quesCheckList = await _quesCheckListService.GetQuesCheckListAsync("A1A201");
                var quesCheckListView = Mapper.Map<List<QuesCheckListViewModel>>(quesCheckList);

                foreach (var item in quesCheckListView)
                {
                    item.QuestionSn = int.Parse(questionSn);
                    item.AnsVal = false;
                }

                bizCheck04.WorkEnv = quesCheckListView;
            }
            else
            {
                var quesCheckListView = Mapper.Map<List<QuesCheckListViewModel>>(quesResult1s);
                bizCheck04.WorkEnv = quesCheckListView;
            }

            // A1A202 : 조직만족도
            var quesResult2s = await _quesResult2Service.GetQuesResult2sAsync(int.Parse(questionSn), "A1A202");

            if (quesResult2s.Count() != 3)
            {
                var quesCheckList = await _quesCheckListService.GetQuesCheckListAsync("A1A202");
                var quesYearListView = Mapper.Map<List<QuesYearListViewModel>>(quesCheckList);

                foreach (var item in quesYearListView)
                {
                    item.QuestionSn = int.Parse(questionSn);
                    item.BasicYear = DateTime.Now.Year;
                    item.D = "0";
                    item.D451 = "0";
                    item.D452 = "0";
                    item.D453 = "0";
                }
                //총직원
                bizCheck04.TotalEmp = quesYearListView.SingleOrDefault(i => i.DetailCd == "A1A20201");
                //이직직원
                bizCheck04.MoveEmp = quesYearListView.SingleOrDefault(i => i.DetailCd == "A1A20202");
                //신규직원
                bizCheck04.NewEmp = quesYearListView.SingleOrDefault(i => i.DetailCd == "A1A20203");
            }
            else
            {
                var quesYearListView = Mapper.Map<List<QuesYearListViewModel>>(quesResult2s);
                //총직원
                bizCheck04.TotalEmp = quesYearListView.SingleOrDefault(i => i.DetailCd == "A1A20201");
                //이직직원
                bizCheck04.MoveEmp = quesYearListView.SingleOrDefault(i => i.DetailCd == "A1A20202");
                //신규직원
                bizCheck04.NewEmp = quesYearListView.SingleOrDefault(i => i.DetailCd == "A1A20203");
            }

            bizCheck04.Status = status;
            bizCheck04.QuestionSn = int.Parse(questionSn);
            return View(bizCheck04);
        }

        [HttpPost]
        public async Task<ActionResult> BizCheck04(BizCheck04ViewModel bizCheck04ViewModel)
        {
            ViewBag.naviLeftMenu = Global.Report;
            int questionSn = bizCheck04ViewModel.QuestionSn;

            if (bizCheck04ViewModel.QuestionSn > 0)
            {
                var quesMaster = await _quesMasterService.GetQuesResult1Async(questionSn);

                if (bizCheck04ViewModel.SubmitType == "N")
                {
                    quesMaster.SaveStatus = 7;
                }

                //근로환경 저장 또는 업데이트 정보 설정
                foreach (var item in bizCheck04ViewModel.WorkEnv)
                {
                    var checkItem = quesMaster.QuesResult1.SingleOrDefault(m => m.QuestionSn == questionSn && m.CheckListSn == item.CheckListSn);
                    if (checkItem == null)
                    {
                        var result1 = new QuesResult1();
                        result1.QuestionSn = questionSn;
                        result1.CheckListSn = item.CheckListSn;
                        result1.AnsVal = item.AnsVal;
                        result1.RegDt = DateTime.Now;
                        result1.RegId = Session[Global.LoginID].ToString();
                        quesMaster.QuesResult1.Add(result1);
                    }
                    else
                    {
                        checkItem.AnsVal = item.AnsVal;
                        checkItem.UpdDt = DateTime.Now;
                        checkItem.UpdId = Session[Global.LoginID].ToString();
                    }
                }

                //조직만족도 저장 또는 업데이트 정보 설정
                var quesYearMaster = await _quesMasterService.GetQuesResult2Async(questionSn);

                //총직원
                var yearTotalItem = quesYearMaster.QuesResult2.SingleOrDefault(m => m.QuestionSn == questionSn && m.CheckListSn == bizCheck04ViewModel.TotalEmp.CheckListSn);
                if (yearTotalItem == null)
                {
                    var quesYearTotalEmp = Mapper.Map<QuesResult2>(bizCheck04ViewModel.TotalEmp);
                    quesYearTotalEmp.QuestionSn = questionSn;
                    quesYearTotalEmp.RegDt = DateTime.Now;
                    quesYearTotalEmp.RegId = Session[Global.LoginID].ToString();
                    quesYearTotalEmp.BasicYear = quesYearMaster.BasicYear;
                    quesYearMaster.QuesResult2.Add(quesYearTotalEmp);
                }
                else
                {
                    yearTotalItem.D = bizCheck04ViewModel.TotalEmp.D;
                    yearTotalItem.D451 = bizCheck04ViewModel.TotalEmp.D451;
                    yearTotalItem.D452 = bizCheck04ViewModel.TotalEmp.D452;
                    yearTotalItem.D453 = bizCheck04ViewModel.TotalEmp.D453;
                    yearTotalItem.UpdDt = DateTime.Now;
                    yearTotalItem.UpdId = Session[Global.LoginID].ToString();
                }


                //이직직원
                var yearMoveItem = quesYearMaster.QuesResult2.SingleOrDefault(m => m.QuestionSn == questionSn && m.CheckListSn == bizCheck04ViewModel.MoveEmp.CheckListSn);
                if (yearMoveItem == null)
                {
                    var quesYearMoveEmp = Mapper.Map<QuesResult2>(bizCheck04ViewModel.MoveEmp);
                    quesYearMoveEmp.QuestionSn = questionSn;
                    quesYearMoveEmp.RegDt = DateTime.Now;
                    quesYearMoveEmp.RegId = Session[Global.LoginID].ToString();
                    quesYearMoveEmp.BasicYear = quesYearMaster.BasicYear;
                    quesYearMaster.QuesResult2.Add(quesYearMoveEmp);
                }
                else
                {
                    yearMoveItem.D = bizCheck04ViewModel.MoveEmp.D;
                    yearMoveItem.D451 = bizCheck04ViewModel.MoveEmp.D451;
                    yearMoveItem.D452 = bizCheck04ViewModel.MoveEmp.D452;
                    yearMoveItem.D453 = bizCheck04ViewModel.MoveEmp.D453;
                    yearMoveItem.UpdDt = DateTime.Now;
                    yearMoveItem.UpdId = Session[Global.LoginID].ToString();
                }

                //신규직원
                var yearNewItem = quesYearMaster.QuesResult2.SingleOrDefault(m => m.QuestionSn == questionSn && m.CheckListSn == bizCheck04ViewModel.NewEmp.CheckListSn);
                if (yearNewItem == null)
                {
                    var quesYearNewEmp = Mapper.Map<QuesResult2>(bizCheck04ViewModel.NewEmp);
                    quesYearNewEmp.QuestionSn = questionSn;
                    quesYearNewEmp.RegDt = DateTime.Now;
                    quesYearNewEmp.RegId = Session[Global.LoginID].ToString();
                    quesYearNewEmp.BasicYear = quesYearMaster.BasicYear;
                    quesYearMaster.QuesResult2.Add(quesYearNewEmp);
                }
                else
                {
                    yearNewItem.D = bizCheck04ViewModel.NewEmp.D;
                    yearNewItem.D451 = bizCheck04ViewModel.NewEmp.D451;
                    yearNewItem.D452 = bizCheck04ViewModel.NewEmp.D452;
                    yearNewItem.D453 = bizCheck04ViewModel.NewEmp.D453;
                    yearNewItem.UpdDt = DateTime.Now;
                    yearNewItem.UpdId = Session[Global.LoginID].ToString();
                }

                await _quesMasterService.SaveDbContextAsync();
            }
            else
            {
                //에러처리 필요
                return View(bizCheck04ViewModel);
            }

            if (bizCheck04ViewModel.SubmitType == "T")
            {
                return RedirectToAction("BizCheck04", "Report", new { @questionSn = questionSn });
            }
            else
            {
                return RedirectToAction("BizCheck05", "Report", new { @questionSn = questionSn });
            }
        }


        public async Task<ActionResult> BizCheck05(string questionSn, string status)
        {
            ViewBag.naviLeftMenu = Global.Report;

            if (string.IsNullOrEmpty(questionSn))
            {
                // 오류 처리해야함.
                return View();
            }

            var bizCheck05 = new BizCheck05ViewModel();

            // A1A203 : 정보시스템 활용
            var quesResult1s = await _quesResult1Service.GetQuesResult1sAsync(int.Parse(questionSn), "A1A203");

            if (quesResult1s.Count() != 6)
            {
                var quesCheckList = await _quesCheckListService.GetQuesCheckListAsync("A1A203");
                var quesCheckListView = Mapper.Map<List<QuesCheckListViewModel>>(quesCheckList);

                foreach (var item in quesCheckListView)
                {
                    item.QuestionSn = int.Parse(questionSn);
                    item.AnsVal = false;
                }

                bizCheck05.InfoSystem = quesCheckListView;
            }
            else
            {
                var quesCheckListView = Mapper.Map<List<QuesCheckListViewModel>>(quesResult1s);
                bizCheck05.InfoSystem = quesCheckListView;
            }

            bizCheck05.Status = status;
            bizCheck05.QuestionSn = int.Parse(questionSn);
            return View(bizCheck05);
        }

        [HttpPost]
        public async Task<ActionResult> BizCheck05(BizCheck05ViewModel bizCheck05ViewModel)
        {
            ViewBag.naviLeftMenu = Global.Report;
            int questionSn = bizCheck05ViewModel.QuestionSn;

            if (bizCheck05ViewModel.QuestionSn > 0)
            {
                var quesMaster = await _quesMasterService.GetQuesResult1Async(questionSn);

                if (bizCheck05ViewModel.SubmitType == "N")
                {
                    quesMaster.SaveStatus = 8;
                }

                foreach (var item in bizCheck05ViewModel.InfoSystem)
                {
                    var checkItem = quesMaster.QuesResult1.SingleOrDefault(m => m.QuestionSn == questionSn && m.CheckListSn == item.CheckListSn);
                    if (checkItem == null)
                    {
                        var result1 = new QuesResult1();
                        result1.QuestionSn = questionSn;
                        result1.CheckListSn = item.CheckListSn;
                        result1.AnsVal = item.AnsVal;
                        result1.RegDt = DateTime.Now;
                        result1.RegId = Session[Global.LoginID].ToString();
                        quesMaster.QuesResult1.Add(result1);
                    }
                    else
                    {
                        checkItem.AnsVal = item.AnsVal;
                        checkItem.UpdDt = DateTime.Now;
                        checkItem.UpdId = Session[Global.LoginID].ToString();
                    }
                }

                await _quesMasterService.SaveDbContextAsync();
            }
            else
            {
                //에러처리 필요
                return View(bizCheck05ViewModel);
            }

            if (bizCheck05ViewModel.SubmitType == "T")
            {
                return RedirectToAction("BizCheck05", "Report", new { @questionSn = questionSn });
            }
            else
            {
                return RedirectToAction("BizCheck06", "Report", new { @questionSn = questionSn });
            }
        }

        public async Task<ActionResult> BizCheck06(string questionSn, string status)
        {
            ViewBag.naviLeftMenu = Global.Report;

            if (string.IsNullOrEmpty(questionSn))
            {
                // 오류 처리해야함.
                return View();
            }

            // 설립년도 가져오기
            var quesCompInfo = await _quesCompInfoService.GetQuesCompInfoAsync(int.Parse(questionSn));
            var quesCompInfoView = Mapper.Map<QuesCompanyInfoViewModel>(quesCompInfo);
            //var publishDt = quesCompInfoView.PublishDt.Substring(0, 4);
            var publishDt = quesCompInfoView.PublishYear;

            // 0415 - 
            var bizCheck04 = new BizCheck04ViewModel();

            // A1A201 : 근로환경
            var quesResult1s = await _quesResult1Service.GetQuesResult1sAsync(int.Parse(questionSn), "A1A201");

            if (quesResult1s.Count() != 6)
            {
                var quesCheckList = await _quesCheckListService.GetQuesCheckListAsync("A1A201");
                var quesCheckListView = Mapper.Map<List<QuesCheckListViewModel>>(quesCheckList);

                foreach (var item in quesCheckListView)
                {
                    item.QuestionSn = int.Parse(questionSn);
                    item.AnsVal = false;
                }

                bizCheck04.WorkEnv = quesCheckListView;
            }
            else
            {
                var quesCheckListView = Mapper.Map<List<QuesCheckListViewModel>>(quesResult1s);
                bizCheck04.WorkEnv = quesCheckListView;
            }

            // A1A202 : 조직만족도
            var quesResult2s = await _quesResult2Service.GetQuesResult2sAsync(int.Parse(questionSn), "A1A202");

            if (quesResult2s.Count() != 3)
            {
                var quesCheckList = await _quesCheckListService.GetQuesCheckListAsync("A1A202");
                var quesYearListView = Mapper.Map<List<QuesYearListViewModel>>(quesCheckList);

                foreach (var item in quesYearListView)
                {
                    item.QuestionSn = int.Parse(questionSn);
                    item.BasicYear = DateTime.Now.Year;
                    item.D = "0";
                    item.D451 = "0";
                    item.D452 = "0";
                    item.D453 = "0";
                }
                //총직원
                bizCheck04.TotalEmp = quesYearListView.SingleOrDefault(i => i.DetailCd == "A1A20201");
                //이직직원
                bizCheck04.MoveEmp = quesYearListView.SingleOrDefault(i => i.DetailCd == "A1A20202");
                //신규직원
                bizCheck04.NewEmp = quesYearListView.SingleOrDefault(i => i.DetailCd == "A1A20203");
            }
            else
            {
                var quesYearListView = Mapper.Map<List<QuesYearListViewModel>>(quesResult2s);
                //총직원
                bizCheck04.TotalEmp = quesYearListView.SingleOrDefault(i => i.DetailCd == "A1A20201");
                //이직직원
                bizCheck04.MoveEmp = quesYearListView.SingleOrDefault(i => i.DetailCd == "A1A20202");
                //신규직원
                bizCheck04.NewEmp = quesYearListView.SingleOrDefault(i => i.DetailCd == "A1A20203");
            }


            var bizCheck06 = new BizCheck06ViewModel();

            //인력의 비율
            var quesResult2sEmpRate = await _quesResult2Service.GetQuesResult2sAsync(int.Parse(questionSn), "A1B102");

            if (quesResult2sEmpRate.Count() != 2)
            {
                var quesCheckList = await _quesCheckListService.GetQuesCheckListAsync("A1B102");
                var quesYearListView = Mapper.Map<List<QuesYearListViewModel>>(quesCheckList);

                foreach (var item in quesYearListView)
                {
                    item.QuestionSn = int.Parse(questionSn);
                    item.BasicYear = DateTime.Now.Year;
                    item.D = "0";
                    item.D451 = "0";
                    item.D452 = "0";
                    item.D453 = "0";
                }
                //전체임직원수
                bizCheck06.TotalEmp = quesYearListView.SingleOrDefault(i => i.DetailCd == "A1B10202");

                if (bizCheck06.TotalEmp.BasicYear >= int.Parse(publishDt.Trim()))
                {
                    var plusD = int.Parse(bizCheck04.TotalEmp.D.Trim()) + 1;
                    bizCheck06.TotalEmp.D = Convert.ToString(plusD);
                }

                if (bizCheck06.TotalEmp.BasicYear - 1 >= int.Parse(publishDt.Trim()))
                {
                    var plusD541 = int.Parse(bizCheck04.TotalEmp.D451.Trim()) + 1;
                    bizCheck06.TotalEmp.D451 = Convert.ToString(plusD541);
                }

                if (bizCheck06.TotalEmp.BasicYear - 2 >= int.Parse(publishDt.Trim()))
                {
                    var plusD452 = int.Parse(bizCheck04.TotalEmp.D452.Trim()) + 1;
                    bizCheck06.TotalEmp.D452 = Convert.ToString(plusD452);
                }

                if (bizCheck06.TotalEmp.BasicYear - 3 >= int.Parse(publishDt.Trim()))
                {
                    var plusD453 = int.Parse(bizCheck04.TotalEmp.D453.Trim()) + 1;
                    bizCheck06.TotalEmp.D453 = Convert.ToString(plusD453);
                }

                //연구개발인력
                bizCheck06.RndEmp = quesYearListView.SingleOrDefault(i => i.DetailCd == "A1B10201");
            }
            else
            {
                var quesYearListView = Mapper.Map<List<QuesYearListViewModel>>(quesResult2sEmpRate);

                //전체임직원수
                bizCheck06.TotalEmp = quesYearListView.SingleOrDefault(i => i.DetailCd == "A1B10202");

                if (bizCheck06.TotalEmp.BasicYear >= int.Parse(publishDt.Trim()))
                {
                    var plusD = int.Parse(bizCheck04.TotalEmp.D.Trim()) + 1;
                    bizCheck06.TotalEmp.D = Convert.ToString(plusD);
                }

                if (bizCheck06.TotalEmp.BasicYear - 1 >= int.Parse(publishDt.Trim()))
                {
                    var plusD541 = int.Parse(bizCheck04.TotalEmp.D451.Trim()) + 1;
                    bizCheck06.TotalEmp.D451 = Convert.ToString(plusD541);
                }

                if (bizCheck06.TotalEmp.BasicYear - 2 >= int.Parse(publishDt.Trim()))
                {
                    var plusD452 = int.Parse(bizCheck04.TotalEmp.D452.Trim()) + 1;
                    bizCheck06.TotalEmp.D452 = Convert.ToString(plusD452);
                }

                if (bizCheck06.TotalEmp.BasicYear - 3 >= int.Parse(publishDt.Trim()))
                {
                    var plusD453 = int.Parse(bizCheck04.TotalEmp.D453.Trim()) + 1;
                    bizCheck06.TotalEmp.D453 = Convert.ToString(plusD453);
                }

                //연구개발인력
                bizCheck06.RndEmp = quesYearListView.SingleOrDefault(i => i.DetailCd == "A1B10201");
            }

            //연구개발 인력의 능력
            var quesResult2sEmpCapa = await _quesResult2Service.GetQuesResult2sAsync(int.Parse(questionSn), "A1B103");

            if (quesResult2sEmpCapa.Count() != 5)
            {
                var quesCheckList = await _quesCheckListService.GetQuesCheckListAsync("A1B103");
                var quesYearListView = Mapper.Map<List<QuesYearListViewModel>>(quesCheckList);

                foreach (var item in quesYearListView)
                {
                    item.QuestionSn = int.Parse(questionSn);
                    item.BasicYear = DateTime.Now.Year;
                    item.D = "0";
                    item.D451 = "0";
                    item.D452 = "0";
                    item.D453 = "0";
                }
                //박사급
                bizCheck06.DoctorEmp = quesYearListView.SingleOrDefault(i => i.DetailCd == "A1B10301");
                //석사급
                bizCheck06.MasterEmp = quesYearListView.SingleOrDefault(i => i.DetailCd == "A1B10302");
                //학사급
                bizCheck06.CollegeEmp = quesYearListView.SingleOrDefault(i => i.DetailCd == "A1B10303");
                //기능사급
                bizCheck06.TechEmp = quesYearListView.SingleOrDefault(i => i.DetailCd == "A1B10304");
                //고졸이하급
                bizCheck06.HighEmp = quesYearListView.SingleOrDefault(i => i.DetailCd == "A1B10305");
            }
            else
            {
                var quesYearListView = Mapper.Map<List<QuesYearListViewModel>>(quesResult2sEmpCapa);
                //박사급
                bizCheck06.DoctorEmp = quesYearListView.SingleOrDefault(i => i.DetailCd == "A1B10301");
                //석사급
                bizCheck06.MasterEmp = quesYearListView.SingleOrDefault(i => i.DetailCd == "A1B10302");
                //학사급
                bizCheck06.CollegeEmp = quesYearListView.SingleOrDefault(i => i.DetailCd == "A1B10303");
                //기능사급
                bizCheck06.TechEmp = quesYearListView.SingleOrDefault(i => i.DetailCd == "A1B10304");
                //고졸이하급
                bizCheck06.HighEmp = quesYearListView.SingleOrDefault(i => i.DetailCd == "A1B10305");
            }

            bizCheck06.Status = status;
            bizCheck06.QuestionSn = int.Parse(questionSn);
            return View(bizCheck06);
        }

        [HttpPost]
        public async Task<ActionResult> BizCheck06(BizCheck06ViewModel bizCheck06ViewModel)
        {
            ViewBag.naviLeftMenu = Global.Report;
            int questionSn = bizCheck06ViewModel.QuestionSn;

            // 설립년도 가져오기
            var quesCompInfo = await _quesCompInfoService.GetQuesCompInfoAsync(questionSn);
            var quesCompInfoView = Mapper.Map<QuesCompanyInfoViewModel>(quesCompInfo);
            //var publishDt = quesCompInfoView.PublishDt.Substring(0, 4);
            var publishDt = quesCompInfoView.PublishYear;

            if (bizCheck06ViewModel.QuestionSn > 0)
            {
                //인력의 비율, 연구개발 인력의 능력 저장 또는 업데이트 정보 설정
                var quesYearMaster = await _quesMasterService.GetQuesResult2Async(questionSn);

                if (bizCheck06ViewModel.SubmitType == "N")
                {
                    quesYearMaster.SaveStatus = 9;
                }

                //연구개발 인력수
                var yearRndItem = quesYearMaster.QuesResult2.SingleOrDefault(m => m.QuestionSn == questionSn && m.CheckListSn == bizCheck06ViewModel.RndEmp.CheckListSn);
                if (yearRndItem == null)
                {
                    var quesYearRndEmp = Mapper.Map<QuesResult2>(bizCheck06ViewModel.RndEmp);
                    quesYearRndEmp.QuestionSn = questionSn;
                    quesYearRndEmp.RegDt = DateTime.Now;
                    quesYearRndEmp.RegId = Session[Global.LoginID].ToString();
                    quesYearRndEmp.BasicYear = quesYearMaster.BasicYear;
                    quesYearMaster.QuesResult2.Add(quesYearRndEmp);
                }
                else
                {
                    yearRndItem.D = bizCheck06ViewModel.RndEmp.D;
                    yearRndItem.D451 = bizCheck06ViewModel.RndEmp.D451;
                    yearRndItem.D452 = bizCheck06ViewModel.RndEmp.D452;
                    yearRndItem.D453 = bizCheck06ViewModel.RndEmp.D453;
                    yearRndItem.UpdDt = DateTime.Now;
                    yearRndItem.UpdId = Session[Global.LoginID].ToString();
                }

                // 전체임직원수
                var yearTotalItem = quesYearMaster.QuesResult2.SingleOrDefault(m => m.QuestionSn == questionSn && m.CheckListSn == bizCheck06ViewModel.TotalEmp.CheckListSn);
                if (yearTotalItem == null)
                {
                    var quesYearTotalEmp = Mapper.Map<QuesResult2>(bizCheck06ViewModel.TotalEmp);
                    quesYearTotalEmp.QuestionSn = questionSn;
                    quesYearTotalEmp.RegDt = DateTime.Now;
                    quesYearTotalEmp.RegId = Session[Global.LoginID].ToString();
                    quesYearTotalEmp.BasicYear = quesYearMaster.BasicYear;

                    // 0509 추가 phm
                    if (quesYearTotalEmp.BasicYear >= int.Parse(publishDt))
                    { 
                        quesYearTotalEmp.D = int.Parse(quesYearTotalEmp.D) - 1 + "";
                    }

                    if (quesYearTotalEmp.BasicYear - 1 >= int.Parse(publishDt))
                    {
                        quesYearTotalEmp.D451 = int.Parse(quesYearTotalEmp.D451) - 1 + "";
                    }

                    if (quesYearTotalEmp.BasicYear - 2 >= int.Parse(publishDt))
                    {
                        quesYearTotalEmp.D452 = int.Parse(quesYearTotalEmp.D452) - 1 + "";
                    }

                    if (quesYearTotalEmp.BasicYear - 3 >= int.Parse(publishDt))
                    {
                        quesYearTotalEmp.D453 = int.Parse(quesYearTotalEmp.D453) - 1 + ""; // 처음 작성일 때 하나 빼주는 부분이 생략되어 있었다
                    }

                    quesYearMaster.QuesResult2.Add(quesYearTotalEmp);
                }
                else
                {
                    yearTotalItem.D = int.Parse(bizCheck06ViewModel.TotalEmp.D) + "";
                    yearTotalItem.D451 = int.Parse(bizCheck06ViewModel.TotalEmp.D451) + "";
                    yearTotalItem.D452 = int.Parse(bizCheck06ViewModel.TotalEmp.D452) + "";
                    yearTotalItem.D453 = int.Parse(bizCheck06ViewModel.TotalEmp.D453) + "";
                    yearTotalItem.UpdDt = DateTime.Now;
                    yearTotalItem.UpdId = Session[Global.LoginID].ToString();

                    if (yearTotalItem.BasicYear >= int.Parse(publishDt))
                    {
                        yearTotalItem.D = int.Parse(bizCheck06ViewModel.TotalEmp.D) - 1 + "";
                    }

                    if (yearTotalItem.BasicYear - 1 >= int.Parse(publishDt))
                    {
                        yearTotalItem.D451 = int.Parse(bizCheck06ViewModel.TotalEmp.D451) - 1 + "";
                    }

                    if (yearTotalItem.BasicYear - 2 >= int.Parse(publishDt))
                    {
                        yearTotalItem.D452 = int.Parse(bizCheck06ViewModel.TotalEmp.D452) - 1 + "";
                    }

                    if (yearTotalItem.BasicYear - 3 >= int.Parse(publishDt))
                    {
                        yearTotalItem.D453 = int.Parse(bizCheck06ViewModel.TotalEmp.D453) - 1 + "";
                    }
                }

                //박사급
                var yearDoctorItem = quesYearMaster.QuesResult2.SingleOrDefault(m => m.QuestionSn == questionSn && m.CheckListSn == bizCheck06ViewModel.DoctorEmp.CheckListSn);
                if (yearDoctorItem == null)
                {
                    var quesYearDoctorEmp = Mapper.Map<QuesResult2>(bizCheck06ViewModel.DoctorEmp);
                    quesYearDoctorEmp.QuestionSn = questionSn;
                    quesYearDoctorEmp.RegDt = DateTime.Now;
                    quesYearDoctorEmp.RegId = Session[Global.LoginID].ToString();
                    quesYearDoctorEmp.BasicYear = quesYearMaster.BasicYear;
                    quesYearMaster.QuesResult2.Add(quesYearDoctorEmp);
                }
                else
                {
                    yearDoctorItem.D = bizCheck06ViewModel.DoctorEmp.D;
                    yearDoctorItem.UpdDt = DateTime.Now;
                    yearDoctorItem.UpdId = Session[Global.LoginID].ToString();
                }

                //석사급
                var yearMasterItem = quesYearMaster.QuesResult2.SingleOrDefault(m => m.QuestionSn == questionSn && m.CheckListSn == bizCheck06ViewModel.MasterEmp.CheckListSn);
                if (yearMasterItem == null)
                {
                    var quesYearMasterEmp = Mapper.Map<QuesResult2>(bizCheck06ViewModel.MasterEmp);
                    quesYearMasterEmp.QuestionSn = questionSn;
                    quesYearMasterEmp.RegDt = DateTime.Now;
                    quesYearMasterEmp.RegId = Session[Global.LoginID].ToString();
                    quesYearMasterEmp.BasicYear = quesYearMaster.BasicYear;
                    quesYearMaster.QuesResult2.Add(quesYearMasterEmp);
                }
                else
                {
                    yearMasterItem.D = bizCheck06ViewModel.MasterEmp.D;
                    yearMasterItem.UpdDt = DateTime.Now;
                    yearMasterItem.UpdId = Session[Global.LoginID].ToString();
                }

                //학사급
                var yearCollegeItem = quesYearMaster.QuesResult2.SingleOrDefault(m => m.QuestionSn == questionSn && m.CheckListSn == bizCheck06ViewModel.CollegeEmp.CheckListSn);
                if (yearCollegeItem == null)
                {
                    var quesYearCollegeEmp = Mapper.Map<QuesResult2>(bizCheck06ViewModel.CollegeEmp);
                    quesYearCollegeEmp.QuestionSn = questionSn;
                    quesYearCollegeEmp.RegDt = DateTime.Now;
                    quesYearCollegeEmp.RegId = Session[Global.LoginID].ToString();
                    quesYearCollegeEmp.BasicYear = quesYearMaster.BasicYear;
                    quesYearMaster.QuesResult2.Add(quesYearCollegeEmp);
                }
                else
                {
                    yearCollegeItem.D = bizCheck06ViewModel.CollegeEmp.D;
                    yearCollegeItem.UpdDt = DateTime.Now;
                    yearCollegeItem.UpdId = Session[Global.LoginID].ToString();
                }

                //기능사급
                var yearTechItem = quesYearMaster.QuesResult2.SingleOrDefault(m => m.QuestionSn == questionSn && m.CheckListSn == bizCheck06ViewModel.TechEmp.CheckListSn);
                if (yearTechItem == null)
                {
                    var quesYearTechEmp = Mapper.Map<QuesResult2>(bizCheck06ViewModel.TechEmp);
                    quesYearTechEmp.QuestionSn = questionSn;
                    quesYearTechEmp.RegDt = DateTime.Now;
                    quesYearTechEmp.RegId = Session[Global.LoginID].ToString();
                    quesYearTechEmp.BasicYear = quesYearMaster.BasicYear;
                    quesYearMaster.QuesResult2.Add(quesYearTechEmp);
                }
                else
                {
                    yearTechItem.D = bizCheck06ViewModel.TechEmp.D;
                    yearTechItem.UpdDt = DateTime.Now;
                    yearTechItem.UpdId = Session[Global.LoginID].ToString();
                }

                //고졸이하
                var yearHighItem = quesYearMaster.QuesResult2.SingleOrDefault(m => m.QuestionSn == questionSn && m.CheckListSn == bizCheck06ViewModel.HighEmp.CheckListSn);
                if (yearHighItem == null)
                {
                    var quesYearHighEmp = Mapper.Map<QuesResult2>(bizCheck06ViewModel.HighEmp);
                    quesYearHighEmp.QuestionSn = questionSn;
                    quesYearHighEmp.RegDt = DateTime.Now;
                    quesYearHighEmp.RegId = Session[Global.LoginID].ToString();
                    quesYearHighEmp.BasicYear = quesYearMaster.BasicYear;
                    quesYearMaster.QuesResult2.Add(quesYearHighEmp);
                }
                else
                {
                    yearHighItem.D = bizCheck06ViewModel.HighEmp.D;
                    yearHighItem.UpdDt = DateTime.Now;
                    yearHighItem.UpdId = Session[Global.LoginID].ToString();
                }

                await _quesMasterService.SaveDbContextAsync();
            }
            else
            {
                //에러처리 필요
                return View(bizCheck06ViewModel);
            }

            if (bizCheck06ViewModel.SubmitType == "T")
            {
                return RedirectToAction("BizCheck06", "Report", new { @questionSn = questionSn });
            }
            else
            {
                return RedirectToAction("BizCheck07", "Report", new { @questionSn = questionSn });
            }
        }


        public async Task<ActionResult> BizCheck07(string questionSn, string status)
        {
            ViewBag.naviLeftMenu = Global.Report;

            if (string.IsNullOrEmpty(questionSn))
            {
                // 오류 처리해야함.
                return View();
            }

            var bizCheck07 = new BizCheck07ViewModel();

            // A1B104 : 사업화역량
            var quesResult1s = await _quesResult1Service.GetQuesResult1sAsync(int.Parse(questionSn), "A1B104");

            if (quesResult1s.Count() != 5)
            {
                var quesCheckList = await _quesCheckListService.GetQuesCheckListAsync("A1B104");
                var quesCheckListView = Mapper.Map<List<QuesCheckListViewModel>>(quesCheckList);

                foreach (var item in quesCheckListView)
                {
                    item.QuestionSn = int.Parse(questionSn);
                    item.AnsVal = false;
                }

                bizCheck07.BizCapa = quesCheckListView;
            }
            else
            {
                var quesCheckListView = Mapper.Map<List<QuesCheckListViewModel>>(quesResult1s);
                bizCheck07.BizCapa = quesCheckListView;
            }

            // A1B105 : 사업화실적
            var quesResult2s = await _quesResult2Service.GetQuesResult2sAsync(int.Parse(questionSn), "A1B105");

            if (quesResult2s.Count() != 2)
            {
                var quesCheckList = await _quesCheckListService.GetQuesCheckListAsync("A1B105");
                var quesYearListView = Mapper.Map<List<QuesYearListViewModel>>(quesCheckList);

                foreach (var item in quesYearListView)
                {
                    item.QuestionSn = int.Parse(questionSn);
                    item.BasicYear = DateTime.Now.Year;

                    if (item.DetailCd == "A1B10502")
                    { 
                        item.D = "0";
                        item.D451 = "0";
                        item.D452 = "0";
                        item.D453 = "0";
                    }
                }

                //사업화실적
                bizCheck07.BizResult = quesYearListView.SingleOrDefault(i => i.DetailCd == "A1B10501");
                //사업화실적 총 건수
                bizCheck07.BizResultCnt = quesYearListView.SingleOrDefault(i => i.DetailCd == "A1B10502");
            }
            else
            {
                var quesYearListView = Mapper.Map<List<QuesYearListViewModel>>(quesResult2s);
                //사업화실적
                bizCheck07.BizResult = quesYearListView.SingleOrDefault(i => i.DetailCd == "A1B10501");
                //사업화실적 총 건수
                bizCheck07.BizResultCnt = quesYearListView.SingleOrDefault(i => i.DetailCd == "A1B10502");
            }

            bizCheck07.Status = status;
            bizCheck07.QuestionSn = int.Parse(questionSn);
            return View(bizCheck07);
        }

        [HttpPost]
        public async Task<ActionResult> BizCheck07(BizCheck07ViewModel bizCheck07ViewModel)
        {
            ViewBag.naviLeftMenu = Global.Report;
            int questionSn = bizCheck07ViewModel.QuestionSn;

            if (bizCheck07ViewModel.QuestionSn > 0)
            {
                var quesMaster = await _quesMasterService.GetQuesResult1Async(questionSn);

                if (bizCheck07ViewModel.SubmitType == "N")
                {
                    quesMaster.SaveStatus = 10;
                }

                //사업화 역량 저장 또는 업데이트 정보 설정
                foreach (var item in bizCheck07ViewModel.BizCapa)
                {
                    var checkItem = quesMaster.QuesResult1.SingleOrDefault(m => m.QuestionSn == questionSn && m.CheckListSn == item.CheckListSn);
                    if (checkItem == null)
                    {
                        var result1 = new QuesResult1();
                        result1.QuestionSn = questionSn;
                        result1.CheckListSn = item.CheckListSn;
                        result1.AnsVal = item.AnsVal;
                        result1.RegDt = DateTime.Now;
                        result1.RegId = Session[Global.LoginID].ToString();
                        quesMaster.QuesResult1.Add(result1);
                    }
                    else
                    {
                        checkItem.AnsVal = item.AnsVal;
                        checkItem.UpdDt = DateTime.Now;
                        checkItem.UpdId = Session[Global.LoginID].ToString();
                    }
                }

                //사업화 실적 저장 또는 업데이트 정보 설정
                var quesYearMaster = await _quesMasterService.GetQuesResult2Async(questionSn);

                //사업화실적
                var yearBizResultItem = quesYearMaster.QuesResult2.SingleOrDefault(m => m.QuestionSn == questionSn && m.CheckListSn == bizCheck07ViewModel.BizResult.CheckListSn);
                if (yearBizResultItem == null)
                {
                    var quesYearBizResult = Mapper.Map<QuesResult2>(bizCheck07ViewModel.BizResult);
                    quesYearBizResult.QuestionSn = questionSn;
                    quesYearBizResult.RegDt = DateTime.Now;
                    quesYearBizResult.RegId = Session[Global.LoginID].ToString();
                    quesYearBizResult.BasicYear = quesYearMaster.BasicYear;
                    quesYearMaster.QuesResult2.Add(quesYearBizResult);
                }
                else
                {
                    yearBizResultItem.D = bizCheck07ViewModel.BizResult.D;
                    yearBizResultItem.D451 = bizCheck07ViewModel.BizResult.D451;
                    yearBizResultItem.D452 = bizCheck07ViewModel.BizResult.D452;
                    yearBizResultItem.D453 = bizCheck07ViewModel.BizResult.D453;
                    yearBizResultItem.UpdDt = DateTime.Now;
                    yearBizResultItem.UpdId = Session[Global.LoginID].ToString();
                }


                //사업화실적 총건수
                var yearBizResultCntItem = quesYearMaster.QuesResult2.SingleOrDefault(m => m.QuestionSn == questionSn && m.CheckListSn == bizCheck07ViewModel.BizResultCnt.CheckListSn);
                if (yearBizResultCntItem == null)
                {
                    var quesYearBizResultCnt = Mapper.Map<QuesResult2>(bizCheck07ViewModel.BizResultCnt);
                    quesYearBizResultCnt.QuestionSn = questionSn;
                    quesYearBizResultCnt.RegDt = DateTime.Now;
                    quesYearBizResultCnt.RegId = Session[Global.LoginID].ToString();
                    quesYearBizResultCnt.BasicYear = quesYearMaster.BasicYear;
                    quesYearMaster.QuesResult2.Add(quesYearBizResultCnt);
                }
                else
                {
                    yearBizResultCntItem.D = bizCheck07ViewModel.BizResultCnt.D;
                    yearBizResultCntItem.D451 = bizCheck07ViewModel.BizResultCnt.D451;
                    yearBizResultCntItem.D452 = bizCheck07ViewModel.BizResultCnt.D452;
                    yearBizResultCntItem.D453 = bizCheck07ViewModel.BizResultCnt.D453;
                    yearBizResultCntItem.UpdDt = DateTime.Now;
                    yearBizResultCntItem.UpdId = Session[Global.LoginID].ToString();
                }

                await _quesMasterService.SaveDbContextAsync();
            }
            else
            {
                //에러처리 필요
                return View(bizCheck07ViewModel);
            }

            if (bizCheck07ViewModel.SubmitType == "T")
            {
                return RedirectToAction("BizCheck07", "Report", new { @questionSn = questionSn });
            }
            else
            {
                return RedirectToAction("BizCheck08", "Report", new { @questionSn = questionSn });
            }
        }


        public async Task<ActionResult> BizCheck08(string questionSn, string status)
        {
            ViewBag.naviLeftMenu = Global.Report;

            if (string.IsNullOrEmpty(questionSn))
            {
                // 오류 처리해야함.
                return View();
            }

            var bizCheck08 = new BizCheck08ViewModel();

            // A1B106 : 생산설비의 운영체제 및 관리
            var quesResult1s = await _quesResult1Service.GetQuesResult1sAsync(int.Parse(questionSn), "A1B106");

            if (quesResult1s.Count() != 5)
            {
                var quesCheckList = await _quesCheckListService.GetQuesCheckListAsync("A1B106");
                var quesCheckListView = Mapper.Map<List<QuesCheckListViewModel>>(quesCheckList);

                foreach (var item in quesCheckListView)
                {
                    item.QuestionSn = int.Parse(questionSn);
                    item.AnsVal = false;
                }

                bizCheck08.ProducEquip = quesCheckListView;
            }
            else
            {
                var quesCheckListView = Mapper.Map<List<QuesCheckListViewModel>>(quesResult1s);
                bizCheck08.ProducEquip = quesCheckListView;
            }

            // A1B107 : 공정관리
            var quesResult1sProcess = await _quesResult1Service.GetQuesResult1sAsync(int.Parse(questionSn), "A1B107");

            if (quesResult1sProcess.Count() != 5)
            {
                var quesCheckList = await _quesCheckListService.GetQuesCheckListAsync("A1B107");
                var quesCheckListView = Mapper.Map<List<QuesCheckListViewModel>>(quesCheckList);

                foreach (var item in quesCheckListView)
                {
                    item.QuestionSn = int.Parse(questionSn);
                    item.AnsVal = false;
                }

                bizCheck08.ProcessControl = quesCheckListView;
            }
            else
            {
                var quesCheckListView = Mapper.Map<List<QuesCheckListViewModel>>(quesResult1sProcess);
                bizCheck08.ProcessControl = quesCheckListView;
            }

            bizCheck08.Status = status;
            bizCheck08.QuestionSn = int.Parse(questionSn);
            return View(bizCheck08);
        }

        [HttpPost]
        public async Task<ActionResult> BizCheck08(BizCheck08ViewModel bizCheck08ViewModel)
        {
            ViewBag.naviLeftMenu = Global.Report;
            int questionSn = bizCheck08ViewModel.QuestionSn;

            if (bizCheck08ViewModel.QuestionSn > 0)
            {
                var quesMaster = await _quesMasterService.GetQuesResult1Async(questionSn);

                if (bizCheck08ViewModel.SubmitType == "N")
                {
                    quesMaster.SaveStatus = 11;
                }

                // A1B106 : 생산설비의 운영체제 및 관리
                foreach (var item in bizCheck08ViewModel.ProducEquip)
                {
                    var checkItem = quesMaster.QuesResult1.SingleOrDefault(m => m.QuestionSn == questionSn && m.CheckListSn == item.CheckListSn);
                    if (checkItem == null)
                    {
                        var result1 = new QuesResult1();
                        result1.QuestionSn = questionSn;
                        result1.CheckListSn = item.CheckListSn;
                        result1.AnsVal = item.AnsVal;
                        result1.RegDt = DateTime.Now;
                        result1.RegId = Session[Global.LoginID].ToString();
                        quesMaster.QuesResult1.Add(result1);
                    }
                    else
                    {
                        checkItem.AnsVal = item.AnsVal;
                        checkItem.UpdDt = DateTime.Now;
                        checkItem.UpdId = Session[Global.LoginID].ToString();
                    }
                }

                // A1B107 : 공정관리
                foreach (var item in bizCheck08ViewModel.ProcessControl)
                {
                    var checkItem = quesMaster.QuesResult1.SingleOrDefault(m => m.QuestionSn == questionSn && m.CheckListSn == item.CheckListSn);
                    if (checkItem == null)
                    {
                        var result1 = new QuesResult1();
                        result1.QuestionSn = questionSn;
                        result1.CheckListSn = item.CheckListSn;
                        result1.AnsVal = item.AnsVal;
                        result1.RegDt = DateTime.Now;
                        result1.RegId = Session[Global.LoginID].ToString();
                        quesMaster.QuesResult1.Add(result1);
                    }
                    else
                    {
                        checkItem.AnsVal = item.AnsVal;
                        checkItem.UpdDt = DateTime.Now;
                        checkItem.UpdId = Session[Global.LoginID].ToString();
                    }
                }

                await _quesMasterService.SaveDbContextAsync();
            }
            else
            {
                //에러처리 필요
                return View(bizCheck08ViewModel);
            }

            if (bizCheck08ViewModel.SubmitType == "T")
            {
                return RedirectToAction("BizCheck08", "Report", new { @questionSn = questionSn });
            }
            else
            {
                return RedirectToAction("BizCheck09", "Report", new { @questionSn = questionSn });
            }
        }


        public async Task<ActionResult> BizCheck09(string questionSn, string status)
        {
            ViewBag.naviLeftMenu = Global.Report;

            if (string.IsNullOrEmpty(questionSn))
            {
                // 오류 처리해야함.
                return View();
            }

            var bizCheck09 = new BizCheck09ViewModel();

            // A1B108 : 품질관리
            var quesResult1s = await _quesResult1Service.GetQuesResult1sAsync(int.Parse(questionSn), "A1B108");

            if (quesResult1s.Count() != 6)
            {
                var quesCheckList = await _quesCheckListService.GetQuesCheckListAsync("A1B108");
                var quesCheckListView = Mapper.Map<List<QuesCheckListViewModel>>(quesCheckList);

                foreach (var item in quesCheckListView)
                {
                    item.QuestionSn = int.Parse(questionSn);
                    item.AnsVal = false;
                }

                bizCheck09.QualityControl = quesCheckListView;
            }
            else
            {
                var quesCheckListView = Mapper.Map<List<QuesCheckListViewModel>>(quesResult1s);
                bizCheck09.QualityControl = quesCheckListView;
            }

            bizCheck09.Status = status;
            bizCheck09.QuestionSn = int.Parse(questionSn);
            return View(bizCheck09);
        }

        [HttpPost]
        public async Task<ActionResult> BizCheck09(BizCheck09ViewModel bizCheck09ViewModel)
        {
            ViewBag.naviLeftMenu = Global.Report;
            int questionSn = bizCheck09ViewModel.QuestionSn;

            if (bizCheck09ViewModel.QuestionSn > 0)
            {
                var quesMaster = await _quesMasterService.GetQuesResult1Async(questionSn);

                if (bizCheck09ViewModel.SubmitType == "N")
                {
                    quesMaster.SaveStatus = 12;
                }

                // A1B108 : 품질관리
                foreach (var item in bizCheck09ViewModel.QualityControl)
                {
                    var checkItem = quesMaster.QuesResult1.SingleOrDefault(m => m.QuestionSn == questionSn && m.CheckListSn == item.CheckListSn);
                    if (checkItem == null)
                    {
                        var result1 = new QuesResult1();
                        result1.QuestionSn = questionSn;
                        result1.CheckListSn = item.CheckListSn;
                        result1.AnsVal = item.AnsVal;
                        result1.RegDt = DateTime.Now;
                        result1.RegId = Session[Global.LoginID].ToString();
                        quesMaster.QuesResult1.Add(result1);
                    }
                    else
                    {
                        checkItem.AnsVal = item.AnsVal;
                        checkItem.UpdDt = DateTime.Now;
                        checkItem.UpdId = Session[Global.LoginID].ToString();
                    }
                }

                await _quesMasterService.SaveDbContextAsync();
            }
            else
            {
                //에러처리 필요
                return View(bizCheck09ViewModel);
            }

            if (bizCheck09ViewModel.SubmitType == "T")
            {
                return RedirectToAction("BizCheck09", "Report", new { @questionSn = questionSn });
            }
            else
            {
                return RedirectToAction("BizCheck10", "Report", new { @questionSn = questionSn });
            }
        }


        public async Task<ActionResult> BizCheck10(string questionSn, string status)
        {
            ViewBag.naviLeftMenu = Global.Report;

            if (string.IsNullOrEmpty(questionSn))
            {
                // 오류 처리해야함.
                return View();
            }

            var bizCheck10 = new BizCheck10ViewModel();

            // A1C101 : 마케팅 전략의 수립 및 실행
            var quesResult1s = await _quesResult1Service.GetQuesResult1sAsync(int.Parse(questionSn), "A1C101");

            if (quesResult1s.Count() != 7)
            {
                var quesCheckList = await _quesCheckListService.GetQuesCheckListAsync("A1C101");
                var quesCheckListView = Mapper.Map<List<QuesCheckListViewModel>>(quesCheckList);

                foreach (var item in quesCheckListView)
                {
                    item.QuestionSn = int.Parse(questionSn);
                    item.AnsVal = false;
                }

                bizCheck10.MarketingPlan = quesCheckListView;
            }
            else
            {
                var quesCheckListView = Mapper.Map<List<QuesCheckListViewModel>>(quesResult1s);
                bizCheck10.MarketingPlan = quesCheckListView;
            }

            bizCheck10.Status = status;
            bizCheck10.QuestionSn = int.Parse(questionSn);
            return View(bizCheck10);
        }

        [HttpPost]
        public async Task<ActionResult> BizCheck10(BizCheck10ViewModel bizCheck10ViewModel)
        {
            ViewBag.naviLeftMenu = Global.Report;
            int questionSn = bizCheck10ViewModel.QuestionSn;

            if (bizCheck10ViewModel.QuestionSn > 0)
            {
                var quesMaster = await _quesMasterService.GetQuesResult1Async(questionSn);

                if (bizCheck10ViewModel.SubmitType == "N")
                {
                    quesMaster.SaveStatus = 13;
                }

                // A1B108 : 품질관리
                foreach (var item in bizCheck10ViewModel.MarketingPlan)
                {
                    var checkItem = quesMaster.QuesResult1.SingleOrDefault(m => m.QuestionSn == questionSn && m.CheckListSn == item.CheckListSn);
                    if (checkItem == null)
                    {
                        var result1 = new QuesResult1();
                        result1.QuestionSn = questionSn;
                        result1.CheckListSn = item.CheckListSn;
                        result1.AnsVal = item.AnsVal;
                        result1.RegDt = DateTime.Now;
                        result1.RegId = Session[Global.LoginID].ToString();
                        quesMaster.QuesResult1.Add(result1);
                    }
                    else
                    {
                        checkItem.AnsVal = item.AnsVal;
                        checkItem.UpdDt = DateTime.Now;
                        checkItem.UpdId = Session[Global.LoginID].ToString();
                    }
                }

                await _quesMasterService.SaveDbContextAsync();
            }
            else
            {
                //에러처리 필요
                return View(bizCheck10ViewModel);
            }

            if (bizCheck10ViewModel.SubmitType == "T")
            {
                return RedirectToAction("BizCheck10", "Report", new { @questionSn = questionSn });
            }
            else
            {
                return RedirectToAction("BizCheck11", "Report", new { @questionSn = questionSn });
            }
        }

        public async Task<ActionResult> BizCheck11(string questionSn, string status)
        {
            ViewBag.naviLeftMenu = Global.Report;

            if (string.IsNullOrEmpty(questionSn))
            {
                // 오류 처리해야함.
                return View();
            }

            var bizCheck11 = new BizCheck11ViewModel();

            // A1C102 : 고객관리
            var quesResult1s = await _quesResult1Service.GetQuesResult1sAsync(int.Parse(questionSn), "A1C102");

            if (quesResult1s.Count() != 5)
            {
                var quesCheckList = await _quesCheckListService.GetQuesCheckListAsync("A1C102");
                var quesCheckListView = Mapper.Map<List<QuesCheckListViewModel>>(quesCheckList);

                foreach (var item in quesCheckListView)
                {
                    item.QuestionSn = int.Parse(questionSn);
                    item.AnsVal = false;
                }

                bizCheck11.CustomerMng = quesCheckListView;
            }
            else
            {
                var quesCheckListView = Mapper.Map<List<QuesCheckListViewModel>>(quesResult1s);
                bizCheck11.CustomerMng = quesCheckListView;
            }

            bizCheck11.Status = status;
            bizCheck11.QuestionSn = int.Parse(questionSn);
            return View(bizCheck11);
        }

        [HttpPost]
        public async Task<ActionResult> BizCheck11(BizCheck11ViewModel bizCheck11ViewModel)
        {
            ViewBag.naviLeftMenu = Global.Report;
            int questionSn = bizCheck11ViewModel.QuestionSn;

            if (bizCheck11ViewModel.QuestionSn > 0)
            {
                var quesMaster = await _quesMasterService.GetQuesResult1Async(questionSn);

                if (bizCheck11ViewModel.SubmitType == "N")
                {
                    quesMaster.SaveStatus = 14;
                }

                // A1B108 : 품질관리
                foreach (var item in bizCheck11ViewModel.CustomerMng)
                {
                    var checkItem = quesMaster.QuesResult1.SingleOrDefault(m => m.QuestionSn == questionSn && m.CheckListSn == item.CheckListSn);
                    if (checkItem == null)
                    {
                        var result1 = new QuesResult1();
                        result1.QuestionSn = questionSn;
                        result1.CheckListSn = item.CheckListSn;
                        result1.AnsVal = item.AnsVal;
                        result1.RegDt = DateTime.Now;
                        result1.RegId = Session[Global.LoginID].ToString();
                        quesMaster.QuesResult1.Add(result1);
                    }
                    else
                    {
                        checkItem.AnsVal = item.AnsVal;
                        checkItem.UpdDt = DateTime.Now;
                        checkItem.UpdId = Session[Global.LoginID].ToString();
                    }
                }

                await _quesMasterService.SaveDbContextAsync();
            }
            else
            {
                //에러처리 필요
                return View(bizCheck11ViewModel);
            }

            if (bizCheck11ViewModel.SubmitType == "T")
            {
                return RedirectToAction("BizCheck11", "Report", new { @questionSn = questionSn });
            }
            else
            {
                return RedirectToAction("BizCheck12", "Report", new { @questionSn = questionSn });
            }
        }



        public async Task<ActionResult> BizCheck12(string questionSn, string status)
        {
            ViewBag.naviLeftMenu = Global.Report;

            if (string.IsNullOrEmpty(questionSn))
            {
                // 오류 처리해야함.
                return View();
            }

            var bizCheck12 = new BizCheck12ViewModel();

            // A1D101 : 인적자윈의 확보와 개발관리
            var quesResult1s = await _quesResult1Service.GetQuesResult1sAsync(int.Parse(questionSn), "A1D101");

            if (quesResult1s.Count() != 5)
            {
                var quesCheckList = await _quesCheckListService.GetQuesCheckListAsync("A1D101");
                var quesCheckListView = Mapper.Map<List<QuesCheckListViewModel>>(quesCheckList);

                foreach (var item in quesCheckListView)
                {
                    item.QuestionSn = int.Parse(questionSn);
                    item.AnsVal = false;
                }

                bizCheck12.HRMng = quesCheckListView;
            }
            else
            {
                var quesCheckListView = Mapper.Map<List<QuesCheckListViewModel>>(quesResult1s);
                bizCheck12.HRMng = quesCheckListView;
            }

            // A1D102 : 이적자원의 보상 및 유지관리
            var quesResult1sMaintenance = await _quesResult1Service.GetQuesResult1sAsync(int.Parse(questionSn), "A1D102");

            if (quesResult1sMaintenance.Count() != 6)
            {
                var quesCheckList = await _quesCheckListService.GetQuesCheckListAsync("A1D102");
                var quesCheckListView = Mapper.Map<List<QuesCheckListViewModel>>(quesCheckList);

                foreach (var item in quesCheckListView)
                {
                    item.QuestionSn = int.Parse(questionSn);
                    item.AnsVal = false;
                }

                bizCheck12.HRMaintenance = quesCheckListView;
            }
            else
            {
                var quesCheckListView = Mapper.Map<List<QuesCheckListViewModel>>(quesResult1sMaintenance);
                bizCheck12.HRMaintenance = quesCheckListView;
            }

            bizCheck12.Status = status;
            bizCheck12.QuestionSn = int.Parse(questionSn);
            return View(bizCheck12);
        }

        [HttpPost]
        public async Task<ActionResult> BizCheck12(BizCheck12ViewModel bizCheck12ViewModel)
        {
            ViewBag.naviLeftMenu = Global.Report;
            int questionSn = bizCheck12ViewModel.QuestionSn;

            if (bizCheck12ViewModel.QuestionSn > 0)
            {
                var quesMaster = await _quesMasterService.GetQuesResult1Async(questionSn);

                if (bizCheck12ViewModel.SubmitType == "N")
                {
                    quesMaster.SaveStatus = 15;
                }

                // A1D101 : 인적자윈의 확보와 개발관리
                foreach (var item in bizCheck12ViewModel.HRMng)
                {
                    var checkItem = quesMaster.QuesResult1.SingleOrDefault(m => m.QuestionSn == questionSn && m.CheckListSn == item.CheckListSn);
                    if (checkItem == null)
                    {
                        var result1 = new QuesResult1();
                        result1.QuestionSn = questionSn;
                        result1.CheckListSn = item.CheckListSn;
                        result1.AnsVal = item.AnsVal;
                        result1.RegDt = DateTime.Now;
                        result1.RegId = Session[Global.LoginID].ToString();
                        quesMaster.QuesResult1.Add(result1);
                    }
                    else
                    {
                        checkItem.AnsVal = item.AnsVal;
                        checkItem.UpdDt = DateTime.Now;
                        checkItem.UpdId = Session[Global.LoginID].ToString();
                    }
                }


                // A1D102 : 이적자원의 보상 및 유지관리
                foreach (var item in bizCheck12ViewModel.HRMaintenance)
                {
                    var checkItem = quesMaster.QuesResult1.SingleOrDefault(m => m.QuestionSn == questionSn && m.CheckListSn == item.CheckListSn);
                    if (checkItem == null)
                    {
                        var result1 = new QuesResult1();
                        result1.QuestionSn = questionSn;
                        result1.CheckListSn = item.CheckListSn;
                        result1.AnsVal = item.AnsVal;
                        result1.RegDt = DateTime.Now;
                        result1.RegId = Session[Global.LoginID].ToString();
                        quesMaster.QuesResult1.Add(result1);
                    }
                    else
                    {
                        checkItem.AnsVal = item.AnsVal;
                        checkItem.UpdDt = DateTime.Now;
                        checkItem.UpdId = Session[Global.LoginID].ToString();
                    }
                }

                await _quesMasterService.SaveDbContextAsync();
            }
            else
            {
                //에러처리 필요
                return View(bizCheck12ViewModel);
            }

            if (bizCheck12ViewModel.SubmitType == "T")
            {
                return RedirectToAction("BizCheck12", "Report", new { @questionSn = questionSn });
            }
            else
            {
                return RedirectToAction("BizCheck13", "Report", new { @questionSn = questionSn });
            }
        }



        public async Task<ActionResult> BizCheck13(string questionSn, string status)
        {
            ViewBag.naviLeftMenu = Global.Report;

            if (string.IsNullOrEmpty(questionSn))
            {
                // 오류 처리해야함.
                return View();
            }

            // 설립년도 가져오기
            var quesCompInfo = await _quesCompInfoService.GetQuesCompInfoAsync(int.Parse(questionSn));
            var quesCompInfoView = Mapper.Map<QuesCompanyInfoViewModel>(quesCompInfo);
            var publishDt = quesCompInfoView.PublishDt.Substring(0, 4);

            // 0415 
            var bizCheck04 = new BizCheck04ViewModel();

            // A1A202 : 조직만족도
            var quesResult2sFor4 = await _quesResult2Service.GetQuesResult2sAsync(int.Parse(questionSn), "A1A202");

            if (quesResult2sFor4.Count() != 3)
            {
                var quesCheckList = await _quesCheckListService.GetQuesCheckListAsync("A1A202");
                var quesYearListView = Mapper.Map<List<QuesYearListViewModel>>(quesCheckList);

                foreach (var item in quesYearListView)
                {
                    item.QuestionSn = int.Parse(questionSn);
                    item.BasicYear = DateTime.Now.Year;
                    item.D = "0";
                    item.D451 = "0";
                    item.D452 = "0";
                    item.D453 = "0";
                }
                //총직원
                bizCheck04.TotalEmp = quesYearListView.SingleOrDefault(i => i.DetailCd == "A1A20201");
                //이직직원
                bizCheck04.MoveEmp = quesYearListView.SingleOrDefault(i => i.DetailCd == "A1A20202");
                //신규직원
                bizCheck04.NewEmp = quesYearListView.SingleOrDefault(i => i.DetailCd == "A1A20203");
            }
            else
            {
                var quesYearListView = Mapper.Map<List<QuesYearListViewModel>>(quesResult2sFor4);
                //총직원
                bizCheck04.TotalEmp = quesYearListView.SingleOrDefault(i => i.DetailCd == "A1A20201");
                //이직직원
                bizCheck04.MoveEmp = quesYearListView.SingleOrDefault(i => i.DetailCd == "A1A20202");
                //신규직원
                bizCheck04.NewEmp = quesYearListView.SingleOrDefault(i => i.DetailCd == "A1A20203");
            }


            var bizCheck13 = new BizCheck13ViewModel();

            // A1E102 : 지적재산권성과
            var quesResult2s = await _quesResult2Service.GetQuesResult2sAsync(int.Parse(questionSn), "A1E102");

            if (quesResult2s.Count() != 5)
            {
                var quesCheckList = await _quesCheckListService.GetQuesCheckListAsync("A1E102");
                var quesYearListView = Mapper.Map<List<QuesYearListViewModel>>(quesCheckList);

                foreach (var item in quesYearListView)
                {
                    item.QuestionSn = int.Parse(questionSn);
                    item.BasicYear = DateTime.Now.Year;
                    item.D = "0";
                    item.D451 = "0";
                    item.D452 = "0";
                    item.D453 = "0";
                }

                //등록 특허
                bizCheck13.RegPatent = quesYearListView.SingleOrDefault(i => i.DetailCd == "A1E10201");
                //등록 실용신안
                bizCheck13.RegUtilityModel = quesYearListView.SingleOrDefault(i => i.DetailCd == "A1E10202");
                //출원 특허
                bizCheck13.ApplyPatent = quesYearListView.SingleOrDefault(i => i.DetailCd == "A1E10203");
                //출원 실용신안
                bizCheck13.ApplyUtilityModel = quesYearListView.SingleOrDefault(i => i.DetailCd == "A1E10204");
                //기타
                bizCheck13.Etc = quesYearListView.SingleOrDefault(i => i.DetailCd == "A1E10205");
            }
            else
            {
                var quesYearListView = Mapper.Map<List<QuesYearListViewModel>>(quesResult2s);
                //등록 특허
                bizCheck13.RegPatent = quesYearListView.SingleOrDefault(i => i.DetailCd == "A1E10201");
                //등록 실용신안
                bizCheck13.RegUtilityModel = quesYearListView.SingleOrDefault(i => i.DetailCd == "A1E10202");
                //출원 특허
                bizCheck13.ApplyPatent = quesYearListView.SingleOrDefault(i => i.DetailCd == "A1E10203");
                //출원 실용신안
                bizCheck13.ApplyUtilityModel = quesYearListView.SingleOrDefault(i => i.DetailCd == "A1E10204");
                //기타
                bizCheck13.Etc = quesYearListView.SingleOrDefault(i => i.DetailCd == "A1E10205");
            }


            // A1E103 : 임직원 수
            var quesResult2sTotalEmp = await _quesResult2Service.GetQuesResult2sAsync(int.Parse(questionSn), "A1E103");

            if (quesResult2sTotalEmp.Count() != 1)
            {
                var quesCheckList = await _quesCheckListService.GetQuesCheckListAsync("A1E103");
                var quesYearListView = Mapper.Map<List<QuesYearListViewModel>>(quesCheckList);

                foreach (var item in quesYearListView)
                {
                    item.QuestionSn = int.Parse(questionSn);
                    item.BasicYear = DateTime.Now.Year;
                    item.D = "0";
                    item.D451 = "0";
                    item.D452 = "0";
                    item.D453 = "0";
                }

                //전체 임직원
                bizCheck13.TotalEmp = quesYearListView.SingleOrDefault(i => i.DetailCd == "A1E10301");

                bizCheck13.TotalEmp.D = bizCheck04.TotalEmp.D;
                bizCheck13.TotalEmp.D451 = bizCheck04.TotalEmp.D451;
                bizCheck13.TotalEmp.D452 = bizCheck04.TotalEmp.D452;
                bizCheck13.TotalEmp.D453 = bizCheck04.TotalEmp.D453;

            }
            else
            {
                var quesYearListView = Mapper.Map<List<QuesYearListViewModel>>(quesResult2sTotalEmp);

                //전체 임직원
                bizCheck13.TotalEmp = quesYearListView.SingleOrDefault(i => i.DetailCd == "A1E10301");

                bizCheck13.TotalEmp.D = bizCheck04.TotalEmp.D;
                bizCheck13.TotalEmp.D451 = bizCheck04.TotalEmp.D451;
                bizCheck13.TotalEmp.D452 = bizCheck04.TotalEmp.D452;
                bizCheck13.TotalEmp.D453 = bizCheck04.TotalEmp.D453;
            }

            bizCheck13.Status = status;
            bizCheck13.QuestionSn = int.Parse(questionSn);
            return View(bizCheck13);
        }

        [HttpPost]
        public async Task<ActionResult> BizCheck13(BizCheck13ViewModel bizCheck13ViewModel)
        {
            ViewBag.naviLeftMenu = Global.Report;
            int questionSn = bizCheck13ViewModel.QuestionSn;

            if (bizCheck13ViewModel.QuestionSn > 0)
            {
                var quesYearMaster = await _quesMasterService.GetQuesResult2Async(questionSn);

                if (bizCheck13ViewModel.SubmitType == "N")
                {
                    quesYearMaster.SaveStatus = 16;
                }

                //등록특허
                var yearRegPatentItem = quesYearMaster.QuesResult2.SingleOrDefault(m => m.QuestionSn == questionSn && m.CheckListSn == bizCheck13ViewModel.RegPatent.CheckListSn);
                if (yearRegPatentItem == null)
                {
                    var quesYear = Mapper.Map<QuesResult2>(bizCheck13ViewModel.RegPatent);
                    quesYear.QuestionSn = questionSn;
                    quesYear.RegDt = DateTime.Now;
                    quesYear.RegId = Session[Global.LoginID].ToString();
                    quesYear.BasicYear = quesYearMaster.BasicYear;
                    quesYearMaster.QuesResult2.Add(quesYear);
                }
                else
                {
                    yearRegPatentItem.D = bizCheck13ViewModel.RegPatent.D;
                    yearRegPatentItem.UpdDt = DateTime.Now;
                    yearRegPatentItem.UpdId = Session[Global.LoginID].ToString();
                }

                //등록실용신안
                var yearRegUtilityModelItem = quesYearMaster.QuesResult2.SingleOrDefault(m => m.QuestionSn == questionSn && m.CheckListSn == bizCheck13ViewModel.RegUtilityModel.CheckListSn);
                if (yearRegUtilityModelItem == null)
                {
                    var quesYear = Mapper.Map<QuesResult2>(bizCheck13ViewModel.RegUtilityModel);
                    quesYear.QuestionSn = questionSn;
                    quesYear.RegDt = DateTime.Now;
                    quesYear.RegId = Session[Global.LoginID].ToString();
                    quesYear.BasicYear = quesYearMaster.BasicYear;
                    quesYearMaster.QuesResult2.Add(quesYear);
                }
                else
                {
                    yearRegUtilityModelItem.D = bizCheck13ViewModel.RegUtilityModel.D;
                    yearRegUtilityModelItem.UpdDt = DateTime.Now;
                    yearRegUtilityModelItem.UpdId = Session[Global.LoginID].ToString();
                }

                //출원특허
                var yearApplyPatentItem = quesYearMaster.QuesResult2.SingleOrDefault(m => m.QuestionSn == questionSn && m.CheckListSn == bizCheck13ViewModel.ApplyPatent.CheckListSn);
                if (yearApplyPatentItem == null)
                {
                    var quesYear = Mapper.Map<QuesResult2>(bizCheck13ViewModel.ApplyPatent);
                    quesYear.QuestionSn = questionSn;
                    quesYear.RegDt = DateTime.Now;
                    quesYear.RegId = Session[Global.LoginID].ToString();
                    quesYear.BasicYear = quesYearMaster.BasicYear;
                    quesYearMaster.QuesResult2.Add(quesYear);
                }
                else
                {
                    yearApplyPatentItem.D = bizCheck13ViewModel.ApplyPatent.D;
                    yearApplyPatentItem.UpdDt = DateTime.Now;
                    yearApplyPatentItem.UpdId = Session[Global.LoginID].ToString();
                }

                //출원실용신안
                var yearApplyUtilityModelItem = quesYearMaster.QuesResult2.SingleOrDefault(m => m.QuestionSn == questionSn && m.CheckListSn == bizCheck13ViewModel.ApplyUtilityModel.CheckListSn);
                if (yearApplyUtilityModelItem == null)
                {
                    var quesYear = Mapper.Map<QuesResult2>(bizCheck13ViewModel.ApplyUtilityModel);
                    quesYear.QuestionSn = questionSn;
                    quesYear.RegDt = DateTime.Now;
                    quesYear.RegId = Session[Global.LoginID].ToString();
                    quesYear.BasicYear = quesYearMaster.BasicYear;
                    quesYearMaster.QuesResult2.Add(quesYear);
                }
                else
                {
                    yearApplyUtilityModelItem.D = bizCheck13ViewModel.ApplyUtilityModel.D;
                    yearApplyUtilityModelItem.UpdDt = DateTime.Now;
                    yearApplyUtilityModelItem.UpdId = Session[Global.LoginID].ToString();
                }

                //기타
                var yearEtcItem = quesYearMaster.QuesResult2.SingleOrDefault(m => m.QuestionSn == questionSn && m.CheckListSn == bizCheck13ViewModel.Etc.CheckListSn);
                if (yearEtcItem == null)
                {
                    var quesYear = Mapper.Map<QuesResult2>(bizCheck13ViewModel.Etc);
                    quesYear.QuestionSn = questionSn;
                    quesYear.RegDt = DateTime.Now;
                    quesYear.RegId = Session[Global.LoginID].ToString();
                    quesYear.BasicYear = quesYearMaster.BasicYear;
                    quesYearMaster.QuesResult2.Add(quesYear);
                }
                else
                {
                    yearEtcItem.D = bizCheck13ViewModel.Etc.D;
                    yearEtcItem.UpdDt = DateTime.Now;
                    yearEtcItem.UpdId = Session[Global.LoginID].ToString();
                }


                //전체 임직원
                var yearTotalEmpItem = quesYearMaster.QuesResult2.SingleOrDefault(m => m.QuestionSn == questionSn && m.CheckListSn == bizCheck13ViewModel.TotalEmp.CheckListSn);
                if (yearTotalEmpItem == null)
                {
                    var quesYear = Mapper.Map<QuesResult2>(bizCheck13ViewModel.TotalEmp);
                    quesYear.QuestionSn = questionSn;
                    quesYear.RegDt = DateTime.Now;
                    quesYear.RegId = Session[Global.LoginID].ToString();
                    quesYear.BasicYear = quesYearMaster.BasicYear;
                    quesYearMaster.QuesResult2.Add(quesYear);
                }
                else
                {
                    yearTotalEmpItem.D = bizCheck13ViewModel.TotalEmp.D;
                    yearTotalEmpItem.D451 = bizCheck13ViewModel.TotalEmp.D451;
                    yearTotalEmpItem.D452 = bizCheck13ViewModel.TotalEmp.D452;
                    yearTotalEmpItem.D453 = bizCheck13ViewModel.TotalEmp.D453;
                    yearTotalEmpItem.UpdDt = DateTime.Now;
                    yearTotalEmpItem.UpdId = Session[Global.LoginID].ToString();
                }

                await _quesMasterService.SaveDbContextAsync();
            }
            else
            {
                //에러처리 필요
                return View(bizCheck13ViewModel);
            }

            if (bizCheck13ViewModel.SubmitType == "T")
            {
                return RedirectToAction("BizCheck13", "Report", new { @questionSn = questionSn });
            }
            else
            {
                return RedirectToAction("FinanceCheck01", "Report", new { @questionSn = questionSn });
            }
        }



        public async Task<ActionResult> OrgCheck01(string questionSn, string status)
        {
            ViewBag.naviLeftMenu = Global.Report;

            if (string.IsNullOrEmpty(questionSn))
            {
                // 오류 처리해야함.
                return View();
            }

            var orgCheck01 = new OrgCheck01ViewModel();

            // 조직만족도 상의 값을 가져오기
            var quesResult2s = await _quesResult2Service.GetQuesResult2sAsync(int.Parse(questionSn), "A1A202");


            // 조직구성 조회
            var quesMaster = await _quesMasterService.GetQuesOgranAnalysisAsync(int.Parse(questionSn));

            //기획관리
            var management = quesMaster.QuesOgranAnalysis.SingleOrDefault(i => i.DeptCd == "M");

            if (management == null)
            {
                orgCheck01.Management = new OrgCompositionViewModel();
            }
            else
            {
                orgCheck01.Management = Mapper.Map<OrgCompositionViewModel>(management);
            }

            //생산관리
            var produce = quesMaster.QuesOgranAnalysis.SingleOrDefault(i => i.DeptCd == "P");

            if (produce == null)
            {
                orgCheck01.Produce = new OrgCompositionViewModel();
            }
            else
            {
                orgCheck01.Produce = Mapper.Map<OrgCompositionViewModel>(produce);
            }

            //연구개발
            var rnd = quesMaster.QuesOgranAnalysis.SingleOrDefault(i => i.DeptCd == "R");

            if (rnd == null)
            {
                orgCheck01.RND = new OrgCompositionViewModel();
            }
            else
            {
                orgCheck01.RND = Mapper.Map<OrgCompositionViewModel>(rnd);
            }

            //마케팅
            var salse = quesMaster.QuesOgranAnalysis.SingleOrDefault(i => i.DeptCd == "S");

            if (salse == null)
            {
                orgCheck01.Salse = new OrgCompositionViewModel();
            }
            else
            {
                orgCheck01.Salse = Mapper.Map<OrgCompositionViewModel>(salse);
            }

            orgCheck01.StaffSumCount = orgCheck01.Management.StaffCount + orgCheck01.Produce.StaffCount + orgCheck01.RND.StaffCount + orgCheck01.Salse.StaffCount;

            orgCheck01.ChiefSumCount = orgCheck01.Management.ChiefCount + orgCheck01.Produce.ChiefCount + orgCheck01.RND.ChiefCount + orgCheck01.Salse.ChiefCount;

            orgCheck01.OfficerSumCount = orgCheck01.Management.OfficerCount + orgCheck01.Produce.OfficerCount + orgCheck01.RND.OfficerCount + orgCheck01.Salse.OfficerCount;

            orgCheck01.BeginnerSumCount = orgCheck01.Management.BeginnerCount + orgCheck01.Produce.BeginnerCount + orgCheck01.RND.BeginnerCount + orgCheck01.Salse.BeginnerCount;

            orgCheck01.TotalSumCount = orgCheck01.StaffSumCount + orgCheck01.ChiefSumCount + orgCheck01.OfficerSumCount + orgCheck01.BeginnerSumCount;

            var boss = quesMaster.QuesOgranAnalysis.SingleOrDefault(i => i.DeptCd == "C");

            if (boss == null)
            {
                orgCheck01.BossType = "1";
            }
            else
            {
                if (boss.OfficerCount == 1)
                {
                    orgCheck01.BossType = "1";
                }
                else if (boss.ChiefCount == 1)
                {
                    orgCheck01.BossType = "2";
                }
                else if (boss.StaffCount == 1)
                {
                    orgCheck01.BossType = "3";
                }
                else
                {
                    orgCheck01.BossType = "4";
                }
            }

            // bizcheck04번에서 가져올 data
            var bizCheck04 = new BizCheck04ViewModel();

            // A1A202 : 조직만족도
            var quesResult2sFor4 = await _quesResult2Service.GetQuesResult2sAsync(int.Parse(questionSn), "A1A202");

            if (quesResult2sFor4.Count() != 3)
            {
                var quesCheckList = await _quesCheckListService.GetQuesCheckListAsync("A1A202");
                var quesYearListView = Mapper.Map<List<QuesYearListViewModel>>(quesCheckList);

                foreach (var item in quesYearListView)
                {
                    item.QuestionSn = int.Parse(questionSn);
                    item.BasicYear = DateTime.Now.Year;
                    item.D = "0";
                    item.D451 = "0";
                    item.D452 = "0";
                    item.D453 = "0";
                }
                //총직원
                bizCheck04.TotalEmp = quesYearListView.SingleOrDefault(i => i.DetailCd == "A1A20201");
                //이직직원
                bizCheck04.MoveEmp = quesYearListView.SingleOrDefault(i => i.DetailCd == "A1A20202");
                //신규직원
                bizCheck04.NewEmp = quesYearListView.SingleOrDefault(i => i.DetailCd == "A1A20203");
            }
            else
            {
                var quesYearListView = Mapper.Map<List<QuesYearListViewModel>>(quesResult2sFor4);
                //총직원
                bizCheck04.TotalEmp = quesYearListView.SingleOrDefault(i => i.DetailCd == "A1A20201");
                //이직직원
                bizCheck04.MoveEmp = quesYearListView.SingleOrDefault(i => i.DetailCd == "A1A20202");
                //신규직원
                bizCheck04.NewEmp = quesYearListView.SingleOrDefault(i => i.DetailCd == "A1A20203");
            }

            orgCheck01.CorrectValue = bizCheck04.TotalEmp.D;
            orgCheck01.Status = status;
            orgCheck01.QuestionSn = int.Parse(questionSn);
            return View(orgCheck01);
        }

        [HttpPost]
        public async Task<ActionResult> OrgCheck01(OrgCheck01ViewModel orgCheck01ViewModel)
        {
            ViewBag.naviLeftMenu = Global.Report;
            int questionSn = orgCheck01ViewModel.QuestionSn;

            if (orgCheck01ViewModel.QuestionSn > 0)
            {
                var quesMaster = await _quesMasterService.GetQuesOgranAnalysisAsync(questionSn);

                if (orgCheck01ViewModel.SubmitType == "N")
                {
                    quesMaster.SaveStatus = 17;
                }

                //기획관리
                var management = quesMaster.QuesOgranAnalysis.SingleOrDefault(i => i.DeptCd == "M");

                if (management == null)
                {
                    var QuesOgranAnalMgmt = Mapper.Map<QuesOgranAnalysis>(orgCheck01ViewModel.Management);
                    if(QuesOgranAnalMgmt.OfficerCount == null)
                    {
                        QuesOgranAnalMgmt.OfficerCount = 0;
                    }
                    if (QuesOgranAnalMgmt.ChiefCount == null)
                    {
                        QuesOgranAnalMgmt.ChiefCount = 0;
                    }
                    if (QuesOgranAnalMgmt.StaffCount == null)
                    {
                        QuesOgranAnalMgmt.StaffCount = 0;
                    }
                    if (QuesOgranAnalMgmt.BeginnerCount == null)
                    {
                        QuesOgranAnalMgmt.BeginnerCount = 0;
                    }
                    QuesOgranAnalMgmt.QuestionSn = questionSn;
                    QuesOgranAnalMgmt.DeptCd = "M";
                    QuesOgranAnalMgmt.RegDt = DateTime.Now;
                    QuesOgranAnalMgmt.RegId = Session[Global.LoginID].ToString();
                    quesMaster.QuesOgranAnalysis.Add(QuesOgranAnalMgmt);
                }
                else
                {
                    management.Dept1 = orgCheck01ViewModel.Management.Dept1;
                    management.Dept2 = orgCheck01ViewModel.Management.Dept2;
                    management.OfficerCount = orgCheck01ViewModel.Management.OfficerCount;
                    management.ChiefCount = orgCheck01ViewModel.Management.ChiefCount;
                    management.StaffCount = orgCheck01ViewModel.Management.StaffCount;
                    management.BeginnerCount = orgCheck01ViewModel.Management.BeginnerCount;
                    management.UpdDt = DateTime.Now;
                    management.UpdId = Session[Global.LoginID].ToString();
                }

                //생산관리
                var produce = quesMaster.QuesOgranAnalysis.SingleOrDefault(i => i.DeptCd == "P");

                if (produce == null)
                {
                    var QuesOgranAnalProd = Mapper.Map<QuesOgranAnalysis>(orgCheck01ViewModel.Produce);
                    if (QuesOgranAnalProd.OfficerCount == null)
                    {
                        QuesOgranAnalProd.OfficerCount = 0;
                    }
                    if (QuesOgranAnalProd.ChiefCount == null)
                    {
                        QuesOgranAnalProd.ChiefCount = 0;
                    }
                    if (QuesOgranAnalProd.StaffCount == null)
                    {
                        QuesOgranAnalProd.StaffCount = 0;
                    }
                    if (QuesOgranAnalProd.BeginnerCount == null)
                    {
                        QuesOgranAnalProd.BeginnerCount = 0;
                    }
                    QuesOgranAnalProd.QuestionSn = questionSn;
                    QuesOgranAnalProd.DeptCd = "P";
                    QuesOgranAnalProd.RegDt = DateTime.Now;
                    QuesOgranAnalProd.RegId = Session[Global.LoginID].ToString();
                    quesMaster.QuesOgranAnalysis.Add(QuesOgranAnalProd);
                }
                else
                {
                    produce.Dept1 = orgCheck01ViewModel.Produce.Dept1;
                    produce.Dept2 = orgCheck01ViewModel.Produce.Dept2;
                    produce.OfficerCount = orgCheck01ViewModel.Produce.OfficerCount;
                    produce.ChiefCount = orgCheck01ViewModel.Produce.ChiefCount;
                    produce.StaffCount = orgCheck01ViewModel.Produce.StaffCount;
                    produce.BeginnerCount = orgCheck01ViewModel.Produce.BeginnerCount;
                    produce.UpdDt = DateTime.Now;
                    produce.UpdId = Session[Global.LoginID].ToString();
                }

                //연구개발
                var rnd = quesMaster.QuesOgranAnalysis.SingleOrDefault(i => i.DeptCd == "R");

                if (rnd == null)
                {
                    var QuesOgranAnalRnd = Mapper.Map<QuesOgranAnalysis>(orgCheck01ViewModel.RND);
                    if (QuesOgranAnalRnd.OfficerCount == null)
                    {
                        QuesOgranAnalRnd.OfficerCount = 0;
                    }
                    if (QuesOgranAnalRnd.ChiefCount == null)
                    {
                        QuesOgranAnalRnd.ChiefCount = 0;
                    }
                    if (QuesOgranAnalRnd.StaffCount == null)
                    {
                        QuesOgranAnalRnd.StaffCount = 0;
                    }
                    if (QuesOgranAnalRnd.BeginnerCount == null)
                    {
                        QuesOgranAnalRnd.BeginnerCount = 0;
                    }
                    QuesOgranAnalRnd.QuestionSn = questionSn;
                    QuesOgranAnalRnd.DeptCd = "R";
                    QuesOgranAnalRnd.RegDt = DateTime.Now;
                    QuesOgranAnalRnd.RegId = Session[Global.LoginID].ToString();
                    quesMaster.QuesOgranAnalysis.Add(QuesOgranAnalRnd);
                }
                else
                {
                    rnd.Dept1 = orgCheck01ViewModel.RND.Dept1;
                    rnd.Dept2 = orgCheck01ViewModel.RND.Dept2;
                    rnd.OfficerCount = orgCheck01ViewModel.RND.OfficerCount;
                    rnd.ChiefCount = orgCheck01ViewModel.RND.ChiefCount;
                    rnd.StaffCount = orgCheck01ViewModel.RND.StaffCount;
                    rnd.BeginnerCount = orgCheck01ViewModel.RND.BeginnerCount;
                    rnd.UpdDt = DateTime.Now;
                    rnd.UpdId = Session[Global.LoginID].ToString();
                }

                //마케팅
                var salse = quesMaster.QuesOgranAnalysis.SingleOrDefault(i => i.DeptCd == "S");

                if (salse == null)
                {
                    var QuesOgranAnalSalse = Mapper.Map<QuesOgranAnalysis>(orgCheck01ViewModel.Salse);
                    if (QuesOgranAnalSalse.OfficerCount == null)
                    {
                        QuesOgranAnalSalse.OfficerCount = 0;
                    }
                    if (QuesOgranAnalSalse.ChiefCount == null)
                    {
                        QuesOgranAnalSalse.ChiefCount = 0;
                    }
                    if (QuesOgranAnalSalse.StaffCount == null)
                    {
                        QuesOgranAnalSalse.StaffCount = 0;
                    }
                    if (QuesOgranAnalSalse.BeginnerCount == null)
                    {
                        QuesOgranAnalSalse.BeginnerCount = 0;
                    }
                    QuesOgranAnalSalse.QuestionSn = questionSn;
                    QuesOgranAnalSalse.DeptCd = "S";
                    QuesOgranAnalSalse.RegDt = DateTime.Now;
                    QuesOgranAnalSalse.RegId = Session[Global.LoginID].ToString();
                    quesMaster.QuesOgranAnalysis.Add(QuesOgranAnalSalse);
                }
                else
                {
                    salse.Dept1 = orgCheck01ViewModel.Salse.Dept1;
                    salse.Dept2 = orgCheck01ViewModel.Salse.Dept2;
                    salse.OfficerCount = orgCheck01ViewModel.Salse.OfficerCount;
                    salse.ChiefCount = orgCheck01ViewModel.Salse.ChiefCount;
                    salse.StaffCount = orgCheck01ViewModel.Salse.StaffCount;
                    salse.BeginnerCount = orgCheck01ViewModel.Salse.BeginnerCount;
                    salse.UpdDt = DateTime.Now;
                    salse.UpdId = Session[Global.LoginID].ToString();
                }

                // 0412 - phm - 대표자 체크
                var boss = quesMaster.QuesOgranAnalysis.SingleOrDefault(i => i.DeptCd == "C");

                if (boss == null) // db에 값이 존재하지 않음
                {
                    QuesOgranAnalysis oo = new QuesOgranAnalysis();
                    string compare = orgCheck01ViewModel.BossType;

                    oo.QuestionSn = questionSn;
                    oo.DeptCd = "C";

                    if (compare == "1")
                    {
                        oo.OfficerCount = 1;
                    }
                    else if (compare == "2")
                    {
                        oo.ChiefCount = 1;
                    }
                    else if (compare == "3")
                    {
                        oo.StaffCount = 1;
                    }
                    else
                    {
                        oo.BeginnerCount = 1;
                    }

                    oo.RegDt = DateTime.Now;
                    oo.RegId = Session[Global.LoginID].ToString();
                    quesMaster.QuesOgranAnalysis.Add(oo);
                }
                else // db에 값이 존재함
                {
                    string compare = orgCheck01ViewModel.BossType;
                    if (compare == "1")
                    {
                        boss.OfficerCount = 1;
                        boss.ChiefCount = 0;
                        boss.StaffCount = 0;
                        boss.BeginnerCount = 0;
                    }
                    else if (compare == "2")
                    {
                        boss.OfficerCount = 0;
                        boss.ChiefCount = 1;
                        boss.StaffCount = 0;
                        boss.BeginnerCount = 0;
                    }
                    else if (compare == "3")
                    {
                        boss.OfficerCount = 0;
                        boss.ChiefCount = 0;
                        boss.StaffCount = 1;
                        boss.BeginnerCount = 0;
                    }
                    else
                    {
                        boss.OfficerCount = 0;
                        boss.ChiefCount = 0;
                        boss.StaffCount = 0;
                        boss.BeginnerCount = 1;
                    }
                    boss.UpdDt = DateTime.Now;
                    boss.UpdId = Session[Global.LoginID].ToString();
                }

                await _quesMasterService.SaveDbContextAsync();
            }
            else
            {
                //에러처리 필요
                return View(orgCheck01ViewModel);
            }

            if (orgCheck01ViewModel.SubmitType == "T")
            {
                return RedirectToAction("OrgCheck01", "Report", new { @questionSn = questionSn });
            }
            else
            {
                return RedirectToAction("BasicSurveyComplete", "Report", new { @questionSn = questionSn });
            }
        }


        public ActionResult BasicSurveyComplete(string questionSn, string status)
        {
            ViewBag.naviLeftMenu = Global.Report;

            var viewModel = new QuesViewModel();
            viewModel.QuestionSn = int.Parse(questionSn);
            viewModel.Status = status;
            return View(viewModel);
        }

        // 문진표 마무리
        [HttpPost]
        public async Task<ActionResult> BasicSurveyComplete(QuesViewModel quesViewModel)
        {
            ViewBag.naviLeftMenu = Global.Report;

            int questionSn = quesViewModel.QuestionSn;          // viewModel에 붙어서 들어오는 questionSn

            string num;

            if (quesViewModel.QuestionSn > 0)                   // questionSn이 null이 아닐경우
            {
                var quesMaster = await _quesMasterService.GetQuesMasterAsync(questionSn);   // 데이터를 넣을 문진표Master객체 할당
                quesMaster.Status = "C";                                                    // 문진표 status를 C로 만들고
                await _quesMasterService.SaveDbContextAsync();                              // 저장

                num = await makeNumSn(quesMaster.BasicYear ?? default(int));                // 필요한 numSn 구하기 성공

                var vcCompInfo = await _vcCompInfoService.getVcCompInfoByKey(int.Parse(Session[Global.LoginID].ToString())); // tcmsLoginKey로 기업정보 조회

                var vcMentorMapping = await vcMentorMappingService.getVcMentorMappingSingleByCompSn(vcCompInfo.CompSn.ToString(), num); // compsn, WriteYn="Y"를 통하여 vcMentorMapping 객체 조회 +) numSn도 마찬가지

                var vcMentorInfo = await _scUsrService.getMentorInfoBySn(vcMentorMapping.MentorSn.ToString());

                var rptMasterObj = await _rptMasterService.GetRptMasterAsync(questionSn, vcCompInfo.CompSn, (int)quesMaster.BasicYear);

                var vcUsrInfo = await _scUsrService.selectScUsrByTcms(vcMentorInfo.TcmsLoginKey.ToString());

                if (rptMasterObj == null)
                {
                    RptMaster rptMaster = new RptMaster();

                    rptMaster.BasicYear = quesMaster.BasicYear.Value;       // 해당 문진표 기준년도
                    rptMaster.NumSn = vcMentorMapping.NumSn;                // 이렇게 적용하면 추후에 NumSn관련으로 문제가 생길 가능성이 있다 ************
                    rptMaster.Status = "T";                                 // 
                    rptMaster.QuestionSn = questionSn;                      // 해당 문진표식별자
                    rptMaster.CompSn = vcMentorMapping.CompSn;
                    rptMaster.MentorId = vcUsrInfo.LoginId;
                    rptMaster.RegDt = DateTime.Now;                         // 등록 일시
                    rptMaster.RegId = vcUsrInfo.LoginId;   // 문진표완료 등록자(기업) ID

                    await _rptMasterService.AddRptMasterAsync(rptMaster);
                }
                else
                {
                    rptMasterObj.BasicYear = quesMaster.BasicYear.Value;       // 해당 문진표 기준년도
                    rptMasterObj.NumSn = vcMentorMapping.NumSn;                // 이렇게 적용하면 추후에 NumSn관련으로 문제가 생길 가능성이 있다 ************
                    rptMasterObj.Status = "T";
                    rptMasterObj.QuestionSn = questionSn;
                    rptMasterObj.CompSn = vcMentorMapping.CompSn;
                    rptMasterObj.MentorId = vcUsrInfo.LoginId;
                    rptMasterObj.RegDt = DateTime.Now;
                    rptMasterObj.RegId = vcUsrInfo.LoginId;

                    _rptMasterService.ModifyRptMaster(rptMasterObj);
                }
            }
            else
            {
                //에러처리 필요
                return View(quesViewModel);
            }

            return RedirectToAction("BasicSurvey", "Report", new { area = "Company" });
        }

        // 문진표 작성 완료 전, 매핑된 멘토 확인
        [HttpPost]
        public async Task<JsonResult> mentorMappedExist(string questionSn)
        {
            int iQuestionSn = int.Parse(questionSn);

            string status = "F";
            string num = "";

            if (iQuestionSn > 0)
            {
                var quesMaster = await _quesMasterService.GetQuesMasterAsync(iQuestionSn);   // 데이터를 넣을 문진표Master객체 할당

                num = await makeNumSn(quesMaster.BasicYear ?? default(int));                // 필요한 numSn 구하기 성공

                var vcCompInfo = await _vcCompInfoService.getVcCompInfoByKey(int.Parse(Session[Global.LoginID].ToString())); // tcmsLoginKey로 기업정보 조회

                var vcMentorMapping = await vcMentorMappingService.getVcMentorMappingSingleByCompSn(vcCompInfo.CompSn.ToString(), num); // compsn, WriteYn="Y"를 통하여 vcMentorMapping 객체 조회 +) numSn도 마찬가지

                if (vcMentorMapping != null)
                {
                    status = "S";
                }
            }

            return Json(status);
        }

        //// 문진표 수정 액션
        //public async Task<ActionResult> ModifyBasicSurveyComplete(string questionSn)
        //{
        //    ViewBag.naviLeftMenu = Global.Report;

        //    int QuestionSn = int.Parse(questionSn);

        //    string compSn = Session[Global.CompSN].ToString();

        //    if (QuestionSn > 0)
        //    {
        //        var quesMaster = await _quesMasterService.GetQuesMasterAsync(QuestionSn);
        //        quesMaster.Status = "P";
        //        await _quesMasterService.SaveDbContextAsync();

        //        var compMappings = await _scCompMappingService.GetCompMappingsForCompanyAsync(int.Parse(compSn));
        //        for (int i = 0; i < compMappings.Count; i++)
        //        {
        //            var rptMasterObj = await _rptMasterService.GetRptMasterAsync(QuestionSn, int.Parse(compSn), (int)quesMaster.BasicYear);
        //            if (rptMasterObj == null)
        //            {
        //                RptMaster rptMaster = new RptMaster();
        //                rptMaster.BasicYear = quesMaster.BasicYear.Value;
        //                rptMaster.BizWorkSn = compMappings[i].BizWorkSn;
        //                rptMaster.Status = "T";
        //                rptMaster.QuestionSn = QuestionSn;
        //                rptMaster.CompSn = int.Parse(compSn);
        //                rptMaster.MentorId = compMappings[i].MentorId;
        //                rptMaster.RegDt = DateTime.Now;
        //                rptMaster.RegId = Session[Global.LoginID].ToString();

        //                await _rptMasterService.AddRptMasterAsync(rptMaster);
        //            }
        //            else
        //            {
        //                rptMasterObj.BasicYear = quesMaster.BasicYear.Value;
        //                rptMasterObj.BizWorkSn = compMappings[i].BizWorkSn;
        //                rptMasterObj.Status = "T";
        //                rptMasterObj.QuestionSn = QuestionSn;
        //                rptMasterObj.CompSn = int.Parse(compSn);
        //                rptMasterObj.MentorId = compMappings[i].MentorId;
        //                rptMasterObj.RegDt = DateTime.Now;
        //                rptMasterObj.RegId = Session[Global.LoginID].ToString();

        //                _rptMasterService.ModifyRptMaster(rptMasterObj);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        //에러처리 필요
        //        return View();
        //    }


        //    return RedirectToAction("BasicSurvey", "Report", new { area = "Company" });
        //}

        public ActionResult FinanceCheck01(string questionSn, string status)
        {
            ViewBag.naviLeftMenu = Global.Report;

            var viewModel = new QuesViewModel();
            viewModel.QuestionSn = int.Parse(questionSn);
            viewModel.Status = status;
            return View(viewModel);
        }

        public async Task<ActionResult> FinanceCheck02(string questionSn, string status)
        {
            ViewBag.naviLeftMenu = Global.Report;

            var vcCompInfo = await _vcCompInfoService.getVcCompInfoById(Session[Global.LoginID].ToString());
            int compSn = vcCompInfo.CompSn;
            var viewModel = new FinanceCheckViewModel();

            var qm = _quesMasterService.GetQuesMaster(int.Parse(questionSn));

            viewModel.QuestionSn = int.Parse(questionSn);
            viewModel.Status = status;
            viewModel.BasicYear = qm.BasicYear ?? default(int); // 2016

            var cur = _scCompanyFinanceService.getScCompanyFinance(compSn, viewModel.BasicYear - 2);    // 2015
            var pre = _scCompanyFinanceService.getScCompanyFinance(compSn, viewModel.BasicYear - 3);    // 2014

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

        // html에 돌려주는 값들
        public async Task<JsonResult> FinanceYearJson(int year) 
        {
            ViewBag.naviLeftMenu = Global.Report;

            var vcCompInfo = await _vcCompInfoService.getVcCompInfoById(Session[Global.LoginID].ToString());
            int compSn = vcCompInfo.CompSn;

            // 만약 해당하는 값이 없을 때 객체는 null값이 들어간다
            var cur = _scCompanyFinanceService.getScCompanyFinance(compSn, year);   
            var pre = _scCompanyFinanceService.getScCompanyFinance(compSn, year-1); 

            // 그냥 가져와서 객체에 담기
            var ob = new                        // json으로 전달할 Object
            {
                Current_FpACa = cur.FpACa,      // 유동자산
                Previous_FpACa = pre.FpACa,
                Current_FpAQa = cur.FpAQa,      // 당좌자산
                Previous_FpAQa = pre.FpAQa,
                Current_FpATraderecv = cur.FpATraderecv,    // 매출채권
                Previous_FpATraderecv = pre.FpATraderecv,
                Current_FpAIntent = cur.FpAIntent,          // 재고자산
                Previous_FpAIntent = pre.FpAIntent,
                Current_FpAFixasset = cur.FpAFixasset,      // 비유동자산
                Previous_FpAFixasset = pre.FpAFixasset,
                Current_FpATangible = cur.FpATangible,      // 투자자산
                Previous_FpATangible = pre.FpATangible,
                Current_FpAInvest = cur.FpAInvest,          // 유형자산
                Previous_FpAInvest = pre.FpAInvest,
                Current_FpAIntangible = cur.FpAIntangible,  // 무형자산
                Previous_FpAIntangible = pre.FpAIntangible,
                Current_FpARndcost = cur.FpARndcost,        // 개발비
                Previous_FpARndcost = pre.FpARndcost,
                Current_FpANoncurrentasset = cur.FpANoncurrentasset,    // 기타비유동자산
                Previous_FpANoncurrentasset = pre.FpANoncurrentasset,
                Current_FpASum = cur.FpASum,                // 자산총계
                Previous_FpASum = pre.FpASum,
                Current_FpLCurrent = cur.FpLCurrent,        // 유동부채
                Previous_FpLCurrent = pre.FpLCurrent,
                Current_FpLLongterm = cur.FpLLongterm,      // 비유동부채
                Previous_FpLLongterm = pre.FpLLongterm,
                Current_FpLSum = cur.FpLSum,                // 부채총계
                Previous_FpLSum = pre.FpLSum,
                Current_FpCStock = cur.FpCStock,            // 자본금
                Previous_FpCStock = pre.FpCStock,
                Current_FpCSurplus = cur.FpCSurplus,        // 자본잉여금
                Previous_FpCSurplus = pre.FpCSurplus,
                Current_FpCAdjust = cur.FpCAdjust,          // 자본조정
                Previous_FpCAdjust = pre.FpCAdjust,
                Current_FpCOthercomp = cur.FpCOthercomp,    // 기타포괄손익누계
                Previous_FpCOthercomp = pre.FpCOthercomp,
                Current_FpCRelatedearning = cur.FpCRelatedearning,  // 이익잉여금
                Previous_FpCRelatedearning = pre.FpCRelatedearning,
                Current_FpCSum = cur.FpCSum,                // 자본총계
                Previous_FpCSum = pre.FpCSum,
                Current_CiSales = cur.CiSales,              // 매출액
                Previous_CiSales = pre.CiSales,
                Current_CiCostofsales = cur.CiCostofsales,  // 매출원가
                Previous_CiCostofsales = pre.CiCostofsales,
                Current_CiGrosspoint = cur.CiGrosspoint,    // 매출총이익
                Previous_CiGrosspoint = pre.CiGrosspoint,
                Current_CiAdminexpanses = cur.CiAdminexpanses,  // 판매비와 관리비
                Previous_CiAdminexpanses = pre.CiAdminexpanses,
                Current_CiWages = cur.CiWages,              // 인건비
                Previous_CiWages = pre.CiWages,
                Current_CiWageBorder = cur.CiWageBorder,    // 임원급여
                Previous_CiWageBorder = pre.CiWageBorder,
                Current_CiWageMain = cur.CiWageMain,    // 급여
                Previous_CiWageMain = pre.CiWageMain,
                Current_CiWageBonus = cur.CiWageBonus,    // 상여
                Previous_CiWageBonus = pre.CiWageBonus,
                Current_CiWageAllowances = cur.CiWageAllowances,    // 제수당
                Previous_CiWageAllowances = pre.CiWageAllowances,
                Current_CiWageOther = cur.CiWageOther,      // 잡급
                Previous_CiWageOther = pre.CiWageOther,
                Current_CiWageRetirepay = cur.CiWageRetirepay,  // 퇴직급여
                Previous_CiWageRetirepay = pre.CiWageRetirepay,
                Current_CiRental = cur.CiRental,            // 임차료 
                Previous_CiRental = pre.CiRental,
                Current_CiDepexp = cur.CiDepexp,            // 감가상각비
                Previous_CiDepexp = pre.CiDepexp,
                Current_CiAmoexp = cur.CiAmoexp,            // 무형자산상각비
                Previous_CiAmoexp = pre.CiAmoexp,
                Current_CiTax = cur.CiTax,                  // 세금과공과
                Previous_CiTax = pre.CiTax,
                Current_CiOrdevexp = cur.CiOrdevexp,        // 경상개발비
                Previous_CiOrdevexp = pre.CiOrdevexp,
                Current_CiResearch = cur.CiResearch,        // 연구비
                Previous_CiResearch = pre.CiResearch,
                Current_CiDci = cur.CiDci,                  // 개발비상각액
                Previous_CiDci = pre.CiDci,
                Current_CiOpincome = cur.CiOpincome,        // 영업이익
                Previous_CiOpincome = pre.CiOpincome,
                Current_CiOthergains = cur.CiOthergains,    // 영엽외수익
                Previous_CiOthergains = pre.CiOthergains,
                Current_CiIntincome = cur.CiIntincome,      // 이자수익
                Previous_CiIntincome = pre.CiIntincome,
                Current_CiOtherloses = cur.CiOtherloses,    // 영업외비용
                Previous_CiOtherloses = pre.CiOtherloses,
                Current_CiIntexpanses = cur.CiIntexpanses,  // 이자비용
                Previous_CiIntexpanses = pre.CiIntexpanses,
                Current_CiPlt = cur.CiPlt,                  // 법인세비용차감전손익
                Previous_CiPlt = pre.CiPlt,
                Current_CiInctaxexp = cur.CiInctaxexp,      // 법인세비용
                Previous_CiInctaxexp = pre.CiInctaxexp,
                Current_CiProfit = cur.CiProfit,            // 당기순이익
                Previous_CiProfit = pre.CiProfit,
                Current_McRaw = cur.McRaw,                  // 원재료비
                Previous_McRaw = pre.McRaw,
                Current_McPart = cur.McPart,                // 부재료비
                Previous_McPart = pre.McPart,
                Current_McWages = cur.McWages,              // 노무비
                Previous_McWages = pre.McWages,
                Current_McOverhead = cur.McOverhead,        // 경비
                Previous_McOverhead = pre.McOverhead,
                Current_McRent = cur.McRent,                // 지급임차료
                Previous_McRent = pre.McRent,
                Current_McTax = cur.McTax,                  // 세금과공과
                Previous_McTax = pre.McTax,
                Current_McRndexp = cur.McRndexp,            // 경상개발비
                Previous_McRndexp = pre.McRndexp,
                Current_McDepexp = cur.McDepexp,            // 감가상각비
                Previous_McDepexp = pre.McDepexp,
                Current_McYeartotal = cur.McYeartotal,      // 당기총제조비용
                Previous_McYeartotal = pre.McYeartotal,
                Current_McBegin = cur.McBegin,              // 기초재공품재고액
                Previous_McBegin = pre.McBegin,
                Current_McFromother = cur.McFromother,      // 타계정에서대체액
                Previous_McFromother = pre.McFromother,
                Current_McTotal = cur.McTotal,              // 합계
                Previous_McTotal = pre.McTotal,
                Current_McEnd = cur.McEnd,                  // 기말재공품재고액
                Previous_McEnd = pre.McEnd,
                Current_McToother = cur.McToother,          // 타계정으로대체액
                Previous_McToother = pre.McToother,
                Current_McFinishgoodscost = cur.McFinishgoodscost,  // 당기제품제조원가
                Previous_McFinishgoodscost = pre.McFinishgoodscost
            };
            return Json(ob, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> FinanceCheck02(FinanceCheckViewModel fcvm)
        {
            ViewBag.Left = Global.Report;

            int QuestionSn = fcvm.QuestionSn;
            var vcCompInfo = await _vcCompInfoService.getVcCompInfoById(Session[Global.LoginID].ToString());
            int compSn = vcCompInfo.CompSn;

            int basicYear = fcvm.BasicYear;         // 2016

            if (QuestionSn > 0)
            {
                if (fcvm.SubmitType == "N")
                {
                    fcvm.SaveStatus = 0;
                }

                // 당기
                var modCur = await _scCompanyFinanceService.getScCompanyFinanceAsync(compSn, basicYear);
                var cur = Mapper.Map<ScCompanyFinance>(fcvm.Current);
                cur.CiWages = cur.CiWageBorder + cur.CiWageMain + cur.CiWageBonus + cur.CiWageAllowances + cur.CiWageOther + cur.CiWageRetirepay;

                if (modCur == null) // insert
                {
                    cur.CompSn = compSn;
                    cur.FnYear = basicYear;
                    await _scCompanyFinanceService.AddScCompanyFinanceAsync(cur);
                }
                else // update
                {
                    modCur.FpACa = cur.FpACa;
                    modCur.FpAQa = cur.FpAQa;
                    modCur.FpATraderecv = cur.FpATraderecv;
                    modCur.FpAIntent = cur.FpAIntent;
                    modCur.FpAFixasset = cur.FpAFixasset;
                    modCur.FpATangible = cur.FpATangible;
                    modCur.FpAInvest = cur.FpAInvest;
                    modCur.FpAIntangible = cur.FpAIntangible;
                    modCur.FpARndcost = cur.FpARndcost;
                    modCur.FpANoncurrentasset = cur.FpANoncurrentasset;
                    modCur.FpASum = cur.FpASum;
                    modCur.FpLCurrent = cur.FpLCurrent;
                    modCur.FpLLongterm = cur.FpLLongterm;
                    modCur.FpLSum = cur.FpLSum;
                    modCur.FpCStock = cur.FpCStock;
                    modCur.FpCSurplus = cur.FpCSurplus;
                    modCur.FpCAdjust = cur.FpCAdjust;
                    modCur.FpCOthercomp = cur.FpCOthercomp;
                    modCur.FpCRelatedearning = cur.FpCRelatedearning;
                    modCur.FpCSum = cur.FpCSum;

                    modCur.CiSales = cur.CiSales;
                    modCur.CiCostofsales = cur.CiCostofsales;
                    modCur.CiGrosspoint = cur.CiGrosspoint;
                    modCur.CiAdminexpanses = cur.CiAdminexpanses;
                    modCur.CiWages = cur.CiWageBorder + cur.CiWageMain + cur.CiWageBonus + cur.CiWageAllowances + cur.CiWageOther + cur.CiWageRetirepay;                   // 수정되어야 할 부분
                    modCur.CiWageBorder = cur.CiWageBorder;
                    modCur.CiWageMain = cur.CiWageMain;
                    modCur.CiWageBonus = cur.CiWageBonus;
                    modCur.CiWageAllowances = cur.CiWageAllowances;
                    modCur.CiWageOther = cur.CiWageOther;
                    modCur.CiWageRetirepay = cur.CiWageRetirepay;   // end
                    modCur.CiRental = cur.CiRental;
                    modCur.CiDepexp = cur.CiDepexp;
                    modCur.CiAmoexp = cur.CiAmoexp;
                    modCur.CiTax = cur.CiTax;
                    modCur.CiOrdevexp = cur.CiOrdevexp;
                    modCur.CiResearch = cur.CiResearch;
                    modCur.CiDci = cur.CiDci;
                    modCur.CiOpincome = cur.CiOpincome;
                    modCur.CiOthergains = cur.CiOthergains;
                    modCur.CiIntincome = cur.CiIntincome;
                    modCur.CiOtherloses = cur.CiOtherloses;
                    modCur.CiIntexpanses = cur.CiIntexpanses;
                    modCur.CiPlt = cur.CiPlt;
                    modCur.CiInctaxexp = cur.CiInctaxexp;
                    modCur.CiProfit = cur.CiProfit;

                    modCur.McRaw = cur.McRaw;
                    modCur.McPart = cur.McPart;
                    modCur.McWages = cur.McWages;
                    modCur.McOverhead = cur.McOverhead;
                    modCur.McRent = cur.McRent;
                    modCur.McTax = cur.McTax;
                    modCur.McRndexp = cur.McRndexp;
                    modCur.McDepexp = cur.McDepexp;
                    modCur.McYeartotal = cur.McYeartotal;
                    modCur.McBegin = cur.McBegin;
                    modCur.McFromother = cur.McFromother;
                    modCur.McTotal = cur.McTotal;
                    modCur.McEnd = cur.McEnd;
                    modCur.McToother = cur.McToother;
                    modCur.McFinishgoodscost = cur.McFinishgoodscost;

                    _scCompanyFinanceService.modifyScCompanyFinanceAsync(modCur); // update complete
                }

                // 전기
                var modPre = await _scCompanyFinanceService.getScCompanyFinanceAsync(compSn, basicYear - 1);
                var pre = Mapper.Map<ScCompanyFinance>(fcvm.Previous);
                pre.CiWages = pre.CiWageBorder + pre.CiWageMain + pre.CiWageBonus + pre.CiWageAllowances + pre.CiWageOther + pre.CiWageRetirepay;
                if (modPre == null) // insert
                {
                    pre.CompSn = compSn;
                    pre.FnYear = basicYear - 1;
                    await _scCompanyFinanceService.AddScCompanyFinanceAsync(pre);
                }
                else // update, update시에는 compsn과 year를 수정할 필요 없음
                {
                    modPre.FpACa = pre.FpACa;
                    modPre.FpAQa = pre.FpAQa;
                    modPre.FpATraderecv = pre.FpATraderecv;
                    modPre.FpAIntent = pre.FpAIntent;
                    modPre.FpAFixasset = pre.FpAFixasset;
                    modPre.FpATangible = pre.FpATangible;
                    modPre.FpAInvest = pre.FpAInvest;
                    modPre.FpAIntangible = pre.FpAIntangible;
                    modPre.FpARndcost = pre.FpARndcost;
                    modPre.FpANoncurrentasset = pre.FpANoncurrentasset;
                    modPre.FpASum = pre.FpASum;
                    modPre.FpLCurrent = pre.FpLCurrent;
                    modPre.FpLLongterm = pre.FpLLongterm;
                    modPre.FpLSum = pre.FpLSum;
                    modPre.FpCStock = pre.FpCStock;
                    modPre.FpCSurplus = pre.FpCSurplus;
                    modPre.FpCAdjust = pre.FpCAdjust;
                    modPre.FpCOthercomp = pre.FpCOthercomp;
                    modPre.FpCRelatedearning = pre.FpCRelatedearning;
                    modPre.FpCSum = pre.FpCSum;

                    modPre.CiSales = pre.CiSales;
                    modPre.CiCostofsales = pre.CiCostofsales;
                    modPre.CiGrosspoint = pre.CiGrosspoint;
                    modPre.CiAdminexpanses = pre.CiAdminexpanses;
                    modPre.CiWages = pre.CiWageBorder + pre.CiWageMain + pre.CiWageBonus + pre.CiWageAllowances + pre.CiWageOther + pre.CiWageRetirepay;                   // 수정되어야 할 부분
                    modPre.CiWageBorder = pre.CiWageBorder;
                    modPre.CiWageMain = pre.CiWageMain;
                    modPre.CiWageBonus = pre.CiWageBonus;
                    modPre.CiWageAllowances = pre.CiWageAllowances;
                    modPre.CiWageOther = pre.CiWageOther;
                    modPre.CiWageRetirepay = pre.CiWageRetirepay;   // end
                    modPre.CiRental = pre.CiRental;
                    modPre.CiDepexp = pre.CiDepexp;
                    modPre.CiAmoexp = pre.CiAmoexp;
                    modPre.CiTax = pre.CiTax;
                    modPre.CiOrdevexp = pre.CiOrdevexp;
                    modPre.CiResearch = pre.CiResearch;
                    modPre.CiDci = pre.CiDci;
                    modPre.CiOpincome = pre.CiOpincome;
                    modPre.CiOthergains = pre.CiOthergains;
                    modPre.CiIntincome = pre.CiIntincome;
                    modPre.CiOtherloses = pre.CiOtherloses;
                    modPre.CiIntexpanses = pre.CiIntexpanses;
                    modPre.CiPlt = pre.CiPlt;
                    modPre.CiInctaxexp = pre.CiInctaxexp;
                    modPre.CiProfit = pre.CiProfit;

                    modPre.McRaw = pre.McRaw;
                    modPre.McPart = pre.McPart;
                    modPre.McWages = pre.McWages;
                    modPre.McOverhead = pre.McOverhead;
                    modPre.McRent = pre.McRent;
                    modPre.McTax = pre.McTax;
                    modPre.McRndexp = pre.McRndexp;
                    modPre.McDepexp = pre.McDepexp;
                    modPre.McYeartotal = pre.McYeartotal;
                    modPre.McBegin = pre.McBegin;
                    modPre.McFromother = pre.McFromother;
                    modPre.McTotal = pre.McTotal;
                    modPre.McEnd = pre.McEnd;
                    modPre.McToother = pre.McToother;
                    modPre.McFinishgoodscost = pre.McFinishgoodscost;

                    _scCompanyFinanceService.modifyScCompanyFinanceAsync(modPre); // update complete
                }

                var modFin = await _scFinancialIndexTService.getScFinancialIndexTCheckAsync(compSn, basicYear.ToString());   // ScFinancialIndexT 확인
                if (modFin == null) // insert
                {
                    ScFinancialIndexT sit = new ScFinancialIndexT(); // 새 객체 생성
                    sit.CompSn = compSn;
                    sit.Year = basicYear + "";

                    // 연구개발 투자비 = 재무상태표.개발비(당기) - 재무상태표.개발비(전기) + 손익계산서.경상개발비(당기) + 연구비(당기) + 제조원가명세서.경상개발비(당기) ************************** 개발비상각액 들어가야한다
                    sit.ReserchAmt = cur.FpARndcost - pre.FpARndcost + cur.CiOrdevexp + cur.CiResearch + cur.McRndexp;   // 연구개발투자비
                    sit.CurrentSale = cur.CiSales;       // 당기매출액
                    sit.PrevSale = pre.CiSales;          // 전기매출액
                    sit.CurrentEarning = cur.CiProfit;   // 당기순이익
                    sit.PrevEarning = pre.CiProfit;      // 전기순이익
                    sit.OperatingEarning = cur.CiOpincome; // 영업이익
                    sit.TotalCapital = cur.FpCSum;         // 자본총계
                    sit.CurrentAsset = cur.FpACa;          // 유동자산
                    sit.InventoryAsset = cur.FpAIntent;    // 재고자산
                    sit.CurrentLiability = cur.FpLCurrent; // 유동부채
                    sit.TotalLiability = cur.FpLSum;       // 부채총계
                    sit.TotalAsset = cur.FpASum;           // 자산총계
                    sit.NonOperEar = cur.CiOthergains;     // 영업외손익
                    sit.InterstCost = cur.CiIntexpanses;   // 이자비용
                    sit.SalesCredit = cur.FpATraderecv;    // 매출채권

                    // 이전산식
                    // 부가가치 = 법인세비용차감전이익 + 손익계산서.인건비 + 제조원가명세서.인건비 + (손익계산서.이자수익 - 손익계산서.이자비용) + (손익계산서.임차료 + 손익계산서.세금과공과 + 손익계산서.감가상각)
                    // + (제조원가명세서.임차료 + 제조원가명세서.세금과공과 + 제조원가명세서.감가상각)

                    // 바뀐산식 02.25
                    // 부가가치 = 법인세비용차감전이익 + 손익계산서.인건비 + 임차료 + 세금과공과 + 감가상각비 + 이자수익 - 이자비 
                    // + 제조원가명세서.노무비 + 지급임차료 + 세금과공과 + 감가상각비

                    // 바뀐산식 04.21
                    // 부가가치 = 법인세비용차감전이익 + 손익계산서.인건비 + 임차료 + 세금과공과 + 감가상각비 + 이자수익 - 이자비 
                    // + 제조원가명세서.노무비 + 지급임차료 + 세금과공과 + 감가상각비 + 무형자산상각비

                    sit.ValueAdded = cur.CiPlt + cur.CiWages + cur.CiRental + cur.CiTax + cur.CiDepexp + cur.CiIntincome - cur.CiIntexpanses
                                   + cur.McWages + cur.McRent + cur.McTax + cur.McDepexp + cur.CiAmoexp; // 무형자산 상각비 추가 0421

                    // 무형자산상각비를 더해준다 04.20 추가 산식
                    // cur.CiAmoexp


                    sit.MaterialCost = cur.McRaw + cur.McPart;    // 재료비 

                    sit.InsertDts = DateTime.Now;         // 데이터 넣은 시간 -> DateTime.Now 
                    sit.InsertId = Session[Global.LoginID].ToString(); // 데이터 넣은 아이디
                    sit.ModifyDts = DateTime.Now;         // 데이터 수정한 시간
                    sit.ModifyId = Session[Global.LoginID].ToString(); // 데이터 수정한 아이디

                    //var bizCheck04 = new BizCheck04ViewModel();

                    //// A1A202 : 조직만족도
                    //var quesResult2s = await _quesResult2Service.GetQuesResult2sAsync(QuestionSn, "A1A202");

                    //if (quesResult2s.Count() != 3)
                    //{
                    //    var quesCheckList = await _quesCheckListService.GetQuesCheckListAsync("A1A202");
                    //    var quesYearListView = Mapper.Map<List<QuesYearListViewModel>>(quesCheckList);

                    //    foreach (var item in quesYearListView)
                    //    {
                    //        item.QuestionSn = QuestionSn;
                    //        item.BasicYear = DateTime.Now.Year;
                    //        item.D = "0";
                    //        item.D451 = "0";
                    //        item.D452 = "0";
                    //        item.D453 = "0";
                    //    }
                    //    //총직원
                    //    bizCheck04.TotalEmp = quesYearListView.SingleOrDefault(i => i.DetailCd == "A1A20201");
                    //    //이직직원
                    //    bizCheck04.MoveEmp = quesYearListView.SingleOrDefault(i => i.DetailCd == "A1A20202");
                    //    //신규직원
                    //    bizCheck04.NewEmp = quesYearListView.SingleOrDefault(i => i.DetailCd == "A1A20203");
                    //}
                    //else
                    //{
                    //    var quesYearListView = Mapper.Map<List<QuesYearListViewModel>>(quesResult2s);
                    //    //총직원
                    //    bizCheck04.TotalEmp = quesYearListView.SingleOrDefault(i => i.DetailCd == "A1A20201");
                    //    //이직직원
                    //    bizCheck04.MoveEmp = quesYearListView.SingleOrDefault(i => i.DetailCd == "A1A20202");
                    //    //신규직원
                    //    bizCheck04.NewEmp = quesYearListView.SingleOrDefault(i => i.DetailCd == "A1A20203");
                    //}
                    //modFin.QtEmp = decimal.Parse(bizCheck04.TotalEmp.D452);
                    var bizCheck13 = new BizCheck13ViewModel();

                    var quesResult2sTotalEmp = await _quesResult2Service.GetQuesResult2sAsync(QuestionSn, "A1E103");
                    if (quesResult2sTotalEmp.Count() != 1)
                    {
                        var quesCheckList = await _quesCheckListService.GetQuesCheckListAsync("A1E103");
                        var quesYearListView = Mapper.Map<List<QuesYearListViewModel>>(quesCheckList);

                        foreach (var item in quesYearListView)
                        {
                            item.QuestionSn = QuestionSn;
                            item.BasicYear = DateTime.Now.Year;
                            item.D = "0";
                            item.D451 = "0";
                            item.D452 = "0";
                            item.D453 = "0";
                        }
                        bizCheck13.TotalEmp = quesYearListView.SingleOrDefault(i => i.DetailCd == "A1E10301");
                    }
                    else
                    {
                        var quesYearListView = Mapper.Map<List<QuesYearListViewModel>>(quesResult2sTotalEmp);
                        bizCheck13.TotalEmp = quesYearListView.SingleOrDefault(i => i.DetailCd == "A1E10301");
                    }
                    sit.QtEmp = decimal.Parse(bizCheck13.TotalEmp.D452);    // 현 년도의 이전 인원수를 가져와야 맞다
                    await _scFinancialIndexTService.AddScFinancialIndexTAsync(sit);
                }
                else // 이미 저장되어있는 데이터가 있다면 update
                {
                    // 재무상태표.개발비(당기) - 재무상태표.개발비(전기) + 손익계산서.경상개발비(당기) + 연구비(당기) + 제조원가명세서.경상개발비(당기) ************************** 개발비상각액 들어가야한다
                    modFin.ReserchAmt = cur.FpARndcost - pre.FpARndcost + cur.CiOrdevexp + cur.CiResearch + cur.McRndexp;   // 연구개발투자비
                    modFin.CurrentSale = cur.CiSales;       // 당기매출액
                    modFin.PrevSale = pre.CiSales;          // 전기매출액
                    modFin.CurrentEarning = cur.CiProfit;   // 당기순이익
                    modFin.PrevEarning = pre.CiProfit;      // 전기순이익
                    modFin.OperatingEarning = cur.CiOpincome; // 영업이익
                    modFin.TotalCapital = cur.FpCSum;         // 자본총계
                    modFin.CurrentAsset = cur.FpACa;          // 유동자산
                    modFin.InventoryAsset = cur.FpAIntent;    // 재고자산
                    modFin.CurrentLiability = cur.FpLCurrent; // 유동부채
                    modFin.TotalLiability = cur.FpLSum;       // 부채총계
                    modFin.TotalAsset = cur.FpASum;           // 자산총계
                    modFin.NonOperEar = cur.CiOthergains;     // 영업외손익
                    modFin.InterstCost = cur.CiIntexpanses;   // 이자비용
                    modFin.SalesCredit = cur.FpATraderecv;    // 매출채권


                    // 이전산식
                    // 부가가치 = 법인세비용차감전이익 + 손익계산서.인건비 + 제조원가명세서.인건비 + (손익계산서.이자수익 - 손익계산서.이자비용) + (손익계산서.임차료 + 손익계산서.세금과공과 + 손익계산서.감가상각)
                    // + (제조원가명세서.임차료 + 제조원가명세서.세금과공과 + 제조원가명세서.감가상각)
                    // 법인세비용차감전손익(cur.CiPlt) + 인건비(cur.CiWages) + 이자수익(cur.CiIntincome) - 이자비용(cur.CiIntexpanses) + 임차료(cur.CiRental) + 세금과공과(cur.CiTax) + 감가상각비(cur.CiDepexp)
                    // modFin.ValueAdded = cur.CiPlt + cur.CiWages + (cur.CiIntincome - cur.CiIntexpanses) + cur.CiRental + cur.CiTax + cur.CiDepexp;    // 부가가치

                    // 바뀐산식 02.25
                    // 부가가치 = 법인세비용차감전이익 + 손익계산서.인건비 + 임차료 + 세금과공과 + 감가상각비 + 이자수익 - 이자비 
                    // + 제조원가명세서.노무비 + 지급임차료 + 세금과공과 + 감가상각비

                    // 바뀐산식 04.21
                    // 부가가치 = 법인세비용차감전이익 + 손익계산서.인건비 + 임차료 + 세금과공과 + 감가상각비 + 이자수익 - 이자비 
                    // + 제조원가명세서.노무비 + 지급임차료 + 세금과공과 + 감가상각비 + 무형자산상각비
                    modFin.ValueAdded = cur.CiPlt + cur.CiWages + cur.CiRental + cur.CiTax + cur.CiDepexp + cur.CiIntincome - cur.CiIntexpanses
                                      + cur.McWages + cur.McRent + cur.McTax + cur.McDepexp + cur.CiAmoexp; // 무형자산 상각비 추가 0421
                    
                    // 무형자산상각비를 더해준다 04.20 추가 산식
                    // cur.CiAmoexp

                    modFin.MaterialCost = cur.McRaw + cur.McPart;    // 재료비 

                    modFin.ModifyDts = DateTime.Now;         // 데이터 수정한 시간
                    modFin.ModifyId = Session[Global.LoginID].ToString(); // 데이터 수정한 아이디

                    //var bizCheck13 = new BizCheck13ViewModel();
                    var bizCheck04 = new BizCheck04ViewModel();

                    // A1A202 : 조직만족도
                    var quesResult2s = await _quesResult2Service.GetQuesResult2sAsync(QuestionSn, "A1A202");

                    if (quesResult2s.Count() != 3)
                    {
                        var quesCheckList = await _quesCheckListService.GetQuesCheckListAsync("A1A202");
                        var quesYearListView = Mapper.Map<List<QuesYearListViewModel>>(quesCheckList);

                        foreach (var item in quesYearListView)
                        {
                            item.QuestionSn = QuestionSn;
                            item.BasicYear = DateTime.Now.Year;
                            item.D = "0";
                            item.D451 = "0";
                            item.D452 = "0";
                            item.D453 = "0";
                        }
                        //총직원
                        bizCheck04.TotalEmp = quesYearListView.SingleOrDefault(i => i.DetailCd == "A1A20201");
                        //이직직원
                        bizCheck04.MoveEmp = quesYearListView.SingleOrDefault(i => i.DetailCd == "A1A20202");
                        //신규직원
                        bizCheck04.NewEmp = quesYearListView.SingleOrDefault(i => i.DetailCd == "A1A20203");
                    }
                    else
                    {
                        var quesYearListView = Mapper.Map<List<QuesYearListViewModel>>(quesResult2s);
                        //총직원
                        bizCheck04.TotalEmp = quesYearListView.SingleOrDefault(i => i.DetailCd == "A1A20201");
                        //이직직원
                        bizCheck04.MoveEmp = quesYearListView.SingleOrDefault(i => i.DetailCd == "A1A20202");
                        //신규직원
                        bizCheck04.NewEmp = quesYearListView.SingleOrDefault(i => i.DetailCd == "A1A20203");
                    }
                    modFin.QtEmp = decimal.Parse(bizCheck04.TotalEmp.D452);

                    //// 조직만족도의 인원수를 qtemp
                    //var quesResult2sTotalEmp = await _quesResult2Service.GetQuesResult2sAsync(QuestionSn, "A1E103");
                    //if (quesResult2sTotalEmp.Count() != 1)
                    //{
                    //    var quesCheckList = await _quesCheckListService.GetQuesCheckListAsync("A1E103");
                    //    var quesYearListView = Mapper.Map<List<QuesYearListViewModel>>(quesCheckList);

                    //    foreach (var item in quesYearListView)
                    //    {
                    //        item.QuestionSn = QuestionSn;
                    //        item.BasicYear = DateTime.Now.Year;
                    //        item.D = "0";
                    //        item.D451 = "0";
                    //        item.D452 = "0";
                    //        item.D453 = "0";
                    //    }
                    //    bizCheck13.TotalEmp = quesYearListView.SingleOrDefault(i => i.DetailCd == "A1E10301");
                    //}
                    //else
                    //{
                    //    var quesYearListView = Mapper.Map<List<QuesYearListViewModel>>(quesResult2sTotalEmp);
                    //    bizCheck13.TotalEmp = quesYearListView.SingleOrDefault(i => i.DetailCd == "A1E10301");
                    //}
                    //modFin.QtEmp = decimal.Parse(bizCheck13.TotalEmp.D452);   // 종업원 수 구하기 성공
                    await _scFinancialIndexTService.SaveDbContextAsync();
                }
            }
            else
            {
                return View(fcvm);
            }
            //return RedirectToAction("OrgCheck01", "Report", new { area = "Company", QuestionSn = QuestionSn, Status = fcvm.Status });

            
            if (fcvm.SubmitType == "T")
            {
                return RedirectToAction("FinanceCheck02", "Report", new { area = "Company", @QuestionSn = fcvm.QuestionSn, @status = fcvm.Status });
            }
            else
            {
                return RedirectToAction("OrgCheck01", "Report", new { area = "Company", QuestionSn = fcvm.QuestionSn, Status = fcvm.Status });
            }
        }


        public ActionResult FinanceMng()
        {
            ViewBag.naviLeftMenu = Global.Report;
            return View();
        }

        public ActionResult BasicSurveyReport() // 5월 22일 여기부터 시작
        {
            ViewBag.naviLeftMenu = Global.Report;
            
            ViewBag.SelectBizWorkYearList = ReportHelper.MakeYear(2015);
            ViewBag.SelectBizWorkList = ReportHelper.MakeBizWorkList(null);

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> BasicSurveyReport(BasicSurveyReportViewModel paramModel)
        {
            ViewBag.naviLeftMenu = Global.Report;

            var vcCompInfo = await _vcCompInfoService.getVcCompInfoById(Session[Global.LoginID].ToString());
            var compSn = vcCompInfo.CompSn;

            var rptMasterObj = await _rptMasterService.getRptMasterByCompSnBasicYear(compSn, paramModel.BizWorkYear);
            //var rptMaster = await _rptMasterService.GetRptMasterAsyncForCompany(paramModel.BizWorkSn, int.Parse(compSn), paramModel.BizWorkYear);

            return RedirectToAction("ReportCover", "BasicSurveyReport", new { area = "", NumSn = rptMasterObj.NumSn, CompSn = rptMasterObj.CompSn, BizWorkYear = rptMasterObj.BasicYear, Status = rptMasterObj.Status, QuestionSn = rptMasterObj.QuestionSn });
            //return RedirectToAction("ReportCover", "BasicSurveyReport", new { area = "", BizWorkSn = rptMaster.BizWorkSn, CompSn = rptMaster.CompSn, BizWorkYear = rptMaster.BasicYear, Status = rptMaster.Status, QuestionSn = rptMaster.QuestionSn });
        }

        //기업 심화보고서 Page
        public async Task<ActionResult> DeepenReportList(string curPage, string numSn, string subNumSn)
        {
            ViewBag.naviLeftMenu = Global.Report;

            int pagingSize = int.Parse(ConfigurationManager.AppSettings["PagingSize"]);
            int pageSize = int.Parse(ConfigurationManager.AppSettings["PagingSize"]);

            var vcBaInfo = await _vcCompInfoService.getVcCompInfoById(Session[Global.LoginID].ToString());
            var compSn = vcBaInfo.CompSn;


            // 추가되어야함
            //var mentoringTotalReport = await _scMentoringTotalReportService.getDeepenReportListByCompSn(compSn);

            var GetLastReport = await _VcLastReportNSatService.getVcCompInfoById(Convert.ToString(compSn)); // 단순히 해당 테이블에서 compSn으로만 걸러내는 부분

            //ViewBag.SelectNumSnList = ReportHelper.MakeNumSnList(GetLastReport);        // 기수 selectList얻기
            //ViewBag.SelectSubNumSnList = ReportHelper.MakeSubNumSnList(GetLastReport);  // 부기수 selectList얻기

            SqlParameter compParam = new SqlParameter("COMP_SN", compSn);               // 테스트용
            object[] parameters = new object[] { compParam };
            var pagedReport = await _procMngService.getDeepenReportListL(parameters);

            if (numSn != null && subNumSn != null) // 둘 다 값이 있다?
            {

            }
            else // 처음엔 둘 다 값이 없다... 그렇다면 그냥 compSn을 기준으로 전부를 가져와야 한다는 뜻
            {
                SqlParameter compParam2 = new SqlParameter("COMP_SN", compSn);
                object[] parameters2 = new object[] { compParam2 };
                var defObj = await _procMngService.getDeepenReportListL(parameters2);
                var defViews = Mapper.Map<List<VcLastReportViewModel>>(defObj);

                return View(defViews);
            }


            //if (numSn != null && subNumSn != null) // 둘 다 해당 값이 없다면
            //{
            //    //SqlParameter compSnParam = new SqlParameter("COMP_SN", compSn);
            //    //SqlParameter numSnParam = new SqlParameter("NUM_SN", numSn);
            //    //SqlParameter subNumSnParam = new SqlParameter("SUB_NUM_SN", subNumSn);
            //    //object[] parameters = new object[] { compSnParam, numSnParam, subNumSnParam };
            //    //var numsReport = _procMngService.getDeepenReportListNums(parameters, int.Parse(curPage ?? "1"), pageSize);
            //}
            //else
            //{
            //    var pagedReport = _procMngService.getDeepenReportList2(parameters2, int.Parse(curPage ?? "1"), pageSize);
            //    //var sb = _procMngService.getDeepenReportList3(parameters);
            //}
            //var filLastReport =
            //int.Parse(curPage ?? "1")이런식으로 null일 경우 무조건 1페이지로 보내주는 것이 필요하다
            //numSn과 subNumSn의 값이 없다면 처리...

            //만약 NumSn과, SubNumSn 둘 다 데이터가 없을 경우에는 일반적인 GetLastReport를 PagedList식으로 보내주는것이 필요하고
            //둘 다 값이 존재한다면 해당 부분을 던져서 값을 가져오는 경우를 지정해주어야 할 것이다
            // 1.단순히 compSn만 던져서 pagedList를 가져오는 부분에 대한 경우를 생각해보자
            // 2.NumSn과 SubNumSn둘 다 같이 던져 pagedList를 가져오는 부분에 대한 경우
            // *.선택된 NumSn과 SubNumSn을 둘 다 계속해서 던져서 같이 가져오는 것에 대한 생각도 되어야함

            //var usrViews = Mapper.Map<List<VcLastReportViewModel>>(GetLastReport);

            //if (usrViews.Count == 0) //심화보고서 테이블 null처리
            //{
            //    return View();
            //}
            //else
            //{
            //    for (int i = 0; i < usrViews.Count; i++)
            //    {
            //        if (usrViews[i].SATISFACTION_GRADE == 0)
            //        {
            //            usrViews[i].CON_STATUS = "P";
            //        }
            //        else
            //        {
            //            usrViews[i].CON_STATUS = "C";
            //        }
            //    }
            //    return View(usrViews);
            //}
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> DeepenReportList(VcLastReportNSatViewModel paramModel)
        {
            ViewBag.naviLeftMenu = Global.Report;
            var vcBaInfo = await _vcCompInfoService.getVcCompInfoById(Session[Global.LoginID].ToString());
            var compSn = vcBaInfo.CompSn;

            var GetLastReport = await _VcLastReportNSatService.getVcCompInfoById(Convert.ToString(compSn));

            var usrViews = Mapper.Map<List<VcLastReportNSatViewModel>>(GetLastReport);

            ViewBag.SatSn = paramModel.SatSn;
            ViewBag.CompSn = paramModel.CompSn;
            ViewBag.SubNumSn = paramModel.SubNumSn;
            return View(usrViews);
        }
        //기업 만족도 조사 Page
        public ActionResult SatisFaction(VcLastReportNSatViewModel paramModel)
        {
            // ?? satisfaction 재진입시 다시 볼 수 있도록 필요??

            ViewBag.naviLeftMenu = Global.Report;

            VcSatCheckViewModel viewModel = new VcSatCheckViewModel();

            viewModel.SatSn = paramModel.SatSn;
            viewModel.CompSn = paramModel.CompSn;
            viewModel.NumSn = paramModel.NumSn; 
            viewModel.SubNumSn = paramModel.SubNumSn;
            viewModel.MentorSn = paramModel.MentorSn;
            viewModel.TotalReportSn = paramModel.TotalReportSn;
            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> SatisFaction(VcSatCheckViewModel vsModel, VcLastReportNSatViewModel paramModel) //만족도 조사 값 입력하는 부분
        {
            ViewBag.naviLeftMenu = Global.Report;

            var obj = vsModel;
            VcSatCheck insertObj = new VcSatCheck();    // vcsatcheck의 값을 채워넣을 빈 객체 생성

            var lastReportObj = await _VcLastReportNSatService.getSatSn(vsModel.CompSn, vsModel.SubNumSn, vsModel.MentorSn, int.Parse(vsModel.TotalReportSn));

            if (lastReportObj == null)  // 객체 오류 처리
                return View();

            var satObj = await _VcSatCheckService.getVcSatCheckBySatSN(vsModel.SatSn);

            if (satObj == null) // 해당 테이블에 값이 없으므로 데이터 Insert
            {
                #region 본 테이블 insert
                insertObj.SatSn = vsModel.SatSn;        // SatSn이 존재이유
                insertObj.Check01 = vsModel.Check01;
                insertObj.Check02 = vsModel.Check02;
                insertObj.Check03 = vsModel.Check03;
                insertObj.Check04 = vsModel.Check04;
                insertObj.Check05 = vsModel.Check05;
                insertObj.Check06 = vsModel.Check06;
                insertObj.Check07 = vsModel.Check07;
                insertObj.Check08 = vsModel.Check08;
                insertObj.Check09 = vsModel.Check09;
                insertObj.Check10 = vsModel.Check10;
                insertObj.Check11 = vsModel.Check11;
                insertObj.Check12 = vsModel.Check12;
                insertObj.Check13 = vsModel.Check13;
                insertObj.Check14 = vsModel.Check14;
                insertObj.Check15 = vsModel.Check15;
                insertObj.Check16 = vsModel.Check16;
                insertObj.Check17 = vsModel.Check17;
                insertObj.Check18 = vsModel.Check18;
                insertObj.Check19 = vsModel.Check19;
                insertObj.Check20 = vsModel.Check20;
                insertObj.Check21 = vsModel.Check21;
                insertObj.Check22 = vsModel.Check22;
                insertObj.Check23 = vsModel.Check23;
                insertObj.Check24 = vsModel.Check24;
                insertObj.Text01 = vsModel.Text01;
                insertObj.Text02 = vsModel.Text02;
                insertObj.RegDt = DateTime.Now;

                _VcSatCheckService.Insert(insertObj);   // add
                _VcSatCheckService.SaveDbContext();     // saveDbContext

                lastReportObj.SatisfactionGrade = calcSatisfaction(insertObj);
                lastReportObj.ConStatus = "C";
                _VcLastReportNSatService.SaveDbContext(); // Save
                #endregion

                #region If 테이블
                TcmsIfSurvey tcmsIfSurvey = new TcmsIfSurvey(); // 인터페이스 테이블 데이터 넣을 객체

                tcmsIfSurvey.InfId = await satiNumGenerator();

                var compObj = await _vcCompInfoService.getVcCompInfoByCompSn(lastReportObj.CompSn);
                tcmsIfSurvey.CompLoginKey = compObj.TcmsLoginKey;

                var baObj = await _vcBaInfoService.getVcBaInfoByBaSn(lastReportObj.BaSn);
                tcmsIfSurvey.BaLoginKey = baObj.TcmsLoginKey;

                var mentorObj = await _vcMentorInfoService.getVcMentorInfoByMentorSn(lastReportObj.MentorSn+"");
                tcmsIfSurvey.MentorLoginKey = mentorObj.TcmsLoginKey;

                tcmsIfSurvey.NumSn = lastReportObj.NumSn;
                tcmsIfSurvey.SubNumSn = lastReportObj.SubNumSn;
                tcmsIfSurvey.ConCode = lastReportObj.ConCode;

                tcmsIfSurvey.SatisfactionScore = calcSatisfaction(insertObj); // 계산필요

                tcmsIfSurvey.Check01 = vsModel.Check01;
                tcmsIfSurvey.Check02 = vsModel.Check02;
                tcmsIfSurvey.Check03 = vsModel.Check03;
                tcmsIfSurvey.Check04 = vsModel.Check04;
                tcmsIfSurvey.Check05 = vsModel.Check05;
                tcmsIfSurvey.Check06 = vsModel.Check06;
                tcmsIfSurvey.Check07 = vsModel.Check07;
                tcmsIfSurvey.Check08 = vsModel.Check08;
                tcmsIfSurvey.Check09 = vsModel.Check09;
                tcmsIfSurvey.Check10 = vsModel.Check10;
                tcmsIfSurvey.Check11 = vsModel.Check11;
                tcmsIfSurvey.Check12 = vsModel.Check12;
                tcmsIfSurvey.Check13 = vsModel.Check13;
                tcmsIfSurvey.Check14 = vsModel.Check14;
                tcmsIfSurvey.Check15 = vsModel.Check15;
                tcmsIfSurvey.Check16 = vsModel.Check16;
                tcmsIfSurvey.Check17 = vsModel.Check17;
                tcmsIfSurvey.Check18 = vsModel.Check18;
                tcmsIfSurvey.Check19 = vsModel.Check19;
                tcmsIfSurvey.Check20 = vsModel.Check20;
                tcmsIfSurvey.Check21 = vsModel.Check21;
                tcmsIfSurvey.Check22 = vsModel.Check22;
                tcmsIfSurvey.Check23 = vsModel.Check23;
                tcmsIfSurvey.Check24 = vsModel.Check24;

                tcmsIfSurvey.Text01 = vsModel.Text01;
                tcmsIfSurvey.Text02 = vsModel.Text02;

                tcmsIfSurvey.InfDt = DateTime.Today;

                _tcmsIfSurveyService.Insert(tcmsIfSurvey);
                _tcmsIfSurveyService.SaveDbContext();
                //... if 테이블 넣기 종료
                #endregion

                var status = sendSatisfaction(tcmsIfSurvey); // 데이터 전송

                if (status == "S")  // 데이터 결과 값
                {
                    var sTcmsIfSurvey = await _tcmsIfSurveyService.getTcmsIfSurveyByInfId(tcmsIfSurvey.InfId);
                    sTcmsIfSurvey.InsertYn = "S";
                    _tcmsIfSurveyService.SaveDbContext();
                }
                else if (status == "E")
                {
                    var eTcmsIfSurvey = await _tcmsIfSurveyService.getTcmsIfSurveyByInfId(tcmsIfSurvey.InfId);
                    eTcmsIfSurvey.InsertYn = "E";
                    _tcmsIfSurveyService.SaveDbContext();
                }
            }
            //... 본테이블 종료
            // 넣고 나서 전송부도 여기에 둔다

            ViewBag.SatSn = lastReportObj.SatSn; 
            ViewBag.CompSn = lastReportObj.CompSn; 
            ViewBag.SubNumSn = lastReportObj.SubNumSn; 
            
            return RedirectToAction("DeepenReportList", "Report");
        }


        public int calcSatisfaction(VcSatCheck vsc)
        {
            int sum = 0;
            sum += int.Parse(vsc.Check01) + int.Parse(vsc.Check02) + int.Parse(vsc.Check03) + int.Parse(vsc.Check04) + int.Parse(vsc.Check05)
                + int.Parse(vsc.Check06) + int.Parse(vsc.Check07) + int.Parse(vsc.Check08) + int.Parse(vsc.Check09) + int.Parse(vsc.Check10)
                + int.Parse(vsc.Check11) + int.Parse(vsc.Check12) + int.Parse(vsc.Check13) + int.Parse(vsc.Check14) + int.Parse(vsc.Check15)
                + int.Parse(vsc.Check16) + int.Parse(vsc.Check17) + int.Parse(vsc.Check18) + int.Parse(vsc.Check19) + int.Parse(vsc.Check20);

            return sum;
        }

        public async Task<string> satiNumGenerator()
        {
            string infId = "";
            string infPrefix = "TCMS_";
            string rst = "";

            var tcmsIfSurveyList = await _tcmsIfSurveyService.getTcmsIfSurvey();

            if (tcmsIfSurveyList.Count == 0)
            {
                infId = "00001";
                rst = infPrefix + infId;
            }
            else
            {
                var maxVal = tcmsIfSurveyList.Max(x => x.InfId); // 최대값 식별
                string[] maxValResult = maxVal.Split('_');
                int itemp = int.Parse(maxValResult[1]);
                string temp = ++itemp +"";

                if (temp.Length == 1)
                {
                    rst = infPrefix + "0000" + temp;
                }
                else if (temp.Length == 2)
                {
                    rst = infPrefix + "000" + temp;
                }
                else if (temp.Length == 3)
                {
                    rst = infPrefix + "00" + temp;
                }
                else if (temp.Length == 4)
                {
                    rst = infPrefix + "0" + temp;
                }
                else
                {
                    rst = infPrefix + temp;
                }
            }

            return rst;
        }

        public string sendSatisfaction(TcmsIfSurvey tcmsIfSurvey)
        {
            HttpContext.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            string result = "";

            StatusModel statusModel = new StatusModel();

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(Global.TCMSGATE + "Api/tcms_if_surveyffff.php");
            httpWebRequest.ContentType = "application/x-www-form-urlencoded";
            httpWebRequest.Method = "POST";
            httpWebRequest.CookieContainer = new CookieContainer();
            HttpCookieCollection cookies = Request.Cookies;
            for (int i = 0; i < cookies.Count; i++)
            {
                HttpCookie httpCookie = cookies.Get(i);
                Cookie cookie = new Cookie();
                cookie.Domain = httpWebRequest.RequestUri.Host;
                cookie.Expires = httpCookie.Expires;
                cookie.Name = httpCookie.Name;
                cookie.Path = httpCookie.Path;
                cookie.Secure = httpCookie.Secure;
                cookie.Value = httpCookie.Value;
                httpWebRequest.CookieContainer.Add(cookie);
            }

            using (var requestStream = httpWebRequest.GetRequestStream())
            {
                string backSlash = "";
                var dt = DateTime.Today;
                string dtc = String.Format("{0:yyyy-MM-dd ss:ss:ss}", dt);
                string jsont = new JavaScriptSerializer().Serialize(new
                {
                    InfId = tcmsIfSurvey.InfId,
                    CompLoginKey = tcmsIfSurvey.CompLoginKey,
                    BaLoginKey = tcmsIfSurvey.BaLoginKey,
                    MentorLoginKey = tcmsIfSurvey.MentorLoginKey,
                    NumSn = tcmsIfSurvey.NumSn,
                    SubNumSn = tcmsIfSurvey.SubNumSn,
                    ConCode = tcmsIfSurvey.ConCode,
                    SatisfactionGrade = tcmsIfSurvey.SatisfactionScore,
                    Check01 = tcmsIfSurvey.Check01,
                    Check02 = tcmsIfSurvey.Check02,
                    Check03 = tcmsIfSurvey.Check03,
                    Check04 = tcmsIfSurvey.Check04,
                    Check05 = tcmsIfSurvey.Check05,
                    Check06 = tcmsIfSurvey.Check06,
                    Check07 = tcmsIfSurvey.Check07,
                    Check08 = tcmsIfSurvey.Check08,
                    Check09 = tcmsIfSurvey.Check09,
                    Check10 = tcmsIfSurvey.Check10,
                    Check11 = tcmsIfSurvey.Check11,
                    Check12 = tcmsIfSurvey.Check12,
                    Check13 = tcmsIfSurvey.Check13,
                    Check14 = tcmsIfSurvey.Check14,
                    Check15 = tcmsIfSurvey.Check15,
                    Check16 = tcmsIfSurvey.Check16,
                    Check17 = tcmsIfSurvey.Check17,
                    Check18 = tcmsIfSurvey.Check18,
                    Check19 = tcmsIfSurvey.Check19,
                    Check20 = tcmsIfSurvey.Check20,
                    Check21 = tcmsIfSurvey.Check21,
                    Check22 = tcmsIfSurvey.Check22,
                    Check23 = tcmsIfSurvey.Check23,
                    Check24 = tcmsIfSurvey.Check24,
                    Text01 = tcmsIfSurvey.Text01,
                    Text02 = tcmsIfSurvey.Text02,
                    InfDt = dtc
                });
                //backSlash = jsont.Replace("\\", "");
                byte[] ba = Encoding.UTF8.GetBytes("json=" + jsont);

                requestStream.Write(ba, 0, ba.Length);
                requestStream.Flush();
                requestStream.Close();
            }
            try
            {
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    JavaScriptSerializer js = new JavaScriptSerializer();
                    result = streamReader.ReadToEnd();
                    string[] rptSplit = result.Split('\n');
                    statusModel = (StatusModel)js.Deserialize(rptSplit[1], typeof(StatusModel));
                }
                return statusModel.status;
            }
            catch (Exception e)
            {
                return "E";
            }
            
        }

        public async Task<ActionResult> DeepenReportDetail(int totalReportSn)
        {
            ViewBag.naviLeftMenu = Global.Report;

            var scMentoringTotalReport = await _scMentoringTotalReportService.GetMentoringTotalReportById(totalReportSn);

            var listscFileInfo = scMentoringTotalReport.ScMentoringTrFileInfoes.Select(mtfi => mtfi.ScFileInfo).Where(fi => fi.Status == "N");

            var listFileContent =
               Mapper.Map<List<FileContent>>(listscFileInfo);

            var totalReportViewModel =
               Mapper.Map<MentoringTotalReportViewModel>(scMentoringTotalReport);

            // compSn을 이용해서 compNm가져오기
            var compInfo = await _vcCompInfoService.getVcCompInfoByCompSn(totalReportViewModel.CompSn);

            totalReportViewModel.CompNm = compInfo.CompNm;
            totalReportViewModel.FileContents = listFileContent;

            //검색조건 유지를 위해
            //ViewBag.SelectParam = selectParam;

            return View(totalReportViewModel);
        }


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


        public async Task DownloadTRReportFileMulti(string totalReportSn)
        {

            string archiveName = "download.zip";

            //Eager Loading 방식
            var listscMentoringTrFileInfo = await _scMentoringTrFileInfoService.GetMentoringTrFileInfo(int.Parse(totalReportSn));

            var listScFileInfo = new List<ScFileInfo>();
            foreach (var scMentoringTrFileInfo in listscMentoringTrFileInfo)
            {
                listScFileInfo.Add(scMentoringTrFileInfo.ScFileInfo);
            }

            var files = Mapper.Map<IList<FileContent>>(listScFileInfo);

            new FileHelper().DownloadFile(files, archiveName);

        }

        public async Task DownloadReportFileMulti(string reportSn)
        {

            string archiveName = "download.zip";

            //Eager Loading 방식
            var listscMentoringFileInfo = await _scMentoringFileInfoService.GetMentoringFileInfo(int.Parse(reportSn));

            var listScFileInfo = new List<ScFileInfo>();
            foreach (var scMentoringFileInfo in listscMentoringFileInfo)
            {
                listScFileInfo.Add(scMentoringFileInfo.ScFileInfo);
            }

            var files = Mapper.Map<IList<FileContent>>(listScFileInfo);

            new FileHelper().DownloadFile(files, archiveName);

        }

        [HttpPost]
        public async Task<JsonResult> RptExist(string year)
        {

            string status = "C";
            string data = "{\"status\":\"" + status + "\"}";

            // 저장되어있는 loginId, 즉 tcmsLoginKey를 기준으로
            // vc_comp_info에서 registrationNo 조회
            var vcCompInfo = await _vcCompInfoService.getVcCompInfoById(Session[Global.LoginID].ToString());

            // 조회된 registrationNo와 basicYear를 기준으로 quesMaster 조회
            // quesMaster에서 questioSn 획득
            var quesMaster = await _quesMasterService.GetQuesMasterAsync(vcCompInfo.RegistrationNo, int.Parse(year));

            if (quesMaster != null)
            {
                var rptMaster = await _rptMasterService.GetRptMasterAsync(quesMaster.QuestionSn, vcCompInfo.CompSn, int.Parse(year));

                // 보고서 작성중
                if(rptMaster.Status == "P")
                {
                    status = "P";
                    data = "{\"status\":\"" + status + "\"}";
                }
                // 보고서 미작성 
                else if(rptMaster.Status == "T")
                {
                    status = "P";
                }
            }
            else
            {
                // 해당 년도의 보고서가 존재하지 않을때 막음
                status = "N";
            }

            // 획득한 questionSn을 기준으로
            // rptmaster 조회
            // 관련된 객체가 null이 아니면
            // status Y를
            // 그게 아니면 status N을
            // 리턴하고 메소드 종료
            return Json(status);
        }

        public async Task<string> makeNumSn(int basicYear) // quesMaster의 basicYear를 통해
        {
            // 인자로 들어온 basicYear와
            // 가장 최근 numObject를 비교하여
            // 해당 basicYear의 numSn을 구해준다
            int maxYear, yearGap;
            int maxNum, retNum;
            string retString;

            DateTime dtObj;

            var numObj = await _scUsrService.getNumInfoOneAsync();

            dtObj = (DateTime)numObj.BizStDt;
            maxYear = dtObj.Year;

            yearGap = maxYear - basicYear; // 년도 차이구하기
            // 2017 - 2016 = 1
            // 2016 - 2017 = -1
            // 2016 - 2016 = 0

            maxNum = int.Parse(numObj.NumSn);
            // 2
            // 1
            // 1

            retNum = maxNum + (-1 * yearGap);
            // 2 - 1
            // 1 + (-1 * -1);
            // = 1 + (-1 * 0);

            retString = retNum.ToString();

            if (retString.Length == 1)
            {
                retString = "00" + retString;
            }
            else if (retString.Length == 2)
            {
                retString = "0" + retString;
            }

            return retString;
        }
    }
}