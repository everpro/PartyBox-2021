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
        private const int RELE_PIN1 = 21;
        private const int RELE_PIN2 = 20;
        private const int RELE_PIN3 = 16;
        private const int RELE_PIN4 = 12;
        private const int RELE_PIN5 = 7;
        private const int RELE_PIN6 = 8;
        private const int RELE_PIN7 = 25;
        private const int RELE_PIN8 = 24;

        private GpioPin relePin1;
        private GpioPin relePin2;
        private GpioPin relePin3;
        private GpioPin relePin4;
        private GpioPin relePin5;
        private GpioPin relePin6;
        private GpioPin relePin7;
        private GpioPin relePin8;

        private GpioPinValue releValue1 = GpioPinValue.Low;
        private GpioPinValue releValue2 = GpioPinValue.Low;
        private GpioPinValue releValue3 = GpioPinValue.Low;
        private GpioPinValue releValue4 = GpioPinValue.Low;
        private GpioPinValue releValue5 = GpioPinValue.Low;
        private GpioPinValue releValue6 = GpioPinValue.Low;
        private GpioPinValue releValue7 = GpioPinValue.Low;
        private GpioPinValue releValue8 = GpioPinValue.Low;

        private const int VOl_PIN1 = 23;
        private const int VOl_PIN2 = 18;
        private const int VOl_PIN3 = 26;
        private const int VOl_PIN4 = 13;
        private const int VOl_PIN5 = 6;
        private const int VOl_PIN6 = 5;
        private const int VOl_PIN7 = 11;
        private const int VOl_PIN8 = 9;

        private GpioPin volPin1;
        private GpioPin volPin2;
        private GpioPin volPin3;
        private GpioPin volPin4;
        private GpioPin volPin5;
        private GpioPin volPin6;
        private GpioPin volPin7;
        private GpioPin volPin8;

        Boolean[] releStatus = new Boolean[8] { false, false, false, false, false, false, false, false };
        public MainPage()
        {

            InitGPIO();
            this.InitializeComponent();
        }

        private void InitGPIO()
        {
            var gpio = GpioController.GetDefault();

            if (gpio == null)
            {
                TextBox_ReleStatus.Text = "GPIO НЕ инициализировано!";
                return;
            }
            else TextBox_ReleStatus.Text = "GPIO инициализировано";

            relePin1 = gpio.OpenPin(RELE_PIN1);
            relePin2 = gpio.OpenPin(RELE_PIN2);
            relePin3 = gpio.OpenPin(RELE_PIN3);
            relePin4 = gpio.OpenPin(RELE_PIN4);
            relePin5 = gpio.OpenPin(RELE_PIN5);
            relePin6 = gpio.OpenPin(RELE_PIN6);
            relePin7 = gpio.OpenPin(RELE_PIN7);
            relePin8 = gpio.OpenPin(RELE_PIN8);

            volPin1 = gpio.OpenPin(VOl_PIN1);
            volPin2 = gpio.OpenPin(VOl_PIN2);
            volPin3 = gpio.OpenPin(VOl_PIN3);
            volPin4 = gpio.OpenPin(VOl_PIN4);
            volPin5 = gpio.OpenPin(VOl_PIN5);
            volPin6 = gpio.OpenPin(VOl_PIN6);
            volPin7 = gpio.OpenPin(VOl_PIN7);
            volPin8 = gpio.OpenPin(VOl_PIN8);

            relePin1.Write(releValue1);
            relePin2.Write(releValue2);
            relePin3.Write(releValue3);
            relePin4.Write(releValue4);
            relePin5.Write(releValue5);
            relePin6.Write(releValue6);
            relePin7.Write(releValue7);
            relePin8.Write(releValue8);

            relePin1.SetDriveMode(GpioPinDriveMode.Output);
            relePin2.SetDriveMode(GpioPinDriveMode.Output);
            relePin3.SetDriveMode(GpioPinDriveMode.Output);
            relePin4.SetDriveMode(GpioPinDriveMode.Output);
            relePin5.SetDriveMode(GpioPinDriveMode.Output);
            relePin6.SetDriveMode(GpioPinDriveMode.Output);
            relePin7.SetDriveMode(GpioPinDriveMode.Output);
            relePin8.SetDriveMode(GpioPinDriveMode.Output);

            if (volPin1.IsDriveModeSupported(GpioPinDriveMode.InputPullDown) &&
               volPin2.IsDriveModeSupported(GpioPinDriveMode.InputPullDown) &&
               volPin3.IsDriveModeSupported(GpioPinDriveMode.InputPullDown) &&
               volPin4.IsDriveModeSupported(GpioPinDriveMode.InputPullDown) &&
               volPin5.IsDriveModeSupported(GpioPinDriveMode.InputPullDown) &&
               volPin6.IsDriveModeSupported(GpioPinDriveMode.InputPullDown) &&
               volPin7.IsDriveModeSupported(GpioPinDriveMode.InputPullDown) &&
               volPin8.IsDriveModeSupported(GpioPinDriveMode.InputPullDown))
            {
                volPin1.SetDriveMode(GpioPinDriveMode.InputPullDown);
                volPin2.SetDriveMode(GpioPinDriveMode.InputPullDown);
                volPin3.SetDriveMode(GpioPinDriveMode.InputPullDown);
                volPin4.SetDriveMode(GpioPinDriveMode.InputPullDown);
                volPin5.SetDriveMode(GpioPinDriveMode.InputPullDown);
                volPin6.SetDriveMode(GpioPinDriveMode.InputPullDown);
                volPin7.SetDriveMode(GpioPinDriveMode.InputPullDown);
                volPin8.SetDriveMode(GpioPinDriveMode.InputPullDown);

                TextBox_VolStatus.Text = "д.расх. успешн инц.";
            }
            else
            {
                volPin1.SetDriveMode(GpioPinDriveMode.Input);
                volPin2.SetDriveMode(GpioPinDriveMode.Input);
                volPin3.SetDriveMode(GpioPinDriveMode.Input);
                volPin4.SetDriveMode(GpioPinDriveMode.Input);
                volPin5.SetDriveMode(GpioPinDriveMode.Input);
                volPin6.SetDriveMode(GpioPinDriveMode.Input);
                volPin7.SetDriveMode(GpioPinDriveMode.Input);
                volPin8.SetDriveMode(GpioPinDriveMode.Input);
                TextBox_VolStatus.Text = "Ошибка инц. Д.Расх";
            }

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            /*
            sensorTarget1 = Convert.ToInt32(textBox_SetSensor_1.Text);
            sensorTarget2 = Convert.ToInt32(textBox_SetSensor_2.Text);
            sensorTarget3 = Convert.ToInt32(textBox_SetSensor_3.Text);
            sensorTarget4 = Convert.ToInt32(textBox_SetSensor_4.Text);
            sensorTarget5 = Convert.ToInt32(textBox_SetSensor_5.Text);
            sensor_TextBox_1.Text = Convert.ToString(sensorTarget1);
            sensor_TextBox_2.Text = Convert.ToString(sensorTarget2);
            sensor_TextBox_3.Text = Convert.ToString(sensorTarget3);
            sensor_TextBox_4.Text = Convert.ToString(sensorTarget4);
            sensor_TextBox_5.Text = Convert.ToString(sensorTarget5);

            FlowText.Text = Convert.ToString(sensor1);

    */
        }

        private void toggleSwitch_1_Toggled(object sender, RoutedEventArgs e)
        {
            if (toggleSwitch_1.IsOn)
            {

                rele_action_on(1);
            }

            else
            {
                rele_action_off(1);
            }

        }

        private void toggleSwitch_2_Toggled(object sender, RoutedEventArgs e)
        {
            if (toggleSwitch_2.IsOn)
            {

                rele_action_on(2);
            }

            else
            {
                rele_action_off(2);
            }
        }

        private void toggleSwitch_3_Toggled(object sender, RoutedEventArgs e)
        {
            if (toggleSwitch_3.IsOn)
            {

                rele_action_on(3);
            }

            else
            {
                rele_action_off(3);
            }
        }

        private void toggleSwitch_4_Toggled(object sender, RoutedEventArgs e)
        {
            if (toggleSwitch_4.IsOn)
            {

                rele_action_on(4);
            }

            else
            {
                rele_action_off(4);
            }
        }

        private void toggleSwitch_5_Toggled(object sender, RoutedEventArgs e)
        {
            if (toggleSwitch_5.IsOn)
            {

                rele_action_on(5);
            }

            else
            {
                rele_action_off(5);
            }
        }

        private void toggleSwitch_6_Toggled(object sender, RoutedEventArgs e)
        {
            if (toggleSwitch_6.IsOn)
            {

                rele_action_on(6);
            }

            else
            {
                rele_action_off(6);
            }
        }

        private void toggleSwitch_7_Toggled(object sender, RoutedEventArgs e)
        {
            if (toggleSwitch_7.IsOn)


                rele_action_on(7);


            else

                rele_action_off(7);

        }

        private void toggleSwitch_8_Toggled(object sender, RoutedEventArgs e)
        {
            if (toggleSwitch_8.IsOn)
            {

                rele_action_on(8);
            }

            else
            {
                rele_action_off(8);
            }
        }

        // Функции работы обрудования (релле, счетчик расхода, шаковый двигатель)----------------
        private void rele_action_on(int nuber_rele)
        {

            switch (nuber_rele)
            {
                case 1:
                    releStatus[0] = true;
                    releValue1 = GpioPinValue.High;
                    relePin1.Write(releValue1);
                    break;

                case 2:
                    releStatus[1] = true;
                    releValue2 = GpioPinValue.High;
                    relePin2.Write(releValue2);
                    break;
                case 3:
                    releStatus[2] = true;
                    releValue3 = GpioPinValue.High;
                    relePin3.Write(releValue3);
                    break;
                case 4:
                    releStatus[3] = true;
                    releValue4 = GpioPinValue.High;
                    relePin4.Write(releValue4);
                    break;
                case 5:
                    releStatus[4] = true;
                    releValue5 = GpioPinValue.High;
                    relePin5.Write(releValue5);
                    break;
                case 6:
                    releStatus[5] = true;

                    releValue6 = GpioPinValue.High;
                    relePin6.Write(releValue6);
                    break;
                case 7:
                    releStatus[6] = true;

                    releValue7 = GpioPinValue.High;
                    relePin7.Write(releValue7);
                    break;
                case 8:
                    releStatus[7] = true;

                    releValue8 = GpioPinValue.High;
                    relePin8.Write(releValue8);
                    break;
            }

        }
        private void rele_action_off(int nuber_rele)
        {

            switch (nuber_rele)
            {
                case 1:
                    releStatus[0] = false;
                    releValue1 = GpioPinValue.Low;
                    relePin1.Write(releValue1);
                    break;

                case 2:
                    releStatus[1] = false;
                    releValue2 = GpioPinValue.Low;
                    relePin2.Write(releValue2);
                    break;
                case 3:
                    releStatus[2] = false;
                    releValue3 = GpioPinValue.Low;
                    relePin3.Write(releValue3);
                    break;
                case 4:
                    releStatus[2] = false;
                    releValue4 = GpioPinValue.Low;
                    relePin4.Write(releValue4);
                    break;
                case 5:
                    releStatus[3] = false;
                    releValue5 = GpioPinValue.Low;
                    relePin5.Write(releValue5);
                    break;
                case 6:
                    releStatus[4] = false;
                    releValue6 = GpioPinValue.Low;
                    relePin6.Write(releValue6);
                    break;
                case 7:
                    releStatus[5] = false;
                    releValue7 = GpioPinValue.Low;
                    relePin7.Write(releValue7);
                    break;
                case 8:
                    releStatus[6] = false;
                    releValue8 = GpioPinValue.Low;
                    relePin8.Write(releValue8);
                    break;
            }

        }
    }
}
