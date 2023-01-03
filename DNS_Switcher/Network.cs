using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace DNS_Switcher
{
    class Network
    {
        //Get the Network Interface
        public static NetworkInterface GetActiveEthernetOrWifiNetworkInterface()
        {
            var Nic = NetworkInterface.GetAllNetworkInterfaces().FirstOrDefault(
                a => a.OperationalStatus == OperationalStatus.Up &&
                (a.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 || a.NetworkInterfaceType == NetworkInterfaceType.Ethernet) &&
                a.GetIPProperties().GatewayAddresses.Any(g => g.Address.AddressFamily.ToString() == "InterNetwork"));
            return Nic;
        }

        //Get the current DNS of the active network adapter
        public List<string> DisplayDnsAddresses()
        {
            List<string> result = new List<string>();
            var CurrentInterface = GetActiveEthernetOrWifiNetworkInterface();
            NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface adapter in adapters)
            {
                if (CurrentInterface != null)
                {
                    if (adapter.Description == CurrentInterface.Description)
                    {
                        IPInterfaceProperties adapterProperties = adapter.GetIPProperties();
                        IPAddressCollection dnsServers = adapterProperties.DnsAddresses;
                        if (dnsServers.Count > 0)
                        {

                            foreach (IPAddress dns in dnsServers)
                            {
                                result.Add(dns.ToString());
                            }
                        }
                    }
                }
            }
            return result;
        }

        //Set the system DNS to the user selected DNS server
        public void SetDNS(string dns)
        {
            string[] dnslist = dns.Split(',');
            var CurrentInterface = GetActiveEthernetOrWifiNetworkInterface();
            if (CurrentInterface == null) return;

            ManagementClass objMC = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection objMOC = objMC.GetInstances();
            foreach (ManagementObject objMO in objMOC)
            {
                if ((bool)objMO["IPEnabled"])
                {
                    if (objMO["Description"].ToString().Equals(CurrentInterface.Description))
                    {
                        ManagementBaseObject objdns = objMO.GetMethodParameters("SetDNSServerSearchOrder");
                        if (objdns != null)
                        {
                            objdns["DNSServerSearchOrder"] = dnslist;
                            objMO.InvokeMethod("SetDNSServerSearchOrder", objdns, null);
                        }
                    }
                }
            }

        }

        //remove every dns and set it to auto ,a new dns will be asign by the router 
        public void RemoveDNS()
        {
            var CurrentInterface = GetActiveEthernetOrWifiNetworkInterface();
            if (CurrentInterface == null) return;

            ManagementClass objMC = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection objMOC = objMC.GetInstances();
            foreach (ManagementObject objMO in objMOC)
            {
                if ((bool)objMO["IPEnabled"])
                {
                    if (objMO["Description"].ToString().Equals(CurrentInterface.Description))
                    {
                        ManagementBaseObject objdns = objMO.GetMethodParameters("SetDNSServerSearchOrder");
                        if (objdns != null)
                        {
                            objdns["DNSServerSearchOrder"] = null;
                            objMO.InvokeMethod("SetDNSServerSearchOrder", objdns, null);
                        }
                    }
                }
            }
        }




        //check the ping to given ip address
        public double GetPing(string IP)
        {
            long totalTime = 0;
            Ping pingSender = new Ping();

            for (int i = 0; i < 16; i++)
            {
                PingReply reply = pingSender.Send(IP, 120);
                if (reply.Status == IPStatus.Success)
                {
                    totalTime += reply.RoundtripTime;
                }
            }
            return totalTime / 16;
        }





        //flush the DNS Cache
        public bool FlushDNS()
        {
            Process proc = new Process();
            proc.StartInfo.FileName = "CMD.exe";
            proc.StartInfo.Arguments = "/c ipconfig /flushdns";
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.CreateNoWindow = true;
            proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.Start();
            string output = proc.StandardOutput.ReadToEnd();
            proc.WaitForExit();
            if (output == "\r\nWindows IP Configuration\r\n\r\nSuccessfully flushed the DNS Resolver Cache.\r\n")//check if the cmd outpot is success message
            {
                return true;
            }
            else
                return false;

        }



        //testing download and upload speed function 
        public SpeedCheck.Root GetInternetSpeed(string server = "Auto")
        {

            SpeedCheck.Root result =null;
            Process? proc = null;
            try
            {
                proc= new Process();
                proc.StartInfo.UseShellExecute=false;
                proc.StartInfo.RedirectStandardOutput= true;
                proc.StartInfo.CreateNoWindow= true;
                proc.StartInfo.WindowStyle= ProcessWindowStyle.Hidden;
                proc.StartInfo.FileName= "speedtest.exe";
                proc.StartInfo.Arguments = "-P 0 -f json";
                proc.Start();
                string output = proc.StandardOutput.ReadToEnd();
                proc.WaitForExit();
                var code = proc.ExitCode;
                result = JsonConvert.DeserializeObject<SpeedCheck.Root>(output);              
            }
            catch(Exception)
            {
                throw;

            }
            finally
            {
                if(proc != null ) proc.Dispose();

            }
            return result;


        }

    }
}
