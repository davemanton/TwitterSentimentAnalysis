using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public interface IAppSettings
    {
        string StanfordModelDirectory { get; }
        string RawDataPath { get; }
        string ProcessedDataPath { get; }
        string StopWordsFilePath { get; }
    }

    public class AppSettings : IAppSettings
    {
        public string StanfordModelDirectory => GetAppSetting("StanfordModelDirectory");
        public string RawDataPath => Path.Combine(RawDataDirectory, GetAppSetting("RawDataFilename"));
        public string ProcessedDataPath => Path.Combine(ProcessedDataDirectory, GetAppSetting("ProcessedDataFilename"));
        public string StopWordsFilePath => GetAppSetting("StopWordsFilePath");

        private static string RawDataDirectory => GetAppSetting("RawDataDirectory");
        private static string ProcessedDataDirectory => GetAppSetting("ProcessedDataDirectory");

        private static string GetAppSetting(string key) => ConfigurationManager.AppSettings[key];
    }
}
