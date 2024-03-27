using Cinemachine;
using System;
using UnityEngine;

public class CameraManager : Singleton<CameraManager>
{
    [Header("Virtual Camera Manager")]
    [SerializeField] private CinemachineBrain _cinemachineBrain;
    [SerializeField] private CinemachineVirtualCamera _leaderCamera;
    [SerializeField] private CinemachineVirtualCamera _sniperCamera;
    [SerializeField] private CinemachineVirtualCamera _carrierCamera;
    [SerializeField] private int _activeCameraPriority = 20;
    [SerializeField] private float _transitionTime = 2f;
    [SerializeField] private CinemachineBlendDefinition.Style _blendStyle = CinemachineBlendDefinition.Style.EaseInOut;

    public Action<CameraType> OnCameraChange;
    private CinemachineVirtualCamera _activeCamera;
    private CameraType _activeCameraType;

    private void Start()
    {
        Init();
        ChangeCamera(CameraType.Leader);
    }

    private void Init()
    {
        _cinemachineBrain.m_CameraActivatedEvent.AddListener(OnCameraActived);
        _cinemachineBrain.m_DefaultBlend = new CinemachineBlendDefinition(_blendStyle, _transitionTime);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeCamera(CameraType.Leader);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeCamera(CameraType.Sniper);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangeCamera(CameraType.Carrier);
        }
    }

    private void ChangeCamera(CameraType cameraType)
    {
        CinemachineVirtualCamera camera;

        switch (cameraType)
        {
            case CameraType.Leader:
                camera = _leaderCamera;
                break;
            case CameraType.Sniper:
                camera = _sniperCamera;
                break;
            case CameraType.Carrier:
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

    public CinemachineVirtualCamera GetActiveCamera()
    {
        return _activeCamera;
    }

    public void ChangeCurrentCameraProperty(float fieldOfView, float orthographicSize, float orthographicNearClipPlane, float orthographicFarClipPlane)
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

    public void ChangeCinemachineProperty(CinemachineBlendDefinition.Style blendStyle, float transitionTime)
    {
        _cinemachineBrain.m_DefaultBlend = new CinemachineBlendDefinition(blendStyle, transitionTime);
    }

    private void OnCameraActived(ICinemachineCamera newCamera, ICinemachineCamera oldCamera)
    {
        Debug.Log($"Camera changed from {oldCamera.Name} to {newCamera.Name}");
        OnCameraChange?.Invoke(_activeCameraType);
    }
}
