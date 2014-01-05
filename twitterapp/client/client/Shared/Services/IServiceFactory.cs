using System;

namespace Shared.Services {
    public interface IServiceFactory {
        ITwitterService TwitterManager(string credentials_config);
        object RestManager();
    }
}

