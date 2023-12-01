using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/EnemyData")]
public class EnemyStats: ScriptableObject
{
    [SerializeField] private Sprite image;
    [SerializeField] private float defaultSpeed;
    [SerializeField] private float defaultShootingSpeed;
    [SerializeField] private float defaultLineTimer;

    public Sprite getImage() => image;
    public float getDefaultSpeed() => defaultSpeed;
    public float getDefaultShootingSpeed() => defaultShootingSpeed;
    public float getDefaultLineTimer() => defaultLineTimer;
}
