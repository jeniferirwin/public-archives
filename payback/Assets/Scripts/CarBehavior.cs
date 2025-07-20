using UnityEngine;

public class CarBehavior : MonoBehaviour
{
    private ExplosionDB explosions;
    [SerializeField] private int hp;
    [SerializeField] private HPController hpController;
    [SerializeField] private LayerMask vehicleMask;
    private AudioSource audioSource;
    private UserVolume userVolume;
    private AudioDB audioDB;
    private float moveSpeed;
    private Rigidbody rb;
    private Settings settings;
    private float ttl;
    private bool killed;

    private void Start()
    {
        settings = GameObject.FindGameObjectWithTag("Settings").GetComponent<Settings>();
        explosions = GameObject.Find("Environment").GetComponent<ExplosionDB>();
        audioSource = GetComponent<AudioSource>();
        userVolume = GameObject.FindGameObjectWithTag("AudioSetting").GetComponent<UserVolume>();
        audioDB = GameObject.FindGameObjectWithTag("AudioSetting").GetComponent<AudioDB>();
        audioSource.volume = userVolume.volume;
        moveSpeed = Random.Range(GetMinSpeed() / 2.5f, GetMaxSpeed() / 2.5f);
        rb = GetComponent<Rigidbody>();
        ttl = 50f;
        hpController.SetHP(hp);
    }
    
    private float GetMinSpeed()
    {
        switch (settings.Difficulty)
        {
            case 1: return 0.5f;
            case 2: return 0.65f;
            case 3: return 0.75f;
            case 4: return 0.85f;
            case 5: return 0.95f;
            default: return 1.05f;
        }
    }
    
    private float GetMaxSpeed()
    {
        switch (settings.Difficulty)
        {
            case 1: return 0.8f;
            case 2: return 1f;
            case 3: return 1.2f;
            case 4: return 1.4f;
            case 5: return 1.6f;
            default: return 1.8f;
        }
    }
    private void Update()
    {
        if (Mathf.Abs(transform.position.x) > 50) GameObject.Destroy(gameObject);
        if (killed) return;
        if (hp <= 0) Explode();
        ttl -= Time.deltaTime;
        if (ttl <= 0)
        {
            GameObject.Destroy(gameObject);
        }
    }
    
    private void TurnOffColliders()
    {
        foreach (var coll in GetComponentsInChildren<Collider>())
        {
            coll.enabled = false;
        }
    }
    private void Explode()
    {
        if (killed) return;
        audioDB.PlayExplosion();
        GameObject.Instantiate(explosions.GetRandomExplosion(),transform.position, Quaternion.identity);
        RaycastHit[] hitInfo = Physics.SphereCastAll(transform.position, 10f, Vector3.up, 1f, vehicleMask);
        foreach (var hit in hitInfo)
        {
            CarBehavior otherBehavior;
            if (hit.collider.gameObject.transform.parent.TryGetComponent<CarBehavior>(out otherBehavior))
            {
                otherBehavior.TakeDamage(3);
            }
        }
        killed = true;
        settings.OnCarDestroyed();
        TurnOffColliders();
        var randomZ = Random.Range(-1f, 1f);
        var randomY = Random.Range(-1f, 1f);
        var randomX = Random.Range(-1f, 1f);
        var randomDir = new Vector3(randomX * 5, 1, randomZ * 5);
        rb.constraints = RigidbodyConstraints.None;
        rb.AddForce(randomDir * 10, ForceMode.Impulse);
        var newTorque = new Vector3(randomX, 0, 0);
        rb.AddTorque(newTorque * 5000);
    }
    private void FixedUpdate()
    {
        if (killed) return;
        if (Mathf.Abs(transform.position.z) > 50) GameObject.Destroy(gameObject);
        if (Mathf.Abs(rb.velocity.z) > moveSpeed) return;
        rb.AddForce(transform.forward * -1 * moveSpeed, ForceMode.Impulse);
    }

    public void TakeDamage(int value)
    {
        if (killed) return;
        hp -= value;
        if (hp < 0) hp = 0;
        hpController.SetHP(hp);
    }
}
