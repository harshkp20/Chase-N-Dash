using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealthEnemy : MonoBehaviour
{
    public float RotationSpeed;
    public LineRenderer LineofSight;
    public float SearchDistance;
    public Gradient RedColor,GreenColor,BlueColor;
    float X_Position,Z_Position;
    public GameObject Player;
    Vector3 Instantiated_Position;
    public GameObject Enemy;
    Vector3 Difference;
    // Start is called before the first frame update
    void Start()
    {
        LineofSight = this.GetComponent<LineRenderer>();
        LineofSight.SetPosition(0,this.transform.position);
        Player = GameObject.FindGameObjectWithTag("Player");
        this.transform.Rotate(0,Random.Range(0,360),0);
        Enemy = GameObject.FindGameObjectWithTag("Enemy");
        Difference = Player.transform.position - Enemy.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(0,RotationSpeed*Time.deltaTime,0);
        LineofSight.SetPosition(1,this.transform.position+transform.right*SearchDistance);
        LineofSight.colorGradient = RedColor;

        var ray = new Ray(this.transform.position,transform.right);
        RaycastHit GroundDetection;
        if((Physics.Raycast(ray,out GroundDetection,SearchDistance)))
        {
            GameObject Collided = GroundDetection.transform.gameObject;
            LineofSight.SetPosition(1,GroundDetection.point);
            LineofSight.colorGradient = GreenColor;
            if(Collided.CompareTag("Player"))
            {
                if((Player.transform.position.x - Enemy.transform.position.x) > 4.2)
                {
                    Enemy.transform.position = Player.transform.position - new Vector3(4.2f,0,0);
                    Enemy.GetComponent<EnemyPatrol>().Speed +=4;
                    Mathf.Clamp(Enemy.GetComponent<EnemyPatrol>().Speed,0,15);
                }
                LineofSight.colorGradient = BlueColor;
            }
        }

    }

    void DestroyGameObject()
    {
        Destroy(this.transform.gameObject);
    }
}
