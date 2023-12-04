using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager
{

    public Player Player { get; private set; }
    public List<Enemy> Enemies { get; private set; } = new();

    public T Spawn<T>(string key, Vector2 position) where T : Thing
    {
        System.Type type = typeof(T);

        if (type == typeof(Player))
        {
            return null;
        }
        else if (type == typeof(Enemy))
        {
            return null;
        }

        return null;
    }

    public void Despawn<T>(T obj) where T : Thing
    {

    }
}

