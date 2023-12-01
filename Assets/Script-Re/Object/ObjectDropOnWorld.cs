using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class ObjectDropOnWorld : MonoBehaviour
{
    [SerializeField] SpriteAtlas spriteAtlasItems;
    [SerializeField] SpriteRenderer spriteRenderer;
    public void UpdateSprite(string spriteName)
    {
        spriteRenderer.sprite = spriteAtlasItems.GetSprite(spriteName);
    }
}
