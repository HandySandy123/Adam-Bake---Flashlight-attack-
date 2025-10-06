using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    [SerializeField] private GameObject damageImageGo;
    Image damageImage;
    public GameObject player;

    public GameObject[] enemies;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        damageImage = damageImageGo.GetComponent<Image>();
        damageImage.color = new Color(1f, 1f, 1f, 0f);
        
        
        if (instance != null && instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            instance = this; 
        } 
    }

    private void bloodyScreen()
    {
        switch (player.GetComponent<playerHealth>().health)
        {
            case 4: damageImage.color = new Color(1f, 1f, 1f, 0f); 
                break;
            case 3: damageImage.color = new Color(1f, 1f, 1f, 0.25f);
                break;
            case 2: damageImage.color = new Color(1f, 1f, 1f, 0.5f);
                break;
            case 1: damageImage.color = new Color(1f, 1f, 1f, 1f);
                break;
        }
    }

    public void resetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            resetLevel();
        }
    }
}
