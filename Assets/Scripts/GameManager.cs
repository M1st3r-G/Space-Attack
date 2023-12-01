using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    private float curStrength;
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
        curStrength = 1;
        SpawnEnemies(1,false);
        
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

    private void SpawnEnemies(int str, bool random)
    {
        var offset = (Vector2)(grid - new Vector2Int(1,1)) * (1 + padding) / 2;
        for (int i = 0; i < grid.x; i++)
        {
            for (int j = 0; j < grid.y; j++)
            {
                Vector2 pos = new Vector2(i*(1+padding), j*(1+padding)) - offset + spawnPostion;
                GameObject tmp = Instantiate(enemy, pos , Quaternion.identity);
                var deb = tmp.GetComponent<Enemy>();
                deb.SetStats(types[random ? Random.Range(1, Mathf.Clamp(str + 1, 1, 3)) : str]);
                allEnemies.Add(deb);
            }
        }
    }

    private void OnHitMethod(Enemy e)
    {
        allEnemies.Remove(e);
        Points++;
        if (!AnyEnemyActive())
        {
            LoadNextScene();
        }
    }

    private void LoadNextScene()
    {
        curStrength += 0.5f;
        foreach (Enemy e in allEnemies)
        {   
            Destroy(e.gameObject);
        }
        allEnemies = new List<Enemy>();
        SpawnEnemies((int)curStrength, curStrength%1 != 0);
        if (curStrength == 4) SceneManager.LoadScene(0);
    }
    
    private void OnPlayerHitMethod()
    {
        Life--;
    }

    private bool AnyEnemyActive()
    {
        return allEnemies.Any(e => e.gameObject.activeSelf);
    }
}