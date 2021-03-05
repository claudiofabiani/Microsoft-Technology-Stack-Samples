using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Model
{
    public class ApiResponseModel
    {
        public int StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public string[] Messages { get; set; }
        public long Id { get; set; }
        public object Data { get; set; }
        public ApiResponseModel()
        {
           
        }
        public ApiResponseModel(int code, bool isSuccess, string[] messages)
        {
            this.StatusCode = code;
            this.IsSuccess = isSuccess;
            this.Messages = messages;
        }

        public ApiResponseModel(int code, bool isSuccess, string[] messages, object data)
        {
            this.StatusCode = code;
            this.IsSuccess = isSuccess;
            this.Messages = messages;
            this.Data = data;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
        }

    }
}
