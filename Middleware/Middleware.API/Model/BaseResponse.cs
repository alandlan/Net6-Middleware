namespace Middleware.API.Model
{
    public class BaseResponse
    {
        private ResponseMessage _responseMessage;
        public bool Success { get; set; }
        public ResponseDetails? ResponseDetails { get; set; }

        public BaseResponse AsError(ResponseMessage? message = null, params string[] errors)
        {
            errors ??= Array.Empty<string>();
            Success = false;
            _responseMessage = message ?? ResponseMessage.GenericError;
            ResponseDetails = new ResponseDetails
            {
                Message = _responseMessage.ToString(),
                Errors = errors
            };
            return this;
        }

        public ResponseMessage GetMessage()
        {
            return _responseMessage;
        }

        public BaseResponse AsSuccess()
        {
            Success = true;
            _responseMessage = ResponseMessage.Success;
            ResponseDetails = new ResponseDetails
            {
                Message = _responseMessage.ToString(),
                Errors = Array.Empty<string>()
            };
            return this;
        }

        public bool ResponseMessageEquals(ResponseMessage message)
        {
            return _responseMessage == message;
        }
    }
}
