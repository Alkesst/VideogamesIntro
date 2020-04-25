using UnityEngine;
using UnityEngine.AI;

public class RampaBehaviour : MonoBehaviour
{
    private enum States { andando, buscando, combatiendo }
    private readonly float AGENT_SPEED_PATROL = 4f;
    private readonly float AGENT_SPEED_FIGHT = 3f;
    private readonly Vector3 LOOKUP_INITIAL = new Vector3(25, 1, -20);
    private readonly Vector3 LOOKUP_FINAL = new Vector3(-25, 1, -20);
    private readonly float LOOKUP_SPEED = 10f;
    private float LOOKUP_LENGTH;

    public NavMeshAgent agent;
    private States state;
    public GameObject[] path;
    private int current;
    private GameObject player;
    private float currentTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        LOOKUP_LENGTH = Vector3.Distance(LOOKUP_INITIAL, LOOKUP_FINAL); 
        state = States.andando;
        if (agent == null) agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(path[0].transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if(state == States.andando)
        {
            if(agent.remainingDistance <= agent.stoppingDistance)
            {
                current++;
                if (current >= path.Length) current = 0;
                agent.speed = AGENT_SPEED_PATROL;
                state = States.buscando;
                currentTime = Time.time;
            }
        } else if (state == States.buscando)
        {
            float distCovered = (Time.time - currentTime) * LOOKUP_SPEED;
            float fractionOfJourney = distCovered / LOOKUP_LENGTH;
            // esto es horrible por favor no lo hagais en casa pero yo solo quiero aprobar me da igual la calidad del codigo
            if (current == 0) {
                transform.LookAt(Vector3.Lerp(LOOKUP_INITIAL + new Vector3(0, 0, 50), LOOKUP_FINAL + new Vector3(0, 0, 50), fractionOfJourney));
            } else
            {
                transform.LookAt(Vector3.Lerp(LOOKUP_INITIAL, LOOKUP_FINAL, fractionOfJourney));
            }
            if(fractionOfJourney >= 1)
            {
                agent.SetDestination(path[current].transform.position);
                state = States.andando;
            }
        } else if (state == States.combatiendo)
        {
            transform.LookAt(player.transform);
            agent.speed = AGENT_SPEED_FIGHT;
            agent.SetDestination(player.transform.position);
        }
        RayCasting();
    }

    private void RayCasting()
    {
        int maskPlayer = 1 << 10;
        int maskScene = 1 << 9;
        int layerMask = maskPlayer | maskScene;
        RaycastHit raycastHit;

        if(Physics.Raycast(transform.position, transform.forward, out raycastHit, 50, layerMask))
        {
            if(raycastHit.collider.gameObject.layer == 10)
            {
                Debug.DrawRay(transform.position, transform.forward * raycastHit.distance, Color.red);
                state = States.combatiendo;
                player = raycastHit.collider.gameObject;
                print("Nigga pizza");
            } else if (raycastHit.collider.gameObject.layer == 9)
            {
                Debug.DrawRay(transform.position, transform.forward * 50, Color.black);
                state = (state == States.andando) ? States.andando : States.buscando;
                player = null;
            }
        } else
        {
            Debug.DrawRay(transform.position, transform.forward * 50, Color.black);
            state = (state == States.andando) ? States.andando : States.buscando;
        }

    }
}
