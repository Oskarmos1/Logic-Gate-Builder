using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Gate_Builder.UI_Classes.Command_Classes
{
    public class DeleteGate : ICommand
    {
        private string commandType = "DELETE";
        private MyList<GateComp> gateCompL = new MyList<GateComp>();
        private int referenceToGateCompStates;
        public DeleteGate(GateComp gc, int rTGCS)
        {
            referenceToGateCompStates = rTGCS;
            gateCompL.add(gc.ExportState());
        }

        public string getCommandType()
        {
            return commandType;
        }

        public void undo(ref int componentCount)
        {
            MainForm mf = Program.mainForm;
            mf.getGateCompStates().setVal(referenceToGateCompStates, gateCompL.getItem(0));

            mf.addComponentFromOutsideForm(gateCompL.getItem(0));
        }

        public void redo(ref int componentCount)
        {
            MainForm mf = Program.mainForm;
            gateCompL.add(gateCompL.getItem(0).ExportState());
            gateCompL.removeAt(0);
            mf.getGateCompStates().getItem(referenceToGateCompStates).deleteSelfFromOutside();
            componentCount--;
        }

        public string debugInfo()
        {
            return commandType;
        }
    }
}
