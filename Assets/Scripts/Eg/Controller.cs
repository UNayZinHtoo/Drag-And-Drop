using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Eg
{

    /// <summary>
    /// Example of control application for drag and drop events handle
    /// </summary>
    public class Controller : MonoBehaviour
    {
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
                    Debug.Log(
                        "Request " + desc.item.name + " from " + sourceSheet.name + " to " + destinationSheet.name);
                    break;
                case Cell.TriggerType.DropEventEnd: // Drop event completed (successful or not)
                    if (desc.permission == true) // If drop successful (was permitted before)
                    {
                        Debug.Log("Successful drop " + desc.item.name + " from " + sourceSheet.name + " to " +
                                  destinationSheet.name);
                    }
                    else // If drop unsuccessful (was denied before)
                    {
                        Debug.Log("Denied drop " + desc.item.name + " from " + sourceSheet.name + " to " +
                                  destinationSheet.name);
                    }

                    break;
                default:
                    Debug.Log("Unknown drag and drop event");
                    break;
            }
        }
    }
}
