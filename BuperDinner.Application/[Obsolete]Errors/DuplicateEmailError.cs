//namespace BuperDinner.Application.Errors;


// Record Struct (record struct):
// Introduced in C# 10, record structs combine the features of records and structs.
// Record structs are value types, meaning they are allocated on the stack and provide better performance in certain scenarios compared to reference types.
// They are intended for modeling immutable data structures similar to regular records.
// Record structs provide value-based equality semantics, automatic generation of Equals() and GetHashCode() methods, and a ToString() method.
// Record structs cannot be inherited, similar to regular structs.
// public record struct DuplicateEmailError : IError
// {
//     public HttpStatusCode StausCode => HttpStatusCode.Conflict;

//     public string Message => "Email already exists.";
// }


// public class DuplicateEmailError : FluentResults.IError
// {
//     public string Message => "Email already exists.";

//     public List<FluentResults.IError> Reasons => throw new NotImplementedException();

//     public Dictionary<string, object> Metadata => throw new NotImplementedException();
// }