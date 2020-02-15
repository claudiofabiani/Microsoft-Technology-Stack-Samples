using Avro;
using Avro.Specific;
using Avro_Schemaless_Event_Receiver.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Avro_Schemaless_Event_Receiver.Model
{
    [DataContract]
    public class SubObjectEntity : ISpecificRecord
    {
        [DataMember(Name = "doubleField")]
        public double DoubleField;
        [DataMember(Name = "intField")]
        public int IntField;

        public static Schema _SCHEMA = Avro.Schema.Parse(SchemaClass.SCHEMA_SUBOBJECT_ENTITY);

        public virtual Schema Schema
        {
            get
            {
                return ObjectEntity._SCHEMA;
            }
        }

        public object Get(int fieldPos)
        {
            switch (fieldPos)
            {
                case 0: return this.DoubleField;
                case 1: return this.IntField;
                default: throw new AvroRuntimeException("Bad index " + fieldPos + " in Get()");
            };
        }

        public void Put(int fieldPos, object fieldValue)
        {
            switch (fieldPos)
            {
                case 0: this.DoubleField = (System.Double)fieldValue; break;
                case 1: this.IntField = (System.Int32)fieldValue; break;
                default: throw new AvroRuntimeException("Bad index " + fieldPos + " in Put()");
            };
        }
    }
}
