using UnityEngine;

public class CameraPickup : MonoBehaviour
{
    public Camera playerCamera;          // assign in inspector
    public float rayDistance = 3f;       // how far the raycast reaches
    public LayerMask pickupLayer;        // filter for pickup items
    private GameObject heldItem;

    void Update()
    {
        // Cast a ray from the camera center forward
        if (Input.GetMouseButtonDown(0)) // Left click to pick up
        {
            if (heldItem == null)
            {
                Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
                if (Physics.Raycast(ray, out RaycastHit hit, rayDistance, pickupLayer))
                {
                    if (hit.collider.CompareTag("Pickup"))
                    {
                        heldItem = hit.collider.gameObject;
                        heldItem.GetComponent<Rigidbody>().isKinematic = true;

                        // Parent to hand
                        heldItem.transform.SetParent(transform);

                        // Reset local position/rotation so it aligns with the hand
                        heldItem.transform.localPosition = Vector3.zero;
                        heldItem.transform.localRotation = Quaternion.identity;

                        Debug.Log($"Picked up: {heldItem.name}");
                    }
                }
            }
        }

        // Drop item
        if (Input.GetMouseButtonDown(1)) // Right click
        {
            if (heldItem != null)
            {
                heldItem.transform.SetParent(null);
                heldItem.GetComponent<Rigidbody>().isKinematic = false;
                heldItem = null;
            }
        }
    }

    void OnDrawGizmos()
    {
        if (playerCamera != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(playerCamera.transform.position, playerCamera.transform.forward * rayDistance);
        }
    }
}