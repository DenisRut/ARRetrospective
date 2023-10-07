# ARRetrospective
University Project - AR Gallery for Video Game Enthusiasts

Pfad zum Repository:
https://github.com/DenisRut/ARRetrospective.git

Im folgenden Ordner liegen Video und auszudruckende Imagemarker vor:
PROJEKTDATEIEN

Idee:
Das Projekt soll dem Nutzer ermöglichen in Videospielszenen seines Lieblingstitels abzutauchen. Dabei bietet der Produzent eine Reihe an vorgefertigten Bildern an, die sich der Nutzer auswählen kann und auf Anfrage erweitert werden kann. Die Bilder aus den Videospielszenen rahmt sich der Nutzer ein und klebt den dazugehörigen Imagemarker auf die Ecke unten rechts am Bilderrahmen. Sobald der Nutzer einen Imagemarker gescannt hat wird ein Video passend zur abgebildeten Videospielszene abgespielt, sowie eine Information zum entsprechenden Videospiel.
Der Bilderrahmen in dem das Szenenbild eingeklebt bzw. eingefügt wird muss eine Größe von 25 x 25cm besitzen (derzeitige Verwendung des Ikea-Bilderrahmens "SANNAHED"), damit der dafür entsprechende Imagemarker beim Tracken dessen den virtuellen Bilderrahmen und Video mit der korrekten Größe auf den Rahmen in der reelen Welt projezieren kann.
Dies sind alle aufwendige Einzelanfertigungen (von Anfertigung des Videomaterials bishin zur Anfertigung der Informationslabels).
Ziel ist es ein immersives Erlebnis für den Nutzer zu ermöglichen, welches Bilder in der realen Welt augmentiert und aufwertet indem die AR Komponente diese durch Videoclips zum Leben erweckt. Derzeitige Zielgruppe sind Videospielenthusiasten, die sich eine Videospiel-Bildergallerie zulegen oder erweitern wollen und diese zu Hause interaktiv gestalten möchten.

Die Grundidee existiert bereits und wurde nach eigener Vorstellung nachentwickelt.
Quellen: 

"The Shadow Box Gallery – Youtube" - https://www.youtube.com/watch?v=urvSEezpap4

"Video Game Shadow Box – Homepage" - https://www.videogameshadowbox.com/


Umsetzung & Anwendung:

Folgende Software (-Komponenten) wurden bei der Umsetzung des Projekts angewandt:

Unity Personal (Version  2021.3.13f1)

Vuforia SDK (Version 10.12.3)

Aseprite (Grafikprogramm für Pixelart)

DaVinci Resolve (Videoschnittprogramm)

Blender 3D (3D Modellierungssoftware)


Die AR Anwendung wurde mit Hilfe von "Unity" und dem "Vuforia SDK" umgesetzt. Aufgrund von Berücksichtigungen der Grundidee, sowie technischen Schwierigkeiten, als auch der Automatisierung / Vereinfachung durch genannter Engine und SDK bei der Realisierung ist die Umsetzung letzten Endes sehr "codearm" geworden. Das kam unter anderem dadurch zustande, dass das Tracking durch Vuforia gesteuert wird und lediglich Einstellungen über den Inspector der einzelnen Vuforia-Komponenten vorgenommen wurden, sodass eigene Skripte nicht zwingend erforderlich waren.
Besondere Schwierigkeiten traten auf beim Abrufen des Tracking Status. Dieser wird über eine mitgelieferte Klasse "DefaultObserverEventHandler" des Vuforia SDKs gesteuert, die allerdings keinerlei Möglichkeit anbietet den Trackingstatus für weitere Anwendungsbereiche ausserhalb der Vuforia Komponenten zu nutzen, sodass ein sogenannter "Hack" in die Klasse geschrieben wurde, der das Auslesen des Zustands "TRACKED" nun zulässt. Das ist essentiell damit das Abspielen des Videos mit der Einblendung der Elemente unter dem Imagetracker Objekts (Bilderrahmen, Videopanel, Informationspanel) zeitgleich passieren kann.
Aufgrund dessen, dass das Verändern der "DefaultObserverEventHandler"-Klasse nicht vorgesehen scheint und diese Veränderung an einem älteren Projekt sich nicht auf ein neues Projekt reproduzieren ließ wird die alte Projektstruktur als Grundlage zur Verwendung des essentiell wichtigen Hacks wiederverwendet. Auf dieser Tatsache basiert die Verwendung älterer Unity und Vuforia SDK Versionen.

Das Videomaterial wurden über ein sogenanntes "Capture Device" (in dem Fall die "Mira Box") von der Videospielkonsole auf den Computer übertragen und aufgenommen. Das Material wurde danach mit "DaVinci Resolve" geschnitten und in das für uns notwendige Format gebracht.
Der virtuelle Bilderrahmen entstand in "Blender 3D" und umfasst das Videopanel.
Die Informationspanel, sowie die Imagemarker wurden mit "Aseprite" erstellt, einem einem Grafikprogramm, dass sich auf Pixelkunst spezialisiert.


Aufgetretene Probleme:

