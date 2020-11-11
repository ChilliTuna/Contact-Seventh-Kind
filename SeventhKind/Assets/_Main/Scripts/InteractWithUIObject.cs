using UnityEngine;

public class InteractWithUIObject : MonoBehaviour
{
    public Camera mainCamera;
    public float maxDistance;
    public int interactionLayer;
    public GameObject HUD;

    [HideInInspector]
    public GameObject currentInteraction;
    [HideInInspector]
    public bool isInteracting;

    private bool doInteraction;
    private Vector3 previousPos;
    private Quaternion previousRotation;
    private RaycastHit raycastHit;
    private GrabItem grabSensorScript;

    private void Start()
    {
        grabSensorScript = transform.Find("Main Camera").transform.Find("Item Grab Sensor").GetComponent<GrabItem>();
        
    }

    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Mouse0)) && !grabSensorScript.isGrabbing)
        {
            Debug.Log("Click!");
            doInteraction = true;
        }
        Ray ray = mainCamera.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
        if (Physics.Raycast(ray, out raycastHit, maxDistance, 1 << interactionLayer) && doInteraction)
        {
            doInteraction = false;
            Debug.Log("Raycast Hit");
            EnterInteraction();
            currentInteraction = raycastHit.collider.gameObject;
            if (currentInteraction.name == "Computer Base")
            {
                ComputerMainScript computerScript = currentInteraction.transform.GetChild(0).GetChild(0).GetComponent<ComputerMainScript>();
                computerScript.gameObject.SetActive(true);
                computerScript.currentPlayer = gameObject;
            }
            else
            {
                Interactable interactionScript = currentInteraction.GetComponent<Interactable>();
                interactionScript.currentPlayer = gameObject;
            }
            Interactable interactDetails = currentInteraction.GetComponent<Interactable>();
            Vector3 newPos = currentInteraction.transform.TransformPoint(interactDetails.xOffset, 0, interactDetails.viewDistance);
            newPos.y = interactDetails.viewHeight;
            Vector3 newRotation = currentInteraction.transform.rotation.eulerAngles;
            newRotation.y -= 180;
            gameObject.transform.position = newPos;
            gameObject.transform.rotation = Quaternion.Euler(newRotation);
            mainCamera.transform.localRotation = Quaternion.identity;
        }
        if (currentInteraction != null)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (currentInteraction.name != "Computer Base")
                {
                    ExitInteraction();
                }
            }
        }
        doInteraction = false;
    }

    public void ExitInteraction()
    {
        isInteracting = false;
        gameObject.transform.position = previousPos;
        gameObject.transform.rotation = previousRotation;
        gameObject.GetComponent<PlayerControls>().isMovementDisabled = false;
        mainCamera.GetComponent<CameraToMouse>().TurnOn();
        HUD.SetActive(true);
        gameObject.GetComponent<CapsuleCollider>().enabled = true;
        gameObject.GetComponent<Rigidbody>().useGravity = true;
        currentInteraction = null;
    }

    public void EnterInteraction()
    {
        isInteracting = true;
        previousPos = gameObject.transform.position;
        previousRotation = gameObject.transform.rotation;
        gameObject.GetComponent<PlayerControls>().isMovementDisabled = true;
        mainCamera.GetComponent<CameraToMouse>().TurnOff();
        HUD.SetActive(false);
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        gameObject.GetComponent<Rigidbody>().useGravity = false;
    }
}