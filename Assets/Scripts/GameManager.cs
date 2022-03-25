using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject shootingBallPrefab;

    public List<Color> availableColor;

    public List<GameObject> baseListObjects;
    
    public Color currentColor;

    Color myAnswerColor;
    public Color correctAnswerColor;

    public Camera camera;

    public LineRenderer laserDetection;

    public GameObject UIavalaibleColorsPrefab;

    public GameObject UIPArentPanel;

    public GameObject RotationBase;

    int currentTaskIndex;

    [SerializeField]
    bool TaskCompleted;

    bool CanShoot;

    // Start is called before the first frame update
    void Start()
    {
        TaskCompleted = false;
        CanShoot = false;

        availableColor.Add(Color.white);
        availableColor.Add(Color.black);
        availableColor.Add(Color.red);
        availableColor.Add(Color.green);
        availableColor.Add(Color.blue);

        currentColor = availableColor[0];

        for(int i = 0; i < availableColor.Count; i++)
        {
            GameObject uiPanel = Instantiate(UIavalaibleColorsPrefab, UIPArentPanel.transform);

            uiPanel.GetComponent<Image>().color = availableColor[i];
            uiPanel.transform.GetChild(0).GetComponent<Text>().text = (i + 1).ToString();

            if (availableColor[i] == Color.white)
                uiPanel.transform.GetChild(0).GetComponent<Text>().color = Color.black;
        }

        currentTaskIndex = 0;

        for(int i = 0; i < baseListObjects.Count; i++)
        {
            if(currentTaskIndex != i)
                baseListObjects[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        Vector3 cameraPosition = camera.transform.position;
        Vector3 spawnPosition = new Vector3(cameraPosition.x + 2.0f, 5.0f, cameraPosition.z + 5.0f);
        laserDetection.SetPosition(0, spawnPosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            laserDetection.SetPosition(1, hit.point);

            if(hit.collider.gameObject.CompareTag("Colorball"))
            {
                laserDetection.material.color = Color.green;
                laserDetection.material.color = Color.green;

                CanShoot = true;
            }
            else
            {
                laserDetection.material.color = Color.red;
                laserDetection.material.color = Color.red;

                CanShoot = false;
            }
        }

        if(CanShoot && Input.GetMouseButtonUp(0))
        {
            GameObject shootBall = Instantiate(shootingBallPrefab, spawnPosition, Quaternion.identity) as GameObject;
            shootBall.GetComponent<ShootingBall>().SetTargetPos(laserDetection.GetPosition(1));
            shootBall.GetComponent<ShootingBall>().SetColor(currentColor);
        }

        if(Input.GetKeyDown(KeyCode.Alpha1) && availableColor[0] != null)
        {
            currentColor = availableColor[0];
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && availableColor[1] != null)
        {
            currentColor = availableColor[1];
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && availableColor[2] != null)
        {
            currentColor = availableColor[2];
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) && availableColor[3] != null)
        {
            currentColor = availableColor[3];
        }
        if (Input.GetKeyDown(KeyCode.Alpha5) && availableColor[4] != null)
        {
            currentColor = availableColor[4];
        }
    }

    public void SetAnswerColor(Color newColor)
    {
        myAnswerColor = newColor;

        if (myAnswerColor == correctAnswerColor)
        {
            TaskCompleted = true;

            RotateBase();
        }
    }

    void RotateBase()
    {
        Debug.Log("Rotation Started");

        float currentRotation = RotationBase.transform.localRotation.y;
        float targetRotationY = currentRotation - 90.0f;

        RotationBase.transform.Rotate(0.0f, targetRotationY, 0.0f, Space.Self);

        TaskCompleted = false;

        baseListObjects[currentTaskIndex].SetActive(false);

        currentTaskIndex++;

        baseListObjects[currentTaskIndex].SetActive(true);

        if(currentTaskIndex < baseListObjects.Count)
            SetNewAnswerColor();
    }

    void SetNewAnswerColor()
    {
        //myAnswerColor = null;
        correctAnswerColor = baseListObjects[currentTaskIndex].transform.GetChild(1).GetComponent<AnswerBall>().goalColor;
    }
}
