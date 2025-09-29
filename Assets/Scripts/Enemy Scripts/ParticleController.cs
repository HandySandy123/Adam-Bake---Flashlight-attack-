using UnityEngine;

public class ParticleController : MonoBehaviour
{
    public ParticleSystem ps;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    public void PlayParticles()
    {
        Debug.Log("Playing Particles");
        ps.Play();
    }
}
