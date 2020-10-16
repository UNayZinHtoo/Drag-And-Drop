using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Example3

{
    public class ItemPool : MonoBehaviour, IDropHandler
    {
        public void OnDrop(PointerEventData eventData)
        {
            DragHandler.itemDragging.transform.SetParent(transform);
        }
    }
}
