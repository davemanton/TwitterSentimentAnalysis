using System;
using System.Collections.Generic;
using System.IO;
using Common;
using edu.stanford.nlp.pipeline;
using java.util;

namespace Data.Preparation
{
    public interface IStanfordCoreNlpFactory
    {
        StanfordCoreNLP GetStanfordCoreNlp(Dictionary<string, string> properties);
    }

    public class StanfordCoreNlpFactory : IStanfordCoreNlpFactory
    {
        private readonly IAppSettings _appSettings;

        public StanfordCoreNlpFactory(IAppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        public StanfordCoreNLP GetStanfordCoreNlp(Dictionary<string, string> properties)
        {

            var nlpProperties = new Properties();

            foreach (var property in properties)            
                nlpProperties.setProperty(property.Key, property.Value);
            
            var curDir = Environment.CurrentDirectory;

            Directory.SetCurrentDirectory(_appSettings.StanfordModelDirectory);
            var stanfordCoreNlp = new StanfordCoreNLP(nlpProperties);

            Directory.SetCurrentDirectory(curDir);

            return stanfordCoreNlp;
        }
    }
}
