using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SlotSystem
{
    public class SlotController : MonoBehaviour
    {
        [HideInInspector]
        public byte slotState; //0 - default; 1 - first colot; 2 - second color

        public bool useDefaultState;

        [SerializeField]
        public GameObject[] slotLinks;
    }
}
