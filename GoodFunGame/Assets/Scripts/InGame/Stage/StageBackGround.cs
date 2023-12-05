using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageBackGround : MonoBehaviour
{
    [Serializable]
    public class LayerSet
    {
        public Transform bottomLayer;
        public Transform middleLayer;
        public Transform topLayer;
        public float BottomSpeed { get; set; } = 0.5f;
        public float MiddleSpeed { get; set; }= 1f;
        public float TopSpeed{ get; set; } =1.5f;
    }

    [SerializeField] private List<LayerSet> layerSets;

    private float CameraHeight { get; set; }
    private Transform CameraTransform { get; set; }
    private float ResetPosition{ get; set; }

    private void Awake()
    {
        if (Camera.main == null)
        {
            return;
        }

        Camera main = Camera.main;
        CameraHeight = main.orthographicSize * 2;
        CameraTransform = main.transform;
        ResetPosition = CameraTransform.position.y + CameraHeight;
    }

    private void Update()
    {
        foreach (LayerSet layerSet in layerSets)
        {
            Scrolling(layerSet.bottomLayer, layerSet.BottomSpeed);
            Scrolling(layerSet.middleLayer, layerSet.MiddleSpeed);
            Scrolling(layerSet.topLayer, layerSet.TopSpeed);
        }
    }

    private void Scrolling(Transform layer, float speed)
    {
        if (layer == null)
            return;

        layer.position -= new Vector3(0, speed * Time.deltaTime, 0);

        if (!(layer.position.y < CameraTransform.position.y - CameraHeight))
        {
            return;
        }

        Vector3 position = layer.position;
        position = new Vector3(position.x, ResetPosition, position.z);
        layer.position = position;
    }
}
