using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float velocidadMovimiento;
    public float velocidadRotacion;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        transform.Rotate(new Vector3(0, 1, 0), horizontal * velocidadRotacion);
        transform.Translate(Vector3.forward * vertical * velocidadMovimiento * Time.deltaTime);
    }
}
