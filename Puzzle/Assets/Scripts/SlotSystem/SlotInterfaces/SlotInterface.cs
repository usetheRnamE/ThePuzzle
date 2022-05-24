using UnityEngine;

namespace Interfaces
{
    public interface ISlotFunctions
    {
        public void LinkDisable(int linkToDisableNum);

        public void GetLinked(int linkToTied, int colorState);

        public int GetSlotState();

        public void SetSlotState(int slotState);

        public Color[] GetColor();
        public void SetColor(Color color);

        public void SetSlotID(int xID,int yID);
        
        public void SetLinks(GameObject[] links);
    }
}
