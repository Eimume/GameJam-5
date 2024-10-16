using System.Collections;
using UnityEngine;

public class sumlong : MonoBehaviour
{
    public Transform[] tiles; 
    public int currentTileIndex = 0; 
    public float moveSpeed = 2f; 

    public bool isMoving = false;

    [SerializeField] Animator animator;


    private void Start()
    {
        //animator = GetComponent<Animator>();
        if (PlayerData.instance != null && PlayerData.instance.lastPosition != Vector3.zero)
        {
            RestorePlayerState(PlayerData.instance.lastPosition, PlayerData.instance.lastTileIndex);
        }
    }

    private void Update()
    {
        // อัปเดตตำแหน่งของผู้เล่นใน PlayerData ทุกเฟรม
        if (PlayerData.instance != null)
        {
            PlayerData.instance.SavePlayerPosition(transform.position, currentTileIndex);
        }
    }

    public void MovePlayer(int steps)
    {
        if (!isMoving)
        {
            StartCoroutine(MovePlayerCoroutine(steps));
        }
    }

    private IEnumerator MovePlayerCoroutine(int steps)
    {
        isMoving = true;

        while (steps > 0)
        {
            if (currentTileIndex + 1 >= tiles.Length)
            {
                // Stop if we've reached the end of the tiles array
                break;
            }

            Vector3 targetPosition = tiles[currentTileIndex + 1].position;
            SetAnimationDirection(targetPosition);

            // Move towards the target tile
            while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
            {
                Vector3 newPosition = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
                SetAnimationDirection(newPosition); // Update direction if needed during movement
                transform.position = newPosition;
                yield return null;
            }

            currentTileIndex++; // Increment the tile index
            steps--;
            yield return new WaitForSeconds(0.2f); // Wait briefly before the next step
        }

        ResetToIdle();
        CheckSpecialTile();

        isMoving = false;
    }

    public void MoveBackward(int steps)
    {
        if (!isMoving)
        {
            StartCoroutine(MoveBackwardCoroutine(steps));
        }
    }

    private IEnumerator MoveBackwardCoroutine(int steps)
    {
        isMoving = true;

        while (steps > 0)
        {
            if (currentTileIndex - 1 < 0)
            {
                // Stop if we've reached the start of the tiles array
                break;
            }

            Vector3 targetPosition = tiles[currentTileIndex - 1].position;

            // Determine the direction for animation
            SetAnimationDirection(targetPosition);

            // Move towards the target tile
            while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
            {
                Vector3 newPosition = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
                SetAnimationDirection(newPosition); // Update direction if needed during movement
                transform.position = newPosition;
                yield return null;
            }

            currentTileIndex--; // Decrement the tile index
            steps--;
            yield return new WaitForSeconds(0.2f); // Wait briefly before the next step
        }

        ResetToIdle();
        CheckSpecialTile();

        isMoving = false;
    }
    private void SetAnimationDirection(Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - transform.position;

        // Reset all movement booleans
        animator.SetBool("isMovingRight", false);
        animator.SetBool("isMovingLeft", false);
        animator.SetBool("isMovingUp", false);
        animator.SetBool("isMovingDown", false);

        // Determine which animation to play based on movement direction
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x > 0) // Moving right
            {
                animator.SetBool("isMovingRight", true);
                //lastDirection = 3; // Right
            }
            else if (direction.x < 0) // Moving left
            {
                animator.SetBool("isMovingLeft", true);
                //lastDirection = 2; // Left
            }
        }
        else
        {
            if (direction.y > 0) // Moving up
            {
                animator.SetBool("isMovingUp", true);
                //lastDirection = 1; // Up
            }
            else if (direction.y < 0) // Moving down
            {
                animator.SetBool("isMovingDown", true);
                //lastDirection = 0; // Down
            }
        }
    }

    private void ResetToIdle()
    {
        // Reset all movement animations and set to idle
        animator.SetBool("isMovingRight", false);
        animator.SetBool("isMovingLeft", false);
        animator.SetBool("isMovingUp", false);
        animator.SetBool("isMovingDown", false);
        animator.SetTrigger("idle"); // Trigger idle animation
    }
    

    private void CheckSpecialTile()
    {
        SpecialTile specialTile = tiles[currentTileIndex].GetComponent<SpecialTile>();
        if (specialTile != null)
        {
            if (specialTile.isMoveBackwardTile)
            {
                //StartCoroutine(MoveBackward(specialTile.moveBackwardSteps));
                MoveBackward(specialTile.moveBackwardSteps);
            }
            else if (specialTile.isDamageTile)
            {
                PlayerData.instance.TakeDamage(specialTile.damageAmount);
            }
            else if (specialTile.isShopTile)
            {
                OpenShop();
            }
            else if (specialTile.isBattleTile && specialTile.IsBattleTileActive())
            {
                PlayerData.instance.SavePlayerPosition(transform.position, currentTileIndex);
                //SceneController.instance.SavePlayerState(transform.position, currentTileIndex);
                specialTile.DeactivateBattleTile();
                SceneController.instance.LoadBattleScene();
            }
        }
    }

    public void RestorePlayerState(Vector3 position, int tileIndex)
    {
        // Restore the player's position and tile index
        transform.position = position;
        currentTileIndex = tileIndex;
    }

    private void OpenShop()
    {
        Debug.Log("Shop opened.");
    }

}
