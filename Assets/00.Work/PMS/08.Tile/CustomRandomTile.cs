using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Tiles/OneTime Random Tile")]
public class CustomRandomTile : TileBase
{
    public Sprite[] sprites;

    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        if (sprites == null || sprites.Length == 0)
            return;

        int seed = position.x * 73856093 ^ position.y * 19349663;
        Random.InitState(seed);

        int index = Random.Range(0, sprites.Length);
        tileData.sprite = sprites[index];

        tileData.color = Color.white;
        tileData.colliderType = Tile.ColliderType.Sprite;
    }
}

