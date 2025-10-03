using UnityEngine;

public class playerHealth : MonoBehaviour
{
    public int health = 4;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameManager.instance.player = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
