using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaspDemo.DeviceManager
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "HostName=iothometestdemo.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=HXI8V2AaLAjtMpLage0m161bxUF73svlzWuiuGxJ2DM=";
            string deviceId = "intouch";
            RegistryManager manager = RegistryManager.CreateFromConnectionString(connectionString);
            Device device = new Device(deviceId);
            try
            {
                device = manager.AddDeviceAsync(device).Result;
            }
            catch(DeviceAlreadyExistsException)
            {
                device = manager.GetDeviceAsync(deviceId).Result;
            }
            catch (AggregateException ae)
            {
                if(ae.InnerExceptions.Any(e => e.GetType() == typeof(DeviceAlreadyExistsException)))
                {
                    device = manager.GetDeviceAsync(deviceId).Result;
                }
            }
            Console.WriteLine(device.Authentication.SymmetricKey.PrimaryKey);
            Debug.WriteLine(device.Authentication.SymmetricKey.PrimaryKey);
            Console.ReadLine();
        }
    }
}
