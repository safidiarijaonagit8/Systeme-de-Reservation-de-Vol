# Systeme de Réservation de Vol
 Projet fait avec ASP .NET CORE avec Entity Framework CORE , base de données SQL SERVER.

 Description:
 Ce projet permet de digitaliser le processus de réservation des vols d'avion.
 On a deux types d' utilisateurs : Administrateur et client.																										
 Pour le côté Administrateur, on peut configurer un avion c' est à dire ajouter un avion, le modifier, le supprimer , voir la liste de touts les avions, rechercher un avion spécifique. Un avion est caracterisé par son modèle, son nombre de siège en classe affaire et économique.
 L' Administrateur peut aussi configurer un vol. Un vol est caracterisé par la date et l'heure du décollage, la durée du vol, les lieux de départ et d' arrivée, l' avion qui va desservir le vol.
 L' Administrateur peut avoir toute la liste des réservations effectuées, qu' il peut trier selon ses besoins.

Pour le côté Client, on peut rechercher un vol en vue d' une réservation, en entrant la date et heure du vol, les lieux de départ et d' arrivée. Si le vol n' est pas disponible pour la date et heure de départ souhaitée, le système propose d' autres vols avec la date la plus proche.
Le client pourra alors réserver le vol qui lui convient, en entrant le nombre de places en classe affaire ou économique qu' il veut réserver. Le système vérifie si le nombre de places souhaitées est disponible pour ce vol et affiche un détail du vol.
Si le nombre de places disponibles est suffisant, la réservation est enregistrée, sinon le système propose d' autres vols par rapport à la demande.

