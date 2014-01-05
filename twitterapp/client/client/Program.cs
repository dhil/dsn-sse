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
            TwitterManager twitterManager = TwitterFactory.Produce.TwitterManager("twitterapp.conf");
            var user = twitterManager.FindTwitterUserByScreenname("dhillerstrom");

            if (user != null) {
                user.printOut();
            } else {
                Console.WriteLine("No user.");
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
