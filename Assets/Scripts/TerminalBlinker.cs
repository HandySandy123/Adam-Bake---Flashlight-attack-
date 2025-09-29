using UnityEngine;

public class TerminalBlinker : MonoBehaviour
{
    // Blends between two materials

    [SerializeField] Material material1, material2;
    public float duration = 2.0f;
    Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer> ();

        // At start, use the first material
        rend.materials[1] = material1;
        float dur = Mathf.Abs(transform.position.x % 1 * transform.position.y % 1);
        dur %= 10;
        duration = dur;
    }

    void Update()
    {
        // ping-pong between the materials over the duration
        float lerp = Mathf.PingPong(Time.time, duration) / duration % 1;
        rend.materials[1].Lerp(material1, material2, lerp);
    }
}
