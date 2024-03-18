namespace HubelSA2
{
    public partial class LegallyDistictWordleBoard : Form
    {

        // needs to have gui improvements
        // textbox stuff
        TextBox[,] grid = new TextBox[6, 5];
        int verticalOffset = 48;
        int horizontalOffset = 32;
        int boxSize = 48;
        int padding = 5;
        // button stuff
        Size buttonSize = new Size(208, 48);
        int currentGuess = 0; // this will keep track of what row we are on
        int letterCount = 0;
        LegallyDistinctWordleLogic wordleLogic;
        Button guessButton;
        string guessWord = "";
        // where to put the function calls to make program run
        public LegallyDistictWordleBoard()
        {

            InitializeComponent();
            SetUpTextBoxGrid();
            SetUpGuessButton();

            //event stuff....
            // our answer string is an event arg
            wordleLogic = new LegallyDistinctWordleLogic();
            wordleLogic.AnswerWordPicked += WordleLogic_AnswerWordPicked;
            wordleLogic.PickWord();


        }


        // needed for the initializer
        // idk how it got here
        // or what its for...
        private void LegallyDistictWordleBoard_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// create guess button
        /// </summary>
        public void SetUpGuessButton()
        {
            guessButton = new Button();
            guessButton.Visible = false;
            guessButton.Text = "Guess Word";
            guessButton.Font = new Font("Nunito", 16);
            guessButton.Size = buttonSize;
            guessButton.Location = new Point(60, 470);
            guessButton.Click += OnButtonClick;
            this.Controls.Add(guessButton);
        }
        public void SetUpTextBoxGrid()
        {
            for (int row = 0; row < grid.GetLength(0); row++)
            {
                for (int col = 0; col < grid.GetLength(1); col++)
                {

                    TextBox temp = new TextBox();
                    temp.Size = new Size(48, 24);
                    temp.Font = new Font("Roboto", 32);
                    temp.MaxLength = 1;
                    temp.Location = new Point((boxSize + padding) * col + horizontalOffset, (boxSize + (4 * padding)) * row + verticalOffset);
                    temp.Margin = new Padding(32);
                    temp.TextChanged += OnTextChange;

                    grid[row, col] = temp;

                    this.Controls.Add(grid[row, col]);
                }
            }
            //need to hide the other rows at the start
            HideOtherRows(currentGuess);
        }
        // will use to move to the next textbox
        // need to add functionality for typos
        public void OnTextChange(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;


            // if letter, increment lettercount and move to next box
            if (textBox.Text.Length == 1)
            {
                letterCount++;
                if (letterCount != grid.GetLength(1)) 
                {
                    SendKeys.Send("{TAB}");
                
                }


            }
            //append letter to guess string
            // if no letter,
            // decrement lettercount and back up
            else if (textBox.Text.Length == 0)
            {
                letterCount--;
                if (letterCount != 0)
                {
                    SendKeys.Send("+{TAB}");

                }
                guessButton.Visible = false;
            }

            // maybe keep track of a letter count var?
            // if letter count = 5
            // show guess button?


            if (letterCount == grid.GetLength(1))
            {
                // create guess string to compare
                // still using the individual textboxes to compare if match answer tho
                for (int i = 0; i < grid.GetLength(1); i++)
                {
                    guessWord += grid[currentGuess, i].Text;

                }
                // convert to lowercase to match database
                guessWord = guessWord.ToLower();
                // show button if word is real and part of database
                if (wordleLogic.wordIsValid(guessWord))
                {
                    guessButton.Visible = true;
                }
                // need to hide guess button if we change our guess
                else
                {
                    guessWord = ""; // reset string if not a word
                                    // should allow for typos
                                    // maybe...
                    guessButton.Visible = false;
                }

            }
        } // end OnTextChange




        /// <summary>
        /// should run thru and check if first the letter matches the spot its supposed to be in
        /// and then if not, check if the letter is even within the word
        /// and if not, update background accordingly
        /// </summary>
        /// <param name="row"></param> taken from whatever guess we are on
        /// also doubles as a guess count
        public void DoesLetterMatch(int row, WordleLogicEventArgs e)
        {

            // loop thru texboxes
            for (int i = 0; i < grid.GetLength(1); i++)
            {

                if (grid[row, i].Text[0] == e.AnswerWord[i]) // read the only character in the textbox and compare it to the answer
                {
                    grid[row, i].BackColor = Color.Green;
                } // loop thru rest of word and see if textbox letter matches anywhere
                else
                {
                    for (int j = 0; j < grid.GetLength(1); j++)
                    {
                        if (grid[row, i].Text[0] == e.AnswerWord[j])
                        {
                            grid[row, i].BackColor = Color.Yellow;
                            break;
                        }
                        else
                        {
                            grid[row, i].BackColor = Color.Red;
                        }
                    }
                }
            }

        } // end DoesLetterMatch


        /// <summary>
        /// will hide the other rows so we can go one at a time
        /// </summary>
        /// <param name="currentRow"></param> used to track our guesses
        public void HideOtherRows(int currentRow)
        {
            // need to set all boxes other than the current row to hidden
            for (int i = currentRow + 1; i < grid.GetLength(0); ++i)
            {
                for (int j = 0; j < grid.GetLength(1); ++j)
                {
                    grid[i, j].Enabled = false;
                }
            }

        } // end HideOtherRows

        /// <summary>
        /// where we will call our doesLetterMatch and set variables
        /// like letter count and current guess accordingly
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnButtonClick(object sender, EventArgs e)
        {
            DoesLetterMatch(currentGuess, new WordleLogicEventArgs(wordleLogic.answerWord));
            // reset vars
            letterCount = 0;
            IsGameOver(currentGuess, new WordleLogicEventArgs(wordleLogic.answerWord));
            currentGuess++;
            guessWord = "";
            // set next row to availible

            for (int i = 0; i < grid.GetLength(1); i++)
            {
                // set next row open and close previous

                grid[currentGuess - 1, i].Enabled = false;
                if (currentGuess < grid.GetLength(0))
                {

                    grid[currentGuess, i].Enabled = true;



                }

            }
            // hide button for next guess
            guessButton.Visible = false;

        } // end OnButtonClick


        // Event handler to handle the picked answerWord
        public void WordleLogic_AnswerWordPicked(object sender, WordleLogicEventArgs e)
        {

            // idk what to put here if anything....
        }
        /// <summary>
        /// will check if all guesses have been used, 
        /// and if all boxes in the row are green
        /// </summary>
        /// <param name="currentGuess"></param>
        public void IsGameOver(int currentGuess,WordleLogicEventArgs e)
        {
            // Initialize gameOver flag
            bool gameOver = true;
            string message = "";

            // Check if all text boxes in the current row are green
            for (int i = 0; i < grid.GetLength(1); i++)
            {
                if (grid[currentGuess, i].BackColor != Color.Green)
                {
                    // At least one text box is not green, so the game is not over
                    gameOver = false;
                    break; // Exit the loop early since we already know the game is not over
                }
            }

            // Display appropriate message based on game state
            if (gameOver)
            {
                // Display victory message
                message = $"Congratulations! You won in {currentGuess+1} guesses!"; // +1 to show accurate count due to where 
                                                                                    // current guess is incremented and starts
                                                                                    // at 0
            }
            else if (currentGuess >= grid.GetLength(1))
            {
                // Display defeat message
                message = $"You lost :( \n So sad.... \nThe word was {e.AnswerWord} btw";
            }

            // Show message box if game is over
            if (gameOver || currentGuess >= grid.GetLength(1))
            {
                
                MessageBox.Show(message);
            }
        }






    } // end class


}