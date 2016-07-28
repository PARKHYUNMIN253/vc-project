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
    // SC_QCL
    [GeneratedCodeAttribute("EF.Reverse.POCO.Generator", "2.15.1.0")]
    public class ScQcl
    {
        public int QclSn { get; set; } // QCL_SN (Primary key)
        public string QclType { get; set; } // QCL_TYPE
        public string QclNm { get; set; } // QCL_NM
        public string Status { get; set; } // STATUS
        public int DspOdr { get; set; } // DSP_ODR
        public string RegId { get; set; } // REG_ID
        public DateTime? RegDt { get; set; } // REG_DT
        public string UpdId { get; set; } // UPD_ID
        public DateTime? UpdDt { get; set; } // UPD_DT

        // Reverse navigation
        public virtual ICollection<ScFaq> ScFaqs { get; set; } // SC_FAQ.FK_SC_QCL_TO_SC_FAQ
        
        public ScQcl()
        {
            ScFaqs = new List<ScFaq>();
        }
    }

}
