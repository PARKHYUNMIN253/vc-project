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
    // SC_REQ_DOC_FILE
    public class ScReqDocFile
    {
        public int FileSn { get; set; } // FILE_SN (Primary key)
        public int ReqDocSn { get; set; } // REQ_DOC_SN
        public string RegType { get; set; } // REG_TYPE

        // Foreign keys
        public virtual ScFileInfo ScFileInfo { get; set; } // FK_SC_FILE_INFO_TO_SC_REQ_DOC_FILE
        public virtual ScReqDoc ScReqDoc { get; set; } // FK_SC_REQ_DOC_TO_SC_REQ_DOC_FILE
    }

}
