using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBuilder : MonoBehaviour {

    public static GameObject BASE, WALL, CORNER, CARPET, FLOWER_1, FLOWER_2, DOOR, PILLAR;

    public static readonly Vector3
        NORTH = new Vector3(0, 0, 270),
        EAST = new Vector3(0, 0, 180),
        SOUTH = new Vector3(0, 0, 90),
        WEST = new Vector3(0, 0, 0);

    /*** Builds the map with given tiles ***/
    public static void Init() {
        BASE = Resources.Load<GameObject>("Prefabs/tile 3");
        WALL = Resources.Load<GameObject>("Prefabs/tile 4");
        CORNER = Resources.Load<GameObject>("Prefabs/tile 5");
        CARPET = Resources.Load<GameObject>("Prefabs/tile 1");
        FLOWER_1 = Resources.Load<GameObject>("Prefabs/tile 6");
        FLOWER_2 = Resources.Load<GameObject>("Prefabs/tile 7");
        DOOR = Resources.Load<GameObject>("Prefabs/tile 2");
        PILLAR = Resources.Load<GameObject>("Prefabs/tile 8");

        float xDiff = -7.916f + 8.556f;
        float yDiff = -4.04f + 4.68f;

        xDiff *= 4;
        float tY = 4.029998f;
        /*** TOP ROW ***/
        for (int count = 0; count < 6; count++) {
            CreateTile(WALL, -8.556f + (count * xDiff), tY, NORTH);
        }
        CreateTile(PILLAR, 5.92f, tY, NORTH);
        xDiff /= 4;

        int row = 1;
        /*** MIDDLE ROWS ***/
        for (row = 1; row < 18; row++) {
            for (int column = 1; column < 30; column++) {
                CreateTile(BASE, -8.55f + (column * xDiff), (4.68f - (yDiff * row)));
            }
        }

        CreateTile(WALL, 8.4f, 1.89f, EAST);
        CreateTile(WALL, 8.4f, -1.49f, EAST);
        CreateTile(PILLAR, 8.4f, 3.53f, EAST);
        CreateTile(PILLAR, 8.4f, -3.13f, EAST);

        xDiff *= 4;

        float bYDiff = (yDiff * 14) - 0.25f;
        /*** BOTTOM ROW ***/
        for (int count = 0; count < 6; count++) {
            CreateTile(WALL, -8.556f + (count * xDiff), 4.68f - bYDiff, SOUTH);
        }

        CreateTile(PILLAR, 5.92f, 4.68f - bYDiff, SOUTH);

        /*** CARPET ***/
        float diffC = 7.46f - 4.9f;
        for (int i = 0; i < 6; i++) {
            CreateTile(CARPET, 7.46f - (diffC * i), 0.2f, SOUTH);
        }

        /*** FLOWERS ***/ // discontinued
        /*CreateTile(FLOWER_1, -6.63f, -3.99f, NORTH);
        CreateTile(FLOWER_1, -1.84f, -3.99f, NORTH);

        CreateTile(FLOWER_2, 1.84f, -3.99f, NORTH);
        CreateTile(FLOWER_2, 6.63f, -3.99f, NORTH);

        CreateTile(FLOWER_2, -6.63f, 4.39f, SOUTH);
        CreateTile(FLOWER_2, -1.84f, 4.39f, SOUTH);

        CreateTile(FLOWER_1, 1.84f, 4.39f, SOUTH);
        CreateTile(FLOWER_1, 6.63f, 4.39f, SOUTH);*/

        /*** DOOR ***/
        CreateTile(DOOR, 7.6f, 0.2f);
    }

    /*** Creates the given tile at the coordinates and rotation given ***/
    public static void CreateTile(GameObject tile, float x, float y, Vector3 direction = default(Vector3)) {
        GameObject cTile = GameObject.Instantiate(tile, new Vector3(x, y, 
            tile == DOOR ? -2 : 
            tile == CARPET || tile == FLOWER_1 || tile == FLOWER_2 || tile == WALL || tile == PILLAR ? -1 : 
            0), Quaternion.identity);

        if (direction != default(Vector3)) {
            cTile.transform.Rotate(direction);
        }

        if (tile == BASE) {
            int randomRotation = Random.Range(0, 4);
            cTile.transform.Rotate(randomRotation == 0 ? NORTH : randomRotation == 1 ? SOUTH : randomRotation == 2 ? EAST : WEST);
        }
    }
}
