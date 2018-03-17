using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetFwTypeLib;
using System.Net;

namespace Bloqueador
{
    class Program
    {
        static void Main(string[] args)
        {

            string howtogeek = "pt.org.br";
            IPAddress[] addresslist = Dns.GetHostAddresses(howtogeek);

            foreach (IPAddress ip in addresslist)
            {
                INetFwRule2 firewallRule = (INetFwRule2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FWRule"));

                firewallRule.Name = "Block IP Addresses";
                firewallRule.Description = "IP Blocked by Blocker!";
                firewallRule.Action = NET_FW_ACTION_.NET_FW_ACTION_BLOCK;
                firewallRule.Direction = NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_OUT; // dentro para fora.
                firewallRule.Enabled = true;
                firewallRule.InterfaceTypes = "All";
                firewallRule.RemoteAddresses = ip.ToString();

                INetFwPolicy2 firewallPolicy = (INetFwPolicy2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FwPolicy2"));
                firewallPolicy.Rules.Add(firewallRule);

                
            }

            //INetFwPolicy2 firewallPolicy = (INetFwPolicy2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FwPolicy2"));
            //var rule = firewallPolicy.Rules.Item("54.77.250.144"); // Name of your rule here
            //rule.Name = "Block Block Block"; // Update the rule here. Nothing else needed to persist the changes
        }
    }
}
