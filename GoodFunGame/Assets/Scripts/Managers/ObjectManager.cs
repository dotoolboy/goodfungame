using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectManager
{

    public Player Player { get; private set; }
    public List<Enemy> Enemies { get; private set; } = new();
    public List<Projectile> Projectiles { get; private set; } = new();

    public List<GameObject> ExplosionVFX { get; set; } = new();

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

        if (type == typeof(Thing))
        {
            GameObject obj = Main.Resource.InstantiatePrefab("Explosion.prefab", pooling: true);
            obj.transform.position = position;
            ExplosionVFX.Add(obj);
            return obj as T;
        }
        return null;
    }

    public void Despawn<T>(T obj) where T : Thing
    {
        System.Type type = typeof(T);

        if (type == typeof(Player))
        {

        }
        else if (type == typeof(Enemy))
        {
            Enemies.Remove(obj as Enemy);
            Main.Resource.Destroy(obj.gameObject);
        }
        else if (type == typeof(Projectile))
        {
            Projectiles.Remove(obj as Projectile);
            Main.Resource.Destroy(obj.gameObject);
        }
    }
    public void DespawnProjectileGenerator<T>(T obj) where T : ProjectileGenerator
    {
        Main.Resource.Destroy(obj.gameObject);
    }
}

