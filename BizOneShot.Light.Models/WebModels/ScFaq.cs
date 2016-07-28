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
    // SC_FAQ
    public class ScFaq
    {
        public int FaqSn { get; set; } // FAQ_SN (Primary key)
        public int? QclSn { get; set; } // QCL_SN
        public string QstTxt { get; set; } // QST_TXT
        public string AnsTxt { get; set; } // ANS_TXT
        public string Stat { get; set; } // STAT
        public string RegId { get; set; } // REG_ID
        public DateTime? RegDt { get; set; } // REG_DT
        public string UpdId { get; set; } // UPD_ID
        public DateTime? UpdDt { get; set; } // UPD_DT

        // Foreign keys
        public virtual ScQcl ScQcl { get; set; } // FK_SC_QCL_TO_SC_FAQ
    }

}
