using UnityEngine;

public class StairChecker : MonoBehaviour
{
    Ray downRay;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        downRay = new Ray(transform.position, -transform.up);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(downRay, out RaycastHit hit, Mathf.Infinity))
        {
            if (hit.collider.gameObject.CompareTag("Stair"))
            {
                LowerPlayer(hit);
            }
        }
    }

    void LowerPlayer(RaycastHit hit)
    {
        transform.position = hit.point;
    }
}
