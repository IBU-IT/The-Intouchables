using System;
using Windows.Devices.Enumeration;
using System.Threading;
using Windows.UI.Xaml.Controls;
using Microsoft.Azure.Devices.Client;
using System.Text;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace RPifromCLoud
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        static DeviceClient deviceClient;
        static string iotHubUri = "//iotuhuburl//";
        static string deviceKey = "//iotdevicekey//";
        private string ReceivedDataFromCloud;
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void GetOrdersFromCloud()
        {
            while (true)
            {

                Message receivedMessage = await deviceClient.ReceiveAsync();
                if (receivedMessage == null) continue;
                ReceivedDataFromCloud = Encoding.ASCII.GetString(receivedMessage.GetBytes());
                textBox.Text = ReceivedDataFromCloud;
                await deviceClient.CompleteAsync(receivedMessage);
            }

        }
    }
}
