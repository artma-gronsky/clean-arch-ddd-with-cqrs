using ErrorOr;

namespace BuperDinner.Domain.Common.Errors;

public static partial class Errors{
        public static class User{
        public static Error DuplicateEmailError = Error.Conflict(
            code: "User.DuplicateEmail", 
            description: "Email already in use");
    }
}