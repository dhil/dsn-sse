using System;

using Models;

namespace Shared.Services {
    public interface TwitterService {
        TwitterUser FindUserByScreenname(string screenname);
    }
}

