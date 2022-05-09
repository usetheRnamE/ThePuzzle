using UnityEngine.EventSystems;
using System.Linq;
using UnityEngine;
using MatrixSystem;

namespace SlotSystem
{
    public class SlotController : MonoBehaviour, IPointerClickHandler
    {
        [HideInInspector]
        public byte slotState; //0 - default; 1 - first colot; 2 - second color

        public bool useDefaultState;

        [HideInInspector]
        public GameObject[] slotLinks;

        private const byte colorCount = 3;

        [HideInInspector]
        public byte xIdInMatrix, yIdInMatrix;  
        public void OnPointerClick(PointerEventData eventData)
        {
            if (!useDefaultState)
                return;

            if (eventData.button == PointerEventData.InputButton.Left)
                ColorModify(1, gameObject.transform);

            else if (eventData.button == PointerEventData.InputButton.Right)
                ColorModify(2, gameObject.transform);

            MatrixController.matrixControllerInstance.LinksCheck(this, xIdInMatrix, yIdInMatrix);
        }

        public void LinkDisable(byte linkToDisableNum)
        {
            slotLinks[linkToDisableNum].SetActive(false);
        }

        public void GetLinked(byte linkToTied, byte colorState)
        {
           ColorModify(colorState, slotLinks[linkToTied].transform);
        }

        private void ColorModify(byte colorState, Transform parentTransform)
        {
            bool isAlreadyActive = parentTransform.GetChild(colorState).gameObject.activeSelf;

            for (int i = 0; i < colorCount; i++)
            {
                  if (parentTransform.GetChild(i).gameObject.activeSelf)
                        parentTransform.GetChild(i).gameObject.SetActive(false);
            }
            
            if(colorState != 0 && isAlreadyActive)
               parentTransform.GetChild(0).gameObject.SetActive(true);
            else
                parentTransform.GetChild(colorState).gameObject.SetActive(true);
        } 
    }
}
