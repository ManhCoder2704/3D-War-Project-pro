using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : Singleton<Sniper>
{
    [SerializeField] private GameObject _bullet;
    private float _shootingSpeed = 100f;

    public void Shoot(Transform startPos, Transform endPos)
    {
        Vector3 direction = startPos.position - endPos.position;
        GameObject bullet = Instantiate(_bullet, startPos.position, Quaternion.identity);
        bullet.transform.position += direction * _shootingSpeed * Time.fixedDeltaTime;
    }
}
