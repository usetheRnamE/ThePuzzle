using SlotSystem;
using UnityEngine;
using Interfaces;

namespace MatrixSystem
{
   [RequireComponent(typeof(LinksInARowController))]
    public class MatrixController : MonoBehaviour
    {
        private const int linksInArray = 4;

        private GameObject[,] slotsMatrix;

        private const int inLineNeighborCount = 2, inLineLinksCount = 2;

        private int xNeighborIndex, yNeighborIndex;

        private LinksInARowController linksInARowController;

        public int xSize, ySize;

        private void Start()
        {
            MatrixSet();

            linksInARowController = GetComponent<LinksInARowController>();
        }

        #region MatrixSet
        private void MatrixSet()
        {
            GameObject slotGameObject;

            int length = 0;

            slotsMatrix = new GameObject[ySize, xSize];

            for (int y = 0; y < ySize; y++)
            {
                for (int x = 0; x < xSize; x++)
                {                    
                    slotGameObject = transform.GetChild(length).gameObject;

                    length++;

                    if (slotGameObject != null)
                    {
                        slotsMatrix[y, x] = slotGameObject;
                      
                        ISlotFunctions slotInterface = slotGameObject.GetComponent<ISlotFunctions>();

                        if (slotInterface != null)
                            slotInterface.SetSlotID(x, y);                                           
                    }
                }
            }

            MatrixUpdate();
        }

      /*  private GameObject[,] GetGridSize()
        {
            int rowCount = 0;
            int columnCount = 0;

            bool isCountRow = true;

            float xFirstSlotNum = ;
           
            foreach (Transform slot in transform)
            {
                if (isCountRow)
                    rowCount++;

                if (slot == xFirstSlotNum)
                {
                    columnCount++;

                    if(isCountRow && columnCount > 1)
                        isCountRow = false;
                }               
            }

            return new GameObject[rowCount, columnCount];
        }*/
        #endregion

        #region MatrixUpdate
        private void MatrixUpdate()
        {
            for (int y = 0; y < slotsMatrix.GetLength(1); y++)
            {
                for (int x = 0; x < slotsMatrix.GetLength(0); x++)
                {
                    LinksCheck(slotsMatrix[y, x].GetComponent<ISlotFunctions>(), x, y);
                }
            }
        }

        public void LinksCheck(ISlotFunctions currentInterface, int xMatrixIndex, int yMatrixIndex)
        {           
            for (int linkNum = 0; linkNum < linksInArray; linkNum++)
            {
                xNeighborIndex = (xMatrixIndex - 1 + linkNum);
                yNeighborIndex = (yMatrixIndex - 1 - inLineLinksCount + linkNum);

                ISlotFunctions neighborInterface;

                if (linkNum < inLineLinksCount && xNeighborIndex >= 0 && xNeighborIndex < inLineNeighborCount)
                {
                    if (xNeighborIndex == xMatrixIndex)
                        xNeighborIndex++;

                    neighborInterface = slotsMatrix[yMatrixIndex, xNeighborIndex].GetComponent<ISlotFunctions>();

                    GetLinkedCheck(linkNum, linkNum == 0 ? 1 : 0, neighborInterface, currentInterface);            
                }
                else if (linkNum >= inLineLinksCount && yNeighborIndex >= 0 && yNeighborIndex < inLineNeighborCount)
                {
                    if (yNeighborIndex == yMatrixIndex)
                        yNeighborIndex++;

                    neighborInterface = slotsMatrix[yNeighborIndex, xMatrixIndex].GetComponent<ISlotFunctions>();

                    GetLinkedCheck(linkNum, linkNum == 2 ? 3 : 2, neighborInterface, currentInterface);
                }
                else
                {
                    currentInterface.LinkDisable(linkNum);
                }
            }
        }
       private void GetLinkedCheck(int linkNum, int invertedLinkNum, ISlotFunctions neighborInterface, ISlotFunctions currentInterface)
       {
            if (neighborInterface != null)
            {
                if (currentInterface.GetSlotState() == neighborInterface.GetSlotState())
                {
                    currentInterface.GetLinked(linkNum, currentInterface.GetSlotState());
                    neighborInterface.GetLinked(invertedLinkNum, currentInterface.GetSlotState());                          
                }
            }
            else
            {
                currentInterface.GetLinked(linkNum, 0);
                neighborInterface.GetLinked(invertedLinkNum, 0);
            }
        }
        #endregion
    }
}
