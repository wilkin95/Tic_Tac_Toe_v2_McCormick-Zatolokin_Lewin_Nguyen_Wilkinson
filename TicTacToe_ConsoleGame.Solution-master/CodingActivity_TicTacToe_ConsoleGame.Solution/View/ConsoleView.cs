using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace CodingActivity_TicTacToe_ConsoleGame
{
    public class ConsoleView
    {
        #region ENUMS

        public enum ViewState
        {
            Active,
            PlayerTimedOut, // TODO Track player time on task
            PlayerUsedMaxAttempts
        }

        #endregion

        #region FIELDS

        private const int GAMEBOARD_VERTICAL_LOCATION = 4;

        private const int POSITIONPROMPT_VERTICAL_LOCATION = 12;
        private const int POSITIONPROMPT_HORIZONTAL_LOCATION = 3;

        private const int MESSAGEBOX_VERTICAL_LOCATION = 15;

        private const int TOP_LEFT_ROW = 3;
        private const int TOP_LEFT_COLUMN = 6;

        private Gameboard _gameboard;
        private ViewState _currentViewStat;

        #endregion

        #region PROPERTIES
        public ViewState CurrentViewState
        {
            get { return _currentViewStat; }
            set { _currentViewStat = value; }
        }

        #endregion

        #region CONSTRUCTORS

        public ConsoleView(Gameboard gameboard)
        {
            _gameboard = gameboard;

            InitializeView();

        }

        #endregion

        #region METHODS

        /// <summary>
        /// Initialize the console view
        /// </summary>
        /// 
        //The two players that will be refrenced through a few different Methods.

        Player Player1 = new Player();
        Player Player2 = new Player();


        public void InitializeView()
        {

            _currentViewStat = ViewState.Active;

            InitializeConsole();
        }

        /// <summary>
        /// configure the console window
        /// </summary>
        public void InitializeConsole()
        {
            ConsoleUtil.WindowWidth = ConsoleConfig.windowWidth;
            ConsoleUtil.WindowHeight = ConsoleConfig.windowHeight;

            Console.BackgroundColor = ConsoleConfig.bodyBackgroundColor;
            Console.ForegroundColor = ConsoleConfig.bodyBackgroundColor;

            ConsoleUtil.WindowTitle = "The Tic-tac-toe Game";
        }

        /// <summary>
        /// display the Continue prompt
        /// </summary>
        public void DisplayContinuePrompt()
        {
            Console.CursorVisible = false;

            Console.WriteLine();

            ConsoleUtil.DisplayMessage("Press any key to continue.");
            ConsoleKeyInfo response = Console.ReadKey();

            Console.WriteLine();

            Console.CursorVisible = true;
        }

        public void AnyPointExit()
        {
            ConsoleKeyInfo userResponse = Console.ReadKey(true);


            if (userResponse.Key == ConsoleKey.Escape)
            {
                Environment.Exit(0);
            }


        }

        /// <summary>
        /// display the Exit prompt on a clean screen
        /// </summary>
        public void DisplayExitPrompt()
        {
            ConsoleUtil.DisplayReset();

            Console.CursorVisible = false;

            Console.WriteLine();
            ConsoleUtil.DisplayMessage("Thank you for playing the game. Press any key to Exit.");

            Console.ReadKey();

            System.Environment.Exit(1);
        }

        /// <summary>
        /// display the session timed out screen
        /// </summary>
        public void DisplayTimedOutScreen()
        {
            ConsoleUtil.HeaderText = "Session Timed Out!";
            ConsoleUtil.DisplayReset();

            DisplayMessageBox("It appears your session has timed out.");

            DisplayContinuePrompt();
        }

        /// <summary>
        /// display the maximum attempts reached screen
        /// </summary>
        public void DisplayMaxAttemptsReachedScreen()
        {
            StringBuilder sb = new StringBuilder();

            ConsoleUtil.HeaderText = "Maximum Attempts Reached!";
            ConsoleUtil.DisplayReset();

            sb.Append(" It appears that you are having difficulty entering your");
            sb.Append(" choice. Please refer to the instructions and play again.");

            DisplayMessageBox(sb.ToString());

            DisplayContinuePrompt();
        }



        /// <summary>
        /// Inform the player that their position choice is not available
        /// </summary>
        public void DisplayGamePositionChoiceNotAvailableScreen()
        {
            StringBuilder sb = new StringBuilder();

            ConsoleUtil.HeaderText = "Position Choice Unavailable";
            ConsoleUtil.DisplayReset();

            sb.Append(" It appears that you have chosen a position that is all ready");
            sb.Append(" taken. Please try again.");

            DisplayMessageBox(sb.ToString());

            DisplayContinuePrompt();
        }


        //prompt player to choose quit or main menu//
        public void OpeningScreenPrompt()
        {
            Console.CursorVisible = true;


            bool validResponse = false;



            while (!validResponse)
            {
                Console.WriteLine();

                ConsoleUtil.DisplayMessage("     Please choose from the options below: (Type in the number and press enter)");
                Console.WriteLine();
                Console.SetCursorPosition(10, 16);
                ConsoleUtil.DisplayMessage("     1. Main Menu" + Environment.NewLine + "2. Quit");

                int choice;

                Console.SetCursorPosition(13, 18);
                string response = Console.ReadLine();


                if (Int32.TryParse(response, out choice))
                {
                    if (choice == 1)
                    {

                        validResponse = true;

                    }
                    else if (choice == 2)
                    {
                        DisplayExitPrompt();
                        validResponse = true;

                    }
                    else
                    {
                        Console.WriteLine("Please choose from the numbers listed.");
                        validResponse = false;
                        ConsoleUtil.DisplayPromptMessage("Press any key to try again.");
                        Console.ReadKey();
                        ConsoleUtil.DisplayReset();

                    }
                }
                else
                {
                    Console.WriteLine();
                    ConsoleUtil.DisplayMessage("That is not a valid response. ");
                    ConsoleUtil.DisplayPromptMessage("Press any key to try again.");
                    Console.ReadKey();
                    validResponse = false;

                    ConsoleUtil.DisplayReset();
                }
            }


        }



        /// <summary>
        /// display the welcome screen
        /// </summary>
        public void DisplayWelcomeScreen()
        {
            StringBuilder sb = new StringBuilder();

            ConsoleUtil.HeaderText = "The Tic-tac-toe Game";
            ConsoleUtil.DisplayReset();

            ConsoleUtil.DisplayMessage("Modified by Patrick McCormick-Zatolokin, Austin Nguyen, Nick Lewin, and Terri Wilkinson");
            ConsoleUtil.DisplayMessage("Northwestern Michigan College CIT 255");
            Console.WriteLine();

            sb.Clear();
            sb.AppendFormat("This application is designed to allow two players to play ");
            sb.AppendFormat("a game of tic-tac-toe. The rules are the standard rules for the ");
            sb.AppendFormat("game with each player taking a turn.");
            ConsoleUtil.DisplayMessage(sb.ToString());
            Console.WriteLine();

            OpeningScreenPrompt();
        }

        /// <summary>
        /// display a closing screen when the user quits the application
        /// </summary>
        public void DisplayClosingScreen()
        {
            ConsoleUtil.HeaderText = "The Tic-tac-toe Game";
            ConsoleUtil.DisplayReset();

            ConsoleUtil.DisplayMessage("Thank you for using The Tic-tac-toe Game.");

            DisplayContinuePrompt();
        }

        public void DisplayGameArea()
        {
            ConsoleUtil.HeaderText = "Current Game Board";
            ConsoleUtil.DisplayReset();

            DisplayGameboard();
            DisplayGameStatus();
        }

        public void DisplayCurrentGameStatus(int roundsPlayed, int playerXWins, int playerOWins, int catsGames)
        {
            ConsoleUtil.HeaderText = "Current Game Status";
            ConsoleUtil.DisplayReset();

            double playerXPercentageWins = (double)playerXWins / roundsPlayed;
            double playerOPercentageWins = (double)playerOWins / roundsPlayed;
            double percentageOfCatsGames = (double)catsGames / roundsPlayed;
            
            if (Player1.GamePiece == "X")
            {
                ConsoleUtil.DisplayMessage("Rounds Played: " + roundsPlayed);
                ConsoleUtil.DisplayMessage("Rounds for won for " + Player1.playerName + ": " + playerXWins + " - " + String.Format("{0:P2}", playerXPercentageWins));
                ConsoleUtil.DisplayMessage("Rounds for won for " + Player2.playerName + ": " + playerOWins + " - " + String.Format("{0:P2}", playerOPercentageWins));
                ConsoleUtil.DisplayMessage("Cat's Games: " + catsGames + " - " + String.Format("{0:P2}", percentageOfCatsGames));

            }
            else if (Player2.GamePiece == "X")
            {
                ConsoleUtil.DisplayMessage("Rounds Played: " + roundsPlayed);
                ConsoleUtil.DisplayMessage("Rounds for won for " + Player2.playerName + ": " + playerXWins + " - " + String.Format("{0:P2}", playerXPercentageWins));
                ConsoleUtil.DisplayMessage("Rounds for won for " + Player1.playerName + ": " + playerOWins + " - " + String.Format("{0:P2}", playerOPercentageWins));
                ConsoleUtil.DisplayMessage("Cat's Games: " + catsGames + " - " + String.Format("{0:P2}", percentageOfCatsGames));

            } else
            {
                ConsoleUtil.DisplayMessage("Rounds Played: " + roundsPlayed);
                ConsoleUtil.DisplayMessage("Rounds for won for X's: " + playerXWins + " - " + String.Format("{0:P2}", playerXPercentageWins));
                ConsoleUtil.DisplayMessage("Rounds for won for O's: " + playerOWins + " - " + String.Format("{0:P2}", playerOPercentageWins));
                ConsoleUtil.DisplayMessage("Cat's Games: " + catsGames + " - " + String.Format("{0:P2}", percentageOfCatsGames));

            }
            
            DisplayContinuePrompt();

        }

        public void DisplayHistoricGameStats()
        {
            ConsoleUtil.HeaderText = "Historic Game Stats";
            ConsoleUtil.DisplayReset();

            //double playerXPercentageWins = (double)playerXWins / roundsPlayed;
            //double playerOPercentageWins = (double)playerOWins / roundsPlayed;
            //double percentageOfCatsGames = (double)catsGames / roundsPlayed;

            //ConsoleUtil.DisplayMessage("Rounds Played: " + roundsPlayed);
            //ConsoleUtil.DisplayMessage("Rounds for Player X: " + playerXWins + " - " + String.Format("{0:P2}", playerXPercentageWins));
            //ConsoleUtil.DisplayMessage("Rounds for Player O: " + playerOWins + " - " + String.Format("{0:P2}", playerOPercentageWins));
            //ConsoleUtil.DisplayMessage("Cat's Games: " + catsGames + " - " + String.Format("{0:P2}", percentageOfCatsGames));

            Console.WriteLine("I'm sorry, that option is not available at this time.");


            DisplayContinuePrompt();
        }

        public void SaveGameResults()
        {
            ConsoleUtil.HeaderText = "Save Game Results";
            ConsoleUtil.DisplayReset();

            //double playerXPercentageWins = (double)playerXWins / roundsPlayed;
            //double playerOPercentageWins = (double)playerOWins / roundsPlayed;
            //double percentageOfCatsGames = (double)catsGames / roundsPlayed;

            //ConsoleUtil.DisplayMessage("Rounds Played: " + roundsPlayed);
            //ConsoleUtil.DisplayMessage("Rounds for Player X: " + playerXWins + " - " + String.Format("{0:P2}", playerXPercentageWins));
            //ConsoleUtil.DisplayMessage("Rounds for Player O: " + playerOWins + " - " + String.Format("{0:P2}", playerOPercentageWins));
            //ConsoleUtil.DisplayMessage("Cat's Games: " + catsGames + " - " + String.Format("{0:P2}", percentageOfCatsGames));

            Console.WriteLine("I'm sorry, that option is not available at this time.");


            DisplayContinuePrompt();
            DisplayGetMenuChoice();
        }

        public bool DisplayNewRoundPrompt()
        {
            ConsoleUtil.HeaderText = "Continue or Quit";
            ConsoleUtil.DisplayReset();

            return DisplayGetYesNoPrompt("Would you like to play another round?");
        }

        public void DisplayGameStatus()
        {
            StringBuilder sb = new StringBuilder();

            switch (_gameboard.CurrentRoundState)
            {
                case Gameboard.GameboardState.NewRound:
                    //
                    // The new game status should not be an necessary option here
                    //
                    break;
                case Gameboard.GameboardState.PlayerXTurn:
                    if (Player1.GamePiece == "X")
                    {
                        DisplayMessageBox("It is currently " + Player1.playerName + "'s turn.");

                    } else if (Player2.GamePiece == "X")
                    {
                        DisplayMessageBox("It is currently " + Player2.playerName + "'s turn.");

                    } 

                    break;
                case Gameboard.GameboardState.PlayerOTurn:
                    if (Player1.GamePiece == "O")
                    {
                        DisplayMessageBox("It is currently " + Player1.playerName + "'s turn.");

                    }
                    else if (Player2.GamePiece == "O")
                    {
                        DisplayMessageBox("It is currently " + Player2.playerName + "'s turn.");

                    }
                    break;
                case Gameboard.GameboardState.PlayerXWin:


                    if (Player1.GamePiece == "X")
                    {
                        DisplayMessageBox("It is currently " + Player1.playerName + "'s turn.");
                        Player1.currentWins += 1;

                    }
                    else if (Player2.GamePiece == "X")
                    {
                        DisplayMessageBox(Player2.playerName + " Wins! Press any key to continue.");
                        Player2.currentWins += 1;

                    }

                    Console.CursorVisible = false;
                    Console.ReadKey();
                    Console.CursorVisible = true;
                    break;
                case Gameboard.GameboardState.PlayerOWin:

                    if (Player1.GamePiece == "O")
                    {
                        DisplayMessageBox("It is currently " + Player1.playerName + "'s turn.");
                        Player1.currentWins += 1;

                    }
                    else if (Player2.GamePiece == "O")
                    {
                        DisplayMessageBox(Player2.playerName + " Wins! Press any key to continue.");
                        Player2.currentWins += 1;

                    }

                    Console.CursorVisible = false;
                    Console.ReadKey();
                    Console.CursorVisible = true;
                    break;
                case Gameboard.GameboardState.CatsGame:
                    DisplayMessageBox("Cat's Game! Press any key to continue.");

                    Console.CursorVisible = false;
                    Console.ReadKey();
                    Console.CursorVisible = true;
                    break;
                default:
                    break;
            }
        }

        public void DisplayMessageBox(string message)
        {
            string leftMargin = new String(' ', ConsoleConfig.displayHorizontalMargin);
            string topBottom = new String('*', ConsoleConfig.windowWidth - 2 * ConsoleConfig.displayHorizontalMargin);

            StringBuilder sb = new StringBuilder();

            Console.SetCursorPosition(0, MESSAGEBOX_VERTICAL_LOCATION);
            Console.WriteLine(leftMargin + topBottom);

            Console.WriteLine(ConsoleUtil.Center("Game Status"));

            ConsoleUtil.DisplayMessage(message);

            Console.WriteLine(Environment.NewLine + leftMargin + topBottom);
        }

        /// <summary>
        /// display the current game board
        /// </summary>
        private void DisplayGameboard()
        {
            //
            // move cursor below header
            //
            Console.SetCursorPosition(0, GAMEBOARD_VERTICAL_LOCATION);

            Console.Write("\t\t\t        |---+---+---|\n");

            for (int i = 0; i < 3; i++)
            {
                Console.Write("\t\t\t        | ");

                for (int j = 0; j < 3; j++)
                {
                    if (_gameboard.PositionState[i, j] == Gameboard.PlayerPiece.None)
                    {
                        Console.Write(" " + " | ");
                    }
                    else
                    {
                        Console.Write(_gameboard.PositionState[i, j] + " | ");
                    }

                }

                Console.Write("\n\t\t\t        |---+---+---|\n");
            }

        }

        /// <summary>
        /// display prompt for a player's next move
        /// </summary>
        /// <param name="coordinateType"></param>
        private void DisplayPositionPrompt(string coordinateType)
        {
            //
            // Clear line by overwriting with spaces
            //
            Console.SetCursorPosition(POSITIONPROMPT_HORIZONTAL_LOCATION, POSITIONPROMPT_VERTICAL_LOCATION);
            Console.Write(new String(' ', ConsoleConfig.windowWidth));
            //
            // Write new prompt
            //
            Console.SetCursorPosition(POSITIONPROMPT_HORIZONTAL_LOCATION, POSITIONPROMPT_VERTICAL_LOCATION);
            Console.Write("Enter " + coordinateType + " number: ");
        }

        /// <summary>
        /// Display a Yes or No prompt with a message
        /// </summary>
        /// <param name="promptMessage">prompt message</param>
        /// <returns>bool where true = yes</returns>
        private bool DisplayGetYesNoPrompt(string promptMessage)
        {
            bool yesNoChoice = false;
            bool validResponse = false;
            string userResponse;

            ConsoleUtil.DisplayPromptMessage(promptMessage + "(yes/no)");
            userResponse = Console.ReadLine();

            while (!validResponse)
            {
                ConsoleUtil.DisplayReset();



                if (userResponse.ToUpper() == "YES")
                {
                    validResponse = true;
                    yesNoChoice = true;

                }
                else if (userResponse.ToUpper() == "NO")
                {
                    validResponse = true;
                    yesNoChoice = false;
                    break;
                }
                else
                {
                    ConsoleUtil.DisplayMessage(
                        "It appears that you have entered an incorrect response." +
                        " Please enter either \"yes\" or \"no\"."
                        );
                    DisplayContinuePrompt();
                }
            }

            return yesNoChoice;
        }



        /// <summary>
        /// Get a player's position choice within the correct range of the array
        /// Note: The ConsoleView is allowed access to the GameboardPosition struct.
        /// </summary>
        /// <returns>GameboardPosition</returns>
        public GameboardPosition GetPlayerPositionChoice()
        {
            //
            // Initialize gameboardPosition with -1 values
            //
            GameboardPosition gameboardPosition = new GameboardPosition(-1, -1);

            //
            // Get row number from player.
            //
            gameboardPosition.Row = PlayerCoordinateChoice("Row");

            //
            // Get column number.
            //
            if (CurrentViewState != ViewState.PlayerUsedMaxAttempts)
            {
                gameboardPosition.Column = PlayerCoordinateChoice("Column");
            }

            return gameboardPosition;

        }

        /// <summary>
        /// Validate the player's coordinate response for integer and range
        /// </summary>
        /// <param name="coordinateType">an integer value within proper range or -1</param>
        /// <returns></returns>
        private int PlayerCoordinateChoice(string coordinateType)
        {
            int tempCoordinate = -1;
            int numOfPlayerAttempts = 1;
            int maxNumOfPlayerAttempts = 4;

            while ((numOfPlayerAttempts <= maxNumOfPlayerAttempts))
            {
                DisplayPositionPrompt(coordinateType);

                if (int.TryParse(Console.ReadLine(), out tempCoordinate))
                {
                    //
                    // Player response within range
                    //
                    if (tempCoordinate >= 1 && tempCoordinate <= _gameboard.MaxNumOfRowsColumns)
                    {
                        return tempCoordinate;
                    }
                    //
                    // Player response out of range
                    //
                    else
                    {
                        DisplayMessageBox(coordinateType + " numbers are limited to (1,2,3)");
                    }
                }
                //
                // Player response cannot be parsed as integer
                //
                else
                {
                    DisplayMessageBox(coordinateType + " numbers are limited to (1,2,3)");
                }

                //
                // Increment the number of player attempts
                //
                numOfPlayerAttempts++;
            }

            //
            // Player used maximum number of attempts, set view state and return
            //
            CurrentViewState = ViewState.PlayerUsedMaxAttempts;
            return tempCoordinate;
        }

        public void DisplayRulesScreen()
        {
            ConsoleUtil.DisplayReset();
            ConsoleUtil.HeaderText = "The Rules";

            Console.WriteLine();
            Console.WriteLine("***************************************************");
            ConsoleUtil.DisplayMessage("TIC-TAC-TOE RULES");
            Console.WriteLine(); Console.WriteLine("***************************************************");

            ConsoleUtil.DisplayMessage("Choose a Player to go first.");
            Console.WriteLine();
            ConsoleUtil.DisplayMessage("The first Player marks a square with an “X”.");
            Console.WriteLine();
            ConsoleUtil.DisplayMessage("The second Player marks a square with an “O”.");
            ConsoleUtil.DisplayMessage("Players continue alternating turns.");
            ConsoleUtil.DisplayMessage("A Player has won when he has");
            ConsoleUtil.DisplayMessage("3 consecutive pieces in a vertical,");
            ConsoleUtil.DisplayMessage("horizontal, or diagonal line.");
            Console.WriteLine();
            Console.WriteLine();

            DisplayContinuePrompt();

        }


        public MenuOption DisplayGetMenuChoice()
        {
            MenuOption playerMenuChoice = MenuOption.None;
            bool usingMenu = true;

            while (usingMenu)
            {
                ConsoleUtil.DisplayReset();
                ConsoleUtil.HeaderText = "Menu Choice";

                Console.CursorVisible = true;

                //Display the Menu//


                Console.WriteLine();
                Console.WriteLine(

                "\t" + "******************************" + Environment.NewLine +
                "\t" + "Menu Choices" + Environment.NewLine +
                "\t" + "******************************" + Environment.NewLine +
                "\t" + "1. Play New Round" + Environment.NewLine +
                "\t" + "2. Game Rules" + Environment.NewLine +
                "\t" + "3. View Current Game Stats" + Environment.NewLine +
                "\t" + "4. View Historic Game Stats" + Environment.NewLine +
                "\t" + "5. Save Game" + Environment.NewLine +
                "\t" + "6. Quit" + Environment.NewLine);

                Console.WriteLine();
                Console.WriteLine();
                ConsoleUtil.DisplayPromptMessage("What would you like to do? (Type Letter)");

                //Get User Response//

                ConsoleKeyInfo userResponse = Console.ReadKey(true);


                switch (userResponse.KeyChar)
                {
                    case '1':
                        playerMenuChoice = MenuOption.PlayNewRound;
                        InputPlayerName();
                        usingMenu = false;
                        break;
                    case '2':
                        playerMenuChoice = MenuOption.GameRules;
                        usingMenu = false;
                        break;
                    case '3':
                        playerMenuChoice = MenuOption.ViewCurrentGameStats;
                        usingMenu = false;
                        break;
                    case '4':
                        playerMenuChoice = MenuOption.ViewHistoricGameStats;
                        usingMenu = false;
                        break;
                    case '5':
                        playerMenuChoice = MenuOption.SaveGameResults;
                        usingMenu = false;
                        break;
                    case '6':
                        playerMenuChoice = MenuOption.Quit;
                        usingMenu = false;
                        break;
                    default:
                        Console.WriteLine("It appears you have selected an incorrect choice.");
                        Console.WriteLine();
                        Console.WriteLine("Press any key to continue or the ESC key to quit the game.");

                        userResponse = Console.ReadKey(true);
                        if (userResponse.Key == ConsoleKey.Escape)
                        {
                            usingMenu = false;
                            Environment.Exit(1);
                        }
                        break;

                }
            }
            Console.CursorVisible = true;

            return playerMenuChoice;
        }

        public void InputPlayerName()
        {
            ConsoleUtil.HeaderText = "Play a New Round";
            ConsoleUtil.DisplayReset();

            //Checks if players already have names
            if (Player1.playerName == null || Player2.playerName == null) { 

                Console.WriteLine("Welcome, You have chosen Play a New Round !!!");
                Console.Write("\nEnter the first player name: ");
                string x = Console.ReadLine();

                //Checks to see if user has named the player
                while (x == "")
                {
                    Console.WriteLine("\nSorry, but the name must contain something. Please try again.");
                    x = Console.ReadLine();
                }
                Player1.playerName = x;
                Console.Write("\nEnter the second player name: ");

                x = Console.ReadLine();

                //Checks to see if user has named the player
                while (x == "")
                {
                    Console.WriteLine("\nSorry, but the name must contain something. Please try again.");
                    x = Console.ReadLine();
                }
                Player2.playerName = x;
                Console.WriteLine("\nChoose the starting player: ");
                x = Console.ReadLine();

                //Checks to see if there is a player with that name.
                while (x != Player1.playerName && x != Player2.playerName) {
                    Console.WriteLine("\nSorry, but the name you chose to start did not match any of the names given.");
                    Console.WriteLine("\nPlease try again.");
                    x = Console.ReadLine();
                }

                //Gives the player their game piece
                if(Player1.playerName == x)
                {
                    Player1.startingFirst = true;
                    Player1.GamePiece = "X";
                    Player2.GamePiece = "O";


                } else if (Player2.playerName == x)
                {
                    Player2.startingFirst = true;
                    Player2.GamePiece = "X";
                    Player1.GamePiece = "O";
                }
            } else
            {
                //Message displayed if players already have names.
                Console.WriteLine("\nThe Players are already named. Thank you for playing again!");
                Console.WriteLine("\nIf you would like to rename the players, please restart the ");
                Console.WriteLine("\napplication.\n");
            }


            Console.WriteLine("\nThank you {0} and {1}", Player1.playerName, Player2.playerName + "." + " Enjoy the game!!!");
            Console.WriteLine("\nPress any key to continue");
            Console.ReadLine();
        }

    }
}

#endregion
