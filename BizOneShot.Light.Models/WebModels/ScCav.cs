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
    // SC_CAV
    public class ScCav
    {
        public int CavDt { get; set; } // CAV_DT (Primary key)
        public decimal CavOp { get; set; } // CAV_OP
        public decimal CavRe { get; set; } // CAV_RE
        public decimal CavSg { get; set; } // CAV_SG
        public decimal CavNg { get; set; } // CAV_NG
        public decimal CavQr { get; set; } // CAV_QR
        public decimal CavCr { get; set; } // CAV_CR
        public decimal CavDebt { get; set; } // CAV_DEBT
        public decimal CavIcr { get; set; } // CAV_ICR
        public decimal CavTat { get; set; } // CAV_TAT
        public decimal CavTrt { get; set; } // CAV_TRT
        public decimal CavIt { get; set; } // CAV_IT
        public decimal CavVr { get; set; } // CAV_VR
        public int CavLp { get; set; } // CAV_LP
        public decimal CavCp { get; set; } // CAV_CP
        public decimal CavSmc { get; set; } // CAV_SMC
        public string RegId { get; set; } // REG_ID
        public DateTime? RegDt { get; set; } // REG_DT
        public string UpdId { get; set; } // UPD_ID
        public DateTime? UpdDt { get; set; } // UPD_DT
    }

}
