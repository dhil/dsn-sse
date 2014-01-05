using System;

namespace Shared.Models {
    public interface ITweet {
        ITwitterUser Author { get; }
        DateTime Date { get; set; }
        string Text { get; set; }
        string Id { get; set; }
    }
}

