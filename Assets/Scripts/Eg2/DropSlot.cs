using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Example2
{

    public class DropSlot : MonoBehaviour, IDropHandler
    {
        public GameObject item;
        
        public void OnDrop(PointerEventData eventData)
        {
            if (transform.childCount==0)
            {
                item = DragHandler.itemDragging;
                item.transform.SetParent(transform);
                item.transform.position = transform.position;
            }
        }
    }
}
