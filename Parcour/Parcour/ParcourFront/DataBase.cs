using System;
using System.Collections.Generic;
using SQLite;
using ParcourBack;

namespace ParcourFront
{
    class DataBase
    {
        // Récupération du chemin vers notre fichier de base de données
        private string _dbPath = "../MyDatabase.db3";

        public List<PositionVille> getPositionVille(double x, double y)
        {
            // Instanciation de notre connexion
            SQLiteConnection connection = new SQLiteConnection(_dbPath);

            // Initialisation de la reqête SQLite
            string myQuery = "SELECT * FROM PositionVille " +
                "WHERE ( " + x + " BETWEEN posXMin AND posXMax) " +
                "AND ( " + y + " BETWEEN posYMin AND posYMax)";

            // Récupération du résultat dans une liste
            List<PositionVille> positionVilles = connection.Query<PositionVille>(myQuery);
            return positionVilles;
        }
    }
}
