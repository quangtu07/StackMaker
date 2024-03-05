using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackingBrick : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform stackPlayerList;
    private float jumpHeight = 0.12f;
    float objectHeight = -1f;
    List<Transform> listPlayerBrick;
    // Start is called before the first frame update

    private void Start()
    {
        listPlayerBrick = new List<Transform>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Brick"))
        {
            // Tăng chiều cao của player
            IncreasePlayerPos();

            // Thêm brick
            AddBrick(other);

        }
        else if (other.CompareTag("UnBrick"))
        {
            // Loại bỏ brick
            RemoveBrick();

            // Giảm chiều cao của player

            DecreasePlayerPos();

            BrickItem childMeshRenderer = other.transform.GetComponent<BrickItem>();
            if (childMeshRenderer)
            {
                childMeshRenderer.ShowChildObject();
            }

            // Cập nhật objectHeight sau khi loại bỏ một brick
            objectHeight -= 0.1f; // Nếu bạn giữ objectHeight được cập nhật như trước đó
        } else if (other.CompareTag("Finish"))
        {
            ClearBricks();
            GameManager.Instance.ChangeVictory();
        }
    }

    void AddBrick(Collider other)
    {      

        Transform t = other.transform;
        // Xóa Collider của brick
        Collider brickCollider = other.GetComponent<Collider>();
        if (brickCollider != null)
        {
            Destroy(brickCollider);
        }

        listPlayerBrick.Add(t);
        other.gameObject.tag = "Untagged"; // Cập nhật tag của Brick
        t.SetParent(stackPlayerList);
        objectHeight = listPlayerBrick.Count * 0.1f - 1f; // Cập nhật objectHeight dựa trên số lượng Brick
        t.localPosition = new Vector3(0, objectHeight, 0);

        UIManager.Instance.ChangeBrickNumber(listPlayerBrick.Count);
    }

    void RemoveBrick()
    {
        // Lấy brick cuối cùng từ stack và loại bỏ nó
        Transform lastBrick = listPlayerBrick[listPlayerBrick.Count - 1];
        listPlayerBrick.RemoveAt(listPlayerBrick.Count - 1);
        Destroy(lastBrick.gameObject); // Xóa GameObject

        UIManager.Instance.ChangeBrickNumber(listPlayerBrick.Count);
    }

    void ClearBricks()
    {
        foreach (var brick in listPlayerBrick)
        {
            Destroy(brick.gameObject);
            DecreasePlayerPos();
        }
    }

    void IncreasePlayerPos()
    {
        Vector3 newPos = player.position;
        newPos.y += jumpHeight;
        player.position = newPos;
    }

    void DecreasePlayerPos()
    {
        Vector3 newPos = player.position;
        newPos.y -= jumpHeight;
        player.position = newPos;
    }
}
