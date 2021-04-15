using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemSpawnerController : MonoBehaviour
{
    public GameObject GemPrefab;
    public GameObject SpawnArea;
    public GameObject CounterControl;

    private GemCounterController _counterController;
    private List<GameObject> _gemsList;
    private Bounds _bounds;

    void Start()
    {
        var collider = SpawnArea.GetComponent<PolygonCollider2D>();
        _bounds = collider.bounds;

        _gemsList = new List<GameObject>();
        _counterController = CounterControl.GetComponentInChildren<GemCounterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_gemsList.Count < 3)
        {
            SpawnGem();
        }
    }

    public void DeleteGem(GameObject gem)
    {
        Debug.Log($"Removing gem {gem.GetInstanceID()}\r\n");

        if (_gemsList.Contains(gem))
        {
            _gemsList.Remove(gem);
            Destroy(gem);
            _counterController.Add();
        }
    }

    public void SpawnGem()
    {
        var newX = Random.Range(_bounds.min.x, _bounds.max.x);
        var newY = Random.Range(_bounds.min.y, _bounds.max.y);

        var newPosition = new Vector3(newX, newY, _bounds.min.z);

        //stworzyć instancję prefaba na pozycji XYZ
        var newGem = Instantiate(GemPrefab, newPosition, new Quaternion());

        //przypisać gem do GemSpawnera
        newGem.transform.parent = SpawnArea.transform;
        _gemsList.Add(newGem);
    }
}
