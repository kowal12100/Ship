using Ships;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using Ships.Model;
using Ships.View;

namespace Ships
{

   public enum Direction
    {
        Vertical, Horizontal
    }

    public partial class Board : Form
    {
        private int score = 0;
        private List<Cell> Trafione = new List<Cell>();
        private int _statkiGraczajednomasztowe = 0;
        private int _statkiGraczadwumasztowe = 0;
        private int _statkiGraczatrzymasztowe = 0;
        private int _statkiGraczaczteromasztowe = 0;
        int _statkiJednomasztowe = 4;
        int _statkiDwumasztowe = 3;
        int _statkiTrzymasztowe = 2;
        int _statkiCzteromasztowe = 1;
        Random _random = new Random();
        private int _activeShips = 1;
        Cell[,] _plansza = new Cell[10, 10];
        Cell[,] _enemyBoard = new Cell[10, 10];
        static List<Ship> _myShips = new List<Ship>();
        static List<Ship> _oponentShips = new List<Ship>();
        Direction _direction = Direction.Vertical;
  

        private void LiczStatki(int ilemasztowiec)
        {
            switch (ilemasztowiec)
            {
                case 1:
                    _statkiGraczajednomasztowe++;
                    if (_statkiGraczajednomasztowe == _statkiJednomasztowe)
                    {
                        button1.Enabled = false;
                        _activeShips = 0;
                    }
                    break;
                case 2:
                    _statkiGraczadwumasztowe++;
                    if (_statkiGraczadwumasztowe == _statkiDwumasztowe)
                    {
                        button2.Enabled = false;
                        _activeShips = 0;
                    }
                    break;
                case 3:
                    _statkiGraczatrzymasztowe++;
                    if (_statkiGraczatrzymasztowe == _statkiTrzymasztowe)
                    {
                        button3.Enabled = false;
                        _activeShips = 0;
                    }
                    break;
                case 4:
                    _statkiGraczaczteromasztowe++;
                    if (_statkiGraczaczteromasztowe == _statkiCzteromasztowe)
                    {
                        button4.Enabled = false;
                        _activeShips = 0;
                    }
                    break;
            }
            if (_statkiGraczajednomasztowe == _statkiJednomasztowe && _statkiGraczadwumasztowe == _statkiDwumasztowe &&
                _statkiGraczatrzymasztowe == _statkiTrzymasztowe && _statkiGraczaczteromasztowe == _statkiCzteromasztowe)
            {
                button107.Enabled = true;
            }
        }
        public Board(bool inicjuj)
        {
            InitializeComponent();
            if (inicjuj)
            {
                Draw(this);
                TargetBoard(this);
                CreateOppentShips();
            }
    
        }

        public void CreateOppentShips()
        {
            var licznik = 0;
            while (licznik!=_statkiJednomasztowe)
            {
               AddOpponentShip(1);
                licznik++;
            }
            licznik = 0;
            while (licznik != _statkiDwumasztowe)
            {
                AddOpponentShip(2);
                licznik++;
            }
            licznik = 0;
            while (licznik != _statkiTrzymasztowe)
            {
                AddOpponentShip(3);
                licznik++;
            }
            licznik = 0;
            while (licznik != _statkiCzteromasztowe)
            {
                AddOpponentShip(4);
                licznik++;
            }


        }

        public void AddOpponentShip(int ileMasztow)
        {
            var wylosowano = true;
            while (wylosowano)
            {
                var cpudirection = _random.Next(2);
                var obrotStatku = cpudirection == 0 ? Direction.Horizontal : Direction.Vertical;
                var x = _random.Next(10);
                var y = _random.Next(10);
                var TabCell = new Cell[ileMasztow];
                if (!_enemyBoard[x, y].CheckIsEmpty()) continue;
                TabCell[0] = _enemyBoard[x, y];
                if (obrotStatku == Direction.Horizontal)
                {


                    if (TabCell[0].X - ileMasztow + 1 >= 0)
                    {
                        switch (ileMasztow)
                        {
                            case 2:
                                TabCell[1] = _enemyBoard[TabCell[0].X - 1, TabCell[0].Y];
                                break;
                            case 3:
                                TabCell[1] = _enemyBoard[TabCell[0].X - 1, TabCell[0].Y];
                                TabCell[2] = _enemyBoard[TabCell[0].X - 2, TabCell[0].Y];
                                break;
                            case 4:
                                TabCell[1] = _enemyBoard[TabCell[0].X - 1, TabCell[0].Y];
                                TabCell[2] = _enemyBoard[TabCell[0].X - 2, TabCell[0].Y];
                                TabCell[3] = _enemyBoard[TabCell[0].X - 3, TabCell[0].Y];
                                break;
                        }
                        if (Cell.CheckIsAroundEmpty(TabCell, _enemyBoard))
                        {
                            _oponentShips.Add(new Ship(TabCell, ileMasztow, obrotStatku, false));
                            wylosowano = false;
                        }
                    }
                }
                if (obrotStatku != Direction.Vertical) continue;
                if (TabCell[0].Y + ileMasztow > 10) continue;
                switch (ileMasztow)
                {
                    case 2:
                        TabCell[1] = _enemyBoard[TabCell[0].X, TabCell[0].Y + 1];
                        break;
                    case 3:
                        TabCell[1] = _enemyBoard[TabCell[0].X, TabCell[0].Y + 1];
                        TabCell[2] = _enemyBoard[TabCell[0].X, TabCell[0].Y + 2];
                        break;
                    case 4:
                        TabCell[1] = _enemyBoard[TabCell[0].X, TabCell[0].Y + 1];
                        TabCell[2] = _enemyBoard[TabCell[0].X, TabCell[0].Y + 2];
                        TabCell[3] = _enemyBoard[TabCell[0].X, TabCell[0].Y + 3];
                        break;
                }
                if (!Cell.CheckIsAroundEmpty(TabCell,_enemyBoard)) continue;
                _oponentShips.Add(new Ship(TabCell, ileMasztow,obrotStatku, false));
                wylosowano = false;
            }
        }






