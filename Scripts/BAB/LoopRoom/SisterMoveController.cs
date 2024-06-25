using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class SisterMoveController : MonoBehaviour
{
    [SerializeField] Transform player;
    private NavMeshAgent agent;
    private Transform nextPoint = null;
    public Transform[] wanderPoints;
    [SerializeField] private int wanderPointIndex = 0;
    public float agentSpeed = 1.25f;
    private Transform mTransform;
    
    private void Start()
    {
        mTransform = transform;
        agent = GetComponent<NavMeshAgent>();
        SetWanderPoint();
        agentSpeed = agent.speed;
    }

    private void Update()
    {
        if (nextPoint != null)
        {
            WanderPointControl();
        }
    }

    private void WanderPointControl()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            wanderPointIndex++;
            wanderPointIndex = (wanderPointIndex < wanderPoints.Length) ? wanderPointIndex : 0;
            SetWanderPoint();
        }
    }

    private void SetWanderPoint()
    {
        if (wanderPoints[wanderPointIndex] == null)
        {
            nextPoint = null;
            return;
        }

        nextPoint = wanderPoints[wanderPointIndex];
        var nextPosition = nextPoint.position;
        agent.SetDestination(new Vector3(nextPosition.x, transform.position.y, nextPosition.z));
    }

    public void SetDestinationPlayer()
    {
        nextPoint = null;
        agent.SetDestination(player.position);
        agent.speed = 3.5f;
    }

    public void CallPauseOnEvent() // called player trigger event or Take Gaze.
    {
        StartCoroutine(Pause());
    }

    private IEnumerator Pause()
    {
        agent.speed = 0f;
        yield return new WaitForSeconds(1.5f);
        agent.speed = agentSpeed;
    }

    public void LookPlayer()
    {
        nextPoint = null;
        agent.ResetPath();
        mTransform.LookAt(player);
        agent.speed = 0;
    }

    public void MoveInitialize()
    {
        agent.ResetPath();
        SetWanderPoint();
        agent.speed = agentSpeed;
    }
}
