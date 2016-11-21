using UnityEngine;
using System.Collections;

public class SpawnHero : MonoBehaviour {

    public int secondBeforeHeroSpawn;
    public GameObject Hero;
    public Transform spawnPoints;

    float start;


    // Use this for initialization
    void Start () {
        start = Time.time;
    }

    void Update()
    {
        if (Time.time - start > secondBeforeHeroSpawn)
        {
            spawning();
        }
    }

    void spawning()
    {
        GameObject trapObj = Instantiate(Hero);
        print(spawnPoints.position);
        Hero.transform.position = spawnPoints.position;
        Destroy(this.gameObject);
    }
}
