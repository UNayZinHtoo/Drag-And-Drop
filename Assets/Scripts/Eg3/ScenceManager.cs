using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Example3
{
    public class ScenceManager : MonoBehaviour
    {
        public static ScenceManager Instance = null;
        
        public Sprite[] itemSptite;

        public String[] slotText;

        public GameObject dropSlotPrefab;
        
        public GameObject dragItemPrefab;

        public Transform poolTransform;

        public Transform slotTransform;
        
        
        // Start is called before the first frame update
        void Start()
        {
            Instance = this;
            PrepareQuestion();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                SceneManager.LoadScene("MainScene");
        }
        
        public void CheckAnswer()
        {
            foreach (Transform child in slotTransform)
            {
                Destroy(child.gameObject);
            }
            foreach (Transform child in poolTransform)
            {
                Destroy(child.gameObject);
            }
            PrepareQuestion();
        }
        public void PrepareQuestion()
        {
            foreach (var item in slotText)
            {
                GameObject slot = Instantiate(dropSlotPrefab, transform.position, Quaternion.identity);
                slot.transform.SetParent (slotTransform, false);
            }
            foreach (var item in itemSptite)
            {
                GameObject itemPool = Instantiate(dragItemPrefab, poolTransform.transform.position, Quaternion.identity);
                itemPool.transform.SetParent (poolTransform, false);
                itemPool.GetComponent<Image>().sprite = item;
            }
            
        }
    }
}
