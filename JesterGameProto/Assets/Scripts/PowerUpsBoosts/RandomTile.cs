using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTile : MonoBehaviour
{
    [SerializeField] GameObject[] specialTiles;
    [SerializeField] Transform spawnPosition;

    private void Awake()
    {
        var TileToSpawn = Random.Range(0, specialTiles.Length);
        Instantiate(specialTiles[TileToSpawn], spawnPosition.position, Quaternion.identity);
    }
}
