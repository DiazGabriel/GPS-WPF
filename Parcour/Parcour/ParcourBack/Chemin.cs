using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcourBack
{
    public class Chemin
    {
        public List<Ville> chemin = new List<Ville>();

        public Chemin() { }
        public Chemin(List<Ville> chemin, int score)
        {
            this.chemin = chemin;
            this.score = score;
        }

        public Chemin(Chemin previousChemin)
        {
            chemin = previousChemin.chemin;
        }

        private double score;
        public double Score
        {
            get { return score; }
            set { score = value; }
        }

        private Random rng = new Random();

        public void Shuffle()
        {
            int n = this.chemin.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                Ville value = this.chemin[k];
                this.chemin[k] = this.chemin[n];
                this.chemin[n] = value;
            }
        }

        public void calculScore(List<Chemin> XOverListChemin, List<Ville> listeVilles)
        {
            double distanceX;
            double distanceY;
            for (int i = 0; i < XOverListChemin.Count; i++)
            {
                for (int j = 0; j < Convert.ToInt32(XOverListChemin[i].chemin.Count)-1; j++)
                {
                    // On commance par calculer la distance entre les points
                    distanceX = Math.Abs(XOverListChemin[i].chemin[j].posX - XOverListChemin[i].chemin[j + 1].posX);
                    distanceY = Math.Abs(XOverListChemin[i].chemin[j].posY - XOverListChemin[i].chemin[j + 1].posY);
                    // Puis on stock la distance entre les deux villes
                    XOverListChemin[i].Score += Math.Sqrt(Math.Pow(distanceX, 2) + Math.Pow(distanceY, 2));
                }
            }
        }

        public int Factorial(int number)
        {
            if (number < 0) throw new ArgumentOutOfRangeException("number", "number should not be negative.");
            int result = 1;
            for (int i = 2; i <= number; i++)
            {
                result *= i;
            }

            return result;
        }
    }
}
