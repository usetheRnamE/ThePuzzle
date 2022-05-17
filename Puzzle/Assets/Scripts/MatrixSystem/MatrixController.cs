using SlotSystem;
using UnityEngine;

namespace MatrixSystem
{
    public class MatrixController : MonoBehaviour
    {
        private const int linksInArray = 4;

        private GameObject[,] slotsMatrix;

        private GameObject rowGameObject;

        private const int inLineNeighborCount = 2;

        int xNeighborIndex, yNeighborIndex;

        SlotController slotController, currentSlotController, rowNeighborController, columnNeighborController;
        ColoredSlotController coloredSlotController, currentColoredSlotController, rowNeighborColoredController, columnNeighborColoredController;

        private void Start()
        {
            MatrixSet();
        }
        private void MatrixSet()
        {
            int rowNum = transform.childCount; // num of rows
            int slotsNum = transform.GetChild(0).transform.childCount; // num of slots in each row

            GameObject slotGameObject;

            slotsMatrix = new GameObject[rowNum, slotsNum];

            for (int y = 0; y < rowNum; y++)
            {
                rowGameObject = transform.GetChild(y).gameObject;

                for (int x = 0; x < slotsNum; x++)
                {
                    slotGameObject = rowGameObject.transform.GetChild(x).gameObject;

                    if (slotGameObject != null)
                    {
                        slotsMatrix[y, x] = slotGameObject;

                        slotController = slotGameObject.GetComponent<SlotController>();

                        if (slotController != null)
                        {
                            slotController.xIdInMatrix = x;
                            slotController.yIdInMatrix = y;
                        }
                        else
                        {
                            coloredSlotController = slotGameObject.GetComponent<ColoredSlotController>();

                            coloredSlotController.xIdInMatrix = x;
                            coloredSlotController.yIdInMatrix = y;
                        }                       
                    }
                }
            }

            MatrixUpdate();
        }

        private void MatrixUpdate()
        {
            for (int y = 0; y < slotsMatrix.GetLength(1); y++)
            {
                for (int x = 0; x < slotsMatrix.GetLength(0); x++)
                {
                    LinksCheck(slotsMatrix[y, x], x, y);
                }
            }
        }

        public void LinksCheck(GameObject currentSlot, int xMatrixIndex, int yMatrixIndex)
        {           
            for (int linkNum = 0; linkNum < linksInArray; linkNum++)
            {
                xNeighborIndex = (xMatrixIndex - 1 + linkNum);
                yNeighborIndex = (yMatrixIndex - 3 + linkNum);

                currentSlotController = currentSlot.GetComponent<SlotController>();
                currentColoredSlotController = currentSlot.GetComponent<ColoredSlotController>();

                if (linkNum < 2 && xNeighborIndex >= 0 && xNeighborIndex < inLineNeighborCount)
                {
                    if (xNeighborIndex == xMatrixIndex)
                        xNeighborIndex++;
                   
                    rowNeighborController = slotsMatrix[yMatrixIndex, xNeighborIndex].GetComponent<SlotController>();
                    rowNeighborColoredController = slotsMatrix[yMatrixIndex, xNeighborIndex].GetComponent<ColoredSlotController>();

                    GetLinkedCheck(linkNum, linkNum == 0 ? 1 : 0, rowNeighborController, rowNeighborColoredController);
                    
                }

                else if (linkNum >= 2 && yNeighborIndex >= 0 && yNeighborIndex < inLineNeighborCount)
                {
                    if (yNeighborIndex == yMatrixIndex)
                        yNeighborIndex++;

                    columnNeighborController = slotsMatrix[yNeighborIndex, xMatrixIndex].GetComponent<SlotController>();
                    columnNeighborColoredController = slotsMatrix[yNeighborIndex, xMatrixIndex].GetComponent<ColoredSlotController>();


                    GetLinkedCheck(linkNum, linkNum == 2 ? 3 : 2, columnNeighborController, columnNeighborColoredController);
                }
                else
                {
                    if(currentSlotController != null)
                       currentSlotController.LinkDisable(linkNum);
                    else 
                        currentColoredSlotController.LinkDisable(linkNum);
                }
            }
        }

       private void GetLinkedCheck(int linkNum, int invertedLinkNum, SlotController neighborSlotController, ColoredSlotController neighborColoredSlotController)
       {

            if (currentSlotController != null)
            {
                if (neighborSlotController != null && currentSlotController.slotState == neighborSlotController.slotState)
                {
                    currentSlotController.GetLinked(linkNum, currentSlotController.slotState);
                    neighborSlotController.GetLinked(invertedLinkNum, currentSlotController.slotState);
                }           
                else if (neighborSlotController == null && currentSlotController.slotState == neighborColoredSlotController.slotState)
                {
                    currentSlotController.GetLinked(linkNum, currentSlotController.slotState);
                    neighborColoredSlotController.GetLinked(invertedLinkNum, currentSlotController.slotState);
                }
                else if (neighborSlotController != null)
                {
                    currentSlotController.GetLinked(linkNum, 0);
                    neighborSlotController.GetLinked(invertedLinkNum, 0);
                }
                else
                {
                    currentSlotController.GetLinked(linkNum, 0);
                    neighborColoredSlotController.GetLinked(invertedLinkNum, 0);
                }
            }
            else
            {
                if (neighborSlotController != null && currentColoredSlotController.slotState == neighborSlotController.slotState)
                {
                    currentColoredSlotController.GetLinked(linkNum, currentColoredSlotController.slotState);
                    neighborSlotController.GetLinked(invertedLinkNum, currentColoredSlotController.slotState);
                }
                else if (neighborSlotController == null && currentColoredSlotController.slotState == neighborColoredSlotController.slotState)
                {
                    currentColoredSlotController.GetLinked(linkNum, currentColoredSlotController.slotState);
                    neighborColoredSlotController.GetLinked(invertedLinkNum, currentColoredSlotController.slotState);
                }
                else if (neighborSlotController != null)
                {
                    currentColoredSlotController.GetLinked(linkNum, 0);
                    neighborSlotController.GetLinked(invertedLinkNum, 0);
                }
                else
                {
                    currentColoredSlotController.GetLinked(linkNum, 0);
                    neighborColoredSlotController.GetLinked(invertedLinkNum, 0);
                }
            }
       }
    }
}
