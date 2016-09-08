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
using System.Web.UI;
using System.Web.Script.Serialization;
using System.Net;
using BizOneShot.Light.Models.CustomModels;
using System.Text;
using System.Threading;

namespace BizOneShot.Light.Web.Areas.Mentor.Controllers
{
    [UserAuthorize(Order = 1)]
    [MenuAuthorize(Roles = UserType.Mentor, Order = 2)]
    public class ReportController : BaseController
    {
        private readonly IScCompMappingService scCompMappingService;
        private readonly IScMentorMappingService scMentorMappingService;
        private readonly IRptMasterService rptMasterService;

        private readonly IQuesMasterService quesMasterService;
        private readonly IScBizWorkService scBizWorkService;
        private readonly IVcCompInfoService vcCompInfoService;

        private readonly IProcMngService procMngService;
        private readonly IScMentoringTotalReportService _scMentoringTotalReportService;
        private readonly IVcMentorMappingService vcMentorMappingService;
        private readonly IVcLastReportNSatService vcLastReportNSatService;
        private readonly IScUsrService vcUsrService;


        // LastReport 연계를 위한 서비스
        private readonly ITcmsIfLastReportService tcmsIfLastReportService;
        private readonly IVcBaInfoService vcBaInfoService;
        private readonly IVcMentorInfoSerivce vcMentorInfoService;

        public ReportController(
            IScCompMappingService scCompMappingService,
            IScMentorMappingService scMentorMappingService,
            IRptMasterService rptMasterService,
            IQuesMasterService quesMasterService,
            IScBizWorkService scBizWorkService,
            IVcCompInfoService vcCompInfoService,
            IProcMngService procMngService,
            IScMentoringTotalReportService _scMentoringTotalReportService,
            IVcLastReportNSatService vcLastReportNSatService,
            IScUsrService vcUsrService,
            IVcMentorMappingService vcMentorMappingService,

            ITcmsIfLastReportService tcmsIfLastReportService,
            IVcBaInfoService vcBaInfoService,
            IVcMentorInfoSerivce vcMentorInfoService
            )

        {
            this.scCompMappingService = scCompMappingService;
            this.scMentorMappingService = scMentorMappingService;
            this.rptMasterService = rptMasterService;
            this.quesMasterService = quesMasterService;
            this.scBizWorkService = scBizWorkService;
            this.vcCompInfoService = vcCompInfoService;
            this.procMngService = procMngService;
            this._scMentoringTotalReportService = _scMentoringTotalReportService;
            this.vcLastReportNSatService = vcLastReportNSatService;
            this.vcUsrService = vcUsrService;
            this.vcMentorMappingService = vcMentorMappingService;
            this.tcmsIfLastReportService = tcmsIfLastReportService;

            this.vcBaInfoService = vcBaInfoService;
            this.vcMentorInfoService = vcMentorInfoService;

        }

        // GET: Mentor/Report
        public ActionResult Index()
        {
            return View();
        }

        //public async Task<ActionResult> ReportCover(BasicSurveyReportViewModel paramModel)
        //{
        //    ViewBag.naviLeftMenu = Global.Report;

        //    ViewBag.paramModel = paramModel;

        //    if (paramModel.CompSn == 0 || paramModel.NumSn == "0")
        //    {
        //        return View(paramModel);
        //    }

        //    var vcCompInfoObj = await vcCompInfoService.getVcCompInfoByCompSn(paramModel.CompSn);

        //    paramModel.CompNm = vcCompInfoObj.CompNm;
        //    paramModel.BizWorkNm = "";
        //    //var scCompMapping = await scCompMappingService.GetCompMappingAsync(paramModel.NumSn, paramModel.CompSn);

        //    //paramModel.CompNm = scCompMapping.ScCompInfo.CompNm;
        //    //paramModel.BizWorkNm = scCompMapping.ScBizWork.BizWorkNm;
        //    ViewBag.CompNm = paramModel.CompNm;
        //    ViewBag.paramModel = paramModel;
        //    ViewBag.NumSn = paramModel.NumSn;
        //    ViewBag.CompSn = paramModel.CompSn;
        //    ViewBag.BizWorkYear = paramModel.BizWorkYear;
        //    ViewBag.Status = paramModel.Status;
        //    ViewBag.QuestionSn = paramModel.QuestionSn;
        //    //ViewBag.NullCheck = paramModel.NullCheck;

        //    return View(paramModel);

        //}


        public async Task<ActionResult> BasicSurveyReport(string curPage, BasicSurveyReportViewModel paramModel)
        {

            SqlParameter loginId = new SqlParameter("TCMS_LOGIN_KEY", Session[Global.LoginID].ToString());
            object[] parameters = new object[] { loginId };

            var rptList = await procMngService.mentorGetReportList2(parameters);

            ViewBag.naviLeftMenu = Global.Report;

            if (paramModel.BizWorkYear == 0)
                paramModel.BizWorkYear = DateTime.Now.Year;                                 // paramModel에 사업 기준년도 넣기

            var bizWorkYearDropDown = ReportHelper.MakeYear(2015);                          // select box 년도 생성
            SelectList bizWorkYear = new SelectList(bizWorkYearDropDown, "Value", "Text");
            ViewBag.SelectBizWorkYearList = bizWorkYear;                                    // 년도 dropdownlist 생성

            var mentorId = Session[Global.LoginID].ToString();

            if (string.IsNullOrEmpty(paramModel.Status))    // Status 초기화
                paramModel.Status = "";

            ViewBag.SelectStatusList = ReportHelper.MakeReportStatusList();                     // 보고서 상태 Status 필요

            ////기초역량 보고서 조회
            int pagingSize = int.Parse(ConfigurationManager.AppSettings["PagingSize"]);
            int pageSize = int.Parse(ConfigurationManager.AppSettings["PagingSize"]);

            //var rptMsters = await rptMasterService.GetRptMasterList(int.Parse(curPage ?? "1"), pagingSize, mentorId, 2016, 2023, 1, "T");

            //실제 매핑될 model
            var rptMasterListView = Mapper.Map<List<BasicSurveyReportViewModel>>(rptList);
            
            // rptList에서 나온 compSn과 mentorKey를 이용해서 Y count 조회 후 1개 이상은 Y
            foreach(var item in rptMasterListView)
            {

                var checkCompSn = item.CompSn;
                var checkMentorSn = item.MentorSn;

                var checkWriteYn = await vcMentorMappingService.getMentorMappingInfoList(Convert.ToString(checkCompSn), Convert.ToString(checkMentorSn));
                var writeYn = checkWriteYn.Where(bw => bw.WriteYn == "Y");

                if( writeYn != null )
                {
                    item.WriteYn = "Y";
                }
                else
                {
                    item.WriteYn = "N";
                }

            }


            var comListViews =
                Mapper.Map<List<BasicSurveyReportViewModel>>(rptMasterListView);

            return View(new StaticPagedList<BasicSurveyReportViewModel>(rptMasterListView, int.Parse(curPage ?? "1"), pagingSize, rptList.Count));

            //return View();
        }

        //[HttpPost]
        //public async Task<ActionResult> BasicSurveyReport(BasicSurveyReportViewModel paramModel, string curPage)
        //{

        //    ViewBag.LeftMenu = Global.Report;
        //    //사업년도 DownDown List Data
        //    //ViewBag.SelectBizWorkYearList = ReportHelper.MakeYear(2015);

