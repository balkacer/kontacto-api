using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kontacto_api.Tools.Enums;

namespace kontacto_api.Tools
{

    public class Response<Type> where Type : class
    {
        public Response( string Message, ResponseCodeEnum Code )
        {
            this.Message = Message;
            this.Code = Code;
        }

        public Response( string Message, ResponseCodeEnum Code, Type Data )
        {
            this.Message = Message;
            this.Code = Code;
            this.Data = Data;
        }

        public string Message { get; set; }
        public ResponseCodeEnum Code { get; set; }
        
        public Type Data { get; set; }
    }
}
