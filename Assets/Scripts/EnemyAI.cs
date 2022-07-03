using System.Collections;
using Player;
using UnityEngine;


public class EnemyAI : MonoBehaviour
{
    public enum EnemyState
    {
        Calm,
        Attack
    }

    public int moveSpeed = 1;
    public int rotationSpeed = 1;
    public int contactDamage = 10;
    public int contactDamageDelay = 1;
    
    public EnemyState enemyState = EnemyState.Attack;

    private Rigidbody enemyRigidbody;
    private Transform playerTransform;
    private GameObject playerGameObject;
    private PlayerState playerState;
    private readonly IEnumerator dealContactDamageCoroutine;

    public EnemyAI()
    {
        dealContactDamageCoroutine = DealContactDamage();
    }

    private void Awake()
    {
        enemyRigidbody = GetComponent<Rigidbody>();
        playerGameObject = GameObject.FindWithTag("Player");
        playerState = playerGameObject.GetComponent<PlayerState>();
        playerTransform = playerGameObject.GetComponent<Transform>();

        GlobalEventManager.PlayerDeath.AddListener(() => enemyState = EnemyState.Calm);
    }

    private void FixedUpdate()
    {
        //LookAtPlayer();
        if (enemyState == EnemyState.Attack)
            MoveToPlayer();
    }

    private void OnCollisionEnter(Collision collision)
    {
        var collisionGameObject = collision.gameObject;
        if (collisionGameObject.CompareTag("Player"))
        {
            StartCoroutine(dealContactDamageCoroutine);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        var collisionGameObject = collision.gameObject;
        if (collisionGameObject.CompareTag("Player"))
        {
            StopCoroutine(dealContactDamageCoroutine);
        }
    }

    private IEnumerator DealContactDamage()
    {
        while (true)
        {
            playerState.TakeDamage(contactDamage);
            yield return new WaitForSeconds(contactDamageDelay);
        }
    }

    private void MoveToPlayer()
    {
        var normalizedTargetPosition = (playerTransform.position - transform.position).normalized;
        var targetPositionXY = new Vector3(normalizedTargetPosition.x, 0, normalizedTargetPosition.z);
        enemyRigidbody.velocity = transform.TransformDirection(targetPositionXY * (moveSpeed * Time.fixedDeltaTime));
    }

    private void LookAtPlayer()
    {
        var lookRotation = Quaternion.LookRotation(playerTransform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.fixedDeltaTime);
    }
}