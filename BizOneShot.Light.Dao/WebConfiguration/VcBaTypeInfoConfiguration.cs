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
    // VC_BA_TYPE_INFO
    internal partial class VcBaTypeInfoConfiguration : EntityTypeConfiguration<VcBaTypeInfo>
    {
        public VcBaTypeInfoConfiguration()
            : this("dbo")
        {
        }
 
        public VcBaTypeInfoConfiguration(string schema)
        {
            ToTable(schema + ".VC_BA_TYPE_INFO");
            HasKey(x => new { x.TypeBa, x.TypeName });

            Property(x => x.TypeBa).HasColumnName("TYPE_BA").IsRequired().IsFixedLength().IsUnicode(false).HasColumnType("char").HasMaxLength(2);
            Property(x => x.TypeYn).HasColumnName("TYPE_YN").IsOptional().IsFixedLength().IsUnicode(false).HasColumnType("char").HasMaxLength(1);
            Property(x => x.TypeName).HasColumnName("TYPE_NAME").IsRequired().IsUnicode(false).HasColumnType("varchar").HasMaxLength(25);
            Property(x => x.RegDt).HasColumnName("REG_DT").IsOptional().IsUnicode(false).HasColumnType("varchar").HasMaxLength(25);
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
