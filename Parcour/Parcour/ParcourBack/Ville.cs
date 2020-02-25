using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcourBack
{
    public class Ville
    {
        public Ville() { }

        public Ville(String name, double posX, double posY)
        {
            this.name = name;
            this.posX = posX;
            this.posY = posY;
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private double posx, posy;

        public double posX
        {
            get { return posx; }
            set { posx = value; }
        }

        public double posY
        {
            get { return posy; }
            set { posy = value; }
        }
    }
}
