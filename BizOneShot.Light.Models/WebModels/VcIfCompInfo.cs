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
    // VC_IF_COMP_INFO
    public class VcIfCompInfo
    {
        public string InfId { get; set; } // INF_ID
        public int TcmsLoginKey { get; set; } // TCMS_LOGIN_KEY
        public string RegistrationSn { get; set; } // REGISTRATION_SN
        public string CompNm { get; set; } // COMP_NM
        public string OwnNm { get; set; } // OWN_NM
        public string OwnEmail { get; set; } // OWN_EMAIL
        public string OwnTelNo { get; set; } // OWN_TEL_NO
        public DateTime? InfDt { get; set; } // INF_DT
        public string InsertYn { get; set; } // INSERT_YN
        public string InsertStatus { get; set; } // INSERT_STATUS
    }

}
