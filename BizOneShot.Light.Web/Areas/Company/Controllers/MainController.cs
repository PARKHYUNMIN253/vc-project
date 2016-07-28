using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BizOneShot.Light.Web.ComLib;
using BizOneShot.Light.Models.ViewModels;
using System.Threading.Tasks;
using BizOneShot.Light.Models.WebModels;
using BizOneShot.Light.Services;
using PagedList;
using AutoMapper;

namespace BizOneShot.Light.Web.Areas.Company.Controllers
{
    [UserAuthorize(Order = 1)]
    public class MainController : BaseController
    {
        private readonly IScUsrService _scUsrService;
        private readonly IScNtcService _scNtcService;
        private readonly IVcCompInfoService _vcCompInfoService;

        public MainController(IScNtcService scNtcServcie, IVcCompInfoService _vcCompInfoService, IScUsrService _scUsrService)
        {
            this._scNtcService = scNtcServcie;
            this._vcCompInfoService = _vcCompInfoService;
            this._scUsrService = _scUsrService;

        }
        // GET: Company/Main
        [MenuAuthorize(Roles = UserType.Company, Order = 2)]
        public async Task<ActionResult> Index()
        {
            string agreeYn = Session[Global.AgreeYn].ToString();
            ViewBag.UserNM = Session[Global.UserNM].ToString();
            //var listScNtc = _scNtcService.GetNotices();

            if (agreeYn != "Y")
            {
                TempData["alert"] = "개인정보 수집 및 이용을 동의하셔야 사용이 가능합니다.";
                return RedirectToAction("CompanyAgreement", "Main");
            }


            var listScNtc = await _scNtcService.GetNoticesForMainAsync();

            // .. Session에 RegistrationNo 추가
            var vcCompInfo = await _vcCompInfoService.getVcCompInfoById(Session[Global.LoginID].ToString());
            Session[Global.CompRegistrationNo] = vcCompInfo.RegistrationNo;
            //

            var noticeViews =
                Mapper.Map<List<NoticeViewModel>>(listScNtc);
            return View(noticeViews);
        }

        public ActionResult CompanyAgreement()
        {
            return View();
        }

        public async Task<ActionResult> AgreeCompanyAgreement()
        {
            VcUsrInfo scUsr = await _scUsrService.selectScUsrByTcms(Session[Global.LoginID].ToString());
            scUsr.AgreeYn = "Y";
            _scUsrService.ModifyScUsr(scUsr);
            await _scUsrService.SaveDbContextAsync();
            Session[Global.AgreeYn] = "Y";
            return RedirectToAction("Index", "Main");
        }


    }
}