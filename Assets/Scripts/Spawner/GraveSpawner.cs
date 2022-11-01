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
        //Loop through all tiles
        foreach (Vector3Int positionInt3 in structuresTileMap.cellBounds.allPositionsWithin)
        {
            TileBase tilebase = structuresTileMap.GetTile(positionInt3);

            if(tilebase !=null)
            {
               // CGUtils.DebugLog($"Found a structure at  {positionInt3}");
            }
            else
            {
                //Cell bounds are from 0 - 30 in X. -29 to 30 in Y

                //Skip placing graves on or close to bounds on map
                if (positionInt3.x < 4)
                    continue;

                if (positionInt3.x > 26)
                    continue;

                if (positionInt3.y < -26)
                    continue;

                if (positionInt3.y > 26)
                    continue;

                //Since there is no tile on this position we can spawn a grave here
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

            //Do not reuse the same spot.
            potentialGraveSpawnPoints.RemoveAt(randomPositionIndex);

            yield return waitTimeSpawnGraves;
        }

    }

}
