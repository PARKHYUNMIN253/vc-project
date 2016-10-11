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
using System.Collections.ObjectModel;
using System.Linq;
using System.Data.Entity.Infrastructure;
using System.Linq.Expressions;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using BizOneShot.Light.Models.WebModels;
using System.Threading;
using DatabaseGeneratedOption = System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption;

namespace BizOneShot.Light.Dao.WebConfiguration
{
    // database_firewall_rules
    internal partial class sys_DatabaseFirewallRuleConfiguration : EntityTypeConfiguration<sys_DatabaseFirewallRule>
    {
        public sys_DatabaseFirewallRuleConfiguration()
            : this("sys")
        {
        }
 
        public sys_DatabaseFirewallRuleConfiguration(string schema)
        {
            ToTable(schema + ".database_firewall_rules");
            HasKey(x => new { x.Id, x.Name, x.StartIpAddress, x.EndIpAddress, x.CreateDate, x.ModifyDate });

            Property(x => x.Id).HasColumnName("id").IsRequired().HasColumnType("int").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Name).HasColumnName("name").IsRequired().HasColumnType("nvarchar").HasMaxLength(128);
            Property(x => x.StartIpAddress).HasColumnName("start_ip_address").IsRequired().IsUnicode(false).HasColumnType("varchar").HasMaxLength(45);
            Property(x => x.EndIpAddress).HasColumnName("end_ip_address").IsRequired().IsUnicode(false).HasColumnType("varchar").HasMaxLength(45);
            Property(x => x.CreateDate).HasColumnName("create_date").IsRequired().HasColumnType("datetime");
            Property(x => x.ModifyDate).HasColumnName("modify_date").IsRequired().HasColumnType("datetime");
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
