using System;

using Models;

namespace Shared.Models {
    public class ModelFactory : IModelFactory {
        #region Static variables
        private static ModelFactory _instance = null;
        #endregion

        #region Constructor
        internal ModelFactory() {
        }
        #endregion

        #region Instance getter
        public static ModelFactory Produce {
            get {
                if (_instance == null)
                    _instance = new ModelFactory();
                return _instance;
            }
        }
        #endregion

        #region Factory methods
        public ITwitterUser TwitterUser(string id, string name, string screenName, string location, string description) {
            TwitterUser tu = new TwitterUser {
                Id = id, Name = name, ScreenName = screenName, 
                Location = location, Description =  description
            };
            return tu;
        }

        public ITwitterUser TwitterUser() {
            return TwitterUser(null, null, null, null, null);
        }
        #endregion

    }
}

