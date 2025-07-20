using System;
using Shooty.Core;
using UnityEngine;

namespace Shooty
{
    public class SpawnerSettings : MonoBehaviour
    {
        private static SpawnerSettings _instance;
        public static SpawnerSettings Instance { get { return _instance; } }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            _instance = this;
        }

        [SerializeField] private GameObject spherePrefab;
        [SerializeField] private GameObject cubePrefab;
        [SerializeField] private float minCooldown;
        [SerializeField] private float maxCooldown;
        [SerializeField] private float minForce;
        [SerializeField] private float maxForce;
        
        public static float MinCooldown { get { return Instance.minCooldown; } }
        public static float MaxCooldown { get { return Instance.maxCooldown; } }
        public static float MinForce { get { return Instance.minForce; } }
        public static float MaxForce { get { return Instance.maxForce; } }

        public static GameObject Prefab
        {
            get
            {
                if (RoundData.ChosenTargetType == TargetType.Cube)
                {
                    return Instance.cubePrefab;
                }
                else
                {
                    return Instance.spherePrefab;
                }
            }
        }
        
        private void Start()
        {
            RoundData.GameOver += GameOver;
        }
        
        private void OnDestroy()
        {
            RoundData.GameOver -= GameOver;
        }
        
        private void GameOver(object sender, EventArgs e)
        {
            gameObject.SetActive(false);
        }
    }
}
