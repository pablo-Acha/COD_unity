using UnityEngine;
using UnityEngine.AI;

public class BotController : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform target;
    public float visionRange = 15f;
    public LayerMask obstacleMask;
    public float minDistance = 5f;   // muy cerca → retrocede
    public float maxDistance = 10f;  // muy lejos → se acerca
    public WeaponSystem weapon;

    void Update()
    {
        SearchEnemy();

        if (target != null)
        {
            float distance = Vector3.Distance(transform.position, target.position);

            // 🔥 MOVIMIENTO SEGÚN DISTANCIA
            if (distance > maxDistance)
            {
                // 👉 acercarse
                agent.SetDestination(target.position);
            }
            else if (distance < minDistance)
            {
                // 👉 alejarse
                Vector3 dir = (transform.position - target.position).normalized;
                Vector3 newPos = transform.position + dir * 5f;

                agent.SetDestination(newPos);
            }
            else
            {
                // 👉 quedarse quieto
                agent.ResetPath();
            }

            // 🔥 ROTACIÓN SUAVE (mejor que LookAt)
            Vector3 lookDir = target.position - transform.position;
            lookDir.y = 0;

            Quaternion rot = Quaternion.LookRotation(lookDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * 5f);

            // 🔫 APUNTADO DEL ARMA
            Vector3 direction = (target.position - weapon.shootPoint.position).normalized;
            weapon.shootPoint.rotation = Quaternion.LookRotation(direction);

            // 🔥 DISPARO SOLO SI ESTÁ EN RANGO
            if (distance <= maxDistance)
            {
                weapon.ShootAt(target.position);
            }
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