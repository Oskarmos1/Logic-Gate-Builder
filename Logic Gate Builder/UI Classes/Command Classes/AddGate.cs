using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Gate_Builder.UI_Classes.Command_Classes
{
    public class AddGate:ICommand
    {
        private string commandType = "ADD";
        private MyList<GateComp> gateCompL = new MyList<GateComp>();
        private int referenceToGateCompStates;
        public AddGate(GateComp gc, int refToGCS) {
            referenceToGateCompStates = refToGCS;
            gateCompL.add(gc);
        }

        public string getCommandType() {
            return commandType;
        }
        public void undo(ref int componentCount) {
            MainForm mf = Program.mainForm;
            gateCompL.add(gateCompL.getItem(0).ExportState());
            gateCompL.removeAt(0);
            mf.getGateCompStates().getItem(referenceToGateCompStates).deleteSelfFromOutside();
            componentCount--;
        }
        public void redo(ref int componentCount) {
            MainForm mf = Program.mainForm;
            mf.getGateCompStates().setVal(referenceToGateCompStates, gateCompL.getItem(0));
            mf.addComponentFromOutsideForm(gateCompL.getItem(0));
        }

        public string debugInfo()
        {
            return commandType;
        }
    }
}
