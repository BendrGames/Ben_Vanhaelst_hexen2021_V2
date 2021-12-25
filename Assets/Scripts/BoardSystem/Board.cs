﻿using DAE.Commons;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace DAE.BoardSystem
{
    public class PlacedEventArgs<TPosition, TPiece> : EventArgs
    {
        public TPosition ToPosition { get; }

        public TPiece Piece { get; }

        public PlacedEventArgs(TPosition toPosition, TPiece piece)
        {
            ToPosition = toPosition;
            Piece = piece;
        }
    }

    public class MovedEventArgs<TPosition, TPiece> : EventArgs
    {
        public TPosition ToPosition { get; }
        public TPosition FromPosition { get; }
        public TPiece Piece { get; }

        public MovedEventArgs(TPosition toPosition, TPosition fromPosition, TPiece piece)
        {
            ToPosition = toPosition;
            FromPosition = fromPosition;
            Piece = piece;
        }
    }

    public class TakenEventArgs<TPosition, TPiece> : EventArgs
    {
        public TPosition FromPosition { get; }
        public TPiece Piece { get; }

        public TakenEventArgs(TPosition fromPosition, TPiece piece)
        {
            FromPosition = fromPosition;
            Piece = piece;
        }
    }


    public class Board<TPosition, TPiece>
    {

        private BidirectionalDictionary<TPosition, TPiece> _positionPiece = new BidirectionalDictionary<TPosition, TPiece>();


        public bool Place(TPiece piece, TPosition toPosition)
        {
            if (TryGetPieceAt(toPosition, out _))
                return false;

            if (TryGetPositionOf(piece, out _))
                return false;

            _positionPiece.Add(toPosition, piece);
            OnPlaced(new PlacedEventArgs<TPosition, TPiece>(toPosition, piece));

            return true;

        }

        public bool Move(TPiece piece, TPosition toPosition)
        {
            if (TryGetPieceAt(toPosition, out _))
                return false;

            if (!TryGetPositionOf(piece, out var fromPosition))
                return false;

            if (!_positionPiece.Remove(piece))
                return false;

            _positionPiece.Add(toPosition, piece);

            OnMoved(new MovedEventArgs<TPosition, TPiece>(toPosition, fromPosition, piece));

            return true;
        }

        public bool Take(TPiece piece)
        {
            if (!TryGetPositionOf(piece, out var fromPosition))
                return false;

            if (!_positionPiece.Remove(piece))
                return false;

            OnTaken(new TakenEventArgs<TPosition, TPiece>(fromPosition, piece));
            return true;
        }


        public bool TryGetPieceAt(TPosition position, out TPiece piece) => _positionPiece.TryGetValue(position, out piece);

        public bool TryGetPositionOf(TPiece piece, out TPosition position) => _positionPiece.TryGetKey(piece, out position);


        #region events

        public event EventHandler<PlacedEventArgs<TPosition, TPiece>> placed;
        public event EventHandler<MovedEventArgs<TPosition, TPiece>> moved;
        public event EventHandler<TakenEventArgs<TPosition, TPiece>> taken;

        protected virtual void OnPlaced(PlacedEventArgs<TPosition, TPiece> eventargs)
        {
            var handlers = placed;
            handlers?.Invoke(this, eventargs);
        }
        protected virtual void OnMoved(MovedEventArgs<TPosition, TPiece> eventargs)
        {
            var handlers = moved;
            handlers?.Invoke(this, eventargs);
        }
        protected virtual void OnTaken(TakenEventArgs<TPosition, TPiece> eventargs)
        {
            var handlers = taken;
            handlers?.Invoke(this, eventargs);
        }


        #endregion
    }
}
