using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avro_Schemaless_Event_Receiver.Resource
{
    public class SchemaClass
    {
        public const string SCHEMA_OBJECT_ENTITY = @"{
          ""type"": ""record"",
          ""name"": ""ObjectEntity"",
          ""namespace"": ""Avro_Schemaless_Event_Receiver.Model"",
          ""fields"": [
            {
              ""name"": ""stringField"",
              ""type"": ""string"",
              ""default"": null
            },
            {
              ""name"": ""nullableIntField"",
              ""type"": [
                ""null"",
                ""int""
                ],
                ""default"": null
            },
            {
              ""name"": ""listField"",
              ""type"": {
                ""type"": ""array"",
                ""items"": {
                  ""type"": ""record"",
                  ""name"": ""SubObjectEntity"",
                  ""fields"": [
                    {
                      ""name"": ""doubleField"",
                      ""type"": ""double"",
                        ""default"": null
                    },
                    {
                      ""name"": ""intField"",
                      ""type"": ""int"",
                        ""default"": null
                    }
                  ]
                }
              }
            }
          ]
        }";

        public const string SCHEMA_SUBOBJECT_ENTITY = @"{
          ""type"": ""record"",
          ""name"": ""SubObjectEntity"",
          ""namespace"": ""Avro_Schemaless_Event_Receiver.Model"",
          ""fields"": [
            {
                ""name"": ""doubleField"",
                ""type"": ""double"",
                ""default"": null
            },
            {
                ""name"": ""intField"",
                ""type"": ""int"",
                ""default"": null
            }
            ]
        }";
    }
}
