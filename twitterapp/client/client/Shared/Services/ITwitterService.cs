using System;

using Shared.Models;

namespace Shared.Services {
    public interface ITwitterService {
        ITwitterUser FindUserByScreenName(string screenName);
        ITwitterUser FindUserById(string userId);
    }
}

