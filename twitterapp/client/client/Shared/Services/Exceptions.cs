using System;

namespace Shared.Services {
    public class UserAlreadyExistsException : Exception {
        public UserAlreadyExistsException(string message, Exception innerException) : base(message, innerException) {}
        public UserAlreadyExistsException() : this(null, null) {}
    }

    public class ConnectionFailedException : Exception {
        public ConnectionFailedException(string message, Exception innerException) : base(message, innerException) {}
        public ConnectionFailedException() : this(null, null) {}
    }

    public class AuthenticationException : Exception {
        public AuthenticationException(string message, Exception innerException) : base(message, innerException) {}
        public AuthenticationException() : this("Could not authenticate.", null) {}
    }
}

