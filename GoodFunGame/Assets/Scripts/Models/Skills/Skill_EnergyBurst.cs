using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_EnergyBurst : SkillBase
{
    public override void Initialize()
    {
        base.Initialize();
        Cooldown = 20f;
    }

    public override bool Activate()
    {
        if (!base.Activate()) return false;

        // 이펙트

        // enemyProjectile 모두 Despawn
        Main.Object.DespawnAllProjectile();

        // enemy 데미지


        return true;
    }
}
