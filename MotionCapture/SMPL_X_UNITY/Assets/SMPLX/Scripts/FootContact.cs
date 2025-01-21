using UnityEngine;
using ClipperLib;

public class FootContact : MonoBehaviour
{
    public Transform leftFoot;
    public Transform rightFoot;
    public LayerMask groundLayer;

    private float leftContactRatio = 0.0f;
    private float rightContactRatio = 0.0f;

    void Update()
    {
        // �� ���� ���� ���� ���
        leftContactRatio = CalculateFootContact(leftFoot);
        rightContactRatio = CalculateFootContact(rightFoot);

        // ��ü ���� ���� ���
        float totalContactRatio = (leftContactRatio + rightContactRatio) / 2.0f;
        Debug.Log("Total Contact Ratio: " + totalContactRatio);
    }

    float CalculateFootContact(Transform foot)
    {
        // ���� Collider ��������
        Collider footCollider = foot.GetComponent<Collider>();
        if (footCollider == null) return 0.0f;

        // �� �Ʒ��� Raycast
        RaycastHit hit;
        bool isGrounded = Physics.Raycast(foot.position, Vector3.down, out hit, 0.1f, groundLayer);

        if (isGrounded)
        {
            // ������ ���� ���
            Vector3 contactPoint = hit.point;
            Bounds bounds = footCollider.bounds;

            // ���� �ٴ� ���� ��� (xz ���)
            float footArea = bounds.size.x * bounds.size.z;

            // ���� ���� ���� ��� (�뷫������ 1�� ����)
            return Mathf.Clamp01(1.0f); // �ܼ�ȭ�� ���
        }

        // �������� ����
        return 0.0f;
    }
}
