using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] private int _life = 5;
    [SerializeField] private int _maxLife = 5;

    [SerializeField] private Material _bloodMat;

    [SerializeField] private float _currentCD = 0f, _maxCD = 3f;
    private bool _canShowBlood = true;

    [SerializeField] private CameraShake _camShake;

    public int Life
    {
        get { return _life; }
    }

    private void Start()
    {
        _bloodMat.SetFloat("_AlphaValue", 0);
    }

    public void TakeDamage(int damage)
    {
        _life = Mathf.Clamp(_life, 0, _maxLife);
        _life -= damage;

        if(_canShowBlood ) StartCoroutine(ShowBlood());
        if(_camShake != null) StartCoroutine(_camShake.Shake(0.5f, 0.2f));

        if (_life <= 0)
        {
            Debug.Log("MUERTE");
            Die();
        }
    }

    private void Die()
    {
        SceneManager.LoadScene("LevelAssetScene");
    }

    public IEnumerator ShowBlood()
    {
        _canShowBlood = false;
        _currentCD = 0f;
        while(_currentCD < _maxCD)
        {
            _currentCD += Time.deltaTime / 0.2f;

            _bloodMat.SetFloat("_AlphaValue", _currentCD);

            yield return null;
        }
        if(_currentCD >= _maxCD)
        {
            while (_currentCD >= 0)
            {
                _currentCD -= Time.deltaTime;

                _bloodMat.SetFloat("_AlphaValue", _currentCD);

                yield return null;
            }
        }
        yield return new WaitForSeconds(2f);

        _canShowBlood = true;
    }
}
