using UnityEngine;

public class HitDetection : MonoBehaviour
{
    public LayerMask targetsMask;
    public LayerMask vehicleMask;
    public GameObject hitDisplay;
    public Settings settings;
    private AudioDB audioDB;
    
    public float rifleShotSize;
    public float shotgunBlastSize;
    public float machineGunBlastSize;
    public float grenadeBlastSize;
    public float rocketBlastSize;

    private float cooldown;
    
    private void Start()
    {
        settings = GameObject.FindGameObjectWithTag("Settings").GetComponent<Settings>();
        audioDB = GameObject.FindGameObjectWithTag("AudioSetting").GetComponent<AudioDB>();
        cooldown = 0f;
    }

    void Update()
    {
        cooldown -= Time.deltaTime;
        if (cooldown > 0) return;
        if (Input.GetMouseButton(0))
        {
            cooldown = GetWeaponCooldown();
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray.origin, ray.direction, out hit, 30, targetsMask))
            {
                switch (settings.CarsDestroyed)
                {
                    case int n when n < 30:
                        RifleShot(hit.point);
                        audioDB.PlayRifle();
                        break;
                    case int n when n >= 30 && n < 60:
                        ShotgunBlast(hit.point);
                        audioDB.PlayShotgun();
                        break;
                    case int n when n >= 60 && n < 90:
                        MachineGunShot(hit.point);
                        audioDB.PlayMachineGun();
                        break;
                    case int n when n >= 90 && n < 120:
                        GrenadeBlast(hit.point);
                        audioDB.PlayGrenade();
                        break;
                    default:
                        RocketBlast(hit.point);
                        audioDB.PlayRocket();
                        break;
                }
                // GameObject.Instantiate(hitDisplay, hit.point, Quaternion.identity);
            }
        }
    }
    
    private void RifleShot(Vector3 location)
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(location + new Vector3(0, 5, 0), Vector3.down, out hitInfo, 10f, vehicleMask))
        {
            var carBehavior = hitInfo.collider.gameObject.transform.parent.GetComponent<CarBehavior>();
            carBehavior.TakeDamage(1);
            DrawHitDisplay(location, rifleShotSize);
        }
    }
    private void ShotgunBlast(Vector3 location)
    {
        RaycastHit[] hitInfo = Physics.SphereCastAll(location + new Vector3(0, 5, 0), 1f, Vector3.down, 10f, vehicleMask);
        foreach (var hit in hitInfo)
        {
            CarBehavior behavior;
            if (hit.collider.gameObject.transform.parent.TryGetComponent<CarBehavior>(out behavior))
            {
                behavior.TakeDamage(2);
                DrawHitDisplay(location, shotgunBlastSize);
            }
        }
    }
    
    private void MachineGunShot(Vector3 location)
    {
        RaycastHit hitInfo;
        if (Physics.SphereCast(location + new Vector3(0, 5, 0), 0.1f, Vector3.down, out hitInfo, 10f, vehicleMask))
        {
            var carBehavior = hitInfo.collider.gameObject.transform.parent.GetComponent<CarBehavior>();
            carBehavior.TakeDamage(1);
            DrawHitDisplay(location, machineGunBlastSize);
        }
    }
    
    private void GrenadeBlast(Vector3 location)
    {
        RaycastHit[] hitInfo = Physics.SphereCastAll(location + new Vector3(0, 5, 0), 1.5f, Vector3.down, 10f, vehicleMask);
        foreach (var hit in hitInfo)
        {
            CarBehavior behavior;
            if (hit.collider.gameObject.transform.parent.TryGetComponent<CarBehavior>(out behavior))
            {
                behavior.TakeDamage(2);
                DrawHitDisplay(location, grenadeBlastSize);
            }
        }
    }
    
    private void RocketBlast(Vector3 location)
    {
        RaycastHit[] hitInfo = Physics.SphereCastAll(location + new Vector3(0, 5, 0), 2.5f, Vector3.down, 10f, vehicleMask);
        foreach (var hit in hitInfo)
        {
            CarBehavior behavior;
            if (hit.collider.gameObject.transform.parent.TryGetComponent<CarBehavior>(out behavior))
            {
                behavior.TakeDamage(3);
                DrawHitDisplay(location, rocketBlastSize);
            }
        }
    }
    
    private void DrawHitDisplay(Vector3 location, float size)
    {
        var sphere = GameObject.Instantiate(hitDisplay, location, Quaternion.identity);
        sphere.transform.localScale = new Vector3(size,size,size);
    }

    private float GetWeaponCooldown()
    {
        switch (settings.CarsDestroyed)
        {
            case int n when n < 30: return 1f / 2f;
            case int n when n >= 30 && n < 60: return 1f / 2f;
            case int n when n >= 60 && n < 90: return 1f / 50f;
            case int n when n >= 90 && n < 120: return 1f / 3f;
            case int n when n >= 120: return 1f / 2f;
            default: return 0.3f;
        }
    }
}
