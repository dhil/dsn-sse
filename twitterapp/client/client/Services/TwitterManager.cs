using System;
using System.Linq;
using LinqToTwitter;

using Shared.Models;
using Shared.Services;

namespace Services {
    public class TwitterManager : ITwitterService {
        #region Instance variables
        private IModelFactory Produce = ModelFactory.Produce;
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
        public virtual ITwitterUser FindUserByScreenName(string screenName) {
            if (screenName == null)
                throw new ArgumentNullException("screenname", "Cannot lookup 'null' name.");

            ITwitterUser tu = null;
            try {
                User user = (from tweet in Context.User
                             where tweet.Type == UserType.Show && tweet.ScreenName == screenName
                             select tweet).FirstOrDefault();
                // Populate TwitterUser model
                tu = Produce.TwitterUser(user.UserID, user.Name, user.ScreenName, user.Location, user.Description);
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

