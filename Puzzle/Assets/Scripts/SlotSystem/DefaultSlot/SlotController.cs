using UnityEngine.EventSystems;
using Interfaces;
using UnityEngine;
using MatrixSystem;

namespace SlotSystem
{
    public class SlotController : MonoBehaviour, ISlotFunctions, IPointerClickHandler
    {
        [HideInInspector]
        public int slotState; //0 - default; 1 - first colot; 2 - second color

        [HideInInspector]
        public GameObject[] slotLinks;

        [HideInInspector]
        public int xIdInMatrix, yIdInMatrix;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
                ColorController.Instance.ColorModify(1, gameObject.transform);

            else if (eventData.button == PointerEventData.InputButton.Right)
                ColorController.Instance.ColorModify(2, gameObject.transform);

           // MatrixController.Instance.LinksCheck(this.gameObject, xIdInMatrix, yIdInMatrix);
        }

        public  void LinkDisable(int linkToDisableNum)
        {
            slotLinks[linkToDisableNum].SetActive(false);
        }

        public  void GetLinked(int linkToTied, int colorState)
        {
            ColorController.Instance.ColorModify(colorState, slotLinks[linkToTied].transform);
        }       
    }
}
