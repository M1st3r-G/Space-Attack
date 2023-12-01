using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //ComponentReferences
    [SerializeField] private GameObject enemy;
    //Params
    [SerializeField] private Vector2Int grid;
    [SerializeField] private Vector2 spawnPostion;
    [SerializeField] private float padding;
    //Temps
    private List<EnemyController> allEnemies;
    //Publics
    private static GameManager _instance;
    public static GameManager Instance => _instance;
     
    private void Awake()
    {
        if (_instance is not null)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        
        allEnemies = new List<EnemyController>();
        SpawnEnemies();
        
        DontDestroyOnLoad(this);
    }

    private void SpawnEnemies()
    {
        var offset = (Vector2)(grid - new Vector2Int(1,1)) * (1 + padding) / 2;
        for (int i = 0; i < grid.x; i++)
        {
            for (int j = 0; j < grid.y; j++)
            {
                Vector2 pos = new Vector2(i*(1+padding), j*(1+padding)) - offset;
                GameObject tmp = Instantiate(enemy, pos , Quaternion.identity);
                var deb = tmp.GetComponent<EnemyController>();
                print(deb.name);
                allEnemies.Add(deb);
            }
        }
    }
}