using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public struct Box
    {
        public bool isAppear;
        public GameObject goods;
    }

    public Box[][] box;
    public GameObject boxObj;
    public float cellSize = 1.0f; // 单元格大小

    void Start()
    {
        Screen.SetResolution(1280, 720, false);
        InitMap();
    }

    private void InitMap()
    {
        int box_x_num = 15; // 设置数组的大小为 10
        box = new Box[box_x_num][];
        for (int i = 0; i < box_x_num; i++)
        {
            box[i] = new Box[box_x_num]; 
        }
    }

    // Update is called once per frame
    // Update is called once per frame
    void Update()
    {
        // 获取鼠标点击的位置
        if (Input.GetMouseButtonDown(0))
        {
            // 获取鼠标屏幕坐标
            Vector3 mousePosition = Input.mousePosition;

            // 将鼠标屏幕坐标转换为世界坐标
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, Camera.main.nearClipPlane));

            // 调试输出点击的世界坐标
            Debug.Log($"World position clicked: ({worldPosition.x}, {worldPosition.y})");

            // 设置偏移量，使得所有世界坐标都能映射到数组索引范围内
            float offsetX = 6; // 根据你的世界坐标范围来设置一个合适的偏移值
            float offsetY = 3; // 同上

            // 将偏移后的世界坐标映射到数组索引
            int currentX = Mathf.FloorToInt((worldPosition.x + offsetX) / cellSize);
            int currentY = Mathf.FloorToInt((worldPosition.y + offsetY) / cellSize);

            // 检查坐标是否在有效范围内，并且没有被占用
            if (currentX >= 0 && currentX < box.Length && currentY >= 0 && currentY < box[0].Length)
            {
                // 检查当前格子和下一个格子是否都为空
                if (!box[currentX][currentY].isAppear && !box[currentX][currentY + 1].isAppear)
                {
                    // 实例化货架对象
                    Instantiate(boxObj, new Vector3(currentX * cellSize - offsetX, currentY * cellSize - offsetY, 0), Quaternion.identity);
                    
                    // 更新 Box 的状态，标记两个格子为已占用
                    box[currentX][currentY].isAppear = true;
                    box[currentX][currentY + 1].isAppear = true;
                }
                else
                {
                    Debug.Log("The space is already occupied.");
                }
            }
            else
            {
                Debug.Log("Click position is out of bounds.");
            }
        }
    }


}
