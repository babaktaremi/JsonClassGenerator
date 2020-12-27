using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;


namespace JsonClassGenerator
{
    public class ClassGeneratorTest
    {
        public static string GenerateClass()
        {
            var classSource = new StringBuilder();

            classSource.AppendLine(@"namespace JsonClass.Tools");
            classSource.AppendLine("{");

            var jsonFiles = JsonFileReader.GetJson("F:\\Source Generator Explore\\JsonClassGenerator\\SampleProject\\Settings.json");

            foreach (var jsonFile in jsonFiles)
            {
                classSource.AppendLine($@"public partial class {jsonFile.Key} {{");

                var classObject = (IDictionary<string, object>) JsonParser.Parse(jsonFile.Value);

                foreach (var obj in classObject)
                {
                    if (obj.Value is ExpandoObject e)
                    {
                        classSource.AppendLine($@"public static class {obj.Key} {{");

                        var eValue = (IDictionary<string, object>) e;

                        foreach (var o in eValue)
                        {
                            if (o.Value is string)
                            {
                                classSource.AppendLine($@"public static {o.Value.GetType().FullName} {o.Key} => ""{o.Value}"" ;");
                                continue;
                            }

                            if (o.Value is bool eee)
                            {
                                classSource.AppendLine($@"public static {o.Value.GetType().FullName} {o.Key} => {eee.ToString().ToLower()} ;");
                                continue;

                            }

                            classSource.AppendLine($@"public static {o.Value.GetType().FullName} {o.Key} => {o.Value} ;");
                        }

                        classSource.AppendLine("}");

                        //classSource.AppendLine($@"public static Nested{obj.Key} {obj.Key}= new Nested{obj.Key} ();");

                        continue;
                    }

                    if (obj.Value is string)
                    {
                        classSource.AppendLine($@"public static {obj.Value.GetType().FullName} {obj.Key} => ""{obj.Value}"" ;");
                        continue;
                    }

                    if (obj.Value is bool ee)
                    {
                        classSource.AppendLine($@"public static {obj.Value.GetType().FullName} {obj.Key} => {ee.ToString().ToLower()} ;");
                        continue;
                    }

                    classSource.AppendLine($@"public static {obj.Value.GetType().FullName} {obj.Key} => {obj.Value} ;");
                }

                classSource.AppendLine("}");
            }
            classSource.AppendLine("}");


            return classSource.ToString();
        }
    }
}
