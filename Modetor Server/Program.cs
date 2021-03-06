﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using Modetor.Net.Server;
namespace Modetor_Server
{
    class Program
    {
        static void Main(string[] args)
        {
            
            if(args != null && args.Length != 0)
            {
                Server server = Server.GetServer();

                SetupServerEvent(server);

                foreach (string command in args)
                {
                    if(command.Equals("help"))
                    {
                        Blue("All available commands { \n");
                        Green("\thelp  -  "); Console.WriteLine(" prints all commands");
                        Green("\trun   -  "); Console.WriteLine(" used to start the server. e.g: run=127.0.0.1:80");
                        Green("\tlsips -  "); Console.WriteLine(" list available ips");
                        Blue("}\n");
                    }
                    else if(command.Equals("lsips"))
                        PrintAvailableIPs();
                    else if (command.StartsWith("run="))
                    {
                        Console.WriteLine("Yes");
                        if (command.Length == 4 || !command.Contains(':'))
                            Red("Syntax Error. Run command must be like run=127.0.0.1:80");
                        else
                        {
                            string[] address = command.Split('=')[1].Trim().Split(':');
                            int port = 80;
                            if (!int.TryParse(address[1], out port))
                            {
                                port = 80;
                                Red("[Command.Run] : Port value must be a valid integer. fallback port(80) will be used");

                            }
                            server.SetAddress(address[0], port);
                            server.Start();
                            System.Threading.Thread.CurrentThread.Join();
                        }
                    }
                }
            }
            else
                Welcome();


        }

        private static void SetupServerEvent(Server server)
        {
            server.OnStart = (ip, port) =>
            {
                Green($"Server started {ip}:{port}\n");
            };
        }

        static string[] GetNeworkIPs()
        {
            List<string> l = new List<string>();
            IPAddress[] addr = Dns.GetHostEntry(Dns.GetHostName()).AddressList;
            int i = 0;
            for (; i < addr.Length; i++)
            {
                if (addr[i].AddressFamily == AddressFamily.InterNetwork) { l.Add(addr[i].ToString()); }
            }

            if (!l.Contains("127.0.0.1"))
                l.Add("127.0.0.1");
            return l.ToArray();
        }

        static void Welcome()
        {
            Yellow("// ");Blue("Modetor Server\n"); 
            Yellow("// "); Blue("Date : 7-9-2020\n");
            Yellow("// "); Blue("Author : Mohammad S. Albay\n");
            for(int i = 0; ++i < 50;)
                Yellow("-");

            PrintAvailableIPs();

            Yellow("\n\nNote: help to show available commands\n");
        }

        private static void PrintAvailableIPs()
        {
            Green("\nAvailable IPs : \n");

            foreach (string item in GetNeworkIPs())
                Console.WriteLine(" - " + item);
        }

        static void Blue(string text)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write(text);
            Console.ResetColor();
        }
        static void Yellow(string text)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write(text);
            Console.ResetColor();
        }
        static void Red(string text)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write(text);
            Console.ResetColor();
        }
        static void Green(string text)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write(text);
            Console.ResetColor();
        }

    }
}
