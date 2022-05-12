
using UnityEngine;

namespace SlotSystem {
    public class LinkAttacher : MonoBehaviour
    {       
        private SlotController slotControllerInstance;

        private void Start()
        {
            slotControllerInstance = transform.GetComponentInParent<SlotController>();

            slotControllerInstance.slotLinks = new GameObject[transform.childCount];

            for (int i = 0; i < transform.childCount; i++)
                slotControllerInstance.slotLinks[i] = transform.GetChild(i).gameObject;
        }
    }
}
