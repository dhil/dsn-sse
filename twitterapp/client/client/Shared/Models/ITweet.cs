using System;

namespace Shared.Models {
    public interface ITweet {
        ITwitterUser Author { get; }
        DateTime CreatedAt { get; set; }
        string Text { get; set; }
        string Id { get; set; }
        string UserId { get; set; }
    }
}

