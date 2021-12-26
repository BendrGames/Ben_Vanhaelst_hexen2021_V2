using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DAE.HexSystem;
using System.Linq;


namespace DAE.Gamesystem
{
    

    public class Deck : MonoBehaviour, IDeck<Card>
    {
        //[SerializeField]
        //private int _maxdecksize;

        [SerializeField]
        private List<Card> _currentDeckList;

        [SerializeField]
        private List<Card> _startingDeckList;
        
        private List<Card> _temporaryCardList;
      


        //shuffle shit, generate new deck etc
        //public int DeckSize => _decksize;
        public List<Card> CurrentDeckList => _currentDeckList;
        public List<Card> StartingDecklist => _startingDeckList;
        public List<Card> CardList => _temporaryCardList;

        public List<Card> TemporaryCardsList => throw new System.NotImplementedException();

        public void EqualizeDecks()        
        {         
            CurrentDeckList.AddRange(StartingDecklist);
        }
        public List<Card> ShuffleCurrentDeck()
        {
            return _currentDeckList.OrderBy(x => Random.value).ToList();
        }

        //for prototype im working on
        public List<Card> ShuffleStartingDeck()
        {
            return _startingDeckList.OrderBy(x => Random.value).ToList();
        }


        public void AddCardRandom(Card newCard)
        {
            _currentDeckList.Insert(Random.Range(0, _currentDeckList.Count), newCard);
            _startingDeckList.Insert(Random.Range(0, _startingDeckList.Count), newCard);
        }

        public void AddCardToCurrentDeckRandom(Card newCard)
        {
            _currentDeckList.Insert(Random.Range(0, _currentDeckList.Count), newCard);            
        }

        public void AddCardToStartingDeckRandom(Card newCard)
        {           
            _startingDeckList.Insert(Random.Range(0, _startingDeckList.Count), newCard);
        }

        public void AddCardTop(Card newCard)
        {
            _currentDeckList.Insert(Random.Range(0, _currentDeckList.Count), newCard);
            _startingDeckList.Insert(Random.Range(0, _startingDeckList.Count), newCard);
        }

        public void AddCardToCurrentDeckTop(Card newCard)
        {
            _currentDeckList.Insert(0, newCard);            
        }

        public void AddCardToStartingDeckTop(Card newCard)
        {
            _startingDeckList.Insert(0, newCard);
        }

        public void AddCardBottom(Card newCard)
        {
            _currentDeckList.Insert(_startingDeckList.Count - 1, newCard);
            _startingDeckList.Insert(_startingDeckList.Count - 1, newCard);
        }

        public void AddCardToCurrentDeckBottom(Card newCard)
        {
            _currentDeckList.Insert(_startingDeckList.Count - 1, newCard);            
        }

        public void AddCardToStartingDeckBottom(Card newCard)
        {            
            _startingDeckList.Insert(_startingDeckList.Count - 1, newCard);
        }

        public void RemoveCard(Card toRemove)
        {
            if (_currentDeckList.Contains(toRemove))
            {
                _currentDeckList.Remove(toRemove);
            }

            if (_startingDeckList.Contains(toRemove))
            {
                _startingDeckList.Remove(toRemove);
            }
        }

        public void RemoveCardFromCurrentDeck(Card toRemove)
        {
            if (_currentDeckList.Contains(toRemove))
            {
                _currentDeckList.Remove(toRemove);
            }           
        }

        public void RemoveCardFromStartingDeck(Card toRemove)
        {        

            if (_startingDeckList.Contains(toRemove))
            {
                _startingDeckList.Remove(toRemove);
            }
        }
    }


}

