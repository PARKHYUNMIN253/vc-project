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
    // database_firewall_rules
    public class sys_DatabaseFirewallRule
    {
        public int Id { get; set; } // id
        public string Name { get; set; } // name
        public string StartIpAddress { get; set; } // start_ip_address
        public string EndIpAddress { get; set; } // end_ip_address
        public DateTime CreateDate { get; set; } // create_date
        public DateTime ModifyDate { get; set; } // modify_date
    }

}
