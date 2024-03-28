using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpwan : MonoBehaviour
{
    [Header("ZOmbieSpawn Var")]
    public GameObject zombiePrefabs;
    public Transform zombieSpawnPosition;
    //public GameObject dangerZone;
    public float repeatCycle = 1f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            InvokeRepeating(nameof(EnemySpawner), 1f, repeatCycle);
            Destroy(gameObject, 10f);
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }

    void EnemySpawner()
    {
        Instantiate(zombiePrefabs, zombieSpawnPosition.position, zombieSpawnPosition.rotation);
    }
}