        private void AddShip(object sender, EventArgs e)
        {
            if (_activeShips == 0) return;
            var tabCell = new Cell[_activeShips];
            tabCell[0] = (Cell)sender;



            if (_direction == Direction.Horizontal)
            {


                if (tabCell[0].X - _activeShips + 1 >= 0)
                {
                    if (_activeShips == 2)
                    {
                        tabCell[1] = _plansza[tabCell[0].X - 1, tabCell[0].Y];

                    }
                    else if (_activeShips == 3)
                    {
                        tabCell[1] = _plansza[tabCell[0].X - 1, tabCell[0].Y];
                        tabCell[2] = _plansza[tabCell[0].X - 2, tabCell[0].Y];
                    }
                    else if (_activeShips == 4)
                    {
                        tabCell[1] = _plansza[tabCell[0].X - 1, tabCell[0].Y];
                        tabCell[2] = _plansza[tabCell[0].X - 2, tabCell[0].Y];
                        tabCell[3] = _plansza[tabCell[0].X - 3, tabCell[0].Y];
                    }
                    if (Cell.CheckIsAroundEmpty(tabCell, _plansza)) //!!!
                    {

                        _myShips.Add(new Ship(tabCell, _activeShips, _direction, true));
                        LiczStatki(_activeShips);

                    }
                }
            }
            if (_direction != Direction.Vertical) return;
            if (tabCell[0].Y + _activeShips > 10) return;
            switch (_activeShips)
            {
                case 2:
                    tabCell[1] = _plansza[tabCell[0].X, tabCell[0].Y + 1];
                    break;
                case 3:
                    tabCell[1] = _plansza[tabCell[0].X, tabCell[0].Y + 1];
                    tabCell[2] = _plansza[tabCell[0].X, tabCell[0].Y + 2];
                    break;
                case 4:
                    tabCell[1] = _plansza[tabCell[0].X, tabCell[0].Y + 1];
                    tabCell[2] = _plansza[tabCell[0].X, tabCell[0].Y + 2];
                    tabCell[3] = _plansza[tabCell[0].X, tabCell[0].Y + 3];
                    break;
            }
            if (!Cell.CheckIsAroundEmpty(tabCell, _plansza)) return;
            _myShips.Add(new Ship(tabCell, _activeShips, _direction, true));
            LiczStatki(_activeShips);
        }

        public bool Draw(Form board)
        {
            var errors = false;

            try
            {
                var boardX = 0;
                var boardY = 0;

                for (var x = 0; x < 10; x++)
                {
                    for (var y = 0; y < 10; y++)
                    {
                        var temp = new Cell(x,y);
                      
                            temp.Height = 40;
                           temp.Width = 40;
                            temp.Location = new Point(boardX + 200, boardY + 200);
                            temp.Click +=new EventHandler(AddShip);
                       // temp.BackgroundImage= ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
                        _plansza[x, y] = temp;
                        board.Controls.Add(_plansza[x, y]);
              
                        
                        boardY += 40;
                    }
                    boardY = 0;
                    boardX += 40;
                }

            }
            catch (Exception e)
            {
                errors = true;
            }

            return errors;
        }

        public bool TargetBoard(Form board)
        {
            var errors = false;

            try
            {
                var boardX = 0;
                var boardY = 0;

                for (var x = 0; x < 10; x++)
                {
                    for (var y = 0; y < 10; y++)
                    {
                        var temp = new Cell(x, y);

                        temp.Height = 40;
                        temp.Width = 40;
                        temp.Location = new Point(boardX + 800, boardY + 200);
                        temp.Click += new EventHandler(Fire);
                        _enemyBoard[x, y] = temp;
                        board.Controls.Add(_enemyBoard[x, y]);

                        boardY += 40;
                    }
                    boardY = 0;
                    boardX += 40;
                }

            }
            catch (Exception e)
            {
                errors = true;
            }

            return errors;
        }

