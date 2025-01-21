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

        // ����̶� �� ���� ��� �� ���� ������ �� �������� �̿��ؼ� 3���� ���͸� ����
        // ��ݿ��� �� �ߵ��� ����� y�ప�� �������� ����Ʈ�� ���
        // ���� ����
        Vector3 foot2pel = avg_foot - pelvis;
        Vector3 pelvis2ground = pel2ground-pelvis;
        float dotProduct = Vector3.Dot(foot2pel, pelvis2ground); // ����
        float magnitudeA = foot2pel.magnitude;           // ���� A ũ��
        float magnitudeB = pelvis2ground.magnitude;           // ���� B ũ��
        // ũ�Ⱑ 0�� ��� ó��
        if (magnitudeA == 0 || magnitudeB == 0)
        {
            Debug.LogWarning("One of the vectors has zero magnitude!");
            return 0;
        }

        return dotProduct / (magnitudeA * magnitudeB);

    }
}
