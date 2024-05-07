using System;

namespace NormandiaNetworking.ro.mpp
{
    [Serializable]
    public class Response
    {
        public ResponseType type { get; set; }
        public Object data { get; set; }

        private Response()
        {
        }

        public class Builder
        {
            private Response _response;
            
            public Builder()
            {
                _response = new Response();
            }
            
            public Builder SetType(ResponseType type)
            {
                _response.type = type;
                return this;
            }

            public Builder SetData(Object data)
            {
                _response.data = data;
                return this;
            }
            
            public Response Build()
            {
                return _response;
            }
            
        }
        
        public override string ToString()
        {
            return "Response{" +
                   "type=" + type +
                   ", data=" + data +
                   '}';
        }
    }
}