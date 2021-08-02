namespace VnCovidStatistics.API.Responses
{
    public class ApiResponse<T>
    {
        public ApiResponse(T data)
        {
            this.Data = data;
        }
        
        public T Data { get; set; }
        
    }
}