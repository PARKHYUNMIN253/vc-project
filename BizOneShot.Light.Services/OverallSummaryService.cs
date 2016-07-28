using BizOneShot.Light.Dao.Repositories;
using BizOneShot.Light.Models.ViewModels;
using BizOneShot.Light.Models.WebModels;
using BizOneShot.Light.Services.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizOneShot.Light.Services
{
    public interface IOverallSummaryService
    {
        //Task<double> makeAvgTotalPoint(int bizWorkSn, BasicSurveyReportViewModel paramModel);
    }

    class OverallSummaryService : IOverallSummaryService
    {
        private readonly IOverallSummaryService overallSummaryService;
        private readonly IScBizWorkRepository _scBizWorkRepository;
        private readonly IQuesMasterRepository _quesMasterRepository;
        private readonly IQuesResult2Repository _quesResult2Repository;
        private readonly IScUsrRepository _scUsrRepository;
        private readonly IScFinancialIndexTRepository _scFinancialIndexTRepository;
        private readonly IQuesResult1Repository _quesResult1Repository;
        private readonly IRptMasterRepository _rptMasterRepository;
        private readonly IScCompanyFinanceRepository _scCompanyFinanceRepository;

        public OverallSummaryService
        (
            IOverallSummaryService overallSummaryService, 
            IScBizWorkRepository _scBizWorkRepository, 
            IQuesMasterRepository _quesMasterRepository,
            IQuesResult2Repository _quesResult2Repostiory,
            IScUsrRepository _scUsrRepository,
            IScFinancialIndexTRepository _scFinancialIndexTRepository,
            IQuesResult1Repository _quesResult1Repository,
            IRptMasterRepository _rptMasterRepository,
            IScCompanyFinanceRepository _scCompanyFinanceRepository
        )
        {
            this.overallSummaryService = overallSummaryService;
            this._scBizWorkRepository = _scBizWorkRepository;
            this._quesMasterRepository = _quesMasterRepository;
            this._quesResult2Repository = _quesResult2Repostiory;
            this._scUsrRepository = _scUsrRepository;
            this._scFinancialIndexTRepository = _scFinancialIndexTRepository;
            this._quesResult1Repository = _quesResult1Repository;
            this._rptMasterRepository = _rptMasterRepository;
            this._scCompanyFinanceRepository = _scCompanyFinanceRepository;
        }

        // #1. viewModel의 AvgTotalPoint(전체평균)값을 조작하기위한 메서드
        //public async Task<double> makeAvgTotalPoint(int bizWorkSn, BasicSurveyReportViewModel paramModel) 
        //{
        //    double avgTotalPoint = 0.0;
        //    var curBizWork = await _scBizWorkRepository.GetBizWorkAsync(bw => bw.BizWorkSn == bizWorkSn); // BizWorkSn을 기준으로 해당 사업에 참여한 기업들 객체 받아오기

        //    // Dictionary 객체
        //    Dictionary<string, double> dicBizInHrMng = new Dictionary<string, double>();        //인적자원관리
        //    Dictionary<string, double> dicBizInFinanceMng = new Dictionary<string, double>();   //재무관리
        //    Dictionary<string, double> dicBizInMkt = new Dictionary<string, double>();          //기술경영마케팅
        //    Dictionary<string, double> dicBizInBasicCpas = new Dictionary<string, double>();    //기초역량
        //    Dictionary<string, decimal> dicSales = new Dictionary<string, decimal>();           //매출액
        //    Dictionary<string, decimal> dicValueadded = new Dictionary<string, decimal>();      //부가가치
        //    Dictionary<string, decimal> dicMaterrial = new Dictionary<string, decimal>();       //재료비
        //    Dictionary<string, decimal> dicQtEmp = new Dictionary<string, decimal>();           //종업원수
        //    Dictionary<string, decimal> dicOperatingErning = new Dictionary<string, decimal>(); //영업이익
        //    Dictionary<string, decimal> dicCurrentAsset = new Dictionary<string, decimal>();    //유동자산
        //    Dictionary<string, decimal> dicCurrentLiability = new Dictionary<string, decimal>();//유동부채

        //    var compMappings = curBizWork.ScCompMappings.Where(sc => sc.Status == "A"); // 승인된 기업만 가져오기

        //    foreach (var compMapping in compMappings)
        //    {
        //        var quesMaster = await _quesMasterRepository.GetQuesMasterAsync(
        //                                                        qm => qm.RegistrationNo == compMapping.ScCompInfo.RegistrationNo && 
        //                                                        qm.BasicYear ==  paramModel.BizWorkYear && 
        //                                                        qm.Status == "C");      // 문진표 작성내역 조회
        //        if (quesMaster == null) continue; // 문진표 작성내역 객체 검사

        //        var quesResult2s = (await _quesResult2Repository.GetQuesResult2sAsync(
        //                                                        qr => qr.QuestionSn == quesMaster.QuestionSn &&
        //                                                        qr.QuesCheckList.CurrentUseYn == "Y" &&
        //                                                        qr.QuesCheckList.SmallClassCd == "A1A202"))
        //                                                        .OrderBy(qr => qr.QuesCheckList.SmallClassCd).ToList(); // 조직만족도 코드 : A1A202

        //        var totalEmp = quesResult2s.SingleOrDefault(i => i.QuesCheckList.DetailCd == "A1A20201");   // 총직원은 문진표상의 총 인원

        //        double bizInHrMng, bizInMkt, bizInBasicCapa, bizInFinance = 0.0; // 참여기업의 점수를 계산하기위한 변수

        //        var scFinancialIndexT = await _scFinancialIndexTRepository.getScFinancialIndexTAsync(
        //                                                        sft => sft.CompSn == compMapping.ScCompInfo.CompSn && 
        //                                                        sft.Year == paramModel.BizWorkYear.ToString()); // 재무정보 객체
        //        if (scFinancialIndexT == null) continue; // 재무정보 객체 검사

        //        scFinancialIndexT.QtEmp = decimal.Parse(totalEmp.D452); // 인원수는 d-2년도의 값을 가져와 저장한다

        //        //bizInHrMng = await GetHumanResourceMng(quesMaster.QuestionSn);
        //        //bizInMkt = await GetTechMng()
        //    }


            
        //    return avgTotalPoint;
        //}

        // bizInHrMng를 계산하기위한 function
        public async Task<double> GetHumanResourceMng(int questionSn)
        {
            //산식엑셀 1-18 ~ 1-19
            double totalPoint = 0;

            // A1D101 : 인적자윈의 확보와 개발관리
            var quesResult1sHrMng = (await _quesResult1Repository.GetQuesResult1sAsync(
                                            q1r => q1r.QuestionSn == questionSn &&
                                            q1r.QuesCheckList.CurrentUseYn == "Y" &&
                                            q1r.QuesCheckList.SmallClassCd == "A1D101")).
                                            OrderBy(q1r => q1r.QuesCheckList.SmallClassCd).ToList();

            totalPoint = totalPoint + ReportHelper.CalcPoint(ReportHelper.GetCodeTypeA(ReportHelper.CalcCheckCount(quesResult1sHrMng)), 11);

            // A1D102 : 인적자원의 보상 및 유지관리
            var quesResult1sMaintenance = (await _quesResult1Repository.GetQuesResult1sAsync(
                                            q1r => q1r.QuestionSn == questionSn &&
                                            q1r.QuesCheckList.CurrentUseYn == "Y" &&
                                            q1r.QuesCheckList.SmallClassCd == "A1D102")).
                                            OrderBy(q1r => q1r.QuesCheckList.SmallClassCd).ToList();

            totalPoint = totalPoint + ReportHelper.CalcPoint(ReportHelper.GetCodeTypeA(ReportHelper.CalcCheckCount(quesResult1sMaintenance)), 8);

            return totalPoint;
        }

        // bizInMkt를 계산하기위한 function
        public async Task<double> GetTechMng(int questionSn, ScFinancialIndexT sboFinancialIndexT)
        {
            double totalPoint = 0; // 리턴할 총점
            double preReserchAmt = 0.0; // 전기 연구개발비

            var rpt = _rptMasterRepository.GetRptMasterByQuestionSn(rm => rm.QuestionSn == questionSn);
            var pre = _scCompanyFinanceRepository.getScCompanyFinance(scf => scf.CompSn == rpt.CompSn && scf.FnYear == int.Parse(sboFinancialIndexT.Year) - 1);

            if (pre != null)
            {
                preReserchAmt = Convert.ToDouble(pre.CiOrdevexp + pre.CiResearch + pre.McRndexp);
            }

            {
                double avg = 0D;
                double numerator = (Convert.ToDouble(sboFinancialIndexT.ReserchAmt) + preReserchAmt) / 2.0;
                double denominator = Convert.ToDouble(sboFinancialIndexT.CurrentSale);

                if (denominator != 0)    // 매출액 있을 때
                {
                    if (numerator == 0) // 연구개발비가 0이면
                    {
                        avg = 0;
                    }
                    else // 연구개발비가 0이 아니면
                    {
                        avg = (numerator / denominator) * 100;
                    }
                }
                else // 매출액이 0일 때
                {
                    if (numerator != 0) // 연구개발비가 있으면
                    {
                        avg = 3D;   // 최고점
                    }
                    else // 연구개발비가 0이면
                    {
                        avg = 0;
                    }
                }
                totalPoint = totalPoint + ReportHelper.CalcPoint(ReportHelper.GetCodeTypeM(avg), 2);
            }

            //연구개발 인력의 비율
            var quesResult2sEmpRate = (await _quesResult2Repository.GetQuesResult2sAsync(qrt => qrt.QuestionSn == questionSn &&
                                                                                        qrt.QuesCheckList.CurrentUseYn == "Y" &&
                                                                                        qrt.QuesCheckList.SmallClassCd == "A1B102"))
                                                                                        .OrderBy(qr => qr.QuesCheckList.SmallClassCd)
                                                                                        .ToList();
            //전체임직원수
            var TotalEmp = quesResult2sEmpRate.SingleOrDefault(i => i.QuesCheckList.DetailCd == "A1B10202");
            //연구개발인력
            var RndEmp = quesResult2sEmpRate.SingleOrDefault(i => i.QuesCheckList.DetailCd == "A1B10201");
            if (TotalEmp != null)
            {
                if (int.Parse(TotalEmp.D) != 0)
                {
                    double avg = (int.Parse(RndEmp.D) / double.Parse(TotalEmp.D)) * 100;
                    totalPoint = totalPoint + ReportHelper.CalcPoint(ReportHelper.GetCodeTypeE(avg), 1);
                }
            }

            //연구개발 인력의 능력
            var quesResult2sEmpCapa = (await _quesResult2Repository.GetQuesResult2sAsync(qrt => qrt.QuestionSn == questionSn &&
                                                                                        qrt.QuesCheckList.CurrentUseYn == "Y" &&
                                                                                        qrt.QuesCheckList.SmallClassCd == "A1B103"))
                                                                                        .OrderBy(qr => qr.QuesCheckList.SmallClassCd)
                                                                                        .ToList();
            //박사급
            var DoctorEmp = quesResult2sEmpCapa.SingleOrDefault(i => i.QuesCheckList.DetailCd == "A1B10301");
            //석사급
            var MasterEmp = quesResult2sEmpCapa.SingleOrDefault(i => i.QuesCheckList.DetailCd == "A1B10302");
            //학사급
            var CollegeEmp = quesResult2sEmpCapa.SingleOrDefault(i => i.QuesCheckList.DetailCd == "A1B10303");
            //기능사급
            var TechEmp = quesResult2sEmpCapa.SingleOrDefault(i => i.QuesCheckList.DetailCd == "A1B10304");
            //고졸이하급
            var HighEmp = quesResult2sEmpCapa.SingleOrDefault(i => i.QuesCheckList.DetailCd == "A1B10305");
            if ((int.Parse(DoctorEmp.D) + int.Parse(MasterEmp.D) + int.Parse(CollegeEmp.D) + int.Parse(TechEmp.D) + int.Parse(HighEmp.D)) != 0)
            {
                double avg = ((int.Parse(DoctorEmp.D) * 5) + (int.Parse(MasterEmp.D) * 4) + (int.Parse(CollegeEmp.D) * 3) + (int.Parse(TechEmp.D) * 2) + (int.Parse(HighEmp.D) * 1)) / (double.Parse(DoctorEmp.D) + double.Parse(MasterEmp.D) + double.Parse(CollegeEmp.D) + double.Parse(TechEmp.D) + double.Parse(HighEmp.D));
                totalPoint = totalPoint + ReportHelper.CalcPoint(ReportHelper.GetCodeTypeE(avg), 3);
            }


            // A1B104 : 사업화역량
            var quesResult1sBizCapa = (await _quesResult1Repository.GetQuesResult1sAsync(qro => qro.QuestionSn == questionSn &&
                                                                                        qro.QuesCheckList.CurrentUseYn == "Y" &&
                                                                                        qro.QuesCheckList.SmallClassCd == "A1B104"))
                                                                                        .OrderBy(qr => qr.QuesCheckList.SmallClassCd)
                                                                                        .ToList();
                
            totalPoint = totalPoint + ReportHelper.CalcPoint(ReportHelper.GetCodeTypeC(ReportHelper.CalcCheckCount(quesResult1sBizCapa)), 5);

            // A1B105 : 사업화실적
            var quesResult2sBizResult = (await _quesResult2Repository.GetQuesResult2sAsync(qrt => qrt.QuestionSn == questionSn &&
                                                                                        qrt.QuesCheckList.CurrentUseYn == "Y" &&
                                                                                        qrt.QuesCheckList.SmallClassCd == "A1B105"))
                                                                                        .OrderBy(qr => qr.QuesCheckList.SmallClassCd)
                                                                                        .ToList();
            //사업화실적 총 건수
            var BizResultCnt = quesResult2sBizResult.SingleOrDefault(i => i.QuesCheckList.DetailCd == "A1B10502");
            {
                double avg = (int.Parse(BizResultCnt.D) + int.Parse(BizResultCnt.D451) + int.Parse(BizResultCnt.D452)) / 3.0;
                totalPoint = totalPoint + ReportHelper.CalcPoint(ReportHelper.GetCodeTypeE(avg), 4);
            }

            // A1B106 : 생산설비의 운영체제 및 관리
            var quesResult1sFacMng = (await _quesResult1Repository.GetQuesResult1sAsync(qro => qro.QuestionSn == questionSn &&
                                                                                        qro.QuesCheckList.CurrentUseYn == "Y" &&
                                                                                        qro.QuesCheckList.SmallClassCd == "A1B106"))
                                                                                        .OrderBy(qr => qr.QuesCheckList.SmallClassCd)
                                                                                        .ToList();

            totalPoint = totalPoint + ReportHelper.CalcPoint(ReportHelper.GetCodeTypeA(ReportHelper.CalcCheckCount(quesResult1sFacMng)), 2);

            // A1B107 : 공정관리
            var quesResult1sProcess = (await _quesResult1Repository.GetQuesResult1sAsync(qro => qro.QuestionSn == questionSn &&
                                                                                        qro.QuesCheckList.CurrentUseYn == "Y" &&
                                                                                        qro.QuesCheckList.SmallClassCd == "A1B107"))
                                                                                        .OrderBy(qr => qr.QuesCheckList.SmallClassCd)
                                                                                        .ToList();

            totalPoint = totalPoint + ReportHelper.CalcPoint(ReportHelper.GetCodeTypeC(ReportHelper.CalcCheckCount(quesResult1sProcess)), 2);

            // A1B108 : 품질관리
            var quesResult1sQaMng = (await _quesResult1Repository.GetQuesResult1sAsync(qro => qro.QuestionSn == questionSn &&
                                                                                        qro.QuesCheckList.CurrentUseYn == "Y" &&
                                                                                        qro.QuesCheckList.SmallClassCd == "A1B108"))
                                                                                        .OrderBy(qr => qr.QuesCheckList.SmallClassCd)
                                                                                        .ToList();

            totalPoint = totalPoint + ReportHelper.CalcPoint(ReportHelper.GetCodeTypeC(ReportHelper.CalcCheckCount(quesResult1sQaMng)), 3);

            // A1C101 : 마케팅 전략의 수립 및 실행
            var quesResult1sMarketing = (await _quesResult1Repository.GetQuesResult1sAsync(qro => qro.QuestionSn == questionSn &&
                                                                                        qro.QuesCheckList.CurrentUseYn == "Y" &&
                                                                                        qro.QuesCheckList.SmallClassCd == "A1C101"))
                                                                                        .OrderBy(qr => qr.QuesCheckList.SmallClassCd)
                                                                                        .ToList();

            totalPoint = totalPoint + ReportHelper.CalcPoint(ReportHelper.GetCodeTypeG(ReportHelper.CalcCheckCount(quesResult1sMarketing)), 8);

            // A1C102 : 고객관리
            var quesResult1sCustMng = (await _quesResult1Repository.GetQuesResult1sAsync(qro => qro.QuestionSn == questionSn &&
                                                                                        qro.QuesCheckList.CurrentUseYn == "Y" &&
                                                                                        qro.QuesCheckList.SmallClassCd == "A1C102"))
                                                                                        .OrderBy(qr => qr.QuesCheckList.SmallClassCd)
                                                                                        .ToList();

            totalPoint = totalPoint + ReportHelper.CalcPoint(ReportHelper.GetCodeTypeA(ReportHelper.CalcCheckCount(quesResult1sCustMng)), 9);

            return totalPoint;
        }

        //public async Task<double> GetHumanResourceMng(int questionSn)
        //{
        //    //산식엑셀 1-18 ~ 1-19
        //    double totalPoint = 0;

        //    // A1D101 : 인적자윈의 확보와 개발관리
        //    var quesResult1sHrMng = await quesResult1Service.GetQuesResult1sAsync(questionSn, "A1D101");
        //    totalPoint = totalPoint + ReportHelper.CalcPoint(ReportHelper.GetCodeTypeA(ReportHelper.CalcCheckCount(quesResult1sHrMng)), 11);

        //    // A1D102 : 인적자원의 보상 및 유지관리
        //    var quesResult1sMaintenance = await quesResult1Service.GetQuesResult1sAsync(questionSn, "A1D102");
        //    totalPoint = totalPoint + ReportHelper.CalcPoint(ReportHelper.GetCodeTypeA(ReportHelper.CalcCheckCount(quesResult1sMaintenance)), 8);

        //    return totalPoint;
        //}
    }
}