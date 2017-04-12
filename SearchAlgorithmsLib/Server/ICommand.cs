using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public interface ICommand
    {
        string doMission(Params c);
        // פונקצייה שמקבלת את הפרמטריפ מהקליינט ומחזירה סטרינג 
    }
}
