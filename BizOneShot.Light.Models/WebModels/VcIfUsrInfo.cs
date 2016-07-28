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
    // VC_IF_USR_INFO
    public class VcIfUsrInfo
    {
        public string InfId { get; set; } // INF_ID
        public string LoginId { get; set; } // LOGIN_ID
        public int TcmsLoginKey { get; set; } // TCMS_LOGIN_KEY
        public string UsrType { get; set; } // USR_TYPE
        public string TypeYn { get; set; } // TYPE_YN
        public string Name { get; set; } // NAME
        public string Email { get; set; } // EMAIL
        public string TelNo { get; set; } // TEL_NO
        public string MbNo { get; set; } // MB_NO
        public string PostNo { get; set; } // POST_NO
        public string Addr1 { get; set; } // ADDR_1
        public string FaxNo { get; set; } // FAX_NO
        public DateTime? InfDt { get; set; } // INF_DT
        public string InsertYn { get; set; } // INSERT_YN
        public string InsertStatus { get; set; } // INSERT_STATUS
    }

}
