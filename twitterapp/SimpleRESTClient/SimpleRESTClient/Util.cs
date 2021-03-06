﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.Net;

namespace RESTClient
{
    /*Static class holding utility functions*/
    public static class Util
    {
        /*The base uri of the board*/
        public static string boardUri = "http://localhost:8080/BasicServer/rest";

        /*Display the menu and return the choice of the user*/
        public static int menu()
        {
            Console.WriteLine("Menu:");
            Console.WriteLine("0: Exit.");
            Console.WriteLine("1: Print out a twitt user's information from specified screen name.");
            Console.WriteLine("2: Print all Twitt Users from the board.");
            Console.WriteLine("3: Print specified Twitt User from the board.");
            Console.WriteLine("4: Print N last statuses from specificed screen name.");
            Console.WriteLine("5: Print all statuses from specified user id from the board.");
            Console.WriteLine("6: Print a specific Twitt status from the board.");
            Console.WriteLine("7: Forward one Twitt User from specified screen name to the board.");
            Console.WriteLine("8: Forward N last statuses from specified screen name to the board.");
            Console.WriteLine("9: Modify a Twitt User description from the board.");
            Console.WriteLine("10: Modify a Twitt status text from the board.");
            Console.WriteLine("11: Delete Twitt user.");
            Console.WriteLine("12: Delete a Twitt status.");
            bool p = false;
            int choice;
            do {
                Console.WriteLine("Enter your choice:");
                p = int.TryParse(Console.ReadLine(), out choice);
            } while (choice < 0 || choice > 12 || !p);

            return choice;
        }

        /*Forward a twitt user to the board*/
        public static void printTwittUser() {
            Console.WriteLine("Enter the screen name of the twitt user you want to print out:");
            string screenName = Console.ReadLine();

            TwittUser tu = TwittManager.GetTwittUser(screenName);
            if (tu == null) return;
            tu.printOut();
        }

        /*Print all the twitt users from the board*/
        public static void printTwittUsersFromBoard() {
            string uri = Util.boardUri + "/twittusers";

            XmlDocument doc;
            XmlNodeList xnlist;
            List<TwittUser> tsList = new List<TwittUser>();
            doc = RestManager.GetXmlResponse(uri);
            if (doc == null)
                return;

            xnlist = doc.SelectNodes("twittUsers/twittUser");

            foreach (XmlNode xn in xnlist) {
                XmlSerializer xs = new XmlSerializer(typeof(TwittUser), new XmlRootAttribute() { ElementName = "twittUser" });
                TwittUser tu = (TwittUser)xs.Deserialize(new XmlNodeReader(xn));
                tu.printOut();
            }
        }

        /*Print a specified twitt user from the board*/
        public static void printTwittUserFromBoard() {
            TwittUser tu;

            Console.WriteLine("Enter the id of the twitt user you want to retrieve:");
            string id = Console.ReadLine();

            string uri = Util.boardUri + "/twittusers/" + id;

            XmlReader xr;

            try {
                xr = XmlReader.Create(uri);
            } catch (WebException we) {
                HttpWebResponse res = (HttpWebResponse)we.Response;
                Console.WriteLine(res.StatusCode.ToString());
                return;
            }

            XmlSerializer xs = new XmlSerializer(typeof(TwittUser), new XmlRootAttribute("twittUser"));
            tu = (TwittUser)xs.Deserialize(xr);

            tu.printOut();
        }

        /*Forward a twitt user to the board*/
        public static void forwardTwittUser() {
            Console.WriteLine("Enter the screen name of the twitt user you want to forward:");
            string screenName = Console.ReadLine();

            TwittUser tu = TwittManager.GetTwittUser(screenName);
            if (tu == null) return;
            RestManager.PostXmlRequest(Util.boardUri + "/twittusers", tu.toXmlString());
        }


        /*Modify the description of a specified twitt user*/
        public static void changeTwittUser() {
            int n;
            string description;

            do {
                Console.WriteLine("Enter the id of the twitt user you want to modify:");
                n = int.Parse(Console.ReadLine());
            } while (n < 1);

            do {
                Console.WriteLine("Enter the new description:");
                description = Console.ReadLine();
            } while (description == null);


            String uri = Util.boardUri + "/twittusers/" + n.ToString();
            XmlReader xr;

            try {
                xr = XmlReader.Create(uri);
            } catch (WebException we) {
                HttpWebResponse res = (HttpWebResponse)we.Response;
                Console.WriteLine(res.StatusCode.ToString());
                return;
            }

            XmlSerializer xs = new XmlSerializer(typeof(TwittUser), new XmlRootAttribute() { ElementName = "twittUser" });
            TwittUser tu = (TwittUser)xs.Deserialize(xr);
            tu.description = description;

            RestManager.PutXmlRequest(uri, tu.toXmlString());
        }

