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
        static ServiceClient serviceClient;
        static string connectionString = "HostName=TheIntouchablesOOP.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=QTiRXXO6nteHDLVs0Gx1rE+oUbqqOL5fmAoO9ZP3B4w=";
        public MainPage()
        {
            InitializeComponent();
            serviceClient = ServiceClient.CreateFromConnectionString(connectionString);
        }

        private async void button_Click(object sender, RoutedEventArgs e)
        {
            await SendCloudToDeviceMessageAsync("1");
        }

        private async void Up_Click(object sender, RoutedEventArgs e)
        {
            await SendCloudToDeviceMessageAsync("Up");
        }
        private async void Down_Click(object sender, RoutedEventArgs e)
        {
            await SendCloudToDeviceMessageAsync("Down");
        }
        private async void Right_Click(object sender, RoutedEventArgs e)
        {
            await SendCloudToDeviceMessageAsync("Right");
        }
        private async void Left_Click(object sender, RoutedEventArgs e)
        {
            await SendCloudToDeviceMessageAsync("Left");
        }




        private async static Task SendCloudToDeviceMessageAsync(string commandString)
        {
            var commandMessage = new Microsoft.Azure.Devices.Message(Encoding.ASCII.GetBytes(commandString));
            await serviceClient.SendAsync("TheIntouchables", commandMessage);
        }


    }
}
