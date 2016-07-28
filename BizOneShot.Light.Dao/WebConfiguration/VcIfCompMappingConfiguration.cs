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
    // VC_IF_COMP_MAPPING
    internal partial class VcIfCompMappingConfiguration : EntityTypeConfiguration<VcIfCompMapping>
    {
        public VcIfCompMappingConfiguration()
            : this("dbo")
        {
        }
 
        public VcIfCompMappingConfiguration(string schema)
        {
            ToTable(schema + ".VC_IF_COMP_MAPPING");
            HasKey(x => new { x.InfId, x.CompLoginKey, x.BaLoginKey, x.NumSn, x.SubNumSn });

            Property(x => x.InfId).HasColumnName("INF_ID").IsRequired().IsUnicode(false).HasColumnType("varchar").HasMaxLength(25);
            Property(x => x.CompLoginKey).HasColumnName("COMP_LOGIN_KEY").IsRequired().HasColumnType("int");
            Property(x => x.BaLoginKey).HasColumnName("BA_LOGIN_KEY").IsRequired().HasColumnType("int");
            Property(x => x.NumSn).HasColumnName("NUM_SN").IsRequired().HasColumnType("nvarchar").HasMaxLength(3);
            Property(x => x.SubNumSn).HasColumnName("SUB_NUM_SN").IsRequired().HasColumnType("nvarchar").HasMaxLength(2);
            Property(x => x.WriteYn).HasColumnName("WRITE_YN").IsOptional().IsFixedLength().IsUnicode(false).HasColumnType("char").HasMaxLength(1);
            Property(x => x.ConCode).HasColumnName("CON_CODE").IsOptional().HasColumnType("nvarchar").HasMaxLength(5);
            Property(x => x.InfDt).HasColumnName("INF_DT").IsOptional().HasColumnType("datetime");
            Property(x => x.InsertYn).HasColumnName("INSERT_YN").IsOptional().IsFixedLength().IsUnicode(false).HasColumnType("char").HasMaxLength(1);
            Property(x => x.InsertStatus).HasColumnName("INSERT_STATUS").IsOptional().IsFixedLength().IsUnicode(false).HasColumnType("char").HasMaxLength(1);
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
