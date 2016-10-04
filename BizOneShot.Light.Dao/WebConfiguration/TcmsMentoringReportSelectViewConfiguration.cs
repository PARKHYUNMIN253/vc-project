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
    // TCMS_MENTORING_REPORT_SELECT_VIEW
    internal partial class TcmsMentoringReportSelectViewConfiguration : EntityTypeConfiguration<TcmsMentoringReportSelectView>
    {
        public TcmsMentoringReportSelectViewConfiguration()
            : this("dbo")
        {
        }
 
        public TcmsMentoringReportSelectViewConfiguration(string schema)
        {
            ToTable(schema + ".TCMS_MENTORING_REPORT_SELECT_VIEW");
            HasKey(x => new { x.MentorSn, x.BaSn, x.MentorKey, x.ReportSn, x.BizWorkSn });

            Property(x => x.Idx).HasColumnName("idx").IsOptional().HasColumnType("int");
            Property(x => x.BaNm).HasColumnName("ba_nm").IsOptional().HasColumnType("nvarchar").HasMaxLength(70);
            Property(x => x.MentorSn).HasColumnName("mentor_sn").IsRequired().HasColumnType("int");
            Property(x => x.BaSn).HasColumnName("ba_sn").IsRequired().HasColumnType("int");
            Property(x => x.MentorName).HasColumnName("mentor_name").IsOptional().IsUnicode(false).HasColumnType("varchar").HasMaxLength(50);
            Property(x => x.MentorKey).HasColumnName("mentor_key").IsRequired().HasColumnType("int");
            Property(x => x.CompNm).HasColumnName("comp_nm").IsOptional().HasColumnType("nvarchar").HasMaxLength(70);
            Property(x => x.ReportSn).HasColumnName("REPORT_SN").IsRequired().HasColumnType("int");
            Property(x => x.BizWorkSn).HasColumnName("BIZ_WORK_SN").IsRequired().HasColumnType("int");
            Property(x => x.MentorId).HasColumnName("MENTOR_ID").IsOptional().IsUnicode(false).HasColumnType("varchar").HasMaxLength(25);
            Property(x => x.CompSn).HasColumnName("COMP_SN").IsOptional().HasColumnType("int");
            Property(x => x.MentoringDt).HasColumnName("MENTORING_DT").IsOptional().HasColumnType("datetime");
            Property(x => x.MentoringStHr).HasColumnName("MENTORING_ST_HR").IsOptional().IsUnicode(false).HasColumnType("varchar").HasMaxLength(5);
            Property(x => x.MentoringEdHr).HasColumnName("MENTORING_ED_HR").IsOptional().IsUnicode(false).HasColumnType("varchar").HasMaxLength(5);
            Property(x => x.MentoringPlace).HasColumnName("MENTORING_PLACE").IsOptional().HasColumnType("nvarchar").HasMaxLength(200);
            Property(x => x.Attendee).HasColumnName("ATTENDEE").IsOptional().HasColumnType("nvarchar").HasMaxLength(200);
            Property(x => x.MentorAreaCd).HasColumnName("MENTOR_AREA_CD").IsOptional().IsFixedLength().IsUnicode(false).HasColumnType("char").HasMaxLength(1);
            Property(x => x.MentoringSubject).HasColumnName("MENTORING_SUBJECT").IsOptional().HasColumnType("nvarchar").HasMaxLength(1000);
            Property(x => x.MentoringContents).HasColumnName("MENTORING_CONTENTS").IsOptional().HasColumnType("nvarchar").HasMaxLength(2000);
            Property(x => x.SubmitDt).HasColumnName("SUBMIT_DT").IsOptional().HasColumnType("datetime");
            Property(x => x.Status).HasColumnName("STATUS").IsOptional().IsFixedLength().IsUnicode(false).HasColumnType("char").HasMaxLength(1);
            Property(x => x.RegId).HasColumnName("REG_ID").IsOptional().IsUnicode(false).HasColumnType("varchar").HasMaxLength(25);
            Property(x => x.RegDt).HasColumnName("REG_DT").IsOptional().HasColumnType("datetime");
            Property(x => x.UpdId).HasColumnName("UPD_ID").IsOptional().IsUnicode(false).HasColumnType("varchar").HasMaxLength(25);
            Property(x => x.UpdDt).HasColumnName("UPD_DT").IsOptional().HasColumnType("datetime");
            Property(x => x.NumSn).HasColumnName("NUM_SN").IsOptional().HasColumnType("nvarchar").HasMaxLength(4);
            Property(x => x.SubNumSn).HasColumnName("SUB_NUM_SN").IsOptional().HasColumnType("nvarchar").HasMaxLength(2);
            Property(x => x.ConCode).HasColumnName("CON_CODE").IsOptional().HasColumnType("nvarchar").HasMaxLength(5);
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
