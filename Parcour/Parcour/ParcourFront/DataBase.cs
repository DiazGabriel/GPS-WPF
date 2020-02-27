using System;
using System.Collections.Generic;
using SQLite;
using ParcourBack;
using System.IO;

namespace ParcourFront
{
    class DataBase
    {
        // Récupération du chemin vers notre fichier de base de données
        private string _dbPath = "../MyDatabase.db3";

        public List<PositionVille> getPositionVille(double x, double y)
        {
            List<PositionVille> positionVilles = new List<PositionVille>();
            // Instanciation de notre connexion
            SQLiteConnection connection = new SQLiteConnection(_dbPath);

            if (connection.GetTableInfo("PositionVille").Count != 0)
            {
                // Initialisation de la reqête SQLite
                string myQuery = "SELECT * FROM PositionVille " +
                    "WHERE ( " + (int)x + " BETWEEN posXMin AND posXMax) " +
                    "AND ( " + (int)y + " BETWEEN posYMin AND posYMax)";

                // Récupération du résultat dans une liste
                positionVilles = connection.Query<PositionVille>(myQuery);
            }
            return positionVilles;
        }

        public void addPositionVilles()
        {
            SQLiteConnection connection = new SQLiteConnection(_dbPath);

            // On test si la table existe déjà
            // Si non on la cré et on la remplie
            if (connection.GetTableInfo("PositionVille").Count != 0)
            {
                Console.WriteLine("*****Table PositionVille exists*****");
            }
            else
            {
                // On lit le fichier contenant les données ligne par ligne
                string textFile = "./../../ressources/coords.txt";
                string[] lines = File.ReadAllLines(textFile);

                connection.CreateTable<PositionVille>();
                foreach (string line in lines)
                {
                    // On traite chaque ligne et on cré un objet de type PositionVille
                    string[] splittedLine = line.Split(';');
                    PositionVille pv = new PositionVille(
                        splittedLine[0],
                        Convert.ToDouble(splittedLine[1]),
                        Convert.ToDouble(splittedLine[2]),
                        Convert.ToDouble(splittedLine[3]),
                        Convert.ToDouble(splittedLine[4])
                        );
                    
                    // On insère l'objet
                    connection.Insert(pv);
                }
                Console.WriteLine("*****Table PositionVilles created*****");
            }
        }

        public void printAllPositionVilles()
        {
            SQLiteConnection connection = new SQLiteConnection(_dbPath);

            List<PositionVille> list = connection.Query<PositionVille>("SELECT * FROM PositionVille");
            foreach (PositionVille pv in list)
            {
                Console.WriteLine($"Name: {pv.Name}\tposxMin: {pv.posXMin}\tposxMax: {pv.posXMax}\tposyMin: {pv.posYMin}\tposyMax: {pv.posYMax}");
            }
        }

        public void deletePositionVillesTable()
        {
            SQLiteConnection connection = new SQLiteConnection(_dbPath);
            if (connection.GetTableInfo("PositionVille").Count != 0)
            {
                connection.Query<PositionVille>("DROP TABLE PositionVille");
                Console.WriteLine("*****Table PositionVille deleted*****");
            }
            else
            {
                Console.WriteLine("*****Table PositionVille don't exist*****");
            }
        }
    }
}
