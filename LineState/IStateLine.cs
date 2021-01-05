using System;
using System.Collections.Generic;
using System.Text;

namespace PartyBox_2021.LineState
{
    interface IStateLine
    {
        void StartDispensing();
        void StopDispensing();
        void LoadLine();
        void FreeTheLine();
    }
}
