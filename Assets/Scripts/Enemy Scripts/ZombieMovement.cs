using System.Collections;
using Enemy_Scripts;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(TrackPlayer))]
public class ZombieMovement : MonoBehaviour
{
    public enum enemyState
    {
        Rotating,
        Idle, 
        Attacking
    }

    TrackPlayer trackPlayer;
    public Transform player;
    NavMeshAgent navMeshAgent;
    [SerializeField] private GameObject eyePlane;
    private NavMeshSurface surface;
    
    public readonly float rotationWait = 2;
    [SerializeField]private float currRotationWait, rotationAngle, rotationTime = 2, t = 0;
    public bool seesPlayer { get; set; }
    
    public enemyState currState;
    public enemyState prevState;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        trackPlayer = GetComponent<TrackPlayer>();
        currRotationWait = rotationWait;
        navMeshAgent = GetComponent<NavMeshAgent>();
        currState = enemyState.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        if (prevState != currState)
        {
            if (currState == enemyState.Idle)
            {
                StartCoroutine(nameof(idling));
            } else if (currState == enemyState.Rotating)
            {
                StartCoroutine(nameof(rotateEnemy));
            } else if (currState == enemyState.Attacking)
            {
                StartCoroutine(nameof(Attacking));
            }
            prevState = currState;
        }
    }

    IEnumerator idling()
    {
        while (currRotationWait > 0)
        {
            currRotationWait -= Time.deltaTime;
            yield return null;
        }
        if (currRotationWait <= 0) currState = enemyState.Rotating;
        rotationAngle = Random.Range(-180, 180);
    }

    IEnumerator Attacking()
    {
        while (seesPlayer)
        {
            transform.LookAt(player.position);
            navMeshAgent.destination = player.position;
            yield return null;
        }
        currState = enemyState.Idle;
    }
    
    IEnumerator rotateEnemy()
    {
        //Debug.Log("transform rotation: " + transform.rotation);
        //Debug.Log("Rotation target: " + rotationTarget);
        Quaternion startRotation = transform.rotation;
        float endYRot = rotationAngle;

        while (t < rotationTime)
        {
            t = Mathf.Min(rotationTime, t + Time.deltaTime/rotationTime);
            Vector3 newEulerOffset = Vector3.up * (endYRot * t);      
            transform.rotation = Quaternion.Euler(newEulerOffset) * startRotation;
            if (player != null)
            {
                currState = enemyState.Attacking;
            }
            yield return null;
        } 
        
        currRotationWait = rotationWait;
        t = 0;
        currState = enemyState.Idle;

        yield return new WaitForSeconds(rotationWait);
    }
}
