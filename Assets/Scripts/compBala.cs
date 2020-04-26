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
            Rigidbody rigidBody = collider.GetComponent<Rigidbody>();
            print(rigidBody.tag);
            if (rigidBody != null)
            {
                print("Nigga toilet");
                float fuerza = 10000f;
                rigidBody.AddExplosionForce(fuerza, transform.position, 20f);
                Destroy(gameObject);
            }
        }
    }
}

