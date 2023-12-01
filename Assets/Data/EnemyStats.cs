using UnityEngine;

[CreateAssetMenu(menuName = "Data/EnemyData")]
public class EnemyStats: ScriptableObject
{
    [SerializeField] private Sprite image;
    [SerializeField] private float defaultSpeed;
    [SerializeField] private float defaultShootingSpeed;
    [SerializeField, Range(0f,1f)] private float shootingProbability;
    [SerializeField] private float defaultLineTimer;
    [SerializeField] private float bulletSpeed;
    

    public Sprite GetImage() => image;
    public float GetDefaultSpeed() => defaultSpeed;
    public float GetDefaultShootingSpeed() => defaultShootingSpeed;
    public float GetDefaultLineTimer() => defaultLineTimer;
    public float GetShootingProbability() => shootingProbability;
    public float GetBulletSpeed() => bulletSpeed;
}
