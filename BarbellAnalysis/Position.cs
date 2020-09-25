using System.Diagnostics;

namespace BarbellAnalysis
{
    [DebuggerDisplay("{Month}: {Value}")]
    public class Position
    {
        public Position(MonthDate month, float value)
        {
            Month = month;
            Value = value;
        }

        public MonthDate Month { get; }
        public float Value { get; }
    }
}
