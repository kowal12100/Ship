using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ships.Model
{
    public class Cell : Button

    {
        public int X;
        public int Y;
        public bool Empty;
        public bool Checked;
        public Cell(int x, int y)
        {
            X = x;
            Y = y;
            Empty = true;
            Checked = false;
        }

        public bool CheckIsEmpty()
        {
            return (Empty);
        }

        public static bool CheckIsEmpty(Cell[] cells)
        {
            foreach (var cell in cells)
            {
                if (!cell.CheckIsEmpty())
                {
                    return false;
                }
            }
            return true;
        }


        public static bool CheckIsTOPEmpty(Cell cell, Cell[,] board)
        {
            var x =cell.X;
            var y = 0 > cell.Y - 1 ? 0 : cell.Y - 1;
            return board[x, y].Empty;
        }
        public static bool CheckIsDownEmpty(Cell cell, Cell[,] board)
        {
            var x = cell.X;
            var y = 10 <= cell.Y + 1 ? 9 : cell.Y + 1;
            return board[x, y].Empty;
        }
        public static bool CheckIsLeftPEmpty(Cell cell, Cell[,] board)
        {
            var x =0>=cell.X - 1 ? 0 : cell.X - 1;
            var y = cell.Y;
            return board[x, y].Empty;
        }
        public static bool CheckIsRightEmpty(Cell cell, Cell[,] board)
        {
            var x =10 <= cell.X + 1 ? 9 : cell.X + 1;
            var y = cell.Y;
            return board[x, y].Empty;
        }


        public static bool CheckIsTopLeftCornerEmpty(Cell cell, Cell[,] board)
        {
            var x = 0 >= cell.X - 1 ? 0 : cell.X - 1;
            var y = 0 > cell.Y - 1 ? 0 : cell.Y - 1;
            return board[x, y].Empty;
        }
        public static bool CheckIsDownLeftCornerEmpty(Cell cell, Cell[,] board)
        {
            var x = 0 >= cell.X - 1 ? 0 : cell.X - 1;
            var y = 10 <= cell.Y + 1 ? 9 : cell.Y + 1;
            return board[x, y].Empty;
        }


        public static bool CheckIsTopRightCornerEmpty(Cell cell, Cell[,] board)
        {
            var x = 10 <= cell.X + 1 ? 9 : cell.X + 1;
            var y = 0 > cell.Y - 1 ? 0 : cell.Y - 1;
            return board[x, y].Empty;
        }
        public static bool CheckIsDownRightCornerEmpty(Cell cell, Cell[,] board)
        {
            var x = 10 <= cell.X + 1 ? 9 : cell.X + 1;
            var y = 10 <= cell.Y + 1 ? 9 : cell.Y + 1;
            return board[x, y].Empty;
        }

        public static bool CheckEveryDirection(Cell cell,Cell[,] board)
        {
            return board[cell.X,cell.Y].Empty&&CheckIsDownEmpty(cell, board) && CheckIsTOPEmpty(cell, board) &&
                   CheckIsRightEmpty(cell, board) && CheckIsLeftPEmpty(cell, board) &&
                   CheckIsTopLeftCornerEmpty(cell, board) && CheckIsTopRightCornerEmpty(cell, board) &&
                   CheckIsDownLeftCornerEmpty(cell, board) && CheckIsDownRightCornerEmpty(cell, board);
        }







        public static bool CheckIsAroundEmpty(Cell[] cells,Cell[,] board)
        {
            //int size = 9; //0..10 
            //foreach (var cell in cells)
            //{     
            //        if (!cell.CheckIsEmpty())
            //        {
            //            return false;
            //        }
            //    }
            //foreach (var cell in cells)
            //{











            //    if ((cell.X>0)&&(cell.Y>0)&&(cell.X<= size) &&(cell.Y<=size))
            //    {
            //        return (board[cell.X, cell.Y + 1].CheckIsEmpty() &&
            //                board[cell.X, cell.Y - 1].CheckIsEmpty() &&
            //                board[cell.X + 1, cell.Y].CheckIsEmpty() &&
            //                board[cell.X - 1, cell.Y].CheckIsEmpty());
            //    }
            //    else if (cell.X==0&& (cell.Y > 0) &&(cell.Y <= size))
            //    {
            //        return (board[cell.X, cell.Y + 1].CheckIsEmpty() &&
            //                board[cell.X, cell.Y - 1].CheckIsEmpty() &&
            //                board[cell.X + 1, cell.Y].CheckIsEmpty());
            //    }
            //    else if (cell.Y == 0 && (cell.X > 0) && (cell.X <= size))
            //    {
            //        return (board[cell.X, cell.Y + 1].CheckIsEmpty() &&
            //                board[cell.X + 1, cell.Y].CheckIsEmpty() &&
            //                board[cell.X - 1, cell.Y].CheckIsEmpty());
            //    }
            //    else if (cell.Y == 10 && (cell.X > 0) && (cell.X <= size))
            //    {
            //        return (board[cell.X, cell.Y - 1].CheckIsEmpty() &&
            //                board[cell.X + 1, cell.Y].CheckIsEmpty() &&
            //                board[cell.X - 1, cell.Y].CheckIsEmpty());
            //    }
            //    else if (cell.X == 0 && (cell.Y > 0) && (cell.Y <= size))
            //    {
            //        return (board[cell.X, cell.Y + 1].CheckIsEmpty() &&
            //                board[cell.X, cell.Y - 1].CheckIsEmpty() &&
            //                board[cell.X + 1, cell.Y].CheckIsEmpty());
            //    }


            //}

            return cells.All(cell => CheckEveryDirection(cell, board));
        }

        public static void OznaczNiemozliwe(Cell cell)
        {
            if (!cell.Empty) return;
            cell.BackColor = Color.Yellow;
            cell.Enabled = false;
            cell.Checked = true;
        }


        public static void SetIsTOPEmpty(Cell cell, Cell[,] board)
        {
            var x = cell.X;
            var y = 0 > cell.Y - 1 ? 0 : cell.Y - 1;
            OznaczNiemozliwe(board[x,y]);
        }
        public static void SetIsDownEmpty(Cell cell, Cell[,] board)
        {
            var x = cell.X;
            var y = 10 <= cell.Y + 1 ? 9 : cell.Y + 1;
            OznaczNiemozliwe(board[x, y]);
        }
        public static void SetIsLeftPEmpty(Cell cell, Cell[,] board)
        {
            var x = 0 >= cell.X - 1 ? 0 : cell.X - 1;
            var y = cell.Y;
            OznaczNiemozliwe(board[x, y]);
        }
        public static void SetIsRightEmpty(Cell cell, Cell[,] board)
        {
            var x = 10 <= cell.X + 1 ? 9 : cell.X + 1;
            var y = cell.Y;
            OznaczNiemozliwe(board[x, y]);
        }


        public static void SetIsTopLeftCornerEmpty(Cell cell, Cell[,] board)
        {
            var x = 0 >= cell.X - 1 ? 0 : cell.X - 1;
            var y = 0 > cell.Y - 1 ? 0 : cell.Y - 1;
            OznaczNiemozliwe(board[x, y]);
        }
        public static void SetIsDownLeftCornerEmpty(Cell cell, Cell[,] board)
        {
            var x = 0 >= cell.X - 1 ? 0 : cell.X - 1;
            var y = 10 <= cell.Y + 1 ? 9 : cell.Y + 1;
            OznaczNiemozliwe(board[x, y]);
        }


        public static void SetIsTopRightCornerEmpty(Cell cell, Cell[,] board)
        {
            var x = 10 <= cell.X + 1 ? 9 : cell.X + 1;
            var y = 0 > cell.Y - 1 ? 0 : cell.Y - 1;
            OznaczNiemozliwe(board[x, y]);
        }
        public static void SetIsDownRightCornerEmpty(Cell cell, Cell[,] board)
        {
            var x = 10 <= cell.X + 1 ? 9 : cell.X + 1;
            var y = 10 <= cell.Y + 1 ? 9 : cell.Y + 1;
            OznaczNiemozliwe(board[x, y]);
        }

        public static void SetEveryDirection(Cell cell, Cell[,] board)
        {
            SetIsDownEmpty(cell, board);
            SetIsTOPEmpty(cell, board);
            SetIsRightEmpty(cell, board);
            SetIsLeftPEmpty(cell, board);
            SetIsTopLeftCornerEmpty(cell, board);
            SetIsTopRightCornerEmpty(cell, board);
            SetIsDownLeftCornerEmpty(cell, board);
            SetIsDownRightCornerEmpty(cell, board);
        }
        public static void NoFireSection(Cell cell, Cell[,] board,Direction direction)
        {
        
            if (direction == Direction.Horizontal)
            {         
                SetIsDownEmpty(cell, board);
                SetIsTOPEmpty(cell, board);
            }
            else
            {
                SetIsRightEmpty(cell, board);
                SetIsLeftPEmpty(cell, board);
            }
 
            SetIsTopLeftCornerEmpty(cell, board);
            SetIsTopRightCornerEmpty(cell, board);
            SetIsDownLeftCornerEmpty(cell, board);
            SetIsDownRightCornerEmpty(cell, board);
        }
    }



}
