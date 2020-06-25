using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField]
    private MovingCube cubePref;
    [SerializeField]
    private MoveDirection moveDirection;

    public void SpawnCube()
    {
        var cube = Instantiate(cubePref);
        if(MovingCube.LastCube != null && MovingCube.LastCube.gameObject != GameObject.Find("StartCube"))
        {
            float x = moveDirection == MoveDirection.X ? transform.position.x : MovingCube.LastCube.transform.position.x;
            float z = moveDirection == MoveDirection.Z ? transform.position.z : MovingCube.LastCube.transform.position.z;

            cube.transform.position = new Vector3(x,
                MovingCube.LastCube.transform.position.y + cubePref.transform.localScale.y,
                z);
        }
        else
        {
            cube.transform.position = transform.position;
        }

        cube.MoveDirection = moveDirection;
    }
}
