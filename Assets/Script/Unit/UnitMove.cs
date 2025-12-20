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

    private Vector3 _target;
    public bool move;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        move = false;
    }

    public void SetTarget(Vector3 target)
    {
        _target = ClampToAllowed(target);
        move = true;
    }

    private bool IsReached()
    {
        float dist = Vector3.Distance(transform.position, _target);

        if (dist <= reachThreshold)
        {
            return true;
        }

        return false;
    }

    private void FaceTarget(float deltaTime)
    {
        Vector3 to = _target - transform.position;
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

    // Tick에서 사용해야하나?
    private void MoveTo(float deltaTime)
    {
        if (agent == null)
        {
            return;
        }

        if (IsReached())
        {
            agent.isStopped = true;
            move = false;
            // FaceTarget(deltaTime);
            return;
        }
        else
        {
            Debug.DrawRay(transform.position, _target, Color.red);
            agent.SetDestination(_target);
            agent.isStopped = false;
        }
    }

    public void Stop()
    {
        if (agent != null)
        {
            agent.isStopped = true;
        }
    }

    // 목적지를 NavMesh에 스냅하고, 금지 영역이면 경계선으로 끌어당겨 반환한다.
    public Vector3 ClampToAllowed(Vector3 desired)
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

    public void FixedTick(float deltaTime)
    {
        if (move)
        {
            MoveTo(deltaTime);
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
