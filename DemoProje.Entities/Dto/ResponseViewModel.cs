using System;

namespace DemoProje.Entities.Dto
{
    [Serializable]
    public class ResponseViewModel
    {
        public bool IsSuccess { get; set; } = true;
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
