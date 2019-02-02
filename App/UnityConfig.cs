using Common;
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
                .RegisterType<IDataPreparer, DataPreparer>()
                .RegisterType<IStanfordCoreNlpFactory, StanfordCoreNlpFactory>()
                .RegisterType<ITextFormatter, TextFormatter>()
                ;

            return container;
        }
    }
}