using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _rotationSpeed = 3.0f;
    [SerializeField]
    private GameObject _explosionPrefab;
    private SpawnManager _spawnManager;
    // Start is called before the first frame update
    void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, 1 * _rotationSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Laser")
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
            _spawnManager.StartSpawning();
            Destroy(this.gameObject, 0.25f);
        }
    }
}
