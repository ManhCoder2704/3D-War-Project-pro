using UnityEngine;

public class Package : MonoBehaviour
{
    [SerializeField] private int _id;
    [SerializeField] private float _dropTime;

    internal int Id { get => _id; set => _id = value; }

    private void Start()
    {
        //this.transform.DOM
    }
}
