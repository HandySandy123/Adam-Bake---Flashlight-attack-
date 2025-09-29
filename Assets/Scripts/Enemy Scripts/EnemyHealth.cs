using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]private float health = 6000f;
    [SerializeField]private GameObject particlesGameObject, meshObject;
    private ParticleSystem particles;
    private float time = 0;
    public float iFrameDur = 2000f;
    private Rigidbody rb;
    public bool isAttacked = false;

    void Start()
    {
        particles = particlesGameObject.GetComponent<ParticleSystem>();
        rb = GetComponent<Rigidbody>();
    }
    public void takeDamage(float damage)
    {
        if(isAttacked == true)
        {
            health -= damage;
            Debug.Log("Taking Damage");

        }
        if (health <= 0)
        {
            StartCoroutine(Die());
        }
    }

    private IEnumerator Die()
    {
        particles.Play();
        yield return new WaitForSeconds(particles.main.duration + particles.main.startLifetime.constantMax);
        meshObject.SetActive(false);
        Debug.Log("Monster killed!");
        Destroy(this.gameObject);
        yield return null;
    }
}
