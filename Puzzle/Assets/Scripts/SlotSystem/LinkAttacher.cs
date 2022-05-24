using Interfaces;
using UnityEngine;

namespace SlotSystem {
    public class LinkAttacher : MonoBehaviour
    {       
        private ISlotFunctions parentInterface;       

        private void Start()
        {      
            parentInterface = GetComponentInParent<ISlotFunctions>();

            if (parentInterface != null)
            {
               GameObject[] links = new GameObject[transform.childCount];

                for (int i = 0; i < transform.childCount; i++)
                    links[i] = transform.GetChild(i).gameObject;

                parentInterface.SetLinks(links);
            }
        }
    }
}
