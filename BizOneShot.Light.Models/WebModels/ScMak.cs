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
    // SC_MAK
    public class ScMak
    {
        public int MakDt { get; set; } // MAK_DT (Primary key)
        public int MaksCa { get; set; } // MAKS_CA
        public int MaksMp { get; set; } // MAKS_MP
        public int MaksTd { get; set; } // MAKS_TD
        public int MaksPs { get; set; } // MAKS_PS
        public int MaksPro { get; set; } // MAKS_PRO
        public int MaksPb { get; set; } // MAKS_PB
        public int MaksMak { get; set; } // MAKS_MAK
        public int MaksCc { get; set; } // MAKS_CC
        public int MakslCa { get; set; } // MAKSL_CA
        public int MakslMp { get; set; } // MAKSL_MP
        public int MakslTd { get; set; } // MAKSL_TD
        public int MakslPs { get; set; } // MAKSL_PS
        public int MakslPro { get; set; } // MAKSL_PRO
        public int MakslPb { get; set; } // MAKSL_PB
        public int MakslMak { get; set; } // MAKSL_MAK
        public int MakslCc { get; set; } // MAKSL_CC
        public int MaksmCa { get; set; } // MAKSM_CA
        public int MaksmMp { get; set; } // MAKSM_MP
        public int MaksmTd { get; set; } // MAKSM_TD
        public int MaksmPs { get; set; } // MAKSM_PS
        public int MaksmPro { get; set; } // MAKSM_PRO
        public int MaksmPb { get; set; } // MAKSM_PB
        public int MaksmMak { get; set; } // MAKSM_MAK
        public int MaksmCc { get; set; } // MAKSM_CC
        public string RegId { get; set; } // REG_ID
        public DateTime? RegDt { get; set; } // REG_DT
        public string UpdId { get; set; } // UPD_ID
        public DateTime? UpdDt { get; set; } // UPD_DT
    }

}
