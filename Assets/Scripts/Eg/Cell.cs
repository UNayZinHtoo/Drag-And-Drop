using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

namespace Eg
{
    /// <summary>
    /// Every item's cell must contain this script
    /// </summary>
    [RequireComponent(typeof(Image))]
    public class Cell : MonoBehaviour, IDropHandler
    {
        public enum CellType // Cell types
        {
            DropOnly, // Item will be dropped into cell
            DragOnly // Item will be dragged from this cell
        }

        public enum TriggerType // Types of drag and drop events
        {
            DropRequest, // Request for item drop from one cell to another
            DropEventEnd
        }

        public class DropEventDescriptor // Info about item's drop event
        {
            public TriggerType triggerType; // Type of drag and drop trigger
            public Cell sourceCell; // From this cell item was dragged
            public Cell destinationCell; // Into this cell item was dropped
            public Item item; // Dropped item
            public bool permission; // Decision need to be made on request
        }

        [Tooltip("Functional type of this cell")]
        public CellType cellType = CellType.DropOnly; // Special type of this cell

        private Item myDadItem; // Item of this DaD cell

        void OnEnable()
        {
            Item.OnItemDragStartEvent += OnAnyItemDragStart; // Handle any item drag start
            Item.OnItemDragEndEvent += OnAnyItemDragEnd; // Handle any item drag end
            UpdateMyItem();
        }

        void OnDisable()
        {
            Item.OnItemDragStartEvent -= OnAnyItemDragStart;
            Item.OnItemDragEndEvent -= OnAnyItemDragEnd;
            StopAllCoroutines(); // Stop all coroutines if there is any
        }

        /// <summary>
        /// On any item drag start need to disable all items raycast for correct drop operation
        /// </summary>
        /// <param name="item"> dragged item </param>
        private void OnAnyItemDragStart(Item item)
        {
            UpdateMyItem();
            if (myDadItem != null)
            {
                myDadItem.MakeRaycast(false); // Disable item's raycast for correct drop handling
                if (myDadItem == item) // If item dragged from this cell
                {
                    // Check cell's type
                    switch (cellType)
                    {
                        case CellType.DropOnly:
                            Item.icon.SetActive(false); // Item can not be dragged. Hide icon
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// On any item drag end enable all items raycast
        /// </summary>
        /// <param name="item"> dragged item </param>
        private void OnAnyItemDragEnd(Item item)
        {
            UpdateMyItem();
            if (myDadItem != null)
            {
                myDadItem.MakeRaycast(true); // Enable item's raycast
            }
        }

        /// <summary>
        /// Item is dropped in this cell
        /// </summary>
        /// <param name="data"></param>
        public void OnDrop(PointerEventData data)
        {
            if (Item.icon != null)
            {
                Item item = Item.draggedItem;
                Cell sourceCell = Item.sourceCell;
                if (Item.icon.activeSelf == true) // If icon inactive do not need to drop item into cell
                {
                    if ((item != null) && (sourceCell != this))
                    {
                        DropEventDescriptor desc = new DropEventDescriptor();
                        switch (cellType) // Check this cell's type
                        {
                            case CellType.DropOnly: // Item only can be dropped into destination cell
                                // Fill event descriptor
                                desc.item = item;
                                desc.sourceCell = sourceCell;
                                desc.destinationCell = this;
                                SendRequest(desc); // Send drop request
                                StartCoroutine(NotifyOnDragEnd(desc)); // Send notification after drop will be finished
                                if (desc.permission == true) // If drop permitted by application
                                {
                                    PlaceItem(item); // Place dropped item in this cell
                                }

                                break;
                            default:
                                Debug.Log("Drag Only");
                                break;
                        }
                    }
                }

                if (item != null)
                {
                    if (item.GetComponentInParent<Cell>() == null) // If item have no cell after drop
                    {
                        Destroy(item.gameObject); // Destroy it
                    }
                }

                UpdateMyItem();
                sourceCell.UpdateMyItem();
            }
        }

        /// <summary>
        /// Put item into this cell.
        /// </summary>
        /// <param name="item">Item.</param>
        private void PlaceItem(Item item)
        {
            if (item != null)
            {
                myDadItem = null;
                item.transform.SetParent(transform, false);
                item.transform.localPosition = Vector3.zero;
                item.MakeRaycast(true);
                myDadItem = item;
            }
        }

        /// <summary>
        /// Send drag and drop information to application
        /// </summary>
        /// <param name="desc"> drag and drop event descriptor </param>
        private void SendNotification(DropEventDescriptor desc)
        {
            if (desc != null)
            {
                // Send message with DragAndDrop info to parents GameObjects
                gameObject.SendMessageUpwards("OnSimpleDragAndDropEvent", desc, SendMessageOptions.DontRequireReceiver);
            }
        }

        /// <summary>
        /// Send drag and drop request to application
        /// </summary>
        /// <param name="desc"> drag and drop event descriptor </param>
        /// <returns> result from desc.permission </returns>
        private bool SendRequest(DropEventDescriptor desc)
        {
            bool result = false;
            if (desc != null)
            {
                desc.triggerType = TriggerType.DropRequest;
                if (transform.childCount == 0 && transform.name.Trim() == desc.sourceCell.transform.name.Trim())
                {
                    desc.permission = true;
                    Debug.Log("This " + transform.name + " is equal " + desc.sourceCell.transform.name);
                }
                else
                {
                    desc.permission = false;
                }

                SendNotification(desc);
                result = desc.permission;
            }

            return result;
        }

        /// <summary>
        /// Wait for event end and send notification to application
        /// </summary>
        /// <param name="desc"> drag and drop event descriptor </param>
        /// <returns></returns>
        private IEnumerator NotifyOnDragEnd(DropEventDescriptor desc)
        {
            // Wait end of drag operation
            while (Item.draggedItem != null)
            {
                yield return new WaitForEndOfFrame();
            }

            desc.triggerType = TriggerType.DropEventEnd;
            SendNotification(desc);
        }

        /// <summary>
        /// Updates my item
        /// </summary>
        public void UpdateMyItem()
        {
            myDadItem = GetComponentInChildren<Item>();
        }
    }
}
