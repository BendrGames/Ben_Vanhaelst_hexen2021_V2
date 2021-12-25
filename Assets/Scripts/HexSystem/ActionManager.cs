using DAE.BoardSystem;
using DAE.HexSystem.Actions;
using DAE.Commons;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DAE.HexSystem
{
    public class ActionManager<TCard,TPiece> where TPiece : IPiece where TCard : ICard
    {
        //private MultiValueDictionary<CardType, ICheckPosition> _actions = new MultiValueDictionary<CardType, ICheckPosition>();
        private MultiValueDictionary<CardType, ICheckPosition<TCard, TPiece>> _actions = new MultiValueDictionary<CardType, ICheckPosition<TCard, TPiece>>();
        private readonly Board<IHex, TPiece> _board;
        private readonly Grid<IHex> _grid;

        public ActionManager(Board<IHex, TPiece> board, Grid<IHex> grid)
        {
            //cardtype?
            _board = board;
            _grid = grid;

            InitializeMoves();
        }

        

        private void InitializeMoves()
        {
            
            _actions.Add(CardType.Beam, new LaserBeamAction<TCard, TPiece>());

            //_actions.Add(CardType.Thunderclap, new ThunderClapAction<TCard, TPiece>());

            //_actions.Add(CardType.Teleport, new TeleportAction<TCard, TPiece>());

            //_actions.Add(CardType.Cleave, new CleaveAction<TCard, TPiece>());           


            //_actions.Add(CardType.Cleave, new ConfigurableAction<TCard, TPiece>((b, g, pos, p, c)
            //=> new ActionHelper<TCard, TPiece>(b, g, pos, p, c)
            //                            .Direction0(1)
            //                            .Direction1(1)
            //                            .Direction2(1)
            //                            .Collect()));                  

        }

        //public List<Position> ValidPisitionsFor(TPiece piece, CardType card)
        //{
        //    return _actions[card]
        //        .Where(m => m.CanExecute(_board, _grid, piece))
        //        .SelectMany(m => m.Positions(_board, _grid, piece, position))
        //        .ToList();
        //}

        public List<IHex> ValidPisitionsFor(TPiece piece, IHex position, CardType cardType)
        {
            return _actions[cardType]
                .Where(m => m.CanExecute(_board, _grid, position, piece, cardType))
                .SelectMany(m => m.ValidPositionsCalc(_board, _grid, position, piece, cardType)/*.Contains(position)*/)
                .ToList();
        }

        public void Action(TPiece piece, IHex position, CardType cardType)
        {
            _actions[cardType]
            .Where(m => m.CanExecute(_board, _grid, position, piece, cardType))
            .First(m => m.ValidPositionsCalc(_board, _grid, position, piece, cardType).Contains(position))
            .ExecuteAction(_board, _grid, position, piece, cardType);
        }
       
    }
    }