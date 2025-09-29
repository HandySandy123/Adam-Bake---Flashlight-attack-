using UnityEngine;

namespace Enemy_Scripts
{
    [RequireComponent(typeof(ZombieMovement))]
    public class TrackPlayer : MonoBehaviour
    {
        LayerMask mask;

        [SerializeField] private int rayAmount = 7;
        public Transform player;

        [SerializeField] private float rayAngle = 10;
        [SerializeField] private float rayDistance = 999f;
        private ZombieMovement movement;
    
    
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            mask = LayerMask.GetMask("Player");
            movement = GetComponent<ZombieMovement>();
            //rotationAngle = Random.Range(-0, 2* Mathf.PI);
        }

        void FixedUpdate()
        {
            checkRays();
        }

        /*
         * SUMMARY: If returns true, enemy has spotted player
         */
        public bool checkRays()
        {
            for (var i = 0; i < rayAmount; i++)
            {
                RaycastHit hit;
                var shot = new Ray(transform.position,
                    Quaternion.Euler(0, (i - rayAmount / 2.0f) * rayAngle, 0) * transform.rotation *
                    new Vector3(0, 0, 1));
                if (Physics.Raycast(shot, out hit, rayDistance, mask))
                {
                    drawRay(shot, Color.yellow);
                    player = hit.transform;
                    movement.player = player;
                    movement.seesPlayer = true;
                    return true;
                }
            }
            player = null;
            movement.seesPlayer = false;
            return false;
        }
    
        private void drawRay(Ray ray, Color rayColor)
        {
            //ONLY VISIBLE IN GIZMOS VIEW
            Debug.DrawRay(ray.origin, ray.direction * rayDistance, rayColor, 0.2f);
        }
    }
}
