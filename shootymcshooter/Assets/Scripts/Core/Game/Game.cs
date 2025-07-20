using System;
using System.Collections.Generic;
using UnityEngine;

namespace Shooty.Core
{
    public class Game
    {
        public const string DEFAULT_PLAYER_NAME = "Player";
        public const float DEFAULT_VOLUME = 0.1f;

        public static SaveFileData Data { get; private set; }
        public static AudioSource SFXPlayer { get; private set; }
        public static AudioSource MusicPlayer { get; private set; }
        
        public static void Initialize(Transform audioContainer, AudioClip backgroundMusic)
        {
            Data = DataManagement.SaveDataFromFile();
            if (Data == null)
            {
                Data = new SaveFileData(DEFAULT_PLAYER_NAME, DEFAULT_VOLUME);
            }
            
            GameObject SFXPlayerObject = new GameObject("SFXSource");
            GameObject MusicPlayerObject = new GameObject("MusicSource");
            SFXPlayerObject.transform.parent = audioContainer;
            MusicPlayerObject.transform.parent = audioContainer;
            
            SFXPlayer = SFXPlayerObject.AddComponent<AudioSource>();
            MusicPlayer = MusicPlayerObject.AddComponent<AudioSource>();
            
            MusicPlayer.clip = backgroundMusic;
            ApplyAudioSettings();
            MusicPlayer.Play();
        }
        
        public static void ApplyAudioSettings()
        {
            SFXPlayer.volume = Data.Prefs.SFXVolume;
            SFXPlayer.playOnAwake = false;
            SFXPlayer.loop = false;

            MusicPlayer.volume = Data.Prefs.MusicVolume;
            MusicPlayer.loop = true;
        }
    }
}
