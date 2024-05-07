using System;

namespace NormandiaNetworking.ro.mpp
{
    [Serializable]
    public class Request
    {
        public RequestType _type { get; set; }
        public Object _data { get; set; }

        private Request()
        {
        }

        public class Builder
        {
            private Request _request;
            
            public Builder()
            {
                _request = new Request();
            }
            
            public Builder Type(RequestType type)
            {
                _request._type = type;
                return this;
            }
            
            public Builder Data(Object data)
            {
                _request._data = data;
                return this;
            }

            public Request Build()
            {
                return _request;
            }
        }
        
        public override string ToString()
        {
            return "Request{" +
                   "type=" + _type +
                   ", data=" + _data +
                   '}';
        }
    }
}