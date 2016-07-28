using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BizOneShot.Light.Dao.Infrastructure;
using BizOneShot.Light.Dao.Repositories;
using BizOneShot.Light.Models.ViewModels;
using BizOneShot.Light.Models.WebModels;
using PagedList;

namespace BizOneShot.Light.Services
{
    public interface IVcLastReportNSatService : IBaseService
    {
        Task<int> AddCheckAasync(VcLastReportNSat RnS);
        Task<IList<VcLastReportNSat>> getVcCompInfoById(string CompSn);
        Task<VcLastReportNSat> getSatSn(string CompSn, string SubNumSn, string MentorSn, int TotalReportSn);
        Task<VcLastReportNSat> getSatSn(string CompSn, string NumSn, string SubNumSn, string MentorSn, string Concode);

        // add loy for Deepen Confirm Submit
        Task<VcLastReportNSat> checkDeepenSubmit(int compSn, int baSn, int mentorSn);
        Task<IList<VcLastReportNSat>> checkDeepenSubmitByMentor(int compSn, int baSn, string numSn, string subNumSn, string conCode);

        // ba가 멘토 조회 할때 심화보고서 상태 확인
        Task<VcLastReportNSat> checkDeepenSubmitByBa(int compSn, int baSn, int mentorSn, string conCode);

    }
    public class VcLastReportNSatService : IVcLastReportNSatService
    {
        private readonly IVcLastReportNSatRepository VcLastReportNSatRepository;
        private readonly IUnitOfWork unitOfWork;

        public VcLastReportNSatService(IVcLastReportNSatRepository VcLastReportNSatRepository, IUnitOfWork unitOfWork)
        {
            this.VcLastReportNSatRepository = VcLastReportNSatRepository;
            this.unitOfWork = unitOfWork;
        }

        public void SaveDbContext()
        {
            unitOfWork.Commit();
        }

        public async Task<int> SaveDbContextAsync()
        {
            return await unitOfWork.CommitAsync();
        }

        public async Task<int> AddCheckAasync(VcLastReportNSat RnS)
        {
            var makRnS = VcLastReportNSatRepository.insert(RnS);
            if (makRnS == null)
            {
                return -1;
            }
            return await SaveDbContextAsync();
        }

        public async Task<IList<VcLastReportNSat>> getVcCompInfoById(string CompSn)
        {
            var convertCompSn = int.Parse(CompSn);
            return await VcLastReportNSatRepository.getVcCompInfoById(vc => vc.CompSn == convertCompSn);
        }

        public async Task<VcLastReportNSat> getSatSn(string CompSn, string SubNumSn, string MentorSn, int TotalReportSn)
        {
            var convertCompSn = int.Parse(CompSn);
            var convertMentorSn = int.Parse(MentorSn);

            return await VcLastReportNSatRepository.getSatSn(sn => sn.CompSn == convertCompSn &&
            sn.SubNumSn == SubNumSn && sn.MentorSn == convertMentorSn && sn.TotalReportSn == TotalReportSn);
        }

        public async Task<VcLastReportNSat> getSatSn(string CompSn, string NumSn, string SubNumSn, string MentorSn, string Concode)
        {
            var convertCompSn = int.Parse(CompSn);
            var convertMentorSn = int.Parse(MentorSn);
            return await VcLastReportNSatRepository.getSatSn(sn => sn.CompSn == convertCompSn && sn.NumSn == NumSn &&
            sn.SubNumSn == SubNumSn && sn.MentorSn == convertMentorSn && sn.ConCode == Concode);
        }
        public async Task<VcLastReportNSat> checkDeepenSubmit(int compSn, int baSn, int mentorSn)
        {
            return await VcLastReportNSatRepository.checkDeepenSubmit(compSn, baSn, mentorSn);
        }

        public async Task<IList<VcLastReportNSat>> checkDeepenSubmitByMentor(int compSn, int baSn, string numSn, string subNumSn, string conCode)
        {
            return await VcLastReportNSatRepository.checkDeepenSubmitByMentor(compSn, baSn, numSn, subNumSn, conCode);
        }

        public async Task<VcLastReportNSat> checkDeepenSubmitByBa(int compSn, int baSn, int mentorSn, string conCode)
        {
            return await VcLastReportNSatRepository.checkDeepenSubmitByBa(compSn, baSn, mentorSn, conCode);
        }
    }
}
