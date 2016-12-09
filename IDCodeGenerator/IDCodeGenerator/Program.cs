using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Common.Exceptions;
using System.Threading.Tasks;

namespace IDCodeGenerator
{
    class Program
    {
        static RegistryManager registryManager;
        //connectionString = "{iothub connection string}";
        static string connectionString = "HostName=TheIntouchablesOOP.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=QTiRXXO6nteHDLVs0Gx1rE+oUbqqOL5fmAoO9ZP3B4w=";

        static void Main(string[] args)
        {
            registryManager = RegistryManager.CreateFromConnectionString(connectionString);
            AddDeviceAsync().Wait();
            Console.ReadLine();
        }

        private static async Task AddDeviceAsync()
        {
            string deviceId = "TheIntouchables";
            Device device;
            try
            {
                device = await registryManager.AddDeviceAsync(new Device(deviceId));
            }
            catch (DeviceAlreadyExistsException)
            {
                device = await registryManager.GetDeviceAsync(deviceId);
            }
            Console.WriteLine("Generated device key: {0}", device.Authentication.SymmetricKey.PrimaryKey);
        }
    }
}
