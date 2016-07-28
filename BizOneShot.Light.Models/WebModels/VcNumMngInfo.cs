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
    // VC_NUM_MNG_INFO
    public class VcNumMngInfo
    {
        public string NumSn { get; set; } // NUM_SN
        public string BizWorkNm { get; set; } // BIZ_WORK_NM
        public string BizWorkSummary { get; set; } // BIZ_WORK_SUMMARY
        public DateTime? BizStDt { get; set; } // BIZ_ST_DT
        public DateTime? BizEdDt { get; set; } // BIZ_ED_DT
    }

}
