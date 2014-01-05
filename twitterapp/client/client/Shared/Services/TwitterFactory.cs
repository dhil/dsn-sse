using System;
using System.Collections.Generic;
using System.IO;
using LinqToTwitter;

using Services;

namespace Shared.Services {
    public class TwitterFactory {
        #region Instance variables
        private static TwitterFactory _instance = null;
        private Dictionary<string, object> IdentityMap = null;
        #endregion

        #region Constructors
        internal TwitterFactory() {
            IdentityMap = new Dictionary<string, object>();
        }
        #endregion

        #region Properties -- Instance getter
        public static TwitterFactory Produce {
            get { 
                if (_instance == null)
                    _instance = new TwitterFactory();
                return _instance;
            }
        }
        #endregion

        #region Factory methods
        public TwitterManager TwitterManager(string credentials_config) {
            // Look-up the object in cache
            if (IdentityMap.ContainsKey(credentials_config))
                return (TwitterManager)IdentityMap[credentials_config];

            // Get consumer key & secret
            string key, secret;
            try {
                using (var fs = new FileStream(credentials_config, FileMode.Open, FileAccess.Read, FileShare.Read)) {
                    using (var sr = new StreamReader(fs, System.Text.Encoding.UTF8)) {
                        key = sr.ReadLine();
                        secret = sr.ReadLine();
                    }
                }
            } catch (IOException e) {
                throw new IOException("IOException occurred: " + e.Message, e);
            }

            // Create auth & authorize
            ITwitterAuthorizer auth;
            try {
                auth = new ApplicationOnlyAuthorizer {
                    Credentials = new InMemoryCredentials {
                        ConsumerKey = key,
                        ConsumerSecret = secret
                    }
                };
                auth.Authorize();
            } catch (Exception e) {
                throw new Exception("Authorization exception: " + e.Message, e);
            }

            // Instantiate Twitter manager object & store it in cache
            TwitterManager twitterManager = new TwitterManager(auth);
            IdentityMap.Add(credentials_config, twitterManager);
            return twitterManager;
        }
        #endregion
    }
}

