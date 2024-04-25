using UnityEngine;

public class Package : MonoBehaviour
{
    [SerializeField] private int _id;
    [SerializeField] private float _dropTime;
    [SerializeField] private int _reward;

    internal float DropTime { get => _dropTime; }
    internal int Id { get => _id; set => _id = value; }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 1 << 7) // collect package
        {
            OnGetPackage();
        }
    }

    private void OnGetPackage()
    {

    }
}
