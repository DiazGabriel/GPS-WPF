# GPS - C# et WPF


## Développeurs : 

- Chatti Nader
- Diaz Gabriel


## Doc d'utilisation 

# Onglet Map

L'onglet Map contient une carte de la France et une ListView contenant 3 champs : Ville, Longitude, Latitude

- Un clic sur la carte permet d'ajouter une ville à la liste. Toutes les villes dont le nom apparaît sur la carte est répertorié dans la base de données SQLite. 
	- Si le clic se situe dans l'intervalle de coordonnées définies pour une ville donnée, le système récupère le nom de la ville automatiquement. 
	- Si le clic donne lieu à un conflit entre les coordonnées de plusieurs villes, une boite de dialogue apparaît permettant à l'utilisateur de choisir entre les villes résultantes de la requête en base. 
	- Si le clic ne correspond à aucune coordonnées répertorié en base, une boite de dialogue apparaît demandant à l'utilisateur de saisir le nom de la ville.
- Après chaque clic, un point rouge apparaît sur la carte pour notifier la saisie géographique de la ville et la ListView est enrichie d'une ligne supplémentaire.
- Un double clic sur une ligne de la ListView entraîne la suppression de la ville.
- Nous sommes partis du principe qu'un GPS "classique" auquel l'utilisateur demande d'aller à Paris en partant de Nice ne donnera pas la route à suivre pour aller de Paris jusqu'à Nice mais de Nice jusqu'à Paris. Notre algorithme applique le même principe : la première ville contenu dans la ListView est celle de départ et la dernière celle d'arrivée. 

# Onglet Setting

L'onglet Setting contient les paramètres d'entrée de l'application et le bouton "Run" permettant de lancer l'algorithme.

-  Le 1er champ de saisie permet de renseigner le nombre de chemins qui seront tirés aléatoirement en début d'algo et implicitement le nombre de chemins qui seront choisis comme élites à la fin de chaque tour de boucle. 
- Le 2ème champ de saisie donne le nombre de chemins qui seront générés par le Xover. Plus ce nombre est grand, plus l'algorithme trouvera rapidement le chemin optimal.  
- Les paramètres étant numériques, l'utilisateur ne peux rentrer que des nombres dans les champs de saisie.
- Le bouton Run permet de lancer le traitement.
	- Le bouton est grisé tant que 3 villes au moins n'ont pas été renseignées (un trajet entre 1 ou 2 villes n'a pas de sens ni d'intérêt) et que les paramètres sont à blanc.
	- Lorsque l'utilisateur clique dessus, l'algorithme se lance.
	- Lorsque l'algorithme est termniné, le système renvoie automatiquement l'utilisateur dans l'onglet "Run"

# Onglet Run

L'onglet Run contient 2 ListView et 2 boutons :

- La 1ère ListView affiche des statistiques sur l'exécution : 
	- Temps d'exécution,
	- Récap des paramètres d'entrée, 
	- Nombre de tours de boucle éffectués par l'algorithme,
	- Nombre de chemins traités.
- La 2ème ListView affiche le chemin optimal calculé par l'algorithme 
- Le bouton "Map" permet de retourner sur le 1er onglet où l'on peux voir apparaître le chemin optimal. En fin de traitement, le système relie les villes afin d'observer visuellement le meilleur chemin.
- Le bouton "Reset" permet de rammener l'application à l'état initial.

# L'algorithme

L'algorithme reprend le concept qui nous a été présenté lors du cours avec 4 grandes étapes : tirage aléatoire, Xover, mutation, élite.

- Comme dit précédemment, l'algorithme prend en compte la ville de départ et d'arrivée. Ces deux villes ne bougeront jamais de position au sein des chemins traités dans le Xover ou la mutation.
- Dû à ce choix de traitement, si l'utilisateur ne renseigne que 3 villes, l'algorithme ne s'éxecute pas. Le système renvoie simplement le chemin : ville de départ --> ville étape --> ville d'arrivée, étant donné que c'est la seule possibilité.
- L'algorithme boucle sur lui-même avec une condition d'arrêt fonction du nombre de villes à traiter et des paramètres d'entrée : 
	- A la fin de chaque sélection des élites, le système calcule la moyenne des scores de chaque chemin. 
	- Si la moyenne est égale à celle du tour de boucle précédent (avec une marge à + ou - 0.01), un compteur est incrémenté de 1. Sinon le compteur est remis à 0.
	- Lorsque le compteur est égal au nombre de villes sélectionnées multiplié par le nombre de chemins tirés en entrée d'algo multiplié par le nombre de chemins générés par le Xover (nombre de villes X param 1 X param 2), alors le système stoppe l'exécution. Le chemin retenu sera le meilleur parmi les meilleurs.

# Faiblesse de l'application 

- L'application est totalement responsive. Cependant, lorsque l'utilisateur redimentionne l'écran manuellement (en positionnant sa souris sur le bord de la fenêtre), les objets dessinés sur la carte (points et chemin optimal) perdent leurs positions initiales. 
	- Ce problème est résolu lors d'un double clic sur l'entête de la fenêtre. Le système recalcule la position en pixel des objets en fonction du pourcentage de variation entre la dimention initale de la fenêtre et sa dimention en plein écran.

