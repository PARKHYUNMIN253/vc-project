using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Linq;
using AutoMapper;
using BizOneShot.Light.Models.ViewModels;
using BizOneShot.Light.Models.WebModels;
using BizOneShot.Light.Models.DareModels;
using BizOneShot.Light.Services;
using BizOneShot.Light.Util.Security;
using BizOneShot.Light.Web.ComLib;
using PagedList;
using BizOneShot.Light.Util.Helper;

namespace BizOneShot.Light.Web.Areas.BizManager.Controllers
{
    [UserAuthorize(Order = 1)]
    [MenuAuthorize(Roles = UserType.BizManager, Order = 2)]
    public class MentorMngController : BaseController
    {
        private readonly IScUsrService _scUsrService;
        private readonly IScBizWorkService _scBizWorkService;
        private readonly IScMentorMappingService _scMentorMappingService;

        public MentorMngController(IScUsrService scUsrService, IScBizWorkService _scBizWorkService, IScMentorMappingService _scMentorMappingService)
        {
            this._scUsrService = scUsrService;
            this._scBizWorkService = _scBizWorkService;
            this._scMentorMappingService = _scMentorMappingService;
        }

        // GET: BizManager/MentorMng
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> MentorList()
        {
            ViewBag.LeftMenu = Global.MentorMng;

            string excutorId = null;
            int bizWorkSn = 0;

            //사업담당자 일 경우 담당 사업만 조회
            if (Session[Global.UserDetailType].ToString() == "M")
            {
                excutorId = Session[Global.LoginID].ToString();
            }

            //사업 DropDown List Data
            var listScBizWork = await _scBizWorkService.GetBizWorkList(int.Parse(Session[Global.CompSN].ToString()), excutorId);

            var bizWorkDropDown =
                Mapper.Map<List<BizWorkDropDownModel>>(listScBizWork);

            //사업담당자 일 경우 담당 사업만 조회
            if (Session[Global.UserDetailType].ToString() == "M")
            {
                bizWorkSn = listScBizWork.First().BizWorkSn;
            }
            else
            {
                BizWorkDropDownModel title = new BizWorkDropDownModel();
                title.BizWorkSn = 0;
                title.BizWorkNm = "사업명 선택";
                bizWorkDropDown.Insert(0, title);
            }

            SelectList bizList = new SelectList(bizWorkDropDown, "BizWorkSn", "BizWorkNm");

            ViewBag.SelectBizWorkList = bizList;


            //멘토 분야 DropDown List Data
            var mentorPart = new List<SelectListItem>(){
                new SelectListItem { Value = "", Text = "멘토분야", Selected = true },
                new SelectListItem { Value = "F", Text = "자금" },
                new SelectListItem { Value = "D", Text = "기술개발" },
                new SelectListItem { Value = "T", Text = "세무회계" },
                new SelectListItem { Value = "L", Text = "법무" },
                new SelectListItem { Value = "W", Text = "노무" },
                new SelectListItem { Value = "P", Text = "특허" },
                new SelectListItem { Value = "M", Text = "마케팅" },
                new SelectListItem { Value = "E", Text = "기타" }
            };

            SelectList mentorPartList = new SelectList(mentorPart, "Value", "Text");

            ViewBag.SelectMentorPartList = mentorPartList;



            //전문가 리스트 조회
            var listMentor = await _scMentorMappingService.GetMentorListAsync(int.Parse(Session[Global.CompSN].ToString()), bizWorkSn);

            var usrViews =
                Mapper.Map<List<JoinMentorViewModel>>(listMentor);
            int pagingSize = int.Parse(ConfigurationManager.AppSettings["PagingSize"]);
            return View(new StaticPagedList<JoinMentorViewModel>(usrViews.ToPagedList(1, pagingSize), 1, pagingSize, usrViews.Count));

        }

