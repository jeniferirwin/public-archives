using UnityEngine;

namespace Shooty.Core
{
    public class StaticCommander : MonoBehaviour
    {
        // Data Management Commands
        
        public static void DMErasePreferences()
        {
            Game.Data.ErasePreferences();
            DataManagement.SaveDataToFile(Game.Data);
        }
        
        public static void DMEraseHighScores()
        {
            Game.Data.EraseHighScores();
            DataManagement.SaveDataToFile(Game.Data);
        }
        
        public static void DMEraseAllData()
        {
            Game.Data.EraseAllData();
            DataManagement.SaveDataToFile(Game.Data);
        }
        
        public static void DMSaveData()
        {
            DataManagement.SaveDataToFile(Game.Data);
        }
        
        public static void RDResetScores()
        {
            RoundData.ResetScore();
        }
        
        public static void RDSetType(string type)
        {
            if (type == "Cube")
            {
                RoundData.SetChosenTargetType(TargetType.Cube);
            }
            else if (type == "Sphere")
            {
                RoundData.SetChosenTargetType(TargetType.Sphere);
            }
        }
        
        public static void MMQuit()
        {
            Application.Quit();
        }
    }
}