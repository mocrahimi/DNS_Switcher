using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNS_Switcher
{
    class SpeedCheck
    {
        
        public class Download
        {
            public int bandwidth { get; set; }
            public float bytes { get; set; }
            public int elapsed { get; set; }
            public Latency latency { get; set; }
        }

        public class Interface
        {
            public string internalIp { get; set; }
            public string name { get; set; }
            public string macAddr { get; set; }
            public bool isVpn { get; set; }
            public string externalIp { get; set; }
        }

        public class Latency
        {
            public double iqm { get; set; }
            public double low { get; set; }
            public double high { get; set; }
            public double jitter { get; set; }
        }

        public class Ping
        {
            public double jitter { get; set; }
            public double latency { get; set; }
            public double low { get; set; }
            public double high { get; set; }
        }

        public class Result
        {
            public string id { get; set; }
            public string url { get; set; }
            public bool persisted { get; set; }
        }

        public class Root
        {
            public string type { get; set; }
            public DateTime timestamp { get; set; }
            public Ping ping { get; set; }
            public Download download { get; set; }
            public Upload upload { get; set; }
            public int packetLoss { get; set; }
            public string isp { get; set; }
            public Interface @interface { get; set; }
            public Server server { get; set; }
            public Result result { get; set; }
        }

        public class Server
        {
            public int id { get; set; }
            public string host { get; set; }
            public int port { get; set; }
            public string name { get; set; }
            public string location { get; set; }
            public string country { get; set; }
            public string ip { get; set; }
        }

        public class Upload
        {
            public int bandwidth { get; set; }
            public int bytes { get; set; }
            public int elapsed { get; set; }
            public Latency latency { get; set; }
        }


    }
}
