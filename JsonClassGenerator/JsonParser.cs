using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;


namespace JsonClassGenerator
{
   public class JsonParser
    {
        public static ExpandoObject Parse(string json)
        {
            
            var obj=new ExpandoObject();

            if(string.IsNullOrEmpty(json))
                throw new ArgumentNullException("Provided json is null");

            if(!JsonValidation(json))
                throw new InvalidDataException("Provided Json Format not Correct");

            var jsonLines = json.Split('\n');

            StringBuilder sb=new StringBuilder();
            
            for (int i = 1; i < jsonLines.Length-1; i++)
            {

                if (jsonLines[i].Split(':')[1].Trim().StartsWith("{"))
                {
                    do
                    {
                        sb.AppendLine(jsonLines[i]);
                        i++;
                    } while (!jsonLines[i].Contains("}"));

                    var complexJson = sb.ToString().Split('\n').ToList();

                    AddComplexJsonData(complexJson,obj);
                    sb.Clear();
                }

                else
                {
                    ShapeJsonToObject(jsonLines[i], obj);
                }
            }

            return obj;
        }


        private static bool JsonValidation(string json)
        {
            if (!json.Trim().StartsWith("{") || !json.EndsWith("}"))
                return false;

            return true;
        }

        private static void ShapeJsonToObject(string jsonData, ExpandoObject obj)
        {
            var data = jsonData.Replace(',',' ').Trim().Replace('\r',' ').Replace('"',' ').Split(':');

            if(data[1].Equals("{") || data[1].Equals("["))
                return;


            if (int.TryParse(data[1],out int intValue))
            {
                ((IDictionary<string, object>)obj).Add(data[0].Capitalize(),intValue);
                return;
            }

            if (bool.TryParse(data[1], out var boolValue))
            {
                ((IDictionary<string, object>)obj).Add(data[0].Capitalize(), boolValue);
                return;
            }

            if (data[1].Equals("null", StringComparison.CurrentCultureIgnoreCase))
            {
                ((IDictionary<string, object>)obj).Add(data[0].Capitalize(), default);
                return;
            }


            ((IDictionary<string, object>)obj).Add(data[0].Capitalize(), data[1]);
            
        }

        private static void AddComplexJsonData(List<string> jsonData, ExpandoObject obj)
        {
            var objectName = jsonData.First().Split(':')[0];

            var complexObject=new ExpandoObject();

            for (int i = 1; i < jsonData.Count; i++)
            {
                if(string.IsNullOrEmpty(jsonData[i]))
                    continue;

                ShapeJsonToObject(jsonData[i],complexObject);
            }

            ((IDictionary<string, object>)obj).Add(objectName.Replace('"',' ').Trim().Capitalize(), complexObject);
        }

        //private static string Capitalize(string input) 
    }
}
