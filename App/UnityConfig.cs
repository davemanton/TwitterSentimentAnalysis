using Common;
using Data.Analysis;
using Data.Preparation;
using Unity;

namespace App
{
    public static class UnityConfig
    {
        public static UnityContainer RegisterDependencies()
        {
            var container = new UnityContainer();

            container
                .RegisterType<IAppSettings, AppSettings>()
                .RegisterType<IFileManager, FileManager>()
                .RegisterType<IDataPreparer, DataPreparer>()
                .RegisterType<ITextFormatter, TextFormatter>()
                .RegisterType<IDataAnalyser, DataAnalyser>()
                .RegisterType<IStanfordCoreNlpFactory, StanfordCoreNlpFactory>()
                .RegisterType<IClassificationFrequencyAnalyser, ClassificationFrequencyAnalyser>()
                .RegisterType<ILemmaAnalyser, LemmaAnalyser>()
                ;

            return container;
        }
    }
}