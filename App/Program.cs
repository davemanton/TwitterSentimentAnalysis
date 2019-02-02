using System;
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

            dataPreparer.Prepare();

            Console.ReadKey();
        }
    }
}
