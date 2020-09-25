using System.Diagnostics;

namespace BarbellAnalysis
{
    [DebuggerDisplay("{Month}: Selic {Selic} / IPCA {Ipca} / Stocks {Stocks} / Dolar {Dolar}")]
    public class BarbellPosition
    {
        public BarbellPosition(MonthDate month, float selic, float ipca, float stocks, float dolar)
        {
            Month = month;
            Selic = selic;
            Ipca = ipca;
            Stocks = stocks;
            Dolar = dolar;
        }

        public MonthDate Month { get; }
        public float Selic { get; }
        public float Ipca { get; }
        public float Stocks { get; }
        public float Dolar { get; }
    }
}
