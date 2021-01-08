using System;
using System.Collections.Generic;
using System.Text;
using Windows.Devices.Gpio;

namespace PartyBox_2021.LineState
{
    class Line
    {
        private IStateLine _currentState;
        public ReadyLineState ReadyLine { get; private set; }
        public WorkLineState WorkLine { get; private set; }
        public EmptyLineState EmptyLine { get; private set; }

        private GpioPin RELE_OUT;
        private bool RELE_OUT_STATE = false;
        private GpioPin VOL_IN;

       public int VOLUME_POURED = 0;
        int VOLUME_TARGET = 0;
        int VOLUME_LEFT = 0;

        //---Переменные для теста
        public int VOLUME_TEST = 22;
        public bool trigger = false;
        //------------------
        

        public Line (int RELE_PIN_ID, int VOL_PIN_ID)
        {
            var gpio = GpioController.GetDefault();

            VOL_IN = gpio.OpenPin(VOL_PIN_ID);                   //утанавливаем номер пина (реле)
            RELE_OUT = gpio.OpenPin(RELE_PIN_ID);                //утанавливаем номер пина (датчик расхода)
            RELE_OUT.SetDriveMode(GpioPinDriveMode.Output);      //режим работы пина реле.
            VOL_IN.SetDriveMode(GpioPinDriveMode.Input);        //режим работы пина датчика расхода.
            RELE_OUT.Write(GpioPinValue.Low);                   //устанавливаем 0-сигнал на выход реле
            VOL_IN.ValueChanged += VOL_IN_ValueChanged;         //инициализация мониторинг вход для датчика расхода

            ReadyLine = new ReadyLineState(this);
            WorkLine = new WorkLineState(this);
            EmptyLine = new EmptyLineState(this);

            _currentState = EmptyLine; 
        }

        private void VOL_IN_ValueChanged(GpioPin sender, GpioPinValueChangedEventArgs e) // Срабатывает при изминение уровня сиганала на пине датчика расхода
        {
            
            if (e.Edge == GpioPinEdge.RisingEdge)
            {
                VOLUME_POURED++;
                if (RELE_OUT_STATE == true && VOLUME_POURED>VOLUME_TARGET)
                {
                    StopDispensing();
                }
          
            }
        }

        public void StartDispensing()
        {
            _currentState.StartDispensing();
            
        }

        public void StopDispensing()
        {
            _currentState.StopDispensing();
        }

        public void LoadLine()
        {
            _currentState.LoadLine();
        }

        public void FreeTheLine()
        {
            _currentState.FreeTheLine();
        }

        public void SetState (IStateLine state)
        {
            _currentState = state;
        }

        public void Task (int VOL)          //Функция Запуска задания розлива(необходимой объем жидкости)
        {
            VOLUME_TARGET = VOL;
            StartDispensing();
        }
        public void Rele_On()               //Функция Включения Реле
        {
            RELE_OUT.Write(GpioPinValue.High);
            RELE_OUT_STATE = true;
            VOLUME_POURED = 0;
        }
        public void Rele_Off()              //Функция Отключения Реле
        {
            RELE_OUT.Write(GpioPinValue.Low);
            RELE_OUT_STATE = false;
        }
    }
    
}