        private void Fire(object sender, EventArgs e)
        {
            var cell = (Cell)sender;
            cell.Checked = true;
            if (cell.Empty)
            {
                cell.BackColor = Color.BlueViolet;
                cell.Text = "*";
                Game.UpdateScore(-10);
            }
            else
            {
                Game.UpdateScore(100);
                cell.BackColor = Color.OrangeRed;
                cell.Text = "F";
                var ship = _oponentShips.First(x => x.buttons.Contains(cell));
                ship.life -= 1;
                if (ship.life == 0)
                {
                    ship.Destroyed = true;
                    foreach (var VARIABLE in ship.buttons)
                    {
                        Cell.SetEveryDirection(VARIABLE, _enemyBoard);
                        VARIABLE.BackColor = Color.SaddleBrown;
                        VARIABLE.Text = "X";
                    }

                    Game.UpdateScore(1000);
                }
                else if (ship.life + 2 <= ship.Maxlife)
                {
                    foreach (var VARIABLE in ship.buttons)
                    {
                        if (VARIABLE.Checked)
                        {
                            Cell.NoFireSection(VARIABLE, _enemyBoard, ship.direction);
                        }

                    }

                }

            }
            cell.Enabled = false;

            KolejkaCPU();
        }
        private void FireCPU(Cell cell)
        {

            cell.Checked = true;
            if (cell.Empty)
            {
                cell.BackColor = Color.BlueViolet;
                cell.Text = "*";
            }
            else
            {
                Trafione.Add(cell);
                //        score += 100;
                cell.BackColor = Color.OrangeRed;
                cell.Text = "F";
                var ship = _myShips.First(x => x.buttons.Contains(cell));
                ship.life -= 1;
                if (ship.life == 0)
                {
                    ship.Destroyed = true;
                    foreach (var VARIABLE in ship.buttons)
                    {
                        Cell.SetEveryDirection(VARIABLE, _plansza);
                        VARIABLE.BackColor = Color.SaddleBrown;
                        VARIABLE.Text = "X";
                        Trafione.Remove(VARIABLE);
                    }

                    //          score += 1000;

                }
                else if (ship.life + 2 <= ship.Maxlife)
                {
                    foreach (var VARIABLE in ship.buttons)
                    {
                        if (VARIABLE.Checked)
                        {
                            Cell.NoFireSection(VARIABLE, _plansza, ship.direction);
                        }

                    }

                }

            }
            //          cell.Enabled = false;
        }
        


        private void button1_Click(object sender, EventArgs e)
        {
            _activeShips = 1;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            _activeShips = 2;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            _activeShips = 3;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            _activeShips = 4;
        }


        private void button106_Click(object sender, EventArgs e)
        {
            string a = "Wybierz jaki rodzaj statku chcesz postawić. \n Zaznacz na planszy gdzie chcesz swój statek. \n Kiedy skończysz kliknij przycisk dalej.\n";
            MessageBox.Show(a);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Menu a = new Menu();
            a.Show();
            this.Hide();
        }
        

        void Board_Load(object sender, EventArgs e)
        {
            var _controller = new Board(false);
            _controller.Draw(this);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            _direction = _direction==Direction.Horizontal ? Direction.Vertical : Direction.Horizontal;
            label1.Text=_direction.ToString();
        }

        private void KolejkaCPU()
        {
            while (true)
            {
                var x = _random.Next(10);
                var y = _random.Next(10);

                if (Trafione.Count != 0)
                {
                    var index = _random.Next(Trafione.Count);
                    var cell = Trafione.ElementAt(index);
                    x = cell.X;
                    y = cell.Y;
                    switch (_random.Next(4))
                    {
                        case 0:
                            x += 1;
                            break;

                        case 1:
                            x -= 1;
                            break;
                        case 2:
                            y += 1;
                            break;
                        case 3:
                            y -= 1;
                            break;
                    }


                }

                if (x >= 10 || x < 0 || y >= 10 || y < 0) continue;
                if (_plansza[x, y].Checked) continue;
                if (CheckIsEnd())
                {
                    var score = new Score(true);
                    score.Show();
                }
                else
                {
                    FireCPU(_plansza[x, y]);
                    if (CheckIsEnd())
                    {
                        var score = new Score(false);
                        score.Show();
                    }
                }
              
                break;
            }
        }
        public static bool CheckIsEnd()
        {
            return _myShips.All(ship => ship.Destroyed) || _oponentShips.All(ship => ship.Destroyed);
        }

        private void button107_Click(object sender, EventArgs e)
        {
            var game = new Game(_plansza,_enemyBoard);
            game.Show();
            this.Close();
        }        
    }
}
