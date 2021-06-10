using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using unity_game.Assets.DTOs;
using Newtonsoft.Json;

public class MenuBoxController : MonoBehaviour {

    public GameObject PlayerGameObject;
    public GameObject GemSpawnerObject;
    public GameObject FrogSpawnerObject;

    void Update() {
        if (IsEscPressed) {
            SaveGame();
        }
    }

    private void SaveGame () {
        var playerController = PlayerGameObject.GetComponentInChildren<PlayerController>();
        var frogSpawnerController = FrogSpawnerObject.GetComponentInChildren<FrogSpawner>();
        var gemSpawnerController = GemSpawnerObject.GetComponentInChildren<GemSpawnerController>();

        var gemsPositions = gemSpawnerController.GemsList.Select(v => new PointDTO{X = v.transform.position.x, Y = v.transform.position.y});
        var frogsPositions = frogSpawnerController.FrogList.Select(v => new PointDTO{X = v.transform.position.x, Y = v.transform.position.y});

        var gameState = new GameStateDTO{
            HP = playerController.Health,
            Position = new PointDTO{
                X = playerController.transform.position.x,
                Y = playerController.transform.position.y
            },
            Gems = gemsPositions.ToList(),
            Frogs = frogsPositions.ToList()
        };

        var gameStateJson = JsonConvert.SerializeObject(gameState);
    }

    private bool IsEscPressed => Input.GetKeyDown(KeyCode.Escape);
}
