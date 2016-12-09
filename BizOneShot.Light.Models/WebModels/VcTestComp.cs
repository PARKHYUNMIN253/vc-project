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
    // VC_TEST_COMP
    public class VcTestComp
    {
        public int CompSn { get; set; } // COMP_SN (Primary key)
        public string RegistrationNo { get; set; } // REGISTRATION_NO
        public string CompNm { get; set; } // COMP_NM
        public string OwnEmail { get; set; } // OWN_EMAIL
        public string OwnTelNo { get; set; } // OWN_TEL_NO
        public string OwnNm { get; set; } // OWN_NM
        public DateTime? RegDt { get; set; } // REG_DT
        public DateTime? UpdDt { get; set; } // UPD_DT
        public string LoginId { get; set; } // LOGIN_ID
        public int TcmsLoginKey { get; set; } // TCMS_LOGIN_KEY
    }

}
