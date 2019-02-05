using System;
using System.Collections.Generic;
using System.Linq;
using Accord.Controls;
using Deedle;

namespace Data.Analysis
{
    public interface IClassificationFrequencyAnalyser
    {
        Series<string, int> CalculateClassifications(Frame<int, string> frame,
            string classificationColumnName);

        void GenerateTotalsGraph(Series<string, int> values, string title = "Classification Frequency");
    }

    public class ClassificationFrequencyAnalyser : IClassificationFrequencyAnalyser
    {
        public Series<string, int> CalculateClassifications(Frame<int, string> frame,
            string classificationColumnName)
        {
           return frame
                .GetColumn<string>(classificationColumnName)
                .GroupBy(x => x.Value)
                .Select(x => x.Value.KeyCount);
        }

        public void GenerateTotalsGraph(Series<string, int> values, string title = "Classification Frequency")
        {
            var barChart = DataBarBox.Show(
                values.Keys,
                values.Values.Select(Convert.ToDouble).ToArray());

            barChart.SetTitle(title);
        }
    }
}