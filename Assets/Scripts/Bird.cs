using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Bird : MonoBehaviour {

    private bool isClick = false;//判断鼠标按下还是抬起
    public float maxDis = 3;//最大距离
    [HideInInspector]//虽然是公有变量，但是在InInspector面板中隐藏
    public SpringJoint2D sp;//控制失效
    protected Rigidbody2D rg;//速度

    //画线
    public LineRenderer right;
    public Transform rightPos;//右边子物体位置
    public LineRenderer left;
    public Transform leftPos;//左边子物体位置

    public GameObject boom;
    protected TestMyTrail myTrail;
    [HideInInspector]
    public bool canMove = false;//判断是否在可以拉动的范围内
    public float smooth = 3;//平滑值

    //播放音效
    public AudioClip select;
    public AudioClip fly;

    private bool isFly = false;
    public bool isReleased = false;

    public Sprite hurt;
    protected SpriteRenderer render;


    private void Awake()
    {
        sp = GetComponent<SpringJoint2D>();
        rg = GetComponent<Rigidbody2D>();
        myTrail = GetComponent<TestMyTrail>();
        render = GetComponent<SpriteRenderer>();
    }

    private void OnMouseDown()//鼠标按下
    {
        if (canMove)
        {
            Audioplay(select);
            isClick = true;
            rg.isKinematic = true;//开启动力学
        }
    }

    private void OnMouseUp()//鼠标抬起
    {
        if (canMove)
        {
            isClick = false;
            rg.isKinematic = false;
            Invoke("Fly", 0.1f);
            //禁用画线组件
            right.enabled = false;
            left.enabled = false;
            canMove = false;
        }
    }

    private void Update()
    {
        //判断当前是否点击UI界面，避免点暂停时触发小鸟的技能
        if(EventSystem.current.IsPointerOverGameObject())
        {
            //点击UI界面则返回
            return;
        }

        if(isClick)
        {
            //鼠标一直按下，进行位置的跟随
            //游戏中是世界坐标，也就是原点在画面中间，而玩家往往是把屏幕左下角当做原点
            //Camera.main.ScreenToWorldPoint方法可以把屏幕坐标转换成世界坐标
            transform.position =Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //由于调用了主摄像机的方法，小鸟也获得了主摄像机的Z轴，所以要减去这个Z轴让小鸟保持在平面上
            transform.position += new Vector3(0,0,-Camera.main.transform.position.z);
            if (Vector3.Distance(transform.position, rightPos.position) > maxDis)
            {
                //进行位置限定，不能大于最大距离
                Vector3 pos = (transform.position - rightPos.position).normalized;//两点坐标求向量，并且单位化
                pos *= maxDis;//最大长度向量
                transform.position = pos + rightPos.position;
            }
            Line();
        }

        //相机跟随
        float posX = transform.position.x;
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position,new Vector3(Mathf.Clamp(posX,0,9),Camera.main.transform.position.y,Camera.main.transform.position.z),smooth*Time.deltaTime);

        if (isFly)
        {
            if (Input.GetMouseButtonDown(0))
            {
                ShowSkill();
            }
        }
    }

    void Fly()
    {
        //开始飞行
        isReleased = true;
        isFly = true;
        Audioplay(fly);
        myTrail.StartTrails();
        sp.enabled = false;
        Invoke("Next", 5);
    }

    void Line()
    {
        //画线操作，在弹弓和小鸟之间画线
        right.enabled = true;
        left.enabled = true;

        right.SetPosition(0, rightPos.position);
        right.SetPosition(1, transform.position);

        left.SetPosition(0, leftPos.position);
        left.SetPosition(1, transform.position);

    }

    protected virtual void Next()
    {
        //对小鸟进行轮换飞出
        GameManager._instance.birds.Remove(this);//移除小鸟
        Destroy(gameObject);
        Instantiate(boom,transform.position,Quaternion.identity);
        GameManager._instance.NextBird();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //碰撞到物体结束拖尾和修改飞行状态
        isFly = false;
        myTrail.ClearTrails();

    }

    //播放音效
    public void Audioplay(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip,transform.position);
    }

    //炫技
    public virtual void ShowSkill()
    {
        isFly = false;

    }

    //小鸟受伤
    public void Hurt()
    {
        render.sprite = hurt;
    }
}
