using UnityEngine;
using UnityEngine.AI;

public class RampaBehaviour : MonoBehaviour
{
    private enum States { patrullando, combatiendo }
    private readonly float AGENT_SPEED_PATROL = 4f;
    private readonly float AGENT_SPEED_FIGHT = 4f;

    public NavMeshAgent agent;
    private States state;
    public GameObject[] path;
    private int current;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        state = States.patrullando;
        if (agent == null) agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(path[0].transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if(state == States.patrullando)
        {
            if(agent.remainingDistance <= agent.stoppingDistance)
            {
                current++;
                if (current >= path.Length) current = 0;
                agent.speed = AGENT_SPEED_PATROL;
                agent.SetDestination(path[current].transform.position);
            } else if (state == States.combatiendo)
            {
                transform.LookAt(player.transform);
                agent.speed = AGENT_SPEED_FIGHT;
                agent.SetDestination(player.transform.position);
            }
        }
        
    }
}
