using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SlotSystem
{
    public class ColorController : MonoBehaviour
    {
        private const int colorCount = 3;

        private bool isAlreadyActive;

        private bool isSlot;

        SlotController slotController;

        #region Singleton
        private static ColorController instance = null;
        private static readonly object padlock = new object();

        public static ColorController Instance
        {
            get
            {
                lock (padlock)
                {
                    instance ??= new ColorController();

                    return instance;
                }
            }
        }
        #endregion

        public void ColorModify(int colorState, Transform parentTransform)
        {
            isAlreadyActive = parentTransform.GetChild(colorState).gameObject.activeSelf;

            slotController = parentTransform.GetComponent<SlotController>();

            isSlot = slotController != null;

            for (int i = 0; i < colorCount; i++)
            {
                if (parentTransform.GetChild(i).gameObject.activeSelf)
                    parentTransform.GetChild(i).gameObject.SetActive(false);
            }

            if (colorState != 0 && isAlreadyActive)
            {
                if (isSlot)
                    slotController.slotState = 0;
                else
                    //make stuff for links

                parentTransform.GetChild(0).gameObject.SetActive(true);
            }
            else
            {
                if(isSlot)
                  slotController.slotState = colorState;
                else
                    //make stuff for links

                parentTransform.GetChild(colorState).gameObject.SetActive(true);
            }
        }
    }
}
