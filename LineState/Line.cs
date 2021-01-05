using System;
using System.Collections.Generic;
using System.Text;

namespace PartyBox_2021.LineState
{
    class Line
    {
        public ReadyLineState ReadyLine { get; private set; }
        public WorkLineState WorkLine { get; private set; }
        public EmptyLineState EmptyLine { get; private set; }

        private IStateLine _currentState;

        public Line ()
        {
            ReadyLine = new ReadyLineState(this);
            WorkLine = new WorkLineState(this);
            EmptyLine = new EmptyLineState(this);

            _currentState = EmptyLine;
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
    }
    
}
