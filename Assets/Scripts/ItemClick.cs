using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemClick : MonoBehaviour, IPointerClickHandler
{
    private GameObject parentGameObject;
    public bool clicked;
    
    void Start()
    {
        parentGameObject = GameObject.FindGameObjectWithTag("AnsPanel");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (clicked) {
            transform.SetParent(parentGameObject.transform);
        }
    }
}