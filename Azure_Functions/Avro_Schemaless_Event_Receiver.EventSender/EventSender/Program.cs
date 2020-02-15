using Avro;
using Avro.Specific;
using Avro_Schemaless_Event_Receiver.Model;
using Avro_Schemaless_Event_Receiver.Resource;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging;

namespace EventSender
{
    /// <summary>
    /// This project is used to test the function in the Avro_Schemaless_Event_Receiver project.
    /// </summary>
    public class Program
    {
        static void Main(string[] args)
        {
            ObjectEntity objectEntity = new ObjectEntity();
            objectEntity.NullableIntField = 10;
            objectEntity.StringField = "teststring";
            objectEntity.ListField = new List<SubObjectEntity>();
            objectEntity.ListField.Add(new SubObjectEntity{IntField = 1,DoubleField = 1.1});

            Schema schema = Schema.Parse(SchemaClass.SCHEMA_OBJECT_ENTITY);
            SpecificDatumWriter<ObjectEntity> w = new SpecificDatumWriter<ObjectEntity>(schema);

            byte[] finalbytes = null;
            using (MemoryStream memStream = new MemoryStream())
            {
                w.Write(objectEntity, new Avro.IO.BinaryEncoder(memStream));
                memStream.Seek(0, SeekOrigin.Begin);
                finalbytes = memStream.ToArray();
            }

            EventData eventToSend = new EventData(finalbytes);
            EventHubClient eventHubClient = EventHubClient.CreateFromConnectionString(
                "<eventhubconnection>",
                "<eventhubname>"
                );
            eventHubClient.SendAsync(eventToSend).Wait();
        }
    }
}
