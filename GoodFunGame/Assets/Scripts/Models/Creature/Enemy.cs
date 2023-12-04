using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Creature {

    #region Properties



    #endregion

    #region Fields

<<<<<<< Updated upstream


=======
    private EnemyData _enemyData;
    private DataManager _dataManager;
>>>>>>> Stashed changes
    #endregion

    #region MonoBehaviours

<<<<<<< Updated upstream

=======
    private void Start()
    {
        _dataManager = ServiceLocator.GetService<DataManager>();
    }
>>>>>>> Stashed changes

    #endregion

    #region Initialize / Set

    public override bool Initialize() {
        if (base.Initialize() == false) return false;
<<<<<<< Updated upstream
=======
        foreach (var enemy in _dataManager.Enemies)
        {
            Debug.Log(enemy.Key);
            // SetInfo(enemy.Key);
        }
>>>>>>> Stashed changes

        return true;
    }

<<<<<<< Updated upstream
    public override void SetInfo(string key) {
        base.SetInfo(key);


=======
    public override void SetInfo(string key)
    {
        var enemy = Main.Data.Enemies.FirstOrDefault(e => e.Key == key).Value;
        enemyType = (EnemyData.EnemyKey)Enum.Parse(typeof(EnemyData.EnemyKey), enemy.keyName);
        hp = enemy.hp;
        currentHp = hp;
        speed = enemy.speed;
        damage = enemy.damage;
>>>>>>> Stashed changes
    }

    #endregion

    #region State

    protected override void OnStateEntered_Dead() {
        base.OnStateEntered_Dead();

        // TODO:: Player의 KillCount 증가.

        // TODO:: 오브젝트 디스폰
    }

    #endregion
}