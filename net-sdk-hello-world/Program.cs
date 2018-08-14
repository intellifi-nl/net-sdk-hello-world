using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**
 * Import the Intellifi SpotLib;
 * See: https://intellifi-nl.github.io/net-sdk-api-reference/api/SpotLib.html
 */
using SpotLib;

namespace net_sdk_hello_world {
  class Program {
    private static SpotLib.API api;

    static void Main(string[] args) {
      Console.WriteLine("Enter the IP Address or Hostname of the Smartspot and press enter...");
      String ipAddress = Console.ReadLine();
      Console.WriteLine("Trying to connect to Smartspot {0}", ipAddress);

      // Initialize API with IP Address and port
      api = new SpotLib.API(ipAddress, 23);

      // Initialize the API
      api.Init();

      // Subscribe to the events
      api.Spot += spotEventHandler;
      api.Presence += presenceEventHandler;

      // Make sure that the application does not exit
      Console.Read();
    }

    private static void spotEventHandler(object Smartspot, EventArgs e) {
      SpotEvent spotEvent = (SpotEvent)e; // Cast EventArgs to SpotEvent
      if (spotEvent.IsConnected) {
        Console.WriteLine("Smartspot {0} is connected", spotEvent.SpotId);
      } else {
        Console.WriteLine("Smartspot {0} is disconnected", spotEvent.SpotId);
      }
    }

    private static void presenceEventHandler(object Smartspot, EventArgs e) {
      PresenceEvent presenceEvent = (PresenceEvent)e; // Cast EventArgs to PresenceEvent
      Console.WriteLine("Presence type: {0,5}, Presence proximity: {1, 10}, Item code: {2}",
        presenceEvent.Type,
        presenceEvent.Presence.Proximity,
        presenceEvent.Presence.Item.Code);
    }
  }
}
