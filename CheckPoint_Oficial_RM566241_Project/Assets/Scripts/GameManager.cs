using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;


public class GameManager : MonoBehaviour
{
    [Header("AR Content")]
    public GameObject[] arContentObjects;

    [Header("AR Camera")]
    public Camera arCamera;

    [Header("Chão")]
    public LayerMask groundLayer;

    [Header("Personagem")]
    public NavMeshAgent agent;
    public Animator animator;

    [Header("Moedas")]
    public GameObject[] coins;
    public int maxCoins = 4;

    [Header("Vitória")]
    public ParticleSystem victoryEffect;

    [Header("UI")]
    public TextMeshProUGUI coinsText;

    private int coinCount;
    private bool gameFinished;

    void Start()
    {
        if (arCamera == null)
            arCamera = Camera.main;

        coinCount = 0;
        gameFinished = false;
        UpdateCoinsText();
    }

    void Update()
    {
        if (gameFinished)
            return;

        HandleTouch();
    }

    private void HandleTouch()
    {
        Vector2 screenPos;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase != TouchPhase.Began)
                return;

            if (EventSystem.current != null &&
                EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                return;

            screenPos = touch.position;
        }
#if UNITY_EDITOR
        else if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
                return;

            screenPos = Input.mousePosition;
        }
#endif
        else
        {
            return;
        }

        if (arCamera == null)
            return;

        Ray ray = arCamera.ScreenPointToRay(screenPos);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundLayer))
        {
            MoveCharacterTo(hit.point);
        }
    }

    private void MoveCharacterTo(Vector3 point)
    {
        if (agent == null || !agent.isOnNavMesh)
        {
            Debug.LogWarning("Personagem não está sobre o NavMesh.");
            return;
        }

        agent.isStopped = false;
        agent.SetDestination(point);

        SetAnimation(walking: true);
    }


    public void CollectCoin(GameObject coin)
    {
        if (gameFinished || coin == null)
            return;

        coin.SetActive(false);

        coinCount++;
        UpdateCoinsText();

        if (coinCount >= maxCoins)
        {
            FinishGame();
        }
    }

    private void FinishGame()
    {
        gameFinished = true;

        if (agent != null && agent.isOnNavMesh)
        {
            agent.isStopped = true;
            agent.ResetPath();
        }

        SetAnimation(dancing: true);

        if (victoryEffect != null)
            victoryEffect.gameObject.SetActive(true);

        UpdateCoinsText();
    }

    private void UpdateCoinsText()
    {
        if (coinsText == null)
            return;

        coinsText.text = gameFinished ? "Congratulations!" : $"Total Coins: {coinCount}";
    }

    private void SetAnimation(bool idle = false, bool walking = false, bool dancing = false)
    {
        if (animator == null)
            return;

        animator.SetBool("IsIdle", idle);
        animator.SetBool("IsWalking", walking);
        animator.SetBool("IsDancing", dancing);
    }



    public void OnTargetFound()
    {
        SetObjectsActive(true);
        ResetGame();
    }

    public void OnTargetLost()
    {
        SetObjectsActive(false);
    }

    private void SetObjectsActive(bool active)
    {
        foreach (GameObject obj in arContentObjects)
        {
            if (obj != null)
                obj.SetActive(active);
        }
    }

    private void ResetGame()
    {
        gameFinished = false;
        coinCount = 0;

        foreach (GameObject coin in coins)
        {
            if (coin != null)
                coin.SetActive(true);
        }

        if (victoryEffect != null)
            victoryEffect.gameObject.SetActive(false);

        if (agent != null && agent.isOnNavMesh)
        {
            agent.isStopped = true;
            agent.ResetPath();
        }

        SetAnimation(idle: true);
        UpdateCoinsText();
    }
}