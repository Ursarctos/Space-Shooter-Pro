using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float _speed = 5f;
    [SerializeField]
    private float _speedMultiplier = 2;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private float _fireRate = 0.15f;
    private float _canFire = -1f;
    [SerializeField]
    private int _lives = 3;
    private int _shieldHealth = 3;
    private SpawnManager _spawnManager;

    [SerializeField]
    private bool _isTripleShotActive = false;
    [SerializeField]
    private bool _isSpeedBoostActive = false;
    [SerializeField]
    private bool _isShieldActive = false;
    [SerializeField]
    private GameObject _tripleShotPrefab;

    [SerializeField]
    private GameObject _shieldVisualizer;
    [SerializeField]
    private GameObject _shieldDamagePurple;
    [SerializeField]
    private GameObject _shieldDamageRed;
    [SerializeField]
    private int _score;

    [SerializeField]
    private int _ammoCount = 20;
    [SerializeField]
    private GameObject _leftEngineVisualizer;
    [SerializeField]
    private GameObject _rightEngineVisualizer;
    [SerializeField]
    private GameObject _thrusterVisualizer;

    private UIManager _uiManager;

    [SerializeField]
    private AudioSource _laserSound;
    [SerializeField]
    private AudioSource _powerUpSound;
    

    void Start()
    {
        
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        if(_spawnManager == null)
        {
            Debug.LogError("The SpawnManager is NULL");
        }
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if(_uiManager == null)
        {
            Debug.LogError("The UI Manager is NULL");
        }
    }
    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _speed = 10f;
            _thrusterVisualizer.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            _speed = 5f;
            _thrusterVisualizer.SetActive(false);
        }
       
    }
    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        if (_isSpeedBoostActive == false)
        {
            transform.Translate(direction * _speed * Time.deltaTime);
        }
        else if (_isSpeedBoostActive == true)
        {
            transform.Translate(direction * _speed * _speedMultiplier * Time.deltaTime);
        }
       
        if (transform.position.y > 6)
        {
            transform.position = new Vector3(transform.position.x, 6, 0);
        }
        if (transform.position.y < -4)
        {
            transform.position = new Vector3(transform.position.x, -4, 0);
        }
        if (transform.position.x > 9)
        {
            transform.position = new Vector3(9, transform.position.y, 0);
        }
        if (transform.position.x < -9)
        {
            transform.position = new Vector3(-9, transform.position.y, 0);
        }
    }
    void FireLaser()
    {
        Vector3 offset = new Vector3(0, 1.0f, 0);
        if (_ammoCount > 0)
        {
            _canFire = Time.time + _fireRate;
            if (_isTripleShotActive == true)
            {
                Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(_laserPrefab, transform.position + offset, Quaternion.identity);
            }
            _laserSound.Play();
            _ammoCount--;
        }
        else if (_ammoCount == 0)
        {
            Debug.Log("out of ammo");
        }
           
    }
    public void Damage()
    {
        if(_isShieldActive == true)
        {
            _shieldHealth--;
            if (_shieldHealth == 2)
            {
                _shieldDamageRed.SetActive(false);
                _shieldDamagePurple.SetActive(true);
                _shieldVisualizer.SetActive(false);
            }
            else if (_shieldHealth == 1)
            {
                _shieldVisualizer.SetActive(false);
                _shieldDamagePurple.SetActive(false);
                _shieldDamageRed.SetActive(true);
            }
            else if (_shieldHealth == 0)
            {
                _shieldDamageRed.SetActive(false);
                _shieldDamagePurple.SetActive(false);
                _isShieldActive = false;
                _shieldVisualizer.SetActive(false);
            }
        }
        else
        {
            _lives -= 1;
            _uiManager.UpdateLives(_lives);
        }
        if (_lives == 2)
        {
            _rightEngineVisualizer.SetActive(true);
        }
        else if (_lives == 1)
        {
            _leftEngineVisualizer.SetActive(true);
        }
        else if (_lives <= 0)
        {
          
            _spawnManager.OnPlayerDeath();
            _uiManager.GameOverDisplay();
            Destroy(this.gameObject);
        }
    }
    public void TripleShot()
    {
        _powerUpSound.Play();
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());


    }
    IEnumerator TripleShotPowerDownRoutine()
    {
            yield return new WaitForSeconds(5.0f);
            _isTripleShotActive = false;
    }
    public void SpeedBoost()
    {
        _powerUpSound.Play();
        _isSpeedBoostActive = true;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }
    IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isSpeedBoostActive = false;
    }
    public void ShieldActive()
    {
        if (_isShieldActive == false)
        {
            _shieldHealth = 3;
            _powerUpSound.Play();
            _isShieldActive = true;
            _shieldVisualizer.SetActive(true);
            _shieldDamagePurple.SetActive(false);
            _shieldDamageRed.SetActive(false);
        }
    }
    public void AddScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }
    public void AddAmmo(int addAmmo)
    {
        _ammoCount += addAmmo;
        _uiManager.UpdateAmmoCount(_ammoCount);
    }
}
