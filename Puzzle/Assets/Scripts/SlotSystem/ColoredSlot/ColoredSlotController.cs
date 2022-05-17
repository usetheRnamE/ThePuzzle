using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interfaces;
using MatrixSystem;

namespace SlotSystem
{
    public class ColoredSlotController : MonoBehaviour, ISlotFunctions
    {

        public int slotState; // 1 - first color; 2 - second color 

        [HideInInspector]
        public GameObject[] slotLinks;

        [HideInInspector]
        public int xIdInMatrix, yIdInMatrix;

        private ColorController colorController;

        private void Start()
        {
            colorController = FindObjectOfType<ColorController>();
        }

        public void LinkDisable(int linkToDisableNum)
        {
            slotLinks[linkToDisableNum].SetActive(false);
        }

        public void GetLinked(int linkToTied, int colorState)
        {
            colorController.ColorModify(colorState, slotLinks[linkToTied].transform);            
        }
    }
}