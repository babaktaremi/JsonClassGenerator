using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace JsonClassGenerator
{
  public class JsonFileReader
    {
        public static IDictionary<string,string> GetJson(GeneratorExecutionContext context)
        {
            string validExtension = ".json";

            var result=new Dictionary<string,string>();

            foreach (AdditionalText file in context.AdditionalFiles)
            {
                var fileExtension = Path.GetExtension(file.Path);

                if (validExtension.Equals(fileExtension, StringComparison.CurrentCultureIgnoreCase))
                {
                    using StreamReader reader=new StreamReader(file.Path);

                    var fileName = Path.GetFileName(file.Path).Split('.')[0];

                   result.Add(fileName.Capitalize(),reader.ReadToEnd());
                }

            }

            return result;
        }

        public static IDictionary<string, string> GetJson(string filePath)
        {
            string validExtension = ".json";

            var result = new Dictionary<string, string>();

                var fileExtension = Path.GetExtension(filePath);

                if (validExtension.Equals(fileExtension, StringComparison.CurrentCultureIgnoreCase))
                {
                    using StreamReader reader = new StreamReader(filePath);

                    var fileName = Path.GetFileName(filePath).Split('.');

                    result.Add(fileName[0].Capitalize(), reader.ReadToEnd().Trim().Replace('\r',' '));
                }

                return result;
        }
    }
}
