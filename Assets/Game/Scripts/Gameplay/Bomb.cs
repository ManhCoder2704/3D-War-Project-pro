using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private Rigidbody _bombRG;
    [SerializeField] private LayerMask _groundLayer;
    private float _speed = 10f;

    public void BombDrop()
    {
        _bombRG.velocity = Vector3.down * _speed;
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"layer of collider {other.gameObject.layer}");
        if (other.gameObject.layer == _groundLayer)
        {
            Destroy(this);
            Tile temp = other.GetComponent<Tile>();
            temp.OnOffBlock(true);
        }
    }
}
