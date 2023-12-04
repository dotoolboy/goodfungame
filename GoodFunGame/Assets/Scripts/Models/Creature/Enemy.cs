
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : Creature 
{
    #region Properties

    public EnemyData.EnemyKey enemyType;
    public int hp;
    public float speed;
    public int currentHp;
    public int damage;
    public EnemyData.FireType fireType;


    #endregion

    #region Fields

    private EnemyData _enemyData;
    private DataManager _dataManager;
    #endregion

    #region MonoBehaviours

    private void Start()
    {
        _dataManager = ServiceLocator.GetService<DataManager>();
    }
    #endregion

    #region Initialize / Set
    public override bool Initialize() 
    {
        if (base.Initialize() == false) return false;
        foreach (var enemy in _dataManager.Enemies)
        {
            Debug.Log(enemy.Key);
            // SetInfo(enemy.Key);
        }

        return true;
    }

    public override void SetInfo(string key) {
        base.SetInfo(key);

        var enemy = Main.Data.Enemies.FirstOrDefault(e => e.Key == key).Value;
        enemyType = (EnemyData.EnemyKey)Enum.Parse(typeof(EnemyData.EnemyKey), enemy.keyName);
        hp = enemy.hp;
        currentHp = hp;
        speed = enemy.speed;
        damage = enemy.damage;
    }
    #endregion

    #region State
    protected override void OnStateEntered_Dead() 
    {
        base.OnStateEntered_Dead();

        // TODO:: Player의 KillCount 증가.

        // TODO:: 오브젝트 디스폰

    }
    #endregion
}