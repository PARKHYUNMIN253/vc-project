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
    // VC_IF_TCMS_INFO
    internal partial class VcIfTcmsInfoConfiguration : EntityTypeConfiguration<VcIfTcmsInfo>
    {
        public VcIfTcmsInfoConfiguration()
            : this("dbo")
        {
        }
 
        public VcIfTcmsInfoConfiguration(string schema)
        {
            ToTable(schema + ".VC_IF_TCMS_INFO");
            HasKey(x => new { x.InfId, x.TcmsLoginKey });

            Property(x => x.InfId).HasColumnName("INF_ID").IsRequired().IsUnicode(false).HasColumnType("varchar").HasMaxLength(25);
            Property(x => x.TcmsLoginKey).HasColumnName("TCMS_LOGIN_KEY").IsRequired().HasColumnType("int");
            Property(x => x.Name).HasColumnName("NAME").IsOptional().IsUnicode(false).HasColumnType("varchar").HasMaxLength(50);
            Property(x => x.Tel).HasColumnName("TEL").IsOptional().IsUnicode(false).HasColumnType("varchar").HasMaxLength(20);
            Property(x => x.InfDt).HasColumnName("INF_DT").IsOptional().HasColumnType("datetime");
            Property(x => x.InsertYn).HasColumnName("INSERT_YN").IsOptional().IsFixedLength().IsUnicode(false).HasColumnType("char").HasMaxLength(1);
            Property(x => x.InsertStatus).HasColumnName("INSERT_STATUS").IsOptional().IsFixedLength().IsUnicode(false).HasColumnType("char").HasMaxLength(1);
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
