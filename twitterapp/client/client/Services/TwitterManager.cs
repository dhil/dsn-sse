using System;
using System.Collections.Generic;
using System.Linq;
using LinqToTwitter;

using Shared.Models;
using Shared.Services;

namespace Services {
    public class TwitterManager : ITwitterService {
        #region Instance variables
        private IModelFactory Produce = ModelFactory.Produce;
        private Dictionary<string, object> IdentityMap = null;
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
            IdentityMap = new Dictionary<string, object>();
        }
        #endregion

        #region Methods
        public virtual ITwitterUser FindUserByScreenName(string screenName) {
            if (screenName == null)
                throw new ArgumentNullException("screenname", "Cannot lookup 'null' name.");
            // Try cache first!
            object cached_tu;
            if (TryLookup(screenName, out cached_tu))
                return (ITwitterUser)cached_tu;

            // Else fetch from Twitter
            ITwitterUser tu = null;
            try {
                User user = (from usr in Context.User
                             where usr.Type == UserType.Show && usr.ScreenName == screenName
                             select usr).FirstOrDefault();
                // Populate TwitterUser model
                tu = Produce.TwitterUser(user.Identifier.UserID, user.Name, user.ScreenName, user.Location, user.Description);
                // Store in cache
                Store(screenName.ToLower(), tu);
                Store(tu.Id, tu);
            } catch (TwitterQueryException e) {
                throw new Exception("Fatal error: Invalid twitter query.", e);
            } catch (Exception e) {
                throw new Exception("Unhandled exception occurred.", e);
            }
            return tu;
        }

        public virtual ITwitterUser FindUserById(string userId) {
            if (userId == null)
                throw new ArgumentNullException("userId", "Cannot lookup 'null' id.");

            // Try cache first!
            object cached_tu;
            if (TryLookup(userId, out cached_tu))
                return (ITwitterUser)cached_tu;

            // Else fetch from Twitter
            ITwitterUser tu = null;
            try {
                User user = (from usr in Context.User
                             where usr.Type == UserType.Show && usr.UserID == userId
                             select usr).FirstOrDefault();
                // Populate TwitterUser model
                tu = Produce.TwitterUser(user.Identifier.UserID, user.Name, user.Identifier.ScreenName, user.Location, user.Description);
                // Store in cache
                Store(userId, tu);
                Store(tu.ScreenName.ToLower(), tu);
            } catch (TwitterQueryException e) {
                throw new Exception("Fatal error: Invalid twitter query.", e);
            } catch (Exception e) {
                throw new Exception("Unhandled exception occurred.", e);
            }
            return tu;
        }
        #endregion

        #region Cache / Identity map lookup
        protected virtual bool TryLookup(string key, out object obj) {
            return IdentityMap.TryGetValue(key, out obj);
        }

        protected virtual void Store(string key, object obj) {
            IdentityMap[key] = obj;
        }
        #endregion

        #region Legacy
        public static RESTClient.TwittUser GetTwittUser(string screenName) {
            return null;
        }
        #endregion
    }
}

