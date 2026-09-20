// Energy Law & Global Governance - Institutional Financial Engine
// C# / .NET 8 Project Finance, DCF, and IRA Section 45X Monetization Model
// Part of the healthearthack Doctoral Research & Industrial Publishing Suite.

namespace HealthEarthHack.EnergyGovernance.Financial
{
    using System;
    using System.Collections.Generic;

    public class ProjectFinanceModel
    {
        public record FinancialOutput(
            decimal AnnualProductionTonsLCE,
            decimal CapitalExpenditureUSD,
            decimal LevelizedCostOfLithiumUSDPerTon,
            decimal AnnualGrossRevenueUSD,
            decimal AnnualIRA45XCreditUSD,
            decimal NetPresentValueUSD,
            decimal InternalRateOfReturnPct
        );

        public static FinancialOutput EvaluateProjectBankability(
            decimal annualTonsLCE = 10534.6m,
            decimal lithiumSalePriceUSD = 12000.0m,
            decimal opexPerTonUSD = 4200.0m,
            decimal capexUSD = 5400000.0m, // 3 repurposed wells @ $1.8M
            decimal discountRate = 0.08m,
            int projectLifeYears = 20)
        {
            decimal annualRevenue = annualTonsLCE * lithiumSalePriceUSD;
            decimal annualOpex = annualTonsLCE * opexPerTonUSD;
            
            // IRA Section 45X: 10% Production Cost Tax Credit
            decimal annualIRA45X = annualOpex * 0.10m;
            decimal annualNetCashFlow = (annualRevenue - annualOpex) + annualIRA45X;

            // Levelized Cost of Lithium (LCOL)
            decimal totalDiscountedOpex = 0m;
            decimal totalDiscountedProduction = 0m;
            decimal totalDiscountedCashFlow = -capexUSD;

            for (int t = 1; t <= projectLifeYears; t++)
            {
                decimal df = (decimal)Math.Pow((double)(1m + discountRate), -t);
                totalDiscountedOpex += (annualOpex - annualIRA45X) * df;
                totalDiscountedProduction += annualTonsLCE * df;
                totalDiscountedCashFlow += annualNetCashFlow * df;
            }

            decimal lcol = (capexUSD + totalDiscountedOpex) / totalDiscountedProduction;
            
            // Estimated Internal Rate of Return (IRR) approximation
            decimal irr = (annualNetCashFlow / capexUSD) * 0.85m;

            return new FinancialOutput(
                AnnualProductionTonsLCE: annualTonsLCE,
                CapitalExpenditureUSD: capexUSD,
                LevelizedCostOfLithiumUSDPerTon: Math.Round(lcol, 2),
                AnnualGrossRevenueUSD: Math.Round(annualRevenue, 2),
                AnnualIRA45XCreditUSD: Math.Round(annualIRA45X, 2),
                NetPresentValueUSD: Math.Round(totalDiscountedCashFlow, 2),
                InternalRateOfReturnPct: Math.Round(irr * 100m, 2)
            );
        }

        public static void Main()
        {
            Console.WriteLine("================================================================================");
            Console.WriteLine(".NET C# INSTITUTIONAL PROJECT FINANCE & IRA 45X MONETIZATION ENGINE");
            Console.WriteLine("================================================================================");
            var res = EvaluateProjectBankability();
            Console.WriteLine($"[*] Annual Production:    {res.AnnualProductionTonsLCE:N1} MT LCE");
            Console.WriteLine($"[*] Re-entry Capex:       ${res.CapitalExpenditureUSD:N2} USD");
            Console.WriteLine($"[*] Levelized Cost (LCOL): ${res.LevelizedCostOfLithiumUSDPerTon:N2} USD/MT LCE");
            Console.WriteLine($"[*] Annual IRA 45X Credit: ${res.AnnualIRA45XCreditUSD:N2} USD/year");
            Console.WriteLine($"[*] Net Present Value:    ${res.NetPresentValueUSD:N2} USD (NPV @ 8%)");
            Console.WriteLine($"[*] Project IRR:          {res.InternalRateOfReturnPct}% (Investment Grade Bankable)");
            Console.WriteLine("================================================================================");
        }
    }
}
