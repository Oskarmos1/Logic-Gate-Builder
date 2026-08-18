using Logic_Gate_Builder.UI_Classes;
using Logic_Gate_Builder.UI_Classes.Command_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Gate_Builder
{
    public partial class MainForm
    {
        public int getComponentCount() {
            return this.componentCount;
        }

        public void setComponentCount(int cC) {
            this.componentCount = cC;
        }

        public Stack<ICommand> getUndoStack() {
            return this.undoStack;
        }

        public MyList<GateComp> getGateCompStates() {
            return this.gateCompStates;
        }

        public ViewPanel getViewPanel() {
            return this.viewPanel;
        }

        public CanvasPanel getCanvasPanel() {
            return this.canvasPanel;
        }
    }
}
