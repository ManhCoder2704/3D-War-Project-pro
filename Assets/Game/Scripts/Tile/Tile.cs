using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private GameObject block;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void OffBlock()
    {
        block.SetActive(false);
    }
}
