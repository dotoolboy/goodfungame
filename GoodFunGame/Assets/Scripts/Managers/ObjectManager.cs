using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class ObjectManager
{
    public Dictionary<EnemyData.EnemyKey, string[]> ProjectileMappings = new Dictionary<EnemyData.EnemyKey, string[]>
    {
        { EnemyData.EnemyKey.BOSS_MWJ, new[] { "Bullet_1_MWJ", "Bullet_2_MWJ", "Bullet_3_MWJ" } },
        { EnemyData.EnemyKey.SOLDIER1_MWJ, new[] { "Bullet_1_MWJ" } },
        { EnemyData.EnemyKey.SOLDIER2_MWJ, new[] { "Bullet_2_MWJ" } },
        { EnemyData.EnemyKey.SOLDIER3_MWJ, new[] { "Bullet_3_MWJ" } },
        { EnemyData.EnemyKey.BOSS_CHH, new[] { "Bullet_1_CHH", "Bullet_2_CHH", "Bullet_3_CHH" } },
        { EnemyData.EnemyKey.SOLDIER1_CHH, new[] { "Bullet_1_CHH" } },
        { EnemyData.EnemyKey.SOLDIER2_CHH, new[] { "Bullet_2_CHH" } },
        { EnemyData.EnemyKey.SOLDIER3_CHH, new[] { "Bullet_3_CHH" } },
        { EnemyData.EnemyKey.BOSS_LJH, new[] { "Bullet_1_LJH", "Bullet_2_LJH", "Bullet_3_LJH" } },
        { EnemyData.EnemyKey.SOLDIER1_LJH, new[] { "Bullet_1_LJH" } },
        { EnemyData.EnemyKey.SOLDIER2_LJH, new[] { "Bullet_2_LJH" } },
        { EnemyData.EnemyKey.SOLDIER3_LJH, new[] { "Bullet_3_LJH" } },
        { EnemyData.EnemyKey.BOSS_JEH, new[] { "Bullet_1_JEH", "Bullet_2_JEH", "Bullet_3_JEH" } },
        { EnemyData.EnemyKey.SOLDIER1_JEH, new[] { "Bullet_1_JEH" } },
        { EnemyData.EnemyKey.SOLDIER2_JEH, new[] { "Bullet_2_JEH" } },
        { EnemyData.EnemyKey.SOLDIER3_JEH, new[] { "Bullet_3_JEH" } },
        { EnemyData.EnemyKey.BOSS_KSJ, new[] { "Bullet_1_KSJ", "Bullet_2_KSJ", "Bullet_3_KSJ" } },
        { EnemyData.EnemyKey.SOLDIER1_KSJ, new[] { "Bullet_1_KSJ" } },
        { EnemyData.EnemyKey.SOLDIER2_KSJ, new[] { "Bullet_2_KSJ" } },
        { EnemyData.EnemyKey.SOLDIER3_KSJ, new[] { "Bullet_3_KSJ" } },
    };

    public Player Player { get; private set; }
    public List<Enemy> Enemies { get; set; } = new();
    public event Action<int> OnVictory;
    public int KillCount;
    private List<Projectile> Projectiles { get; set; } = new();
    private List<Explosion> ExplosionVFX { get; set; } = new();

    public Transform EnemyParent
    {
        get
        {
            GameObject root = GameObject.Find("@Enemies");
            if (root == null) root = new("@Enemies");
            return root.transform;
        }
    }
    public T SpawnProjectileGenerator<T>() where T : ProjectileGenerator
    {
        GameObject newObject = new("ProjectileGenerator");
        T pg = newObject.GetOrAddComponent<T>();

        return pg as T;
    }

    public T Spawn<T>(string key, Vector2 position) where T : Thing
    {
        System.Type type = typeof(T);

        if (type == typeof(Player))
        {
            GameObject obj = Main.Resource.InstantiatePrefab("Player.prefab");
            obj.transform.position = position;

            Player = obj.GetOrAddComponent<Player>();
            Player.SetInfo(key);

            return Player as T;
        }

        if (type == typeof(Enemy))
        {
            GameObject obj = Main.Resource.InstantiatePrefab("TempEnemy.prefab", pooling: true);
            obj.transform.position = position;
            Enemy enemy = obj.GetOrAddComponent<Enemy>();
            enemy.SetInfo(key);
            Enemies.Add(enemy);
            return enemy as T;
        }

        if (type == typeof(Projectile))
        {
            GameObject obj = Main.Resource.InstantiatePrefab("Projectile_TEMP.prefab", pooling: true);
            obj.transform.position = position;

            Projectile projectile = obj.GetOrAddComponent<Projectile>();
            Projectiles.Add(projectile);

            return projectile as T;
        }

        if (type == typeof(Explosion))
        {
            GameObject obj = Main.Resource.InstantiatePrefab("Explosion.prefab", pooling: true);
            obj.transform.position = position;

            Explosion explosion = obj.GetOrAddComponent<Explosion>();
            ExplosionVFX.Add(explosion);
            return explosion as T;
        }
        return null;
    }

    public void Despawn<T>(T obj) where T : Thing
    {
        Type type = typeof(T);

        if (type == typeof(Player))
        {

        }
        else if (type == typeof(Enemy))
        {
            Enemies.Remove(obj as Enemy);
            CheckForVictory();
            Main.Resource.Destroy(obj.gameObject);
        }
        else if (type == typeof(Projectile))
        {
            Projectiles.Remove(obj as Projectile);
            Main.Resource.Destroy(obj.gameObject);
        }
        else if (type == typeof(Explosion))
        {
            ExplosionVFX.Remove(obj as Explosion);
            Main.Resource.Destroy(obj.gameObject);
        }
    }
    public void DespawnProjectileGenerator<T>(T obj) where T : ProjectileGenerator
    {
        Main.Resource.Destroy(obj.gameObject);
    }

    public void DespawnAllProjectile()
    {
        for (int i = 0; i < Projectiles.Count; ++i)
            Despawn(Projectiles[i]);
    }

    public void Clear()
    {
        Player = null;
        Enemies.Clear();
        Projectiles.Clear();
        ExplosionVFX.Clear();
    }

    private void CheckForVictory()
    {
        KillCount++;
        if (Enemies.Count == 0) OnVictory?.Invoke(KillCount);
    }
}

