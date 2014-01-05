using System;
using System.Collections.Generic;
using System.IO;
using LinqToTwitter;

using Services;

namespace Shared.Services {
    public class ServiceFactory : IServiceFactory {
        #region Instance variables
        private static ServiceFactory _instance = null;
        private Dictionary<string, object> IdentityMap = null;
        #endregion

        #region Constructors
        internal ServiceFactory() {
            IdentityMap = new Dictionary<string, object>();
        }
        #endregion

        #region Properties -- Instance getter
        public static ServiceFactory Produce {
            get { 
                if (_instance == null)
                    _instance = new ServiceFactory();
                return _instance;
            }
        }
        #endregion

        #region Factory methods
        public ITwitterService TwitterManager(string credentials_config) {
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
                throw new AuthenticationException("Authorization exception: " + e.Message, e);
            }

            // Instantiate Twitter manager object & store it in cache
            TwitterManager twitterManager = new TwitterManager(auth);
            IdentityMap.Add(credentials_config, twitterManager);
            return twitterManager;
        }

        public object RestManager() {
            return null;
        }
        #endregion
    }
}

