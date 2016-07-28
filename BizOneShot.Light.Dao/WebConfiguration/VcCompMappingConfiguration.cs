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
    // VC_COMP_MAPPING
    internal partial class VcCompMappingConfiguration : EntityTypeConfiguration<VcCompMapping>
    {
        public VcCompMappingConfiguration()
            : this("dbo")
        {
        }
 
        public VcCompMappingConfiguration(string schema)
        {
            ToTable(schema + ".VC_COMP_MAPPING");
            HasKey(x => new { x.CpMpCode, x.CompSn, x.BaSn, x.NumSn });

            Property(x => x.CpMpCode).HasColumnName("CP_MP_CODE").IsRequired().HasColumnType("nvarchar").HasMaxLength(26);
            Property(x => x.CompSn).HasColumnName("COMP_SN").IsRequired().HasColumnType("int");
            Property(x => x.BaSn).HasColumnName("BA_SN").IsRequired().HasColumnType("int");
            Property(x => x.NumSn).HasColumnName("NUM_SN").IsRequired().HasColumnType("nvarchar").HasMaxLength(3);
            Property(x => x.SubNumSn).HasColumnName("SUB_NUM_SN").IsOptional().HasColumnType("nvarchar").HasMaxLength(2);
            Property(x => x.ConCode).HasColumnName("CON_CODE").IsOptional().HasColumnType("nvarchar").HasMaxLength(5);
            Property(x => x.TypeYn).HasColumnName("TYPE_YN").IsOptional().IsFixedLength().IsUnicode(false).HasColumnType("char").HasMaxLength(1);
            Property(x => x.WriteYn).HasColumnName("WRITE_YN").IsOptional().IsFixedLength().IsUnicode(false).HasColumnType("char").HasMaxLength(1);
            Property(x => x.RegDt).HasColumnName("REG_DT").IsOptional().HasColumnType("datetime");
            Property(x => x.UpdDt).HasColumnName("UPD_DT").IsOptional().HasColumnType("datetime");
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
