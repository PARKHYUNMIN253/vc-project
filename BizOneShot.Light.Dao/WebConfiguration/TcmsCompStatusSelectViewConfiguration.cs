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
    // TCMS_COMP_STATUS_SELECT_VIEW
    internal partial class TcmsCompStatusSelectViewConfiguration : EntityTypeConfiguration<TcmsCompStatusSelectView>
    {
        public TcmsCompStatusSelectViewConfiguration()
            : this("dbo")
        {
        }
 
        public TcmsCompStatusSelectViewConfiguration(string schema)
        {
            ToTable(schema + ".TCMS_COMP_STATUS_SELECT_VIEW");
            HasKey(x => new { x.CpMpCode, x.CompSn, x.BaSn, x.NumSn });

            Property(x => x.Id).HasColumnName("ID").IsOptional().HasColumnType("int");
            Property(x => x.CpMpCode).HasColumnName("CP_MP_CODE").IsRequired().HasColumnType("nvarchar").HasMaxLength(26);
            Property(x => x.CompSn).HasColumnName("COMP_SN").IsRequired().HasColumnType("int");
            Property(x => x.BaSn).HasColumnName("BA_SN").IsRequired().HasColumnType("int");
            Property(x => x.CompNm).HasColumnName("COMP_NM").IsOptional().HasColumnType("nvarchar").HasMaxLength(70);
            Property(x => x.BaNm).HasColumnName("BA_NM").IsOptional().HasColumnType("nvarchar").HasMaxLength(70);
            Property(x => x.WriteYn).HasColumnName("WRITE_YN").IsOptional().IsFixedLength().IsUnicode(false).HasColumnType("char").HasMaxLength(1);
            Property(x => x.RegistrationNo).HasColumnName("REGISTRATION_NO").IsOptional().IsUnicode(false).HasColumnType("varchar").HasMaxLength(10);
            Property(x => x.OwnNm).HasColumnName("OWN_NM").IsOptional().IsUnicode(false).HasColumnType("varchar").HasMaxLength(50);
            Property(x => x.UsrType).HasColumnName("USR_TYPE").IsOptional().IsFixedLength().IsUnicode(false).HasColumnType("char").HasMaxLength(1);
            Property(x => x.ConCode).HasColumnName("CON_CODE").IsOptional().HasColumnType("nvarchar").HasMaxLength(5);
            Property(x => x.QuestionSn).HasColumnName("QUESTION_SN").IsOptional().HasColumnType("int");
            Property(x => x.QStatus).HasColumnName("Q_STATUS").IsOptional().IsFixedLength().IsUnicode(false).HasColumnType("char").HasMaxLength(1);
            Property(x => x.RStatus).HasColumnName("R_STATUS").IsOptional().IsFixedLength().IsUnicode(false).HasColumnType("char").HasMaxLength(1);
            Property(x => x.BasicYear).HasColumnName("BASIC_YEAR").IsOptional().HasColumnType("int");
            Property(x => x.NumSn).HasColumnName("NUM_SN").IsRequired().HasColumnType("nvarchar").HasMaxLength(3);
            Property(x => x.SubNumSn).HasColumnName("SUB_NUM_SN").IsOptional().HasColumnType("nvarchar").HasMaxLength(2);
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
