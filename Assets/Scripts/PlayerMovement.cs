using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 5.0f;
    [SerializeField]
    Transform movePoint;
    [SerializeField]
    GridManager gridManager;



    // Start is called before the first frame update
    void Start()
    {
        movePoint.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, movePoint.position) <= 0.05f)
        {
            Vector2Int moveDir = new Vector2Int(0, 0);
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
            {
                moveDir += new Vector2Int((int)Input.GetAxisRaw("Horizontal"), 0);
            }

            if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
            {
               moveDir += new Vector2Int(0, (int)Input.GetAxisRaw("Vertical"));
            }

            Vector2Int newCoord = new Vector2Int((int)movePoint.position.x + moveDir.x, (int)movePoint.position.z + moveDir.y);

            if (gridManager.Grid.ContainsKey(newCoord) && gridManager.Grid[newCoord].GetComponent<Node>().isWalkable)
            {
                movePoint.position += new Vector3(moveDir.x, 0f, moveDir.y);
            }
        }
    }
}
