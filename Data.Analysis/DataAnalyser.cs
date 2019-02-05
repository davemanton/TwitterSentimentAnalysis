using System;
using Common;
using Deedle;

namespace Data.Analysis 
{
    public interface IDataAnalyser
    {
        void Analyse();
    }

    public class DataAnalyser : IDataAnalyser
    {
        private readonly IAppSettings _appSettings;
        private readonly IClassificationFrequencyAnalyser _classificationFrequencyAnalyser;

        public DataAnalyser(IAppSettings appSettings, IClassificationFrequencyAnalyser classificationFrequencyAnalyser)
        {
            _appSettings = appSettings;
            _classificationFrequencyAnalyser = classificationFrequencyAnalyser;
        }

        public void Analyse()
        {
            Console.WriteLine("--- Data Analysis Started ---");
            var processedDataFrame = Frame.ReadCsv(_appSettings.ProcessedDataPath);

            var sentimentFreqency =
                _classificationFrequencyAnalyser.CalculateClassifications(processedDataFrame, "airline_sentiment");

            _classificationFrequencyAnalyser.GenerateTotalsGraph(sentimentFreqency, "Sentiment Distribution");

            Console.WriteLine("--- Data Analysis Complete ---");
        }
    }
}
