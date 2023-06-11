using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] _points;

    [SerializeField] private Tree[] _smallTrees;
    [SerializeField] private Tree[] _largeTrees;

    [SerializeField] private float _speedCloserToRoad;
    [SerializeField] private float _speedAwayFromRoad;

    [SerializeField] private float _spawnTimeCloserToRoad;
    [SerializeField] private float _spawnTimeAwayFromRoad;

    private float _absoluteDistanceValueCloserToRoad = 7;
    private float _absoluteDistanceValueAwayFromRoad = 9;

    private Transform[] _pointsAwayFromRoad = new Transform[2];
    private Transform[] _pointsCloserToRoad = new Transform[2];


    private void Awake()
    {
        SetPiontsPositions();
        SortPointsArrays();

        StartCoroutine(Creator(_pointsAwayFromRoad, _largeTrees, _speedAwayFromRoad, _spawnTimeAwayFromRoad));
        StartCoroutine(Creator(_pointsCloserToRoad, _smallTrees, _speedCloserToRoad, _spawnTimeCloserToRoad));
    }

    private void SetPiontsPositions()
    {
        float leftSideCoefficient = -1;

        Vector3 leftBackPoint = new Vector3(_absoluteDistanceValueAwayFromRoad * leftSideCoefficient, 0, 0);
        Vector3 leftMiddlePoint = new Vector3(_absoluteDistanceValueCloserToRoad * leftSideCoefficient, 0, 0);
        Vector3 rightMiddlePoint = new Vector3(_absoluteDistanceValueCloserToRoad, 0, 0);
        Vector3 rightBackPoint = new Vector3(_absoluteDistanceValueAwayFromRoad, 0, 0);

        _points[0].transform.position = leftBackPoint;
        _points[1].transform.position = leftMiddlePoint;
        _points[2].transform.position = rightMiddlePoint;
        _points[3].transform.position = rightBackPoint;
    }

    private void SortPointsArrays()
    {
        _pointsAwayFromRoad[0] = _points[0];
        _pointsAwayFromRoad[1] = _points[3];

        _pointsCloserToRoad[0] = _points[1];
        _pointsCloserToRoad[1] = _points[2];
    }

    private IEnumerator Creator(Transform[] points, Tree[] trees, float speed, float spawnTime)
    {
        float spread = 0.3f;

        while (true)
        {
            float seconds = spawnTime + Random.Range(-spread, spread);

            foreach (var point in points)
                    InstantiateTree(trees, point, speed);

            yield return new WaitForSeconds(seconds);
        }
    }

    private void InstantiateTree(Tree[] trees, Transform point, float speed)
    {
        Quaternion rotation = new Quaternion();

        rotation.y = 360 * Random.value;

        Tree tree = Instantiate(trees[Random.Range(0, trees.Length)], point.position, rotation, transform);

        tree.SetSpeed(speed);
    }
}
