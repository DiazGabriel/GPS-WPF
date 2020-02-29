using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcourBack
{
    public class Parcour
    {
        public Random iterable = new Random();
        public int getIterateur()
        {
            int nb = 0;
            bool form = false;
            while (form != true)
            {
                // On vérifie que la saisie correspond bien à un nombre
                try
                {
                    nb = int.Parse(Console.ReadLine());
                    form = true;
                }
                catch
                {
                    form = false;
                }

            }
            return nb;
        }

        public void printChemins(List<Chemin> listeChemins, List<Ville> listeVilles)
        {
            for (int i = 0; i < listeChemins.Count; i++)
            {
                Console.WriteLine("---------------------------------------------------");
                for (int j = 0; j < listeVilles.Count; j++)
                {
                    Console.WriteLine($"\t {j + 1}: {listeChemins[i].chemin[j].Name} ({listeChemins[i].chemin[j].posX}, {listeChemins[i].chemin[j].posY})");
                }
                Console.WriteLine($"\n\t => Score: {listeChemins[i].Score}");
                Console.WriteLine("---------------------------------------------------");
            }
        }

        public int moyenne(double[] moy, List<Chemin> listeCheminAleatoire, int stop)
        {
            if (moy.Equals(0))
            {
                for (int i = 0; i < listeCheminAleatoire.Count; i++)
                {
                    moy.SetValue(moy[0] + listeCheminAleatoire[i].Score, 0);
                }
                moy.SetValue(moy[0] / (listeCheminAleatoire.Count), 0);
            }
            else
            {
                for (int i = 0; i < listeCheminAleatoire.Count; i++)
                {
                    moy.SetValue(moy[1] + listeCheminAleatoire[i].Score, 1);
                }
                moy.SetValue(moy[1] / (listeCheminAleatoire.Count), 1);
            }

            if (moy[1] > (moy[0] - 0.01) || moy[1] < (moy[0] + 0.01))
            {
                stop++;
                moy.SetValue(moy.GetValue(1), 0);
            }
            else
            {
                stop = 0;
            }

            return stop;
        }

        public Chemin generateCheminAleatoire(List<Ville> listeVilles)
        {

            int index;
            // Ex: Si on a 6 ville, on va donc créer une liste contenant 4 nombre tiré de manière alatoire entre 1 et 4
            List<int> aleatoireUnique = new List<int>();
            for (int i = 1; i < listeVilles.Count - 1; i++) { aleatoireUnique.Add(i); }
            List<int> aleatoire = new List<int>();
            int j = listeVilles.Count - 2;
            for (int ctr = 0; ctr < listeVilles.Count - 2; ctr++)
            {
                index = iterable.Next(0, j);
                aleatoire.Add(aleatoireUnique[index]);
                aleatoireUnique.RemoveAt(index);
                j--;
            }
            // A partir de cette liste on extrait les 6 villes dans l'ordre donnée pour faire un chemin
            List<Ville> listeVilleAleatoire = new List<Ville>();

            listeVilleAleatoire.Add(listeVilles[0]);
            for (j = 0; j < aleatoire.Count; j++)
            {
                index = aleatoire[j];
                listeVilleAleatoire.Add(listeVilles[index]);
            }
            listeVilleAleatoire.Add(listeVilles[listeVilles.Count - 1]);

            // Pour finir on transforme c'est liste de ville en un chemin avec un score à 0
            Chemin cheminAleatoire = new Chemin(listeVilleAleatoire, 0);

            return cheminAleatoire;
        }

        public List<Chemin> XOver(int nombreChemins, int iterateur, List<Chemin> listeCheminAleatoire, List<Ville> listeVilles)
        {
            List<Chemin> XOverListChemin = new List<Chemin>();
            while (iterateur > 0)
            {
                List<Ville> ville1 = new List<Ville>();
                List<Ville> ville2 = new List<Ville>();

                // On tire 2 index random
                int index1 = 0;
                int index2 = 0;
                if (nombreChemins > 3)
                {
                    index1 = iterable.Next(1, nombreChemins - 2);
                    index2 = iterable.Next(1, nombreChemins - 2);
                }
                else
                {
                    index1 = 1;
                    index2 = 1;
                }

                // On s'assure qu'ils soient bien distinct
                while (index1 == index2 && nombreChemins > 3)
                {
                    index2 = iterable.Next(1, nombreChemins - 2);
                }

                // Puis on extrait nos deux chemin
                Chemin chemin1 = new Chemin(listeCheminAleatoire[index1]);
                Chemin chemin2 = new Chemin(listeCheminAleatoire[index2]);

                // On définie le pivot aléatoirement et on construit 2 liste de ville en fonction
                // Ex: Si on a 6 villes on prend un nombre entre 1 et 4
                index1 = iterable.Next(1, listeVilles.Count - 2);
                for (int i = 0; i < listeVilles.Count - 1; i++)
                {
                    if (i < index1)
                    {
                        ville1.Add(chemin1.chemin[i]);
                        ville2.Add(chemin2.chemin[i]);
                    }
                    if (i >= index1)
                    {
                        ville1.Add(chemin2.chemin[i]);
                        ville2.Add(chemin1.chemin[i]);
                    }
                }

                // En sortie, on contôle les double de ville et on les remplace par celles manquantes
                List<Ville> ControleUnique1 = new List<Ville>();
                List<Ville> ControleUnique2 = new List<Ville>();

                // On commance par alimenter deux liste avec les valeurs de nos chemins.
                // Si une ville a déja été ajouté, on la supprime du chemin
                for (int i = 0; i < ville1.Count; i++)
                {
                    if (ControleUnique1.Contains(ville1[i]))
                    {
                        ville1.RemoveAt(i);
                        i--;
                    }
                    else
                    {
                        ControleUnique1.Add(ville1[i]);
                    }
                }
                for (int i = 0; i < ville2.Count; i++)
                {
                    if (ControleUnique2.Contains(ville2[i]))
                    {
                        ville2.RemoveAt(i);
                        i--;
                    }
                    else
                    {
                        ControleUnique2.Add(ville2[i]);
                    }
                }

                // A cette étape, on a supprimer les doublons, il reste à rajouter les listes manquantes
                // Pour ce faire, on part de la liste initiale de toutes les villes, on supprime les éléments présents dans ville1&2
                // afin de conserer uniquement les valeurs manquantes
                ControleUnique1 = new List<Ville>(listeVilles);
                ControleUnique2 = new List<Ville>(listeVilles);
                for (int i = 0; i < ville1.Count; i++)
                {
                    ControleUnique1.RemoveAll(A => A.Name == ville1[i].Name);
                }
                for (int i = 0; i < ville2.Count; i++)
                {
                    ControleUnique2.RemoveAll(B => B.Name == ville2[i].Name);
                }

                // En sortie de la boucle, on a supprimé les doublons présents dans les chemins et on a deux listes de ville contenant celles manquantes
                // A partir de la, ils nous reste à compléter les listes de villes avec les deux autres
                for (int i = 0; i < ControleUnique1.Count; i++)
                {
                    ville1.Add(ControleUnique1[i]);
                }
                for (int i = 0; i < ControleUnique2.Count; i++)
                {
                    ville2.Add(ControleUnique2[i]);
                }

                // Maintenant que l'on a terminer d traiter nos villes, on les ajoute à la liste de chemin du Xover et on repart pour un tour de boucle
                Chemin CheminXover1 = new Chemin(ville1, 0);
                XOverListChemin.Add(CheminXover1);
                Chemin CheminXover2 = new Chemin(ville2, 0);
                XOverListChemin.Add(CheminXover2);
                iterateur--;
            }
            return XOverListChemin;
        }

        public List<Chemin> mutation(List<Chemin> XOverListChemin, List<Ville> listeVilles, int nombreChemins)
        {
            // Mutation : On va itérer sur la liste de chemin en sorti du Xover
            // Pour chaque chemin, on défini deux villes aléatoirement puis on les switch
            List<Chemin> listeCheminAleatoire = new List<Chemin>();
            Ville mutation1;
            Ville mutation2;
            for (int i = 0; i < XOverListChemin.Count; i++)
            {
                // On tire 2 index random
                int index1 = iterable.Next(1, listeVilles.Count - 2);
                int index2 = iterable.Next(1, listeVilles.Count - 2);

                // On s'assure qu'ils soient bien distinct
                int compteur = 0;
                while (index1 == index2)
                {
                    // Dans le cas ou nous avons quattre chemin l'algo bloque ici, on le force 
                    compteur++;
                    index2 = iterable.Next(1, listeVilles.Count - 2);
                    if (compteur == 1000)
                    {
                        index2++;
                    }
                }

                // On sauvegarde les valeurs avant de les attribuer
                mutation1 = XOverListChemin[i].chemin[index1];
                mutation2 = XOverListChemin[i].chemin[index2];
                XOverListChemin[i].chemin[index1] = mutation2;
                XOverListChemin[i].chemin[index2] = mutation1;
            }
            return XOverListChemin;
        }
    }
}
