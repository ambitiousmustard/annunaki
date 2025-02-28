﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class WorldGenerator {

    public int frequency = 3;
    public float water = 20;
    public float lush = 10;
    public float humid = 10;
    public float dry = 10;
    public float arid = 50;

    public int[,] GetRandomTerrain(int szx, int szy) {

        int[,] terrain = new int[szx, szy];
        float[,] perlin = PerlinNoise(frequency, new Node(szx, szy));

        for (int x = 0; x < szx; x++)
            for (int y = 0; y < szy; y++) {

                float noise = perlin[x, y];

                if (noise < water)
                    terrain[x, y] = 0;
                else if (noise < lush + water)
                    terrain[x, y] = 1;
                else if (noise < humid + lush + water)
                    terrain[x, y] = 2;
                else if (noise < dry + humid + lush + water)
                    terrain[x, y] = 3;
                else
                    terrain[x, y] = 4;

            }

        if (terrain[0, 0] == (int)Terrain.Water)
            terrain[0, 0] = (int)Terrain.Grass;

        if (terrain[szx - 1, szx - 1] == (int)Terrain.Water)
            terrain[szy - 1, szy - 1] = (int)Terrain.Grass;

        return terrain;

    }

    public int[,] GetRandomTerrain(Node size) {

        return GetRandomTerrain((int)size.X, (int)size.Y);

    }

    public List<Node> GetTrees(int szx, int szy) {

        List<Node> trees = new List<Node>();
        float[,] perlin = PerlinNoise(3, new Node(szx, szy));

        for (int x = 0; x < szx; x++)
            for (int y = 0; y < szy; y++) {

                float noise = perlin[x, y];

                if (noise < 30)
                    trees.Add(new Node(x, y));

            }
        
        return trees;

    }

    public List<Node> GetTrees(Node size) {

        return GetTrees((int)size.X, (int)size.Y);

    }

    public float[,] PerlinNoise(float freq, int szx, int szy) {

        float offset = Random.Range(0, 1000);
        float[,] perlin = new float[szx, szy];

        for (int x = 0; x < szx; x++)
            for (int y = 0; y < szy; y++) {

                float nx = (float)x / szx * freq + offset;
                float ny = (float)y / szy * freq + offset;

                float noise = Mathf.PerlinNoise(nx, ny);
                noise *= 100;

                perlin[x, y] = noise;

            }

        return perlin;

    }

    public float[,] PerlinNoise(float freq, Node size) {

        return PerlinNoise(freq, (int)size.X, (int)size.Y);

    }

}
