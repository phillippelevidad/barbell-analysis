using System;

namespace BarbellAnalysis
{
    public class BarbellWallet
    {
        private readonly float selicAllocation;
        private readonly float ipcaAllocation;
        private readonly float stocksAllocation;
        private readonly float dolarAllocation;

        public float Selic { get; private set; }
        public float Ipca { get; private set; }
        public float Stocks { get; private set; }
        public float Dolar { get; private set; }
        public float InvestedAmount { get; private set; }

        public float Balance => Selic + Ipca + Stocks + Dolar;
        public float ProfitOrLoss => Balance - InvestedAmount;

        private BarbellWallet(float selicAllocation, float ipcaAllocation, float stocksAllocation, float dolarAllocation)
        {
            if (selicAllocation + ipcaAllocation + stocksAllocation + dolarAllocation != 1f)
                throw new ArgumentException("The total allocation must be 100%");

            this.selicAllocation = selicAllocation;
            this.ipcaAllocation = ipcaAllocation;
            this.stocksAllocation = stocksAllocation;
            this.dolarAllocation = dolarAllocation;
        }

        public static BarbellWallet Open(float selicAllocation, float ipcaAllocation, float stocksAllocation, float dolarAllocation, float startingAmount = 0)
        {
            var wallet = new BarbellWallet(selicAllocation, ipcaAllocation, stocksAllocation, dolarAllocation);
            wallet.AllocateDeposit(startingAmount);
            return wallet;
        }

        public void UpdateFundsAndMakeDeposit(float selicVariation, float ipcaVariation, float stocksVariation, float dolarVariation, float depositAmount)
        {
            UpdateFunds(selicVariation, ipcaVariation, stocksVariation, dolarVariation);
            AllocateDeposit(depositAmount);
        }

        private void UpdateFunds(float selicVariation, float ipcaVariation, float stocksVariation, float dolarVariation)
        {
            Selic *= selicVariation;
            Ipca *= ipcaVariation;
            Stocks *= stocksVariation;
            Dolar *= dolarVariation;
        }

        private void AllocateDeposit(float amount)
        {
            var newTotal = Balance + amount;

            Selic = newTotal * selicAllocation;
            Ipca = newTotal * ipcaAllocation;
            Stocks = newTotal * stocksAllocation;
            Dolar = newTotal * dolarAllocation;

            InvestedAmount += amount;
        }
    }
}
