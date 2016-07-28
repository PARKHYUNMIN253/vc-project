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
    // VC_TCMS_INFO
    public class VcTcmsInfo
    {
        public int TcmsSn { get; set; } // TCMS_SN (Primary key)
        public int TcmsLoginKey { get; set; } // TCMS_LOGIN_KEY
        public string TypeYn { get; set; } // TYPE_YN
        public DateTime? RegDt { get; set; } // REG_DT
    }

}
