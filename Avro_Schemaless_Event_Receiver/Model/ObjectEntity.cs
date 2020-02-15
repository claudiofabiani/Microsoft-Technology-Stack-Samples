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
    public class ObjectEntity : ISpecificRecord
    {
        [DataMember(Name = "stringField")]
        public string StringField;
        [DataMember(Name = "nullableIntField")]
        public int? NullableIntField;
        [DataMember(Name = "listField")]
        public List<SubObjectEntity> ListField;

        public static Schema _SCHEMA = Avro.Schema.Parse(SchemaClass.SCHEMA_OBJECT_ENTITY);

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
                case 0: return this.StringField;
                case 1: return this.NullableIntField;
                case 2: return this.ListField;
                default: throw new AvroRuntimeException("Bad index " + fieldPos + " in Get()");
            };
        }

        public void Put(int fieldPos, object fieldValue)
        {
            switch (fieldPos)
            {
                case 0: this.StringField = (System.String)fieldValue; break;
                case 1: this.NullableIntField = (System.Nullable<int>)fieldValue; break;
                case 2: this.ListField = (List<SubObjectEntity>)fieldValue; break;                
                default: throw new AvroRuntimeException("Bad index " + fieldPos + " in Put()");
            };
        }
    }    
}
