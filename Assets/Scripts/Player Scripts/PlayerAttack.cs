using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Transform flashLight;
    private Light playerLight;
    private float  minOuter, maxOuter, angleOffset = 5.0f;
    [SerializeField]private float attackSpeed = 10f;
    public bool isAttacking = false;
    private readonly float attackStrength = 6;
    private float attackModifier = 1.125f;
    private float currAttackStrength;
    // public int rayAmount = 5;
    // public float rayAngle = 10; 
    public float rayDistance = 8f;
    public bool targetHit;
    [SerializeField] private GameObject enemy;

    void Start()
    {
        playerLight =  flashLight.GetComponent<Light>();
        
        maxOuter = playerLight.spotAngle;
        minOuter = playerLight.innerSpotAngle + angleOffset;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            isAttacking = true;
            StartCoroutine(AimLight());
        }
        if (Input.GetMouseButtonUp(0))
        {
            isAttacking = false;
            StartCoroutine(RelaxLight());
        }
        if (isAttacking)
        {
            currAttackStrength = attackStrength * attackModifier;
        }
        else
        {
            currAttackStrength = attackStrength;
        }
        
        RaycastHit hit;
        var shot = new Ray(flashLight.transform.position, flashLight.transform.forward * rayDistance);
        drawRay(shot, Color.green);

        if (Physics.Raycast(shot, out hit, rayDistance))
        {
            if (hit.collider.gameObject.CompareTag("Enemy"))
            {
                enemy = hit.collider.gameObject;
                // if(enemy.TryGetComponent<EnemyHealth>(out EnemyHealth health))
                // {
                //     health.takeDamage(Attack());
                //     
                // }
            } 
            targetHit = true;
        }
        else
        {
            enemy = null;
            targetHit = false;
        }

    }

    public float Attack()
    {
        return currAttackStrength;
    }

    private IEnumerator RelaxLight()
    {
        while (!isAttacking)
        {
            playerLight.spotAngle = Mathf.Lerp(playerLight.spotAngle, maxOuter, Time.deltaTime * attackSpeed);
            yield return null;
        }
    }

    IEnumerator AimLight()
    {
        while (isAttacking)
        {
            playerLight.spotAngle = Mathf.Lerp(playerLight.spotAngle, minOuter, Time.deltaTime * attackSpeed);
            yield return null;
        }
         
        yield return null;
    }
    private void drawRay(Ray ray, Color rayColor)
    {
        //ONLY VISIBLE IN GIZMOS VIEW
        Debug.DrawRay(ray.origin, ray.direction * rayDistance, rayColor, 0.2f);
    }
}
