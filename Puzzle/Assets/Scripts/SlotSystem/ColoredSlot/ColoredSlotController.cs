using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interfaces;

namespace SlotSystem
{
    public class ColoredSlotController : MonoBehaviour, ISlotFunctions
    {

        public int slotState; // 1 - first color; 2 - second color 

        [HideInInspector]
        public GameObject[] slotLinks;

        [HideInInspector]
        public int xIdInMatrix, yIdInMatrix;

        public void LinkDisable(int linkToDisableNum)
        {
            slotLinks[linkToDisableNum].SetActive(false);
        }

        public void GetLinked(int linkToTied, int colorState)
        {
           ColorController.Instance.ColorModify(colorState, slotLinks[linkToTied].transform);
        }
    }
}