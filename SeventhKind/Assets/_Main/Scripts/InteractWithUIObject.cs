using UnityEngine;

public class InteractWithUIObject : MonoBehaviour
{
    public Camera mainCamera;
    public float maxDistance;
    public int interactionLayer;
    public float newDistance;
    public GameObject HUD;

    [HideInInspector]
    public Vector3 previousPos;

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
                GameObject raycastTarget = raycastHit.collider.gameObject;
                if (raycastTarget.name == "Computer Base")
                {
                    ComputerMainScript computer = raycastTarget.transform.GetChild(0).GetChild(0).GetComponent<ComputerMainScript>();
                    computer.gameObject.SetActive(true);
                    computer.currentPlayer = gameObject;
                    
                }
                Vector3 newPos = raycastTarget.transform.TransformPoint(raycastTarget.transform.lossyScale.x / 2, -1.675f, -newDistance);
                Vector3 newRotation = raycastTarget.transform.rotation.eulerAngles;
                gameObject.transform.position = newPos;
                gameObject.transform.rotation = Quaternion.Euler(newRotation);
                mainCamera.transform.localRotation = Quaternion.identity;
            }
        }
    }

    public void ExitInteraction()
    {
        gameObject.transform.position = previousPos;
        gameObject.GetComponent<PlayerControls>().isMovementDisabled = false;
        mainCamera.GetComponent<CameraToMouse>().TurnOn();
        HUD.SetActive(true);
        gameObject.GetComponent<CapsuleCollider>().enabled = true;
        gameObject.GetComponent<Rigidbody>().useGravity = true;
    }

    public void EnterInteraction()
    {
        previousPos = gameObject.transform.position;
        gameObject.GetComponent<PlayerControls>().isMovementDisabled = true;
        mainCamera.GetComponent<CameraToMouse>().TurnOff();
        HUD.SetActive(false);
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        gameObject.GetComponent<Rigidbody>().useGravity = false;
    }
}