using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]private float health = 100f;
    [SerializeField]private GameObject particlesGameObject, meshObject;
    private ParticleSystem particles;
    NavMeshAgent agent;
    public float iFrameDur = 0.5f;
    private float iFrame = 0f;
    [SerializeField] private bool invulnerable = false, dying = false;
    private Rigidbody rb;

    void Start()
    {
        particles = particlesGameObject.GetComponent<ParticleSystem>();
        agent = GetComponent<NavMeshAgent>();
    }
    public void takeDamage(float damage)
    {
        if (iFrame <= iFrameDur && !invulnerable && !dying)
        {
            invulnerable = true;
            StartCoroutine(startIFrame(damage));
        }
        if (health <= 0)
        {
            dying = true;
            StartCoroutine(Die());
        }
    }

    IEnumerator startIFrame(float damage)
    {
        health -= damage;
        Debug.Log("Taking Damage");
        yield return new WaitForSeconds(iFrameDur);
        invulnerable = false;
    }

    private IEnumerator Die()
    {
        Debug.Log(gameObject.name + " dying now");
        particles.Play();
        agent.enabled = false;
        rb = gameObject.AddComponent<Rigidbody>();
        yield return new WaitForSeconds(particles.main.duration + particles.main.startLifetime.constantMax);
        meshObject.SetActive(false);
        Debug.Log("Monster killed!");
        Destroy(this.gameObject);
        yield return null;
    }
}
