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
    // RPT_FINANCE_COMMENT
    public class RptFinanceComment
    {
        public int CompSn { get; set; } // COMP_SN (Primary key)
        public string ExpertId { get; set; } // EXPERT_ID (Primary key)
        public int BizWorkSn { get; set; } // BIZ_WORK_SN
        public int BasicYear { get; set; } // BASIC_YEAR (Primary key)
        public int BasicMonth { get; set; } // BASIC_MONTH (Primary key)
        public string Comment { get; set; } // COMMENT
        public DateTime? WriteDt { get; set; } // WRITE_DT

        // Foreign keys
        public virtual VcCompInfo VcCompInfo { get; set; } // FK_SC_COMP_INFO_TO_RPT_FINANCE_COMMENT
        public virtual VcUsrInfo VcUsrInfo { get; set; } // FK_SC_USR_TO_RPT_FINANCE_COMMENT
    }

}
