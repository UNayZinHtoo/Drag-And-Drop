﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Example3
{

    public class DropSlot : MonoBehaviour, IDropHandler
    {
        public GameObject item;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            /*if (item != null && item.transform.parent != transform)
            {
                item = null;
            }*/
        }

        public void OnDrop(PointerEventData eventData)
        {
            /*if (!item)
            {
                item = DragHandler.itemDragging;
                item.transform.SetParent(transform);
                item.transform.position = transform.position;
            }*/
            if (transform.childCount==0)
            {
                item = DragHandler.itemDragging;
                item.transform.SetParent(transform);
                item.transform.position = transform.position;
            }
        }
    }
}
