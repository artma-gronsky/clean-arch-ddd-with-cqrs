using System.Net;

namespace BuperDinner.Application.Common.Errors;

public interface IServiseException{
    public HttpStatusCode StausCode { get; }
    
    public string Message { get; }
}