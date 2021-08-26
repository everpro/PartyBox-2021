using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartyBox_2021.ApparatState
{
    interface IStateApparat
    {

        void startWork ();
        void stopWork ();
        void getEvent ();
        void EnterSetup();


    }
}
