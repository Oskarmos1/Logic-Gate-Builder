using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Gate_Builder.UI_Classes
{
    public class Connection
    {
        private GateComp sourceG;
        private GateComp targetG;
        private Point sourceP;
        private Point targetP;
        public Connection(GateComp sourceG, GateComp targetG, Point sourceP, Point targetP)
        {
            this.sourceG = sourceG;
            this.targetG = targetG;
            this.sourceP = sourceP;
            this.targetP = targetP;
        }

        public Point getSourceP() {
            return sourceP;
        }

        public Point getTargetP() {
            return targetP;
        }

        public void setSourceP(Point newPoint) {
            sourceP = newPoint;
        }

        public void setTargetP(Point newPoint) {
            targetP = newPoint;
        }

        public GateComp getSourceG() {
            return sourceG;
        }

        public GateComp getTargetG() {
            return targetG;
        }

        public Connection exportConnection() {
            return new Connection(sourceG, targetG, sourceP, targetP);
        }
    }
}
