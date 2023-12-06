using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.WSA;
using static UnityEngine.GraphicsBuffer;

public class AutoTargetSkill : MonoBehaviour
{
    GameObject autoProjectile;

    public void Activate()
    {
        autoProjectile = Main.Resource.InstantiatePrefab("AutoProjectile.prefab");
        autoProjectile.transform.position = transform.position;
    }
}
