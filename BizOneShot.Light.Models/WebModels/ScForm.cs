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
    // SC_FORM
    [GeneratedCodeAttribute("EF.Reverse.POCO.Generator", "2.15.1.0")]
    public class ScForm
    {
        public int FormSn { get; set; } // FORM_SN (Primary key)
        public string Subject { get; set; } // SUBJECT
        public string Contents { get; set; } // CONTENTS
        public string FormType { get; set; } // FORM_TYPE
        public string Status { get; set; } // STATUS
        public string RegId { get; set; } // REG_ID
        public DateTime? RegDt { get; set; } // REG_DT
        public string UpdId { get; set; } // UPD_ID
        public DateTime? UpdDt { get; set; } // UPD_DT

        // Reverse navigation
        public virtual ICollection<ScFormFile> ScFormFiles { get; set; } // SC_FORM_FILE.FK_SC_FORM_TO_SC_FORM_FILE
        
        public ScForm()
        {
            ScFormFiles = new List<ScFormFile>();
        }
    }

}
