using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interfaces;
using MatrixSystem;
using UnityEngine.UI;

namespace SlotSystem
{
    public class ColoredSlot : MonoBehaviour, ISlotFunctions
    {
        public int slotState; // 1 - first color; 2 - second color 

        [HideInInspector]
        public GameObject[] slotLinks;

        [HideInInspector]
        public int xId, yId;

        public Color[] colors;

        private ColorManager colorController;

        private Image image;

        private void Start()
        {
            colorController = FindObjectOfType<ColorManager>();
            image = GetComponent<Image>();
        }

        public void LinkDisable(int linkToDisableNum)
        {
            slotLinks[linkToDisableNum].SetActive(false);
        }

        public void GetLinked(int linkToTied, int colorState)
        {
            //colorController.ColorModify(colorState, slotLinks[linkToTied].transform);            
        }

        public int GetSlotState()
        {
            return slotState;
        }

        public void SetSlotState(int slotStateToSet)
        {
            slotState = slotStateToSet;
        }

        public Color[] GetColor()
        {
            return colors;
        }

        public void SetColor(Color colorToSet)
        {
            image.color = colorToSet;
        }

        public void SetSlotID(int xID, int yID)
        {
            xId = xID;
            yId = yID;
        }

        public void SetLinks(GameObject[] links)
        {
            slotLinks = links;
        }
    }
}