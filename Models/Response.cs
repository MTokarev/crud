namespace crud.Models
{
    public class Response<T> 
        where T : class
    {
        public Error Error { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }
        public bool HasError => Error != null;
    }
}
