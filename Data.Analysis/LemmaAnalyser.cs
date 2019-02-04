using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Common;
using Data.Preparation;
using Deedle;
using edu.stanford.nlp.ling;
using edu.stanford.nlp.pipeline;
using javax.smartcardio;

namespace Data.Analysis
{
    public interface ILemmaAnalyser
    {
        Frame<int, string> CreateLemmaVector(Series<int, string> rows);
    }

    public class LemmaAnalyser : ILemmaAnalyser
    {
        private readonly IStanfordCoreNlpFactory _stanfordCoreNlpFactory;
        private readonly IAppSettings _appSettings;

        private readonly List<string> _stopWords;

        public LemmaAnalyser(IStanfordCoreNlpFactory stanfordCoreNlpFactory, IFileManager fileManager, IAppSettings appSettings)
        {
            _stanfordCoreNlpFactory = stanfordCoreNlpFactory;
            _appSettings = appSettings;

            _stopWords = fileManager.ReadFromFile(_appSettings.StopWordsFilePath)
                .Distinct()
                .Select(x => x.ToLower())
                .ToList();
        }

        public Frame<int, string> CreateLemmaVector(Series<int, string> rows)
        {
            var stanfordCoreNlp = _stanfordCoreNlpFactory.GetStanfordCoreNlp(new Dictionary<string, string>()
            {
                {"annotators", "tokenize, ssplit, pos, lemma"},
                {"ner.useSUTime", "0"}
            });

            var lemmaByRows = rows.GetAllValues().Select((term, index) =>
            {
                var seriesBuilder = new SeriesBuilder<string, int>();

                var annotation = new Annotation(term.Value);
                stanfordCoreNlp.annotate(annotation);

                var tokens = annotation.get(typeof(CoreAnnotations.TokensAnnotation));
               
                var analysedTokens = (tokens as ArrayList)?
                    .Cast<CoreLabel>()
                    .Where(token => !_stopWords.Contains(token.lemma().ToLower()))
                    .Select(token => token.lemma().ToLower())
                    .Distinct();

                if (analysedTokens == null)
                {
                    Console.WriteLine($" Analysed Tokens was null for term: {term.Value}");
                    return KeyValue.Create(index, seriesBuilder.Series);
                }


                foreach (var token in analysedTokens)
                    seriesBuilder.Add(token, 1);

                return KeyValue.Create(index, seriesBuilder.Series);
            });

            var lemmaVector = Frame.FromRows(lemmaByRows).FillMissing(0);

            return lemmaVector;
        }
    }
}