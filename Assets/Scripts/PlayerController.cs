using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float velocidadMovimiento;
    public float velocidadRotacion;
    public GameObject ammoPrefab, bulletPrefab;
    public int ammo;
    private int HP = 20;
    private float lastHitTime = 0;
    private readonly float SHOT_CADENCE = 0.5f;
    private float lastDrop = 0;
    private float lastShot = 0;
    public Transform bulletPosition;
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        ammo = 15;
        text.text = "HP: " + HP + "  -   Ammo: " + ammo;
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "HP: " + HP + "  -   Ammo: " + ammo;
        // preparado para que cuando le de a la q lance municion enfrente tuya;
        if (Input.GetKey("q") && Time.time - lastDrop > 1)
        {
            DropAmmo();
        }
        if(Input.GetButton("Fire1") && Time.time - lastShot > SHOT_CADENCE)
        {
            Shot();
        }
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        transform.Rotate(new Vector3(0, 1, 0), horizontal * velocidadRotacion);
        transform.Translate(Vector3.forward * vertical * velocidadMovimiento * Time.deltaTime);
    }

    private void DropAmmo()
    {
        ammo -= 5;
        GameObject ammoGo = Instantiate(ammoPrefab);
        ammoGo.layer = 12;
        ammoGo.transform.position = transform.position + new Vector3(2, 0.25f, 0);
        lastDrop = Time.time;
    }

    private void Shot()
    {
        if(ammo > 0)
        {
            ammo--;
            GameObject bullet = Instantiate(bulletPrefab, bulletPosition.position, transform.rotation);
            bullet.layer = 14;
            Destroy(bullet, 1.5f);
            lastShot = Time.time;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.layer == 11 && Time.time >= lastHitTime + SHOT_CADENCE)
        {
            lastHitTime = Time.time;
            HP--;
            if(HP <= 0)
            {
                Destroy(gameObject);
            }
        } else if (collider.gameObject.layer == 12)
        {
            ammo += 5;
        } else if (collider.gameObject.layer == 13)
        {
            HP += 5;
        }
    }
}
