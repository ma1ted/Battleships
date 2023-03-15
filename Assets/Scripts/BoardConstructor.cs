using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardConstructor : MonoBehaviour
{
    public struct TileListElement
    {
        public GameObject tile;
        public float lastWaveY;
    }
    public List<TileListElement> tiles = new();

    public int hexSideLength;
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private Transform tileParent;
    [SerializeField] private float tileHeight;
    [SerializeField] private Vector2 randomYRange;
    [SerializeField] private float animateInTime;

    float EaseOutElastic(float x) => x == 0 ? 0 : x == 1 ? 1 : Mathf.Pow(2, -10 * x) * Mathf.Sin((x * 10 - 0.75f) * (2 * Mathf.PI) / 3) + 1;

    private IEnumerator AnimateTile(GameObject tile)
    {
        float elapsedTime = 0f;
        tile.transform.localScale = Vector3.zero;
        Vector3 targetScale = new Vector3(1f, tileHeight, 1f);

        while (elapsedTime < animateInTime)
        {
            elapsedTime += Time.deltaTime;
            tile.transform.localScale = targetScale * EaseOutElastic(elapsedTime / animateInTime);
            yield return null;
        }
    }

    private IEnumerator BuildBoard()
    {
        for (int x = 0; x < (2 * hexSideLength - 1); x++)
        {
            int zMax = hexSideLength * 2 - 1 - Mathf.Abs(x - (hexSideLength - 1));
            for (int z = 0; z < zMax; z++)
            {
                GameObject tile = GameObject.Instantiate(tilePrefab, tileParent);
                TileListElement el = new() { tile = tile };
                tiles.Add(el);

                float xOffset = Mathf.Tan(Mathf.PI / 3) / 2;
                float zOffset = (x % 2 == 0 ? 0.5f : 0) - Mathf.Floor(zMax / 2);

                tile.transform.localPosition = new Vector3(x * xOffset, Random.Range(randomYRange.x, randomYRange.y), z + zOffset);

                StartCoroutine("AnimateTile", tile);

                yield return null;
            }
        }
    }

    private void Awake()
    {
        StartCoroutine("BuildBoard");
    }
}
