using System;
using System.Collections.Generic;
using System.Text;

namespace PartyBox_2021.LineState
{
    class ReadyLineState : IStateLine
    {

        Line _line;
        public ReadyLineState(Line line)
        {
            _line = line;
        }
        public void StartDispensing()
        {
            Console.WriteLine("Запуск розлива на линии.");
            Console.WriteLine("Линия переходит в состояние WorkLineState.");
            _line.SetState(_line.WorkLine);
            _line.Rele_On();
          
            
        }

        public void StopDispensing()
        {
            Console.WriteLine("Ошибка. На линии не осуществляется розлив.");
        }

        public void LoadLine()
        {
            Console.WriteLine("Ошибка. Линия уже загруженна, сначала освободите линию.");
        }

        public void FreeTheLine()
        {
            Console.WriteLine("Линии освобождена.");
            Console.WriteLine("Линия переходит в состояние EmptyLineState.");
            _line.SetState(_line.EmptyLine);
        }

    }
}
