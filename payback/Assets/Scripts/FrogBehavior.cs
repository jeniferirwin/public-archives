using UnityEngine;

public class FrogBehavior : MonoBehaviour
{
    public GameObject frogGuts;
    private float moveSpeed;
    private bool isJumper;
    private float jumpCooldown;
    private Animator anim;
    private Rigidbody rb;
    private Settings settings;
    private int frogIntensity;
    private AudioSource audioSource;
    private UserVolume userVolume;
    private AudioDB audioDB;
    
    private void Start()
    {
        settings = GameObject.FindGameObjectWithTag("Settings").GetComponent<Settings>();
        audioDB = GameObject.FindGameObjectWithTag("AudioSetting").GetComponent<AudioDB>();
        frogIntensity = Random.Range(1,4);
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        anim.SetTrigger("Idle");
        moveSpeed = Random.Range(1,4);
        jumpCooldown = GetCooldown();
        if (Random.Range(1,101) <= 50)
        {
            isJumper = true;
        }
        else
        {
            isJumper = false;
        }
    }
    
    private float GetCooldown()
    {
        float min = 0;
        float max = 0;
        switch (frogIntensity)
        {
            case 1:
              min = Random.Range(2f, 3f);
              max = Random.Range(4f, 5f);
              break;
            case 2:
              min = Random.Range(1f, 2f);
              max = Random.Range(3f, 4f);
              break;
            default:
              min = Random.Range(0.5f, 1f);
              max = Random.Range(2f, 3f);
              break;
        }
        return Random.Range(min,max);
    }

    private void FixedUpdate()
    {
        if (transform.position.z > 13f)
        {
            settings.OnFrogSaved();
            GameObject.Destroy(gameObject);
            GameObject.Destroy(this);
        }
        jumpCooldown -= Time.fixedDeltaTime;
        if (isJumper && jumpCooldown <= 0)
        {
            jumpCooldown = Random.Range(1, 5f);
            Jump();
        }
        else if (!isJumper)
        {
            anim.SetTrigger("Crawl");
            var speed = Vector3.forward * moveSpeed * Time.fixedDeltaTime;
            transform.Translate(speed);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        GetSquished();     
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground") && isJumper)
        {
            anim.SetTrigger("Idle");
        }
    }
    
    private void GetSquished()
    {
        audioDB.PlaySquish();
        var guts = GameObject.Instantiate(frogGuts,transform.position,Quaternion.identity);
        var randomY = Random.Range(0,180);
        var gutsRotation = new Vector3(0,randomY,0);
        guts.transform.Rotate(gutsRotation);
        settings.OnFrogSplatted();
        GameObject.Destroy(gameObject);
        GameObject.Destroy(this);
    }

    private void Jump()
    {
        anim.ResetTrigger("Idle");
        anim.SetTrigger("Jump");
        var force = (transform.forward + transform.up) * Random.Range(50f, 100f);
        rb.AddForce(force, ForceMode.Impulse);
    }
}
