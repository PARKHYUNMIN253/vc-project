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
    // M_SERVICE_DOMAIN
    internal partial class MServiceDomainConfiguration : EntityTypeConfiguration<MServiceDomain>
    {
        public MServiceDomainConfiguration()
            : this("dbo")
        {
        }
 
        public MServiceDomainConfiguration(string schema)
        {
            ToTable(schema + ".M_SERVICE_DOMAIN");
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName("ID").IsRequired().HasColumnType("int").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.DomainName).HasColumnName("DOMAIN_NAME").IsRequired().HasColumnType("nvarchar").HasMaxLength(50);
            Property(x => x.MainImage).HasColumnName("MAIN_IMAGE").IsOptional().HasColumnType("nvarchar").HasMaxLength(50);
            Property(x => x.Prefix).HasColumnName("PREFIX").IsRequired().HasColumnType("nvarchar").HasMaxLength(50);
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
