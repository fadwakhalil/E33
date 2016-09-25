using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SecureCommunicationComponent
{
    public class Communicator
    {
        public string SendAlert(string Addressee, string Message)
        {
            // The AssemblyName type can be used to parse the full name.
            string version = typeof(Communicator).Assembly.GetName().Version.ToString();

            return Message + " Received: " + version;
        }
    }
}
