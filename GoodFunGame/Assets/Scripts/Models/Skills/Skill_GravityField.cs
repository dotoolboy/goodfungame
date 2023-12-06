using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Skill_GravityField : SkillBase
{
    private float _fieldRadius = 7.0f;
    private float _attractionForce = 10.0f;

    public override void Initialize()
    {
        base.Initialize();

        Cooldown = 2f;
    }

    public override bool Activate()
    {
        if (!base.Activate()) return false;

        Main.Object.Player.Invincible = true;

        Collider2D[] hits = Physics2D.OverlapCircleAll(this.transform.position, _fieldRadius);
        Rigidbody2D[] rigids = hits.Where(x => x.CompareTag("EnemyProjectile")).Select(x => x.GetComponent<Rigidbody2D>()).ToArray();

        for (int i = 0; i < rigids.Length; i++)
        {
            Vector3 direction = (this.transform.position - rigids[i].transform.position).normalized;
            rigids[i].AddForce(direction * _attractionForce, ForceMode2D.Impulse);
        }

        return true;
    }
}