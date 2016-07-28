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
    // VC_MENTOR_INFO
    internal partial class VcMentorInfoConfiguration : EntityTypeConfiguration<VcMentorInfo>
    {
        public VcMentorInfoConfiguration()
            : this("dbo")
        {
        }
 
        public VcMentorInfoConfiguration(string schema)
        {
            ToTable(schema + ".VC_MENTOR_INFO");
            HasKey(x => x.MentorSn);

            Property(x => x.MentorSn).HasColumnName("MENTOR_SN").IsRequired().HasColumnType("int").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.BaSn).HasColumnName("BA_SN").IsRequired().HasColumnType("int");
            Property(x => x.TcmsLoginKey).HasColumnName("TCMS_LOGIN_KEY").IsRequired().HasColumnType("int");
            Property(x => x.RegDt).HasColumnName("REG_DT").IsOptional().HasColumnType("datetime");
            Property(x => x.UpdDt).HasColumnName("UPD_DT").IsOptional().HasColumnType("datetime");
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
