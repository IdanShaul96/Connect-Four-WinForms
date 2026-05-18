using System;

namespace Ex05
{
    public class BoardLogic
    {
        private readonly eCellState[,] r_Matrix;

        public BoardLogic(int i_Rows, int i_Columns)
        {
            Rows = i_Rows;
            Columns = i_Columns;
            r_Matrix = new eCellState[i_Rows, i_Columns];
            Reset();
        }

        public int Rows 
        {
            get;
        }

        public int Columns
        {
            get; 
        }

        public eCellState GetCell(int i_Row, int i_Column)
        {
            return r_Matrix[i_Row, i_Column];
        }

        public void Reset()
        {
            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Columns; col++)
                {
                    r_Matrix[row, col] = eCellState.Empty;
                }
            }
        }

        public bool IsColumnFull(int i_Column)
        {
            return r_Matrix[0, i_Column] != eCellState.Empty;
        }

        private int getNextFreeRow(int i_Column)
        {
            int rowNumber = -1;

            for (int row = Rows - 1; row >= 0; row--)
            {
                if (GetCell(row, i_Column) == eCellState.Empty)
                {
                    rowNumber = row;
                    break;
                }
            }

            return rowNumber;
        }

        public void TryDropDisc(out int o_Row, int i_Column)
        {
            o_Row = getNextFreeRow(i_Column);
        }

        public bool IsFull()
        {
            bool isFull = true;

            for (int column = 0; column < Columns; column++)
            {
                if (!IsColumnFull(column))
                {
                    isFull = false;
                    break;
                }
            }

            return isFull;
        }

        public void InsertNewDisc(eCellState i_Disc, int i_Row, int i_Column)
        {
            r_Matrix[i_Row, i_Column] = i_Disc;
        }

        public bool IsGameWon(eCellState i_Disc, int i_Row, int i_Column)
        {
            bool gameIsWon = false;
            bool rows;
            bool columns;
            bool diagonal;

            rows = checkRowsForVictory(i_Disc, i_Row);
            columns = checkColumnsForVictory(i_Disc, i_Column);
            diagonal = checkDiagonalForVictory(i_Disc, i_Row, i_Column);

            if (rows || columns || diagonal)
            {
                gameIsWon = true;
            }

            return gameIsWon;
        }

        private bool checkRowsForVictory(eCellState i_Disc, int i_Row)
        {
            int countDiscsInARow = 0;
            bool isGameWon = false;

            for (int indexColumn = 0; indexColumn < Columns; indexColumn++)
            {
                if (r_Matrix[i_Row, indexColumn] == i_Disc)
                {
                    countDiscsInARow++;

                    if (countDiscsInARow == 4)
                    {
                        isGameWon = true;
                        break;
                    }
                }
                else
                {
                    countDiscsInARow = 0;
                }
            }

            return isGameWon;
        }

        private bool checkColumnsForVictory(eCellState i_Disc, int i_Column)
        {
            int countDiscsInARow = 0;
            bool isGameWon = false;

            for (int indexRows = 0; indexRows < Rows; indexRows++)
            {
                if (r_Matrix[indexRows, i_Column] == i_Disc)
                {
                    countDiscsInARow++;

                    if (countDiscsInARow == 4)
                    {
                        isGameWon = true;
                        break;
                    }
                }
                else
                {
                    countDiscsInARow = 0;
                }
            }

            return isGameWon;
        }

        private bool checkDiagonalForVictory(eCellState i_Disc, int i_Row, int i_Column)
        {
            bool isGameWon = false;
            int countDiscInDiagonalDown = 0;
            int countDiscInDiagonalUp = 0;

            countDiscInDiagonalDown = countDiagonalDown(i_Disc, i_Row, i_Column);
            if (countDiscInDiagonalDown == 4)
            {
                isGameWon = true;
            }

            if (!isGameWon)
            {
                countDiscInDiagonalUp = countDiagonalUp(i_Disc, i_Row, i_Column);

                if (countDiscInDiagonalUp == 4)
                {
                    isGameWon = true;
                }
            }

            return isGameWon;
        }

        private int countDiagonalDown(eCellState i_Disc, int i_Row, int i_Column)
        {
            int countDiscs = 0;
            int minMovmentOfDiagonal = Math.Min(i_Row, i_Column);
            int currentRow = i_Row - minMovmentOfDiagonal;
            int currentColumn = i_Column - minMovmentOfDiagonal;

            while (currentRow < Rows && currentColumn < Columns)
            {
                if (r_Matrix[currentRow, currentColumn] == i_Disc)
                {
                    countDiscs++;
                    if (countDiscs == 4)
                    {
                        break;
                    }
                }

                else
                {
                    countDiscs = 0;
                }

                currentRow++;
                currentColumn++;
            }

            return countDiscs;
        }

        private int countDiagonalUp(eCellState i_Disc, int i_Row, int i_Column)
        {
            int stepsDown = Rows - 1 - i_Row;
            int stepsLeft = i_Column;

            int countDiscs = 0;
            int minStepsToMove = Math.Min(stepsDown, stepsLeft);
            int currentRow = i_Row + minStepsToMove;
            int currentColumn = i_Column - minStepsToMove;

            while (currentRow >= 0 && currentColumn < Columns)
            {
                if (r_Matrix[currentRow, currentColumn] == i_Disc)
                {
                    countDiscs++;
                    if (countDiscs == 4)
                    {
                        break;
                    }
                }
                else
                {
                    countDiscs = 0;
                }

                currentRow--;
                currentColumn++;
            }

            return countDiscs;
        }

        public int GetComputerMove()
        {
            int randomColumn = 0;
            Random randomGenerator = new Random();
            bool isValidColumn = false;

            while (!isValidColumn)
            {
                randomColumn = randomGenerator.Next(0, Columns);

                if (!IsColumnFull(randomColumn))
                {
                    isValidColumn = true;
                }
            }

            return randomColumn;
        }
    }
}