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
        // 각 발의 접촉 비율 계산
        leftContactRatio = CalculateFootContact(leftFoot);
        rightContactRatio = CalculateFootContact(rightFoot);

        // 전체 접촉 비율 계산
        float totalContactRatio = (leftContactRatio + rightContactRatio) / 2.0f;
        Debug.Log("Total Contact Ratio: " + totalContactRatio);
    }

    float CalculateFootContact(Transform foot)
    {
        // 발의 Collider 가져오기
        Collider footCollider = foot.GetComponent<Collider>();
        if (footCollider == null) return 0.0f;

        // 발 아래로 Raycast
        RaycastHit hit;
        bool isGrounded = Physics.Raycast(foot.position, Vector3.down, out hit, 0.1f, groundLayer);

        if (isGrounded)
        {
            // 접촉한 영역 계산
            Vector3 contactPoint = hit.point;
            Bounds bounds = footCollider.bounds;

            // 발의 바닥 면적 계산 (xz 평면)
            float footArea = bounds.size.x * bounds.size.z;

            // 접촉 면적 비율 계산 (대략적으로 1로 설정)
            return Mathf.Clamp01(1.0f); // 단순화된 계산
        }

        // 접촉하지 않음
        return 0.0f;
    }
}
