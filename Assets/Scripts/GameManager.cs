using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    [SerializeField] private GameObject damageImageGo;
    private Image damageImage;
    public GameObject player;

    public GameObject[] enemies;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        
        
        if (instance != null && instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            instance = this; 
        } 
    }

    void Start()
    {
        
    }

    void setPlayer(GameObject player)
    {
        this.player = player;
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
