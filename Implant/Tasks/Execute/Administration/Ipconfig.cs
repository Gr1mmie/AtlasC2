using System.Text;
using System.Net.NetworkInformation;

using Implant.Models;

namespace Implant.Tasks.Execute
{
    class Ipconfig : ImplantCommands
    {
        public override string Name => "ipconfig";

        public override string Execute(ImplantTask task)
        {
            StringBuilder _out = new StringBuilder();
            NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();

            if (interfaces == null) { return "[-] No network interfaces detected"; }

            foreach (NetworkInterface iface in interfaces)
            {
                IPInterfaceProperties properties = iface.GetIPProperties();

                _out.AppendLine($"{iface.Name}");
                _out.AppendLine($" {nameof(iface.NetworkInterfaceType)} : {iface.NetworkInterfaceType}");
                _out.AppendLine($" {nameof(iface.OperationalStatus)} : {iface.OperationalStatus}");
                _out.AppendLine($" {"IP Address"} : {properties.DnsAddresses[0]}");
                if (properties.GatewayAddresses.Count != 0) { _out.AppendLine($" {"Gateway Address"} : {properties.GatewayAddresses[0].Address}"); }
                if (!(iface.GetPhysicalAddress().GetAddressBytes().Length is 0)) { _out.AppendLine($" {"MAC Address"} : {iface.GetPhysicalAddress()}"); }
                _out.AppendLine();
            }

            return _out.ToString();
        }
    }
}
