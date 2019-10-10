using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatController : MonoBehaviour
{
    //实例化的对象
    public GameObject effect;
    private Vector3 rawPosition;
    private Vector3 hatPosition;
    private float maxWidth;
    // Start is called before the first frame update
    void Start()
    {
        //将屏幕的宽度转换成世界坐标
        Vector3 screenPos = new Vector3(Screen.width, 0, 0);
        Vector3 moveWidth = Camera.main.ScreenToWorldPoint(screenPos);
        //计算帽子的宽度
        float hatWidth = GetComponent<Renderer>().bounds.extents.x;
        hatPosition = transform.position;
        //计算帽子的移动宽度
        maxWidth = moveWidth.x - hatWidth;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //将鼠标的屏幕位置转换成世界坐标
        rawPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //设置帽子将要移动的位置，帽子移动范围控制
        hatPosition = new Vector3(rawPosition.x, hatPosition.y, 0);
        hatPosition.x = Mathf.Clamp(hatPosition.x, -maxWidth, maxWidth);
        //帽子移动
        GetComponent<Rigidbody2D>().MovePosition(hatPosition);
    }
    //有碰撞体进入触发器时触发
    void OnTriggerEnter2D(Collider2D col)
    {
        //实例化粒子效果，并将粒子效果设置为帽子的子物体
        GameObject neweffect = (GameObject)Instantiate(effect, transform.position, effect.transform.rotation);
        neweffect.transform.parent = transform;
        //删除该碰撞体的物体
        Destroy(col.gameObject);
        Destroy(neweffect, 1.0f);
    }
}
