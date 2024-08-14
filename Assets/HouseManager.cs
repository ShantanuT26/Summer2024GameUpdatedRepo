using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HouseManager : MonoBehaviour
{
    private Dictionary<Vector2, House> houses;
    [SerializeField] private Tilemap houseTileMap;

    private void Start()
    {
        houses = new Dictionary<Vector2, House>();
        InitializeHouses();
    }
    private void InitializeHouses()
    {
        BoundsInt mapSize = houseTileMap.cellBounds;
        Debug.Log("mapsize: " + mapSize);

        List<BoundsInt> housePositions = DetectHouses(mapSize);

        StartCoroutine(InitializePlayerCoroutine(housePositions));
        
    }
    private IEnumerator InitializePlayerCoroutine(List<BoundsInt> housePositions)
    {
        while(ObjectPooler.Instance.poolDict==null)
        {
            yield return null;
        }
        foreach (BoundsInt hp in housePositions)
        {
            Vector3 positionToInstantiate = FindWorldPositionOfBoundsInt(hp);
            ObjectPooler.Instance.SpawnFromPool("NPC", positionToInstantiate, Quaternion.Euler(0, 0, 0));
        }

    }

    private List<BoundsInt> DetectHouses(BoundsInt mapSize)
    {
        Debug.Log("mapsize x: " + mapSize.size.x);
        Debug.Log("mapsize y: " + mapSize.size.y);

        List<BoundsInt> detectedHouses = new List<BoundsInt>();
        for(int x = 0; x<mapSize.size.x; x++) 
        {
            Debug.Log("debug x: " + x);
            for(int y = 0; y<mapSize.size.y; y++) 
            {
                if(houseTileMap.HasTile(new Vector3Int(x, y, 0)))
                {
                    Debug.Log("hastile");
                    Vector3Int startPos = (new Vector3Int(x, y, 0));
                    Vector3Int houseDimensions = new Vector3Int(14, 7, 0);
                    detectedHouses.Add(ExtrapolateHouse(startPos, houseDimensions));
                    x = startPos.x + houseDimensions.x;
                    y = startPos.y + houseDimensions.y;
                }
                
            }
        }
        return detectedHouses;
    }

    private BoundsInt ExtrapolateHouse(Vector3Int startPos, Vector3Int houseDimensions)
    {
        return new BoundsInt(startPos, houseDimensions);
    }

    private Vector3 FindWorldPositionOfBoundsInt(BoundsInt house)
    {
        Vector3Int bottomCenter = new Vector3Int(house.xMin + (house.size.x / 2), house.yMin);
        Vector3 actualPosition = houseTileMap.CellToWorld(bottomCenter);
        return actualPosition;
    }


}
