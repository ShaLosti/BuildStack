using System;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class MovingCube : MonoBehaviour
{
    [Range(0, 6)]
    [SerializeField] private float moveSpeed = 1f;

    public static MovingCube CurrentCube { get; private set; }
    public static MovingCube LastCube { get; private set; }

    internal void Stop()
    {
        moveSpeed = 0;
        float hangover = transform.position.z - LastCube.transform.position.z;

        float direction = hangover > 0 ? 1f : -1f;
        SplitCubeOnZ(hangover, direction);
    }

    private void SplitCubeOnZ(float hangover, float direction)
    {
        float newZSize = LastCube.transform.localScale.z - Mathf.Abs(hangover);
        float fallingBlockSize = transform.localScale.z - newZSize;

        float newZPosition = LastCube.transform.position.z + hangover / 2;
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, newZSize);
        transform.position = new Vector3(transform.position.x, transform.position.y, newZPosition);

        float cubeEdge = transform.position.z + (newZSize / 2f * direction);
        float fallingBlockZPosition = cubeEdge + fallingBlockSize / 2f * direction;
        
        SpawnDropeCube(fallingBlockZPosition , fallingBlockSize);
    }

    private void SpawnDropeCube(float fallingBlockZPosition , float fallingBlockSize)
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.GetComponent<Renderer>().shadowCastingMode = ShadowCastingMode.Off;
        cube.GetComponent<Renderer>().material = GetComponent<MeshRenderer>().material;
        cube.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, fallingBlockSize);
        cube.transform.position = new Vector3(transform.position.x, transform.position.y, fallingBlockZPosition);
        cube.AddComponent<Rigidbody>();

        Destroy(cube, 1f);
    }

    private void OnEnable()
    {
        if (LastCube == null)
            LastCube = GameObject.Find("StartCube").GetComponent<MovingCube>();

        CurrentCube = this;
        GetComponent<Renderer>().material.color = GetRandomColor();
    }

    private Color GetRandomColor()
    {
        return new Color(UnityEngine.Random.Range(0, 1f), UnityEngine.Random.Range(0, 1f), UnityEngine.Random.Range(0, 1f));
    }

    private void Start()
    {
        //transform.DOMoveZ(transform.position.z + 4, 2f);
    }
    private void Update()
    {
        transform.position += transform.forward * Time.deltaTime * moveSpeed;
    }
}
