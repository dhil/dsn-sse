using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace RESTClient
{
    class Program
    {
        /*Entry point of the program*/
        static void Main(string[] args)
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
                        Util.printNLastStatuses();
                        break;
                    case 5:
                        Util.printAllStatusesFromBoardByUserId();
                        break;
                        case 6:
                        Util.forwardTwittUser();
                        break;
                    case 7:
                        Util.forwardNStatuses();
                        break;
                        case 8:
                        Util.changeTwittUser();
                        break;
                        case 9:
                        Util.deleteTwittUser();
                        break;
                }

            }
        }
    }
}