        [HttpPost]
        public async Task<ActionResult> MentorList(string BizWorkList, string MentorPartList, string curPage)
        {
            ViewBag.LeftMenu = Global.MentorMng;

            string excutorId = null;

            //사업담당자 일 경우 담당 사업만 조회
            if (Session[Global.UserDetailType].ToString() == "M")
            {
                excutorId = Session[Global.LoginID].ToString();
            }
            //사업 DropDown List Data
            var listScBizWork = await _scBizWorkService.GetBizWorkList(int.Parse(Session[Global.CompSN].ToString()), excutorId);


            var bizWorkDropDown =
                Mapper.Map<List<BizWorkDropDownModel>>(listScBizWork);

            //사업담당자 일 경우 담당 사업만 조회
            if (Session[Global.UserDetailType].ToString() == "M")
            {
                BizWorkList = listScBizWork.First().BizWorkSn.ToString();
            }
            else
            {
                BizWorkDropDownModel title = new BizWorkDropDownModel();
                title.BizWorkSn = 0;
                title.BizWorkNm = "사업명 선택";
                bizWorkDropDown.Insert(0, title);
            }

            SelectList bizList = new SelectList(bizWorkDropDown, "BizWorkSn", "BizWorkNm");

            ViewBag.SelectBizWorkList = bizList;


            //멘토 분야 DropDown List Data
            var mentorPart = new List<SelectListItem>(){
                new SelectListItem { Value = "", Text = "멘토분야", Selected = true },
                new SelectListItem { Value = "F", Text = "자금" },
                new SelectListItem { Value = "D", Text = "기술개발" },
                new SelectListItem { Value = "T", Text = "세무회계" },
                new SelectListItem { Value = "L", Text = "법무" },
                new SelectListItem { Value = "W", Text = "노무" },
                new SelectListItem { Value = "P", Text = "특허" },
                new SelectListItem { Value = "M", Text = "마케팅" },
                new SelectListItem { Value = "E", Text = "기타" }
            };

            SelectList mentorPartList = new SelectList(mentorPart, "Value", "Text");

            ViewBag.SelectMentorPartList = mentorPartList;

            //전문가 리스트 조회
            var listMentor = await _scMentorMappingService.GetMentorListAsync(int.Parse(Session[Global.CompSN].ToString()), int.Parse(BizWorkList), MentorPartList);

            var usrViews =
                Mapper.Map<List<JoinMentorViewModel>>(listMentor);

            int pagingSize = int.Parse(ConfigurationManager.AppSettings["PagingSize"]);
            return View(new StaticPagedList<JoinMentorViewModel>(usrViews.ToPagedList(int.Parse(curPage), pagingSize), int.Parse(curPage), pagingSize, usrViews.Count));

        }

