// ReSharper disable RedundantUsingDirective
// ReSharper disable DoNotCallOverridableMethodsInConstructor
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable PartialMethodWithSinglePart
// ReSharper disable RedundantNameQualifier
// TargetFrameworkVersion = 4.51
#pragma warning disable 1591    //  Ignore "Missing XML Comment" warning

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Threading;

namespace BizOneShot.Light.Models.WebModels
{
    // CT_WEB_LOG
    public class CtWebLog
    {
        public int LogId { get; set; } // LOG_ID (Primary key)
        public string SrvCd { get; set; } // SRV_CD
        public string SvrIp { get; set; } // SVR_IP
        public string RmkTxt { get; set; } // RMK_TXT
        public string Line { get; set; } // LINE
        public string LoginId { get; set; } // LOGIN_ID
        public string UsrAgn { get; set; } // USR_AGN
        public string FileNm { get; set; } // FILE_NM
        public DateTime? RegDt { get; set; } // REG_DT
    }

}
