using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Example3;
using TMPro;

namespace Type24
{
    /// <summary>
    /// Example of control application for drag and drop events handle
    /// </summary>
    public class Controller : MonoBehaviour
    {
        public MatchNumber matchNumber;
        
        //String[] correct ={"Star","Cup","Winner","Blue Star"};
        /// <summary>
        /// Operate all drag and drop requests and events from children cells
        /// </summary>
        /// <param name="desc"> request or event descriptor </param>
        void OnSimpleDragAndDropEvent(Cell.DropEventDescriptor desc)
        {
            
            // Get control unit of source cell
            Controller sourceSheet = desc.sourceCell.GetComponentInParent<Controller>();
            // Get control unit of destination cell
            Controller destinationSheet = desc.destinationCell.GetComponentInParent<Controller>();

            switch (desc.triggerType) // What type event is?
            {
                case Cell.TriggerType.DropRequest: // Request for item drag (note: do not destroy item on request)
                    //Debug.Log(desc.destinationCell.transform.GetSiblingIndex());
                    Debug.Log(
                        "Request " + desc.item.name + " from " + sourceSheet.name + " to " + destinationSheet.name);
                    if (desc.destinationCell.cellType == Cell.CellType.DropOnly)
                    {
                        if (desc.item.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text !=
                            matchNumber.GetAnswer().ToString())
                        {
                            desc.permission = false;
                        }
                        Debug.Log(
                            "Request " + desc.item.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text + " from " + sourceSheet.name + " to " + destinationSheet.name);
                    }
                    break;
                case Cell.TriggerType.DropEventEnd: // Drop event completed (successful or not)
                    if (desc.permission == true) // If drop successful (was permitted before)
                    {
                        Debug.Log("Successful drop " + desc.item.name + " from " + sourceSheet.name + " to " +
                                  destinationSheet.name);
                        if (desc.destinationCell.cellType == Cell.CellType.DropOnly)
                        {
                            StartCoroutine(NextQuestion(desc));
                        }
                        
                    }
                    else // If drop unsuccessful (was denied before)
                    {
                        Debug.Log("Denied drop " + desc.item.name + " from " + sourceSheet.name + " to " +
                                  destinationSheet.name);
                    }
                    break;
                case Cell.TriggerType.ItemAdded: // New item is added from application
                    Debug.Log("Item " + desc.item.name + " added into " + destinationSheet.name);
                    break;
                case Cell.TriggerType.ItemWillBeDestroyed
                    : // Called before item be destructed (can not be canceled)
                    Debug.Log("Item " + desc.item.name + " will be destroyed from " + sourceSheet.name);
                    break;
                default:
                    Debug.Log("Unknown drag and drop event");
                    break;
            }
        }

        /// <summary>
        /// Add item in first free cell
        /// </summary>
        /// <param name="item"> new item </param>
        public void AddItemInFreeCell(Item item)
        {
            foreach (Cell cell in GetComponentsInChildren<Cell>())
            {
                if (cell != null)
                {
                    if (cell.GetItem() == null)
                    {
                        cell.AddItem(Instantiate(item.gameObject).GetComponent<Item>());
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Remove item from first not empty cell
        /// </summary>
        public void RemoveFirstItem()
        {
            foreach (Cell cell in GetComponentsInChildren<Cell>())
            {
                if (cell != null)
                {
                    if (cell.GetItem() != null)
                    {
                        cell.RemoveItem();
                        break;
                    }
                }
            }
        }
        IEnumerator NextQuestion(Cell.DropEventDescriptor desc){
            yield return new WaitForSeconds(1.5f);
            StartCoroutine(matchNumber.SetUpQuestion());
            desc.sourceCell.GetComponent<Cell>().AddItem(desc.destinationCell.GetComponent<Cell>().GetItem());
            desc.sourceCell.GetComponent<Cell>().GetItem().transform.gameObject.AddComponent<Item>();
            //desc.destinationCell.GetComponent<Cell>().RemoveItem();
            Destroy(desc.destinationCell.GetComponent<Cell>().GetItem());
        }
    }

}
