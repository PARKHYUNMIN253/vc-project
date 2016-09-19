using BizOneShot.Light.Models.CustomModels;
using BizOneShot.Light.Models.WebModels;
using BizOneShot.Light.Services;
using BizOneShot.Light.Web.ComLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace BizOneShot.Light.Web.Controllers
{
    public class ResendController : BaseController
    {
        private readonly ITcmsIfLastReportService _tcmsIfLastReportService;
        private readonly ITcmsIfSurveyService _tcmsIfSurveyService;

        // GET: Resend
        public ActionResult Index()
        {
            return View();
        }

        public ResendController(ITcmsIfLastReportService _tcmsIfLastReportService, ITcmsIfSurveyService _tcmsIfSurveyService)
        {
            this._tcmsIfLastReportService = _tcmsIfLastReportService;
            this._tcmsIfSurveyService = _tcmsIfSurveyService;
        }

        public string resendAllData()
        {
            return "S";
        }

        public string ResendLastReport()
        {
            var tcmsIfLastReportObj = _tcmsIfLastReportService.unAsyncGetTcmsIfLastReportInfo();

            foreach (var obj in tcmsIfLastReportObj)
            {
                if (obj.InsertYn == null || obj.InsertYn == "E" || obj.InsertYn == "T" || obj.InsertYn == "H")
                {
                    var status = sendingLastReport(obj);

                    if (status == "E") // 에러 발생
                    {
                        if (obj.InsertYn == "E")
                        {
                            obj.InsertYn = "T";
                        }
                        else if (obj.InsertYn == "T")
                        {
                            obj.InsertYn = "H";
                        }else if(obj.InsertYn == null)
                        {
                            obj.InsertYn = "T";
                        }
                    }else if(status == "S")
                    {
                        obj.InsertYn = "S";
                    }

                    _tcmsIfLastReportService.SaveDbContext();
                }
            }
            return "S";
        }

        public string ResendSatisfaction()
        {
            var tcmsIfSurveyObj = _tcmsIfSurveyService.unAsyncGetTcmsIfSurvey();

            foreach (var obj in tcmsIfSurveyObj)
            {
                if (obj.InsertYn == null || obj.InsertYn == "E" || obj.InsertYn == "T" || obj.InsertYn == "H")
                {
                    var status = sendSatisfaction(obj);

                    if (status == "E")
                    {
                        if (obj.InsertYn == "E")
                        {
                            obj.InsertYn = "T";
                        }
                        else if (obj.InsertYn == "T")
                        {
                            obj.InsertYn = "H";
                        }
                        else if (obj.InsertYn == null)
                        {
                            obj.InsertYn = "T";
                        }
                    }
                    else if (status == "S")
                    {
                        obj.InsertYn = "S";
                    }

                    _tcmsIfLastReportService.SaveDbContext();
                }
            }
            return "S";
        }

        public string sendingLastReport(TcmsIfLastReport tcmsIfLastReport)
        {
            HttpContext.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            string result = "";
            string backSlash = "";

            string rdt = String.Format("{0:yyyy-MM-dd hh:mm:ss}", tcmsIfLastReport.RegDt);
            string idt = String.Format("{0:yyyy-MM-dd hh:mm:ss}", tcmsIfLastReport.InfDt);

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

            var file1 = tcmsIfLastReport.File1;
            var file2 = tcmsIfLastReport.File2;
            var file3 = tcmsIfLastReport.File3;
            var file4 = tcmsIfLastReport.File4;
            var file5 = tcmsIfLastReport.File5;


            if (tcmsIfLastReport.File1 != null)
            {

                file1 = tcmsIfLastReport.File1.Replace("\\", "/");

            }

            if (tcmsIfLastReport.File2 != null)
            {

                file2 = tcmsIfLastReport.File2.Replace("\\", "/");

            }

            if (tcmsIfLastReport.File3 != null)
            {

                file3 = tcmsIfLastReport.File3.Replace("\\", "/");

            }

            if (tcmsIfLastReport.File4 != null)
            {

                file4 = tcmsIfLastReport.File4.Replace("\\", "/");

            }

            if (tcmsIfLastReport.File5 != null)
            {

                file5 = tcmsIfLastReport.File5.Replace("\\", "/");

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

                    File1 = file1,
                    File2 = file2,
                    File3 = file3,
                    File4 = file4,
                    File5 = file5,

                    regDt = rdt,
                    InfDt = idt
                });
                backSlash = jsont.Replace("\\", "");
                byte[] ba = Encoding.UTF8.GetBytes("json=" + backSlash);

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
                    string[] rstSplit = result.Split('\n');
                    statusModel = (StatusModel)js.Deserialize(rstSplit[1], typeof(StatusModel));
                }
                return statusModel.status;

            }
            catch (Exception e)
            {
                return "E";
            }
            
        }

        public string sendSatisfaction(TcmsIfSurvey tcmsIfSurvey)
        {
            HttpContext.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            string result = "";

            StatusModel statusModel = new StatusModel();

            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://tcms.igarim.com/Api/tcms_if_survey.php");
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
                backSlash = jsont.Replace("\\", "");
                byte[] ba = Encoding.UTF8.GetBytes("json=" + backSlash);

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
    }
}