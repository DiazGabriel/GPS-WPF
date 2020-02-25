using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcourBack
{
    class Program
    {
        static void Main(string[] args)
        {
            Parcour p = new Parcour();
            Chemin ch = new Chemin();
            // On commance par créer une liste de 6 villes auxquelles on attribu des coordonnées
            List<Ville> listeVilles = new List<Ville>() {
                new Ville("Nice", 7.2661, 43.7031),
                new Ville("Antibes", 7.1167, 43.5833),
                new Ville("Cagnes Sur Mer", 7.15, 43.6667),
                new Ville("Marseille", 5.4, 43.3),
                new Ville("Cannes", 7.0167, 43.55),
                new Ville("St Tropez", 6.6333, 43.2667)
            };

            // Le but de cette partie de l'algo est de créer un liste de chemin à partir de ville tiré aléatoirement
            // On itère dessus N fois de sorte à avoir N chemin aléatoire en sortie
            List<Chemin> listeCheminAleatoire = new List<Chemin>();

            // L'utilisateur est solicité pour saisir le nombre de chemin
            int nombreChemins = 0;
            while (nombreChemins < 3)
            {
                Console.Write("Veuillez saisir le nombre de chemins souhaité (> 2): ");
                nombreChemins = p.getIterateur();
            }
            int iterateur = nombreChemins;

            if (listeVilles.Count > 3)
            {
                // On génère des chemins aléatoires qu'on ajoute dans notre liste de chemins
                while (iterateur > 0)
                {
                    listeCheminAleatoire.Add(p.generateCheminAleatoire(listeVilles));
                    iterateur--;
                }

                // On vérifie les chemin dans la console
                Console.WriteLine("\n********************************************************Chemins********************************************************");
                p.printChemins(listeCheminAleatoire, listeVilles);

                // Xover : on va mixer les chemin en se basant sur un pivot. 
                // En sortie de cette étape on ressort le nombre de chemin originel X N afin d'avoir un échantillonage correct
                while (iterateur < 1)
                {
                    Console.Write("Veuillez saisir le nombre d'itérations pour le XOver (> 0): ");
                    iterateur = nombreChemins * p.getIterateur();
                }
                //iterateur = nombreChemins * nombreChemins - 1;
                int stop = 0;
                double[] moy = { 0, 0 };
                while (stop < listeVilles.Count * iterateur)
                {
                    List<Chemin> XOverListChemin = p.XOver(nombreChemins, iterateur, listeCheminAleatoire, listeVilles);

                    // Mutation : On va itérer sur la liste de chemin en sorti du Xover
                    // Pour chaque chemin, on défini deux villes aléatoirement puis on les switch

                    XOverListChemin = p.mutation(XOverListChemin, listeVilles, nombreChemins);
                    // Elite : on calcule le score de tout les chemin
                    ch.calculScore(XOverListChemin, listeVilles);
                    // On vérifie dans la console
                    Console.WriteLine("\n*********************************************************Xover*********************************************************");
                    p.printChemins(XOverListChemin, listeVilles);
                    //XOverListChemin = p.dropDoubles(XOverListChemin);

                    var Query =
                        from chemin in XOverListChemin
                        orderby chemin.Score ascending
                        select chemin;
                    listeCheminAleatoire = Query.Take(nombreChemins).ToList();

                    stop = p.moyenne(moy, listeCheminAleatoire, stop);
                }
                var Query1 =
                        from chemin in listeCheminAleatoire
                        orderby chemin.Score ascending
                        select chemin;
                listeCheminAleatoire = Query1.Take(1).ToList();
            }
            else
            {
                Chemin chemin = new Chemin(listeVilles, 0);
                listeCheminAleatoire.Add(chemin);
                ch.calculScore(listeCheminAleatoire, listeVilles);
            }
            Console.WriteLine("\n*********************************************************Query*********************************************************");
            p.printChemins(listeCheminAleatoire, listeVilles);
            Console.ReadLine();
        }
    }
}