        //    var bizWorkYearDropDown = ReportHelper.MakeYear(2015);
        //    SelectList bizWorkYear = new SelectList(bizWorkYearDropDown, "Value", "Text");
        //    ViewBag.SelectBizWorkYearList = bizWorkYear;

        //    var mentorId = Session[Global.LoginID].ToString();
        //    if (string.IsNullOrEmpty(paramModel.Status))
        //        paramModel.Status = "";

        //    //사업 DropDown List Data 
        //    var listScMentorMapping = await scMentorMappingService.GetMentorMappingListByMentorId(mentorId, paramModel.BizWorkYear);
        //    var listScBizWork = listScMentorMapping.Select(mmp => mmp.ScBizWork).ToList();
        //    ViewBag.SelectBizWorkList = ReportHelper.MakeBizWorkList(listScBizWork);

        //    var listScCompMapping = await scCompMappingService.GetCompMappingListByMentorId(mentorId, "A", paramModel.BizWorkSn, paramModel.BizWorkYear);
        //    var listScCompInfo = listScCompMapping.Select(cmp => cmp.ScCompInfo).ToList();
        //    ViewBag.SelectCompInfoList = ReportHelper.MakeCompanyList(listScCompInfo);
        //    ViewBag.SelectStatusList = ReportHelper.MakeReportStatusList();

        //    //기초역량 보고서 조회
        //    int pagingSize = int.Parse(ConfigurationManager.AppSettings["PagingSize"]);
        //    int pageSize = int.Parse(ConfigurationManager.AppSettings["PagingSize"]);

        //    // listScCompInfo -> RegistrationNo를 추출
        //    var compsRegistrationNo = listScCompInfo.Select(cmp => cmp.RegistrationNo).ToList();

        //    var rptMsters = await rptMasterService.GetRptMasterList(int.Parse(curPage ?? "1"), pagingSize, mentorId, paramModel.BizWorkYear, paramModel.BizWorkSn, paramModel.CompSn, paramModel.Status);

        //    //뷰모델 맵핑
        //    var rptMasterListView = Mapper.Map<List<BasicSurveyReportViewModel>>(rptMsters);

        //    // ---------------------
        //    //var listScCompInfoMapping = await scCompMappingService.GetCompMappingInfoPagedListByMentorId(paramModel.BizWorkSn, mentorId, "A", paramModel.BizWorkYear, int.Parse(curPage), pageSize);

        //    var comListViews =
        //        Mapper.Map<List<BasicSurveyReportViewModel>>(rptMasterListView);

        //    // pagedListScBizWork.subset.CompSn
        //    int allCompanyCount = 0;
        //    int completedCompanyCount = 0;

        //    foreach (BasicSurveyReportViewModel v in comListViews)
        //    {
        //        if (v.Status.Equals("C")) completedCompanyCount++;
        //        allCompanyCount++;
        //    }

        //    ViewBag.TotalCountForMember = allCompanyCount;
        //    ViewBag.CompleteCount = completedCompanyCount;

        //    // ---------------------

        //    return View(new StaticPagedList<BasicSurveyReportViewModel>(rptMasterListView, int.Parse(curPage ?? "1"), pagingSize, rptMsters.TotalItemCount));

        //}

        //#region 드롭다운박스 처리 controller
        //[HttpPost]
        //public async Task<JsonResult> GetBizWorkNm(int Year)
        //{
        //    var compSn = Session[Global.CompSN].ToString();
        //    var mentorId = Session[Global.LoginID].ToString();

        //    // page처리
        //    int pagingSize = int.Parse(ConfigurationManager.AppSettings["PagingSize"]);
        //    int pageSize = int.Parse(ConfigurationManager.AppSettings["PagingSize"]);

        //    //사업 DropDown List Data
        //    var listScMentorMapping = scMentorMappingService.GetMentorMappingListByMentorIdSync(mentorId, Year);

        //    var listScBizWork = listScMentorMapping.Select(mmp => mmp.ScBizWork).ToList();
        //    var bizList = ReportHelper.MakeBizWorkList(listScBizWork);

        //    return Json(bizList);
        //}

