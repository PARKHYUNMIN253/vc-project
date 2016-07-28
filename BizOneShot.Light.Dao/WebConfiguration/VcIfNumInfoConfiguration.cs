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
    // VC_IF_NUM_INFO
    internal partial class VcIfNumInfoConfiguration : EntityTypeConfiguration<VcIfNumInfo>
    {
        public VcIfNumInfoConfiguration()
            : this("dbo")
        {
        }
 
        public VcIfNumInfoConfiguration(string schema)
        {
            ToTable(schema + ".VC_IF_NUM_INFO");
            HasKey(x => x.InfId);

            Property(x => x.InfId).HasColumnName("INF_ID").IsRequired().IsUnicode(false).HasColumnType("varchar").HasMaxLength(25);
            Property(x => x.BizWorkNm).HasColumnName("BIZ_WORK_NM").IsOptional().HasColumnType("nvarchar").HasMaxLength(50);
            Property(x => x.BizWorkSummary).HasColumnName("BIZ_WORK_SUMMARY").IsOptional().HasColumnType("nvarchar").HasMaxLength(1000);
            Property(x => x.BizStDt).HasColumnName("BIZ_ST_DT").IsOptional().HasColumnType("datetime");
            Property(x => x.BizEdDt).HasColumnName("BIZ_ED_DT").IsOptional().HasColumnType("datetime");
            Property(x => x.InfDt).HasColumnName("INF_DT").IsOptional().HasColumnType("datetime");
            Property(x => x.InsertYn).HasColumnName("INSERT_YN").IsOptional().IsFixedLength().IsUnicode(false).HasColumnType("char").HasMaxLength(1);
            Property(x => x.InsertStatus).HasColumnName("INSERT_STATUS").IsOptional().IsFixedLength().IsUnicode(false).HasColumnType("char").HasMaxLength(1);
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
