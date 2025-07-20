using UnityEngine;
using Shooty.Core;

namespace Shooty.UI
{
    public class TargetChoiceInput : MonoBehaviour
    {
        public void SetTargetType(string type)
        {
            TargetType playerTarget = TargetType.Sphere;
            if (type == "Cube") playerTarget = TargetType.Cube; 
                
            RoundData.SetChosenTargetType(playerTarget);
        }
    }
}
