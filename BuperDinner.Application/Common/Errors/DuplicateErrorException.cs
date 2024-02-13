using System.Net;

namespace BuperDinner.Application.Common.Errors;

public class DuplicateErrorException : Exception, IServiseException{
    public DuplicateErrorException(){}

    public HttpStatusCode StausCode { get => HttpStatusCode.Conflict; }
    string IServiseException.Message { get => "Email already exist.";}
}