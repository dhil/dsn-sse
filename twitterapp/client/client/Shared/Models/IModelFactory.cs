using System;

namespace Shared.Models {
    public interface IModelFactory {
        ITwitterUser TwitterUser(string id, string name, string screenName, string location, string description);
        ITwitterUser TwitterUser();
        ITweet Tweet(string id, ITwitterUser author, string text, DateTime createdAt);
    }
}

