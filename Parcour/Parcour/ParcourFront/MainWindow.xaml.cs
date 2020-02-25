using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using ParcourBack;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Linq;
using System;
using GalaSoft.MvvmLight.Command;

namespace ParcourFront
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Parcour p = new Parcour();
        public Chemin ch = new Chemin();
        public PositionVille pv = new PositionVille();
        public List<PositionVille> pvList = new List<PositionVille>();

        // On créer une nouvelle liste de ville, on y stockera les points saisie
        public List<Ville> listeVilles = new List<Ville>();
        public List<Chemin> listeCheminAleatoire = new List<Chemin>();
        public ObservableCollection<Ville> listeVillesAffichage = new ObservableCollection<Ville>();
        public ObservableCollection<Ville> listeCheminsAffichage = new ObservableCollection<Ville>();
        public ObservableCollection<Ville> listeResultatAffichage = new ObservableCollection<Ville>();
        public ObservableCollection<string> sortieRun = new ObservableCollection<string>();

        public bool IsInitNotEmpty = false;
        public bool IsXoverNotEmpty = false;
        public int nombreChemins;
        public int nombreTraitXOver;
        public MainWindow()
        {
            InitializeComponent();
            pvList = pv.initPositionsVilles();
            villes.ItemsSource = listeVillesAffichage;
            sortie.ItemsSource = sortieRun;
            resultats.ItemsSource = listeResultatAffichage;
        }

        // On capture les clicks sur la carte
        public void getCoordinate(object sender, MouseButtonEventArgs e)
        {
            // On récupère les coordonnées (x,y) de la souris
            Point p = Mouse.GetPosition(Carte);

            // Puis on affiche une inputBox pour demandé à l'utilisateur de saisir le nom de la ville 
            InputCityName getCityName = new InputCityName();
            string cityName = "";
            /*if (getCityName.ShowDialog() == true && getCityName.Answer != "")
            {
                cityName = getCityName.Answer;
            }*/

            foreach (PositionVille x in pvList)
            {
                if (p.X < x.posXMax && p.X > x.posXMin && p.Y < x.posYMax && p.Y > x.posYMin)
                {
                    cityName = x.Name;
                }
            }

            if (cityName.Equals(""))
            {
                if (getCityName.ShowDialog() == true && getCityName.Answer != "")
                {
                    cityName = getCityName.Answer;
                }
            }

            addVilleToList(cityName, p);

            EnableRunButton();
        }

        public void addVilleToList(string cityName, Point p)
        {
            Ville ville = new Ville(cityName, p.X, p.Y);
            listeVillesAffichage.Add(ville);
            listeVilles.Add(ville);
            // On ajouter un point à l'endroit du click
            drowPoint(p);
        }

        public void drowPoint(Point p)
        {
            Ellipse Circle = new Ellipse();
            SolidColorBrush mySolidColorBrush = new SolidColorBrush();
            mySolidColorBrush.Color = Colors.Red;
            Circle.Fill = mySolidColorBrush;
            Circle.StrokeThickness = 2;
            Circle.Stroke = Brushes.Red;
            Circle.Width = 5;
            Circle.Height = 5;
            Canvas.SetTop(Circle, p.Y - 2.5);
            Canvas.SetLeft(Circle, p.X - 2.5);
            CanvasCarte.Children.Add(Circle);
        }

        public void dropVilleFromList(object sender, MouseButtonEventArgs e)
        {
            //villes.Items.RemoveAt(villes.Items.IndexOf(villes.SelectedItem));
            Ville selectedVille = (Ville)this.villes.SelectedItem;
            listeVilles.RemoveAt(villes.SelectedIndex);
            CanvasCarte.Children.RemoveAt(villes.SelectedIndex + 1);
            listeVillesAffichage.RemoveAt(villes.SelectedIndex);
        }

        // Gestion des inputs de l'onglet setting : on force le user à saisir des nombre entier
        public void PutIntOnly(object sender, TextChangedEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            //e.Handled = regex.IsMatch(e.Text);
            // On en profite pour valider que les champs ont été saisis graçe à des booléens
            if (NbrCheminAnswer.Text.Equals(""))
            {
                IsInitNotEmpty = false;
            }
            else
            {
                if (!regex.IsMatch(NbrCheminAnswer.Text))
                {
                    IsInitNotEmpty = true;
                    nombreChemins = int.Parse(NbrCheminAnswer.Text);
                }
                else
                {
                    NbrCheminAnswer.Text = "";
                }
            }
            if (NbrXoverAnswer.Text.Equals(""))
            {
                IsXoverNotEmpty = false;
            }
            else
            {
                if (!regex.IsMatch(NbrXoverAnswer.Text))
                {
                    IsXoverNotEmpty = true;
                    nombreTraitXOver = int.Parse(NbrXoverAnswer.Text);
                }
                else
                {
                    NbrXoverAnswer.Text = "";
                }
            }


            EnableRunButton();

        }

        public void run(object sender, RoutedEventArgs e)
        {

            DateTime DateBegin = DateTime.Now;
            int nombreBoucle = 0;
            int nombreCheminEvaluer = 0;
            myTabControl.SelectedIndex = 2;
            if (listeVilles.Count > 3)
            {
                int iterateur = nombreChemins;
                int i = 0;
                while (iterateur > 0)
                {
                    Chemin chemin = p.generateCheminAleatoire(listeVilles);
                    listeCheminAleatoire.Add(chemin);
                    ch.calculScore(listeCheminAleatoire, listeVilles);

                    Ville chem = new Ville("", 0, 0);
                    foreach (Ville ch in chemin.chemin)
                    {
                        chem.Name += " -> " + ch.Name;
                    }

                    chem.Name += "\t=> Score: " + listeCheminAleatoire[i].Score;
                    listeCheminsAffichage.Add(chem);
                    iterateur--;
                    i++;
                }

                int stop = 0;
                double[] moy = { 0, 0 };
                iterateur = nombreChemins * nombreTraitXOver;
                while (stop < listeVilles.Count * iterateur)
                {
                    nombreBoucle++;

                    List<Chemin> XOverListChemin = p.XOver(nombreChemins, iterateur, listeCheminAleatoire, listeVilles);

                    // Mutation : On va itérer sur la liste de chemin en sorti du Xover
                    // Pour chaque chemin, on défini deux villes aléatoirement puis on les switch

                    XOverListChemin = p.mutation(XOverListChemin, listeVilles, nombreChemins);
                    // Elite : on calcule le score de tout les chemin
                    ch.calculScore(XOverListChemin, listeVilles);

                    //XOverListChemin = p.dropDoubles(XOverListChemin);

                    var Query =
                        from chemin in XOverListChemin
                        orderby chemin.Score ascending
                        select chemin;
                    listeCheminAleatoire = Query.Take(nombreChemins).ToList();

                    nombreCheminEvaluer += Query.Count();

                    stop = p.moyenne(moy, listeCheminAleatoire, stop);
                }
                Ville chemRes = new Ville("", 0, 0);
                foreach (Ville v in listeCheminAleatoire[0].chemin)
                {
                    chemRes.Name += " -> " + v.Name;
                }
                chemRes.Name += "\t=> Score: " + listeCheminAleatoire[0].Score;
                listeResultatAffichage.Add(chemRes);
                // Une fois l'éxecution terminé, on relie les points par le chemin optimal
                drawPath(listeCheminAleatoire[0].chemin);
            }
            else
            {
                Ville chemRes = new Ville("", 0, 0);
                List<Chemin> chem = new List<Chemin>();
                chem.Add(new Chemin(listeVilles, 0));
                foreach (Ville v in chem[0].chemin)
                {
                    chemRes.Name += " -> " + v.Name;
                }
                ch.calculScore(chem, listeVilles);
                chemRes.Name += "\t=> Score: " + chem[0].Score;
                listeResultatAffichage.Add(chemRes);
                // Une fois l'éxecution terminé, on relie les points par le chemin optimal
                drawPath(listeVilles);
            }

            DateTime DateEnd = DateTime.Now;

            // Une fois le traitement terminé, on affiche les stats recueillis
            sortieRun.Add("Date de début d'éxecution : " + DateBegin.ToString());
            sortieRun.Add("Date de fin d'éxecution : " + DateEnd.ToString());
            sortieRun.Add("");
            sortieRun.Add("Récap des paramètres d'initalisation");
            sortieRun.Add("Nombre de chemin initiaux (tirage aléatoire) : " + nombreChemins);
            sortieRun.Add("Nombre de chemin générés par le Xover : " + nombreTraitXOver);
            sortieRun.Add("");
            sortieRun.Add("Nombre de tour de boucle effectué par l'algorithme : " + nombreBoucle.ToString());
            sortieRun.Add("Nombre de chemins evalués par l'algorithme : " + nombreCheminEvaluer.ToString());
        }

        public void EnableRunButton()
        {
            // Si les deux input sont renseigné, on dégrise le bouton run
            if (IsXoverNotEmpty == true && IsInitNotEmpty == true && listeVilles.Count >= 3)
            {
                Run.IsEnabled = true;
            }
            else
            {
                Run.IsEnabled = false;
            }
        }

        public void drawPath(List<Ville> v)
        {
            for (int i = 0; i < (v.Count - 1); i++)
            {
                Line chemin = new Line();

                chemin.Stroke = Brushes.Red;

                chemin.X1 = v[i].posX;
                chemin.X2 = v[i + 1].posX;
                chemin.Y1 = v[i].posY;
                chemin.Y2 = v[i + 1].posY;

                chemin.StrokeThickness = 1;

                CanvasCarte.Children.Add(chemin);
            }
        }

        public void dropPathAndPoints()
        {
            while (CanvasCarte.Children.Count > 1)
            {
                CanvasCarte.Children.RemoveAt(CanvasCarte.Children.Count - 1);
            }
        }

        public void showResultOnMap(object sender, RoutedEventArgs e)
        {
            myTabControl.SelectedIndex = 0;
        }

        public void Reset(object sender, RoutedEventArgs e)
        {
            listeVillesAffichage.Clear();
            listeVilles.Clear();
            listeCheminAleatoire.Clear();
            sortieRun.Clear();
            listeCheminsAffichage.Clear();
            listeResultatAffichage.Clear();
            NbrCheminAnswer.Text = "";
            NbrXoverAnswer.Text = "";
            dropPathAndPoints();
        }
    }
}
