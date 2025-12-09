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
    [SerializeField] private float currRotationWait, rotationAngle, rotationTime = 2;
    private float t = 0;
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
            switch (currState)
            {
                case enemyState.Attacking :
                    Attacking();
                    break;
                case enemyState.Idle :
                    Idle();
                    break;
                case enemyState.Rotating :
                    StartCoroutine(rotateEnemy());
                    break;
            }
            
            prevState = currState;
        }
    }

    void Idle()
    {
        StopAllCoroutines();
        if (currRotationWait > 0)
        {
            currRotationWait -= Time.deltaTime;
        }
        else
        {
            currState = enemyState.Rotating;
        }
        rotationAngle = Random.Range(-180, 180);
    }

    void Attacking()
    {
        StopAllCoroutines();
        if (seesPlayer && navMeshAgent.enabled)
        {
            transform.LookAt(player.position);
            navMeshAgent.SetDestination(player.position);
        }
        currState = enemyState.Idle;
    }
    
    IEnumerator rotateEnemy()
    {
        //Debug.Log("transform rotation: " + transform.rotation);
        //Debug.Log("Rotation target: " + rotationTarget);
        Quaternion startRotation = transform.rotation;
        float endYRot = rotationAngle;
        Debug.Log(gameObject.name + " Is rotating");

        while (t < rotationTime)
        {
            t = Mathf.Min(rotationTime, t + Time.deltaTime/rotationTime);
            Vector3 newEulerOffset = Vector3.up * (endYRot * t);      
            transform.rotation = Quaternion.Euler(newEulerOffset) * startRotation;
            if (player != null)
            {
                currState = enemyState.Attacking;
                StopAllCoroutines();
            }
            
            yield return null;
        } 
        StopAllCoroutines();
        currRotationWait = rotationWait;
        t = 0;
        currState = enemyState.Idle;

        yield return new WaitForSeconds(rotationWait);
    }
}
