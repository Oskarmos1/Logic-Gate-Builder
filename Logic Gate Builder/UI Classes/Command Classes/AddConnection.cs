using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Gate_Builder.UI_Classes.Command_Classes
{
    public class AddConnection:ICommand
    {
        private string commandType = "ADDC";
        private MyList<Connection> connection = new MyList<Connection>();
        private int inputNum;
        private int connectionListReference;

        public AddConnection(Connection tC, int cLR) {
            connection.add(tC);

            string sGN = tC.getSourceG().getGate().getName();
            dynamic tG = tC.getTargetG().getGate();
            MyList<InputNode> inputNodes = tG.getInputs();
            inputNum = -1;
            for (int i = 0; i < inputNodes.getLength(); i++) {
                try
                {
                    if (inputNodes.getItem(i).getPreviousOutputGate() == sGN)
                    {
                        inputNum = i;
                    }
                }
                catch { }
            }

            connectionListReference = cLR;
        }
        public string getCommandType()
        {
            return commandType;
        }
        public void undo(ref int componentCount)
        {
            connection.add(connection.getItem(0).exportConnection());
            MainForm mf = Program.mainForm;
            dynamic tG = connection.getItem(0).getTargetG().getGate();
            MyList<InputNode> inputList = tG.getInputs();
            tG.deleteInputConnectionFromPreviousGateName(connection.getItem(0).getSourceG().getGate().getName());
            connection.removeAt(0);
            mf.getCanvasPanel().getConnectionList().removeAt(connectionListReference);
            mf.getCanvasPanel().Invalidate();

        }
        public void redo(ref int componentCount)
        {
            MainForm mf = Program.mainForm;
            dynamic tG = connection.getItem(0).getTargetG().getGate();
            dynamic sG = connection.getItem(0).getSourceG().getGate();
            if (tG.getGateType() == "CUSTOM")
            {
                tG.connectToInput(sG.getOutput(), inputNum);
            }
            else {
                tG.connectToInput(sG.getOutput(), inputNum+1);
            }
            mf.getCanvasPanel().getConnectionList().add(connection.getItem(0));
            mf.getCanvasPanel().Invalidate();

        }
        public Connection getCurrentConnection() {
            return connection.getItem(0);
        }

        public string debugInfo() {
            return commandType;      
        }
    }
}
