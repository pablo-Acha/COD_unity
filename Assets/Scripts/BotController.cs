using UnityEngine;
using UnityEngine.AI;

public class BotController : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform target;
    public float visionRange = 15f;
    public LayerMask obstacleMask;

    public WeaponSystem weapon;

    void Update()
    {
        SearchEnemy();

        if (target != null)
        {
            agent.SetDestination(target.position);

            transform.LookAt(target);

            weapon.Shoot();
        }
    }

    void SearchEnemy()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, visionRange);

        foreach (var hit in hits)
        {
            HealthSystem h = hit.GetComponent<HealthSystem>();

            if (h != null && h.team != GetComponent<HealthSystem>().team)
            {
                if (!Physics.Linecast(transform.position, hit.transform.position, obstacleMask))
                {
                    target = hit.transform;
                    return;
                }
            }
        }

        target = null;
    }
}