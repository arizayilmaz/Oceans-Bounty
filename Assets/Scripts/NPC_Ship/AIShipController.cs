using UnityEngine;
using UnityEngine.AI;
using System.Linq;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
public class AIShipController : MonoBehaviour
{
    [Header("Parent Name (e.g. NPC_Ship)")]
    [SerializeField] private string parentName = "NPC_Ship";
    [Header("Stuck Detection")]
    [SerializeField] private float stuckCheckInterval = 3f;
    [SerializeField] private float stuckThresholdDistance = 0.5f;

    private NavMeshAgent agent;
    private Transform[] waypoints;
    private Vector3 lastPosition;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

        // 1) Waypoint'leri topla
        var parentObj = GameObject.Find(parentName);
        if (parentObj == null)
        {
            Debug.LogError($"[{name}] '{parentName}' bulunamadý!");
            return;
        }
        waypoints = parentObj.transform
            .Cast<Transform>()
            .Where(t => t.name.StartsWith("AI_Ship_"))
            .ToArray();

        // 2) NavMesh'e warp et
        NavMeshHit hit;
        if (NavMesh.SamplePosition(transform.position, out hit, 2f, NavMesh.AllAreas))
        {
            agent.Warp(hit.position);
            Debug.Log($"[{name}] Warp successful to {hit.position}");
        }
        else
            Debug.LogWarning($"[{name}] Warp failed: yakýn NavMesh yok.");

        // 3) Avoidance ayarlarý
        agent.obstacleAvoidanceType = ObstacleAvoidanceType.HighQualityObstacleAvoidance;
        agent.avoidancePriority = Random.Range(10, 90);
        agent.autoBraking = false; // sürekli akýcý hareket için
    }

    private void Start()
    {
        Debug.Log($"[{name}] Start() called. isOnNavMesh = {agent.isOnNavMesh}");
        if (agent.isOnNavMesh && waypoints.Length > 0)
        {
            GoToRandomWaypoint();
            lastPosition = transform.position;
            StartCoroutine(StuckChecker());
        }
        else
            Debug.LogError($"[{name}] NavMesh üzerinde deðil ya da waypoint yok, yola çýkmýyor.");
    }

    public void GoToRandomWaypoint()
    {
        if (!agent.isOnNavMesh || waypoints.Length == 0) return;

        var target = waypoints[Random.Range(0, waypoints.Length)];
        Debug.Log($"[{name}] Moving to {target.name} at {target.position}");
        agent.SetDestination(target.position);
    }

    private IEnumerator StuckChecker()
    {
        while (true)
        {
            yield return new WaitForSeconds(stuckCheckInterval);

            // Eðer çok az hareket ettiyse veya path tamamlanmamýþsa yeniden rota belirle
            float dist = Vector3.Distance(transform.position, lastPosition);
            if (dist < stuckThresholdDistance || agent.pathStatus != NavMeshPathStatus.PathComplete)
            {
                Debug.LogWarning($"[{name}] Stuck detected (dist={dist:F2}, pathStatus={agent.pathStatus}), new waypoint.");
                GoToRandomWaypoint();
            }

            lastPosition = transform.position;
        }
    }

    private void Update()
    {
        // Hedefe ulaþtýysa yeni bir hedef seç
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            GoToRandomWaypoint();
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (waypoints == null) return;
        Gizmos.color = Color.yellow;
        foreach (var wp in waypoints)
            Gizmos.DrawSphere(wp.position, 0.5f);
    }

    private void OnDrawGizmos()
    {
        if (agent == null || !agent.hasPath) return;
        Gizmos.color = Color.green;
        var corners = agent.path.corners;
        for (int i = 0; i < corners.Length - 1; i++)
            Gizmos.DrawLine(corners[i], corners[i + 1]);
    }
}
