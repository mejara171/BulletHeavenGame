using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GraveSpawner : MonoBehaviour
{
    public Tilemap structuresTileMap;

    public GameObject gravePrefab;

    WaitForSeconds waitTimeSpawnGraves = new WaitForSeconds(30);

    List<Vector3> potentialGraveSpawnPoints = new List<Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        foreach (Vector3Int positionInt3 in structuresTileMap.cellBounds.allPositionsWithin)
        {
            TileBase tilebase = structuresTileMap.GetTile(positionInt3);

            if(tilebase !=null)
            {
            }
            else
            {

                if (positionInt3.x < 4)
                    continue;

                if (positionInt3.x > 26)
                    continue;

                if (positionInt3.y < -26)
                    continue;

                if (positionInt3.y > 26)
                    continue;

                potentialGraveSpawnPoints.Add(structuresTileMap.CellToWorld(positionInt3));

            }
        }

        StartCoroutine(SpawnGraveCO());
    }


    IEnumerator SpawnGraveCO()
    {
        while (true)
        {
            int randomPositionIndex = Random.Range(0, potentialGraveSpawnPoints.Count);

            Instantiate(gravePrefab, potentialGraveSpawnPoints[randomPositionIndex], Quaternion.identity);

            potentialGraveSpawnPoints.RemoveAt(randomPositionIndex);

            yield return waitTimeSpawnGraves;
        }

    }

}
