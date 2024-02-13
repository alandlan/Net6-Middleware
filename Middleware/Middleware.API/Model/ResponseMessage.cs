using System.ComponentModel;

namespace Middleware.API.Model
{
    public enum ResponseMessage
    {
        [Description("An error occurred.")]
        GenericError,
        [Description("The request was successful.")]
        Success,
        [Description("The request was not successful.")]
        Failure
    }
}
