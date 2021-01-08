using System;
using System.Collections.Generic;
using System.Text;

namespace PartyBox_2021.LineState
{
    class EmptyLineState : IStateLine
    {
        Line _line;
        public EmptyLineState (Line line)
        {
            _line = line;
        }

        public void StartDispensing ()
        {
           // Console.WriteLine("Ошибка. Линия пустая, розлив невозможен");
        }

        public void StopDispensing ()
        {
            Console.WriteLine("Ошибка. Линия пустая остановка линии невозможна");
        }

        public void LoadLine()
        {
            Console.WriteLine("Запуск процедуры зыгрузки.");
            Console.WriteLine("Линия переходит в состояние ReadyLineState.");
            _line.SetState(_line.ReadyLine);
            
        }

        public void FreeTheLine ()
        {

        }
    }
}