Um die Immersion zu steigern war das Ein- und Ausblenden des virtuellen Bilderrahmens, des Informationspanels und des Videos angedacht. Sobald der Tracking Status auf "TRACKED" übergeht sollten die genannten Komponenten über eine "Fade-In"-Funktion langsam eingeblendet und über eine "Fade-Out"-Funktion zum Ende des Videos ausgeblendet werden. Dafür benötigen alle Komponenten über eine Textur / ein Material, dass über einen Alphawert verfügt um die Transparenz zu steuern. Dadurch, dass das Video das Material des Panel-Objekts überschreibt und Videodateien über keinen Alphawert verfügen funktionierte diese Umsetzung leider nicht. Ein möglicher Ansatz wäre die Verwendung und Transformation eines anderen Shaders der auf dem Material angewendet wird. Allerdings verwenden Videodateien keinen Shader, wodurch ein anderer Lösungsansatz gefunden werden müsste der sich noch nicht erschloss.


Alternativlösungen:

Weitere Ansätze um die Immersion zu steigern wären das Einfügen von weiteren 3D Modellen die sich animieren ließen (beispielsweise ein optisches "Highlight" neben dem Informationspanel). Dies wurde vorerst berücksichtigt in Form eines 3D Modells passend zum entsprechenden Videospiel, welches sich um seinen Pivot Punkt rotierte. Das Ergebnis war allerdings nicht zufriedenstellend, da es in dem Kontext des Konzepts keine sinnvolle Anwendung fand bzw. vom eigentlichen Highlight, dem virtuellen Bilderrahmen mit dem Videoclip unnötig ablenkte.
Eine weitere Idee war es eine Gestiksteuerung hinzuzufügen. Diese sah vor die Videos anhand von bestimmten Gestiken stoppen oder vor- /zurpckspulen zu können. Allerdings sind die Videoclips von einer geringen Abspielzeit, sowie die Verwendung von Gestik bei der gleichzeitigen Handhabung eines Smartphones umständlich, sodass einfachheitshalber das Video gestartet und gestoppt wird je nachdem ob der entsprechende Imagemarker gerade erkannt wird oder nicht.
Eine mögliche weitere Umsetzung einer Steuerung könnte eine Spracherkennung sein, mit der beispielsweise das Video gesteuert werden könnte und weitere Informationen zum Clip anhand eines aufklappenden Informationspanels angezeigt werden könnten.
Ausserdem wird bei genauer Betrachtung des im oben genannten Youtube-Videos deutlich, dass manche virtuelle Bilderrahmen über ein weiteres Panel verfügen, welches sich im virtuellen Bilderrahmen befindet, allerdings vor dem Video platziert wurde um für Tiefe zu sorgen, die besonders bei seitlicher Betrachtung auffällt (siehe das "The Legend of Zelda – A Link to the Past" und "Super Metroid" Beispiel im Video). Dabei handelt es sich um grafische Elemente wie Baumkronen oder anderer Vordergrund, der entsprechend zu den besagten Szenen aus den Clips passt.


Zusätzlicher Inhalt – "Research Mode":

Um das Verhalten zwischen verschiedenen Imagetrackern zu untersuchen wurden zu Beginn des Projekts eigene Imagemarker angelegt. Diese Imagemarker befinden sich im dem deaktivierten Gameobject namens "QRManager". Dabei kann folgendes Verhalten beobachtet werden:

Welche QRMarker werden derzeit aktiv erkannt? (Text oben rechts in UI)

Wie viele werden derzeit aktiv erkannt? (Text "Target Count" unten links in UI)

Welche QRMarker haben derzeit den weitesten Abstand zueinander? (Text oben links in UI)

Wie weit ist der Abstand zwischen den weitentferntesten QRMarkern die derzeit erkannt werden? (Text "Length" unten links in UI)
Die Informationen dazu werden in einem User Interface (UI) wiedergegeben.
Für weiteres, visuelles Feedback werden auf den gescannten QRMarkern halbtransparente, farbliche Quadrate projeziert.
Für die korrekte Verwendung dieser sollen die dafür vorgesehenen Imagemarker "QRMarker 1 - 4" ausgedruckt verwendet werden um diese beliebig in die Kamera zeigen und umpositionieren zu können. Beim Anwenden des "Research Mode" sollten das Gameobject "ShadowBoxManager" deaktiviert und das Gameobject "QRManager" in der Szene aktiviert werden. Dafür auf die genannten Gameobjects in der Hierarchy klicken und im Inspector neben dem Namen des Objektes den Haken in der Controlbox setzen / entfernen.


Zukunftsperspektive:

Das Projekt wird vorerst für den privaten Gebrauch weiter geführt und in einer erweiterten Form ausgearbeitet. Dabei werden die simplen Bilder in den Bilderrahmen ersetzt gegen die in dem Beispielvideo von Youtube gezeigten "Shadowboxen".
Ausserdem wird ein Corporate Design entworfen für die mögliche, potentiell zukünftige Vermarktung die wie folgt aussehen könnte:
Ein Kunde bestellt sich von einem Anbieter wie Etsy eins der von mir angebotenen Shadowboxen. Diese werden dann von mir in Kleinstarbeit angefertigt und ein Imagemarker in Form eines Stickers auf dem Bilderrahmen angebracht. Die App wird kostenlos zur Verfügung gestellt. Zu überlegen wäre, ob man für jeden Kunden einen Account anlegt und ihn seine Anmeldedaten via Email zusendet, sodass die Verwendung der App lediglich mit dem Kauf einer Shadowbox einher geht. Andererseits werden erst beim Kauf die entsprechenden Imagemarker zum gekauften Bild mitgeliefert, was vorerst vor unbefugter Nutzung ausreichend schützen sollte.
