using SlotSystem;
using UnityEngine;
using Interfaces;
using System.Collections.Generic;

namespace MatrixSystem
{
   [RequireComponent(typeof(ColorManager))]
    public class MatrixController : MonoBehaviour
    {
        private const int linksInArray = 4;

        [HideInInspector]
        public ISlotFunctions[,] interfaceMatrix;

        private const int inLineNeighborCount = 2, inLineLinksCount = 2;

        private int xNeighborIndex, yNeighborIndex;

        public int xSize, ySize;

        private List<ISlotFunctions> firstColorLinksInARow;

        private List<ISlotFunctions> secondColorLinksInARow;

        private void Start()
        {
            MatrixSet();
        }

        #region MatrixSet
        private void MatrixSet()
        {
            GameObject slotGameObject;

            int length = 0;

            interfaceMatrix = new ISlotFunctions[ySize, xSize];

            for (int y = 0; y < ySize; y++)
            {
                for (int x = 0; x < xSize; x++)
                {                    
                    slotGameObject = transform.GetChild(length).gameObject;

                    length++;

                    if (slotGameObject != null)
                    {
                        interfaceMatrix[y, x] = slotGameObject.GetComponent<ISlotFunctions>();                                           

                        if (interfaceMatrix[y, x] != null)
                            interfaceMatrix[y, x].SetSlotID(x, y);                                           
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
            for (int y = 0; y < interfaceMatrix.GetLength(1); y++)
                for (int x = 0; x < interfaceMatrix.GetLength(0); x++)
                    LinksCheck(interfaceMatrix[y, x], x, y);
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

                    neighborInterface = interfaceMatrix[yMatrixIndex, xNeighborIndex];

                    GetLinkedCheck(linkNum, linkNum == 0 ? 1 : 0, neighborInterface, currentInterface);                    
                }
                else if (linkNum >= inLineLinksCount && yNeighborIndex >= 0 && yNeighborIndex < inLineNeighborCount)
                {
                    if (yNeighborIndex == yMatrixIndex)
                        yNeighborIndex++;

                    neighborInterface = interfaceMatrix[yNeighborIndex, xMatrixIndex];

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
          if (currentInterface.GetSlotState() == neighborInterface.GetSlotState())
          { 
              currentInterface.GetLinked(linkNum, currentInterface.GetSlotState());
              neighborInterface.GetLinked(invertedLinkNum, currentInterface.GetSlotState());
          }
          else
          {
              currentInterface.GetLinked(linkNum, 0);
              neighborInterface.GetLinked(invertedLinkNum, 0);
          }        
        }
        #endregion

        #region SlotsInARowFind
       
        public void FindSlotInARow(int xColoredID, int yColoredID)
        {
            ISlotFunctions coloredInterface = interfaceMatrix[xColoredID, yColoredID];

            for (int linkNum = 0; linkNum < linksInArray; linkNum++)
            {
                xNeighborIndex = (xColoredID - 1 + linkNum);
                yNeighborIndex = (yColoredID - 1 - inLineLinksCount + linkNum);

                ISlotFunctions neighborInterface;

                if (linkNum < inLineLinksCount && xNeighborIndex >= 0 && xNeighborIndex < inLineNeighborCount)
                {
                    if (xNeighborIndex == xColoredID)
                        xNeighborIndex++;

                    neighborInterface = interfaceMatrix[yColoredID, xNeighborIndex];

                    CheckSloteState(coloredInterface, neighborInterface);
                }
                else if (linkNum >= inLineLinksCount && yNeighborIndex >= 0 && yNeighborIndex < inLineNeighborCount)
                {
                    if (yNeighborIndex == yColoredID)
                        yNeighborIndex++;

                    neighborInterface = interfaceMatrix[yNeighborIndex, xColoredID];

                    CheckSloteState(coloredInterface, neighborInterface);
                }
            }
        }

        private void CheckSloteState(ISlotFunctions coloredInterface, ISlotFunctions neighborInterface)
        {
            if (coloredInterface.GetSlotState() == neighborInterface.GetSlotState())
            {
                AddToList(neighborInterface, neighborInterface.GetSlotState());

                FindSlotInARow(neighborInterface.GetSlotID().x, neighborInterface.GetSlotID().y);
            }
        }

        private void AddToList(ISlotFunctions interfaceToAdd, int colorNum)
        {
            switch (colorNum)
            {
                case 1: firstColorLinksInARow.Add(interfaceToAdd); break;
                case 2: secondColorLinksInARow.Add(interfaceToAdd); break;
            }
        }

        private void RemoveFromList(ISlotFunctions interfaceToAdd, int colorNum)
        {
            switch (colorNum)
            {
                case 1: firstColorLinksInARow.Remove(interfaceToAdd); break;
                case 2: secondColorLinksInARow.Remove(interfaceToAdd); break;
            }
        }
        #endregion
    }
}
