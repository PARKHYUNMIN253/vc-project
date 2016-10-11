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
    [GeneratedCodeAttribute("EF.Reverse.POCO.Generator", "2.15.1.0")]
    public class FakeWebDbContext : IWebDbContext
    {
        public DbSet<CtWebLog> CtWebLogs { get; set; }
        public DbSet<MServiceDomain> MServiceDomains { get; set; }
        public DbSet<NPsGungu> NPsGungus { get; set; }
        public DbSet<QuesCheckList> QuesCheckLists { get; set; }
        public DbSet<QuesCompExtention> QuesCompExtentions { get; set; }
        public DbSet<QuesCompHistory> QuesCompHistories { get; set; }
        public DbSet<QuesCompInfo> QuesCompInfoes { get; set; }
        public DbSet<QuesMaster> QuesMasters { get; set; }
        public DbSet<QuesOgranAnalysis> QuesOgranAnalysis { get; set; }
        public DbSet<QuesResult1> QuesResult1 { get; set; }
        public DbSet<QuesResult2> QuesResult2 { get; set; }
        public DbSet<QuesWriter> QuesWriters { get; set; }
        public DbSet<RptCheckList> RptCheckLists { get; set; }
        public DbSet<RptFinanceComment> RptFinanceComments { get; set; }
        public DbSet<RptMaster> RptMasters { get; set; }
        public DbSet<RptMentorCheck> RptMentorChecks { get; set; }
        public DbSet<RptMentorComment> RptMentorComments { get; set; }
        public DbSet<RptMentorRadio> RptMentorRadios { get; set; }
        public DbSet<RptMngCode> RptMngCodes { get; set; }
        public DbSet<RptMngComment> RptMngComments { get; set; }
        public DbSet<ScBizType> ScBizTypes { get; set; }
        public DbSet<ScCav> ScCavs { get; set; }
        public DbSet<ScCompanyFinance> ScCompanyFinances { get; set; }
        public DbSet<ScFaq> ScFaqs { get; set; }
        public DbSet<ScFileInfo> ScFileInfoes { get; set; }
        public DbSet<ScFinancialIndexT> ScFinancialIndexTs { get; set; }
        public DbSet<ScForm> ScForms { get; set; }
        public DbSet<ScFormFile> ScFormFiles { get; set; }
        public DbSet<ScMak> ScMaks { get; set; }
        public DbSet<ScMentoringFileInfo> ScMentoringFileInfoes { get; set; }
        public DbSet<ScMentoringReport> ScMentoringReports { get; set; }
        public DbSet<ScMentoringTotalReport> ScMentoringTotalReports { get; set; }
        public DbSet<ScMentoringTrFileInfo> ScMentoringTrFileInfoes { get; set; }
        public DbSet<ScNtc> ScNtcs { get; set; }
        public DbSet<ScQa> ScQas { get; set; }
        public DbSet<ScQcl> ScQcls { get; set; }
        public DbSet<ScReqDoc> ScReqDocs { get; set; }
        public DbSet<ScReqDocFile> ScReqDocFiles { get; set; }
        public DbSet<ScUsrResume> ScUsrResumes { get; set; }
        public DbSet<SyDareDbInfo> SyDareDbInfoes { get; set; }
        public DbSet<sys_DatabaseFirewallRule> sys_DatabaseFirewallRules { get; set; }
        public DbSet<TcmsCompStatusSelectView> TcmsCompStatusSelectViews { get; set; }
        public DbSet<TcmsIfLastReport> TcmsIfLastReports { get; set; }
        public DbSet<TcmsIfSurvey> TcmsIfSurveys { get; set; }
        public DbSet<TcmsMentoringReportSelectView> TcmsMentoringReportSelectViews { get; set; }
        public DbSet<VcBaInfo> VcBaInfoes { get; set; }
        public DbSet<VcBaTypeInfo> VcBaTypeInfoes { get; set; }
        public DbSet<VcBizWork> VcBizWorks { get; set; }
        public DbSet<VcCompInfo> VcCompInfoes { get; set; }
        public DbSet<VcCompMapping> VcCompMappings { get; set; }
        public DbSet<VcIfBaInfo> VcIfBaInfoes { get; set; }
        public DbSet<VcIfCompInfo> VcIfCompInfoes { get; set; }
        public DbSet<VcIfCompMapping> VcIfCompMappings { get; set; }
        public DbSet<VcIfMentorInfo> VcIfMentorInfoes { get; set; }
        public DbSet<VcIfNumInfo> VcIfNumInfoes { get; set; }
        public DbSet<VcIfQuesCompInfo> VcIfQuesCompInfoes { get; set; }
        public DbSet<VcIfTcmsInfo> VcIfTcmsInfoes { get; set; }
        public DbSet<VcIfUsrInfo> VcIfUsrInfoes { get; set; }
        public DbSet<VcLastReportNSat> VcLastReportNSats { get; set; }
        public DbSet<VcMentorInfo> VcMentorInfoes { get; set; }
        public DbSet<VcMentorMapping> VcMentorMappings { get; set; }
        public DbSet<VcNumMngInfo> VcNumMngInfoes { get; set; }
        public DbSet<VcSatCheck> VcSatChecks { get; set; }
        public DbSet<VcTcmsInfo> VcTcmsInfoes { get; set; }
        public DbSet<VcUsrInfo> VcUsrInfoes { get; set; }

        public FakeWebDbContext()
        {
            CtWebLogs = new FakeDbSet<CtWebLog>();
            MServiceDomains = new FakeDbSet<MServiceDomain>();
            NPsGungus = new FakeDbSet<NPsGungu>();
            QuesCheckLists = new FakeDbSet<QuesCheckList>();
            QuesCompExtentions = new FakeDbSet<QuesCompExtention>();
            QuesCompHistories = new FakeDbSet<QuesCompHistory>();
            QuesCompInfoes = new FakeDbSet<QuesCompInfo>();
            QuesMasters = new FakeDbSet<QuesMaster>();
            QuesOgranAnalysis = new FakeDbSet<QuesOgranAnalysis>();
            QuesResult1 = new FakeDbSet<QuesResult1>();
            QuesResult2 = new FakeDbSet<QuesResult2>();
            QuesWriters = new FakeDbSet<QuesWriter>();
            RptCheckLists = new FakeDbSet<RptCheckList>();
            RptFinanceComments = new FakeDbSet<RptFinanceComment>();
            RptMasters = new FakeDbSet<RptMaster>();
            RptMentorChecks = new FakeDbSet<RptMentorCheck>();
            RptMentorComments = new FakeDbSet<RptMentorComment>();
            RptMentorRadios = new FakeDbSet<RptMentorRadio>();
            RptMngCodes = new FakeDbSet<RptMngCode>();
            RptMngComments = new FakeDbSet<RptMngComment>();
            ScBizTypes = new FakeDbSet<ScBizType>();
            ScCavs = new FakeDbSet<ScCav>();
            ScCompanyFinances = new FakeDbSet<ScCompanyFinance>();
            ScFaqs = new FakeDbSet<ScFaq>();
            ScFileInfoes = new FakeDbSet<ScFileInfo>();
            ScFinancialIndexTs = new FakeDbSet<ScFinancialIndexT>();
            ScForms = new FakeDbSet<ScForm>();
            ScFormFiles = new FakeDbSet<ScFormFile>();
            ScMaks = new FakeDbSet<ScMak>();
            ScMentoringFileInfoes = new FakeDbSet<ScMentoringFileInfo>();
            ScMentoringReports = new FakeDbSet<ScMentoringReport>();
            ScMentoringTotalReports = new FakeDbSet<ScMentoringTotalReport>();
            ScMentoringTrFileInfoes = new FakeDbSet<ScMentoringTrFileInfo>();
            ScNtcs = new FakeDbSet<ScNtc>();
            ScQas = new FakeDbSet<ScQa>();
            ScQcls = new FakeDbSet<ScQcl>();
            ScReqDocs = new FakeDbSet<ScReqDoc>();
            ScReqDocFiles = new FakeDbSet<ScReqDocFile>();
            ScUsrResumes = new FakeDbSet<ScUsrResume>();
            SyDareDbInfoes = new FakeDbSet<SyDareDbInfo>();
            sys_DatabaseFirewallRules = new FakeDbSet<sys_DatabaseFirewallRule>();
            TcmsCompStatusSelectViews = new FakeDbSet<TcmsCompStatusSelectView>();
            TcmsIfLastReports = new FakeDbSet<TcmsIfLastReport>();
            TcmsIfSurveys = new FakeDbSet<TcmsIfSurvey>();
            TcmsMentoringReportSelectViews = new FakeDbSet<TcmsMentoringReportSelectView>();
            VcBaInfoes = new FakeDbSet<VcBaInfo>();
            VcBaTypeInfoes = new FakeDbSet<VcBaTypeInfo>();
            VcBizWorks = new FakeDbSet<VcBizWork>();
            VcCompInfoes = new FakeDbSet<VcCompInfo>();
            VcCompMappings = new FakeDbSet<VcCompMapping>();
            VcIfBaInfoes = new FakeDbSet<VcIfBaInfo>();
            VcIfCompInfoes = new FakeDbSet<VcIfCompInfo>();
            VcIfCompMappings = new FakeDbSet<VcIfCompMapping>();
            VcIfMentorInfoes = new FakeDbSet<VcIfMentorInfo>();
            VcIfNumInfoes = new FakeDbSet<VcIfNumInfo>();
            VcIfQuesCompInfoes = new FakeDbSet<VcIfQuesCompInfo>();
            VcIfTcmsInfoes = new FakeDbSet<VcIfTcmsInfo>();
            VcIfUsrInfoes = new FakeDbSet<VcIfUsrInfo>();
            VcLastReportNSats = new FakeDbSet<VcLastReportNSat>();
            VcMentorInfoes = new FakeDbSet<VcMentorInfo>();
            VcMentorMappings = new FakeDbSet<VcMentorMapping>();
            VcNumMngInfoes = new FakeDbSet<VcNumMngInfo>();
            VcSatChecks = new FakeDbSet<VcSatCheck>();
            VcTcmsInfoes = new FakeDbSet<VcTcmsInfo>();
            VcUsrInfoes = new FakeDbSet<VcUsrInfo>();
        }
        
        public int SaveChangesCount { get; private set; } 
        public int SaveChanges()
        {
            ++SaveChangesCount;
            return 1;
        }

        public System.Threading.Tasks.Task<int> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public System.Threading.Tasks.Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        protected virtual void Dispose(bool disposing)
        {
        }
        
        public void Dispose()
        {
            Dispose(true);
        }
    }
}
