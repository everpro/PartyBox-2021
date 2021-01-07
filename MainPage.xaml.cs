using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Devices.Gpio;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x419

namespace PartyBox_2021
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private int[] RELE_PIN = new int[] { 21, 20, 16, 12, 7, 8, 25, 24 };
        private int[] VOL_PIN  = new int[] { 23, 18, 26, 13, 6, 5, 11, 9 };
        private GpioPin[] RELE = new GpioPin[8];
        private GpioPin[] VOL  = new GpioPin[8];


        private GpioPinValue releValue1 = GpioPinValue.Low;

      
        public MainPage()
        {
         
            InitGPIO();
            this.InitializeComponent();
        }

        private void InitGPIO()
        {
            var gpio = GpioController.GetDefault();
/*
            if (gpio == null)
            {
                TextBox_ReleStatus.Text = "GPIO НЕ инициализировано!";
                return;
            }
            else TextBox_ReleStatus.Text = "GPIO инициализировано";
*/
            for (int i = 0; i<RELE.Length; i++)
            {
                RELE[i] = gpio.OpenPin(RELE_PIN[i]);
                VOL[i] = gpio.OpenPin(VOL_PIN[i]);
                RELE[i].Write(GpioPinValue.Low);
                RELE[i].SetDriveMode(GpioPinDriveMode.Output);
                VOL[i].SetDriveMode(GpioPinDriveMode.Input);

            }
           
        }

        private void toggleSwitch_1_Toggled(object sender, RoutedEventArgs e)
        {
            
            int number_rele = int.Parse(TexBox_RELE_ID.Text);

            if (toggleSwitch_1.IsOn)
            {

                RELE[number_rele].Write(GpioPinValue.High);
            }

            else
            {
                RELE[number_rele].Write(GpioPinValue.Low);
            }

        }

    }
}
