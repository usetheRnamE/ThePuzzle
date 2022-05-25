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
        private MatrixController matrixController;

        private Image image;

        private void Start()
        {
            colorController = FindObjectOfType<ColorManager>();
            matrixController = FindObjectOfType<MatrixController>();

            image = GetComponent<Image>();
        }

        public void LinkDisable(int linkToDisableNum)
        {
            slotLinks[linkToDisableNum].SetActive(false);
        }

        public void GetLinked(int linkToTied, int colorState)
        {
            colorController.ColorModify(colorState, this, slotLinks[linkToTied]);

            if (colorState != 0)
                matrixController.FindSlotInARow(xId, yId);
        }

        public int GetSlotState()
        {
            return slotState;
        }

        public void SetSlotState(int slotStateToSet)
        {
            return;
        }

        public Color[] GetColor()
        {
            return colors;
        }

        public Color GetCurrentColor()
        {
            return image.color;
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

        public Vector2Int GetSlotID()
        {
            return new Vector2Int(xId, yId);
        }

        public void SetLinks(GameObject[] links)
        {
            slotLinks = links;
        }
    }
}