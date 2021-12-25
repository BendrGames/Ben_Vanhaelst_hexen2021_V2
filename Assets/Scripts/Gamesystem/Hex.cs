using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using DAE.HexSystem;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace DAE.Gamesystem
{
    public class PositionEventArgs : EventArgs
    {
        public IHex Position { get; }


        public PositionEventArgs(IHex position)
        {
            Position = position;
        }
    }


    public class Hex : MonoBehaviour, IHex , IDropHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public event EventHandler<PositionEventArgs> Dropped;
        public event EventHandler<PositionEventArgs> Entered;
        public event EventHandler<PositionEventArgs> Exitted;

        [SerializeField] private UnityEvent OnActivate;
        [SerializeField] private UnityEvent Ondeactivate;

       
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (eventData.pointerDrag == null)
                return;
            Debug.Log("OnPointerEnter");


            //Card d = eventData.pointerDrag.GetComponent<Card>();
            //if (d != null)
            //{
            //    d.placeholderParent = this.transform;
            //}

            OnEntered(new PositionEventArgs(this));

            //highlight tiles A groep
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (eventData.pointerDrag == null)
                return;
            Debug.Log("OnPointerExit");



            //Card d = eventData.pointerDrag.GetComponent<Card>();
            //if (d != null && d.placeholderParent == this.transform)
            //{
            //    d.placeholderParent = d.parentToReturnTo;
            //}

            OnExited(new PositionEventArgs(this));
            //highlight tiles  b goep
        }

        public void OnDrop(PointerEventData eventData)
        {
            Debug.Log(eventData.pointerDrag.name + " was dropped on " + gameObject.name);
            //Card d = eventData.pointerDrag.GetComponent<Card>();
            //if (d != null)
            //{
            //    d.parentToReturnTo = d.parentToReturnTo;
            //}

            OnDropped(new PositionEventArgs(this));

            //Destroy(eventData.pointerDrag.gameObject);


        }

        protected virtual void OnDropped(PositionEventArgs eventargs)
        {
            var handler = Dropped;
            handler?.Invoke(this, eventargs);
        }

        protected virtual void OnEntered(PositionEventArgs eventargs)
        {
            var handler = Entered;
            handler?.Invoke(this, eventargs);
        }

        protected virtual void OnExited(PositionEventArgs eventargs)
        {
            var handler = Exitted;
            handler?.Invoke(this, eventargs);
        }

    


        public void Activate()
        {
            OnActivate.Invoke();
        }

        public void Deactivate()
        {
            Ondeactivate.Invoke();
        }


    }
}
