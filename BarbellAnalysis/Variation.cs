namespace BarbellAnalysis
{
    public class Variation
    {
        private readonly BarbellPosition previous;
        private readonly BarbellPosition current;

        private Variation(BarbellPosition previous, BarbellPosition current)
        {
            this.previous = previous;
            this.current = current;
        }

        public static Variation BeginTracking(BarbellPosition startingPosition)
        {
            return new Variation(null, startingPosition);
        }

        public float Selic => 1f + current.Selic;
        public float Ipca => previous == null ? 1f : 1f + current.Ipca - previous.Ipca;
        public float Stocks => previous == null ? 1f : current.Stocks / previous.Stocks;
        public float Dolar => previous == null ? 1f : current.Dolar / previous.Dolar;

        public Variation ComparingTo(BarbellPosition newPosition)
        {
            return new Variation(previous: current, current: newPosition);
        }
    }
}
