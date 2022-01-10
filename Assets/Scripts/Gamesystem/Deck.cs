using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DAE.HexSystem;
using System.Linq;


namespace DAE.Gamesystem
{
    

    public class Deck : MonoBehaviour, IDeck<CardData>
    {
        //[SerializeField]
        //private int _maxdecksize;

        [SerializeField]
        private List<CardData> _currentDeckList;

        [SerializeField]
        private List<CardData> _startingDeckList;
        
        private List<CardData> _temporaryCardList;

        [SerializeField]
        private List<CardData> _playerhandList;

        [SerializeField]
        private List<CardData> _discardList;

        [SerializeField]
        private GameObject CardBase;

        public GameObject HandView;


        //shuffle shit, generate new deck etc
        //public int DeckSize => _decksize;
        public List<CardData> CurrentDeckList => _currentDeckList;
        public List<CardData> StartingDecklist => _startingDeckList;      

        public List<CardData> TemporaryCardsList => _temporaryCardList;

        public List<CardData> PlayerHandList => _playerhandList;

        public List<CardData> DiscardList => _discardList;

        public void DrawCard()
        {
            _playerhandList.Add(CurrentDeckList[0]);
            var card =  Instantiate(CardBase, HandView.transform);
            card.GetComponent<Card>().InitializeCard(CurrentDeckList[0]);
            CurrentDeckList.RemoveAt(0);
            //card.initialize;
        }

        public void EqualizeDecks()        
        {         
            CurrentDeckList.AddRange(StartingDecklist);
        }
        public List<CardData> ShuffleCurrentDeck()
        {
            return _currentDeckList.OrderBy(x => Random.value).ToList();
        }

        //for prototype im working on
        public List<CardData> ShuffleStartingDeck()
        {
            return _startingDeckList.OrderBy(x => Random.value).ToList();
        }

        internal void AddToDiscard(CardData cardData)
        {
            _discardList.Add(cardData);
        }
        
        internal void RemoveFromHand(CardData cardData)
        {
            _playerhandList.Remove(cardData);
        }

        public void CardBackWard()
        {
            _playerhandList.Add(_discardList[0]);
            _currentDeckList.Add(_playerhandList[0]);

            var card = Instantiate(CardBase, HandView.transform);
            card.GetComponent<Card>().InitializeCard(_discardList[0]);

            _discardList.RemoveAt(_discardList.Count-1);
            _playerhandList.RemoveAt(_playerhandList.Count - 1);
        }


        public void CardForward()
        {
            _playerhandList.Add(CurrentDeckList[0]);
            _discardList.Add(PlayerHandList[0]);

            var card = Instantiate(CardBase, HandView.transform);
            card.GetComponent<Card>().InitializeCard(CurrentDeckList[0]);               

            _currentDeckList.RemoveAt(_currentDeckList.Count -1);
            _playerhandList.RemoveAt(_playerhandList.Count - 1);
        }

    }


}

