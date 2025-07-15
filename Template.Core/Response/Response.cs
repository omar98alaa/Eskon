using System.Net;

namespace Template.Core.Response
{
    public class Response<T>
    {
        #region Properties
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public object Meta { get; set; }
        public List<string> Errors { get; set; } = new List<string>();

        // public Dictionary<string, List<string>> ErrorsBag { get; set; } = new Dictionary<string, List<string>>();

        #endregion

        #region Constructors
        public Response()
        {
        }
        public Response(T data, string message = null)
        {
            Succeeded = true;
            Data = data;
            Message = message;
            StatusCode = HttpStatusCode.OK;
        }
        public Response(string message, bool succeeded = false)
        {
            Succeeded = succeeded;
            Message = message;
            StatusCode = succeeded ? HttpStatusCode.OK : HttpStatusCode.BadRequest;
        }
        public Response(T data, HttpStatusCode statusCode, bool succeeded, string message = null, object meta = null)
        {
            Data = data;
            StatusCode = statusCode;
            Succeeded = succeeded;
            Message = message;
            Meta = meta;
        }

        #endregion

        #region Factory Methods
        public static Response<T> Success(T data, string message = null, object meta = null)
        {
            return new Response<T>
            {
                Data = data,
                Succeeded = true,
                Message = message,
                Meta = meta,
                StatusCode = HttpStatusCode.OK
            };
        }
        public static Response<T> Fail(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            return new Response<T>
            {
                Succeeded = false,
                Message = message,
                StatusCode = statusCode
            };
        }
        public static Response<T> Fail(List<string> errors, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            return new Response<T>
            {
                Succeeded = false,
                Errors = errors,
                StatusCode = statusCode
            };
        }

        #endregion

        #region Helper Methods
        public void AddError(string error)
        {
            Errors.Add(error);
        }
        public void AddErrors(List<string> errors)
        {
            Errors.AddRange(errors);
        }

        #endregion
    }
}
