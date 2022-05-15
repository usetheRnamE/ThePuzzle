using SlotSystem;
using UnityEngine;

namespace MatrixSystem
{
    public class MatrixController : MonoBehaviour
    {
        #region Singleton
        private static MatrixController instance = null;
        private static readonly object padlock = new object();

        public static MatrixController Instance
        {
            get
            {
                lock (padlock)
                {
                    instance ??= new MatrixController();

                    return instance;
                }
            }
        }
        #endregion

        private const int linksInArray = 4;

        private GameObject[,] slotsMatrix;

        private GameObject rowGameObject;

        private const int inLineNeighborCount = 2;

        int xNeighborIndex, yNeighborIndex;

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

                        dynamic slotController = slotGameObject.GetComponent<SlotController>();
                        slotController ??= slotGameObject.GetComponent<ColoredSlotController>();

                        slotController.xIdInMatrix = x;
                        slotController.yIdInMatrix = y;               
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
            dynamic currentSlotController = currentSlot.GetComponent<SlotController>();
            currentSlotController ??= currentSlot.GetComponent<ColoredSlotController>();

            for (int linkNum = 0; linkNum < linksInArray; linkNum++)
            {
                xNeighborIndex = (xMatrixIndex - 1 + linkNum);
                yNeighborIndex = (yMatrixIndex - 3 + linkNum);
          
                if (linkNum < 2 && xNeighborIndex >= 0 && xNeighborIndex < inLineNeighborCount)
                {
                    if (xNeighborIndex == xMatrixIndex)
                        xNeighborIndex++;

                    dynamic rowNeighborController = slotsMatrix[yMatrixIndex, xNeighborIndex].GetComponent<SlotController>();
                    rowNeighborController ??= slotsMatrix[yMatrixIndex, xNeighborIndex].GetComponent<ColoredSlotController>();

                    if (currentSlotController.slotState == rowNeighborController.slotState)
                    {
                        currentSlotController.GetLinked(linkNum, currentSlotController.slotState);
                        rowNeighborController.GetLinked((linkNum == 0 ? 1 : 0), currentSlotController.slotState);
                    }
                }

                else if (linkNum >= 2 && yNeighborIndex >= 0 && yNeighborIndex < inLineNeighborCount)
                {
                    if (yNeighborIndex == yMatrixIndex)
                        yNeighborIndex++;

                    dynamic columnNeighborSlotController = slotsMatrix[yNeighborIndex, xMatrixIndex].GetComponent<SlotController>();
                    columnNeighborSlotController ??= slotsMatrix[yMatrixIndex, xNeighborIndex].GetComponent<ColoredSlotController>();

                    if (currentSlotController.slotState == columnNeighborSlotController.slotState)
                    {
                        currentSlotController.GetLinked(linkNum , currentSlotController.slotState);
                        columnNeighborSlotController.GetLinked((linkNum == 2 ? 3 : 2), currentSlotController.slotState);
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
