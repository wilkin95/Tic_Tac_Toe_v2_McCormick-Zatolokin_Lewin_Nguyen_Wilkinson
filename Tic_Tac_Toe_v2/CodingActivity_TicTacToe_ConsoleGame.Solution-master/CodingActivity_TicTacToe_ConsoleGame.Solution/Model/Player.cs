using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingActivity_TicTacToe_ConsoleGame
{
    class Player
    {
        #region Feilds

        private string _playerName;

        private int _wins;

        private int _loses;

        private bool _starting;

        private string _gamePiece;

        #endregion

        #region Properties

        public string playerName
        {
            get { return _playerName; }
            set { _playerName = value; }
        }

        public int currentWins
        {
            get { return _wins; }
            set { _wins = value; }
        }

        public int currentLoses
        {
            get { return _loses; }
            set { _loses = value; }
        }

        public bool startingFirst
        {
            get { return _starting; }
            set { _starting = value; }
        }

        public string GamePiece
        {
            get { return _gamePiece; }
            set { _gamePiece = value; }
        }

        #endregion

        #region Constructors

        public Player ()
        {

        }

        public Player(string name)
        {
            playerName = name;
        }

        public Player(string name, int wins, int loses)
        {
            playerName = name;
            currentWins = wins;
            currentLoses = loses;
        }

        public Player (bool starting)
        {
            startingFirst = starting;
        }
        

        #endregion

        #region Methods

        #endregion
    }
}
