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
    // TCMS_IF_LAST_REPORT
    public class TcmsIfLastReport
    {
        public string InfId { get; set; } // INF_ID (Primary key)
        public int? CompLoginKey { get; set; } // COMP_LOGIN_KEY
        public int? BaLoginKey { get; set; } // BA_LOGIN_KEY
        public int? MentorLoginKey { get; set; } // MENTOR_LOGIN_KEY
        public string NumSn { get; set; } // NUM_SN
        public string SubNumSn { get; set; } // SUB_NUM_SN
        public string ConCode { get; set; } // CON_CODE
        public string File1 { get; set; } // FILE_1
        public string File2 { get; set; } // FILE_2
        public string File3 { get; set; } // FILE_3
        public string File4 { get; set; } // FILE_4
        public DateTime? RegDt { get; set; } // REG_DT
        public DateTime? InfDt { get; set; } // INF_DT
        public string File5 { get; set; } // FILE_5
        public string InsertYn { get; set; } // INSERT_YN
    }

}
