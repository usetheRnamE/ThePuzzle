using MatrixSystem;
using UnityEngine;
using System.Linq;

namespace SlotSystem
{
    public class ColorController : MonoBehaviour
    {
        private const int childNum = 0;

        private bool isAlreadyActive;

        private SlotController slotController;
        private MatrixController matrixController;

        private int xID, yID;
        private void Start()
        {
            matrixController = GetComponent<MatrixController>();
        }

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

                xID = parentTransform.GetComponent<SlotController>().xIdInMatrix;
                yID = parentTransform.GetComponent<SlotController>().yIdInMatrix;

                matrixController.LinksCheck(parentTransform.gameObject, xID, yID);
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
