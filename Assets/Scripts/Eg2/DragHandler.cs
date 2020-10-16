using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Example2
{

    public class DragHandler : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
    {
        public static GameObject itemDragging;

        private Vector3 startPosition;

        private Transform startParent;

        private Transform dragParent;

        // Start is called before the first frame update
        void Start()
        {
            dragParent = GameObject.FindGameObjectWithTag("DragParent").transform;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OnDrag(PointerEventData eventData)
        {
            Debug.Log("OnDrag");
            transform.position = Input.mousePosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Debug.Log("OnEndDrag");

            itemDragging = null;
            if (transform.parent == dragParent)
            {
                transform.position = startPosition;
                transform.SetParent(startParent);
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            Debug.Log("OnBeginDrag");

            itemDragging = gameObject;

            startPosition = transform.position;
            startParent = transform.parent;
            transform.SetParent(dragParent);
        }
    }
}
