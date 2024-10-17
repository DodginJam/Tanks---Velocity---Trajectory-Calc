using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveShell : MonoBehaviour
{

    [field: SerializeField] public float Speed
    { get; private set; } = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 velocity = Vector3.forward * Speed;
        transform.Translate(velocity * Time.deltaTime);
    }
}
