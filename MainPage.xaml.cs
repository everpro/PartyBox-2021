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
using PartyBox_2021.LineState;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x419

namespace PartyBox_2021
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {



        private int[] RELE_PIN = new int[] { 21, 20, 16, 12, 7, 8, 25, 24 };
        private int[] VOL_PIN = new int[] { 23, 18, 26, 13, 6, 5, 11, 9 };
        Line[] LineMass ;
        //  private GpioPinValue releValue1 = GpioPinValue.Low;

        public void ButStart()
        {
            
        }
      
        public MainPage()
        {
       

            this.InitializeComponent();
            LineMass = new Line[RELE_PIN.Length];
            for (int i=0; i<LineMass.Length; i++)
            {
                LineMass[i] = new Line(RELE_PIN[i], VOL_PIN[i]);
            }
            
            //  InitGPIO();

        }


        private void START_Click(object sender, RoutedEventArgs e)
        {
            LineMass[int.Parse(TexBox_LINE_ID.Text)].Task(int.Parse(TexBox_ING1.Text));
        }

        private void STOP_Click(object sender, RoutedEventArgs e)
        {
            LineMass[int.Parse(TexBox_LINE_ID.Text)].StopDispensing();
        }

        private void LOAD_Click(object sender, RoutedEventArgs e)
        {
            LineMass[int.Parse(TexBox_LINE_ID.Text)].LoadLine();
        }

        private void CLEAR_Click(object sender, RoutedEventArgs e)
        {
            LineMass[int.Parse(TexBox_LINE_ID.Text)].FreeTheLine();
        }

        private void TRIGGER_Click(object sender, RoutedEventArgs e)
        {
            LineMass[int.Parse(TexBox_LINE_ID.Text)].trigger = true;
        }

        private void VIEW_SENSOR_VALUE_Click(object sender, RoutedEventArgs e)
        {
          TEXTBOX_SENS.Text=Convert.ToString(LineMass[int.Parse(TexBox_LINE_ID.Text)].VOLUME_POURED);
        }

    }
}
