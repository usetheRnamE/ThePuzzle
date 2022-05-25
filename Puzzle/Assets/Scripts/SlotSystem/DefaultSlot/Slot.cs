using UnityEngine.EventSystems;
using Interfaces;
using UnityEngine;
using MatrixSystem;
using UnityEngine.UI;

namespace SlotSystem
{
    public class Slot : MonoBehaviour, ISlotFunctions, IPointerClickHandler
    {
        [HideInInspector]
        public int slotState = 0; //0 - default; 1 - first colot; 2 - second color

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

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
                colorController.ColorModify(1, this, null);           
             
            else if (eventData.button == PointerEventData.InputButton.Right)
                colorController.ColorModify(2, this, null);
                    
            matrixController.LinksCheck(this, xId, yId);
        }

        public  void LinkDisable(int linkToDisableNum)
        {
            slotLinks[linkToDisableNum].SetActive(false);
        }

        public  void GetLinked(int linkToTied, int colorState)
        {
            colorController.ColorModify(colorState, this, slotLinks[linkToTied]);      
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
