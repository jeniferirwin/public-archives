using UnityEngine;

namespace HunterGatherer.UI.Effects
{
    public class TitleDelay : MonoBehaviour
    {
        [SerializeField] private float delay;
        private Rigidbody2D rb;
        
        private float _delay;

        private void Awake()
        {
            _delay = delay;
            rb = gameObject.GetComponent<Rigidbody2D>();
            rb.Sleep();
        }
        
        private void Update()
        {
            if (_delay > 0)
            {
                _delay -= Time.deltaTime;
                return;
            }
            rb.WakeUp();
            this.enabled = false;
        }
    }
}