using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private FixedJoystick _fixedJoystick;
    [SerializeField] private Animator _animator;
    [SerializeField] public float speed = 5;

    private bool isPunching = false;
    private int punchCount = 0;
    private Coroutine punchCoroutine;
    public GameObject[] dropPrefabs;
    public GameObject treePrefab;
    private Vector3 _startPosition;

    private List<GameObject> objectsToDestroy = new List<GameObject>();

    void Start()
    {
     _startPosition = transform.position;
    }

    void Update()
    {
        if(transform.position.y <= -15)
        {
            transform.position = _startPosition;
        }
    }

    private void FixedUpdate()
    {
        if (!isPunching)
        {
            MoveCharacter();

            if (_fixedJoystick.Horizontal != 0 || _fixedJoystick.Vertical != 0)
            {
                transform.rotation = Quaternion.LookRotation(_rigidbody.velocity);
                _animator.SetBool("isRunning", true);
            }
            else
            {
                _animator.SetBool("isRunning", false);
            }

            if (Trigger.trigger)
            {
                punchCoroutine = StartCoroutine(PlayPunchAnimation());
            }
        }
        else
        {

            _rigidbody.velocity = Vector3.zero;
            _animator.SetBool("isRunning", false);
        }
    }

    private void MoveCharacter()
    {
        _rigidbody.velocity = new Vector3(_fixedJoystick.Horizontal * speed, _rigidbody.velocity.y, _fixedJoystick.Vertical * speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Trees"))
        {
            objectsToDestroy.Add(collision.gameObject);
        }
    }

    private IEnumerator PlayPunchAnimation()
    {
        isPunching = true;
        punchCount = 0;

        while (punchCount < 3)
        {
            _animator.SetBool("isRunning", false);
            _animator.SetBool("isPunch", true);
            yield return new WaitForSecondsRealtime(0.6f);
            _animator.SetBool("isPunch", false);
            punchCount++;

        }

        isPunching = false;

        foreach (GameObject obj in objectsToDestroy)
        {
            Destroy(obj);
            StartCoroutine(RespawnObject(obj)); 
            DropRandomObjects(obj.transform.position);
        }

        objectsToDestroy.Clear();
        Trigger.trigger = false;
    }

    private void DropRandomObjects(Vector3 position)
    {
        int randomDropCount = Random.Range(1, 3);

        for (int i = 0; i < randomDropCount; i++)
        {
            int randomDropIndex = Random.Range(0, dropPrefabs.Length);
            GameObject dropPrefab = dropPrefabs[randomDropIndex];


            Vector3 spawnOffset = Random.insideUnitSphere * Random.Range(2f, 0f);


            Vector3 spawnPosition = position + spawnOffset;

            GameObject newDrop = Instantiate(dropPrefab, spawnPosition, Quaternion.identity);
        }
    }

IEnumerator RespawnObject(GameObject obj)
{
    Vector3 savedPosition = obj.transform.position;
    Quaternion savedRotation = obj.transform.rotation;
    
    Destroy(obj);

    yield return new WaitForSeconds(15f);
    GameObject newTree = Instantiate(treePrefab, savedPosition, savedRotation);
}
}
