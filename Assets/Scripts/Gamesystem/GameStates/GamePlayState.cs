using DAE.BoardSystem;
using DAE.Gamesystem;
using DAE.HexSystem;
using DAE.StateSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAE.GameSystem.GameStates
{
    class GamePlayState : GameStateBase
    {
       
        private ActionManager<Card, Piece> _actionManager;
        private Board<IHex, Piece> _board;
        private PlayerHand _playerHand;
        private Card _currentCard;
        private Deck _deck;

        public GamePlayState(StateMachine<GameStateBase> stateMachine, Board<IHex, Piece> board, ActionManager<Card, Piece> moveManager, PlayerHand playerhand, Deck deck) : base(stateMachine)
        {
            _playerHand = playerhand;
            _deck = deck;
            _actionManager = moveManager;
            _board = board;

            _playerHand.InitializePlayerHand(_deck);
            DrawCard();
            DrawCard();
            DrawCard();
            DrawCard();
            DrawCard();

        }

        private void DrawCard()
        {
            var card = _playerHand.Drawcard();
            card.BeginDrag += (s, e) =>
            {
                _currentCard = e.Card;
            };
        }

        internal override void Backward()
        {
            StateMachine.MoveToState(GameState.ReplayState);
        }

        internal override void HighLightNew(Piece piece, Hex position)
        {
            var validpositions = _actionManager.ValidPisitionsFor(piece, position, _currentCard._cardType);
            var IsolatedPositions = _actionManager.IsolatedValidPisitionsFor(piece, position, _currentCard._cardType);

            if (!validpositions.Contains(position))
            {
                foreach (var hex in validpositions)
                {
                    hex.Activate();
                }
            }

            if (IsolatedPositions.Contains(position))
            {
                foreach (var hex in IsolatedPositions)
                {
                    hex.Activate();
                }
            };
        }

        internal override void UnHighlightOld(Piece piece, Hex position)
        {
            var validpositions = _actionManager.ValidPisitionsFor(piece, position, _currentCard._cardType);
            var IsolatedPositions = _actionManager.IsolatedValidPisitionsFor(piece, position, _currentCard._cardType);

            foreach (var hex in validpositions)
            {
                hex.Deactivate();
            }

            foreach (var hex in IsolatedPositions)
            {
                position.Deactivate();
            }
        }

        internal override void OnDrop(Piece piece, Hex position)
        {
            var validpositions = _actionManager.ValidPisitionsFor(piece, position, _currentCard._cardType);
            var IsolatedPositions = _actionManager.IsolatedValidPisitionsFor(piece, position, _currentCard._cardType);

            if (IsolatedPositions.Contains(position))
            {
                _actionManager.Action(piece, position, _currentCard._cardType);

                _currentCard.Used();
                DrawCard();
            }

            foreach (var hex in validpositions)
            {
                hex.Deactivate();
            }
        }
    }

}
