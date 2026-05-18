using System;
using System.Drawing;
using System.Windows.Forms;

namespace Ex05
{
    public class GameInterfaceForm : Form
    {
        private Button[,] m_ButtonsBoard;
        private Button[] m_ColumnHeadersNumber;
        private readonly GameManager m_GameManager;
        private Label m_LabelPlayer1;
        private Label m_LabelPlayer1Score;
        private Label m_LabelPlayer2;
        private Label m_LabelPlayer2Score;
        private readonly string m_Player1Name;
        private readonly string m_Player2Name;

        public GameInterfaceForm(int i_Rows, int i_Cols, string i_Player1Name, string i_Player2Name, bool i_IsComputer, GameManager i_Manager)
        {
            this.Text = "4 in a Row !!";
            this.StartPosition = FormStartPosition.CenterScreen;
            m_Player1Name = i_Player1Name;
            m_GameManager = i_Manager;

            if (i_IsComputer)
            {
                m_Player2Name = "Computer";
            }
            else
            {
                m_Player2Name = i_Player2Name;
            }

            initializeGameGrid(i_Rows, i_Cols);
        }

        private void initializeGameGrid(int i_Rows, int i_Cols)
        {
            m_ButtonsBoard = new Button[i_Rows, i_Cols];
            m_ColumnHeadersNumber = new Button[i_Cols];
            int buttonWidth = 40;
            int buttonHeight = 30;
            int spacing = 10;
            int margin = 20;


            for (int col = 0; col < i_Cols; col++)
            {
                Button buttonNumber = new Button();
                buttonNumber.Text = (col + 1).ToString();
                buttonNumber.Size = new Size(buttonWidth, buttonHeight);
                buttonNumber.Left = margin + col * (buttonWidth + spacing);
                buttonNumber.Top = margin;
                buttonNumber.Click += buttonNumber_Click;
                m_ColumnHeadersNumber[col] = buttonNumber;
                Controls.Add(buttonNumber);
            }

            for (int row = 0; row < i_Rows; row++)
            {
                for (int col = 0; col < i_Cols; col++)
                {
                    Button buttonCell = new Button();
                    buttonCell.Size = new Size(buttonWidth, buttonHeight + 10);
                    buttonCell.Left = margin + col * (buttonWidth + spacing);
                    buttonCell.Top = margin + 40 + row * (buttonWidth + spacing);
                    m_ButtonsBoard[row, col] = buttonCell;
                    Controls.Add(buttonCell);
                }
            }

            Button buttonAtTheLastRow = m_ButtonsBoard[i_Rows - 1, 0];
            int finalWidth = i_Cols * (buttonWidth + spacing) + margin * 2;
            int finalHeight = i_Rows * (buttonWidth + spacing) + margin + buttonWidth * 2;
            m_LabelPlayer1 = new Label();
            m_LabelPlayer1.Text = m_Player1Name + ":";
            m_LabelPlayer1.AutoSize = true;
            m_LabelPlayer1.Left += finalWidth / 2 - m_LabelPlayer1.Width;
            m_LabelPlayer1.Top += buttonAtTheLastRow.Bottom + 16;
            Controls.Add(m_LabelPlayer1);
            m_LabelPlayer2 = new Label();
            m_LabelPlayer2.Text = m_Player2Name + ":";
            m_LabelPlayer2.AutoSize = true;
            m_LabelPlayer2.Left = finalWidth * 3 / 4 - m_LabelPlayer2.Width / 2;
            m_LabelPlayer2.Top += m_LabelPlayer1.Top;
            Controls.Add(m_LabelPlayer2);
            m_LabelPlayer1Score = new Label();
            m_LabelPlayer1Score.Text = "0";
            m_LabelPlayer1Score.Left += m_LabelPlayer1.Right + 2;
            m_LabelPlayer1Score.Top += m_LabelPlayer1.Top;
            m_LabelPlayer1Score.AutoSize = true;
            Controls.Add(m_LabelPlayer1Score);
            m_LabelPlayer2Score = new Label();
            m_LabelPlayer2Score.Text = "0";
            m_LabelPlayer2Score.Left += m_LabelPlayer2.Right + 2;
            m_LabelPlayer2Score.Top += m_LabelPlayer2.Top;
            m_LabelPlayer2Score.AutoSize = true;
            Controls.Add(m_LabelPlayer2Score);
            ClientSize = new Size(finalWidth, finalHeight);
            CenterToScreen();
        }

        private void buttonNumber_Click(object sender, EventArgs e)
        {
            Button buttonNumber = sender as Button;
            int columnIndex = int.Parse(buttonNumber.Text) - 1;

            m_GameManager.MakeMove(columnIndex);
        }

        public void UpdateBoardCell(int i_Row, int i_Col, string i_Text)
        {
            m_ButtonsBoard[i_Row, i_Col].Text = i_Text;
        }

        public void DisableColumnButton(int i_ColumnIndex)
        {
            m_ColumnHeadersNumber[i_ColumnIndex].Enabled = false;
        }

        public void ResetBoardUI()
        {
            foreach (Button button in m_ButtonsBoard)
            {
                button.Text = "";
            }

            foreach (Button button in m_ColumnHeadersNumber)
            {
                button.Enabled = true;
            }
        }

        public void IncrementScore(bool i_IsPlayer1)
        {
            if (i_IsPlayer1)
            {
                int currentScore = int.Parse(m_LabelPlayer1Score.Text);
                m_LabelPlayer1Score.Text = (currentScore + 1).ToString();
            }
            else
            {
                int currentScore = int.Parse(m_LabelPlayer2Score.Text);
                m_LabelPlayer2Score.Text = (currentScore + 1).ToString();
            }
        }
    }
}