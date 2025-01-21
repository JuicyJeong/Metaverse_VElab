using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class get_angle : MonoBehaviour
{

    GameObject pelvis;
    GameObject foot_right;
    GameObject foot_left;

    Vector3 pelvis_pos = new Vector3(0.0f,0.0f,0.0f);
    Vector3 foot_right_pos = new Vector3(0.0f, 0.0f, 0.0f);
    Vector3 foot_left_pos = new Vector3(0.0f, 0.0f, 0.0f);
    Vector3 avg_foot_pos = new Vector3(0.0f, 0.0f, 0.0f);
    Vector3 Pel2Ground = new Vector3(0.0f, 0.0f, 0.0f);


    float timer;
    float waitingTime;
    // Start is called before the first frame update
    void Start()
    {
        pelvis = GameObject.Find("pelvis");
        if (pelvis == null)
        {
            Debug.LogWarning("Pelvis object not found!");
        }

        pelvis_pos = pelvis.transform.position;
        Debug.LogFormat("pelvis_pos:{0}",pelvis_pos);

        foot_right = GameObject.Find("right_foot");
        if (foot_right == null)
        {
            Debug.LogWarning("Right Foot object not found!");
        }
        foot_right_pos = foot_right.transform.position;
        Debug.LogFormat("foot_right_pos:{0}", foot_right_pos);


        foot_left = GameObject.Find("left_foot");
        if (foot_left == null)
        {
            Debug.LogWarning("Left Foot object not found!");
        }
        foot_left_pos = foot_left.transform.position;
        Debug.LogFormat("foot_left_pos:{0}", foot_left_pos);

        timer = 0.0f;
        waitingTime = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {

        timer += Time.deltaTime;
        //Debug.LogFormat("{0}", timer);
        if (timer > waitingTime)
        {
            Get_object_position(false);
            float cossim = Get_Cos_sim(pelvis_pos, avg_foot_pos, Pel2Ground);
            Debug.LogFormat("cossim: {0}", cossim);
            timer = 0;
        }





    }

    void Get_object_position(bool printlog) 
    {
        pelvis_pos = pelvis.transform.position;
        foot_right_pos = foot_right.transform.position;
        foot_left_pos = foot_left.transform.position;
        avg_foot_pos = (foot_right_pos + foot_left_pos) / 2;
        Pel2Ground = pelvis_pos;
        Pel2Ground.y = avg_foot_pos.y;

        if (printlog) 
        {
            Debug.LogFormat("pelvis_pos:{0}", pelvis_pos);
            //Debug.LogFormat("foot_right_pos:{0}", foot_right_pos);
            //Debug.LogFormat("foot_left_pos:{0}", foot_left_pos);
            Debug.LogFormat("avg_foot_pos:{0}", avg_foot_pos);
            Debug.LogFormat("Pel2Ground:{0}", Pel2Ground);


        }
    }
    float Get_Cos_sim(Vector3 pelvis, Vector3 avg_foot, Vector3 pel2ground) 
    {

        // 골반이랑 두 발의 평균 값 도출 낸거의 두 포지션을 이용해서 3차원 벡터를 만듦
        // 골반에서 딱 발들의 평균의 y축값을 기준으로 포인트를 찍기
        // 벡터 생성
        Vector3 foot2pel = avg_foot - pelvis;
        Vector3 pelvis2ground = pel2ground-pelvis;
        float dotProduct = Vector3.Dot(foot2pel, pelvis2ground); // 내적
        float magnitudeA = foot2pel.magnitude;           // 벡터 A 크기
        float magnitudeB = pelvis2ground.magnitude;           // 벡터 B 크기
        // 크기가 0인 경우 처리
        if (magnitudeA == 0 || magnitudeB == 0)
        {
            Debug.LogWarning("One of the vectors has zero magnitude!");
            return 0;
        }

        return dotProduct / (magnitudeA * magnitudeB);

    }
}
