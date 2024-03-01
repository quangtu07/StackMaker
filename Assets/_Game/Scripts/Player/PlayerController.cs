using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;
    [SerializeField] private Transform rayCastPosition;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private BrickItem brickPrefab;
    [SerializeField] private GameObject playerBrickPrefab;
    [SerializeField] private Transform playerListBrick;
    private List<GameObject> listPlayerBrick;
    private Vector3 startMousePosition;
    private Vector3 endMousePosition;
    private Vector3 targetPosition;
    private Vector3 swipeDirection;
    private float speed;
    private float raySpacing;
    private bool isMoving;

    private void Start()
    {
        transform.position = startPoint.position;
        swipeDirection = Vector3.zero;
        speed = 5f;
        raySpacing = 0.5f;
        isMoving = false;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Lưu vị trí bắt đầu vuốt
            startMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            // Lưu vị trí kết thúc vuốt và xác định hướng vuốt
            endMousePosition = Input.mousePosition;
            swipeDirection = DetermineSwipeDirection(startMousePosition, endMousePosition);
        }
        
    }

    void FixedUpdate()
    {

        if (swipeDirection != Vector3.zero && !isMoving)
        {
            ShootRays();
        }
        
        if (isMoving)
        {
            MoveCharacter();
            if (Vector3.Distance(transform.position,targetPosition) <= 0.2f)
            {
                transform.position = new Vector3(targetPosition.x, transform.position.y, targetPosition.z);
                isMoving = false;
            }

            Debug.Log(Vector3.Distance(transform.position, endPoint.position));
            if (Vector3.Distance(transform.position, endPoint.position) <= 0.6f)
            {
                isMoving = false;
            }
        }
        
        
    }

    void ShootRays()
    {
        Vector3 rayStart = transform.position;
        RaycastHit hit;

        while(true)
        {
            rayStart += swipeDirection * raySpacing;

            Ray ray = new Ray(rayStart, Vector3.down * 5f);

            Debug.DrawRay(rayStart, Vector3.down * 5f, Color.red);


            if (Physics.Raycast(rayStart, Vector3.down, out hit, 5f, wallLayer))
            {
                // Ray hit the wall, set end position and start moving
                targetPosition = hit.point - swipeDirection/2;
                isMoving = true;
                return;
            }
        }
            
    }

    Vector3 DetermineSwipeDirection(Vector3 start, Vector3 end)
    {
        Vector3 direction = end - start;
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            // Vuốt ngang
            return (direction.x > 0) ? Vector3.right : Vector3.left;
        }
        else
        {
            // Vuốt dọc
            return (direction.y > 0) ? Vector3.forward : Vector3.back;
        }
    }

    void MoveCharacter()
    {
       transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.fixedDeltaTime);
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "GroundBrick")
        {
            Debug.Log("collision with ground brick");
            Addbrick();
            HideBrick(collision.gameObject);
        }
    }

    void Addbrick()
    {
        GameObject playerBrick = Instantiate(playerBrickPrefab,new Vector3(transform.position.x, 0, transform.position.z), Quaternion.identity);
        playerBrick.transform.SetParent(transform, true);

    }

    void HideBrick(GameObject groundBrick)
    {
        Transform child = groundBrick.gameObject.transform.GetChild(0);
        child.gameObject.SetActive(false);
    }

    void ClearBricks()
    {
        listPlayerBrick.Clear();
    }

}
