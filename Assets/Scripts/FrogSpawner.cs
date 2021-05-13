using System.Collections.Generic;
using UnityEngine;

public class FrogSpawner : MonoBehaviour {
    public GameObject PlayerReference;
    public GameObject FrogPrefab;
    public GameObject SpawnArea;
    private PolygonCollider2D _spawnColider;
    private List<GameObject> _frogList;
    private Bounds _bounds;

    void Start () {
        _frogList = new List<GameObject>();
        _spawnColider = GetComponent<PolygonCollider2D>();
        _bounds = _spawnColider.bounds;
        SpawnFrog();
    }

    public void SpawnFrog () {
        var newX = Random.Range(_bounds.min.x, _bounds.max.x);
        var newY = Random.Range(_bounds.min.y, _bounds.max.y);
        var newPosition = new Vector3(newX, newY, _bounds.min.z);
        var newFrog = Instantiate(FrogPrefab, newPosition, new Quaternion());

        newFrog.transform.parent = SpawnArea.transform;
        _frogList.Add(newFrog);
    }
}
