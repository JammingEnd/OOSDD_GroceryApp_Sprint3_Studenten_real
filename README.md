# GroceryApp sprint3

## Gitflow
In dit project werken we met Gitflow.
Hieronder leg ik de verschillende branches uit en hoe we hiermee werken.

### Main
De Main branch is de stabiele versie van de app. Alleen releases en hotfixes komen hier terrecht.

### Develop
De Develop branch is de integratie branch waar alle features in worden samengevoegd.

### Feature
Voor elke user story (UC) wordt een aparte feature branch gemaakt vanuit Develop.
Zoals: 
- feature/UC08
- feature/UC09

### Release
Wanneer alle features voor een release klaar zijn, wordt er een release branch gemaakt vanuit Develop.
Zoals:
- release/v1.1.0

### Hotfix
Wanneer er een bug in de Main branch wordt gevonden, wordt er een hotfix branch gemaakt vanuit Main.
Zoals:
- hotfix/v1.0.1
# GroceryApp sprint3 Studentversie  
    
## UC07 Delen boodschappenlijst  
Is compleet  
  
## UC08 Zoeken producten  
Aanvullen:
- In de GroceryListItemsView zitten twee Collection Views, namelijk één voor de inhoud van de boodschappenlijst en één voor producten die je toe kunt voegen aan de boodschappenlijst  
- Voeg boven de tweede CollectionView een zoekveld (SearchBar) in om op producten te kunnen zoeken.  
- Zorg dat de SearchCommand wordt gebonden aan een functie in het onderliggende ViewModel (GroceryListItemsViewModel) en dat de zoekterm die in het zoekveld is ingetypt gebruikt wordt als parameter (SearchCommandParameter).  
- Werk in het viewModel (GroceryListItemsViewModel) de zoekfunctie uit en zorg dat de beschikbare producten worden gefilterd op de zoekterm!  

## UC9 Login Captcha 
Wanneer de gebruiker op Inloggen drukt zou er een popup moeten komen die de user een kleine opdracht geeft. Wanneer deze opdracht is gehaald wordt de user naar de boodschappenlijst gestuurd. 


  

