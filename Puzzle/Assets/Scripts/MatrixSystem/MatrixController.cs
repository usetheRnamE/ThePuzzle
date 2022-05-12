using SlotSystem;
using UnityEngine;

namespace MatrixSystem
{
    public class MatrixController : MonoBehaviour
    {
        private GameObject[,] slotsMatrix;

        private GameObject rowGameObject;

        private const byte inLineNeighborCount = 2;

        public static MatrixController matrixControllerInstance; //Singleton pattern cuz only one matrix in the game

        private void Start()
        {
            matrixControllerInstance = this;

            MatrixSet();
        }

        private void MatrixSet()
        {
            byte rowNum = (byte)transform.childCount; // num of rows
            byte slotsNum = (byte)transform.GetChild(0).transform.childCount; // num of slots in each row

            GameObject slotGameObject;

            slotsMatrix = new GameObject[rowNum, slotsNum];

            for (byte y = 0; y < rowNum; y++)
            {
                rowGameObject = transform.GetChild(y).gameObject;

                for (byte x = 0; x < slotsNum; x++)
                {
                    slotGameObject = rowGameObject.transform.GetChild(x).gameObject;

                    if (slotGameObject != null)
                    {
                        slotsMatrix[y, x] = slotGameObject;

                        SlotController slotController = slotGameObject.GetComponent<SlotController>();

                        slotController.xIdInMatrix = x;
                        slotController.yIdInMatrix = y;

                        if (slotController.useDefaultState)
                            slotController.slotState = 0;                     
                    }
                }
            }

            MatrixUpdate();
        }
        private void MatrixUpdate()
        {
            for (byte y = 0; y < slotsMatrix.GetLength(1); y++)
            {
                for (byte x = 0; x < slotsMatrix.GetLength(0); x++)
                {
                    LinksCheck(slotsMatrix[y, x].GetComponent<SlotController>(), x, y);
                }
            }
        }

        public void LinksCheck(SlotController currentSlotController, byte xMatrixIndex, byte yMatrixIndex)
        {
            SlotController rowNeighborSlotController, columnNeighborSlotController;

            sbyte xNeighborIndex, yNeighborIndex;    

            for (byte linkNum = 0; linkNum < currentSlotController.slotLinks.Length; linkNum++)
            {
                xNeighborIndex = (sbyte) (xMatrixIndex - 1 + linkNum);
                yNeighborIndex = (sbyte) (yMatrixIndex - 3 + linkNum);
          
                if (linkNum < 2 && xNeighborIndex >= 0 && xNeighborIndex < inLineNeighborCount)
                {
                    if (xNeighborIndex == xMatrixIndex)
                        xNeighborIndex++;

                    rowNeighborSlotController = slotsMatrix[yMatrixIndex, xNeighborIndex].GetComponent<SlotController>();

                    if (currentSlotController.slotState == rowNeighborSlotController.slotState)
                    {
                        currentSlotController.GetLinked(linkNum, currentSlotController.slotState);
                        rowNeighborSlotController.GetLinked((byte)(linkNum == 0 ? 1 : 0), currentSlotController.slotState);
                    }
                }

                else if (linkNum >= 2 && yNeighborIndex >= 0 && yNeighborIndex < inLineNeighborCount)
                {
                    if (yNeighborIndex == yMatrixIndex)
                        yNeighborIndex++;

                    columnNeighborSlotController = slotsMatrix[yNeighborIndex, xMatrixIndex].GetComponent<SlotController>();

                    if (currentSlotController.slotState == columnNeighborSlotController.slotState)
                    {
                        currentSlotController.GetLinked(linkNum , currentSlotController.slotState);
                        columnNeighborSlotController.GetLinked((byte)(linkNum == 2 ? 3 : 2), currentSlotController.slotState);
                    }
                }
                else 
                { 
                    currentSlotController.LinkDisable(linkNum);
                }
            }
        }
    }
}
