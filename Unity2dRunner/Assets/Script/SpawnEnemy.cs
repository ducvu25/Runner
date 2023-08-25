using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [Header("--------------Enemy-----------")]
    [SerializeField] GameObject[] listEnemy;
    [SerializeField] Transform pointSpawn;
    [SerializeField] Transform pointEnd;
    float time_spawn;
    float m_time_spawn = 0;
    [SerializeField] float speedEnemy = 2f;
    float lv;
    List<GameObject> enemies;
    // Start is called before the first frame update
    void Start()
    {
        enemies = new List<GameObject>();
        GameController.Instance.onPlay.AddListener(Setting);
    }
    public void Setting()
    {
        lv = 1;
        time_spawn = GameController.Instance.TimeSpawn();
    }
    // Update is called once per frame
    void Update()
    {
        if (!GameController.Instance.isPlaying)
        {
            if(enemies.Count > 0)
            {
                for (int i = 0; i < enemies.Count; i++)
                {
                    Destroy(enemies[i]);
                }
                enemies.Clear();
            }
            return;
        }
        Spawn();
        DeleteEnemy();
    }
    void Spawn()
    {
        if (m_time_spawn > 0)
        {
            m_time_spawn -= Time.deltaTime;
        }
        else
        {
            m_time_spawn = time_spawn;
            GameObject enemy = Instantiate(listEnemy[Random.Range(0, listEnemy.Length)], pointSpawn.position, Quaternion.identity);
            enemy.GetComponent<Rigidbody2D>().velocity = Vector3.left * speedEnemy*lv;
            enemy.transform.localScale = new Vector3(Random.RandomRange(0.7f, 1.5f), Random.RandomRange(0.7f, 2f), 1);
            enemies.Add(enemy);
        }
    }
    void DeleteEnemy()
    {
        if (enemies.Count > 0 && enemies[0].transform.position.x < pointEnd.localPosition.x)
        {
            Destroy(enemies[0]);
            enemies.RemoveAt(0);
            DeleteEnemy();
            lv += 0.05f;
            time_spawn *= 0.965f;
            AudioController.instance.PlaySound((int)AudioSetting.o);
        }
    }
}

