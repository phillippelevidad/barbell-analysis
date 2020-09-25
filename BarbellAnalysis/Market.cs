using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace BarbellAnalysis
{
    public static class Market
    {
        public static IReadOnlyList<BarbellPosition> ListBarbellPositions()
        {
            var selicPositions = ReadPositions(Resources.SELIC).Select(p => new Position(p.Month, p.Value / 100f)).ToList();
            var ipcaPositions = ReadPositions(Resources.IPCA).Select(p => new Position(p.Month, p.Value / 12f / 100f)).ToList();
            var stocksPositions = ReadPositions(Resources.BOVA11);
            var dolarPositions = ReadPositions(Resources.USD);

            var barbell = new List<BarbellPosition>(selicPositions.Count);

            var lastMonth = selicPositions.Last().Month;
            for (var month = selicPositions.First().Month; month <= lastMonth; month = month.Next())
            {
                var selic = selicPositions.SingleOrDefault(item => item.Month == month);
                if (selic == null) continue;

                var ipca = ipcaPositions.SingleOrDefault(item => item.Month == month);
                if (ipca == null) continue;

                var stocks = stocksPositions.SingleOrDefault(item => item.Month == month);
                if (stocks == null) continue;

                var dolar = dolarPositions.SingleOrDefault(item => item.Month == month);
                if (dolar == null) continue;

                barbell.Add(new BarbellPosition(month,
                    selic.Value, ipca.Value, stocks.Value, dolar.Value));
            }

            return barbell;
        }

        private static IReadOnlyList<Position> ReadPositions(string file)
        {
            return ReadPositions(Encoding.UTF8.GetBytes(file));
        }

        private static IReadOnlyList<Position> ReadPositions(byte[] file)
        {
            using var stream = new MemoryStream(file);
            using var reader = new StreamReader(stream);
            using var csv = new CsvReader(reader, CultureInfo.CreateSpecificCulture("pt-BR"));

            csv.Configuration.HasHeaderRecord = false;
            csv.Configuration.Delimiter = ",";

            return csv
                .GetRecords<Record>()
                .Select(record => record.AsPosition())
                .GroupBy(position => position.Month)
                .Select(group => new Position(group.Key, group.Average(item => item.Value)))
                .OrderBy(position => position.Month)
                .ToList();
        }

        private class Record
        {
            public DateTime Date { get; set; }
            public float Value { get; set; }
            public Position AsPosition() => new Position(MonthDate.From(Date), Value);
        }
    }
}
