using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcourBack
{
    public class PositionVille
    {
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

        public PositionVille(string name, double posXMin, double posXMax, double posYMin, double posYMax)
        {
            this.name = name;
            this.posXMin = posXMin;
            this.posXMax = posXMax;
            this.posYMin = posYMin;
            this.posYMax = posYMax;
        }

        public PositionVille() { }

        public List<PositionVille> initPositionsVilles()
        {
            List<PositionVille> listePositionsVilles = new List<PositionVille>()
            {
                new PositionVille("Nice", 340, 370, 265, 290),
                new PositionVille("Tounon", 314, 345, 283, 310),
                new PositionVille("Marseille", 271, 312, 281, 307),
                new PositionVille("Montpelier", 220, 264, 281, 307),
                new PositionVille("Carcassone", 195, 236, 300, 328),
                new PositionVille("Foix", 168, 204, 308, 333),
                new PositionVille("Tarbes", 134, 161, 300, 328),
                new PositionVille("Pau", 91, 138, 293, 320),
                new PositionVille("Digne-Les-Bains", 308, 337, 257, 282),
                new PositionVille("Avignon", 283, 307, 264, 284),
                new PositionVille("Nimes", 245, 283, 264, 287),
                new PositionVille("Albi", 192, 222, 270, 296),
                new PositionVille("Toulouse", 160, 200, 282, 314),
                new PositionVille("Auch", 137, 174, 275, 301),
                new PositionVille("Mont-De-Marsan", 100, 142, 255, 291),
                new PositionVille("Rodez", 201, 239, 242, 286),
                new PositionVille("Gap", 306, 347, 231, 264),
                new PositionVille("Valence", 282, 310, 220, 262),
                new PositionVille("Privas", 258, 281, 223, 259),
                new PositionVille("Mende", 233, 258, 238, 267),
                new PositionVille("Cahors", 174, 205, 234, 263),
                new PositionVille("Agen", 141, 172, 247, 273),
                new PositionVille("Bordeaux", 109, 148, 219, 261),
                new PositionVille("Grenoble", 287, 328, 201, 241),
                new PositionVille("Le Puy En-Velay", 238, 271, 220, 242),
                new PositionVille("Aurillac", 206, 238, 218, 245),
                new PositionVille("Périgueux", 149, 184, 206, 249),
                new PositionVille("Chambéry", 309, 350, 198, 226),
                new PositionVille("Annecy", 311, 345, 176, 199),
                new PositionVille("Bourge-En-Bresse", 309, 330, 174, 199),
                new PositionVille("Lyon", 271, 290, 182, 210),
                new PositionVille("Saint-Etienne", 252, 276, 186, 215),
                new PositionVille("Clermont Ferrand", 217, 257, 188, 219),
                new PositionVille("Tulle", 182, 215, 206, 235),
                new PositionVille("Angoulème", 134, 167, 191, 222),
                new PositionVille("La Rochelle", 109, 138, 180, 226),
                new PositionVille("Limoges", 169, 197, 179, 213),
                new PositionVille("Guéret", 186, 216, 178, 210),
                new PositionVille("Lons-Le-Saunier", 299, 325, 142, 180),
                new PositionVille("Mâcon", 251, 295, 152, 180),
                new PositionVille("Moulins", 213, 157, 163, 190),
                new PositionVille("Pomers", 147, 175, 151, 187),
                new PositionVille("Niort", 125, 143, 150, 190),
                new PositionVille("La Roche-SurYon", 89, 126, 150, 178),
                new PositionVille("Châteauroux", 172, 204, 144, 175),
                new PositionVille("Bourges", 201, 229, 130, 166),
                new PositionVille("Nevers", 231, 258, 234, 164),
                new PositionVille("Tours", 149, 181, 126, 158),
                new PositionVille("Dijon", 262, 297, 113, 153),
                new PositionVille("Besançon", 308, 339, 130, 153),
                new PositionVille("Belfort", 333, 343, 117, 126),
                new PositionVille("Colmar", 338, 354, 98, 128),
                new PositionVille("Vesoul", 299, 331, 110, 135),
                new PositionVille("Auxerre", 229, 265, 99, 134),
                new PositionVille("Orléans", 191, 231, 101, 127),
                new PositionVille("Belios", 169, 206, 110, 142),
                new PositionVille("Angers", 113, 152, 122, 146),
                new PositionVille("Nantes", 78, 115, 122, 156),
                new PositionVille("Vannes", 52, 89, 102, 127),
                new PositionVille("Quimper", 20, 51, 78, 112),
                new PositionVille("Saint-Brieuc", 53, 93, 74, 102),
                new PositionVille("Rennes", 88, 117, 85, 116),
                new PositionVille("Laval", 118, 141, 93, 117),
                new PositionVille("Le Mans", 139, 169, 94, 128),
                new PositionVille("Chartres", 171, 201, 86, 111),
                new PositionVille("Aleçon", 129, 170, 75, 92),
                new PositionVille("Saint-Lô", 101, 122, 43, 85),
                new PositionVille("Cean", 124, 157, 57, 74),
                new PositionVille("Epinal", 301, 340, 93, 112),
                new PositionVille("Chaumont", 278, 306, 86, 124),
                new PositionVille("Troyes", 245, 279, 86, 111),
                new PositionVille("Nancy", 305, 339, 70, 93),
                new PositionVille("Strasbourg", 343, 367, 65, 96),
                new PositionVille("Metz", 313, 344, 50, 78),
                new PositionVille("Bar-Le-Duc", 284, 302, 50, 92),
                new PositionVille("Châlong-En-Champagne", 247, 282, 58, 84),
                new PositionVille("Charleville-Mézières", 258, 286, 30, 58),
                new PositionVille("Laon", 233, 255, 29, 66),
                new PositionVille("Beauvais", 196, 229, 45, 67),
                new PositionVille("Rouen", 154, 192, 34, 50),
                new PositionVille("Amiens", 190, 231, 22, 42),
                new PositionVille("Arras", 193, 228, 2, 22),
                new PositionVille("Lille", 243, 259, 2, 26),
                new PositionVille("Bastia", 366, 395, 291, 315),
                new PositionVille("Ajaccio", 372, 395, 319, 338),
                new PositionVille("Créteil Melun", 217, 241, 85, 102),
                new PositionVille("Evry", 197, 213, 82, 98),
                new PositionVille("Paris", 205, 218, 70, 82),
                new PositionVille("Nanterre Versailles", 165, 190, 68, 82),
                new PositionVille("Evreux", 161, 191, 56, 66),
                new PositionVille("Bobigny", 214, 240, 67, 79)
            };

            return listePositionsVilles;
        }
    }
}
