# Lernperiode 5

![image](https://github.com/user-attachments/assets/2546ac70-df9d-4016-bd22-cad4813ad053)

2.5 bis 23.5

## Grob-Planung

Wie können Sie Ihr Projekt aus der Lernperiode 4 um eine Persistenz-Lösung erweitern? Was gibt es zu speichern, was für Tabellen brauchen Sie und wie muss der bestehende Code mit diesen interagieren?

Ich möchte die Spielergebnisse meines Memoryspiels in einer SQL-Datenbank speichern. Jedes Mal, wenn Spieler 1 oder Spieler 2 gewinnt, soll dieser Sieg automatisch in der Datenbank erfasst werden. Ziel ist es, später nachvollziehen zu können, welcher Spieler bisher öfter gewonnen hat.

Dazu plane ich zwei Tabellen:
- Players:Diese Tabelle speichert Informationen über die Spieler (z. B. ID und Name).
- 'memory_game_results': Diese Tabelle erfasst die einzelnen Spielergebnisse, also wer wann gewonnen hat.


## 2.5

- [x] Lernperiode-4 verbessern(gif hinzufügen, fertiges Projekt beschreiben) & Grobplanung machen
- [x] SQL Datenbank erstellen mit Daten, die benutzten ich benutzen möchte Spieler 1 Wins oder Spieler 2 Wins)
- [x] SQL Datenbanktabellen erstellen.(players, 'memory_game_results' und 'gamesplayed')

Heute habe ich zuerst meine letzte Lernperiode überarbeitet, fehlende Inhalte ergänzt und einige Fehler korrigiert. Anstelle eines Bildes habe ich diesmal ein GIF erstellt.
Danach habe ich meine Datenbank aufgebaut und zwei Tabellen erstellt. Dabei habe ich für beide Tabellen IDs und Primary Keys definiert.


## 9.5 Kernfunktionalität

- [x] Siegezähler programmieren(Wie oft Spieler 1 oder 2 gewonnen haben)
- [x] Verbinde dein Spiel mit der Datenbank(Ergebnis nach jedem Spiel automatisch speichern)
      
Heute habe ich erfolgreich meine Datenbank eingerichtet, die nun einwandfrei funktioniert. Ich habe Tabellen angelegt,
und jetzt werden die Daten jedes Mal gespeichert, wenn Spieler 1 oder Spieler 2 gewinnt.


## 16.5 Kernfunktionalität und Ausbau

- [x] Spieler-Login einrichten: Beim Spielstart soll der Spieler auswählen können, ob er neu ist oder bereits gespielt hat.
- [x] Spielernamen verwalten: Bestehende Spieler können ihren Namen aus einer Liste auswählen. Neue Spieler können einen Namen erstellen, der gespeichert wird.
- [x] Datenbank-Anbindung: Die Spielernamen sollen automatisch mit der Datenbank verknüpft und in den entsprechenden Tabellen gespeichert werden.

Heute habe ich versucht, die Datenbankbindung umzusetzen. Leider gab es viele Fehler im Code, was dazu führte, dass ich alles von vorne Anfangen musste.
Aktuell bin ich dabei, den Fehler zu finden und zu korrigieren. Mein Codes enthält noch ein paar Fehler, die ich suchen und beheben möchte.


## 23.5 Abschluss

- [x] Die Fehler, die ich gemacht habe beheben.
- [x] Reflexion über Ihre Arbeitsweise
- [x] Beschreibung des fertigen Projekts mit .gif etc.

Heute habe ich versucht, die bisherigen Fehler in meinem Code zu korrigieren. Zwar funktioniert nun die Eingabe der Spielernamen, was ein Fortschritt ist, jedoch besteht weiterhin das Problem, dass immer Player 1 ist an der Reihe/ "name"ist an der Reihe angezeigt wird, was nicht zum Spielverlauf passt.


## Fertiges Projekt

In meinem fertigen Projekt habe ich den bestehenden Code des Memoryspiels erfolgreich mit einer Datenbank verbunden. Nun wird jedes Spiel zwischen zwei Spielern automatisch in der Datenbank erfasst, wann gespielt wurde, sowie wer der Gewinner des Spiels ist. Bei der Auswahl des Zwei-Spieler-Modus kann man auch die Namen der Spieler eingeben.
Einige kleinere Fehler bestehen noch: Zum Beispiel verschwinden die eingegebenen Namen kurzzeitig, wenn ein Punkt gemacht wird. Ausserdem wird immer noch angezeigt, dass „Player 1“ oder der Name von Spieler 1 am Zug ist unabhängig vom Spielverlauf. Auch diesen Fehler werde ich später noch beheben.

## Reflexion

Die Arbeit mit der Datenbank war für mich der einfachste Teil dieses Projekts. Die Erstellung der Datenbank selbst war nicht besonders schwierig. Der eigentliche schwierige Teil bestanddarin, die Datenbank mit meinem Code zu verbinden, dafür musste ich nähmlich neue Buttons einfügen und Teile des Codes anpassen.
Nachdem ich diese Verbindung erfolgreich hergestellt hatte, wollte ich noch eine neue Funktion coden. Das führte leider dazu, dass mein Code abstürzte und von vorne beginnen musste.
Trotz einiger verbleibender Fehler bin ich insgesamt zufrieden mit dem Ergebnis, weil es mein erstes Projekt war, in dem ich eine Datenbank in ein Programm verbunden habe.
Für nächstes Jahr nehme ich mir vor, einen noch besseren Code zu schreiben und eine noch grössere Datenbank mit mehreren Tabellen zu erstellen.
