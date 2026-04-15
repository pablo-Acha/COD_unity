using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;

    public Transform[] team1Spawns;
    public Transform[] team2Spawns;

    void Awake()
    {
        instance = this;
    }

    public Vector3 GetSpawnPoint(int team)
    {
        if (team == 1)
        {
            int index = Random.Range(0, team1Spawns.Length);
            return team1Spawns[index].position;
        }
        else
        {
            int index = Random.Range(0, team2Spawns.Length);
            return team2Spawns[index].position;
        }
    }
}