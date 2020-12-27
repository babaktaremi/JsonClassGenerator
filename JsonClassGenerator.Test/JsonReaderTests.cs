using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json.Linq;
using Xunit;
using Xunit.Abstractions;

namespace JsonClassGenerator.Test
{
    public class JsonReaderTests
    {
        private readonly ITestOutputHelper output;

        public JsonReaderTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void Write_Property_Types()
        {
            var value = @"{
""address"": {
    ""streetAddress"": ""21 2nd Street"",
    ""city"": ""New York"",
    ""state"": ""NY"",
    ""postalCode"": ""10021-3100""
  },
  ""firstName"": ""John"",
  ""lastName"": ""Smith"",
  ""isAlive"": true,
  ""age"": 27
}";


        }

        [Fact]
        public void Write_Generated_Class()
        {
            var cs = ClassGeneratorTest.GenerateClass();

            output.WriteLine(cs);
        }
    }

    
}
