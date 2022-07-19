Okazuje się że obiekt `_mailService` często ma problemy z wysłaniem maila.
Występuje jakiś bliżej niezidentyfikowany problem z połączeniem. 
Z tego względu zapadła decyzja aby wyciąć ten kawałek kodu i zamiast tego użyć interfejsu IMessageSession do publikacji zdarzenia.
Inna część systemu przechwyci to zdarzenie i wyśle maila. 
Zmiana powinna być bezpieczna i łatwa do sprawdzenia. 
Obserwowalne zachowanie zostanie takie samo. 

* Zmień tylko implementację klasy `VenueService`
* Nie ruszaj przy tym testów



