using UnityEngine;

public class WeaponSystem : MonoBehaviour
{
    public WeaponData currentWeapon;
    public Transform shootPoint;
    public int team; // 👈 IMPORTANTE

    private float nextFireTime;

    public void Shoot()
    {
    
        if (Time.time < nextFireTime) return;
        nextFireTime = Time.time + currentWeapon.fireRate;

        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        Vector3 targetPoint;

        if (Physics.Raycast(ray, out RaycastHit hit, 100f))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(100f);
        }

        Vector3 direction = (targetPoint - shootPoint.position).normalized;

        GameObject bullet = Instantiate(
            currentWeapon.bulletPrefab,
            shootPoint.position,
            Quaternion.LookRotation(direction)
        );

        bullet.GetComponent<Bullet>().team = team;



        AudioSource.PlayClipAtPoint(currentWeapon.shootSound, transform.position);
    }
    public void ShootAt(Vector3 targetPoint)
    {
        if (Time.time < nextFireTime) return;

        nextFireTime = Time.time + currentWeapon.fireRate;

        Vector3 direction = (targetPoint - shootPoint.position).normalized;

        GameObject bullet = Instantiate(
            currentWeapon.bulletPrefab,
            shootPoint.position,
            Quaternion.LookRotation(direction)
        );

        bullet.GetComponent<Bullet>().team = team;
    }
}