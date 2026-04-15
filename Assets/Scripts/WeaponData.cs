using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Weapons/Data")]
public class WeaponData : ScriptableObject
{
    public float damage = 30f;
    public float fireRate = 0.2f;
    public GameObject bulletPrefab;
    public AudioClip shootSound;
}