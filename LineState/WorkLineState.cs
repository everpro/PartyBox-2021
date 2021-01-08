using System;
using System.Collections.Generic;
using System.Text;

namespace PartyBox_2021.LineState
{
    class WorkLineState : IStateLine
    {
        Line _line;
        public WorkLineState(Line line)
        {
            _line = line;
        }
        public void StartDispensing()
        {
            _line.Rele_On();
            
        }

        public void StopDispensing()
        {
            Console.WriteLine("Остановка розлива на линии.");
            Console.WriteLine("Линия переходит в состояние ReadyLineState.");
            _line.SetState(_line.ReadyLine);
            _line.Rele_Off();
        }

        public void LoadLine()
        {
            Console.WriteLine("Ошибка! Линия уже загруженна и выполняет розлив.");
        }

        public void FreeTheLine()
        {
            Console.WriteLine("Ошибка! Линия выполняет розлив. Сначала необходимо остановить розлив.");
        }
      
    }
}
