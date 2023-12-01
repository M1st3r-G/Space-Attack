using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    //ComponentReferences
    [SerializeField] private GameObject enemy;
    [SerializeField] private TextMeshProUGUI lifeText;
    [SerializeField] private TextMeshProUGUI pointsText;
    //Params
    [SerializeField] private Vector2Int grid;
    [SerializeField] private Vector2 spawnPostion;
    [SerializeField] private float padding;
    [SerializeField, Range(1,3)] private int typeToSpawn;
    [SerializeField] private EnemyStats[] types;
    
    //Temps
    private List<Enemy> allEnemies;
    private int _points;
    private int Points
    {
        get => _points;
        set
        {
            pointsText.text = value.ToString();
            _points = value;
        }
    }

    private int _life;
    private int Life
    {
        get => _life;
        set
        {
            lifeText.text = value.ToString();
            _life = value;
        }
    }
    
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
        
        allEnemies = new List<Enemy>();
        Points = 0;
        Life = 3;
        SpawnEnemies();
        
        DontDestroyOnLoad(this);
    }

    private void OnEnable()
    {
        Enemy.OnHit += OnHitMethod;
        Player.OnPlayerHit += OnPlayerHitMethod;
    }

    private void OnDisable()
    {
        Enemy.OnHit -= OnHitMethod;
        Player.OnPlayerHit -= OnPlayerHitMethod;

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
                var deb = tmp.GetComponent<Enemy>();
                deb.setStats(types[typeToSpawn]);
                allEnemies.Add(deb);
            }
        }
    }

    private void OnHitMethod(Enemy e)
    {
        allEnemies.Remove(e);
        Points++;
    }

    private void OnPlayerHitMethod()
    {
        Life--;
    }
}