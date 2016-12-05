﻿using System;
using Windows.Devices.Enumeration;
using Windows.Devices.I2c;
using System.Threading;
using Windows.UI.Xaml.Controls;
using System.Text;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace RPitoArduinoI2C
{
    
    public sealed partial class MainPage : Page
    {
        private I2cDevice device;
        private Timer periodicTimer;
        private string recData; //variable in which will be stored data from cloud
        private static string AQS;
        private static DeviceInformationCollection DIS;
        public MainPage()
        {
            this.InitializeComponent();
            InitCommunication();
        }

        public async void InitCommunication()
        {
            var settings = new I2cConnectionSettings(0x40);
            settings.BusSpeed = I2cBusSpeed.StandardMode;
            AQS = I2cDevice.GetDeviceSelector("I2C1");
            DIS = await DeviceInformation.FindAllAsync(AQS);
            device = await I2cDevice.FromIdAsync(DIS[0].Id, settings);
        }
        private async void GetOrdersFromCloud()
        {
            while (true)
            {
                byte[] WriteBuf;
                if (recData == "1")
                {
                    WriteBuf = Encoding.ASCII.GetBytes("1"); //give order for going forward
                    device.Write(WriteBuf);
                    WriteBuf = Encoding.ASCII.GetBytes("0");//reset arduino and let it wait for new order
                    device.Write(WriteBuf);
                }
                else if (recData == "2")
                {
                    WriteBuf = Encoding.ASCII.GetBytes("2");//give order for going left
                    device.Write(WriteBuf);
                    WriteBuf = Encoding.ASCII.GetBytes("0"); //reset arduino and let it wait for new order
 
                     device.Write(WriteBuf);
                }
                else if (recData == "2")
                {
                    WriteBuf = Encoding.ASCII.GetBytes("3");//give order for going right
                    device.Write(WriteBuf);
                    WriteBuf = Encoding.ASCII.GetBytes("0"); //reset arduino and let it wait for new order
 
                     device.Write(WriteBuf);
                }
                else
                {
                    WriteBuf = Encoding.ASCII.GetBytes("0");// reset arduino and let it wait for new order
 
                     device.Write(WriteBuf);
                }

                
            }

        }
    }
}
