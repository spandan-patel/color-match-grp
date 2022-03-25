using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerBall : MonoBehaviour
{
    public Color goalColor;

    public GameManager manager;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<MeshRenderer>().material.color = goalColor;

        manager.correctAnswerColor = goalColor;
    }
}
