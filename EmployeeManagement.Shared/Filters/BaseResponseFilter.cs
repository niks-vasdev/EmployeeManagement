using EmployeeManagement.Shared.Response;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;


namespace EmployeeManagement.Shared.Filters
{
    public class BaseResponseFilter : IActionFilter, IExceptionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Result is ObjectResult objectResult)
            {
                var statuscode = objectResult.StatusCode ?? StatusCodes.Status200OK;
                var apiResponse = new BaseResponse<object>
                {
                    Message = objectResult.Value
                };

                if (apiResponse.Message is not null && apiResponse.Message is ValidationResult)
                {
                    var validate = apiResponse.Message as ValidationResult;
                    apiResponse.Message = string.Join(" & ", validate?.Errors.Select(x => x.ErrorMessage));
                }
                if (statuscode == 200)
                {
                    dynamic objectRes = objectResult.Value;
                    var messagePropertyInfo = objectRes.GetType().GetProperty("Message");
                    var dataPropertyInfo = objectRes.GetType().GetProperty("Data");
                    apiResponse.Message = messagePropertyInfo.GetValue(objectRes, null);
                    apiResponse.Data = dataPropertyInfo.GetValue(objectRes, null);
                    apiResponse.IsSuccess = true;
                }

                if (IsErrorStatusCode(statuscode))
                {
                    apiResponse.Status = "Error";
                }
                else
                {
                    apiResponse.Status = "Success";

                }

                context.Result = new ObjectResult(apiResponse)
                {
                    StatusCode = statuscode
                };


            }
            else if (context.Result is StatusCodeResult statusCodeResult)
            {
                var apiResponse = new BaseResponse<object>
                {
                    Status = "Success",
                };
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {

        }

        public void OnException(ExceptionContext context)
        {
            var apiResponse = new BaseResponse<object>
            {
                Status = "Error",
                Message = context.Exception.Message,
                Data = null,
                IsSuccess = false
            };

            context.Result = new ObjectResult(apiResponse)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };

            //NOTE : Marking the exception as handled,
            context.ExceptionHandled = true;
        }

        private bool IsErrorStatusCode(int statusCode)
        {
            //Any status code 400 or above is considered an error
            return statusCode > 200;
        }
    }
}
