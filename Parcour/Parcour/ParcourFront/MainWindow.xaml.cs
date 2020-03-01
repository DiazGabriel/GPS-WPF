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
        public bool notFirstRun = false;

        public MainWindow()
        {
            InitializeComponent();
            DataBase db = new DataBase();
            //db.deletePositionVillesTable();
            db.addPositionVilles();
            //db.printAllPositionVilles();
            this.StateChanged += new EventHandler(Window_StateChanged);
            villes.ItemsSource = listeVillesAffichage;
            sortie.ItemsSource = sortieRun;
            resultats.ItemsSource = listeResultatAffichage;
        }

        void Window_StateChanged(object sender, EventArgs e)
        {
            bool pathExist = false;
            double x, y;
            if (CanvasCarte.Children.Count > listeVilles.Count + 1)
            {
                pathExist = true;
            }
            switch (this.WindowState)
            {
                case WindowState.Maximized:
                    // x: 906, y: 751,84
                    x = 906;
                    y = 751.84;
                    dropPathAndPoints();
                    foreach (Ville v in listeVilles)
                    {
                        Point drowableP = new Point((v.posX / 100) * x, (v.posY / 100) * y);
                        drowPoint(drowableP.X, drowableP.Y);
                    }
                    if (pathExist)
                    {
                        if (listeVilles.Count > 3)
                        {
                            drowPath(listeCheminAleatoire[0].chemin, x, y);
                        }
                        else
                        {
                            drowPath(listeVilles, x, y);
                        }
                    }
                    break;
                case WindowState.Normal:
                    // x: 440,88, y: 363,44
                    x = 440.88;
                    y = 363.44;
                    dropPathAndPoints();
                    foreach (Ville v in listeVilles)
                    {
                        Point drowableP = new Point((v.posX / 100) * x, (v.posY / 100) * y);
                        drowPoint(drowableP.X, drowableP.Y);
                    }
                    if (pathExist)
                    {
                        if (listeVilles.Count > 3)
                        {
                            drowPath(listeCheminAleatoire[0].chemin, x, y);
                        }
                        else
                        {
                            drowPath(listeVilles, x, y);
                        }
                    }
                    break;
            }
        }

        // On capture les clicks sur la carte
        public void getCoordinate(object sender, MouseButtonEventArgs e)
        {
            // On récupère les coordonnées (x,y) de la souris
            Point exactP = Mouse.GetPosition(Carte);
            Point percentP = new Point((exactP.X / CanvasCarte.ActualWidth) * 100, (exactP.Y / CanvasCarte.ActualHeight) * 100);

            // Puis on va chercher le nom de la ville dans la base de données en utilisant les coordonnées récupérées
            // Si elle n'existe pas dans la base on affiche une inputBox pour demandé à l'utilisateur de saisir le nom de la ville 
            SelectCityName selectCityName = new SelectCityName();
            string cityName = "";
            DataBase db = new DataBase();
            List<PositionVille> selectedVille = db.getPositionVille(percentP.X, percentP.Y);
            //if(selectedVille.Count != 0) cityName = selectedVille[0].Name;

            if (selectedVille.Count == 1)
            {
                cityName = selectedVille[0].Name;
            }
            else
            {
                string question = "Quelle ville ?";
                foreach (PositionVille sv in selectedVille)
                {
                    question += "\n" + sv.Name + "?";
                }
                selectCityName.Question = question;
                selectCityName.ShowDialog();
                cityName = selectCityName.Answer;
            }

            addVilleToList(cityName, percentP);

            EnableRunButton();
        }

        public void addVilleToList(string cityName, Point percentP)
        {
            Ville ville = new Ville(cityName, percentP.X, percentP.Y);
            listeVillesAffichage.Add(ville);
            listeVilles.Add(ville);

            // On ajouter un point à l'endroit du click
            Point drowableP = new Point((percentP.X / 100) * CanvasCarte.ActualWidth, (percentP.Y / 100) * CanvasCarte.ActualHeight);
            drowPoint(drowableP.X, drowableP.Y);
        }

        public void drowPoint(double x, double y)
        {
            Ellipse Circle = new Ellipse();
            SolidColorBrush mySolidColorBrush = new SolidColorBrush();
            mySolidColorBrush.Color = Colors.Red;
            Circle.Fill = mySolidColorBrush;
            Circle.StrokeThickness = 2;
            Circle.Stroke = Brushes.Red;
            Circle.Width = 8;
            Circle.Height = 8;
            Canvas.SetTop(Circle, y - 4);
            Canvas.SetLeft(Circle, x - 4);
            CanvasCarte.Children.Add(Circle);
        }

        public void dropVilleFromList(object sender, MouseButtonEventArgs e)
        {
            //villes.Items.RemoveAt(villes.Items.IndexOf(villes.SelectedItem));
            Ville selectedVille = (Ville)this.villes.SelectedItem;
            if (selectedVille != null)
            {
                listeVilles.RemoveAt(villes.SelectedIndex);
                CanvasCarte.Children.RemoveAt(villes.SelectedIndex + 1);
                listeVillesAffichage.RemoveAt(villes.SelectedIndex);
            }
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
            double aw = CanvasCarte.ActualWidth;
            double ah = CanvasCarte.ActualHeight;
            clean(aw, ah);
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
                drowPath(listeCheminAleatoire[0].chemin, aw, ah);
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
                drowPath(listeVilles, aw, ah);
            }

            DateTime DateEnd = DateTime.Now;

            // Une fois le traitement terminé, on affiche les stats recueillies
            sortieRun.Add("Date de début d'exécution : " + DateBegin.ToString());
            sortieRun.Add("Date de fin d'exécution : " + DateEnd.ToString());
            sortieRun.Add("");
            sortieRun.Add("Récap des paramètres d'initalisation");
            sortieRun.Add("Nombre de chemins initiaux (tirage aléatoire) : " + nombreChemins);
            sortieRun.Add("Nombre de chemins générés par le Xover : " + nombreTraitXOver);
            sortieRun.Add("");
            sortieRun.Add("Nombre de tours de boucle effectuées par l'algorithme : " + nombreBoucle.ToString());
            sortieRun.Add("Nombre de chemins evalués par l'algorithme : " + nombreCheminEvaluer.ToString());
        }

        private void clean(double aw, double ah)
        {
            listeCheminAleatoire.Clear();
            sortieRun.Clear();
            listeCheminsAffichage.Clear();
            listeResultatAffichage.Clear();
            dropPathAndPoints();
            foreach (Ville v in listeVilles)
            {
                drowPoint((v.posX / 100) * aw, (v.posY / 100) * ah);
            }
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

        public void drowPath(List<Ville> v, double aw, double ah)
        {
            for (int i = 0; i < (v.Count - 1); i++)
            {
                Line chemin = new Line();

                chemin.Stroke = Brushes.Red;

                chemin.X1 = (v[i].posX / 100) * aw;
                chemin.X2 = (v[i + 1].posX / 100) * aw;
                chemin.Y1 = (v[i].posY / 100) * ah;
                chemin.Y2 = (v[i + 1].posY / 100) * ah;

                chemin.StrokeThickness = 2;

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

        private void villes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
