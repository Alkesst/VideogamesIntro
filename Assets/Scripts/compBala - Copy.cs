using UnityEngine;

//ESTE EL CODIGO DE PROYECTIL (CP1) DEL TEMA 2.4-C
// Note produce efectos de explosión para objetos del layer = 8 solamente

public class compBalaPlayer : MonoBehaviour
{

    void Update()
    {
        transform.Translate(Vector3.forward * 50f * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.layer == 8 || collider.gameObject.layer == 9)
        {
            Collider colliderPlayer = collider.GetComponent<Collider>();
            if (colliderPlayer != null)
            {
                Destroy(gameObject);
            }
        }
    }
}

