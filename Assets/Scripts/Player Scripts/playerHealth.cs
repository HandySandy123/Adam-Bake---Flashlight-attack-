using UnityEngine;
using System.Threading.Tasks;

public class playerHealth : MonoBehaviour
{
    public int health = 4;
    private bool damaged = false;

    private float healthRegainTimer = 3f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameManager.instance.player = this.gameObject;
    }

    public void takeDamage(int damage)
    {
        health -= damage;
        damaged = true;
        
    }
    
    async Task regainHealth()
    {
        Debug.Log("Wait for 3 seconds");
        await Task.Delay((int)(healthRegainTimer * 1000f));
        health++;
        if (health == 4)
        {
            damaged = false;
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        if (damaged)
        {
            regainHealth();
        }

        
        if (health <= 0)
        {
            Debug.Log("Player Dead");
        }
    }
}
