using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buyer : MonoBehaviour
{
    public float moveSpeed = 5f; // 移动速度
    public GameController gameController; // 引用 GameController 脚本
    private Queue<Vector3> path; // 用于存储路径点
    private Vector3 targetPosition; // 目标位置

    void Start()
    {
        // 初始化买家的位置
        transform.position = new Vector3(0, 0, 0);
        path = new Queue<Vector3>();
        //初始化目标位置
        targetPosition = new Vector3(0, 0, 0);
        FindPathToBox();
    }

    void Update()
    {
        // 如果有路径点，则移动
        if (path.Count > 0)
        {
            targetPosition = path.Peek(); // 获取下一个目标位置
            MoveTowardsTarget();
        }
    }

    private void MoveTowardsTarget()
    {
        // 移动到目标位置
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // 如果到达目标位置，则从路径中移除该点
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            path.Dequeue(); // 移除已到达的目标点
        }
    }

    private void FindPathToBox()
    {
        // 清空路径
        path.Clear();
        
        // 示例: 寻找第一个可用的 box
        for (int x = 0; x < gameController.box.Length; x++)
        {
            for (int y = 0; y < gameController.box[0].Length; y++)
            {
                if (!gameController.box[x][y].isAppear) // 找到可用的 box
                {
                    // 在这里简单的生成路径，只是示例
                    Vector3 boxPosition = new Vector3(x * gameController.cellSize - 5, y * gameController.cellSize - 5, 0);
                    path.Enqueue(boxPosition);
                    return; // 只找到第一个合适的 box 后退出
                }
            }
        }
        
        Debug.Log("No available box found.");
    }
}
