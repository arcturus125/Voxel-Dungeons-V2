using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public enum spawnTypes
    {
        pointSpawn,
        areaSpawn,
    }

    private float time = 0.0f;
    public float spawnTimer = 0.0f;
    public float spawnRadius = 10.0f;
    public spawnTypes spawnType;

    public GameObject prefab;

    public int maxEnemies = 5;
    public List<GameObject> spawnedEnemies = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (spawnType == spawnTypes.pointSpawn)
        {
            pointSpawn();
        }
        else
        {
            areaSpawn();
        }
    }

    void pointSpawn()
    {
        if (time > spawnTimer)
        {
            if (spawnedEnemies.Count < maxEnemies)
            {
                spawnEnemy(this.transform.position);
                time = 0.0f;
            }
        }
    }

    void areaSpawn()
    {
        if (time > spawnTimer && spawnedEnemies.Count < maxEnemies)
        {
            //Choose random angle
            float angle = UnityEngine.Random.Range(0.0f, 360.0f);
            angle *= Mathf.Deg2Rad;


            //Choose random length
            float length = UnityEngine.Random.Range(0.0f, spawnRadius);

            //Find point at angle and length

            float y = RaycastY(new Vector3(
                this.transform.position.x + length * Mathf.Cos(angle),
                this.transform.position.y,
                this.transform.position.z + length * Mathf.Sin(angle)));
            //Spawn enemy
            spawnEnemy(new Vector3(
                this.transform.position.x + length * Mathf.Cos(angle),
                y,
                this.transform.position.z + length * Mathf.Sin(angle)));
            time = 0.0f;
        }
    }
    
    void spawnEnemy(Vector3 pPosition)
    {
        GameObject enemy = Instantiate(prefab);
        enemy.transform.position = pPosition;
        enemy.GetComponentInChildren<EnemyComponent>().home = this;
        spawnedEnemies.Add(enemy);
    }

    float RaycastY(Vector3 enemySpawnPos)
    {
        RaycastHit ray;
        //origin, direction, ray, distance;

        if(Physics.Raycast(enemySpawnPos, Vector3.down, out ray, Mathf.Infinity))
        {
            if (ADMIN.Debug_Mode)
            {
                Debug.DrawRay(enemySpawnPos, Vector3.down * ray.distance, Color.cyan, 5.0f);
                
            }
            return ray.point.y;
        }
        return enemySpawnPos.y;
    }

    public void removeFromHome(GameObject enemy)
    {
        if(spawnedEnemies.Remove(enemy))
        {
            Debug.Log("Enemy removed");
        }
        else
        {
            Debug.Log("enemy NOT removed");
        }
    }
}
