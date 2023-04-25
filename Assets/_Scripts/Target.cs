using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Target : MonoBehaviour
{
    public ParticleSystem explosionParticle;
    public int setValue;
    private GameManager gameManager;
    private Rigidbody _rigidbody;

    private float minForce = 12, maxForce =16, maxTorque =10, xRange = 4, ySpawnPos =-6;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.AddForce(RandomForce() ,ForceMode.Impulse);
        _rigidbody.AddTorque(RandomTorque(),RandomTorque(),RandomTorque(), ForceMode.Impulse);
        transform.position = RandomSpawnPosition();
        gameManager = FindObjectOfType<GameManager>();
    }

    private Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minForce, maxForce);
    }
    private float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }
    private Vector3 RandomSpawnPosition()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseOver()
    {
        Destroy(gameObject);
        Instantiate(explosionParticle, transform.position,
                    explosionParticle.transform.rotation);
        gameManager.UpdateScore(setValue);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("KillZone"))
        {
            Destroy(gameObject);
            if (gameObject.CompareTag("Good"))
            {
                gameManager.GameOver();
            }
        }
    }
}
