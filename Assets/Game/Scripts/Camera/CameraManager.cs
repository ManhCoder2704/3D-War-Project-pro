using Cinemachine;
using System;
using UnityEngine;

public class CameraManager : Singleton<CameraManager>
{
    [Header("Virtual Camera Manager")]
    [SerializeField] private CinemachineBrain _cinemachineBrain;
    [SerializeField] private CinemachineVirtualCamera _centerCamera;
    [SerializeField] private int _activeCameraPriority = 20;
    [SerializeField] private float _transitionTime = 2f;
    [SerializeField] private CinemachineBlendDefinition.Style _blendStyle = CinemachineBlendDefinition.Style.EaseInOut;
    private CinemachineVirtualCamera _leaderCamera;
    private CinemachineVirtualCamera _sniperCamera;
    private CinemachineVirtualCamera _carrierCamera;

    public Action<CharacterType> OnCameraChange;
    private CinemachineVirtualCamera _activeCamera;
    private CharacterType _activeCameraType;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        _cinemachineBrain.m_CameraActivatedEvent.AddListener(OnCameraActived);
        _cinemachineBrain.m_DefaultBlend = new CinemachineBlendDefinition(_blendStyle, _transitionTime);
        _activeCamera = _centerCamera;
    }

    internal void SetupCamera(GameObject leader, GameObject sniper, GameObject carrier)
    {
        _leaderCamera = leader.GetComponentInChildren<CinemachineVirtualCamera>();
        _sniperCamera = sniper.GetComponentInChildren<CinemachineVirtualCamera>();
        _carrierCamera = carrier.GetComponentInChildren<CinemachineVirtualCamera>();
    }

    internal void ChangeCamera(CharacterType cameraType)
    {
        CinemachineVirtualCamera camera;

        switch (cameraType)
        {
            case CharacterType.Leader:
                camera = _leaderCamera;
                break;
            case CharacterType.Sniper:
                camera = _sniperCamera;
                break;
            case CharacterType.Carrier:
                camera = _carrierCamera;
                break;
            default:
                return;
        }

        if (_activeCamera == camera) return;

        if (_activeCamera != null) _activeCamera.Priority = 0;

        _activeCameraType = cameraType;
        _activeCamera = camera;
        _activeCamera.Priority = _activeCameraPriority;
    }

    internal CinemachineVirtualCamera GetActiveCamera()
    {
        return _activeCamera;
    }

    internal void ChangeCurrentCameraProperty(float fieldOfView, float orthographicSize, float orthographicNearClipPlane, float orthographicFarClipPlane)
    {
        if (_activeCamera == null) return;

        if (_activeCamera.m_Lens.Orthographic)
        {
            _activeCamera.m_Lens.OrthographicSize = orthographicSize;
            _activeCamera.m_Lens.NearClipPlane = orthographicNearClipPlane;
            _activeCamera.m_Lens.FarClipPlane = orthographicFarClipPlane;
        }
        else
        {
            _activeCamera.m_Lens.FieldOfView = fieldOfView;
        }
    }

    internal void ChangeCinemachineProperty(CinemachineBlendDefinition.Style blendStyle, float transitionTime)
    {
        _cinemachineBrain.m_DefaultBlend = new CinemachineBlendDefinition(blendStyle, transitionTime);
    }

    private void OnCameraActived(ICinemachineCamera newCamera, ICinemachineCamera oldCamera)
    {
        Debug.Log($"Camera changed from {oldCamera?.Name} to {newCamera?.Name}");
        OnCameraChange?.Invoke(_activeCameraType);
    }
}
