namespace EmployeeManagement.Shared.Response
{
    public class BaseResponse<T>
    {
        public string Status {  get; set; }
        public dynamic Message {  get; set; }
        public bool IsSuccess { get; set; }
        public T? Data { get; set; }
    }
}
