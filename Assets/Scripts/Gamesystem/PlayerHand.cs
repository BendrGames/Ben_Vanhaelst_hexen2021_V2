
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using DAE.HexSystem;

namespace DAE.Gamesystem
{


    public class PlayerHand : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler, IHand<Card>
    {
        private Deck _playerDeck;
        public GameObject HandView;

        //private int _handsize;
        public List<Card> _playerHandCardList;

        //public int Handsize => _handsize;
        public List<Card> PlayerHandCardList => _playerHandCardList;

        public void InitializePlayerHand(Deck playerDeck)
        {
            _playerDeck = playerDeck;
            
            _playerDeck.EqualizeDecks();
            _playerDeck.ShuffleCurrentDeck();

            //_handsize = handsize;

            
        }

        public Card Drawcard()
        {            
            _playerHandCardList.Add(_playerDeck.CurrentDeckList[0]);
            _playerDeck.CurrentDeckList.RemoveAt(0);
            var card = Instantiate(_playerHandCardList[_playerHandCardList.Count - 1], HandView.transform);

            return card;
        }
                

        public void PlayCard()
        {

        }

        public void DiscardCard(Card currentcard)
        {
            _playerHandCardList.Remove(currentcard);

            //discardpile.add
            
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            //Debug.Log("OnPointerEnter");
            if (eventData.pointerDrag == null)
                return;

            Card d = eventData.pointerDrag.GetComponent<Card>();
            if (d != null)
            {
                d.placeholderParent = this.transform;
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            //Debug.Log("OnPointerExit");
            if (eventData.pointerDrag == null)
                return;

            Card d = eventData.pointerDrag.GetComponent<Card>();
            if (d != null && d.placeholderParent == this.transform)
            {
                d.placeholderParent = d.parentToReturnTo;
            }
        }

        public void OnDrop(PointerEventData eventData)
        {
            Debug.Log(eventData.pointerDrag.name + " was dropped on " + gameObject.name);

            Card d = eventData.pointerDrag.GetComponent<Card>();
            if (d != null)
            {
                d.parentToReturnTo = this.transform;
            }
        }

    }
}
