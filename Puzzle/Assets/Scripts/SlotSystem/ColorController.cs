using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace SlotSystem
{
    public class ColorController : MonoBehaviour
    {
        private const int childNum = 0;

        private bool isAlreadyActive;

        SlotController slotController;

        #region Singleton
        private static ColorController instance = null;
        private static readonly object padlock = new object();

        private ColorController() {}

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
            isAlreadyActive = parentTransform.GetChild(childNum).GetChild(colorState).gameObject.activeSelf;

            slotController = parentTransform.GetComponent<SlotController>();
            foreach (var child in from Transform child in parentTransform.GetChild(childNum).transform
                                  where child.gameObject.activeSelf
                                  select child)
            {
                child.gameObject.SetActive(false);
            }

            if (colorState != 0 && isAlreadyActive)
            {
                if (slotController != null)
                    slotController.slotState = 0;
                // else
                //make stuff for links

                parentTransform.GetChild(childNum).GetChild(0).gameObject.SetActive(true);
            }
            else
            {
                if(slotController != null)
                  slotController.slotState = colorState;
                //else
                    //make stuff for links

                parentTransform.GetChild(childNum).GetChild(colorState).gameObject.SetActive(true);
            }
        }
    }
}
