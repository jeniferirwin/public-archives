using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

namespace Shooty.Core
{
    public class DataManagement
    {
        public const string SAVE_FILE_NAME = "/ShootySaveData.json";
        public static string SaveFilePath { get { return Application.persistentDataPath + SAVE_FILE_NAME; } }

        public static SaveFileData SaveDataFromFile()
        {
            if (!File.Exists(SaveFilePath)) return new SaveFileData(Game.DEFAULT_PLAYER_NAME, Game.DEFAULT_VOLUME);
            string rawText = File.ReadAllText(SaveFilePath);
            SaveFileData fileData = JsonUtility.FromJson<SaveFileData>(rawText);
            if (fileData != null && ValidateSaveFile(fileData))
            {
                return fileData;
            }
            return new SaveFileData(Game.DEFAULT_PLAYER_NAME, Game.DEFAULT_VOLUME);
        }

        public static bool ValidateSaveFile(SaveFileData data)
        {
            if (data.HighScores == null) return false;
            if (data.Prefs == null) return false;
            if (!NameValidation.IsValidAsPlayerName(data.Prefs.PlayerName)) return false;
            return true;
        }

        public static void SaveDataToFile(SaveFileData data)
        {
            string rawText = JsonUtility.ToJson(data);
            File.WriteAllText(SaveFilePath,rawText);
        }
    }

    [Serializable]
    public class SaveFileData
    {
        public HighScoreData HighScores;
        public PrefsData Prefs;

        public SaveFileData(string defaultPlayerName, float defaultVolume)
        {
            HighScores = new HighScoreData();
            Prefs = new PrefsData(defaultPlayerName, defaultVolume);
        }

        public void ErasePreferences()
        {
            Prefs = new PrefsData(Game.DEFAULT_PLAYER_NAME, Game.DEFAULT_VOLUME);
            Game.ApplyAudioSettings();
        }

        public void EraseHighScores()
        {
            HighScores = new HighScoreData();
        }

        public void EraseAllData()
        {
            ErasePreferences();
            EraseHighScores();
        }
    }

    [Serializable]
    public class HighScoreData
    {
        public List<HighScoreSlot> Slots = new List<HighScoreSlot>();

        public void AddScore(string playerName, int finalScore)
        {
            if (playerName == "" || !NameValidation.IsValidAsPlayerName(playerName) || finalScore <= 0) return;
            HighScoreSlot slot = new HighScoreSlot(playerName, finalScore);
            Slots.Add(slot);
            Slots.Sort((x, y) => y.FinalScore.CompareTo(x.FinalScore));
            if (Slots.Count > 5)
            {
                Slots = Slots.GetRange(0, 5);
            }
        }
    }

    [Serializable]
    public class HighScoreSlot
    {
        public string PlayerName;
        public int FinalScore;

        public HighScoreSlot(string name, int score)
        {
            PlayerName = name;
            FinalScore = score;
        }
    }

    [Serializable]
    public class PrefsData
    {
        public string PlayerName;
        public float SFXVolume;
        public float MusicVolume;

        public PrefsData(string defaultPlayerName, float defaultVolume)
        {
            PlayerName = defaultPlayerName;
            SFXVolume = defaultVolume;
            MusicVolume = defaultVolume;
        }
        public void SetPlayerName(string name) => PlayerName = name;
        public void SetSFXVolume(float value) => SFXVolume = value;
        public void SetMusicVolume(float value) => MusicVolume = value;
    }
}
