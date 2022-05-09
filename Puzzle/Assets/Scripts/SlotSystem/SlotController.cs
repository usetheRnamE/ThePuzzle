using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.UI;

namespace SlotSystem
{
    public class SlotController : MonoBehaviour, IPointerClickHandler
    {
        [HideInInspector]
        public byte slotState; //0 - default; 1 - first colot; 2 - second color

        public bool useDefaultState;

        [HideInInspector]
        public GameObject[] slotLinks;

        private const byte colorCount = 3;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
                ColorModify(1, gameObject);

            else if (eventData.button == PointerEventData.InputButton.Right)
                ColorModify(2, gameObject);
        }

        public void LinkDisable(byte linkToDisableNum)
        {
            slotLinks[linkToDisableNum].SetActive(false);
        }

        public void GetLinked(byte linkToTied, byte colorState)
        {
           ColorModify(colorState, slotLinks[linkToTied]);
        }

        private void ColorModify(byte colorState, GameObject parent)
        {
            for (int i = 0; i < colorCount; i++)
                if (parent.transform.GetChild(i).gameObject.activeSelf)
                    parent.transform.GetChild(i).gameObject.SetActive(false);

            parent.transform.GetChild(colorState).gameObject.SetActive(true);
        } 
    }
}
