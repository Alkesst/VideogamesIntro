using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    private float deltaY, deltaZ, deltaAngle;
    // Start is called before the first frame update
    void Start()
    {
        deltaY = 3f;
        deltaZ = -2.5f;
        deltaAngle = 20;

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + new Vector3(0, deltaY, 0) + deltaZ * player.transform.forward;
        transform.rotation = player.transform.rotation;
        transform.Rotate(new Vector3(1, 0, 0), deltaAngle);
    }
}
