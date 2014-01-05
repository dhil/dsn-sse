using System;
using System.Linq;
using LinqToTwitter;

using Models;

namespace Services {
    public class TwitterManager {
        #region Instance variables

        #endregion

        #region Properties
        protected virtual TwitterContext Context { get; set; }
        #endregion

        #region Constructors
        public TwitterManager(ITwitterAuthorizer auth) {
            try {
                Context = new TwitterContext(auth);
            } catch (Exception e) {
                throw new Exception("Could not instantiate a valid Twitter context from given auth.", e);
            }
        }
        #endregion

        #region Methods
        public virtual TwitterUser FindTwitterUserByScreenname(string screenname) {
            if (screenname == null)
                throw new ArgumentNullException("screenname", "Cannot lookup 'null' name.");

            TwitterUser tu = null;
            try {
                User user = (from tweet in Context.User
                             where tweet.Type == UserType.Show && tweet.ScreenName == screenname
                             select tweet).FirstOrDefault();
                // Populate TwitterUser model
                tu = new TwitterUser(user.UserID, user.Name, user.ScreenName, user.Location, user.Description);
            } catch (TwitterQueryException e) {
                throw new Exception("Fatal error: Invalid twitter query.", e);
            } catch (Exception e) {
                throw new Exception("Unhandled exception occurred.", e);
            }
            return tu;
        }
        #endregion

        #region Legacy
        public static RESTClient.TwittUser GetTwittUser(string screenName) {
            return null;
        }
        #endregion
    }
}