        /*Delete a twitt status*/
        public static void deleteTwittUser() {
            Console.WriteLine("Enter the id of the user to delete:");
            string id = Console.ReadLine();
            string uri = Util.boardUri + "/twittusers/" + id;

            RestManager.DeleteRequest(uri);

        }

        public static void printNLastStatuses() {
            Console.WriteLine("Enter the screen name of the user:");
            string name = Console.ReadLine();
            Console.Write("Enter N: ");
            int n = int.Parse(Console.ReadLine());

            List<TwittStatus> statuses = TwittManager.GetLastNStatus(name, n);
            statuses.ForEach(t => t.printOut());
        }

        public static void forwardNStatuses() {
            Console.WriteLine("Enter the screen name of the user:");
            string name = Console.ReadLine();
            Console.Write("Enter N: ");
            int n = int.Parse(Console.ReadLine());

            TwittUser tu = TwittManager.GetTwittUser(name);

            if (tu != null) {
                RestManager.PostXmlRequest(Util.boardUri + "/twittusers", tu.toXmlString());
                List<TwittStatus> statuses = TwittManager.GetLastNStatus(name, n);
                statuses.ForEach(st => RestManager.PostXmlRequest(Util.boardUri + "/twittstatuses", st.toXmlString()));
            }
        }

        public static void printAllStatusesFromBoardByUserId() {
            Console.Write("Enter user id: ");
            string id = Console.ReadLine();

            string uri = Util.boardUri + "/twittusers/" + id + "/statuses";

            XmlDocument doc;
            XmlNodeList xnlist;
            doc = RestManager.GetXmlResponse(uri);
            if (doc == null)
                return;

            xnlist = doc.SelectNodes("twittStatuses/twittStatus");

            foreach (XmlNode xn in xnlist) {
                XmlSerializer xs = new XmlSerializer(typeof(TwittStatus), new XmlRootAttribute() { ElementName = "twittStatus" });
                TwittStatus st = (TwittStatus)xs.Deserialize(new XmlNodeReader(xn));
                st.printOut();
            }
        }

        public static void printSpecificTwittStatusFromBoard() {
            TwittStatus st;

            Console.WriteLine("Enter the id of the twitt status you want to retrieve:");
            string id = Console.ReadLine();

            string uri = Util.boardUri + "/twittstatuses/" + id;

            XmlReader xr;

            try {
                xr = XmlReader.Create(uri);
            } catch (WebException we) {
                HttpWebResponse res = (HttpWebResponse)we.Response;
                Console.WriteLine(res.StatusCode.ToString());
                return;
            }

            XmlSerializer xs = new XmlSerializer(typeof(TwittStatus), new XmlRootAttribute("twittStatus"));
            st = (TwittStatus)xs.Deserialize(xr);

            st.printOut();
        }

        public static void changeTwittStatus() {
            string id;
            string text;
            int n;

            do {
                Console.WriteLine("Enter the id of the twitt status you want to modify:");
                n = int.Parse(Console.ReadLine());
            } while (n < 1);
            id = n.ToString();

            do {
                Console.WriteLine("Enter the new text:");
                text = Console.ReadLine();
            } while (text == null);


            String uri = Util.boardUri + "/twittstatuses/" + id;
            XmlReader xr;

            try {
                xr = XmlReader.Create(uri);
            } catch (WebException we) {
                HttpWebResponse res = (HttpWebResponse)we.Response;
                Console.WriteLine(res.StatusCode.ToString());
                return;
            }

            XmlSerializer xs = new XmlSerializer(typeof(TwittStatus), new XmlRootAttribute() { ElementName = "twittStatus" });
            TwittStatus st = (TwittStatus)xs.Deserialize(xr);
            st.text = text;

            RestManager.PutXmlRequest(boardUri + "/twittstatuses/" + id, st.toXmlString());
        }

        public static void deleteTwittStatus() {
            Console.Write("Enter id of Twitt Status: ");
            string id = Console.ReadLine();

            string uri = Util.boardUri + "/twittstatuses/" + id;

            try {
                RestManager.DeleteRequest(uri);
            } catch (WebException we) {
                HttpWebResponse res = (HttpWebResponse)we.Response;
                Console.WriteLine(res.StatusCode.ToString());
                return;
            }
        }
    }
}
