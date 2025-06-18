using UnityEngine;
using UnityEngine.AI;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
public class CrewShipAI : MonoBehaviour
{
    private NavMeshAgent agent;

    [Header("Avoidance Settings")]
    [Tooltip("Agent’in adalardan ve diðer engellerden kaçýnmak için kullandýðý yarýçap")]
    [SerializeField] private float avoidanceRadius = 20f;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

        // Temel agent ayarlarý
        agent.updatePosition = true;
        agent.updateRotation = true;
        agent.autoBraking = true;
        agent.autoTraverseOffMeshLink = false;

        // Engellerden kaçýnma
        agent.obstacleAvoidanceType = ObstacleAvoidanceType.HighQualityObstacleAvoidance;
        agent.radius = avoidanceRadius;
        agent.avoidancePriority = 30;

        // NavMesh'e yerleþtirme
        PlaceOnNavMesh();
    }

    private void PlaceOnNavMesh()
    {
        NavMeshHit hit;
        float maxSampleDistance = 10f; // 2f yerine 10f
        if (NavMesh.SamplePosition(transform.position, out hit, maxSampleDistance, NavMesh.AllAreas))
        {
            agent.Warp(hit.position);
            Debug.Log($"[CrewShipAI] Warped to NavMesh at {hit.position}", this);
        }
        else
        {
            Debug.LogError($"[CrewShipAI] NavMesh.SamplePosition FAILED at {transform.position}", this);
        }
    }

    /// <summary>
    /// Hýzý ve diðer parametreleri ayarlar.
    /// </summary>
    public void Init(float speed)
    {
        agent.speed = 2.5f * speed;
        agent.angularSpeed = 120f;
        agent.acceleration = speed * 2f;
        agent.baseOffset = -1f;
    }

    /// <summary>
    /// Belirtilen konuma gider ve stopDistance mesafesine yaklaþýnca tamamlanýr.
    /// </summary>
    public IEnumerator MoveTo(Vector3 destination, float stopDistance = 0.5f)
    {
        // Eðer daha önce warp baþarýsýz olduysa yeniden dene
        if (!agent.isOnNavMesh)
            PlaceOnNavMesh();

        if (!agent.isOnNavMesh)
        {
            Debug.LogError($"[CrewShipAI] Agent hâlâ NavMesh üzerinde deðil, MoveTo iptal edildi.", this);
            yield break;
        }

        agent.SetDestination(destination);

        // Path hesaplanana kadar
        while (agent.pathPending)
            yield return null;

        // Hedefe yaklaþana kadar
        while (agent.remainingDistance > stopDistance)
            yield return null;
    }

    private void OnDrawGizmosSelected()
    {
        if (agent != null && agent.hasPath)
        {
            Gizmos.color = Color.green;
            var corners = agent.path.corners;
            for (int i = 0; i < corners.Length - 1; i++)
                Gizmos.DrawLine(corners[i], corners[i + 1]);
        }
    }
}
