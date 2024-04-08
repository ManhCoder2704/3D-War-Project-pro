using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UISO", menuName = "ScriptableObjects/UI/UISO", order = 1)]
public class UISO : ScriptableObject
{
    [SerializeField]
    private List<UIBase> _listUI = new List<UIBase>();

    public UIBase GetUI(UIType type)
    {
        return _listUI?[(int)type];
    }
}
