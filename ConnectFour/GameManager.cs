using System.Windows.Forms;

namespace Ex05
{
    public class GameManager
    {
        private BoardLogic m_GameLogic;
        private GameInterfaceForm m_GameForm;
        private bool m_IsPlayer1Turn = true;
        private string m_Player1Name;
        private string m_Player2Name;
        private bool m_IsComputerMode;

        public void Run()
        {
            GameSettingForm settingsForm = new GameSettingForm();
            settingsForm.ShowDialog();

            if (settingsForm.DialogResult == DialogResult.OK)
            {
                m_Player1Name = settingsForm.Player1Name;
                m_Player2Name = settingsForm.Player2Name;
                m_IsComputerMode = settingsForm.IsComputer;
                int rows = settingsForm.SelectedRows;
                int cols = settingsForm.SelectedCols;
                m_GameLogic = new BoardLogic(rows, cols);
                m_GameForm = new GameInterfaceForm(rows, cols, m_Player1Name, m_Player2Name, m_IsComputerMode, this);
                m_GameForm.ShowDialog();
            }
        }

        public void MakeMove(int i_ColIndex)
        {
            int rowIndex;
            string currentPlayer;
            eCellState currentDisc;

            m_GameLogic.TryDropDisc(out rowIndex, i_ColIndex);

            if (m_IsPlayer1Turn)
            {
                currentPlayer = m_Player1Name;
                currentDisc = eCellState.Player1;
            }
            else
            {
                currentPlayer = m_Player2Name;
                currentDisc = eCellState.Player2;
            }

            m_GameLogic.InsertNewDisc(currentDisc, rowIndex, i_ColIndex);

            if (m_GameLogic.IsColumnFull(i_ColIndex))
            {
                m_GameForm.DisableColumnButton(i_ColIndex);
            }

            if (m_IsPlayer1Turn)
            {
                m_GameForm.UpdateBoardCell(rowIndex, i_ColIndex, "X");
            }
            else
            {
                m_GameForm.UpdateBoardCell(rowIndex, i_ColIndex, "O");
            }

            if (!m_GameLogic.IsGameWon(currentDisc, rowIndex, i_ColIndex))
            {
                m_IsPlayer1Turn = !m_IsPlayer1Turn;
            }

            if (m_GameLogic.IsGameWon(currentDisc, rowIndex, i_ColIndex))
            {
                if (m_IsComputerMode && currentDisc == eCellState.Player2)
                {
                    currentPlayer = "Computer";
                }

                DialogResult result = MessageBox.Show($"{currentPlayer} Won!!\nAnother Round?", "A Win!", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (result == DialogResult.Yes)
                {
                    bool isPlayer1 = (currentDisc == eCellState.Player1);
                    m_GameForm.IncrementScore(isPlayer1);
                    m_GameLogic.Reset();
                    m_GameForm.ResetBoardUI();
                    m_IsPlayer1Turn = true;
                }
                else
                {
                    m_GameForm.Close();
                }
            }

            if (m_GameLogic.IsFull())
            {
                DialogResult result = MessageBox.Show("Tie!!\nAnother Round?", "A Tie!", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (result == DialogResult.Yes)
                {
                    m_GameLogic.Reset();
                    m_GameForm.ResetBoardUI();
                }

                else 
                {
                    m_GameForm.Close(); 
                }
            }
          
            if (!m_IsPlayer1Turn && m_IsComputerMode)
            {
                int computerCol = m_GameLogic.GetComputerMove();
                MakeMove(computerCol);
            }
        }
    }
}