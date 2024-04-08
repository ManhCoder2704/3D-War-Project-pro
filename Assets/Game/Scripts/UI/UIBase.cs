using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class UIBase : MonoBehaviour
{
    [SerializeField] protected bool m_isPopup;
    protected RectTransform m_rectTransform;
    protected CanvasGroup m_canvasGroup;

    public bool IsPopup => m_isPopup;

    protected virtual void Awake()
    {
        m_rectTransform = GetComponent<RectTransform>();
        m_canvasGroup = GetComponent<CanvasGroup>();
    }
    protected virtual void Init()
    {
        m_rectTransform.offsetMax = m_rectTransform.offsetMin = Vector2.zero;
        m_rectTransform.localScale = Vector3.one;
    }
}
