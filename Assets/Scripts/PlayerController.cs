using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 0f;
    [SerializeField] float overlapRadius = 0.2f;
    [SerializeField] LayerMask solidObjectsLayer = default;
    [SerializeField] LayerMask interactablesLayer = default;
    [SerializeField] GameObject quitMenu = null;
    

    private Animator animator;
    private bool isMoving;
    private Vector2 input;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(PlayerData.playerCoords.x, PlayerData.playerCoords.y, PlayerData.playerCoords.z);

        animator = GetComponent<Animator>();

        ToggleQuitMenu(false);

        CheckForGameOver();
    }

    void Update()
    {

        if (!isMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            // remove diagonal movement
            if (input.x != 0) { input.y = 0; }

            if (input != Vector2.zero)
            {
                animator.SetFloat("moveX", input.x);
                animator.SetFloat("moveY", input.y);

                var targetPos = transform.position;
                targetPos.x += input.x;
                targetPos.y += input.y;

                if (IsWalkable(targetPos))
                {
                    StartCoroutine(Move(targetPos));
                }
            }
        }

        animator.SetBool("isMoving", isMoving);

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Interact();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            ToggleQuitMenu(true);
        }

    }

    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;

        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = targetPos;

        isMoving = false;
    }

    private bool IsWalkable(Vector3 targetPos)
    {
        if (Physics2D.OverlapCircle(targetPos, overlapRadius, solidObjectsLayer | interactablesLayer) != null)
        {
            return false;
        }

        return true;
    }

    private void Interact()
    {
        var facingDir = new Vector3(animator.GetFloat("moveX"), animator.GetFloat("moveY"));
        var interactPos = transform.position + facingDir;

        var collider = Physics2D.OverlapCircle(interactPos, 0.3f, interactablesLayer);
        if (collider != null)
        {
            collider.GetComponent<Interactables>()?.Interact();
        }
    }

    public void StorePlayerCoords()
    {
        PlayerData.playerCoords = transform.position;
    }

    private void CheckForGameOver()
    {
        if (PlayerData.playerTotalMoney < 10f) { FindObjectOfType<SceneLoader>().LoadLoseScene(); }
    }

    public void ToggleQuitMenu(bool status)
    {
        quitMenu.SetActive(status);
    }

    public void HandleUpdate()
    {

    }
}
