
using UnityEngine;

namespace SlotSystem {
    public class LinkAttacher : MonoBehaviour
    {       
        private dynamic slotController;

        private void Start()
        {
            slotController = transform.GetComponentInParent<SlotController>();
            slotController ??= transform.GetComponentInParent<ColoredSlotController>();


                slotController.slotLinks = new GameObject[transform.childCount];

                for (int i = 0; i < transform.childCount; i++)
                    slotController.slotLinks[i] = transform.GetChild(i).gameObject;
        }
    }
}
