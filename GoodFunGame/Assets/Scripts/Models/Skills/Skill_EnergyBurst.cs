using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_EnergyBurst : SkillBase
{
    GameObject _energyBurstWind;
    public override void Initialize()
    {
        base.Initialize();
        Cooldown = 8f;
    }

    public override bool Activate()
    {
        if (!base.Activate()) return false;

        // 이펙트
        _energyBurstWind = Main.Resource.InstantiatePrefab("EnergyBurstWind.prefab");
        StartCoroutine(DestroyPrefab());

        // 모든 projectile 없애기
        Main.Object.DespawnAllProjectile();

        // enemy 데미지
        List<Enemy> enemies = Main.Object.Enemies;

        for (int i = 0; i < enemies.Count; ++i)
            enemies[i].OnHit(Main.Object.Player);

        return true;
    }

    IEnumerator DestroyPrefab()
    {
        yield return new WaitForSeconds(2f);
        Main.Resource.Destroy(_energyBurstWind);
    }
}
