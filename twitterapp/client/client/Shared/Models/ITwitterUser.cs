using System;

namespace Shared.Models {
    public interface ITwitterUser : IXmlSerializable {
        string Id { get; set; }
        string Name { get; set; }
        string ScreenName { get; set; }
        string Location { get; set; }
        string Description { get; set; }
    }
}

