using MatrixSystem;
using UnityEngine;
using System.Linq;
using Interfaces;

namespace SlotSystem
{
    public class ColorManager : MonoBehaviour
    {
        private const int childNum = 0;

        private bool isAlreadyActive;

        private MatrixController matrixController;

        private int xID, yID;

        private void Start()
        {
            matrixController = GetComponent<MatrixController>();
        }

        public void ColorModify(int colorState, ISlotFunctions currentInterface)
        {
            currentInterface.SetColor(currentInterface.GetColor()[colorState]);
               // matrixController.LinksCheck(parentInterface, xID, yID);

        }
    }
}
