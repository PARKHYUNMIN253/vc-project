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
    // SC_REQ_DOC
    [GeneratedCodeAttribute("EF.Reverse.POCO.Generator", "2.15.1.0")]
    public class ScReqDoc
    {
        public int ReqDocSn { get; set; } // REQ_DOC_SN (Primary key)
        public string SenderId { get; set; } // SENDER_ID
        public string ReceiverId { get; set; } // RECEIVER_ID
        public string Status { get; set; } // STATUS
        public string ChkYn { get; set; } // CHK_YN
        public DateTime? ReqDt { get; set; } // REQ_DT
        public string ReqSubject { get; set; } // REQ_SUBJECT
        public string ReqContents { get; set; } // REQ_CONTENTS
        public DateTime? ResDt { get; set; } // RES_DT
        public string ResContents { get; set; } // RES_CONTENTS

        // Reverse navigation
        public virtual ICollection<ScReqDocFile> ScReqDocFiles { get; set; } // SC_REQ_DOC_FILE.FK_SC_REQ_DOC_TO_SC_REQ_DOC_FILE

        // Foreign keys
        public virtual VcUsrInfo VcUsrInfo_ReceiverId { get; set; } // FK_SC_USR_TO_SC_REQ_DOC2
        public virtual VcUsrInfo VcUsrInfo_SenderId { get; set; } // FK_SC_USR_TO_SC_REQ_DOC
        
        public ScReqDoc()
        {
            ScReqDocFiles = new List<ScReqDocFile>();
        }
    }

}
