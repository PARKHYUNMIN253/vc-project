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

        // GET: Resend
        public ActionResult Index()
        {
            return View();
        }

        public ResendController(ITcmsIfLastReportService _tcmsIfLastReportService)
        {
            this._tcmsIfLastReportService = _tcmsIfLastReportService;
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
                if (obj.InsertYn == null || obj.InsertYn == "E" || obj.InsertYn == "E2" || obj.InsertYn == "E3")
                {
                    var status = sendingLastReport(obj);

                    if (status == "E") // 에러 발생
                    {
                        if (obj.InsertYn == "E")
                        {
                            obj.InsertYn = "E2";
                        }
                        else if (obj.InsertYn == "E2")
                        {
                            obj.InsertYn = "E3";
                        }
                    }
                    _tcmsIfLastReportService.SaveDbContext();
                }
            }
            return "S";
        }

        public string resendSurvey()
        {
            return "S";
        }

        public string sendingLastReport(TcmsIfLastReport tcmsIfLastReport)
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
                byte[] ba = Encoding.UTF8.GetBytes("json=" + backSlash);

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
    }
}