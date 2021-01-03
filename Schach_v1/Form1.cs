using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Schach_v1
{


    public partial class Form1 : Form
    {
        //Colors des SChachbretts
        Color[] _tileColors = new Color[] { Color.FromArgb(163, 163, 163), Color.FromArgb(102, 59, 7) };

        // MACO: Wofür muss dieses Feld public sein?
        //farbe die das Feld annimmt wenn es besprungen werden kann
        public Color PossibleMoveColor = Color.ForestGreen;

        // Definiert die standart spielzeit
        const int startingTime = 15;

        //der Spielende Spieler
        Color CurrentPlayer = Color.White;

        //größe des Boards
        const int _BOARDSIZE = 800;

        //größe der einzelnen Teile
        Size _tileSize;

        //letzte Figur die angeklickt worden ist
        Figure _lastFigureClicked = null;

        List<Tile> Tiles = new List<Tile> { };

        //Liste der geschlagenen Figuren
        List<Figure> FiguresInfoBarWhite = new List<Figure>();
        List<Figure> FiguresInfoBarBlack = new List<Figure>();

        // Label zur Ausgabe des CurrentPlayers
        Label lbl_currentPlayer;

        //unteren drei buttons aufgeben /remis /neustart
        Button btn_aufgeben;
        Button btn_remis;
        Button btn_neuStarten;

        // Textbox für Eingabe der Zeit
        TextBox txt_durationStopwatch;

        // Panel zum Gruppieren der Elemente im InfoBar
        Panel pnl_InfoBar;

        // Labels um Zeit der Spieler ausgeben
        Label lbl_timeLeft_white, lbl_timeLeft_black;

        // Timer um jede Sekunde die übrige Zeit herunter zu zählen
        Timer tmr_sekunde;

        // Button um Zeit einzugeben
        Button btn_pushDuration;

        // Button zum Starten des Spieles/Stopuhr
        Button btn_startGame;


        // Var zum speichern der eingegebenen Zeit (Spielzeit)
        int duration = 0;

        // Übrige Zeit Label
        int _timeLeftWhite, _timeLeftBlack;

        //bool ob das Spiel gestarted ist
        bool _started = false;

        public Form1()
        {
            Text = "Schach - Extended Version";
            InitializeComponent();
            InitWindow();
            InitInfoBar();
        }

        /// <summary>
        /// Einstellung / Erstellung aller Tiles und Figuren
        /// </summary>
        void InitWindow()
        {
            //Id Counter für die Tiles
            int id = 0;

            //festlegung der Größe des Spielfeldes
            // MACO: Warum wird hier die ClientSize gesetzt, wenn sie nachher eh
            // wieder überschrieben wird?
            // Antwort: Weil wir zuerst die des Boards setzen, anhand von dem die Tiles erstellen, und die
            // Form dann um 3 Tilesizes erweitern (für die Infobar)

            ClientSize = new Size(_BOARDSIZE, _BOARDSIZE);
            _tileSize = new Size(ClientSize.Width / 8, ClientSize.Height / 8);
            ClientSize = new Size(_BOARDSIZE + _tileSize.Width * 3, _BOARDSIZE);

            //erstellung aller Tiles mit den ID's 0-63
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    //"Berechnung" der Hintergrundfarbe
                    Color color;
                    if (id % 2 == 0 && i % 2 == 0)
                    {
                        color = _tileColors[1];
                    }
                    else if (id % 2 == 1 && i % 2 == 1) {
                        color = _tileColors[1];
                    }
                    else
                    {
                        color = _tileColors[0];
                    }

                    //Erstellung des Tiles und Position anhand der ID
                    Tile ChessTile = new Tile(_tileSize, color, new Point(j, i));
                    ChessTile.TileClicked += ChessTile_TileClicked;
                    ChessTile.Left = ChessTile.Width * j;
                    ChessTile.Top = ChessTile.Height * i;

                    //manuelle generation der Queen / King white
                    if (i == 0)
                    {
                        if (j == 3)
                        {
                            Controls.Add(new Queen( ChessTile, Tiles));
                        }
                        if (j == 4)
                        {
                            Controls.Add(new King( ChessTile, Tiles));
                        }
                    }

                    //manuelle generation der Queen / King Black
                    if (i == 7)
                    {
                        if (j == 3)
                        {
                            Controls.Add(new Queen( ChessTile, Tiles));
                        }
                        if (j == 4)
                        {
                            Controls.Add(new King( ChessTile, Tiles));
                        }
                    }

                    //generation other figures
                    if (i == 0 || i == 7)
                    {
                        if (j == 0 || j == 7)
                        {
                            Controls.Add(new Tower( ChessTile, Tiles));
                        }

                        if (j == 1 || j == 6)
                        {
                            Controls.Add(new Horse( ChessTile, Tiles));

                        }

                        if (j == 2 || j == 5)
                        {
                            Controls.Add(new Bishop( ChessTile, Tiles));
                        }
                    }

                    //generation pawns
                    if (i == 1 || i == 6)
                    {
                        Controls.Add(new Pawn( ChessTile, Tiles));
                    }

                    //hinzufpgen zur liste der Tiles
                    Tiles.Add(ChessTile);

                    //hinzufügen der Figur und des Tile's
                    Controls.Add(ChessTile);

                    //id counter
                    id++;
                }

            }

            //Hinzufügen des moves event für jede Figure auf dem Spielfeld
            foreach (Control item in Controls)
            {
                if (item is Figure figure)
                {
                    figure.FigureClicked += Figure_FigureClicked;
                }
            }
        }

        // Methode zur Erstellung vom Informationsbalken rechts
        public void InitInfoBar()
        {
            // InfoBar recht erstellen und definieren
            pnl_InfoBar = new Panel()
            {
                Top = 0,
                Left = 800, // MACO: Warum wird hier nicht die Spielbrettgröße verwendet? (6)
                Width = _tileSize.Width * 3,
                Height = _tileSize.Height * 8 // MACO: (8)
            };
            Controls.Add(pnl_InfoBar);

            // Label zur Ausgabe des Spielers (am Zug), erstellen und definieren
            lbl_currentPlayer = new Label()
            {
                Width = _tileSize.Width * 3,
                Height = _tileSize.Height * 2,
                Font = new Font("Arial", 26)
            };
            pnl_InfoBar.Controls.Add(lbl_currentPlayer); // Label ins Panel hinzufügen
            // Spieler anfangs auf schwarz setzen damit beim Ersten Change Weiß ausgegeben wird (weiß beginnt)
            CurrentPlayer = Color.Black;
            ChangePlayer();

            txt_durationStopwatch = new TextBox()
            {
                Top = lbl_currentPlayer.Height + lbl_currentPlayer.Top + 10,
                Left = 65
            };
            pnl_InfoBar.Controls.Add(txt_durationStopwatch);

            txt_durationStopwatch.Text = Convert.ToString(startingTime); // Standard gemäß haben beide Spieler 15 Minuten Zeit
           
            // Timer generieren der jede Sekunde tickt 
            tmr_sekunde = new Timer();
            tmr_sekunde.Interval = 1000;
            tmr_sekunde.Tick += Tmr_Sekunde_Tick;

            // Labels zum ausgeben der übrigen Zeit
            lbl_timeLeft_white = new Label()
            {
                Top = txt_durationStopwatch.Top + txt_durationStopwatch.Height * 2,
                Font = new Font("Arial", 12),
                Height = 50,
                Width = 90,
                Left = _tileSize.Width - 30,
            };
            pnl_InfoBar.Controls.Add(lbl_timeLeft_white);

            lbl_timeLeft_black = new Label()
            {
                Top = txt_durationStopwatch.Top + txt_durationStopwatch.Height * 2,
                Font = new Font("Arial", 12),
                Height = 50,
                Width = 90,
                Left = lbl_timeLeft_white.Width + lbl_timeLeft_white.Left 
            };
            pnl_InfoBar.Controls.Add(lbl_timeLeft_black);

            #region Buttons
            // Button zum abschicken
            btn_pushDuration = new Button()
            {
                Left = txt_durationStopwatch.Left + txt_durationStopwatch.Width,
                Top = lbl_currentPlayer.Height + lbl_currentPlayer.Top + 10,
                Text = "set"
            };
            // Eventhandler hinzufügen für Click Event
            btn_pushDuration.Click += Btn_pushDuration_Click;
            pnl_InfoBar.Controls.Add(btn_pushDuration);

            // Starten wenn Button geklickt wurde
            btn_startGame = new Button()
            {
                Width = pnl_InfoBar.Width,
                Height = _tileSize.Height  / 2,
                Top = pnl_InfoBar.Height - _tileSize.Height,
                Left = 0,
                Text = "Starten",
                Enabled= false
            };
            pnl_InfoBar.Controls.Add(btn_startGame);
            // Event Handler hinzufügen
            btn_startGame.Click += Btn_startGame_Click;

            btn_remis = new Button()
            {
                Width = pnl_InfoBar.Width / 3,
                Height = _tileSize.Height / 2,
                Top = pnl_InfoBar.Height - btn_startGame.Height,
                Left = 0,
                Text = "Remis",
                Enabled = false
            };
            pnl_InfoBar.Controls.Add(btn_remis);
            // Event Handler hinzufügen
            btn_remis.Click += Btn_Remis_Click;

            btn_aufgeben = new Button()
            {
                Width = pnl_InfoBar.Width / 3,
                Height = _tileSize.Height / 2,
                Top = pnl_InfoBar.Height - btn_startGame.Height,
                Left = btn_remis.Width,
                Text = "Aufgeben",
                Enabled = false
    };
            pnl_InfoBar.Controls.Add(btn_aufgeben);
            // Event Handler hinzufügen
            btn_aufgeben.Click += Btn_Aufgeben_Click;

            btn_neuStarten = new Button()
            {
                Width = pnl_InfoBar.Width / 3,
                Height = _tileSize.Height / 2,
                Top = pnl_InfoBar.Height - btn_startGame.Height,
                Left = btn_remis.Width + btn_aufgeben.Width,
                Text = "Neu Starten",
                Enabled = false
        };
            pnl_InfoBar.Controls.Add(btn_neuStarten);
            // Event Handler hinzufügen
            btn_neuStarten.Click += Btn_NeuStarten_Click;

            btn_startGame.Enabled = false;
            btn_aufgeben.Enabled = false;
            btn_remis.Enabled = false;
            btn_neuStarten.Enabled = false;
            #endregion
        }

        public void ResetGame()
        {
            if (CurrentPlayer == Color.Black)
            {
                ChangePlayer();
            }

            // Liste zum Removen der Items
            List<Control> itemsToRemoveControls = new List<Control>();
            List<Control> itemsToRemovePanelControls = new List<Control>();

            #region Infobar 
            foreach (Figure f in FiguresInfoBarWhite)
            {
                itemsToRemovePanelControls.Add(f);
            }
            foreach (Figure f in FiguresInfoBarBlack)
            {
                itemsToRemovePanelControls.Add(f);
            }
            // MACO: Warum kann das nicht gleich in den oberen Schleifen gelöscht
            // werden?
            // Antwort: Weil man nicht direkt aus der Liste löschen kann, wenn man sie durchgeht

            foreach (Control item in itemsToRemovePanelControls)
            {
                pnl_InfoBar.Controls.Remove(item);
            }
            #endregion

            #region Clear
            lbl_timeLeft_black.Text = "";
            lbl_timeLeft_white.Text = "";

            txt_durationStopwatch.Text = Convert.ToString(startingTime);

            btn_startGame.Enabled = true;
            btn_pushDuration.Enabled = true;
            txt_durationStopwatch.Enabled = true;
            FiguresInfoBarWhite.Clear();
            FiguresInfoBarBlack.Clear();

            btn_remis.Enabled = false;
            btn_aufgeben.Enabled = false;
            btn_neuStarten.Enabled = false;
            btn_startGame.Enabled = false;

            #endregion

            #region Board
            foreach (Control item in Controls)
            {
                if (item is Figure f)
                {
                    itemsToRemoveControls.Add(f);
                }
                // MACO: Warum werden die Tiles gelöscht? Die könnten doch auch wieder-
                // verwendet werden, man müsste nur bei jedem Tile die draufstehende 
                // Figur rauslöschen, oder?
                else if (item is Tile t)
                {
                    itemsToRemoveControls.Add(t);
                }
                
            }
            foreach (Control item in itemsToRemoveControls)
            {
                Controls.Remove(item);
            }
            InitWindow();
            #endregion
        }

        private void Btn_NeuStarten_Click(object sender, EventArgs e)
        {
            SpielPausieren();

            DialogResult answer = MessageBox.Show("Möchten Sie das Spiel wirklich neustarten?", "NEUSTART?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (answer == DialogResult.Yes)
            {
                // Spiel neustarten
                ResetGame();
            }
        }

        private void Btn_Aufgeben_Click(object sender, EventArgs e)
        {
            SpielPausieren();

            DialogResult answer = MessageBox.Show("Möchten Sie wirklich aufgeben?", "AUFGEBEN?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (answer == DialogResult.Yes)
            {
                // MACO: Wenn ich als schwarzer Spieler aufgebe, steht in der Message-
                // Box, dass schwarz gewonnen hat. Das ergibt keinen Sinn.
                DialogResult OK = MessageBox.Show("Spieler " + CurrentPlayer + " hat gewonnen!", "Gewonnen durch Aufgabe des Gegners", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (OK == DialogResult.OK)
                {
                    Close();
                }
            }
        }

        private void SpielPausieren()
        {
            // SPiel pausieren
            tmr_sekunde.Stop();

            _started = false;
        }

        private void Btn_Remis_Click(object sender, EventArgs e)
        {
            SpielPausieren();

            DialogResult answer = MessageBox.Show("Möchten Sie das Remis bestätigen?", "REMIS", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (answer == DialogResult.Yes)
            {
                DialogResult ok = MessageBox.Show("Das Spiel wurde mit einem Unentschieden beendet", "UNENTSCHIEDEN", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (ok == DialogResult.OK)
                {
                    Close();
                }
            }
            else
            {
                tmr_sekunde.Start();
            }
        }

        private void Btn_startGame_Click(object sender, EventArgs e)
        { 
            // Elemente unbenutzbar machen wenn Timer nicht läuft
            if (!tmr_sekunde.Enabled)
            {
                btn_remis.Enabled = true;
                btn_aufgeben.Enabled = true;
                btn_neuStarten.Enabled = true;
                _started = true;
                tmr_sekunde.Start();
                txt_durationStopwatch.Enabled = false;
                btn_pushDuration.Enabled = false;
            }
            btn_startGame.Enabled = false;
        }

        private void Tmr_Sekunde_Tick(object sender, EventArgs e)
        {
            if (CurrentPlayer == Color.White)
            {
                _timeLeftWhite--;
                UpdateTimerText(lbl_timeLeft_white, _timeLeftWhite);
            }
            else
            {
                _timeLeftBlack--;
                UpdateTimerText(lbl_timeLeft_black, _timeLeftBlack);
            }

            // MACO: Hier steht zwei mal hintereinander der vom Prinzip her komplett
            // gleiche Code. -> in Methode auslagern!
            if (_timeLeftBlack == 0)
            {
                SpielPausieren();

                DialogResult answer = MessageBox.Show("Schwarz hat verloren!", "Vorbei!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (answer == DialogResult.Yes)
                {
                        Close();
                }
            }
            else if (_timeLeftWhite == 0)
            {
                SpielPausieren();

                DialogResult answer = MessageBox.Show("Weiss hat verloren!", "Vorbei!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (answer == DialogResult.Yes)
                {
                    Close();
                }
            }

            // TO DO: Stopuhr um 1 vermindern // MACO: ???
        }

        private void UpdateTimerText(Label lbl_toChange, int timeLeft)
        {
            int minLeft, secLeft;
            minLeft = Convert.ToInt32(Math.Floor(timeLeft / 60.0)); // Minuten
            secLeft = Convert.ToInt32(Math.Floor(timeLeft % 60.0)); // Sekunden
            
            lbl_toChange.Text = Convert.ToString(minLeft) + " : " + Convert.ToString(secLeft);
        }

        private void Btn_pushDuration_Click(object sender, EventArgs e)
        {
            txt_durationStopwatch.Enabled = false;
            btn_pushDuration.Enabled = false;
            btn_startGame.Enabled = true;
            btn_aufgeben.Enabled = true;
            btn_remis.Enabled = true;
            btn_neuStarten.Enabled = true;

            if (txt_durationStopwatch.Text == "")
            {
                duration = startingTime; // Standardgemäß haben beide Spieler 15 Minuten Zeit
            }
            else
            {
                duration = Convert.ToInt32(txt_durationStopwatch.Text) * 60; // Dauer in Var speichern * 60 um in Sekunden
                txt_durationStopwatch.Clear();
                _timeLeftWhite = duration;
                _timeLeftBlack = duration;
            }

            UpdateTimerText(lbl_timeLeft_white, duration);
            UpdateTimerText(lbl_timeLeft_black, duration);
        }



        
        private void ChessTile_TileClicked(Tile clickedTile)
        {

            if (_started)
            {
                //checkt ob überhaupt schonmal ne figur geclickt wurde
                if (_lastFigureClicked != null)
                {
                    //wenn ja ob sie die gleiche farbe hat wie der spieler der dran ist //möglicherweise 0 deswegen obiger check (2 if's)
                    if (CurrentPlayer == _lastFigureClicked.FigureColor)
                    {
                        //checkt ob die Hintergrundfarbe die Farbe von einem Möglichen zug hat
                        if (clickedTile.BackColor == PossibleMoveColor)
                        {
                            //wenn die figur leer ist ==> einfach die figur moven
                            if (clickedTile.CurrentFigure == null)
                            {
                                MoveFigure(clickedTile);
                            }
                            //wenn eine figur drauf ist und sie die andere Farbe hat wie der Spieler dran ist ==> schlag
                            else 
                            {
                                //HIT FIGURE
                                //entfernt die Figur von den Controls
                                Controls.Remove(clickedTile.CurrentFigure);

                                // TO DO: geschlagenen Figuren im INFOBAR anzeigen
                                AddToBeatenFigureCollection(clickedTile.CurrentFigure);

                                //die Figur des Tiles wird auf 0 gesetzt
                                clickedTile.CurrentFigure = null;
                                //der zug wird vollbracht
                                MoveFigure(clickedTile);
                            }
                            //spieler wird getausch
                            ChangePlayer();
                        }
                    }
                }
            }
        }

        private void AddToBeatenFigureCollection(Figure beatenFigure)
        {
            // Event Handler löschen
            beatenFigure.FigureClicked -= Figure_FigureClicked;
            // geschlagenen Figur in InfoBar anzeigen
            pnl_InfoBar.Controls.Add(beatenFigure);
            // Größe des Panels festlegen
            beatenFigure.Size = new Size(_tileSize.Width / 4, _tileSize.Height / 4);
            // Hintergrund ändern
            beatenFigure.BackColor = pnl_InfoBar.BackColor;
            if (beatenFigure.FigureColor == Color.White)
            {
                beatenFigure.Location = new Point(lbl_timeLeft_white.Left , lbl_timeLeft_white.Height + lbl_timeLeft_white.Top + FiguresInfoBarWhite.Count() * beatenFigure.Height);
                FiguresInfoBarWhite.Add(beatenFigure);
            }
            else
            {
                beatenFigure.Location = new Point(_tileSize.Width * 2, lbl_timeLeft_black.Height + lbl_timeLeft_black.Top + FiguresInfoBarBlack.Count() * beatenFigure.Height);
                FiguresInfoBarBlack.Add(beatenFigure);
            }
        }

        // MACO: (7)
        private void Figure_FigureClicked(Figure clickedFigure)

        {
            List<Tile> PossibleMoves = clickedFigure.GetPossibleMoves();
            //ob das game scho loft
            if (_started)
            {
                //HIT FIGURE
                //wenn die figur nicht die farbe des zurzeit angreifenden spielers hat (möglichkeit auf absicht auf das schlagen der Figur)
                if (clickedFigure.FigureColor != CurrentPlayer)
                {
                    if (clickedFigure.CurrentTile.BackColor == PossibleMoveColor)
                    {
                        Controls.Remove(clickedFigure);
                        clickedFigure.CurrentTile.CurrentFigure = null;
                        MoveFigure(clickedFigure.CurrentTile);
                        AddToBeatenFigureCollection(clickedFigure);
                        ChangePlayer();
                    }
                }
                //ansonsten ist es halt die zuletzt gedrückte Figur
                else if (clickedFigure.FigureColor == CurrentPlayer)
                {
                    _lastFigureClicked = clickedFigure;

                    //das board einfach neu painten
                    RePaintBoard();

                    foreach (Tile tile in PossibleMoves)
                    {
                        // MACO: Auch das Einfärben von sich und gegebenenfalls des
                        // Hintergrunds der Figur, die drauf steht, gehört vom Sinn
                        // her zum Tile. -> in die Tile-Klasse!
                        tile.BackColor = PossibleMoveColor;

                        if (tile.CurrentFigure != null)
                        {
                            tile.CurrentFigure.BackColor = PossibleMoveColor;
                        }
                    }
                }
            }
        }

        //färbt das Board neu
        private void RePaintBoard()
        {
           //geht jedes einzelne Tile durch
            foreach (Tile tile in Tiles)
            {


                //Setzt die Farben auf den Standard zurück
                // MACO: Sich und gegebenenfalls den Background der draufstehenden
                // Figur zurücksetzen in den farblichen Initialzustand sollte
                // das Tile selbst können. -> in die Tile-Klasse!

                if (tile.X % 2 == 0 && tile.Y % 2 == 0)

                {
                    tile.BackColor = _tileColors[1];
                }
                else if (tile.X % 2 == 1 && tile.Y % 2 == 1)
                {
                    tile.BackColor = _tileColors[1];
                }
                else
                {
                    tile.BackColor = _tileColors[0];
                }

                //checkt ob das Tile eine Currenfigurehat
                if (tile.CurrentFigure != null)
                {
                    //Färbt // setz die Hintergrundfarbe von der Daraufstehenden figur von grün wieder auf die Farbe des Tiles auf dem es steht
                    tile.CurrentFigure.BackColor = tile.BackColor;
                }
            }
        }

        // MACO: Warum hat dieser Parameter nicht den Typ Tile und einen besseren
        // Namen?
        void MoveFigure(object sender)
        {
            Tile clickedTile = sender as Tile;

            //setzt auf das geclickte Tile die alte figure
            clickedTile.CurrentFigure = _lastFigureClicked;

            //cleart das alte Tile wo die Figure war
            _lastFigureClicked.CurrentTile.CurrentFigure = null;

            clickedTile.CurrentFigure.CurrentTile = clickedTile;

            //position anhand der Position des Tiles
            clickedTile.CurrentFigure.Left = clickedTile.Left + _tileSize.Width / 2 - clickedTile.CurrentFigure.Width / 2;
            clickedTile.CurrentFigure.Top = clickedTile.Top + _tileSize.Height / 2 - clickedTile.CurrentFigure.Height / 2;

            //z-index mol wieder
            clickedTile.CurrentFigure.BringToFront();

            //neu färben
            RePaintBoard();
        }

        void ChangePlayer()
        {
            if (CurrentPlayer == Color.White)
            {
                CurrentPlayer = Color.Black;
                // Prüfen welcher Spieler an der Reihe ist und dann im Label ausgeben
                lbl_currentPlayer.ForeColor = Color.White;
                lbl_currentPlayer.BackColor = Color.Black;
                // MACO: Warum braucht es diese Zeile? Abgsehen davon kommt sie in 
                // allen Zweigen vor. -> nach dem If machen!
                lbl_currentPlayer.TextAlign = ContentAlignment.MiddleCenter;
                lbl_currentPlayer.Text = "Schwarz";
            }
            else
            {
                CurrentPlayer = Color.White;
                // Im Infobar ausgeben welcher Spieler an der Reihe ist
                lbl_currentPlayer.ForeColor = Color.Black;
                lbl_currentPlayer.BackColor = Color.White;
                lbl_currentPlayer.TextAlign = ContentAlignment.MiddleCenter;
                lbl_currentPlayer.Text = "Weiß";
            }
        }
    }
}
