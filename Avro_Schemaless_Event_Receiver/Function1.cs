using Avro;
using Avro_Schemaless_Event_Receiver.Model;
using Avro_Schemaless_Event_Receiver.Resource;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Azure.WebJobs.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;

namespace Avro_Schemaless_Event_Receiver
{
    /// <summary>
    /// This function receives events from an event hub.
    /// The events are avro schemaless events.
    /// The function read the bytes in the event by using Apache.Avro library and return the object entity.
    /// 
    /// The object deserialized must implement the ISpecificRecord interface of the Apache.Avro library.
    /// </summary>
    public static class Function1
    {
        [FunctionName("Function1")]
        public static void Run(
            [EventHubTrigger("%EventHubName%", Connection = "EventHubConnection", ConsumerGroup = "%ConsumerGroupName%")] EventData[] eventHubMessages, 
            Microsoft.Azure.WebJobs.ExecutionContext exCtx,
            PartitionContext PartitionContext,
            TraceWriter log)
        {
            try
            {
                log.Info($"Function1 | Run - functionId: {exCtx.InvocationId}");

                Schema writerObjectSchema = Schema.Parse(SchemaClass.SCHEMA_OBJECT_ENTITY);
                Avro.Specific.SpecificDatumReader<ObjectEntity> r = new Avro.Specific.SpecificDatumReader<ObjectEntity>(writerObjectSchema, writerObjectSchema);
                
                for (int i = 0; i < eventHubMessages.Length; i++)
                {
                    ObjectEntity objectEntityWithSchema = null;

                    using (MemoryStream memStream = new MemoryStream(eventHubMessages[i].GetBytes()))
                    {
                        memStream.Seek(0, SeekOrigin.Begin);
                        objectEntityWithSchema = r.Read(null, new Avro.IO.BinaryDecoder(memStream));
                    }

                    /// Here use the deserialized object for your logic
                    /// Put your code here
                    /// 


                }
                log.Info($"Function1 | Run - functionId: {exCtx.InvocationId}");
            }
            catch (Exception ex)
            {
                log.Error($"Function1 | Run - functionId: {exCtx.InvocationId}. Error StackTrace: {ex.StackTrace}");
                throw;
            }
        }

        public static string GetEnvironmentVariable(string name)
        {
            return Environment.GetEnvironmentVariable(name, EnvironmentVariableTarget.Process);
        }
    }
}