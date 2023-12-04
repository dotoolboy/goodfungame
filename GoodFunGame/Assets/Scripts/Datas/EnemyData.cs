using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class EnemyData
{
    /// <summary>
    ///  적 Data를 사용하기 위한 Unique Key
    /// </summary>
    public enum EnemyKey
    {
        BOSS_MWJ,    // 문원정님
        SOLDIER1_MWJ,
        SOLDIER2_MWJ,
        SOLDIER3_MWJ,
        
        BOSS_CHH,    // 최현호님
        SOLDIER1_CHH,
        SOLDIER2_CHH,
        SOLDIER3_CHH,
        
        BOSS_LJS,    // 이정훈님
        SOLDIER1_LJH,
        SOLDIER2_LJH,
        SOLDIER3_LJH,
        
        BOSS_JEH,    // 전은하님
        SOLDIER1_JEH,
        SOLDIER2_JEH,
        SOLDIER3_JEH,
        
        BOSS_KSJ,     // 김세진님
        SOLDIER1_KSJ,
        SOLDIER2_KSJ,
        SOLDIER3_KSJ,
    }

    public enum FireType { BOSS, STRAIGHT, CIRCLE, SECTORGORM }
    public EnemyKey enemyType;
    public string keyName;
    public float speed;
    public int hp;
    public int damage;
    public string fireType;
}

[Serializable]
public class EnemyDataLoader : ILoadData<string, EnemyData>
{
    public List<EnemyData> enemies = new List<EnemyData>();
    public Dictionary<string, EnemyData> MakeData()
    {
       return enemies.ToDictionary(enemy => enemy.keyName);
    }
}
