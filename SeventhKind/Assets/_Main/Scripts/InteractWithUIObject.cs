using UnityEngine;

public class InteractWithUIObject : MonoBehaviour
{
    public Camera mainCamera;
    public float maxDistance;
    public int interactionLayer;
    public float newDistance;
    public float newHeight;
    public GameObject HUD;

    private Vector3 previousPos;
    private Quaternion previousRotation;
    private GameObject currentInteraction;
    private RaycastHit raycastHit;

    // Start is called before the first frame update
    private void Start()
    {
    }

    private void FixedUpdate()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out raycastHit, maxDistance, 1 << interactionLayer))
        {
            Debug.DrawLine(ray.origin, raycastHit.point);
            Debug.Log("Raycast Hit");
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("E");
                EnterInteraction();
                currentInteraction = raycastHit.collider.gameObject;
                if (currentInteraction.name == "Computer Base")
                {
                    ComputerMainScript computer = currentInteraction.transform.GetChild(0).GetChild(0).GetComponent<ComputerMainScript>();
                    computer.gameObject.SetActive(true);
                    computer.currentPlayer = gameObject;
                }
                Vector3 newPos = currentInteraction.transform.TransformPoint(currentInteraction.transform.lossyScale.x / 2, 0, newDistance);
                newPos.y = newHeight;
                Vector3 newRotation = currentInteraction.transform.rotation.eulerAngles;
                newRotation.y -= 180;
                gameObject.transform.position = newPos;
                gameObject.transform.rotation = Quaternion.Euler(newRotation);
                mainCamera.transform.localRotation = Quaternion.identity;
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentInteraction.name != "Computer Base")
            {
                ExitInteraction();
            }
        }
    }

    public void ExitInteraction()
    {
        gameObject.transform.position = previousPos;
        gameObject.transform.rotation = previousRotation;
        gameObject.GetComponent<PlayerControls>().isMovementDisabled = false;
        mainCamera.GetComponent<CameraToMouse>().TurnOn();
        HUD.SetActive(true);
        gameObject.GetComponent<CapsuleCollider>().enabled = true;
        gameObject.GetComponent<Rigidbody>().useGravity = true;
    }

    public void EnterInteraction()
    {
        previousPos = gameObject.transform.position;
        previousRotation = gameObject.transform.rotation;
        gameObject.GetComponent<PlayerControls>().isMovementDisabled = true;
        mainCamera.GetComponent<CameraToMouse>().TurnOff();
        HUD.SetActive(false);
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        gameObject.GetComponent<Rigidbody>().useGravity = false;
    }
}