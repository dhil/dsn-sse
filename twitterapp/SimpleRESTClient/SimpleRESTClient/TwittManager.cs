using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using LinqToTwitter;

namespace RESTClient {
    public static class TwittManager {
        #region global data
        private static string config = "twitterapp.conf";
        private static TwitterContext Context;
        #endregion

        #region Static constructor
        static TwittManager() {
            // Get consumer key & secret
            string key, secret;
            try {
                using (var fs = new FileStream(config, FileMode.Open, FileAccess.Read, FileShare.Read)) {
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
                Context = new TwitterContext(auth);
            } catch (Exception e) {
                throw new Exception("Authorization exception: " + e.Message, e);
            }
        }
        #endregion

        /*Retrieve a twitt user from a given screen name, see https://dev.twitter.com/docs/api/1/get/users/show*/
        public static TwittUser GetTwittUser(string screenName) {
            TwittUser tu = null;
            try {
                tu = (from usr in Context.User
                      where usr.Type == UserType.Show && usr.ScreenName == screenName
                      let u = new TwittUser(usr.Identifier.UserID, usr.Name, usr.ScreenName, usr.Location, usr.Description)
                      select u).FirstOrDefault();
            } catch (TwitterQueryException e) {
                throw new Exception("Invalid Twitter query", e);
            }
            return tu;
        }

        public static List<TwittStatus> GetLastNStatus(string screenName, int n) {
            List<TwittStatus> statuses = new List<TwittStatus>();
            try {
                var tu = GetTwittUser(screenName);
                statuses =(from status in Context.Status
                           where status.Type == StatusType.User 
                           && status.ScreenName == screenName 
                           && status.Count == n
                           let s = new TwittStatus(status.StatusID, tu.id, status.Text, status.CreatedAt)
                           select s).ToList();
            } catch (TwitterQueryException e) {
                throw new Exception("Invalid Twitter query", e);
            }
            return statuses;
        }
    }
}