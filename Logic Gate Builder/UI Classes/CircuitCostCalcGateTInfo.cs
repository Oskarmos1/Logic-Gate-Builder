using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Gate_Builder.UI_Classes.Command_Classes
{
    public class CircuitCostCalcGateTInfo
    {
        private string type;
        private int amount;

        public string getType() {
            return type;
        }

        public int getAmount() {
            return amount;
        }

        public void setType(string t) { 
            type = t;
        }

        public void setAmount(int a) {
            amount = a;
        }
    }
}
