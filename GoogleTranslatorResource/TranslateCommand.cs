using Google.Apis.Services;
using Google.Apis.Translate.v2;
using ManyConsole;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace GoogleTranslatorResource
{
    public class TranslateCommand : ConsoleCommand
    {
        public string Source { get; set; }
        public string Target { get; set; }
        public string ApiKey { get; set; }
        public string ApplicationName { get; set; }

        private TranslateService Service;

        public TranslateCommand()
        {
            IsCommand("Translate", "Quick translate a resource file.");

            HasRequiredOption("s|source=", "The full path of the source file.", p => Source = p);

            HasRequiredOption("t|target=", "The target language.", p => Target = p);

            HasRequiredOption("k|apikey=", "The target language.", p => ApiKey = p);

            HasRequiredOption("n|appname=", "The target language.", p => ApplicationName = p);
        }

        public override int Run(string[] remainingArguments)
        {
            Service = new TranslateService(new BaseClientService.Initializer()
            {
                ApiKey = ApiKey,
                ApplicationName = ApplicationName
            });

            if (Source.Contains(".resx") && File.Exists(Source))
            {
                using (ResXResourceWriter resx = new ResXResourceWriter(GetNewFileName(Source, Target)))
                {
                    foreach (var entry in ReadResourceValues(Source))
                    {
                        string translatedValue = Translate(entry.Value.ToString(), Target);

                        resx.AddResource(entry.Key.ToString(), translatedValue);
                    }
                }
            }

            return 0;
        }

        private string GetNewFileName(string source, string target)
        {
            string filename = Path.GetFileName(source);
            var names = filename.Split('.').ToList();
            names.Insert(names.Count() - 1, target);
            return source.Replace(filename, string.Join(".", names));
        }

        private string Translate(string value, string language)
        {
            string[] srcText = new[] { value };
            var response = Service.Translations.List(srcText, language).ExecuteAsync().Result;

            foreach (var translation in response.Translations)
            {
                return translation.TranslatedText;
            }

            return null;
        }

        private Dictionary<string, string> ReadResourceValues(string filename)
        {
            var results = new Dictionary<string, string>();
            using (ResXResourceReader rsxr = new ResXResourceReader(filename))
            {
                IDictionaryEnumerator id = rsxr.GetEnumerator();
                foreach (DictionaryEntry d in rsxr)
                {
                    results.Add(d.Key.ToString(), d.Value.ToString());
                }
            }

            return results;
        }
    }
}
