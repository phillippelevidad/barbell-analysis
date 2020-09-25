using System;

namespace BarbellAnalysis
{
    class Program
    {
        static void Main()
        {
            var wallet = BarbellWallet.Open(
                selicAllocation: 0.30f,
                ipcaAllocation: 0.05f,
                stocksAllocation: 0.50f,
                dolarAllocation: 0.15f,
                startingAmount: 100_000f);

            Run(wallet, monthlyDeposit: 5_000f);
        }

        static void Run(BarbellWallet wallet, float monthlyDeposit)
        {
            Variation variation = null;

            foreach (var position in Market.ListBarbellPositions())
            {
                variation = variation == null
                    ? Variation.BeginTracking(position)
                    : variation.ComparingTo(position);

                wallet.UpdateFundsAndMakeDeposit(
                   variation.Selic, variation.Ipca, variation.Stocks, variation.Dolar,
                   monthlyDeposit);

                PrintMonth(position.Month, wallet);
            }

            PrintEndResult(wallet);
        }

        static void PrintMonth(MonthDate month, BarbellWallet wallet)
        {
            Console.WriteLine($"{month}: {wallet.Balance:C0}");
        }

        static void PrintEndResult(BarbellWallet wallet)
        {
            Console.WriteLine();

            Console.WriteLine($"Invested:\t{wallet.InvestedAmount:C0}");
            Console.WriteLine($"Balance:\t{wallet.Balance:C0}");
            Console.WriteLine($"Profit/Loss:\t{wallet.ProfitOrLoss:C0}");
        }
    }
}
