using System;
using System.Linq;
using LinqToTwitter;

namespace RESTClient {
    public class TwittManager {
        #region Instance variables

        #endregion

        #region Properties
        protected virtual TwitterContext Context { get; set; }
        #endregion

        #region Constructors
        public TwittManager(ITwitterAuthorizer auth) {
            try {
                Context = new TwitterContext(auth);
            } catch (Exception e) {
                throw new Exception("Could not instantiate a valid Twitter context from given auth.", e);
            }
        }
        #endregion

        #region Methods
        public virtual TwittUser FindTwitterUserByScreenname(string screenname) {
            if (screenname == null)
                throw new ArgumentNullException("screenname", "Cannot lookup 'null' name.");

            TwittUser tu = null;
            try {
                User user = (from tweet in Context.User
                             where tweet.Type == UserType.Show && tweet.ScreenName == screenname
                             select tweet).FirstOrDefault();
                // Populate TwittUser model
                tu = new TwittUser(user.UserID, user.Name, user.ScreenName, user.Location, user.Description);
            } catch (TwitterQueryException e) {
                throw new Exception("Fatal error: Invalid twitter query.", e);
            } catch (Exception e) {
                throw new Exception("Unhandled exception occurred.", e);
            }
            return tu;
        }
        #endregion

        #region Legacy
        public static TwittUser GetTwittUser(string screenName) {
            return null;
        }
        #endregion
    }
}

