using System.Net;

namespace Eskon.Core.Response
{
    public class ResponseHandler
    {
        #region Success Responses

        // For single items
        public Response<T> Success<T>(T data, string message = "Operation succeeded", object meta = null)
        {
            return new Response<T>()
            {
                Data = data,
                StatusCode = HttpStatusCode.OK,
                Succeeded = true,
                Message = message,
                Meta = meta
            };
        }

        public Response<T> Created<T>(T data, string message = "Resources created successfully", object meta = null)
        {
            return new Response<T>()
            {
                Data = data,
                StatusCode = HttpStatusCode.Created,
                Succeeded = true,
                Message = message,
                Meta = meta
            };
        }

        // For collections
        public Response<List<T>> Success<T>(List<T> data, string message = "Operation succeeded", object meta = null)
        {
            return new Response<List<T>>()
            {
                Data = data,
                StatusCode = HttpStatusCode.OK,
                Succeeded = true,
                Message = message,
                Meta = meta
            };
        }

        public Response<List<T>> Created<T>(List<T> data, string message = "Resources created successfully", object meta = null)
        {
            return new Response<List<T>>()
            {
                Data = data,
                StatusCode = HttpStatusCode.Created,
                Succeeded = true,
                Message = message,
                Meta = meta
            };
        }

        #endregion

        #region Paginated Responses
        public Response<Paginated<T>> PaginatedSuccess<T>(
            List<T> data,
            int pageNumber,
            int pageSize,
            int totalRecords,
            string message = "Data retrieved successfully")
        {
            return new Response<Paginated<T>>()
            {
                Data = new Paginated<T>(data, pageNumber, pageSize, totalRecords),
                StatusCode = HttpStatusCode.OK,
                Succeeded = true,
                Message = message
            };
        }

        #endregion

        #region Error Responses
        public Response<T> Unauthorized<T>()
        {
            return new Response<T>()
            {
                StatusCode = System.Net.HttpStatusCode.Unauthorized,
                Succeeded = false,
                Message = "UnAuthorized"
            };
        }
        public Response<T> Forbidden<T>()
        {
            return new Response<T>()
            {
                StatusCode = System.Net.HttpStatusCode.Forbidden,
                Succeeded = false,
                Message = "Forbidden"
            };
        }
        public Response<T> BadRequest<T>(string Message = null)
        {
            return new Response<T>()
            {
                StatusCode = System.Net.HttpStatusCode.BadRequest,
                Succeeded = false,
                Message = Message == null ? "Bad Request" : Message,
                
            };
        }

        public Response<T> BadRequest<T>(List<string>? ErrorsList = null, string? Message = null)
        {
            return new Response<T>
            {
                StatusCode = HttpStatusCode.BadRequest,
                Succeeded = false,
                Message = Message == null ? "Bad Request" : Message,
                Errors = ErrorsList,                
            };
        }

        public Response<T> NotFound<T>(string message = null)
        {
            return new Response<T>()
            {
                StatusCode = System.Net.HttpStatusCode.NotFound,
                Succeeded = false,
                Message = message == null ? "Not Found" : message
            };
        }
        #endregion

        #region Helper Methods

        public Response<List<T>> CustomCollectionResponse<T>(
            HttpStatusCode statusCode,
            bool succeeded,
            List<T> data,
            string message = "",
            object meta = null)
        {
            return new Response<List<T>>()
            {
                Data = data,
                StatusCode = statusCode,
                Succeeded = succeeded,
                Message = message,
                Meta = meta
            };
        }

        #endregion
    }
}
