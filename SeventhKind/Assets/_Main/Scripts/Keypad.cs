using UnityEngine;

public class Keypad : MonoBehaviour
{
    public uint code;
    public Camera mainCamera;

    private string inputCode;
    private RaycastHit raycastHit;

    private void Update()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out raycastHit))
        {
            for (int i = 0; i < 10; i++)
            {
                if (raycastHit.collider.gameObject.name == "0" + i + "_ButtonCollider")
                {
                    inputCode += i;
                }
            }
        }
    }
}