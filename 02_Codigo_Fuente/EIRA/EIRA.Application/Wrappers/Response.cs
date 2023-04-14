﻿using Newtonsoft.Json;

namespace EIRA.Application.Wrappers
{
    public class Response<T>
    {
        public Response() { }
        public Response(T data, string message = null)
        {
            Succeeded = true;
            Message = message;
            Data = data;
        }

        public Response(string message)
        {
            Succeeded = false;
            Message = message;
        }

        [JsonProperty("succeeded")]
        public bool Succeeded { get; set; }
        [JsonProperty("message")]

        public string Message { get; set; }
        [JsonProperty("errors")]

        public List<string> Errors { get; set; }
        [JsonProperty("data")]

        public T Data { get; set; }
    }
}