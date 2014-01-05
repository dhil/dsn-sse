using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using LinqToTwitter;

using Shared.Services;
using Services;

namespace RESTClient
{
    class Program {
        public static void Main(string[] args) {
           ITwitterService twitterManager = ServiceFactory.Produce.TwitterManager("twitterapp.conf");
            var user = twitterManager.FindUserByScreenName("dhillerstrom");

            if (user != null) {
                Console.WriteLine("{0}", user.ToString());
                Console.WriteLine("XML:\n{0}", user.ToXmlString());
            } else {
                Console.WriteLine("No user.");
                return;
            }

            // Now by ID
            var userById = twitterManager.FindUserById(user.Id);
            if (userById != null) {
                Console.WriteLine("{0}", userById.ToString());
                Console.WriteLine("Reference equality? {0}", user == userById);
            } else {
                Console.WriteLine("No user by id.");
            }

            // Get last n tweets by user
            int n = 7, i = 1;
            var tweets = twitterManager.LastNTweetsFromUser(user, n);
            foreach (var tweet in tweets) {
                Console.WriteLine("{0}) {1} @ {2}", i++, tweet.Text, tweet.CreatedAt.ToShortDateString());
            }

            Console.Read();
        }
        /*Entry point of the program*/
  /*      static void Main(string[] args)
        {
            int choice = -1;

            while (choice != 0)
            {
                choice = Util.menu();

                switch (choice)
                {
                    case 1:
                        Util.printTwittUser();
                        break;
                    case 2:
                        Util.printTwittUsersFromBoard();
                        break;
                    case 3:
                        Util.printTwittUserFromBoard();
                        break;
                    case 4:
                        Util.forwardTwittUser();
                        break;
                    case 5:
                        Util.changeTwittUser();
                        break;
                    case 6:
                        Util.deleteTwittUser();
                        break;
                }

            }
        }*/
    }
}
