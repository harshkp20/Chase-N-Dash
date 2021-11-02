using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    public float SpeedX,SpeedZ;
    public GameObject Player;
    bool isStopped=false;
    public float timer,Start_Time;
    public int clock=0;
    public GameObject Enemy;
    public TextMeshProUGUI Text;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        timer=Start_Time;       
    }
    // Update is called once per frame
    void Update()
    {
        clock+=(int)(Time.deltaTime*100);
        Text.text = clock.ToString();
        float X_Position,Z_Position;
        Vector3 Instantiated_Position;

        Z_Position = Random.Range(-3.5f,3.5f);
        X_Position = Random.Range(Player.transform.position.x + 26.5f, Player.transform.position.x+31.5f);

        Instantiated_Position = new Vector3(X_Position,this.transform.position.y,Z_Position);

        float inputX,inputZ;

        inputZ = Input.GetAxis("Horizontal");
        inputX = Input.GetAxis("Vertical");
        if(isStopped ==false)
        {
            if(timer<=0)
            {
                Instantiate(Enemy,Instantiated_Position,Enemy.transform.rotation);
                if(SpeedX>15)
                {
                    if(Z_Position >0) Z_Position-=3.5f;
                    else Z_Position +=3.5f;
                    Instantiated_Position = new Vector3(X_Position+3,this.transform.position.y,Z_Position);
                    GameObject Instantiated =  Instantiate(Enemy,Instantiated_Position,Enemy.transform.rotation);
                    Instantiated.transform.Rotate(0,45f,0);
                    Instantiated.GetComponent<StealthEnemy>().RotationSpeed+=1;
                } 
                timer = Start_Time;
                SpeedX+=2;
            }
            else
            {
                timer-=Time.deltaTime;
            }
        }
        if(inputX>=0)
        {
            transform.Translate(SpeedX*Time.deltaTime,0,0);
            isStopped=false;
            if(inputX>0) SpeedX+=1;
        }
        else
        {
            Player.GetComponent<Rigidbody>().velocity = Vector3.zero;
            SpeedX =5;
            isStopped=true;
        }
        if(inputZ !=0)
        {
            transform.Translate(0,0,-inputZ*SpeedZ*Time.deltaTime);
            isStopped=false;
        }
        if(SpeedX>30) SpeedX =30f;
        inputX=0;
        inputZ=0;
    }
}
