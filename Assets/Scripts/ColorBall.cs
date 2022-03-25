using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorBall : MonoBehaviour
{
    public Color currentColor;

    public Material transparentMat;
    public Material diffuseMat;

    public GameManager manager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ShootingBall"))
        {
            Destroy(other.gameObject);

            GetComponent<MeshRenderer>().material = diffuseMat;

            currentColor = other.gameObject.GetComponent<ShootingBall>().GetColor();

            GetComponent<MeshRenderer>().material.color = currentColor;

            manager.SetAnswerColor(currentColor);
        }
    }
}
