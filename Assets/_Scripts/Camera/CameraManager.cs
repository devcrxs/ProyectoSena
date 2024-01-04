using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;
    [SerializeField] private CinemachineBrain cinemachineBrain;
    [SerializeField] private List<CinemachineVirtualCamera> allCameras;
    [SerializeField] private float fallPanAmount = 0.25f;
    [SerializeField] private float fallYPanTime = 0.35f;
    [SerializeField] private float fallSpeed = -15f;
    private bool _isLearpingYDamping;
    private bool _lerpFromPlayerFalling;
    private CinemachineVirtualCamera _currentCamera;
    private CinemachineFramingTransposer _framingTransposer;
    private float _normYPanAmount;
    private float _sizeCamera;
    private bool _isZooming;
    private bool _isCameraShake;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Start()
    {
        foreach (var virtualCamera in allCameras)
        {
            virtualCamera.enabled = false;
        }
        _currentCamera =  allCameras.Count > 1? allCameras[PersistenDataManager.instance.GetSaveCamera()]:allCameras[0];
        _currentCamera.enabled = true;
        _sizeCamera = _currentCamera.m_Lens.OrthographicSize;
        _framingTransposer = _currentCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
        _normYPanAmount = _framingTransposer.m_YDamping;
    }

    private void Update()
    {
        if (PlayerProperties.instance.Rigidbody2D.velocity.y < fallSpeed && !_isLearpingYDamping &&
            !_lerpFromPlayerFalling)
        {
            LerpYDamping(true);
        }

        if (!(PlayerProperties.instance.Rigidbody2D.velocity.y >= 0f) || !_isLearpingYDamping ||
            !_lerpFromPlayerFalling) return;
        _lerpFromPlayerFalling = false;
        LerpYDamping(false);
    }

    private void LerpYDamping(bool isFallingPlayer)
    {
        StartCoroutine(LerpYAction(isFallingPlayer));
    }

    IEnumerator LerpYAction(bool isfalllingPlayer)
    {
        _isLearpingYDamping = true;
        float startAmount = _framingTransposer.m_YDamping;
        float endAmount;
        if (isfalllingPlayer)
        {
            endAmount = fallPanAmount;
            _lerpFromPlayerFalling = true;
        }
        else
        {
            endAmount = _normYPanAmount;
        }

        float elapsedTime = 0f;
        while (elapsedTime < fallYPanTime)
        {
            elapsedTime += Time.deltaTime;
            float leerpedPanAmount = Mathf.Lerp(startAmount, endAmount, (elapsedTime / fallYPanTime));
            _framingTransposer.m_YDamping = leerpedPanAmount;
            yield return null;
        }
        _isLearpingYDamping = false;
    }

    public void SwapCamera(CinemachineVirtualCamera cameraToChange)
    {
        if (cameraToChange == _currentCamera) return;
        _currentCamera.enabled = false;
        _currentCamera = cameraToChange;
        _currentCamera.enabled = true;
        _framingTransposer = _currentCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
        int indexCamera = 0;
        for (int i = 0; i < allCameras.Count; i++)
        {
            if (allCameras[i].enabled)
            {
                indexCamera = i;
                break;
            }
        }
        PersistenDataManager.instance.SaveCurrentCamera(indexCamera);
        StartCoroutine(PlayerProperties.instance.StopPlayerAsync(cinemachineBrain.m_DefaultBlend.m_Time));
        
    }

    public void ZoomCamera(float zoom, float speedLerp)
    {
        if (_isZooming) return;
        _isZooming = true;
        StartCoroutine(LerpZoomCamera(zoom,speedLerp));
    }

    IEnumerator LerpZoomCamera(float zoom,float speedLerp)
    {
        _sizeCamera = _currentCamera.m_Lens.OrthographicSize;
        float time = 0;
        while (time < 0.4f)
        {
            time += Time.deltaTime;
            float newSize = Mathf.Lerp(_sizeCamera, zoom, (time/ speedLerp));
            _currentCamera.m_Lens.OrthographicSize = newSize;
            yield return null;
        }

        _isZooming = false;
    }

    public void ResetSizeCamera()
    {
        StartCoroutine(LerpZoomCamera(_sizeCamera,0.1f));
    }

    public void PlayCameraShake(float amplitude)
    {
        if(_isCameraShake) return;
        _isCameraShake = true;
        var basicMultiChannelPerlin = GetMultiChanelCurrentCamera();
        basicMultiChannelPerlin.m_AmplitudeGain = amplitude;
    }

    public void StopCameraShake()
    {
        var basicMultiChannelPerlin = GetMultiChanelCurrentCamera();
        basicMultiChannelPerlin.m_AmplitudeGain = 0;
        _isCameraShake = false;
    }

    public IEnumerator CameraShakeRecursive(float amplitude, float timeShake)
    {
        var basicMultiChannelPerlin = GetMultiChanelCurrentCamera();
        basicMultiChannelPerlin.m_AmplitudeGain = amplitude;
        yield return new WaitForSeconds(timeShake);
        basicMultiChannelPerlin.m_AmplitudeGain = 0;
        _isCameraShake = false;
    }

    private CinemachineBasicMultiChannelPerlin GetMultiChanelCurrentCamera()
    {
        CinemachineBasicMultiChannelPerlin basicMultiChannelPerlin =
            _currentCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        return basicMultiChannelPerlin;
    }
}
