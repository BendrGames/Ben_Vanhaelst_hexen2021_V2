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

        private Grid<Hex> _grid;
        private Board<Hex, Piece> _board;

        public Piece Player;

        private StateMachine<GameStateBase> _gameStateMachine;

        void Start()
        {
            _positionHelper.TileRadius = Tileradius;
            _generateboard.GenerateBoardView(Rows, Columns, 1, MapShape, _positionHelper, hex, _boardParent);

            _grid = new Grid<Hex>(Rows, Columns);
            ConnectGrid(_grid);
            _board = new Board<Hex, Piece>();
            ConnectPiece(_grid, _board);

            var replayManager = new ReplayManager();

            BoardListereners();
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

        private void ConnectGrid(Grid<Hex> grid)
        {
            var hexes = FindObjectsOfType<Hex>();
            foreach (var hex in hexes)
            {
                
                //hex.Dropped += (s, e) =>
                //{
                //    var validpositions = _actionManager.ValidPisitionsFor(Player, e.Position, CurrentCard._cardType);

                //    if (validpositions.Contains(e.Position))
                //    {
                //        _actionManager.Action(Player, e.Position, CurrentCard._cardType);

                //        DrawCard();
                //    }
                //    else
                //    {
                //        //ReAddCard();
                //    }

                //    foreach (var position in validpositions)
                //    {
                //        position.Deactivate();
                //    }

                //};

                //hex.Entered += (s, e) =>
                //{
                //    var positions = _actionManager.ValidPisitionsFor(Player, e.Position, CurrentCard._cardType);

                //    _currentMousePosition = e.Position;
                //    Debug.Log(_currentMousePosition);

                //    foreach (var position in positions)
                //    {
                //        position.Activate();
                //    }
                //};

                //hex.Exitted += (s, e) =>
                //{
                //    var positions = _actionManager.ValidPisitionsFor(Player, e.Position, CurrentCard._cardType);

                //    foreach (var position in positions)
                //    {
                //        position.Deactivate();
                //    }
                //};

                
                var gridpos = _positionHelper.ToGridPosition(_boardParent, hex.transform.position);
                Debug.Log($"value of tile { hex.name} is X: {(int)gridpos.x} and y: {(int)gridpos.y}");

                grid.Register((int)gridpos.x, (int)gridpos.y, hex);
                Debug.Log($"value of tile { hex.name} is X: {(int)gridpos.x} and y: {(int)gridpos.y}");
                hex.gameObject.name = $"tile {(int)gridpos.x},{(int)gridpos.y}";
            }
        }

        private void ConnectPiece(Grid<Hex> grid, Board<Hex, Piece> board)
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
