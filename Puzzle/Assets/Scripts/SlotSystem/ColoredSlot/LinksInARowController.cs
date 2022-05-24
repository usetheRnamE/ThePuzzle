using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MatrixSystem
{
    public class LinksInARowController : MonoBehaviour
    {
        [HideInInspector]
        public List<GameObject> firstColorLinksInARow;

        [HideInInspector]
        public List<GameObject> secondColorLinksInARow;

        public void AddToList(GameObject objToAdd, int colorNum)
        {
            switch (colorNum)
            {
                case 0: firstColorLinksInARow.Add(objToAdd); break;
                case 1: secondColorLinksInARow.Add(objToAdd); break;
            }
        }
    }
}
