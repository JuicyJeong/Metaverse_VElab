using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;

public class kiosk : MonoBehaviour
{
    Dictionary<int, string> oneD_Category = new Dictionary<int, string>
    {{0, "Ŀ��"},{1, "��ī����"},{2, "��"},{3, "������"},{4, "����Ʈ"}};
    ////////////////////////////////////////////////////////////
    Dictionary<int, string> twoD_coffee = new  Dictionary<int, string>
    {{0, "�Ƹ޸�ī��"},{1, "ī���"},{2, "�ٴҶ��"},{3, "ī��Ḷ���ƶ�"},{4, "�ݵ���"}};
    Dictionary<int, string> twoD_decaf = new Dictionary<int, string>
    {{0, "��ī����_�Ƹ޸�ī��"},{1, "��ī����_ī���"},{2, "��ī����_�ٴҶ��"},{3, "��ī����_ī��Ḷ���ƶ�"},{4, "��ī����_�ݵ���"}};
    Dictionary<int, string> twoD_tea = new Dictionary<int, string>
    {{0, "��׷���Ƽ"},{1, "���̺���Ƽ"},{2, "�𽺹�Ƽ"},{3, "ĳ����"},{4, "����Ŀ��"}};
    Dictionary<int, string> twoD_smoothy = new Dictionary<int, string>
    {{0, "�÷��ο��Ʈ_������"},{1, "����_������"},{2, "������Ʈ_������"},{3, "��纣��_������"},{4, "�ٴҶ�_������"}};
    Dictionary<int, string> twoD_dessert = new Dictionary<int, string>
    {{0, "ġ������ũ"},{1, "Ƽ��̼�"},{2, "��ī��"},{3, "��Ű"},{4, "�������"}};
    /// /////////////////////////////////////////////////////////

    Dictionary<int, string> threeD_pay = new Dictionary<int, string>
    {{0, "�ſ�ī��"},{1, "���̺�����"},{2, "ļļ������"},{3, "�������"}};

    Dictionary<string, int> Total_Menu_price = new Dictionary<string, int>
    {
        {"�Ƹ޸�ī��", 3000},{"ī���", 3500},{"�ٴҶ��", 4000},{"ī��Ḷ���ƶ�",4500},{"�ݵ���",4000},
        {"��ī����_�Ƹ޸�ī��", 3300},{"��ī����_ī���", 3800},{"��ī����_�ٴҶ��", 4300},{"��ī����_ī��Ḷ���ƶ�",4800},{"��ī����_�ݵ���",4300},
        {"��׷���Ƽ", 2800},{"���̺���Ƽ", 2800},{"�𽺹�Ƽ", 2800},{"ĳ����",2800},{"����Ŀ��",2800},
        {"�÷��ο��Ʈ_������", 4500},{"����_������", 4500},{"������Ʈ_������", 2800},{"��纣��_������",2800},{"�ٴҶ�_������",2800},
        {"ġ������ũ", 4500},{"Ƽ��̼�", 5000},{"��ī��", 3000},{"��Ű",2500},{"�������",3000}
    };


    int MenuIndex = 0;
    int change_counter = 0;

    public string gesture_direction = "";
    string selected_menu = "";
    string current_menu;
    bool is_step0, is_step1, is_step2, is_step3, is_step4 = false;
    bool is_duplicate = false;
    bool is_ready;
    Renderer kioskIMG;
    Material start;
    Material C1, C2, C3, C4, C5;
    //Material C1_1, C1_2, C1_3, C1_4, C1_5;
    //Material C2_1, C2_2, C2_3, C2_4, C2_5;
    //Material C3_1, C3_2, C3_3, C3_4, C3_5;
    //Material C4_1, C4_2, C4_3, C4_4, C4_5;
    //Material C5_1, C5_2, C5_3, C5_4, C5_5;
    //for test.


