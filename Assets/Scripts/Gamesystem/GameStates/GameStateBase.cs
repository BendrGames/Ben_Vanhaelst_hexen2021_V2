using DAE.Gamesystem;

using DAE.StateSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAE.GameSystem.GameStates
{
    class GameStateBase : IState<GameStateBase>
    {
        public StateMachine<GameStateBase> StateMachine { get; }

        public GameStateBase(StateMachine<GameStateBase> stateMachine)
        {
            StateMachine = stateMachine;
        }

        public virtual void OnEnter()
        {
        }

        public virtual void OnExit()
        {
        }

        internal virtual void HighLightNew(Piece piece, Hex position)
        {

        }

        internal virtual void UnHighlightOld(Piece piece, Hex position)
        {

        }

        internal virtual void OnDrop(Piece piece, Hex position)
        {

        }

        internal virtual void BeginDrag(Card card)
        {

        }


        internal virtual void Forward()
        {

        }

        internal virtual void Backward()
        {

        }

    }
}