        #region 심화보고서 작성
        public async Task<ActionResult> RegDeepenReport(int totalReportSn, int compSn, string finalSubmitYn, string conCode)
        {
            ViewBag.naviLeftMenu = Global.Report;

            var tcmsLoginKey = Session[Global.LoginID].ToString();

            // COMP_SN, MENTOR_LOGIN_KEY, CON_CODE
            SqlParameter mentorLoginKey = new SqlParameter("TCMS_LOGIN_KEY", Session[Global.LoginID].ToString());
            SqlParameter compSN = new SqlParameter("COMP_SN", compSn);
            SqlParameter conCodeInfo = new SqlParameter("CON_CODE", conCode);

            object[] parameters = new object[] { mentorLoginKey, compSN, conCodeInfo };

            var regDeepenInfo = await procMngService.getMentorRegDeepenReport(parameters);

            // mentorId 가져오기
            var mentorInfo = await vcUsrService.selectScUsrByTcms(tcmsLoginKey);
            var mentorId = mentorInfo.LoginId;

            // 기존에 가지고있던 보고서가 있을경우 작성 status 변경
            var checkRegDeepenReport = await _scMentoringTotalReportService.checkRegDeepenReport(compSn, mentorId, regDeepenInfo.NUM_SN, regDeepenInfo.SUB_NUM_SN, regDeepenInfo.CON_CODE);

            var compListInfo = Mapper.Map<MentoringTotalReportViewModel>(regDeepenInfo);

            // 이미 작성했던 심화보고서가 있다
            if (checkRegDeepenReport.Count != 0)
            {

                // compSn, mentorId, numSn, subNumSn, conCode를 이용하여 total_report_sn을 가져온다
                var totalReportSnInfo = checkRegDeepenReport[0].TotalReportSn;

                // 기존에 작성했던 보고서 가져오기
                var scMentoringTotalReport = await _scMentoringTotalReportService.GetMentoringTotalReportById(totalReportSnInfo);

                var listscFileInfo = scMentoringTotalReport.ScMentoringTrFileInfoes.Select(mtfi => mtfi.ScFileInfo).Where(fi => fi.Status == "N");

                var listFileContent =
                   Mapper.Map<List<FileContent>>(listscFileInfo);

                var totalReportViewModel =
                   Mapper.Map<MentoringTotalReportViewModel>(scMentoringTotalReport);

                compListInfo.DeepenContents = totalReportViewModel.DeepenContents;
                compListInfo.SubmitDt = totalReportViewModel.SubmitDt.Value;
                compListInfo.FileContents = listFileContent;
                compListInfo.FileNm = totalReportViewModel.FileContents[0].FileNm;
                compListInfo.TotalReportSn = totalReportSn;
                compListInfo.FinalSubmitYn = "D";

                scMentorMappingService.SaveDbContext();


                var compListInfo2 = Mapper.Map<MentoringTotalReportViewModel>(compListInfo);

                return View(compListInfo2);
            }

            compListInfo.TotalReportSn = totalReportSn;

            return View(compListInfo);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegDeepenReport(MentoringTotalReportViewModel dataRequestViewModel,string deleteFileSns, IEnumerable<HttpPostedFileBase> files, string submitType)
        {
            ViewBag.naviLeftMenu = Global.Report;

            var mentorKey = Session[Global.LoginID].ToString();

            var mentorInfos = await vcUsrService.selectScUsrByTcms(mentorKey);
            var mentorId = mentorInfos.LoginId;

            var compInfo = dataRequestViewModel.CompSn;
            var numSn = dataRequestViewModel.NumSn;
            var subNumSn = dataRequestViewModel.SubNumSn;
            var conCodeBy = dataRequestViewModel.ConCode;

            // MentorId로 해당 기업의 num_sn, sub_num_sn을 가져온다
            SqlParameter compSn = new SqlParameter("COMP_SN", compInfo);
            SqlParameter loginId = new SqlParameter("LOGIN_ID", Session[Global.LoginID].ToString());

            object[] parameters = new object[] { compSn, loginId };

            var compList = await procMngService.getMentorCompNumSn(parameters);

            // 기존에 작성했던 심화보고서가 있을 경우 -> 내용 update
            var checkRegDeepenReport = await _scMentoringTotalReportService.checkRegDeepenReport(compInfo, mentorId, numSn, subNumSn, conCodeBy);
            if (checkRegDeepenReport.Count > 0)
            {

                var totalReportSnInfo = checkRegDeepenReport[0].TotalReportSn;

                var scMentoringTotalReport = await _scMentoringTotalReportService.GetMentoringTotalReportById(totalReportSnInfo);

                var listscFileInfo = scMentoringTotalReport.ScMentoringTrFileInfoes.Select(mtfi => mtfi.ScFileInfo).Where(fi => fi.Status == "N");

                var listFileContent = Mapper.Map<List<FileContent>>(listscFileInfo);

                scMentoringTotalReport.DeepenContents = dataRequestViewModel.DeepenContents;
                scMentoringTotalReport.SubmitDt = dataRequestViewModel.SubmitDt.Value;
                scMentoringTotalReport.UpdDt = DateTime.Now;

                //삭제파일 상태 업데이트

                if (!string.IsNullOrEmpty(deleteFileSns.Trim()))
                {
                    foreach (var deleteFileSn in deleteFileSns.Split(',').AsEnumerable())
                    {
                        scMentoringTotalReport.ScMentoringTrFileInfoes.Select(mtfi => mtfi.ScFileInfo).Where(fi => fi.FileSn == int.Parse(deleteFileSn)).FirstOrDefault().Status = "D";
                    }
                }

                //신규파일정보저장 및 파일업로드
                if (files != null)
                {
                    foreach (var file in files)
                    {
                        if (file != null)
                        {

                            var fileHelper = new FileHelper();

                            var savedFileName = fileHelper.GetUploadFileName(file);

                            // folder 별로 묶이게
                            var folderNm = compInfo.ToString() + mentorKey.ToString() + numSn.ToString() + subNumSn.ToString() + conCodeBy.ToString();
                            var subDirectoryPath = Path.Combine(FileType.DeepenReport.ToString(), DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString(), folderNm.ToString());

                            var savedFilePath = Path.Combine(subDirectoryPath, savedFileName);

                            var tcmsLoginKey = Session[Global.LoginID].ToString();
                            var tcmsLoginKeyCon = int.Parse(tcmsLoginKey);

                            var mentorUsrInfo = await vcUsrService.getUsrInfoByTcmsKey(tcmsLoginKeyCon);

                            var mentorIdInfo = mentorUsrInfo.LoginId;

                            var scFileInfo = new ScFileInfo { FileNm = Path.GetFileName(file.FileName), FilePath = savedFilePath, Status = "N", RegId = mentorIdInfo, RegDt = DateTime.Now };

                            var scMentoringTrFileInfo = new ScMentoringTrFileInfo { ScFileInfo = scFileInfo };

                            scMentoringTrFileInfo.Classify = "A";

                            scMentoringTotalReport.RegId = mentorIdInfo;
                            scMentoringTotalReport.MentorId = mentorIdInfo;
                            scMentoringTotalReport.TotalReportSn = totalReportSnInfo;

                            scMentoringTotalReport.ScMentoringTrFileInfoes.Add(scMentoringTrFileInfo);


                            await fileHelper.UploadFile(file, subDirectoryPath, savedFileName);

                        }
                    }
                }

                await _scMentoringTotalReportService.ModifyDeepenReportAsync(scMentoringTotalReport);

                var totalReportViewModel = Mapper.Map<MentoringTotalReportViewModel>(scMentoringTotalReport);

                totalReportViewModel.FinalSubmitYn = "D";

                scMentorMappingService.SaveDbContext();

                // 심화보고서 최종 제출
                if (submitType == "C")
                {
                    //최종 제출 구현
                    var totalReportSn = totalReportViewModel.TotalReportSn;

                    var TotalReportInfo = await _scMentoringTotalReportService.GetTotalReportInfoByReportSn(totalReportSn);

                    var b = Mapper.Map<ScMentoringTotalReport>(TotalReportInfo);

                    int result2 = await _scMentoringTotalReportService.SaveDbContextAsync();

                    // 추출한 mentor_id를 이용하여 mentor_sn, Ba_sn을 추출
                    var mentorLoginKeys = Session[Global.LoginID].ToString();
                    var mentorInfo2 = await vcUsrService.getMentorInfoById(mentorLoginKeys);

                    //var conCode = await vcMentorMappingService.getMentorMappingInfo(mentorInfo.MentorSn);
                    var conCodes = await vcMentorMappingService.getMentorMappingInfoList(Convert.ToString(TotalReportInfo.CompSn), Convert.ToString(mentorInfo2.MentorSn));

                    // 최종 제출을 확인하기 전 이미 최종 제출을 한 보고서가 있는지 확인
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Successfully')", true);
                    var compSns = TotalReportInfo.CompSn ?? default(int);
                    var baSns = mentorInfo2.BaSn;
                    var mentorSn = mentorInfo2.MentorSn;
                    var numSns = TotalReportInfo.NumSn;
                    var subNumSns = TotalReportInfo.SubNumSn;

                    VcLastReportNSat deepenReport = new VcLastReportNSat();
                    deepenReport.TotalReportSn = totalReportSn;
                    deepenReport.CompSn = compSns;
                    deepenReport.BaSn = baSns;
                    deepenReport.MentorSn = mentorSn;
                    deepenReport.NumSn = numSns;
                    deepenReport.SubNumSn = subNumSns;
                    deepenReport.ConCode = conCodes[0].ConCode;
                    deepenReport.ConStatus = "P";

                    var a = Mapper.Map<VcLastReportNSat>(deepenReport);

                    int resultFinalSubmit = await vcLastReportNSatService.AddCheckAasync(a);

                    var mentorIdFinalSubmit = await vcUsrService.getUsrInfoByTcmsKey(int.Parse(mentorLoginKeys));

                    TotalReportInfo.FinalSubmitYn = "Y";
                    await _scMentoringTotalReportService.SaveDbContextAsync();

                    // 심화보고서 최종 제출 시 if테이블에 url 담기는 것
                    int cnt = 0;

                    // if 테이블에 insert
                    await scMentorMappingService.SaveDbContextAsync();

                    TcmsIfLastReport tcmsIfLastReport = new TcmsIfLastReport();

                    // update하기 위한
                    var compObj = await vcCompInfoService.getVcCompInfoByCompSn(deepenReport.CompSn);
                    var baObj = await vcBaInfoService.getVcBaInfoByBaSn(baSns);
                    var mentorObj = await vcMentorInfoService.getVcMentorInfoByMentorSn(Convert.ToString(mentorSn));

                    var tcmsIfLastReportObj = await tcmsIfLastReportService.getTcmsIfLastReportInfo(compObj.TcmsLoginKey, baObj.TcmsLoginKey, mentorObj.TcmsLoginKey, conCodes[0].ConCode);


                    if (files != null)
                    {


                        foreach (var file in files)
                        {

                            var fileHelper = new FileHelper();

                            var rootFilePath = ConfigurationManager.AppSettings["RootFilePath"];
                            var savedFileName = fileHelper.GetUploadFileName(file);

                            var folderNm = compInfo.ToString() + mentorKey.ToString() + numSn.ToString() + subNumSn.ToString() + conCodeBy.ToString();
                            var subDirectoryPath = Path.Combine(FileType.DeepenReport.ToString(), DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString(), folderNm.ToString());

                            var directoryPath = Path.Combine(rootFilePath, subDirectoryPath);
                            var absFilePath = directoryPath + "//" + savedFileName;                // 심화보고서 최종 제출 URL

                            // 기본적인 정보는 한번 담고 
                            if (cnt == 0)
                            {

                                tcmsIfLastReport.InfId = await satiNumGenerator();
                                tcmsIfLastReport.InfDt = DateTime.Today;

                                tcmsIfLastReport.CompLoginKey = compObj.TcmsLoginKey;
                                tcmsIfLastReport.BaLoginKey = baObj.TcmsLoginKey;
                                tcmsIfLastReport.MentorLoginKey = mentorObj.TcmsLoginKey;

                                tcmsIfLastReport.NumSn = numSn;
                                tcmsIfLastReport.SubNumSn = subNumSn;

                                tcmsIfLastReport.ConCode = conCodes[0].ConCode;
                                tcmsIfLastReport.File1 = absFilePath;

                                tcmsIfLastReportService.Insert(tcmsIfLastReport);
                                tcmsIfLastReportService.SaveDbContext();

                            }
                            else
                            {

                                // update 해야 함
                                // compSn, baSn, mentorSn, conCode을 이용하여 조회 해서 file 2부터 update


                                if (cnt == 1)
                                {

                                    tcmsIfLastReportObj.File2 = absFilePath;
                                    tcmsIfLastReportService.SaveDbContext();

                                }
                                else if (cnt == 2)
                                {

                                    tcmsIfLastReportObj.File3 = absFilePath;
                                    tcmsIfLastReportService.SaveDbContext();

                                }
                                else if (cnt == 3)
                                {

                                    tcmsIfLastReportObj.File4 = absFilePath;
                                    tcmsIfLastReportService.SaveDbContext();

                                }
                                else if (cnt == 4)
                                {

                                    tcmsIfLastReportObj.File5 = absFilePath;
                                    tcmsIfLastReportService.SaveDbContext();

                                }

                            }
                            cnt++;
                        }

                    }
                    else
                    {
                        // file 이 null 일경우 조회해서 insert
                        var scMentoringTotalReport2 = await _scMentoringTotalReportService.GetMentoringTotalReportById(totalReportSn);

                        var listscFileInfo2 = scMentoringTotalReport.ScMentoringTrFileInfoes.Select(mtfi => mtfi.ScFileInfo).Where(fi => fi.Status == "N");

                        var rootFilePath = ConfigurationManager.AppSettings["RootFilePath"];

                        var listFileContent2 =
                           Mapper.Map<List<FileContent>>(listscFileInfo2);

                        // 다운로드 가능한 링크
                        var fullPath = rootFilePath + listFileContent2[0].FilePath;

                        tcmsIfLastReport.InfId = await satiNumGenerator();
                        tcmsIfLastReport.RegDt = scMentoringTotalReport2.RegDt;
                        tcmsIfLastReport.InfDt = DateTime.Today;

                        tcmsIfLastReport.CompLoginKey = compObj.TcmsLoginKey;
                        tcmsIfLastReport.BaLoginKey = baObj.TcmsLoginKey;
                        tcmsIfLastReport.MentorLoginKey = mentorObj.TcmsLoginKey;

                        tcmsIfLastReport.NumSn = numSn;
                        tcmsIfLastReport.SubNumSn = subNumSn;

                        tcmsIfLastReport.ConCode = conCodes[0].ConCode;
                        tcmsIfLastReport.File1 = fullPath;

                        tcmsIfLastReportService.Insert(tcmsIfLastReport);
                        tcmsIfLastReportService.SaveDbContext();
                        
                    }

                    // 데이터 전송
                    if (cnt == 0)
                    {
                        var status = sendLastReport(tcmsIfLastReport);

                        if (status == "S")
                        {
                            tcmsIfLastReport.InsertYn = "S";
                            tcmsIfLastReportService.SaveDbContext();
                        }
                        else
                        {
                            tcmsIfLastReport.InsertYn = "E";
                            tcmsIfLastReportService.SaveDbContext();
                        }

                    }
                    else
                    {
                        var status = sendLastReport(tcmsIfLastReportObj);

                        if (status == "S")
                        {
                            tcmsIfLastReportObj.InsertYn = "S";
                            tcmsIfLastReportService.SaveDbContext();
                        }
                        else
                        {
                            tcmsIfLastReportObj.InsertYn = "E";
                            tcmsIfLastReportService.SaveDbContext();
                        }

                    }

                }
                return RedirectToAction("DeepenReportList", "Report");
            }

            else
            {

                var scMentoringTotalReport = Mapper.Map<ScMentoringTotalReport>(dataRequestViewModel);

                scMentoringTotalReport.MentorId = mentorId;
                scMentoringTotalReport.RegId = mentorId;
                scMentoringTotalReport.RegDt = DateTime.Now;
                scMentoringTotalReport.Status = "N";
                scMentoringTotalReport.CompSn = dataRequestViewModel.CompSn;

                // 기수, 서브기수 추가, 최종제출 유무
                scMentoringTotalReport.NumSn = compList[0].NUM_SN;
                scMentoringTotalReport.SubNumSn = compList[0].SUB_NUM_SN;

                // 최종 제출 유무판단 시 확인
                var mentorLoginKey = Session[Global.LoginID].ToString();
                var mentorInfo = await vcUsrService.getMentorInfoById(mentorLoginKey);

                var baSn = mentorInfo.BaSn;

                // conCode를 가져오기위한 method
                //var conCodeInfo = await vcMentorMappingService.getConcodeInfo(dataRequestViewModel.CompSn, baSn, mentorInfo.MentorSn, compList[0].NUM_SN, compList[0].SUB_NUM_SN);

                //var conCode = conCodeInfo.ConCode;
                var conCode = dataRequestViewModel.ConCode;

                // vc_mentor_mapping에서 프로젝트 코드 할당
                scMentoringTotalReport.ConCode = conCode;
                scMentoringTotalReport.FinalSubmitYn = "D";

                // 이미 작성했던 심화보고서가 있다.
                if (checkRegDeepenReport.Count != 0)
                {
                    scMentoringTotalReport.FinalSubmitYn = "D";
                    scMentorMappingService.SaveDbContext();
                }

                //신규파일정보저장 및 파일업로드
                if (files != null)
                {
                    foreach (var file in files)
                    {
                        if (file != null)
                        {

                            var fileHelper = new FileHelper();

                            var savedFileName = fileHelper.GetUploadFileName(file);

                            var folderNm = compInfo.ToString() + mentorKey.ToString() + numSn.ToString() + subNumSn.ToString() + conCodeBy.ToString();
                            var subDirectoryPath = Path.Combine(FileType.DeepenReport.ToString(), DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString(), folderNm.ToString());

                            var savedFilePath = Path.Combine(subDirectoryPath, savedFileName);

                            var tcmsLoginKey = Session[Global.LoginID].ToString();
                            var tcmsLoginKeyCon = int.Parse(tcmsLoginKey);

                            var mentorUsrInfo = await vcUsrService.getUsrInfoByTcmsKey(tcmsLoginKeyCon);

                            var mentorIdInfo = mentorUsrInfo.LoginId;

                            var scFileInfo = new ScFileInfo { FileNm = Path.GetFileName(file.FileName), FilePath = savedFilePath, Status = "N", RegId = mentorIdInfo, RegDt = DateTime.Now };

                            var scMentoringTrFileInfo = new ScMentoringTrFileInfo { ScFileInfo = scFileInfo };

                            scMentoringTrFileInfo.Classify = "A";

                            scMentoringTotalReport.RegId = mentorIdInfo;
                            scMentoringTotalReport.MentorId = mentorIdInfo;

                            scMentoringTotalReport.ScMentoringTrFileInfoes.Add(scMentoringTrFileInfo);

                            //var rootFilePath = ConfigurationManager.AppSettings["RootFilePath"];
                            //var directoryPath = Path.Combine(rootFilePath, subDirectoryPath);
                            //var testFilePath = directoryPath + "//" + savedFileName;                // 심화보고서 최종 제출 URL

                            await fileHelper.UploadFile(file, subDirectoryPath, savedFileName);

                        }
                    }
                }

                // 저장
                int result = await _scMentoringTotalReportService.AddScMentoringTotalReportAsync(scMentoringTotalReport);

                // 심화보고서 최종제출
                if (submitType == "C")
                {
                    // 최종 제출 구현
                    var totalReportSn = scMentoringTotalReport.TotalReportSn;

                    var TotalReportInfo = await _scMentoringTotalReportService.GetTotalReportInfoByReportSn(totalReportSn);

                    var b = Mapper.Map<ScMentoringTotalReport>(TotalReportInfo);

                    int result2 = await _scMentoringTotalReportService.SaveDbContextAsync();

                    // 추출한 mentor_id를 이용하여 mentor_sn, Ba_sn을 추출
                    var mentorLoginKeys = Session[Global.LoginID].ToString();
                    var mentorInfo2 = await vcUsrService.getMentorInfoById(mentorLoginKeys);

                    //var conCode = await vcMentorMappingService.getMentorMappingInfo(mentorInfo.MentorSn);
                    var conCodes = await vcMentorMappingService.getMentorMappingInfoList(Convert.ToString(TotalReportInfo.CompSn), Convert.ToString(mentorInfo2.MentorSn));

                    // 최종 제출을 확인하기 전 이미 최종 제출을 한 보고서가 있는지 확인
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Successfully')", true);
                    var compSns = TotalReportInfo.CompSn ?? default(int);
                    var baSns = mentorInfo2.BaSn;
                    var mentorSn = mentorInfo2.MentorSn;
                    var numSns = TotalReportInfo.NumSn;
                    var subNumSns = TotalReportInfo.SubNumSn;

                    VcLastReportNSat deepenReport = new VcLastReportNSat();
                    deepenReport.TotalReportSn = totalReportSn;
                    deepenReport.CompSn = compSns;
                    deepenReport.BaSn = baSns;
                    deepenReport.MentorSn = mentorSn;
                    deepenReport.NumSn = numSns;
                    deepenReport.SubNumSn = subNumSns;
                    deepenReport.ConCode = conCodes[0].ConCode;
                    deepenReport.ConStatus = "P";

                    var a = Mapper.Map<VcLastReportNSat>(deepenReport);

                    int resultFinalSubmit = await vcLastReportNSatService.AddCheckAasync(a);

                    var mentorIdFinalSubmit = await vcUsrService.getUsrInfoByTcmsKey(int.Parse(mentorLoginKey));

                    TotalReportInfo.FinalSubmitYn = "Y";
                    await _scMentoringTotalReportService.SaveDbContextAsync();

                    // 심화보고서 파일 Url로 확인
                    //foreach (var asdf in files)
                    //{
                    //    var fileNames = asdf.

                    //}

                    // 심화보고서 최종 제출 시 if테이블에 url 담기는 것
                    // 심화보고서 최종 제출 시 if테이블에 url 담기는 것
                    int cnt = 0;

                    // if 테이블에 insert
                    await scMentorMappingService.SaveDbContextAsync();

                    TcmsIfLastReport tcmsIfLastReport = new TcmsIfLastReport();

                    // update하기 위한
                    var compObj = await vcCompInfoService.getVcCompInfoByCompSn(deepenReport.CompSn);
                    var baObj = await vcBaInfoService.getVcBaInfoByBaSn(baSns);
                    var mentorObj = await vcMentorInfoService.getVcMentorInfoByMentorSn(Convert.ToString(mentorSn));

                    var tcmsIfLastReportObj = await tcmsIfLastReportService.getTcmsIfLastReportInfo(compObj.TcmsLoginKey, baObj.TcmsLoginKey, mentorObj.TcmsLoginKey, conCodes[0].ConCode);


                    if (files != null)
                    {


                        foreach (var file in files)
                        {

                            var fileHelper = new FileHelper();

                            var rootFilePath = ConfigurationManager.AppSettings["RootFilePath"];
                            var savedFileName = fileHelper.GetUploadFileName(file);

                            var folderNm = compInfo.ToString() + mentorKey.ToString() + numSn.ToString() + subNumSn.ToString() + conCodeBy.ToString();
                            var subDirectoryPath = Path.Combine(FileType.DeepenReport.ToString(), DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString(), folderNm.ToString());

                            var directoryPath = Path.Combine(rootFilePath, subDirectoryPath);
                            var absFilePath = directoryPath + "//" + savedFileName;                // 심화보고서 최종 제출 URL



                            // 기본적인 정보는 한번 담고 
                            if (cnt == 0)
                            {

                                tcmsIfLastReport.InfId = await satiNumGenerator();
                                tcmsIfLastReport.InfDt = DateTime.Today;

                                tcmsIfLastReport.CompLoginKey = compObj.TcmsLoginKey;
                                tcmsIfLastReport.BaLoginKey = baObj.TcmsLoginKey;
                                tcmsIfLastReport.MentorLoginKey = mentorObj.TcmsLoginKey;

                                tcmsIfLastReport.NumSn = numSn;
                                tcmsIfLastReport.SubNumSn = subNumSn;

                                tcmsIfLastReport.ConCode = conCodes[0].ConCode;
                                tcmsIfLastReport.File1 = absFilePath;

                                tcmsIfLastReportService.Insert(tcmsIfLastReport);
                                tcmsIfLastReportService.SaveDbContext();

                            }
                            else
                            {

                                // update 해야 함
                                // compSn, baSn, mentorSn, conCode을 이용하여 조회 해서 file 2부터 update


                                if (cnt == 1)
                                {

                                    tcmsIfLastReportObj.File2 = absFilePath;
                                    tcmsIfLastReportService.SaveDbContext();

                                }
                                else if (cnt == 2)
                                {

                                    tcmsIfLastReportObj.File3 = absFilePath;
                                    tcmsIfLastReportService.SaveDbContext();

                                }
                                else if (cnt == 3)
                                {

                                    tcmsIfLastReportObj.File4 = absFilePath;
                                    tcmsIfLastReportService.SaveDbContext();

                                }
                                else if (cnt == 4)
                                {

                                    tcmsIfLastReportObj.File5 = absFilePath;
                                    tcmsIfLastReportService.SaveDbContext();

                                }

                            }
                            cnt++;
                        }

                    }
                    else
                    {
                        // file 이 null 일경우 조회해서 insert
                        var scMentoringTotalReport2 = await _scMentoringTotalReportService.GetMentoringTotalReportById(totalReportSn);

                        var listscFileInfo2 = scMentoringTotalReport.ScMentoringTrFileInfoes.Select(mtfi => mtfi.ScFileInfo).Where(fi => fi.Status == "N");

                        var rootFilePath = ConfigurationManager.AppSettings["RootFilePath"];

                        var listFileContent2 =
                           Mapper.Map<List<FileContent>>(listscFileInfo2);

                        // 다운로드 가능한 링크
                        var fullPath = rootFilePath + listFileContent2[0].FilePath;

                        tcmsIfLastReport.InfId = await satiNumGenerator();
                        tcmsIfLastReport.RegDt = scMentoringTotalReport2.RegDt;

                        tcmsIfLastReport.CompLoginKey = compObj.TcmsLoginKey;
                        tcmsIfLastReport.BaLoginKey = baObj.TcmsLoginKey;
                        tcmsIfLastReport.MentorLoginKey = mentorObj.TcmsLoginKey;

                        tcmsIfLastReport.NumSn = numSn;
                        tcmsIfLastReport.SubNumSn = subNumSn;

                        tcmsIfLastReport.ConCode = conCodes[0].ConCode;
                        tcmsIfLastReport.File1 = fullPath;
                        tcmsIfLastReport.InfDt = DateTime.Today;

                        tcmsIfLastReportService.Insert(tcmsIfLastReport);
                        tcmsIfLastReportService.SaveDbContext();


                    }

                    // 데이터 전송
                    if (cnt == 0)
                    {
                        var status = sendLastReport(tcmsIfLastReport);

                        if(status == "S")
                        {
                            tcmsIfLastReport.InsertYn = "S";
                            tcmsIfLastReportService.SaveDbContext();
                        }
                        else
                        {
                            tcmsIfLastReport.InsertYn = "E";
                            tcmsIfLastReportService.SaveDbContext();
                        }
                        
                    }
                    else
                    {
                        var status = sendLastReport(tcmsIfLastReportObj);

                        if (status == "S")
                        {
                            tcmsIfLastReportObj.InsertYn = "S";
                            tcmsIfLastReportService.SaveDbContext();
                        }
                        else
                        {
                            tcmsIfLastReportObj.InsertYn = "E";
                            tcmsIfLastReportService.SaveDbContext();
                        }

                    }

                }
                if (result != -1)

                    return RedirectToAction("DeepenReportList", "Report");

                else
                {

                    ModelState.AddModelError("", "자료요청 등록 실패.");
                    return View(dataRequestViewModel);

                }

            }

        }

        public async Task<string> satiNumGenerator()
        {
            string infId = "";
            string infPrefix = "TCMS_";
            string rst = "";

            var tcmsIfLastReportList = await tcmsIfLastReportService.getTcmsIfSurvey();

            if (tcmsIfLastReportList.Count == 0)
            {
                infId = "00001";
                rst = infPrefix + infId;
            }
            else
            {
                var maxVal = tcmsIfLastReportList.Max(x => x.InfId); // 최대값 식별
                string[] maxValResult = maxVal.Split('_');
                int itemp = int.Parse(maxValResult[1]);
                string temp = ++itemp + "";

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

        #region
        //public string sendLastReport(TcmsIfLastReport tcmsIfLastReport)
        //{
        //    HttpContext.Response.AppendHeader("Access-Control-Allow-Origin", "*");
        //    string result = "";

        //    var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://tcms.igarim.com/Api/tcms_if_last_report.php");
        //    httpWebRequest.ContentType = "application/jsonp";
        //    httpWebRequest.Method = "POST";
        //    httpWebRequest.CookieContainer = new CookieContainer();
        //    HttpCookieCollection cookies = Request.Cookies;
        //    for (int i = 0; i < cookies.Count; i++)
        //    {
        //        HttpCookie httpCookie = cookies.Get(i);
        //        Cookie cookie = new Cookie();
        //        cookie.Domain = httpWebRequest.RequestUri.Host;
        //        cookie.Expires = httpCookie.Expires;
        //        cookie.Name = httpCookie.Name;
        //        cookie.Path = httpCookie.Path;
        //        cookie.Secure = httpCookie.Secure;
        //        cookie.Value = httpCookie.Value;
        //        httpWebRequest.CookieContainer.Add(cookie);
        //    }

        //    using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
        //    {
        //        string jsont = new JavaScriptSerializer().Serialize(new
        //        {

        //            InfId = tcmsIfLastReport.InfId,
        //            CompLoginKey = tcmsIfLastReport.CompLoginKey,
        //            BaLoginKey = tcmsIfLastReport.BaLoginKey,
        //            MentorLoginKey = tcmsIfLastReport.MentorLoginKey,
        //            NumSn = tcmsIfLastReport.NumSn,
        //            SubNumSn = tcmsIfLastReport.SubNumSn,
        //            ConCode = tcmsIfLastReport.ConCode,

        //            File1 = tcmsIfLastReport.File1,
        //            File2 = tcmsIfLastReport.File2,
        //            File3 = tcmsIfLastReport.File3,
        //            File4 = tcmsIfLastReport.File4,
        //            File5 = tcmsIfLastReport.File5,

        //            InfDt = tcmsIfLastReport.InfDt

        //        });

        //        streamWriter.Write(jsont);

        //    }

        //    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
        //    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
        //    {

        //        result = streamReader.ReadToEnd();

        //    }

        //    return result;

        //}
        #endregion
        public string sendLastReport(TcmsIfLastReport tcmsIfLastReport)
        {

            HttpContext.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            string result = "";
            string backSlash = "";

            StatusModel statusModel = new StatusModel();

            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://tcms.igarim.com/Api/tcms_if_last_report.php");
            //httpWebRequest.Accept = "application/json";
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
                string jsont = new JavaScriptSerializer().Serialize(new
                {
                    InfId = tcmsIfLastReport.InfId,
                    CompLoginKey = tcmsIfLastReport.CompLoginKey,
                    BaLoginKey = tcmsIfLastReport.BaLoginKey,
                    MentorLoginKey = tcmsIfLastReport.MentorLoginKey,
                    NumSn = tcmsIfLastReport.NumSn,
                    SubNumSn = tcmsIfLastReport.SubNumSn,
                    ConCode = tcmsIfLastReport.ConCode,

                    File1 = tcmsIfLastReport.File1.Replace("\\", "/"),
                    File2 = tcmsIfLastReport.File2.Replace("\\", "/"),
                    File3 = tcmsIfLastReport.File3.Replace("\\", "/"),
                    File4 = tcmsIfLastReport.File4.Replace("\\", "/"),
                    File5 = tcmsIfLastReport.File5.Replace("\\", "/"),

                    regDt = tcmsIfLastReport.RegDt.ToString(),
                    InfDt = tcmsIfLastReport.InfDt.ToString()
                });
                backSlash = jsont.Replace("\\", "");
                byte[] ba = Encoding.UTF8.GetBytes("json="+backSlash);

                requestStream.Write(ba, 0, ba.Length);
                requestStream.Flush();
                requestStream.Close();
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                result = streamReader.ReadToEnd();
                string[] rstSplit = result.Split('\n');
                statusModel = (StatusModel)js.Deserialize(rstSplit[1], typeof(StatusModel));
            }
            return statusModel.status;
        }


        [HttpPost]
        public async Task<JsonResult> GetCompanyNm(int BizWorkSn, int Year)
        {

            SqlParameter loginId = new SqlParameter("LOGIN_ID", Session[Global.LoginID].ToString());
            object[] parameters = new object[] { loginId };

            var listObj = await procMngService.getMentorReportList(parameters);

            var bizList = ReportHelper.MakeCompanyList(listObj);

            return Json(bizList);
        }

        //#endregion


        #endregion

        public async Task<ActionResult> EditReport(int year, int questionSn, string status)
        {
            RptMaster rpt = await rptMasterService.SelectRpt(year, questionSn, status);

            rpt.Status = "P";

            await rptMasterService.SaveDbContextAsync();

            return RedirectToAction("BasicSurveyReport", "Report", new { area = "Mentor", });
        }

        public async Task<ActionResult> DeepenReportList(SelectedMentorTotalReportParmModel param, string curPage)
        {
            ViewBag.naviLeftMenu = Global.Report;

            var mentorId = Session[Global.LoginID].ToString();

            //검색조건을 유지하기 위한
            ViewBag.SelectParam = param;

            SqlParameter loginId = new SqlParameter("LOGIN_ID", Session[Global.LoginID].ToString());
            object[] parameters = new object[] { loginId };
            var compList = await procMngService.MentorGetDeepenReportList(parameters);

            // 기업명 List
            var compDropDown = Mapper.Map<List<MentoringTotalReportViewModel>>(compList);

            foreach (var item in compDropDown)
            {
                var compSn = item.CompSn;
                var mentorLoginKey = item.MentorId;
                var mentorInfo = await vcUsrService.selectScUsrByTcms(Convert.ToString(mentorLoginKey));
                var mentorIds = mentorInfo.LoginId;

                var numSn = item.NumSn;
                var subNumSn = item.SubNumSn;
                var conCode = item.ConCode;

                var checkRegDeepenReport = await _scMentoringTotalReportService.checkRegDeepenReport(compSn, mentorIds, numSn, subNumSn, conCode);
                if (checkRegDeepenReport.Count > 0)
                {
                    var totalReportSnInfo = checkRegDeepenReport[0].TotalReportSn;
                    item.TotalReportSn = totalReportSnInfo;
                    item.SubmitDt = checkRegDeepenReport[0].SubmitDt;
                    item.FinalSubmitYn = checkRegDeepenReport[0].FinalSubmitYn;
                }

            }

            var compDropDown2 = Mapper.Map<List<MentoringTotalReportViewModel>>(compDropDown);

            int pagingSize = int.Parse(ConfigurationManager.AppSettings["PagingSize"]);

            //실제 쿼리
            return View(new StaticPagedList<MentoringTotalReportViewModel>(compDropDown2, 1, pagingSize, compDropDown2.Count));

        }

        [HttpPost]
        public async Task<ActionResult> DeepenReportList(string curPage)
        {
            ViewBag.naviLeftMenu = Global.Report;

            var mentorId = Session[Global.LoginID].ToString();

            SqlParameter loginId = new SqlParameter("LOGIN_ID", Session[Global.LoginID].ToString());
            object[] parameters = new object[] { loginId };
            var compList = await procMngService.getCompMapping(parameters);

            // 기업명 List
            var compDropDown = Mapper.Map<List<MentoringReportViewModel>>(compList);

            MentoringReportViewModel title = new MentoringReportViewModel();
            title.CompSn = 0;
            title.CompNm = "기업 선택";
            compDropDown.Insert(0, title);

            SelectList compListSelect = new SelectList(compDropDown, "CompSn", "CompNm");

            ViewBag.SelectCompList = compListSelect;

            //실제 쿼리
            int pagingSize = int.Parse(ConfigurationManager.AppSettings["PagingSize"]);

            var mentoringList = await _scMentoringTotalReportService.getDeepenReportListByMentorId(mentorId);

            var listTotalReportView =
               Mapper.Map<List<MentoringTotalReportViewModel>>(mentoringList);

            //실제 쿼리

            return View(new StaticPagedList<MentoringTotalReportViewModel>(listTotalReportView.ToPagedList(int.Parse(curPage), pagingSize), int.Parse(curPage ?? "1"), pagingSize, listTotalReportView.Count));

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
            ViewBag.FinalSubmitYn = selectParam.FinalSubmitYn;

            //totalReportViewModel.CompNm = compNm.ToString();

            //검색조건 유지를 위해
            ViewBag.SelectParam = selectParam;

            // 최종제출 중복 제거
            var TotalReportInfo = await _scMentoringTotalReportService.GetTotalReportInfoByReportSn(totalReportSn);

            // mentorSn을 가져오기 위한 method
            var mentorLoginKey = Session[Global.LoginID].ToString();
            var mentorInfo = await vcUsrService.getMentorInfoById(mentorLoginKey);

            var baSn = mentorInfo.BaSn;
            var numSn = TotalReportInfo.NumSn;
            var subNumSn = TotalReportInfo.SubNumSn;

            // conCode를 가져오기위한 method
            //var conCodeInfo = await vcMentorMappingService.getConcodeInfo(compSn, baSn, mentorInfo.MentorSn, numSn, subNumSn);

            var conCode = TotalReportInfo.ConCode;

            var checkSubmit = await vcLastReportNSatService.checkDeepenSubmitByMentor(compSn, baSn, numSn, subNumSn, conCode);

            // 심화보고서 최종제출 한사람만 Y
            if (TotalReportInfo.FinalSubmitYn == "N")
            {
                if (checkSubmit.Count > 0)
                {
                    // 심화보고서 제출 아예 안했으면 N
                    TotalReportInfo.FinalSubmitYn = "D";
                    _scMentoringTotalReportService.SaveDbContext();
                }
            }

            var scMentoringTotalReport2 = await _scMentoringTotalReportService.GetMentoringTotalReportById(totalReportSn);

            var listscFileInfo2 = scMentoringTotalReport.ScMentoringTrFileInfoes.Select(mtfi => mtfi.ScFileInfo).Where(fi => fi.Status == "N");

            var rootFilePath = ConfigurationManager.AppSettings["RootFilePath"];

            var listFileContent2 =
               Mapper.Map<List<FileContent>>(listscFileInfo2);

            // 다운로드 가능한 링크
            var fullPath = rootFilePath + listFileContent2[0].FilePath;

            var fileFullPath = listFileContent2[0].FileFullPath;

            var testPath = scMentoringTotalReport.ScMentoringTrFileInfoes.Select(mtfi => mtfi.ScFileInfo);

            var totalReportViewModel2 =
               Mapper.Map<MentoringTotalReportViewModel>(scMentoringTotalReport2);

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


        //public async Task DownloadTRReportFileMulti(string totalReportSn)
        //{

        //    string archiveName = "download.zip";

        //    //Eager Loading 방식
        //    var listscMentoringTrFileInfo = await _scMentoringTrFileInfoService.GetMentoringTrFileInfo(int.Parse(totalReportSn));

        //    var listScFileInfo = new List<ScFileInfo>();
        //    foreach (var scMentoringTrFileInfo in listscMentoringTrFileInfo)
        //    {
        //        listScFileInfo.Add(scMentoringTrFileInfo.ScFileInfo);
        //    }

        //    var files = Mapper.Map<IList<FileContent>>(listScFileInfo);

        //    new FileHelper().DownloadFile(files, archiveName);

        //}

        //public async Task DownloadReportFileMulti(string reportSn)
        //{

        //    string archiveName = "download.zip";

        //    //Eager Loading 방식
        //    var listscMentoringFileInfo = await _scMentoringFileInfoService.GetMentoringFileInfo(int.Parse(reportSn));

        //    var listScFileInfo = new List<ScFileInfo>();
        //    foreach (var scMentoringFileInfo in listscMentoringFileInfo)
        //    {
        //        listScFileInfo.Add(scMentoringFileInfo.ScFileInfo);
        //    }

        //    var files = Mapper.Map<IList<FileContent>>(listScFileInfo);

        //    new FileHelper().DownloadFile(files, archiveName);

        //}
        #endregion

        // 심화보고서 최종 제출
        public async Task<ActionResult> FinalSubmitDeepenReport(int totalReportSn, int compSn, int numSn, int subNumSn, string conCode, IEnumerable<HttpPostedFileBase> files)
        {
            
            //totalReportSn = Model.TotalReportSn, compSn = Model.CompSn, numSn = Model.NumSn, subNumSn = Model.SubNumSn, conCode = Model.ConCode

            ViewBag.naviLeftMenu = Global.MentorMng;

            // TotalReportSn을 이용해서 sc_mentoring_totalReport에서 mentor_id, comp_sn, num_sn, sub_num_sn 추출
            var TotalReportInfo = await _scMentoringTotalReportService.GetTotalReportInfoByReportSn(totalReportSn);

            var b = Mapper.Map<ScMentoringTotalReport>(TotalReportInfo);

            int result2 = await _scMentoringTotalReportService.SaveDbContextAsync();

            // 추출한 mentor_id를 이용하여 mentor_sn, Ba_sn을 추출
            var mentorLoginKey = Session[Global.LoginID].ToString();
            var mentorInfo = await vcUsrService.getMentorInfoById(mentorLoginKey);

            //var conCode = await vcMentorMappingService.getMentorMappingInfo(mentorInfo.MentorSn);
            var conCodes = await vcMentorMappingService.getMentorMappingInfoList(Convert.ToString(TotalReportInfo.CompSn), Convert.ToString(mentorInfo.MentorSn));

            // 최종 제출을 확인하기 전 이미 최종 제출을 한 보고서가 있는지 확인
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Successfully')", true);
            var compSns = TotalReportInfo.CompSn ?? default(int);
            var baSn = mentorInfo.BaSn;
            var mentorSn = mentorInfo.MentorSn;
            var numSns = TotalReportInfo.NumSn;
            var subNumSns = TotalReportInfo.SubNumSn;

            VcLastReportNSat deepenReport = new VcLastReportNSat();
            deepenReport.TotalReportSn = totalReportSn;
            deepenReport.CompSn = compSn;
            deepenReport.BaSn = baSn;
            deepenReport.MentorSn = mentorSn;
            deepenReport.NumSn = numSns;
            deepenReport.SubNumSn = subNumSns;
            deepenReport.ConCode = conCodes[0].ConCode;
            deepenReport.ConStatus = "P";

            var a = Mapper.Map<VcLastReportNSat>(deepenReport);

            int result = await vcLastReportNSatService.AddCheckAasync(a);

            var mentorId = await vcUsrService.getUsrInfoByTcmsKey(int.Parse(mentorLoginKey));

            TotalReportInfo.FinalSubmitYn = "Y";
            await _scMentoringTotalReportService.SaveDbContextAsync();

            #region
            // 중복 최종제출 확인
            // var checkSubmit = await vcLastReportNSatService.checkDeepenSubmitByMentor(compSn, baSn, numSn, subNumSn, conCode[0].ConCode);
            // var test1 = await _scMentoringTotalReportService.checkFinalSubmit(compSn, mentorId.LoginId, numSn, subNumSn, conCode[0].ConCode);

            //foreach(var item in test1)
            //{
            //    if(item.TotalReportSn == totalReportSn)
            //    {
            //        TotalReportInfo.FinalSubmitYn = "Y";

            //        await _scMentoringTotalReportService.SaveDbContextAsync();

            //    }
            //    else
            //    {
            //        TotalReportInfo.FinalSubmitYn = "D";

            //        await _scMentoringTotalReportService.SaveDbContextAsync();
            //    }
            //}

            //if(checkSubmit.Count > 0)
            //{
            //    TotalReportInfo.FinalSubmitYn = "D";
            //}
            #endregion

            await scMentorMappingService.SaveDbContextAsync();

            return RedirectToAction("DeepenReportList", "Report", new { area = "Mentor" });
        }

        [HttpPost]
        public async Task<JsonResult> CheckReport(int CompSn)
        {
            var year = DateTime.Now.Year;
            var result = "F";

            var checkReport = await rptMasterService.getRptMasterByCompSnBasicYear(CompSn, year);

            // 작성된 기초보고서가 없을경우
            if (checkReport == null)
            {
                result = "F";
            }
            else
            {
                if(checkReport.Status == "C")
                {
                    result = "T";
                }
                else
                {
                    result = "F";
                }
                
            }

            return Json(result);

        }

        // 반복적으로 Thread가 돌면서 메소드를 실행
        public void runMethod()
        {
            while (true)
            {

                var beginTime = DateTime.Now;

                // 실행 시킬 메소드 
                reSendingData();

                var duration = DateTime.Now.Subtract(beginTime);

                var sleepDuration = (int)(25 - duration.TotalHours);

                if( 0 < sleepDuration )
                {
                    Thread.Sleep(sleepDuration);
                }

            }
        }

        // TCMS_IF_LAST_REPORT에서 INSERT_YN이 E 이거나 NULL인 값들만 체크해서 재연계 하는 METHOD 구현
        public void reSendingData()
        {

            // TCMS_IF_LAST_REPORT테이블에서 객체 가져오는 부분
            var tcmsIfLastReportObj = tcmsIfLastReportService.unAsyncGetTcmsIfLastReportInfo();

            // STATUS가 E or NULL일 경우 재전송 하는 부분
            foreach(var obj in tcmsIfLastReportObj)
            {

                // 재전송 하는 조건
                if(obj.InsertYn == null || obj.InsertYn == "E")
                {

                    // 동일한 데이터의 재전송 횟수 count check
                    var resendCnt = tcmsIfLastReportService.getResendObj(obj.CompLoginKey ?? default(int), 
                                                                         obj.BaLoginKey ?? default(int), 
                                                                         obj.MentorLoginKey ?? default(int), 
                                                                         obj.NumSn, 
                                                                         obj.SubNumSn, 
                                                                         obj.ConCode);

                    // count가 3번까지만 연계 그후로는 넣지 않는다
                    if(resendCnt.Count < 4)
                    {
                        sendLastReport(obj);
                    }


                }

            }


            // 재전송 횟수 3회까지만 CHECK 이상일 경우는 제외


        }

    }
}