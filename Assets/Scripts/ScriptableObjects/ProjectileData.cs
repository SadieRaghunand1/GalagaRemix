using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu]
public class ProjectileData : ScriptableObject
{
    //Make different types, player chooses at begining 2, put those 2 in array on player
    public string enemyName;
    public Sprite skin;
    public float damageDealt;
}
