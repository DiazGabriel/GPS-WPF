using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcourBack
{
    public class PositionVille
    {
        public PositionVille() { }

        public PositionVille(string name, double posXMin, double posXMax, double posYMin, double posYMax)
        {
            this.name = name;
            this.posXMin = posXMin;
            this.posXMax = posXMax;
            this.posYMin = posYMin;
            this.posYMax = posYMax;
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private double posxMin, posyMin, posxMax, posyMax;

        public double posXMin
        {
            get { return posxMin; }
            set { posxMin = value; }
        }

        public double posYMin
        {
            get { return posyMin; }
            set { posyMin = value; }
        }

        public double posXMax
        {
            get { return posxMax; }
            set { posxMax = value; }
        }

        public double posYMax
        {
            get { return posyMax; }
            set { posyMax = value; }
        }
    }
}
