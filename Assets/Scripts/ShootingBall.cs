using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingBall : MonoBehaviour
{
    // Start is called before the first frame update
    public Color ballColor;
    public Vector3 targetPos;

    public float speed;

    public void SetColor(Color newColor)
    {
        ballColor = newColor;

        GetComponent<MeshRenderer>().material.color = ballColor;
    }

    public Color GetColor()
    {
        return ballColor;
    }

    public void SetTargetPos(Vector3 pos)
    {
        targetPos = pos;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
    }
}
