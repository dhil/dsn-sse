using System;
using System.Collections.Generic;

using Shared.Models;

namespace Shared.Services {
    public interface ITwitterService {
        ITwitterUser FindUserByScreenName(string screenName);
        ITwitterUser FindUserById(string userId);
        List<ITweet> LastNTweetsFromUser(string screenName, int n);
        List<ITweet> LastNTweetsFromUser(ITwitterUser user, int n);
    }
}

