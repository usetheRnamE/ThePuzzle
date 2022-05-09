using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SlotSystem
{
    public class SlotController : MonoBehaviour
    {
        [HideInInspector]
        public byte slotState; //0 - default; 1 - first colot; 2 - second color

        public bool useDefaultState;

        [HideInInspector]
        public GameObject[] slotLinks;

        private const byte colorCount = 3; 

        public void LinkDisable(byte linkToDisableNum)
        {
            slotLinks[linkToDisableNum].SetActive(false);
        }

        public void GetLinked(byte linkToTied, byte colorState)
        {
           ColorModify(colorState, slotLinks[linkToTied]);
        }

        private void ColorModify(byte colorState, GameObject parent)
        {
            for (int i = 0; i < colorCount; i++)
                if (parent.transform.GetChild(i).gameObject.activeSelf)
                    parent.transform.GetChild(colorState).gameObject.SetActive(false);

            parent.transform.GetChild(colorState).gameObject.SetActive(true);
        } 
    }
}
