using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class UnitMove : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent agent;
    [SerializeField]
    public float speed = 1;
    [SerializeField]
    public float reachThreshold = 0.5f;

    private float faceTurnSpeedDeg = 360.0f;

    private float gravity = -9.81f;
    private float verticalVelocity = 0.0f;

    private bool clicked = false;
    private Vector3 target;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private bool IsReached(Vector3 worldPos)
    {
        float dist = Vector3.Distance(transform.position, worldPos);
        if (dist <= reachThreshold)
        {
            return true;
        }
        return false;
    }

    private void FaceTarget(Vector3 worldPos, float deltaTime)
    {
        Vector3 to = worldPos - transform.position;
        to.y = 0.0f;

        if (to.sqrMagnitude <= 0.0001f)
        {
            return;
        }

        Quaternion targetRot = Quaternion.LookRotation(to, Vector3.up);
        // (faceTurnSpeedDeg(초당 회전 속도(각도), deg/sec) * Mathf.Deg2Rad) * Time.deltaTime(1프레임 동안 회전해야 할 라디안 계산)
        float t = Mathf.Min(1.0f, (faceTurnSpeedDeg * Mathf.Deg2Rad) * deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, t);
    }

    private void MoveTo(Vector3 worldPos, float deltaTime)
    {        if (agent == null)
        {
            return;
        }

        Vector3 clamped = ClampToAllowed(worldPos, transform.position);
        agent.isStopped = false;

        if (IsReached(clamped))
        {
            FaceTarget(clamped, deltaTime);
            return;
        }
        else
        {
            agent.SetDestination(clamped);
        }
    }

    private void Stop()
    {
        if (agent != null)
        {
            agent.isStopped = true;
        }
    }

    public Vector3 ClampToAllowed(Vector3 desired, Vector3 from)
    {
        // 1) NavMesh 스냅
        NavMeshHit hit;
        Vector3 nav = desired;
        // SamplePosition : desired에서 가장 가까운 지점 찾음
        // hit.position : navmesh상의 가장 가까운 실제 위치
        // hit.distance : 기준점과 navmesh 위치 간 거리
        // hit.mask : 해당 navmesh의 area mask 정보
        if (NavMesh.SamplePosition(desired, out hit, 2.0f, NavMesh.AllAreas) == true)
        {
            nav = hit.position;
        }

        return nav;
    }

    public void Tick(float deltaTime)
    {
        if (agent == null)
        {
            Debug.LogWarning("controller null");
        }

        if (Mouse.current.rightButton.isPressed)
        {
            Ray ray = Camera.main.ScreenPointToRay(
                new Vector3(endPos.x, endPos.y, 0));
            RaycastHit hit;

            bool blocked = Physics.Raycast(ray, out hit, 100, mouse.ground);

            if (blocked)
            {
                GameObject go = Instantiate(
                    mouse.right_click_effect, hit.point + (Vector3.up * 0.1f), mouse.right_click_effect.transform.rotation);
            }

            clicked = true;
        }
    }

    public void FixedTick(float deltaTime)
    {
        if (clicked)
        {
            Vector3  = Camera.main.ScreenToWorldPoint(Mouse.current.position.value);
        }
        // if (clicked)
        // {
        //     Vector2 mouse = Camera.main.ScreenToWorldPoint(Mouse.current.position.value);

        //     Vector3 right = transform.right.normalized;
        //     Vector3 forward = transform.forward.normalized;

        //     Vector3 moveDir = (right * mouse.x) + (forward * mouse.y);
        //     moveDir = moveDir * speed;

        //     if (controller.isGrounded && verticalVelocity <= 0)
        //     {
        //         verticalVelocity = -2.0f; // 지면 밀착
        //     }

        //     verticalVelocity += gravity * deltaTime;

        //     Vector3 velocity = new Vector3(moveDir.x, verticalVelocity, moveDir.y);
        //     velocity = velocity * deltaTime;

        //     controller.Move(velocity);

        //     clicked = false;
        // }
    }
}
