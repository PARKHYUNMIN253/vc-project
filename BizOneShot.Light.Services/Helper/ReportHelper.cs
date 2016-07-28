using BizOneShot.Light.Models.DareModels;
using BizOneShot.Light.Models.WebModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizOneShot.Light.Services.Helper
{
    public static class ReportHelper
    {
        public static string GetCodeTypeA(int trueCnt)
        {
            string code = "";
            switch (trueCnt)
            {
                case 0:
                case 1:
                    code = "E";
                    break;
                case 2:
                    code = "D";
                    break;
                case 3:
                    code = "C";
                    break;
                case 4:
                    code = "B";
                    break;
                default:
                    code = "A";
                    break;

            }
            return code;
        }

        public static string GetCodeTypeB(int Cnt)
        {
            string code = "";
            switch (Cnt)
            {
                case 0:
                    code = "E";
                    break;
                case 1:
                    code = "C";
                    break;
                default:
                    code = "A";
                    break;

            }
            return code;
        }

        public static string GetCodeTypeC(int trueCnt)
        {
            string code = "";
            switch (trueCnt)
            {
                case 0:
                    code = "E";
                    break;
                case 1:
                    code = "D";
                    break;
                case 2:
                    code = "C";
                    break;
                case 3:
                    code = "B";
                    break;
                default:
                    code = "A";
                    break;

            }
            return code;
        }


        public static string GetCodeTypeD(double per)
        {
            string code = "";
            if (per < 5)
                code = "A";
            else if (per >= 5 && per < 10)
                code = "B";
            else if (per >= 10 && per < 15)
                code = "C";
            else if (per >= 15 && per < 20)
                code = "D";
            else
                code = "E";
            return code;
        }


        public static string GetCodeTypeE(double per)
        {
            string code = "";
            if (per >= 5)
                code = "A";
            else if (per < 5 && per >= 4)
                code = "B";
            else if (per < 4 && per >= 3)
                code = "C";
            else if (per < 3 && per >= 2)
                code = "D";
            else
                code = "E";
            return code;
        }

        public static string GetCodeTypeF(double per)
        {
            string code = "";
            if (per >= 4)
                code = "A";
            else if (per < 4 && per >= 3)
                code = "B";
            else if (per < 3 && per >= 2)
                code = "C";
            else if (per < 2 && per >= 1)
                code = "D";
            else
                code = "E";
            return code;
        }

        public static string GetCodeTypeG(int trueCnt)
        {
            string code = "";
            switch (trueCnt)
            {
                case 0:
                case 1:
                case 2:
                    code = "E";
                    break;
                case 3:
                    code = "D";
                    break;
                case 4:
                    code = "C";
                    break;
                case 5:
                    code = "B";
                    break;
                default:
                    code = "A";
                    break;

            }
            return code;
        }

        public static string GetCodeTypeH(double per)
        {
            string code = "";
            if (per >= 25)
                code = "A";
            else if (per < 25 && per >= 20)
                code = "B";
            else if (per < 20 && per >= 15)
                code = "C";
            else if (per < 15 && per >= 10)
                code = "D";
            else
                code = "E";
            return code;
        }

        public static string GetCodeTypeI(double per)
        {
            string code = "";
            if (per >= 7.5)
                code = "A";
            else if (per < 7.5 && per >= 5)
                code = "B";
            else if (per < 5 && per >= 2.5)
                code = "C";
            else if (per < 2.5 && per >= 0.1)
                code = "D";
            else
                code = "E";
            return code;
        }

        public static string GetCodeTypeM(double per)
        {
            string code = "";
            if (per >= 3)
                code = "A";
            else if (per < 3 && per >= 2.5)
                code = "B";
            else if (per < 2.5 && per >= 2)
                code = "C";
            else if (per < 2 && per >= 1.5)
                code = "D";
            else
                code = "E";
            return code;
        }

        public static double CalcPoint(string type, double standardPoint)
        {
            double point = 0;
            switch (type)
            {
                case "A":
                    point = standardPoint * 1;
                    break;
                case "B":
                    point = standardPoint * 0.75;
                    break;
                case "C":
                    point = standardPoint * 0.5;
                    break;
                case "D":
                    point = standardPoint * 0.25;
                    break;
                case "E":
                    point = standardPoint * 0.0;
                    break;
            }
            return point;
        }


        public static string GetArrowTypeA(double point)
        {
            string code = "";
            if (point >= 0 && point <= 50)
                code = "C";
            else if (point > 50 && point <= 75)
                code = "B";
            else
                code = "A";
            return code;
        }

        public static string GetArrowTypeB(double point)
        {
            string code = "";
            if (point >= 0 && point <= 9.5)
                code = "C";
            else if (point > 9.5 && point <= 14.25)
                code = "B";
            else
                code = "A";
            return code;
        }

        public static string GetArrowTypeC(double point)
        {
            string code = "";
            if (point >= 0 && point <= 19.5)
                code = "C";
            else if (point > 19.5 && point <= 29.25)    // 0411 - phm , 29.25로 변경
                code = "B";
            else
                code = "A";
            return code;
        }

        public static string GetArrowTypeD(double point)
        {
            string code = "";
            if (point >= 0 && point <= 5)
                code = "C";
            else if (point > 5 && point <= 7.5)
                code = "B";
            else
                code = "A";
            return code;
        }

        public static string GetArrowTypeE(int type)
        {
            string code = "";
            if (type == 1)
                code = "C";
            else if (type == 2)
                code = "B";
            else
                code = "A";
            return code;
        }

        public static double CalcFinancialPoint(SHUSER_SboFinancialIndexT sboFinancialIndexT, ScCav obj)
        {
            //각 항목의 계산값이 0보다 작을경우는 0점으로 처리한다.


            //매출영업이익률(영업이익 ÷ 매출액)×100
            //if(sboFinancialIndexT.CurrentSale.Value == 0) ? 
            double a = (sboFinancialIndexT.CurrentSale.Value == 0) ? 0 : Convert.ToDouble((sboFinancialIndexT.OperatingEarning.Value / sboFinancialIndexT.CurrentSale.Value) * 100);
            double aPoint = 0.0;

            if (sboFinancialIndexT.OperatingEarning.Value == 0 || sboFinancialIndexT.CurrentSale.Value == 0)
            {
                aPoint = 0;
            }
            else
            {
                // 소수 3.4000000004 식으로 왜??
                //double ex = Convert.ToDouble(obj.CavOp);
                //double gw = getWeight(a, ex);
                //aPoint = gw * 17D;
                aPoint = getWeight(a, Convert.ToDouble(obj.CavOp)) * 17;
            }
            //double aPoint = (a / (5.2 + a)) * 17;

            //자기자본순이익률(당기순이익 ÷ 자본총계)×100
            double b = (sboFinancialIndexT.TotalCapital.Value == 0) ? 0 : Convert.ToDouble((sboFinancialIndexT.CurrentEarning.Value / sboFinancialIndexT.TotalCapital.Value) * 100);
            double bPoint = 0.0;

            if (sboFinancialIndexT.CurrentEarning.Value == 0 || sboFinancialIndexT.TotalCapital.Value == 0)
            {
                bPoint = 0;
            }
            else
            {
                bPoint = getWeight(b, Convert.ToDouble(obj.CavRe)) * 6;
            }
            //double bPoint = (b / (5.19 + b)) * 6;

            //매출증가율((당기매출액 - 전기매출액) ÷ 전기매출액)×100
            double c = (sboFinancialIndexT.PrevSale.Value == 0) ? 0 : Convert.ToDouble(((sboFinancialIndexT.CurrentSale.Value - sboFinancialIndexT.PrevSale.Value) / sboFinancialIndexT.PrevSale.Value) * 100);
            //double cPoint = ((c / (4.93 + c)) * 9);
            double cPoint = 0.0;

            //전기매출액이 0 이고 당기매출액이 0 일때 =  0점
            if (sboFinancialIndexT.PrevSale.Value == 0)
            {
                if (sboFinancialIndexT.CurrentSale.Value == 0)
                {
                    cPoint = 0;
                }
                else if (sboFinancialIndexT.CurrentSale > 0)
                {
                    cPoint = 9;
                }
            }
            else
            {
                cPoint = getWeight(c, Convert.ToDouble(obj.CavSg)) * 9;
            }

            //순이익증가율((당기순이익 - 전기순이익) ÷ 전기순이익)×100
            double d = (sboFinancialIndexT.PrevEarning.Value == 0) ? 0 : Convert.ToDouble(((sboFinancialIndexT.CurrentEarning.Value - sboFinancialIndexT.PrevEarning.Value) / sboFinancialIndexT.PrevEarning.Value) * 100);
            //double dPoint = (d / (19.96 + d)) * 14;
            double dPoint = 0.0;

            //당기손익이 0이하일때 = 0점
            if (sboFinancialIndexT.CurrentEarning.Value <= 0)
            {
                dPoint = 0;
            }
            //당기손익이 이익(양수)이고 전기손익이 0이하일때 = 14점
            else if (sboFinancialIndexT.CurrentEarning.Value > 0 && sboFinancialIndexT.PrevEarning.Value <= 0)
            {
                dPoint = 14;
            }
            else
            {
                dPoint = getWeight(d, (Convert.ToDouble(obj.CavNg))) * 14;
            }

            //당좌비율((유동자산 - 재고자산) ÷ 유동부채)×100
            double e = (sboFinancialIndexT.CurrentLiability.Value == 0) ? 0 : Convert.ToDouble(((sboFinancialIndexT.CurrentAsset.Value - sboFinancialIndexT.InventoryAsset.Value) / sboFinancialIndexT.CurrentLiability.Value) * 100);
            double ePoint = 0.0;
            // double ePoint = (e / (102.09 + e)) * 4;

            // 유동부채가 0일 경우 4점
            if ((sboFinancialIndexT.CurrentAsset.Value - sboFinancialIndexT.InventoryAsset.Value) == 0 && sboFinancialIndexT.CurrentLiability.Value == 0)
            {
                ePoint = 0;
            }
            else if ((sboFinancialIndexT.CurrentAsset.Value - sboFinancialIndexT.InventoryAsset.Value) == 0)
            {
                ePoint = 0;
            }
            else
            {
                if (sboFinancialIndexT.CurrentLiability.Value == 0)
                {
                    ePoint = 4;
                }
                else
                {
                    ePoint = getWeight(e, Convert.ToDouble(obj.CavQr)) * 4;
                }
            }

            //유동비율(유동자산 ÷ 유동부채)×100 
            double f = (sboFinancialIndexT.CurrentLiability.Value == 0) ? 0 : Convert.ToDouble((sboFinancialIndexT.CurrentAsset.Value / sboFinancialIndexT.CurrentLiability.Value) * 100);
            double fPoint = 0.0;

            // 유동부채가 0일 경우 13점
            if (sboFinancialIndexT.CurrentAsset.Value == 0 && sboFinancialIndexT.CurrentLiability.Value == 0)
            {
                fPoint = 0;
            }
            else if (sboFinancialIndexT.CurrentAsset.Value == 0)
            {
                fPoint = 0;
            }
            else
            {
                if (sboFinancialIndexT.CurrentLiability.Value == 0)
                {
                    fPoint = 13;
                }
                else
                {
                    fPoint = getWeight(f, (Convert.ToDouble(obj.CavCr))) * 13;
                }
                //double fPoint = (f / (136.27 + f)) * 13;
            }

            //부채비율(부채 ÷ 자본총계)×100
            double g = (sboFinancialIndexT.TotalCapital.Value == 0) ? 0 : Convert.ToDouble((sboFinancialIndexT.TotalLiability.Value / sboFinancialIndexT.TotalCapital.Value) * 100);
            double gPoint = 0.0;

            if (sboFinancialIndexT.TotalLiability.Value == 0 && sboFinancialIndexT.TotalCapital.Value == 0)
            {
                gPoint = 0;
            }
            else if (sboFinancialIndexT.TotalLiability.Value == 0)
            {
                gPoint = 9;
            }
            else
            {
                gPoint = getWeight(g, Convert.ToDouble(obj.CavDebt)) * 9;
            }

            //이자보상비율(영업이익 ÷ 이자비용)×100
            double h = (sboFinancialIndexT.InterstCost.Value == 0) ? 0 : Convert.ToDouble((sboFinancialIndexT.OperatingEarning.Value / sboFinancialIndexT.InterstCost.Value) * 100);
            double hPoint = 0;

            if (sboFinancialIndexT.OperatingEarning.Value == 0 && sboFinancialIndexT.InterstCost.Value == 0)
            {
                hPoint = 0;
            }
            else if (sboFinancialIndexT.OperatingEarning.Value == 0)
            {
                hPoint = 0;
            }
            else
            {
                // 이자비용이 0일 경우 7
                if (sboFinancialIndexT.InterstCost.Value == 0)
                {
                    hPoint = 7;
                }
                else
                {
                    hPoint = getWeight(h, Convert.ToDouble(obj.CavIcr)) * 7;
                }
                //double hPoint = (h / (333.63 + h)) * 7;
            }

            //총자산회전율(매출액 ÷ 총자산)×100
            double i = (sboFinancialIndexT.TotalAsset.Value == 0) ? 0 : Convert.ToDouble((sboFinancialIndexT.CurrentSale.Value / sboFinancialIndexT.TotalAsset.Value) * 100);
            double iPoint = 0.0;

            if (sboFinancialIndexT.CurrentSale.Value == 0 && sboFinancialIndexT.TotalAsset.Value == 0)
            {
                iPoint = 0;
            }
            else if (sboFinancialIndexT.CurrentSale.Value == 0)
            {
                iPoint = 0;
            }
            else
            {
                // 총 자산이 0일 경우
                if (sboFinancialIndexT.TotalAsset.Value == 0)
                {
                    iPoint = 3;
                }
                else
                {
                    iPoint = getWeight(i, Convert.ToDouble(obj.CavTat)) * 3;
                }
                //double iPoint = (i / (114.75 + i)) * 3;
            }

            //매출채권회전율(매출액 ÷ 매출채권(=외상매출금,미수금,받을어음))×100
            double j = (sboFinancialIndexT.SalesCredit.Value == 0) ? 0 : Convert.ToDouble((sboFinancialIndexT.CurrentSale.Value / sboFinancialIndexT.SalesCredit.Value) * 100);
            double jPoint = 0;

            if (sboFinancialIndexT.CurrentSale.Value == 0 && sboFinancialIndexT.SalesCredit.Value == 0)
            {
                jPoint = 0;
            }
            else if (sboFinancialIndexT.CurrentSale.Value == 0)
            {
                jPoint = 0;
            }
            else
            {
                // 매출채권이 0인 경우
                if (sboFinancialIndexT.SalesCredit.Value == 0)
                {
                    jPoint = 3;
                }
                else
                {
                    jPoint = getWeight(j, Convert.ToDouble(obj.CavTrt)) * 3;
                }
                //double jPoint = (j / (569.36 + j)) * 3;
            }

            //재고자산회전율(매출액 ÷ 재고자산)×100
            double k = (sboFinancialIndexT.InventoryAsset.Value == 0) ? 0 : Convert.ToDouble((sboFinancialIndexT.CurrentSale.Value / sboFinancialIndexT.InventoryAsset.Value) * 100);
            double kPoint = 0;

            if (sboFinancialIndexT.CurrentSale.Value == 0 && sboFinancialIndexT.InventoryAsset.Value == 0)
            {
                kPoint = 0;
            }
            else if (sboFinancialIndexT.CurrentSale.Value == 0)
            {
                kPoint = 0;
            }
            else
            {
                //재고자산이 0일때 = 4점
                if (sboFinancialIndexT.InventoryAsset.Value == 0)
                {
                    kPoint = 4;
                }
                else
                {
                    kPoint = getWeight(k, Convert.ToDouble(obj.CavIt)) * 4;
                }
                //double kPoint = (k / (915.48 + k)) * 4;
            }

            // 부가가치율(부가가치 ÷ 매출액)×100
            double l = (sboFinancialIndexT.CurrentSale.Value == 0) ? 0 : Convert.ToDouble((sboFinancialIndexT.ValueAdded.Value / sboFinancialIndexT.CurrentSale.Value) * 100);
            double lPoint = 0;

            if (sboFinancialIndexT.ValueAdded.Value == 0 && sboFinancialIndexT.CurrentSale.Value == 0)
            {
                lPoint = 0;
            }
            else if (sboFinancialIndexT.ValueAdded.Value == 0)
            {
                lPoint = 0;
            }
            else
            {
                // 당기매출액이 0이면
                if (sboFinancialIndexT.CurrentSale.Value == 0)
                {
                    lPoint = 4;
                }
                else
                {
                    lPoint = getWeight(l, (Convert.ToDouble(obj.CavVr))) * 4;
                }
                //double lPoint = (l / (24.02 + l)) * 4;
            }

            // 노동생산성 = 부가가치 ÷ 종업원수
            double m = (sboFinancialIndexT.QtEmp.Value == 0) ? 0 : Convert.ToDouble(sboFinancialIndexT.ValueAdded.Value / sboFinancialIndexT.QtEmp.Value);
            double mPoint = 0;

            if (sboFinancialIndexT.ValueAdded.Value == 0 && sboFinancialIndexT.QtEmp.Value == 0)
            {
                mPoint = 0;
            }
            else if (sboFinancialIndexT.ValueAdded.Value == 0)
            {
                mPoint = 0;
            }
            else
            {
                // 종업원 수가 0이면
                if (sboFinancialIndexT.QtEmp.Value == 0)
                {
                    mPoint = 4;
                }
                else
                {
                    mPoint = getWeight(Math.Truncate(m / 1000), Convert.ToDouble(obj.CavLp)) * 4;
                }
            }

            //double mPoint = (m / (16163671 + m)) * 4;

            //자본생산성((부가가치 ÷ 자본총계)×100
            double n = (sboFinancialIndexT.TotalCapital.Value == 0) ? 0 : Convert.ToDouble((sboFinancialIndexT.ValueAdded.Value / sboFinancialIndexT.TotalCapital.Value) * 100);
            double nPoint = 0;

            if (sboFinancialIndexT.ValueAdded.Value == 0 && sboFinancialIndexT.TotalCapital.Value == 0)
            {
                nPoint = 0;
            }
            else if (sboFinancialIndexT.ValueAdded.Value == 0)
            {
                nPoint = 0;
            }
            else
            {
                // 자본총계가 0이면
                if (sboFinancialIndexT.TotalCapital.Value == 0)
                {
                    nPoint = 3;
                }
                else
                {
                    nPoint = getWeight(n, Convert.ToDouble(obj.CavCp)) * 3;
                }
                //double nPoint = (n / (137.01 + n)) * 3;
            }

            //재무점수로 환산
            double point = aPoint + bPoint + cPoint + dPoint + ePoint + fPoint + gPoint + hPoint + iPoint + jPoint + kPoint + lPoint + mPoint + nPoint;
            return point;
        }

        public static double CalcFinancialPoint(ScFinancialIndexT sboFinancialIndexT, ScCav obj)
        {

            //매출영업이익률(영업이익 ÷ 매출액)×100
            //if(sboFinancialIndexT.CurrentSale.Value == 0) ? 
            double a = (sboFinancialIndexT.CurrentSale.Value == 0) ? 0 : Convert.ToDouble((sboFinancialIndexT.OperatingEarning.Value / sboFinancialIndexT.CurrentSale.Value) * 100);
            double aPoint = 0.0;

            if (sboFinancialIndexT.OperatingEarning.Value == 0 || sboFinancialIndexT.CurrentSale.Value == 0)
            {
                aPoint = 0;
            }
            else
            {
                // 소수 3.4000000004 식으로 왜??
                //double ex = Convert.ToDouble(obj.CavOp);
                //double gw = getWeight(a, ex);
                //aPoint = gw * 17D;
                aPoint = getWeight(a, Convert.ToDouble(obj.CavOp)) * 17;
            }
            //double aPoint = (a / (5.2 + a)) * 17;

            //자기자본순이익률(당기순이익 ÷ 자본총계)×100
            double b = (sboFinancialIndexT.TotalCapital.Value == 0) ? 0 : Convert.ToDouble((sboFinancialIndexT.CurrentEarning.Value / sboFinancialIndexT.TotalCapital.Value) * 100);
            double bPoint = 0.0;

            if (sboFinancialIndexT.CurrentEarning.Value == 0 || sboFinancialIndexT.TotalCapital.Value == 0)
            {
                bPoint = 0;
            }
            else
            {
                bPoint = getWeight(b, Convert.ToDouble(obj.CavRe)) * 6;
            }
            //double bPoint = (b / (5.19 + b)) * 6;

            //매출증가율((당기매출액 - 전기매출액) ÷ 전기매출액)×100
            double c = (sboFinancialIndexT.PrevSale.Value == 0) ? 0 : Convert.ToDouble(((sboFinancialIndexT.CurrentSale.Value - sboFinancialIndexT.PrevSale.Value) / sboFinancialIndexT.PrevSale.Value) * 100);
            //double cPoint = ((c / (4.93 + c)) * 9);
            double cPoint = 0.0;

            //전기매출액이 0 이고 당기매출액이 0 일때 =  0점
            if (sboFinancialIndexT.PrevSale.Value == 0)
            {
                if (sboFinancialIndexT.CurrentSale.Value == 0)
                {
                    cPoint = 0;
                }
                else if (sboFinancialIndexT.CurrentSale > 0)
                {
                    cPoint = 9;
                }
            }
            else
            {
                cPoint = getWeight(c, Convert.ToDouble(obj.CavSg)) * 9;
            }

            //순이익증가율((당기순이익 - 전기순이익) ÷ 전기순이익)×100
            double d = (sboFinancialIndexT.PrevEarning.Value == 0) ? 0 : Convert.ToDouble(((sboFinancialIndexT.CurrentEarning.Value - sboFinancialIndexT.PrevEarning.Value) / sboFinancialIndexT.PrevEarning.Value) * 100);
            //double dPoint = (d / (19.96 + d)) * 14;
            double dPoint = 0.0;

            //당기손익이 0이하일때 = 0점
            if (sboFinancialIndexT.CurrentEarning.Value <= 0)
            {
                dPoint = 0;
            }
            //당기손익이 이익(양수)이고 전기손익이 0이하일때 = 14점
            else if (sboFinancialIndexT.CurrentEarning.Value > 0 && sboFinancialIndexT.PrevEarning.Value <= 0)
            {
                dPoint = 14;
            }
            else
            {
                dPoint = getWeight(d, (Convert.ToDouble(obj.CavNg))) * 14;
            }

            //당좌비율((유동자산 - 재고자산) ÷ 유동부채)×100
            double e = (sboFinancialIndexT.CurrentLiability.Value == 0) ? 0 : Convert.ToDouble(((sboFinancialIndexT.CurrentAsset.Value - sboFinancialIndexT.InventoryAsset.Value) / sboFinancialIndexT.CurrentLiability.Value) * 100);
            double ePoint = 0.0;
            // double ePoint = (e / (102.09 + e)) * 4;

            // 유동부채가 0일 경우 4점
            if ((sboFinancialIndexT.CurrentAsset.Value - sboFinancialIndexT.InventoryAsset.Value) == 0 && sboFinancialIndexT.CurrentLiability.Value == 0)
            {
                ePoint = 0;
            }
            else if ((sboFinancialIndexT.CurrentAsset.Value - sboFinancialIndexT.InventoryAsset.Value) == 0)
            {
                ePoint = 0;
            }
            else
            {
                if (sboFinancialIndexT.CurrentLiability.Value == 0)
                {
                    ePoint = 4;
                }
                else
                {
                    ePoint = getWeight(e, Convert.ToDouble(obj.CavQr)) * 4;
                }
            }

            //유동비율(유동자산 ÷ 유동부채)×100 
            double f = (sboFinancialIndexT.CurrentLiability.Value == 0) ? 0 : Convert.ToDouble((sboFinancialIndexT.CurrentAsset.Value / sboFinancialIndexT.CurrentLiability.Value) * 100);
            double fPoint = 0.0;

            // 유동부채가 0일 경우 13점
            if (sboFinancialIndexT.CurrentAsset.Value == 0 && sboFinancialIndexT.CurrentLiability.Value == 0)
            {
                fPoint = 0;
            }
            else if (sboFinancialIndexT.CurrentAsset.Value == 0)
            {
                fPoint = 0;
            }
            else
            {
                if (sboFinancialIndexT.CurrentLiability.Value == 0)
                {
                    fPoint = 13;
                }
                else
                {
                    fPoint = getWeight(f, (Convert.ToDouble(obj.CavCr))) * 13;
                }
                //double fPoint = (f / (136.27 + f)) * 13;
            }

            //부채비율(부채 ÷ 자본총계)×100
            double g = (sboFinancialIndexT.TotalCapital.Value == 0) ? 0 : Convert.ToDouble((sboFinancialIndexT.TotalLiability.Value / sboFinancialIndexT.TotalCapital.Value) * 100);
            double gPoint = 0.0;

            if (sboFinancialIndexT.TotalLiability.Value == 0 && sboFinancialIndexT.TotalCapital.Value == 0)
            {
                gPoint = 0;
            }
            else if (sboFinancialIndexT.TotalLiability.Value == 0)
            {
                gPoint = 9;
            }
            else
            {
                gPoint = getWeight(g, Convert.ToDouble(obj.CavDebt)) * 9;
            }

            //이자보상비율(영업이익 ÷ 이자비용)×100
            double h = (sboFinancialIndexT.InterstCost.Value == 0) ? 0 : Convert.ToDouble((sboFinancialIndexT.OperatingEarning.Value / sboFinancialIndexT.InterstCost.Value) * 100);
            double hPoint = 0;

            if (sboFinancialIndexT.OperatingEarning.Value == 0 && sboFinancialIndexT.InterstCost.Value == 0)
            {
                hPoint = 0;
            }
            else if (sboFinancialIndexT.OperatingEarning.Value == 0)
            {
                hPoint = 0;
            }
            else
            {
                // 이자비용이 0일 경우 7
                if (sboFinancialIndexT.InterstCost.Value == 0)
                {
                    hPoint = 7;
                }
                else
                {
                    hPoint = getWeight(h, Convert.ToDouble(obj.CavIcr)) * 7;
                }
                //double hPoint = (h / (333.63 + h)) * 7;
            }

            //총자산회전율(매출액 ÷ 총자산)×100
            double i = (sboFinancialIndexT.TotalAsset.Value == 0) ? 0 : Convert.ToDouble((sboFinancialIndexT.CurrentSale.Value / sboFinancialIndexT.TotalAsset.Value) * 100);
            double iPoint = 0.0;

            if (sboFinancialIndexT.CurrentSale.Value == 0 && sboFinancialIndexT.TotalAsset.Value == 0)
            {
                iPoint = 0;
            }
            else if (sboFinancialIndexT.CurrentSale.Value == 0)
            {
                iPoint = 0;
            }
            else
            {
                // 총 자산이 0일 경우
                if (sboFinancialIndexT.TotalAsset.Value == 0)
                {
                    iPoint = 3;
                }
                else
                {
                    iPoint = getWeight(i, Convert.ToDouble(obj.CavTat)) * 3;
                }
                //double iPoint = (i / (114.75 + i)) * 3;
            }

            //매출채권회전율(매출액 ÷ 매출채권(=외상매출금,미수금,받을어음))×100
            double j = (sboFinancialIndexT.SalesCredit.Value == 0) ? 0 : Convert.ToDouble((sboFinancialIndexT.CurrentSale.Value / sboFinancialIndexT.SalesCredit.Value) * 100);
            double jPoint = 0;

            if (sboFinancialIndexT.CurrentSale.Value == 0 && sboFinancialIndexT.SalesCredit.Value == 0)
            {
                jPoint = 0;
            }
            else if (sboFinancialIndexT.CurrentSale.Value == 0)
            {
                jPoint = 0;
            }
            else
            {
                // 매출채권이 0인 경우
                if (sboFinancialIndexT.SalesCredit.Value == 0)
                {
                    jPoint = 3;
                }
                else
                {
                    jPoint = getWeight(j, Convert.ToDouble(obj.CavTrt)) * 3;
                }
                //double jPoint = (j / (569.36 + j)) * 3;
            }

            //재고자산회전율(매출액 ÷ 재고자산)×100
            double k = (sboFinancialIndexT.InventoryAsset.Value == 0) ? 0 : Convert.ToDouble((sboFinancialIndexT.CurrentSale.Value / sboFinancialIndexT.InventoryAsset.Value) * 100);
            double kPoint = 0;

            if (sboFinancialIndexT.CurrentSale.Value == 0 && sboFinancialIndexT.InventoryAsset.Value == 0)
            {
                kPoint = 0;
            }
            else if (sboFinancialIndexT.CurrentSale.Value == 0)
            {
                kPoint = 0;
            }
            else
            {
                //재고자산이 0일때 = 4점
                if (sboFinancialIndexT.InventoryAsset.Value == 0)
                {
                    kPoint = 4;
                }
                else
                {
                    kPoint = getWeight(k, Convert.ToDouble(obj.CavIt)) * 4;
                }
                //double kPoint = (k / (915.48 + k)) * 4;
            }

            // 부가가치율(부가가치 ÷ 매출액)×100
            double l = (sboFinancialIndexT.CurrentSale.Value == 0) ? 0 : Convert.ToDouble((sboFinancialIndexT.ValueAdded.Value / sboFinancialIndexT.CurrentSale.Value) * 100);
            double lPoint = 0;

            if (sboFinancialIndexT.ValueAdded.Value == 0 && sboFinancialIndexT.CurrentSale.Value == 0)
            {
                lPoint = 0;
            }
            else if (sboFinancialIndexT.ValueAdded.Value == 0)
            {
                lPoint = 0;
            }
            else
            {
                // 당기매출액이 0이면
                if (sboFinancialIndexT.CurrentSale.Value == 0)
                {
                    lPoint = 4;
                }
                else
                {
                    lPoint = getWeight(l, (Convert.ToDouble(obj.CavVr))) * 4;
                }
                //double lPoint = (l / (24.02 + l)) * 4;
            }

            // 노동생산성 = 부가가치 ÷ 종업원수
            double m = (sboFinancialIndexT.QtEmp.Value == 0) ? 0 : Convert.ToDouble(sboFinancialIndexT.ValueAdded.Value / sboFinancialIndexT.QtEmp.Value);
            double mPoint = 0;

            if (sboFinancialIndexT.ValueAdded.Value == 0 && sboFinancialIndexT.QtEmp.Value == 0)
            {
                mPoint = 0;
            }
            else if (sboFinancialIndexT.ValueAdded.Value == 0)
            {
                mPoint = 0;
            }
            else
            {
                // 종업원 수가 0이면
                if (sboFinancialIndexT.QtEmp.Value == 0)
                {
                    mPoint = 4;
                }
                else
                {
                    mPoint = getWeight(Math.Truncate(m / 1000), Convert.ToDouble(obj.CavLp)) * 4;
                }
            }

            //double mPoint = (m / (16163671 + m)) * 4;

            //자본생산성((부가가치 ÷ 자본총계)×100
            double n = (sboFinancialIndexT.TotalCapital.Value == 0) ? 0 : Convert.ToDouble((sboFinancialIndexT.ValueAdded.Value / sboFinancialIndexT.TotalCapital.Value) * 100);
            double nPoint = 0;

            if (sboFinancialIndexT.ValueAdded.Value == 0 && sboFinancialIndexT.TotalCapital.Value == 0)
            {
                nPoint = 0;
            }
            else if (sboFinancialIndexT.ValueAdded.Value == 0)
            {
                nPoint = 0;
            }
            else
            {
                // 자본총계가 0이면
                if (sboFinancialIndexT.TotalCapital.Value == 0)
                {
                    nPoint = 3;
                }
                else
                {
                    nPoint = getWeight(n, Convert.ToDouble(obj.CavCp)) * 3;
                }
                //double nPoint = (n / (137.01 + n)) * 3;
            }

            //재무점수로 환산
            double point = aPoint + bPoint + cPoint + dPoint + ePoint + fPoint + gPoint + hPoint + iPoint + jPoint + kPoint + lPoint + mPoint + nPoint;
            return point;
        }

        // 가중치 부분의 문제 확인 필요
        public static double getWeight(double value, double avg)
        {
            // ** 가중치의 문제는 평균값이 마이너스 일 때, 
            // ** 결국 계산되어 산출된 값의 앞에 -를 붙여 +를 만드는 것으로 생각해 볼 수 있다.
            double per = 0.0;       // 퍼센트 초기화
            double weight = 0.0;    // 가중치 초기화

            per = (value / avg) - 1;  // 퍼센트 구하기

            if (avg < 0 || (avg < 0 && value < 0))
            {
                per = per * -1;
            }

            if (per > 0.2)
            {
                weight = 1;
            }
            else if (per <= 0.2 && per > 0.1)
            {
                weight = 0.8;
            }
            else if (per <= 0.1 && per > -0.1)
            {
                weight = 0.6;
            }
            else if (per <= -0.1 && per >= -0.2)
            {
                weight = 0.4;
            }
            else
            {
                weight = 0.2;
            }
            return weight;
        }

        // 부채비율을 위한 가중치 계산 메서드
        public static double getReverseWeight(double value, double avg)
        {
            // ** 가중치의 문제는 평균값이 마이너스 일 때, 
            // ** 결국 계산되어 산출된 값의 앞에 -를 붙여 +를 만드는 것으로 생각해 볼 수 있다.
            double per = 0.0;       // 퍼센트 초기화
            double weight = 0.0;    // 가중치 초기화

            per = (value / avg) - 1;  // 퍼센트 구하기

            if (avg < 0 || (avg < 0 && value < 0))
            {
                per = per * -1;
            }

            if (per > 0.2)
            {
                weight = 0.2;
            }
            else if (per <= 0.2 && per > 0.1)
            {
                weight = 0.4;
            }
            else if (per <= 0.1 && per > -0.1)
            {
                weight = 0.6;
            }
            else if (per <= -0.1 && per >= -0.2)
            {
                weight = 0.8;
            }
            else
            {
                weight = 1;
            }
            return weight;
        }
        // * 0411 경영자의 능력 - phm
        public static string getLeadershipScore(int officer)
        {
            if (officer >= 1)
            {
                return "A";
            }
            else
            {
                return "C";
            }
        }

        public static int CalcCheckCount(IList<QuesResult1> checkList)
        {
            int trueCount = 0;
            foreach (var check in checkList)
            {
                if (check.AnsVal == true)
                {
                    trueCount++;
                }
            }

            return trueCount;
        }
    }
}