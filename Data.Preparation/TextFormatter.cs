using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Deedle;

namespace Data.Preparation
{
    public interface ITextFormatter
    {
        IEnumerable<string> FormatText(Series<int, string> text);
    }

    public class TextFormatter : ITextFormatter
    {
        private const string UrlPattern = @"https?:\/\/\S+\b|www\.(\w+\.)+\S*";
        private const string UserIdPattern = @"@\w+";
        private const string NumberPattern = @"[-+]?[.\d]*[\d]+[:,.\d]*";
        private const string HashTagPattern = @"#";
        private const string HeartPattern = "<3";
        private const string RepeatedPunctuationPattern = @"([!?.]){2,}";
        private const string ElongatedWordsPattern = @"\b(\S*?)(.)\2{2,}\b";
        private const string EyesPattern = @"[8:=;]";
        private const string NosePattern = @"['`\-]?";
        private readonly string SmileyFacePattern = $"{EyesPattern}{NosePattern}[)dD]+|[)dD]+{NosePattern}{EyesPattern}";
        private readonly string LolFacePattern = $@"{EyesPattern}{NosePattern}[pP]+";
        private readonly string SadFacePattern = $@"{EyesPattern}{NosePattern}\(+|\)+{NosePattern}{EyesPattern}";
        private readonly string NeutralFacePattern = $@"{EyesPattern}{NosePattern}[\/|l*]";

        public IEnumerable<string> FormatText(Series<int, string> text)
        {
            var regex = new Regex($"{UrlPattern}|{UserIdPattern}|{NumberPattern}|{HashTagPattern}");
            var smileyFaceRegex = new Regex(SmileyFacePattern);
            var lolFaceRegex = new Regex(LolFacePattern);
            var sadFaceRegex = new Regex(SadFacePattern);
            var neutralFaceRegex = new Regex(NeutralFacePattern);
            var heartRegex = new Regex(HeartPattern);
            var repeatedPunctuationRegex = new Regex(RepeatedPunctuationPattern);
            var elongatedWordsRegex = new Regex(ElongatedWordsPattern);

            var cleanedText = text.GetAllValues().Select(value =>
            {
                if (!value.HasValue || string.IsNullOrWhiteSpace(value.Value))
                    return value.ValueOrDefault;

                var textValue = regex.Replace(value.ValueOrDefault ?? string.Empty, string.Empty);
                textValue = smileyFaceRegex.Replace(textValue, " emo_smiley");
                textValue = lolFaceRegex.Replace(textValue, " emo_lol");
                textValue = sadFaceRegex.Replace(textValue, " emo_sad");
                textValue = neutralFaceRegex.Replace(textValue, " emo_neutral");
                textValue = heartRegex.Replace(textValue, " emo_heart");
                textValue = repeatedPunctuationRegex.Replace(textValue, " $1_repeat");
                textValue = elongatedWordsRegex.Replace(textValue, " $1$2_emphasised");

                return textValue;
            }).ToList();

            return cleanedText;
        }
    }
}