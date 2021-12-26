using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAE.HexSystem
{
    public interface IDeck<TCard>
    {        
        public List<TCard> CurrentDeckList {get;}
        public List<TCard> StartingDecklist { get; }
        public List<TCard> TemporaryCardsList { get; }

        //discardPile


        public void EqualizeDecks();
        public List<TCard> ShuffleCurrentDeck();

        //for prototype im working on

        //AddFromDiscardPile
        //ClearDiscardPile
        //removeFromDiscardPile (also startingdeck) 

        public List<TCard> ShuffleStartingDeck();

        public void AddCardRandom(TCard newCard);
        public void AddCardToCurrentDeckRandom(TCard newCard);
        public void AddCardToStartingDeckRandom(TCard newCard);

        public void AddCardTop(TCard newCard);
        public void AddCardToCurrentDeckTop(TCard newCard);
        public void AddCardToStartingDeckTop(TCard newCard);

        public void AddCardBottom(TCard newCard);
        public void AddCardToCurrentDeckBottom(TCard newCard);
        public void AddCardToStartingDeckBottom(TCard newCard);

        public void RemoveCard(TCard toRemove);
        public void RemoveCardFromCurrentDeck(TCard toRemove);      


    }
}
