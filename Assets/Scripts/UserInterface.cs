using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInterface : MonoBehaviour
{
    public static UserInterface instance;

    [Header("PANEL")]
    [SerializeField] private GameObject _basicPanel;
    [SerializeField] private GameObject _combatPanel;

    [Header("STAGE NOTIFICATION")]
    [SerializeField] private GameObject _stageStart;
    [SerializeField] private GameObject _stageClear;

    [Header("HEALTH BAR")]
    [SerializeField] private Image _hpBar;

    private Player _player;
    private PlayerCamera _playerCam;

    void Start()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        _player = FindObjectOfType<Player>();
        _playerCam = FindObjectOfType<PlayerCamera>();
    }

    private void Update()
    {
        HealthBar();
    }

    public void PanelChange(PlayerCamera.CameraType newType)
    {
        _basicPanel.SetActive(false);
        _combatPanel.SetActive(false);

        if (newType == PlayerCamera.CameraType.Basic) _basicPanel.SetActive(true);
        if (newType == PlayerCamera.CameraType.Combat) _combatPanel.SetActive(true);
    }

    public void HealthBar()
    {
        _hpBar.fillAmount = _player._hp / _player._maxHp;
    }

    public void StageStart()
    {
        _stageStart.LeanScale(Vector3.one, 0.5f).setDelay(1f).setEaseInSine().setOnComplete(() => _stageStart.LeanScale(Vector3.zero, 0.5f).setDelay(1.5f).setEaseOutSine());
    }

    public void StageClear(string sceneName)
    {
        _stageClear.LeanScale(Vector3.one, 0.5f).setEaseInSine().setOnComplete(() => _stageClear.LeanScale(Vector3.zero, 0.5f).setDelay(1.5f).setEaseOutSine().setOnComplete(() => Transition.instance.Fade(true, sceneName)));
    }
}
