using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipGlue : MonoBehaviour
{
    public GameObject tile;

    public void SetTile(GameObject _tile)
    {
        tile = _tile;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.Equals(tile)) return;

        GetComponent<Rigidbody>().isKinematic = true;
        transform.parent = tile.transform;
        transform.localPosition = Vector3.up * 0.4f;
        tile.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
    }
}
