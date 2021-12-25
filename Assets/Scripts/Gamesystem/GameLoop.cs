using DAE.BoardSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using DAE.HexSystem;
using DAE.StateSystem;
using DAE.GameSystem.GameStates;
using DAE.ReplaySystem;
namespace DAE.Gamesystem
{
    public class GameLoop : MonoBehaviour
    {
        [SerializeField]
        private PositionHelper _positionHelper;
        [SerializeField]
        private GenerateBoard _generateboard;
        [SerializeField]
        private Transform _boardParent;

        [Header("GenerateBoard")]
        public int Rows = 8;    
        public int Columns = 8;
        public int Tileradius = 1;
        public GameObject hex;
        public GenerationShapes MapShape = GenerationShapes.Hexagon;

        public Deck _deckview;
        public PlayerHand _playerhand;
        private ActionManager<Card, Piece> _actionManager;        

        public Card _currentCard;

        private Grid<IHex> _grid;
        private Board<IHex, Piece> _board;

        public Piece Player;

        private StateMachine<GameStateBase> _gameStateMachine;

        void Start()
        {
            _positionHelper.TileRadius = Tileradius;
            _generateboard.GenerateBoardView(Rows, Columns, 1, MapShape, _positionHelper, hex, _boardParent);

            _grid = new Grid<IHex>(Rows, Columns);
            ConnectGrid(_grid);
            _board = new Board<IHex, Piece>();
            ConnectPiece(_grid, _board);
            _actionManager = new ActionManager<Card, Piece>(_board, _grid);
            var replayManager = new ReplayManager();

            _playerhand.InitializePlayerHand(_deckview, 5);
            DrawCard();
            DrawCard();
            DrawCard();
            DrawCard();
            DrawCard();

            BoardListereners();
        }

       

        private void DrawCard()
        {
            var card = _playerhand.Drawcard();
            card.BeginDrag += (s, e) =>
            {
                _currentCard = e.Card;
            };
        }


        private void BoardListereners()
        {
            _board.moved += (s, e) =>
            {
                if (_grid.TryGetCoordinateOf(e.ToPosition, out var toCoordinate))
                {
                    var worldPosition = _positionHelper.ToWorldPosition(_boardParent, toCoordinate.x, toCoordinate.y);

                    e.Piece.MoveTo(worldPosition);
                }
            };

            _board.placed += (s, e) =>
            {

                if (_grid.TryGetCoordinateOf(e.ToPosition, out var toCoordinate))
                {
                    var worldPosition = _positionHelper.ToWorldPosition(_boardParent, toCoordinate.x, toCoordinate.y);


                    e.Piece.Place(worldPosition);
                }

            };

            _board.taken += (s, e) =>
            {
                e.Piece.Taken();
            };
        }

        private void ConnectGrid(Grid<IHex> grid)
        {
            var hexes = FindObjectsOfType<Hex>();
            foreach (var hex in hexes)
            {

                hex.Dropped += (s, e) =>
                {
                    var validpositions = _actionManager.ValidPisitionsFor(Player, e.Position, _currentCard._cardType);

                    if (validpositions.Contains(e.Position))
                    {
                        _actionManager.Action(Player, e.Position, _currentCard._cardType);

                        DrawCard();
                    }
                    else
                    {
                        //ReAddCard();
                    }

                    foreach (var position in validpositions)
                    {
                        position.Deactivate();
                    }

                };

                hex.Entered += (s, e) =>
                {
                    var positions = _actionManager.ValidPisitionsFor(Player, e.Position, _currentCard._cardType);

                    //_currentMousePosition = e.Position;
                    //Debug.Log(_currentMousePosition);

                    foreach (var position in positions)
                    {
                        position.Activate();
                    }
                };

                hex.Exitted += (s, e) =>
                {
                    var positions = _actionManager.ValidPisitionsFor(Player, e.Position, _currentCard._cardType);

                    foreach (var position in positions)
                    {
                        position.Deactivate();
                    }
                };


                var gridpos = _positionHelper.ToGridPosition(_boardParent, hex.transform.position);
                
                grid.Register((int)gridpos.x, (int)gridpos.y, hex);
               
                hex.gameObject.name = $"tile {(int)gridpos.x},{(int)gridpos.y}";
            }
        }

        private void ConnectPiece(Grid<IHex> grid, Board<IHex, Piece> board)
        {
                var pieces = FindObjectsOfType<Piece>();
                foreach (var piece in pieces)
                {
                var gridpos = _positionHelper.ToGridPosition(_boardParent, piece.transform.position);
                    if (grid.TryGetPositionAt((int)gridpos.x, (int)gridpos.y, out var position))
                    {
                        Debug.Log("registered");


                        board.Place(piece, position);
                    }
                }
            }

      
    }

}
