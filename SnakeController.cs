using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    private List<Material> _tailMaterials;
    [SerializeField] private List<Transform> _tailTransforms;
    [SerializeField] private GameObject _tailPrefab;
    [SerializeField] private float _tailDistance;
    [Range(0, 50), SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private Vector3 _moveDir;
    [SerializeField] private Vector3 _rotationDir;
    [SerializeField] private Quaternion _originalDir;
    private void FixedUpdate()
    {
        Move(_moveDir, _speed);
        Rotate(_rotationSpeed, _rotationDir);
        MoveTail(_tailDistance); 
    }

    private void Move(Vector3 moveDir, float speed)
    {
        transform.position = transform.position + moveDir * speed * Time.deltaTime;
    }

    private void Rotate(float rotationSpeed, Vector3 rotationDir)
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            transform.position = transform.position + rotationDir * horizontalInput * Time.deltaTime;
            transform.Rotate(0, rotationSpeed * horizontalInput * Time.deltaTime, 0);
        }
        else
        {
            transform.rotation = _originalDir;
        }
    }

    private void MoveTail(float tailDistance)
    {
        float distance = tailDistance;
        Vector3 prevPos = transform.position;
        Quaternion prevRotation = transform.rotation;

        foreach (var tail in _tailTransforms)
        {
            if ((tail.position - prevPos).magnitude > distance)
            {
                Quaternion currentRotation = tail.rotation;
                Vector3 currentPos = tail.position;
                tail.position = prevPos;
                tail.rotation = prevRotation;
                prevPos = currentPos;
                prevRotation = currentRotation;
            }
            else
            { 
                break;
            }
        }    
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Food"))
        {
            Destroy(other.gameObject);
            GameObject tail = Instantiate(_tailPrefab);
            _tailTransforms.Add(tail.transform);
            _tailMaterials.Add(tail.GetComponent<Renderer>().material);
        }
        else if (other.CompareTag("ColorLine"))
        {
            GetComponent<Renderer>().material.color = other.GetComponent<Renderer>().material.color;
            foreach (var tail in _tailMaterials)
            {
                tail.color = other.GetComponent<Renderer>().material.color;
            }
        }
    }
}