    void step1_2_SELECT_CATEGORY(string direction, Dictionary<int, string> category_OR_menu, int step_num) 
    {
        Material[] MenuMaterial = new Material[] { C1, C2, C3, C4, C5 };
        kioskIMG.material = MenuMaterial[MenuIndex];
        if (!is_duplicate) 
        {
            if (direction == "Up")
            {
                Debug.Log("����. �ش� �޴��� �̵��մϴ�");
                change_counter = 0;
                selected_menu = current_menu;
                MenuIndex = 0;
                if (step_num == 1) { is_step1 = false; is_step2 = true; }
                if (step_num == 2) { is_step2 = false; is_step3 = true; }

                is_duplicate = true;
            }
            if (direction == "Down")
            {
                Debug.Log("���. ���� �޴��� �̵��մϴ�.");
                change_counter = 0;
                MenuIndex = 0;
                if (step_num == 1) { is_step1 = false; is_step0 = true; }
                if (step_num == 2) { is_step2 = false; is_step1 = true; }
                is_duplicate = true;
            }
            if (direction == "Left")
            {
                Debug.Log("�����޴�");
                MenuIndex++;
                change_counter = 0;
                if (MenuIndex > 4) MenuIndex = 0;
                is_duplicate = true;
            }
            if (direction == "Right")
            {
                Debug.Log("�����޴�");
                MenuIndex--;
                change_counter = 0;
                if (MenuIndex < 0) MenuIndex = 4;
                is_duplicate = true;
            }
        }
        //////////////////////////////////////////////////////////////////////////////////////////
        if (direction == "Ready") is_duplicate = false;
        if (change_counter == 0 && direction != "Down" && direction != "Up")
        {
            current_menu = category_OR_menu[MenuIndex];
            Debug.LogFormat("���� ���õ� �޴��� [{0}]�Դϴ�.", current_menu);
            change_counter++;
        }
        
    }
    // Start is called before the first frame update
    void Start()
    {

        C1 = Resources.Load<Material>("1_Coffee");
        C2 = Resources.Load<Material>("2_Decaf");
        C3 = Resources.Load<Material>("3_Tea");
        C4 = Resources.Load<Material>("4_Smoothy");
        C5 = Resources.Load<Material>("5_Dessert");

        start = Resources.Load<Material>("Materials/KioskMenuMaterial/Ready");
        //Debug.Log("�������. ���ϴ� ī�װ��� �������ּ���");
        is_step0 = true;

    }

