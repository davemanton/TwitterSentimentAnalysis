using System;
using System.Linq;
using Data.Analysis;
using Data.Preparation;
using Unity;

namespace App
{
    class Program
    {
        static void Main(string[] args)
        {
            var dependencies = UnityConfig.RegisterDependencies();

            var dataPreparer = dependencies.Resolve<IDataPreparer>();
            var dataAnalyser = dependencies.Resolve<IDataAnalyser>();

            var operation = args != null && args.Any() ? args.First().ToLower() : string.Empty;

            switch (operation)
            {
                case "prepare":
                    dataPreparer.Prepare();
                    break;
                case "analyse":
                    dataAnalyser.Analyse();
                    break;
                default:
                    dataPreparer.Prepare();
                    dataAnalyser.Analyse();
                    break;
            }

            

            Console.ReadKey();
        }
    }
}
