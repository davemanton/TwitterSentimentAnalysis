using System;
using System.Collections.Generic;
using Common;
using Deedle;

namespace Data.Preparation
{
    public interface IDataPreparer
    {
        void Prepare();
    }

    public class DataPreparer : IDataPreparer
    {
        private readonly IAppSettings _appSettings;
        private readonly ITextFormatter _textFormatter;

        public DataPreparer(IAppSettings appSettings,ITextFormatter textFormatter)
        {
            _appSettings = appSettings;
            _textFormatter = textFormatter;
        }

        public void Prepare()
        {
            //var text = "We're going to test our CoreNLP installation!";

            //var properties = new Dictionary<string, string>
            //{
            //    {"annotators", "tokenize, ssplit, pos, lemma"},
            //    {"ner.useSUTime", "0"}
            //};

            //var stanfordCoreNlp = _stanfordCoreNlpFactory.GetStanfordCoreNlp(properties);

            //var annotation = new Annotation(text);
            //stanfordCoreNlp.annotate(annotation);

            //using (var stream = new ByteArrayOutputStream())
            //{
            //    stanfordCoreNlp.prettyPrint(annotation, new PrintWriter(stream));
            //    Console.WriteLine(stream.toString());
            //    stream.close();
            //}

            Console.WriteLine("--- Preparing Data ---");

            var twitterData = Frame.ReadCsv(_appSettings.RawDataPath, hasHeaders: true);
            
            var text = twitterData.GetColumn<string>("text");

            var processedTweets = _textFormatter.FormatText(text);

            twitterData.AddColumn("processed_tweet", processedTweets);

            twitterData.SaveCsv(_appSettings.ProcessedDataPath);

            Console.WriteLine("--- Data Preparation Complete ---");
        }       
    }
}