using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Example2
{

    public class ItemPool : MonoBehaviour, IDropHandler
    {
        public void OnDrop(PointerEventData eventData)
        {
            DragHandler.itemDragging.transform.SetParent(transform);
        }
    }
}
