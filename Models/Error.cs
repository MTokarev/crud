using crud.Enums;

namespace crud.Models
{
    public class Error
    {
        public ResponseErrorTypes ErrorType { get; set; }
        public string Message { get; set; }
        public Error InnerError { get; set; }
    }
}
