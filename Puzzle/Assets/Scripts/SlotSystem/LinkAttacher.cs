
using UnityEngine;

namespace SlotSystem {
    public class LinkAttacher : MonoBehaviour
    {       
        private SlotController slotController;
        private ColoredSlotController coloredSlotController;

        private void Start()
        {
            slotController = transform.GetComponentInParent<SlotController>();          

            if (slotController != null)
            {
                slotController.slotLinks = new GameObject[transform.childCount];

                for (int i = 0; i < transform.childCount; i++)
                    slotController.slotLinks[i] = transform.GetChild(i).gameObject;
            }
            else
            {
                coloredSlotController = transform.GetComponentInParent<ColoredSlotController>();

                coloredSlotController.slotLinks = new GameObject[transform.childCount];

                for (int i = 0; i < transform.childCount; i++)
                    coloredSlotController.slotLinks[i] = transform.GetChild(i).gameObject;
            }
        }
    }
}
