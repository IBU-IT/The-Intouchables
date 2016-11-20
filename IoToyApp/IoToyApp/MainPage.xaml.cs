using Microsoft.Azure.Devices.Client;
using Microsoft.Azure.Devices;
using System;
using System.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Threading.Tasks;

namespace IoToyApp
{
    public sealed partial class MainPage : Page
    {
        static DeviceClient deviceClient;
        static ServiceClient serviceClient;
        static string connectionString = "{iot hub connection string}";
        static string iotHubUri = "{iot hub hostname}";
        static string deviceKey = "{app key}";
        static string receivedData;
        public MainPage()
        {
            InitializeComponent();
            deviceClient = DeviceClient.Create(iotHubUri, new DeviceAuthenticationWithRegistrySymmetricKey("appName", deviceKey));
            serviceClient = ServiceClient.CreateFromConnectionString(connectionString);
            ReceiveCloudToDeviceAsync();
        }

        private async void button_Click(object sender, RoutedEventArgs e)
        {
            await SendCloudToDeviceMessageAsync("1");
        }


        private async static Task SendCloudToDeviceMessageAsync(string commandString)
        {
            var commandMessage = new Microsoft.Azure.Devices.Message(Encoding.ASCII.GetBytes(commandString));
            await serviceClient.SendAsync("device Name", commandMessage);
        }

        private static async void ReceiveCloudToDeviceAsync()
        {
            while (true)
            {
                Microsoft.Azure.Devices.Client.Message receivedMessage = await deviceClient.ReceiveAsync();
                receivedData = Encoding.ASCII.GetString(receivedMessage.GetBytes());
                await deviceClient.CompleteAsync(receivedMessage);
            }
        }


    }
}
