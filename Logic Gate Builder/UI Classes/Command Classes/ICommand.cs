using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Gate_Builder.UI_Classes.Command_Classes
{
    public interface ICommand
    {
        string getCommandType();
        void undo(ref int componentCount);
        void redo(ref int componentCount);
        string debugInfo();
    }
}
