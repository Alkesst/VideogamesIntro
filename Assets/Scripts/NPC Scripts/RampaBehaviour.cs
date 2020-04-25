using UnityEngine;
using UnityEngine.AI;

public class RampaBehaviour : MonoBehaviour
{
    private enum States { andando, buscando, combatiendo }
    private readonly float AGENT_SPEED_PATROL = 4f;
    private readonly float AGENT_SPEED_FIGHT = 4f;
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
            } else if (state == States.combatiendo)
            {
                transform.LookAt(player.transform);
                agent.speed = AGENT_SPEED_FIGHT;
                agent.SetDestination(player.transform.position);
            }
        } else if (state == States.buscando)
        {
            float distCovered = (Time.time - currentTime) * LOOKUP_SPEED;
            float fractionOfJourney = distCovered / LOOKUP_LENGTH;
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
        }
        
    }
}
