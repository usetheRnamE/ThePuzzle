
using UnityEngine;

namespace SlotSystem {
    public class LinkAttacher : MonoBehaviour
    {
        [HideInInspector]
        public byte linksState; //0 - default; 1 - first colot; 2 - second color

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
