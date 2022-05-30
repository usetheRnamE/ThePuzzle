using MatrixSystem;
using UnityEngine;
using Interfaces;
using UnityEngine.UI;
using TMPro;

namespace SlotSystem
{
    public class ColoredSlot : MonoBehaviour, ISlotFunctions
    {
        public int slotState; // 1 - first color; 2 - second color 

        public int requiredLinksCount;

        [HideInInspector]
        public GameObject[] slotLinks;

        [HideInInspector]
        public int xId, yId;

        public Color[] colors;

        private TMP_Text linkedCountText;

        private ColorManager colorController;
        private MatrixController matrixController; 

        private Image image;

        private int firstLinksCount = -1, secondLinksCount = -1;

        private void Start()
        {
            colorController = FindObjectOfType<ColorManager>();
            matrixController = FindObjectOfType<MatrixController>();

            linkedCountText = GetComponentInChildren<TMP_Text>();

            image = GetComponent<Image>();
        }

        private void Update()
        {
            if (matrixController.firstColorLinksInARow.Count == firstLinksCount || matrixController.secondColorLinksInARow.Count == secondLinksCount)
                return;

            ChangeLinkedCountText();
        }

        private void ChangeLinkedCountText()
        {
            switch (slotState)
            {
                case 1: 
                    linkedCountText.text = matrixController.firstColorLinksInARow.Count.ToString() + "/" + Mathf.Abs(requiredLinksCount).ToString();

                    firstLinksCount = matrixController.firstColorLinksInARow.Count;
                    break;

                case 2: 
                    linkedCountText.text = matrixController.secondColorLinksInARow.Count.ToString() + "/" + Mathf.Abs(requiredLinksCount).ToString();

                    secondLinksCount = matrixController.secondColorLinksInARow.Count; 
                    break;
            }    
        }

        public void LinkDisable(int linkToDisableNum)
        {
            slotLinks[linkToDisableNum].SetActive(false);
        }

        public void GetLinked(int linkToTied, int colorState)
        {
            colorController.ColorModify(colorState, this, slotLinks[linkToTied]);
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