    // Update is called once per frame
    void Update()
    {
        kioskIMG = GameObject.Find("Screen").GetComponent<MeshRenderer>();
        gesture_direction = GameObject.Find("Xsens").GetComponent<motion_gesture>().direction;
        is_ready = GameObject.Find("Xsens").GetComponent<motion_gesture>().is_ready_to_order;

        if (is_step0) 
        {
            //Debug.Log("Ű����ũ�� �����Դϴ�. �������!. �����Ͻ÷��� �¿�� ���� �����ּ���.");
            kioskIMG.material = start;

            if (is_ready) 
            {
                is_step0 = false;
                is_step1 = true;
            }

        }
        if (is_step1) 
        {
            C1 = Resources.Load<Material>("Materials/KioskMenuMaterial/1_Category/1_Coffee");
            C2 = Resources.Load<Material>("Materials/KioskMenuMaterial/1_Category/2_Decaf");
            C3 = Resources.Load<Material>("Materials/KioskMenuMaterial/1_Category/3_Tea");
            C4 = Resources.Load<Material>("Materials/KioskMenuMaterial/1_Category/4_Smoothy");
            C5 = Resources.Load<Material>("Materials/KioskMenuMaterial/1_Category/5_Dessert");
            step1_2_SELECT_CATEGORY(gesture_direction, oneD_Category, 1);
        }
        if (is_step2) 
        {
            if (selected_menu == "Ŀ��")
            {
                C1 = Resources.Load<Material>("Materials/KioskMenuMaterial/2_Coffee/Coffee_1");C2 = Resources.Load<Material>("Materials/KioskMenuMaterial/2_Coffee/Coffee_2");
                C3 = Resources.Load<Material>("Materials/KioskMenuMaterial/2_Coffee/Coffee_3");C4 = Resources.Load<Material>("Materials/KioskMenuMaterial/2_Coffee/Coffee_4");
                C5 = Resources.Load<Material>("Materials/KioskMenuMaterial/2_Coffee/Coffee_5");
                step1_2_SELECT_CATEGORY(gesture_direction, twoD_coffee, 2);
            }
            if (selected_menu == "��ī����")
            {
                C1 = Resources.Load<Material>("Materials/KioskMenuMaterial/3_Decaf/Decaf_1"); C2 = Resources.Load<Material>("Materials/KioskMenuMaterial/3_Decaf/Decaf_2");
                C3 = Resources.Load<Material>("Materials/KioskMenuMaterial/3_Decaf/Decaf_3"); C4 = Resources.Load<Material>("Materials/KioskMenuMaterial/3_Decaf/Decaf_4");
                C5 = Resources.Load<Material>("Materials/KioskMenuMaterial/3_Decaf/Decaf_5");
                step1_2_SELECT_CATEGORY(gesture_direction, twoD_decaf, 2); 
            }

            if (selected_menu == "��") 
            {
                C1 = Resources.Load<Material>("Materials/KioskMenuMaterial/4_Tea/Tea_1"); C2 = Resources.Load<Material>("Materials/KioskMenuMaterial/4_Tea/Tea_2");
                C3 = Resources.Load<Material>("Materials/KioskMenuMaterial/4_Tea/Tea_3"); C4 = Resources.Load<Material>("Materials/KioskMenuMaterial/4_Tea/Tea_4");
                C5 = Resources.Load<Material>("Materials/KioskMenuMaterial/4_Tea/Tea_5");
                step1_2_SELECT_CATEGORY(gesture_direction, twoD_tea, 2); 
            }
            if (selected_menu == "������") 
            {
                C1 = Resources.Load<Material>("Materials/KioskMenuMaterial/5_Smoothy/Smoothy_1"); C2 = Resources.Load<Material>("Materials/KioskMenuMaterial/5_Smoothy/Smoothy_2");
                C3 = Resources.Load<Material>("Materials/KioskMenuMaterial/5_Smoothy/Smoothy_3"); C4 = Resources.Load<Material>("Materials/KioskMenuMaterial/5_Smoothy/Smoothy_4");
                C5 = Resources.Load<Material>("Materials/KioskMenuMaterial/5_Smoothy/Smoothy_5");
                step1_2_SELECT_CATEGORY(gesture_direction, twoD_smoothy, 2); 
            }
            if (selected_menu == "����Ʈ") 
            {
                C1 = Resources.Load<Material>("Materials/KioskMenuMaterial/6_Dessert/Dessert_1"); C2 = Resources.Load<Material>("Materials/KioskMenuMaterial/6_Dessert/Dessert_2");
                C3 = Resources.Load<Material>("Materials/KioskMenuMaterial/6_Dessert/Dessert_3"); C4 = Resources.Load<Material>("Materials/KioskMenuMaterial/6_Dessert/Dessert_4");
                C5 = Resources.Load<Material>("Materials/KioskMenuMaterial/6_Dessert/Dessert_5");
                step1_2_SELECT_CATEGORY(gesture_direction, twoD_dessert, 2); 
            }
        }
        if (is_step3) 
        {
            //�� OR ���̽�? 
            //�޴��� �߰� �ֹ��Ͻðڽ��ϱ�?(YES: ī�װ��� �̵�, NO: ����â���� �̵�)

            //Debug.LogFormat("������ �޴��� ������[{}���Դϴ�.]", Total_Menu_price[selected_menu]);
            //Debug.LogFormat("������ �޴��� [{}]�Դϴ�.]", selected_menu);
            
            Debug.Log("���� �غ���...");
            selected_menu = "Ŀ��";

            is_step2 = true;
            is_step3 = false;
        }
        if (is_step4) 
        {
            //���� ������ �������ּ���...
        }

    }
}
