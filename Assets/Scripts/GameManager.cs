using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private Transform _checkpointRespawn;
    [SerializeField] private Transform _player;
    public static bool checkpointReached = false;
    [SerializeField] private bool _checkpointView = false;
    [SerializeField] private GameObject _enemy;

    [SerializeField] private NewTubeWeapon _tubeWeapon;
    [SerializeField] private Flashlight _flashLight;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (checkpointReached)
        {
            _player.position = _checkpointRespawn.position;
            _enemy.SetActive(true);

            _tubeWeapon.Interact();
            _flashLight.Interact();
        }
    }

    private void Update()
    {
        _checkpointView = checkpointReached;
    }

    public void SetCheckpoint()
    {
        checkpointReached = true;
    }
}
