using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float velocidadMovimiento;
    public float velocidadRotacion;
    public int ammo;
    public GameObject paqueteMunicion;
    private int HP = 20;
    private float lastHitTime = 0;
    private readonly float SHOT_CADENCE = 1f;
    // Start is called before the first frame update
    void Start()
    {
        ammo = 5;
    }

    // Update is called once per frame
    void Update()
    {
        // preparado para que cuando le de a la q lance municion enfrente tuya;
        if(false)
        {
            ammo -= 5;
            Instantiate(paqueteMunicion);
            paqueteMunicion.transform.position = transform.position - new Vector3(1, 0 , 1);
        } 
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        transform.Rotate(new Vector3(0, 1, 0), horizontal * velocidadRotacion);
        transform.Translate(Vector3.forward * vertical * velocidadMovimiento * Time.deltaTime);
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
