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
    // VC_SAT_CHECK
    public class VcSatCheck
    {
        public int SatSn { get; set; } // SAT_SN (Primary key)
        public string Check01 { get; set; } // CHECK01
        public string Check02 { get; set; } // CHECK02
        public string Check03 { get; set; } // CHECK03
        public string Check04 { get; set; } // CHECK04
        public string Check05 { get; set; } // CHECK05
        public DateTime? RegDt { get; set; } // REG_DT
        public DateTime? UdpDt { get; set; } // UDP_DT
        public string Check06 { get; set; } // CHECK06
        public string Check07 { get; set; } // CHECK07
        public string Check08 { get; set; } // CHECK08
        public string Check09 { get; set; } // CHECK09
        public string Check10 { get; set; } // CHECK10
        public string Check11 { get; set; } // CHECK11
        public string Check12 { get; set; } // CHECK12
        public string Check13 { get; set; } // CHECK13
        public string Check14 { get; set; } // CHECK14
        public string Check15 { get; set; } // CHECK15
        public string Check16 { get; set; } // CHECK16
        public string Check17 { get; set; } // CHECK17
        public string Check18 { get; set; } // CHECK18
        public string Check19 { get; set; } // CHECK19
        public string Check20 { get; set; } // CHECK20
        public string Check21 { get; set; } // CHECK21
        public string Check22 { get; set; } // CHECK22
        public string Text01 { get; set; } // TEXT01
        public string Text02 { get; set; } // TEXT02
        public string Check23 { get; set; } // CHECK23
        public string Check24 { get; set; } // CHECK24
    }

}
