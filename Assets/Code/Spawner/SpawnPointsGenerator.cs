using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using NewTankio;
using UnityEngine;
using Random = UnityEngine.Random;
public class SpawnPointsGenerator : MonoBehaviour
{
    [SerializeField]
    private float _radius;
    [SerializeField]
    private GenerationMethod _method;

    public GameObject SpawnMarkerPrefab;
    private Vector2 _boundsSize;

    private List<Vector2> _points = new();

    private void OnValidate()
    {
        _boundsSize = GetComponent<SpriteRenderer>().bounds.size - new Vector3(_radius, _radius);
    }

    public void Generate()
    {
        DestroyChildren();
        _points = _method switch
        {
            GenerationMethod.PoissonDiscSampling => PoissonDiscSampling.GeneratePoints(_radius, _boundsSize).ToList(),
            GenerationMethod.PerlinNoise => PerlinNoiseGeneration.GeneratePoints(_radius, _boundsSize).ToList(),
            _ => throw new ArgumentOutOfRangeException()
        };
        foreach (var point in _points)
        {
            var spawnMarker = Instantiate(SpawnMarkerPrefab, transform);
            spawnMarker.transform.position = point + (Vector2)transform.position - _boundsSize / 2;
            spawnMarker.GetComponent<SpawnMarker>().FigureId = (FigureId)Random.Range(0, 2);
        }
    }
    private void DestroyChildren()
    {
        foreach (Transform child in transform)
            DestroyImmediate(child.gameObject);
    }
    private enum GenerationMethod
    {
        PoissonDiscSampling,
        PerlinNoise
    }
}