        public async Task<ActionResult> RegMentor()
        {
            ViewBag.LeftMenu = Global.MentorMng;

            string excutorId = null;

            //사업담당자 일 경우 담당 사업만 조회
            if (Session[Global.UserDetailType].ToString() == "M")
            {
                excutorId = Session[Global.LoginID].ToString();
            }

            //사업 DropDown List Data
            var listScBizWork = await _scBizWorkService.GetBizWorkList(int.Parse(Session[Global.CompSN].ToString()), excutorId);


            var bizWorkDropDown =
                Mapper.Map<List<BizWorkDropDownModel>>(listScBizWork);

            if (Session[Global.UserDetailType].ToString() == "A")
            {
                BizWorkDropDownModel title = new BizWorkDropDownModel();
                title.BizWorkSn = 0;
                title.BizWorkNm = "사업명 선택";
                bizWorkDropDown.Insert(0, title);
            }

            SelectList bizList = new SelectList(bizWorkDropDown, "BizWorkSn", "BizWorkNm");

            ViewBag.SelectBizWorkList = bizList;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> RegMentor(JoinMentorViewModel joinMentorViewModel)
        {
            ViewBag.LeftMenu = Global.MentorMng;

            string excutorId = null;

            //사업담당자 일 경우 담당 사업만 조회
            if (Session[Global.UserDetailType].ToString() == "M")
            {
                excutorId = Session[Global.LoginID].ToString();
            }
            //사업 DropDown List Data
            var listScBizWork = await _scBizWorkService.GetBizWorkList(int.Parse(Session[Global.CompSN].ToString()), excutorId);


            var bizWorkDropDown =
                Mapper.Map<List<BizWorkDropDownModel>>(listScBizWork);

            if (Session[Global.UserDetailType].ToString() == "A")
            {
                BizWorkDropDownModel title = new BizWorkDropDownModel();
                title.BizWorkSn = 0;
                title.BizWorkNm = "사업명 선택";
                bizWorkDropDown.Insert(0, title);
            }

            SelectList bizList = new SelectList(bizWorkDropDown, "BizWorkSn", "BizWorkNm");

            ViewBag.SelectBizWorkList = bizList;


            if (ModelState.IsValid)
            {
                if (joinMentorViewModel.BizWorkSn == 0)
                {
                    ModelState.AddModelError("", "사업명을 선택하지 않았습니다.");
                    return View(joinMentorViewModel);
                }

                var scUsr = Mapper.Map<ScUsr>(joinMentorViewModel);
                var scCompInfo = Mapper.Map<ScCompInfo>(joinMentorViewModel);

                //회원정보 추가 정보 설정
                scUsr.RegId = Session[Global.LoginID].ToString();
                scUsr.RegDt = DateTime.Now;
                scUsr.Status = "N";
                scUsr.UsrType = "M";
                scUsr.UsrTypeDetail = "E";

                SHACryptography sha2 = new SHACryptography();
                scUsr.LoginPw = sha2.EncryptString(scUsr.LoginPw);

                //회사정보 추가 정보 설정
                scCompInfo.Status = "N";
                scCompInfo.RegId = Session[Global.LoginID].ToString();
                scCompInfo.RegDt = DateTime.Now;

                //멘토 매핑정보 생성
                ScMentorMappiing scMentorMappiing = new ScMentorMappiing();
                scMentorMappiing.BizWorkSn = joinMentorViewModel.BizWorkSn;
                scMentorMappiing.MentorId = scUsr.LoginId;
                scMentorMappiing.Status = "N";
                scMentorMappiing.MngCompSn = int.Parse(Session[Global.CompSN].ToString());
                scMentorMappiing.RegId = scUsr.RegId;
                scMentorMappiing.RegDt = scUsr.RegDt;
                scUsr.ScMentorMappiings.Add(scMentorMappiing);

                //저장
                scCompInfo.ScUsrs.Add(scUsr);


                //bool result = _scUsrService.AddCompanyUser(scCompInfo, scUsr, syUser);
                int result = await _scMentorMappingService.AddMentorAsync(scCompInfo);

                if (result != -1)
                    return RedirectToAction("MentorList", "MentorMng");
                else
                {
                    ModelState.AddModelError("", "멘토 등록 실패.");
                    return View(joinMentorViewModel);
                }
            }
            return View(joinMentorViewModel);
        }

        public async Task<ActionResult> MentorDetail(string bizWorkSn, string mentorId)
        {
            ViewBag.LeftMenu = Global.MentorMng;

            ScMentorMappiing scMentorMapping = await _scMentorMappingService.GetMentor(int.Parse(bizWorkSn), mentorId);
            //string a = scMentorMapping.MentorId;
            var usrView =
                Mapper.Map<JoinMentorViewModel>(scMentorMapping);

            return View(usrView);
        }

        [HttpPost]
        public async Task<JsonResult> DoLoginIdSelect(string LoginId)
        {
            bool result = await _scUsrService.ChkLoginId(LoginId);

            if (result.Equals(true))
            {
                return Json(new { result = true });
            }
            else
            {
                return Json(new { result = false });
            }

        }

        public void DownloadResumeFile()
        {
            //System.Collections.Specialized.NameValueCollection col = Request.QueryString;
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

        //public async Task<ActionResult> ResetMentorPw(string bizWorkSn, string mentorId)
        //{
        //    ViewBag.LeftMenu = Global.MentorMng;

        //    ScMentorMappiing scMentorMapping = await _scMentorMappingService.GetMentor(int.Parse(bizWorkSn), mentorId);

        //    var usrView =
        //        Mapper.Map<JoinMentorViewModel>(scMentorMapping);

        //    SHACryptography sha2 = new SHACryptography();

        //    //ScUsr scUsr = await _scUsrService.SelectScUsr(model.ID);
        //    //SHUSER_SyUser syUsr = new SHUSER_SyUser();

        //    //// 특정한 pw를 미리 설정
        //    //model.NewLoginPw = "ab12345678";

        //    //scUsr.LoginPw = sha2.EncryptString(model.NewLoginPw);
        //    //syUsr.SmartPwd = scUsr.LoginPw;
        //    //await _scUsrService.SaveDbContextAsync();
        //    //var rst = _scUsrService.UpdatePassword(syUsr);

        //    //// 누가 누구의 pw를 초기화 시킬 것인지?

        //    //string usrArea;

        //    //if (Session[Global.UserType].ToString().Equals(Global.SysManager))
        //    //{
        //    //    // 시스템 담당자
        //    //    // 시스템 담당자가 -> 사업관리기관, 전문가 회원 pw 초기화
        //    //    usrArea = "SysManager";
        //    //}
        //    //else if (Session[Global.UserType].ToString().Equals(Global.BizManager))
        //    //{
        //    //    // 사업관리기관
        //    //    // 사업관리기관 -> 사업담당자, 멘토 pw 초기화
        //    //    usrArea = "BizManager";
        //    //}


        //    //return View();

        //    return View(usrView);
        //}


        // add Loy

        public async Task<ActionResult> UserPasswordReset(string mentorId)
        {
            //ScMentorMappiing scMentorMapping = await _scMentorMappingService.GetMentor(int.Parse(bizWorkSn), mentorId);
            //ScUsr scUsr = await _scUsrService.SelectScUsr(scMentorMapping.MentorId);
            ScUsr scUsr = await _scUsrService.SelectScUsr(mentorId);

            //SHACryptography sha2 = new SHACryptography();
            //scUsr.LoginPw = sha2.EncryptString("as12345678");

            await _scUsrService.SaveDbContextAsync();

            var rst = _scUsrService.UserPasswordReset(scUsr);

            // 초기화 후 list로 돌아가기
            return RedirectToAction("MentorList", "MentorMng", new { area = "BizManager" });

        }
    }
}