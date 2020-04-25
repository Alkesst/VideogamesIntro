using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float velocidadMovimiento;
    public float velocidadRotacion;
    public int municion;
    public GameObject paqueteMunicion;
    // Start is called before the first frame update
    void Start()
    {
        municion = 5;
    }

    // Update is called once per frame
    void Update()
    {
        // preparado para que cuando le de a la q lance municion enfrente tuya;
        if(false)
        {
            municion -= 5;
            Instantiate(paqueteMunicion);
            paqueteMunicion.transform.position = transform.position - new Vector3(1, 0 , 1);
        } 
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        transform.Rotate(new Vector3(0, 1, 0), horizontal * velocidadRotacion);
        transform.Translate(Vector3.forward * vertical * velocidadMovimiento * Time.deltaTime);
    }
}
