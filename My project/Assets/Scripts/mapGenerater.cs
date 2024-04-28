using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class mapGenerater : MonoBehaviour
{
    public enum DrawMode
    {
        NoiseMap, ColorMap
    }

    public DrawMode drawMode;
    public int mapWidth;
    public int mapHeight;
    public float noiseScale;

    public int octaves;
    [Range(0, 1)]
    public float persistance;
    public float lacunarity;

    public int seed;
    public UnityEngine.Vector2 offset;
    
    public TerrainType[] regions;
    public bool autoUpdate;
    public Tilemap tilemap;
    public Tilemap nonPassableTilemap;


    public void GenerateMap() {
        float[,] noiseMap = noise.GenerateNoiseMap (mapWidth, mapHeight, seed, noiseScale, octaves, persistance, lacunarity, offset);

        Color[] colourMap = new Color[mapWidth * mapHeight];
        for (int y = 0; y < mapHeight; y++) {
            for (int x = 0; x < mapWidth; x++) {
                float currentHeight = noiseMap [x, y];
                for (int i = 0; i < regions.Length; i++) {
                    if (currentHeight <= regions [i].height) {
                        colourMap [y * mapWidth + x] = regions [i].color;
                        break;
                    }
                }
            }
        }


        mapDisplay display = FindObjectOfType<mapDisplay> ();
        if (drawMode == DrawMode.NoiseMap)
        {
            display.drawTexture(textureGenerator.TextureFromHeightMap(noiseMap));
        } else if (drawMode == DrawMode.ColorMap) {
            display.drawRenderer(textureGenerator.TextureFromColorMap(colourMap, mapWidth, mapHeight));
        }
    }


    public void createTilemapMap()
    {
        clearTiles();
        float[,] noiseMap = noise.GenerateNoiseMap(mapWidth, mapHeight, seed, noiseScale, octaves, persistance, lacunarity, offset);

        Color[] colourMap = new Color[mapWidth * mapHeight];
        for (int y = 0; y < mapHeight; y++) {
            for (int x = 0; x < mapWidth; x++) {
                float currentHeight = noiseMap [x, y];
                for (int i = 0; i < regions.Length; i++) {
                    if (currentHeight <= regions[i].height)
                    {
                        Vector3Int tilePosition = new Vector3Int(x, y, 0);
                        if (regions[i].isPassable && regions[i].tile != null && tilemap != null)
                        {
                            tilemap.SetTile(tilePosition, regions[i].tile);
                        }
                        else if (!regions[i].isPassable && regions[i].tile != null && nonPassableTilemap != null)
                        {
                            nonPassableTilemap.SetTile(tilePosition, regions[i].tile);
                        }
                        colourMap [y * mapWidth + x] = regions [i].color;
                        break;
                    }
                }
            }
        }
    }

    public void clearTiles()
    {
        tilemap.ClearAllTiles();
        nonPassableTilemap.ClearAllTiles();
    }


    private void OnValidate()
    {
        if (mapWidth < 1)
            mapWidth = 1;
        if (mapHeight < 1)
            mapHeight = 1;
        if (lacunarity < 1)
            lacunarity = 1;
        if (octaves < 0)
            octaves = 0;
    }
}

[Serializable]
public struct TerrainType
{
    public string name;
    public bool isPassable;
    public float height;
    public Color color;
    public TileBase tile;
}

