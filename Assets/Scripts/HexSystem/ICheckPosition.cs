﻿using DAE.BoardSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAE.HexSystem
{
    interface ICheckPosition<TCard, TPiece> where TPiece : IPiece where TCard : ICard

    {
        
        bool CanExecute(Board<IHex, TPiece> board, Grid<IHex> grid, IHex position, TPiece piece, CardType card);

        void ExecuteAction(Board<IHex, TPiece> board, Grid<IHex> grid, IHex position, TPiece piece, CardType card);

        //void ExecuteAttack(Board<Position, TPiece> board, Grid<Position> grid, TPiece piece, Position position);

        List<IHex> ValidPositionsCalc(Board<IHex, TPiece> board, Grid<IHex> grid, IHex position, TPiece piece, CardType card);



        //bool CanExecute(Board<Position, ICard> board, Grid<Position> grid, ICard piece);

        //void ExecuteMove(Board<Position, ICard> board, Grid<Position> grid, ICard piece, Position position);

        //void ExecuteAttack(Board<Position, ICard> board, Grid<Position> grid, ICard piece, Position position);

        ////SpawnObject
        //List<Position> Positions(Board<Position, ICard> board, Grid<Position> grid, ICard piece);
    }
}
