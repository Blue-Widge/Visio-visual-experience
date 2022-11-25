using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StartGameV3 : MonoBehaviour
{
    public InputActionReference startExperienceReference = null;
    //The target object to follow
    [SerializeField]
    private Transform cameraTransform;
    //Distance that the notebook is moved forward before the camera
    [SerializeField]
    private float forwardTimes;
    //Notebook
    [SerializeField]
    private GameObject notebook;
    //Gameobject the notebook needs to move to
    [SerializeField]
    private GameObject notebookEndPosition;
    //Moving speed of the notebook
    [SerializeField]
    private float movingSPeed;
    [SerializeField]
    private float rotationSpeed;
    //Visual impairment
    [SerializeField]
    private GameObject visualImpairment;

    private bool isStarted;
    private bool notebookMoved;

    private Vector3 desiredScale;

    // Start is called before the first frame update
    void Start()
    {
        isStarted = false;
        notebookMoved = false;
        desiredScale = new Vector3(notebook.transform.localScale.x / 2, notebook.transform.localScale.y / 2, notebook.transform.localScale.z / 2);
    }

    private void Update()
    {
        if (isStarted && !notebookMoved)
        {
            MoveNotebook();
        }
    }

    void LateUpdate()
    {
        if (!isStarted)
        {
            Vector3 resultingPosition = cameraTransform.position + cameraTransform.forward * forwardTimes;
            transform.position = resultingPosition;
            transform.LookAt(cameraTransform.position, cameraTransform.right);
        }
    }

    private void Awake()
    {
        startExperienceReference.action.started += StartKitchen;
    }

    private void OnDestroy()
    {
        startExperienceReference.action.started -= StartKitchen;
    }

    void StartKitchen(InputAction.CallbackContext context)
    {
        if (isStarted) return;
        isStarted = true;
    }

    void MoveNotebook()
    {
        if (notebook.transform.position != notebookEndPosition.transform.position)
        {
            notebook.transform.position = Vector3.MoveTowards(notebook.transform.position, notebookEndPosition.transform.position, Time.deltaTime * movingSPeed);
        }
        if (notebook.transform.rotation != notebookEndPosition.transform.rotation)
        {
            notebook.transform.rotation = Quaternion.Lerp(notebook.transform.rotation, notebookEndPosition.transform.rotation, Time.deltaTime * rotationSpeed);
        }
        if (notebook.transform.localScale != desiredScale)
        {
            notebook.transform.localScale = Vector3.Lerp(notebook.transform.localScale, desiredScale, Time.deltaTime * rotationSpeed);
            Debug.Log("Scaling !");
        }
        if (notebook.transform.position == notebookEndPosition.transform.position &&
          notebook.transform.rotation == notebookEndPosition.transform.rotation &&
          notebook.transform.localScale == desiredScale)
        {
            Debug.Log("Turning on visual impairment");
            visualImpairment.SetActive(true);

            notebookMoved = true;
        }
    }
}
