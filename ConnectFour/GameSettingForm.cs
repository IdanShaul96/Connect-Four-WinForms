using System;
using System.Drawing;
using System.Windows.Forms;

namespace Ex05
{
    internal class GameSettingForm : Form
    {
        private Label m_LabelPlayers;
        private Label m_LabelPlayer1;
        private Label m_LabelPlayer2;
        private Label m_LabelBoardSize;
        private Label m_LabelRows;
        private Label m_LabelCols;
        private TextBox m_TextBoxPlayer1;
        private TextBox m_TextBoxPlayer2;
        private NumericUpDown m_NumericUpDownRows;
        private NumericUpDown m_NumericUpDownCols;
        private Button m_ButtonStart;
        private CheckBox m_CheckBoxPlayer2;

        public string Player1Name
        {
            get 
            {
                return m_TextBoxPlayer1.Text;
            }
        }

        public string Player2Name
        {
            get 
            {
                return m_TextBoxPlayer2.Text;
            }
        }

        public bool IsComputer
        {
            get 
            {
                return !m_CheckBoxPlayer2.Checked;
            }
        }

        public int SelectedRows
        {
            get 
            { 
                return (int)m_NumericUpDownRows.Value;
            }
        }

        public int SelectedCols
        {
            get
            {
                return (int)m_NumericUpDownCols.Value;
            }
        }

        public GameSettingForm()
        {
            this.Text = "Game Setting";
            this.Size = new Size(300, 300);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            initializeComponents();
        }

        private void initializeComponents()
        {
            m_LabelPlayers = new Label();
            m_LabelPlayers.Text = "Players:";
            m_LabelPlayers.Left += 16;
            m_LabelPlayers.Top += 16;
            this.Controls.Add(m_LabelPlayers);
            m_LabelPlayer1 = new Label();
            m_LabelPlayer1.Text = "Player 1:";
            m_LabelPlayer1.Left += m_LabelPlayers.Left + 16;
            m_LabelPlayer1.Top += m_LabelPlayers.Bottom + 10;
            m_LabelPlayer1.AutoSize = true;
            this.Controls.Add(m_LabelPlayer1);
            m_LabelPlayer2 = new Label();
            m_LabelPlayer2.Text = "Player 2:";
            m_LabelPlayer2.Left += m_LabelPlayer1.Left + 16;
            m_LabelPlayer2.Top += m_LabelPlayer1.Bottom + 16;
            m_LabelPlayer2.AutoSize = true;
            this.Controls.Add(m_LabelPlayer2);
            m_LabelBoardSize = new Label();
            m_LabelBoardSize.Text = "Board Size:";
            m_LabelBoardSize.Left += m_LabelPlayers.Left;
            m_LabelBoardSize.Top += m_LabelPlayer2.Bottom + 20;
            this.Controls.Add(m_LabelBoardSize);
            m_LabelRows = new Label();
            m_LabelRows.Text = "Rows:";
            m_LabelRows.Left += m_LabelPlayer1.Left;
            m_LabelRows.Top += m_LabelBoardSize.Bottom + 10;
            m_LabelRows.AutoSize = true;
            this.Controls.Add(m_LabelRows);
            m_LabelCols = new Label();
            m_LabelCols.Text = "Cols:";
            m_LabelCols.Left += m_LabelRows.Right + 80;
            m_LabelCols.Top += m_LabelBoardSize.Bottom + 10;
            m_LabelCols.AutoSize = true;
            this.Controls.Add(m_LabelCols);
            m_TextBoxPlayer1 = new TextBox();
            m_TextBoxPlayer1.Top = m_LabelPlayer1.Top;
            m_TextBoxPlayer1.Left = m_LabelPlayer1.Right + 55;
            this.Controls.Add(m_TextBoxPlayer1);
            m_TextBoxPlayer2 = new TextBox();
            m_TextBoxPlayer2.Top = m_LabelPlayer2.Top;
            m_TextBoxPlayer2.Left = m_TextBoxPlayer1.Left;
            m_TextBoxPlayer2.Text = "[Computer]";
            m_TextBoxPlayer2.Enabled = false;
            this.Controls.Add(m_TextBoxPlayer2);
            m_CheckBoxPlayer2 = new CheckBox();
            m_CheckBoxPlayer2.Top = m_TextBoxPlayer2.Top + ((m_TextBoxPlayer2.Height - m_CheckBoxPlayer2.Height) / 2);
            m_CheckBoxPlayer2.Left += m_LabelPlayers.Left + 16;
            m_CheckBoxPlayer2.CheckedChanged += checkBox_Click;
            this.Controls.Add(m_CheckBoxPlayer2);
            m_NumericUpDownRows = new NumericUpDown();
            m_NumericUpDownRows.Top = m_LabelRows.Top + ((m_LabelRows.Height - m_NumericUpDownRows.Height) / 2);
            m_NumericUpDownRows.Left = m_LabelRows.Right + 10;
            m_NumericUpDownRows.Size = new Size(45, 20);
            m_NumericUpDownRows.Minimum = 4;
            m_NumericUpDownRows.Maximum = 10;
            this.Controls.Add(m_NumericUpDownRows);
            m_NumericUpDownCols = new NumericUpDown();
            m_NumericUpDownCols.Top = m_LabelCols.Top + ((m_LabelCols.Height - m_NumericUpDownCols.Height) / 2);
            m_NumericUpDownCols.Left = m_LabelCols.Right + 10;
            m_NumericUpDownCols.Size = new Size(45, 20);
            m_NumericUpDownCols.Minimum = 4;
            m_NumericUpDownCols.Maximum = 10;
            this.Controls.Add(m_NumericUpDownCols);
            m_ButtonStart = new Button();
            m_ButtonStart.Text = "Start!";
            m_ButtonStart.Top = m_NumericUpDownRows.Bottom + 30;
            m_ButtonStart.Size = new Size(250, 30);
            m_ButtonStart.Left = m_LabelPlayers.Left;
            m_ButtonStart.Click += startButton_Click;
            this.Controls.Add(m_ButtonStart);
        }

        private void checkBox_Click(object sender, EventArgs e)
        {
            if (m_CheckBoxPlayer2.Checked)
            {
                m_TextBoxPlayer2.Enabled = true;
                m_TextBoxPlayer2.Text = "";
            }
            else
            {
                m_TextBoxPlayer2.Enabled = false;
                m_TextBoxPlayer2.Text = "[Computer]";
            }
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            bool isValid = true;

            if (string.IsNullOrWhiteSpace(m_TextBoxPlayer1.Text))
            {
                MessageBox.Show("Please enter a name for Player 1!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                isValid = false;
            }

            if (m_CheckBoxPlayer2.Checked && string.IsNullOrWhiteSpace(m_TextBoxPlayer2.Text))
            {
                MessageBox.Show("Please enter a name for Player 2!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                isValid = false;
            }
            if (isValid)
            {
                this.DialogResult = DialogResult.OK;
                //this.Close();
            }
        }
    }
}