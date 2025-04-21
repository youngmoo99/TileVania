using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCamera : MonoBehaviour
{

    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float moveDistance = 10f;

    private Vector3 startPosition;
    private float movedDistance = 0f;

    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float step = moveSpeed * Time.deltaTime;
        transform.Translate(Vector3.right * step);

        movedDistance += step;

        if(movedDistance >= moveDistance) 
        {
            transform.position = startPosition;
            movedDistance = 0f;
        }
    }
}
