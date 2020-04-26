using UnityEngine;

//ESTE EL CODIGO DE PROYECTIL (CP1) DEL TEMA 2.4-C
// Note produce efectos de explosión para objetos del layer = 8 solamente

public class compBala : MonoBehaviour
{

    void Update()
    {
        transform.Translate(Vector3.forward * 50f * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.layer == 10)
        {
            Collider colliderPlayer = collider.GetComponent<Collider>();
            if (colliderPlayer != null)
            {
                Destroy(gameObject);
            }
        }
    }
}

