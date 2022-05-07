using SlotSystem;
using UnityEngine;

namespace MatrixSystem
{
    public class MaxtrixController : MonoBehaviour
    {
        // private GameObject[,] linksMatrix;
        private GameObject[,] slotsMatrix;

        private GameObject rowGameObject;

        // Start is called before the first frame update
        private void Start()
        {
            MatrixSet();
        }

        // Update is called once per frame
        private void Update()
        {

        }


        private void MatrixSet()
        {
            int rowNum = transform.childCount; // num of rows
            int slotsNum = transform.GetChild(0).transform.childCount; // num of slots in each row

            GameObject slotGameObject;

            slotsMatrix = new GameObject[rowNum, slotsNum];

            for (int j = 0; j < rowNum; j++)
            {
                rowGameObject = transform.GetChild(j).gameObject;

                for (int i = 0; i < slotsNum; i++)
                {
                    slotGameObject = rowGameObject.transform.GetChild(i).gameObject;

                    if (slotGameObject != null)
                    {
                        slotsMatrix[j, i] = slotGameObject;

                        if (slotGameObject.GetComponent<SlotController>().useDefaultState)
                            slotGameObject.GetComponent<SlotController>().slotState = 0;
                    }
                }
            }
        }
        private void MatrixUpdate()
        {
            for (int j = 0; j < slotsMatrix.GetLength(1); j++)
            {
                for (int i = 0; i < slotsMatrix.GetLength(0); i++)
                {
                        SlotController currentSlotController = slotsMatrix[j, i].GetComponent<SlotController>();

                       for (int k = 0; k < currentSlotController.slotLinks.Length; k++)
                       {
                          if(k < 2 && slotsMatrix[j - 1 + k, i]  != null && currentSlotController.slotState == slotsMatrix[j - 1 + k, i].GetComponent<SlotController>().slotState)
                          {
                            // get linked
                          }

                         else if (k >= 2 && slotsMatrix[j, i - 3 + k] != null && currentSlotController.slotState == slotsMatrix[j, i - 3 + k].GetComponent<SlotController>().slotState)
                         {
                            // get linked
                         } 

                         else if(slotsMatrix[j - 1 + k, i] == null)
                         {
                            // disable this link
                         }

                        else if (slotsMatrix[j, i - 3 + k] == null)
                        {
                            // disable this link
                        }

                        else
                        {
                            // set link state to 0 (default)
                        }

                    }
                       // if(slotsMatrix[previousSlotInColumn, i] != null && slotsMatrix[previousSlotInColumn, i].GetComponent<SlotController>().slotState == slotsMatrix[j, i].GetComponent<SlotController>().slotState)
                }
            }
        }

    }
}
