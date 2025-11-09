using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Tiles/Custom Random Tile")]
public class CustomRandomTile : TileBase
{
    public Sprite[] sprites;

    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        if (sprites == null || sprites.Length == 0) return;
        int index = Random.Range(0, sprites.Length);
        tileData.sprite = sprites[index];
    }
}

