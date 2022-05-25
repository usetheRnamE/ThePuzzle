using MatrixSystem;
using UnityEngine;
using Interfaces;
using UnityEngine.UI;

namespace SlotSystem
{
    public class ColorManager : MonoBehaviour
    {
        private MatrixController matrixController;

        private int xID, yID;

        private void Start()
        {
            matrixController = GetComponent<MatrixController>();
        }

        public void ColorModify(int colorState, ISlotFunctions currentInterface, GameObject link)
        {
            if (link == null)
            {
                if (currentInterface.GetCurrentColor() == currentInterface.GetColor()[colorState])
                {
                    currentInterface.SetColor(currentInterface.GetColor()[0]);

                    currentInterface.SetSlotState(0);
                }
                else
                {
                    currentInterface.SetSlotState(colorState);

                    currentInterface.SetColor(currentInterface.GetColor()[colorState]);
                }              

                xID = (int)currentInterface.GetSlotID().x;
                yID = (int)currentInterface.GetSlotID().y;

                matrixController.LinksCheck(currentInterface, xID, yID);
            }
            else
                link.GetComponent<Image>().color = currentInterface.GetColor()[colorState];
        }
    }
}
