using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonBlock : MonoBehaviour
{
    [SerializeField] private Button _btn;
    [SerializeField] private Image _thrownIcon;
    void Start()
    {
        _btn.onClick.AddListener(ThrowBomb);
    }

    private void ThrowBomb()
    {
        throw new NotImplementedException();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
