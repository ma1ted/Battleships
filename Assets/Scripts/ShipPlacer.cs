using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipPlacer : MonoBehaviour
{
    [SerializeField] private GameObject shipPrefab;
    [SerializeField] private float dropHeight;

    private void OnMouseDown()
    {
        GameObject ship = GameObject.Instantiate(shipPrefab, transform.position + (Vector3.up * dropHeight), Quaternion.identity);
        ship.GetComponent<ShipGlue>().SetTile(gameObject);
    }
}
