using System.Collections.Generic;

namespace StratmanMedia.ResponseObjects
{
    public class Response
    {
        public List<string> Messages { get; } = new List<string>();
        public bool IsSuccess => Messages.Count == 0;

        public Response() { }

        public Response(string message)
        {
            Messages.Add(message);
        }

        public Response(IEnumerable<string> messages)
        {
            Messages.AddRange(messages);
        }
    }

    public class Response<T> : Response
    {
        public T Data { get; set; }

        public Response()
        {
            Data = default;
        }

        public Response(string message) : base(message)
        {
            Data = default;
        }

        public Response(IEnumerable<string> messages) : base(messages)
        {
            Data = default;
        }

        public Response(T data)
        {
            Data = data;
        }

        public Response(string message, T data) : base(message)
        {
            Data = data;
        }

        public Response(IEnumerable<string> messages, T data) : base(messages)
        {
            Data = data;
        }
    }
}