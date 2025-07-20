using UnityEngine;
using Shooty.Core;

namespace Shooty
{
    public class SpawnTarget : MonoBehaviour
    {
        private GameObject myActiveBall;
        private float _currentCooldown;
        
        private void Update()
        {
            if (_currentCooldown > 0)
            {
                _currentCooldown -= Time.deltaTime;
                return;
            }
            if (myActiveBall != null) return;
            
            _currentCooldown = GetNewCooldown();
            LaunchTarget();
        }
        
        private void LaunchTarget()
        {
            myActiveBall = GameObject.Instantiate(SpawnerSettings.Prefab, transform.position, Quaternion.identity);
            var rb = myActiveBall.GetComponent<Rigidbody>();
            var newForce = Random.Range(SpawnerSettings.MinForce, SpawnerSettings.MaxForce);
            rb.AddForce(Vector3.up * newForce, ForceMode.Impulse);
            var torqueX = Random.Range(-20, 20);
            var torqueY = Random.Range(-20, 20);
            var torqueZ = Random.Range(-20, 20);
            rb.AddTorque(new Vector3(torqueX, torqueY, torqueZ), ForceMode.Impulse);
        }
        
        private float GetNewCooldown()
        {
            var min = SpawnerSettings.MinCooldown * GetDifficultyMultiplier();
            var max = SpawnerSettings.MaxCooldown * GetDifficultyMultiplier();
            return Random.Range(min, max);
        }
        
        private float GetDifficultyMultiplier()
        {
            return 1f - RoundData.Score / 15 / 100;
        }
    }
}
