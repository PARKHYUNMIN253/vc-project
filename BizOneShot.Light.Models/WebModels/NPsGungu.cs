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
    // N_PS_GUNGU
    public class NPsGungu
    {
        public int GunguId { get; set; } // GUNGU_ID
        public string Sido { get; set; } // SIDO
        public string SidoEn { get; set; } // SIDO_EN
        public string Gungu { get; set; } // GUNGU
        public string GunguEn { get; set; } // GUNGU_EN
    }

}
