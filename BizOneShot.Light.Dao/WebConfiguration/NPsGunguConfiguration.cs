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
    // N_PS_GUNGU
    internal partial class NPsGunguConfiguration : EntityTypeConfiguration<NPsGungu>
    {
        public NPsGunguConfiguration()
            : this("dbo")
        {
        }
 
        public NPsGunguConfiguration(string schema)
        {
            ToTable(schema + ".N_PS_GUNGU");
            HasKey(x => x.GunguId);

            Property(x => x.GunguId).HasColumnName("GUNGU_ID").IsRequired().HasColumnType("int");
            Property(x => x.Sido).HasColumnName("SIDO").IsOptional().HasColumnType("nvarchar").HasMaxLength(30);
            Property(x => x.SidoEn).HasColumnName("SIDO_EN").IsOptional().HasColumnType("nvarchar").HasMaxLength(30);
            Property(x => x.Gungu).HasColumnName("GUNGU").IsOptional().HasColumnType("nvarchar").HasMaxLength(30);
            Property(x => x.GunguEn).HasColumnName("GUNGU_EN").IsOptional().HasColumnType("nvarchar").HasMaxLength(30);
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
