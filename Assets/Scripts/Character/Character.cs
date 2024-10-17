using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public enum MoveDirection {
        left = 0,
        right = 1
    }

    [SerializeField]
    Animator animator;

    [SerializeField]
    SpriteRenderer spriteRenderer;
    [SerializeField]
    Sprite[] spriteArray;

    [SerializeField]
    float scale;
    [SerializeField]
    float posY;
    [SerializeField]
    float moveSpeed = 10.0f;

    Vector3 destination;

    bool blockUpdate = false;

    float sceneWidth;

    public float spriteHalfWidth;

    void Start()
    {
        animator.enabled = false;
        transform.localScale *= scale;
        transform.position = new Vector3(transform.position.x, posY, transform.position.z);
        spriteHalfWidth = spriteRenderer.sprite.bounds.size.x * scale / 2;
        gameObject.SetActive(false);
    }

    void OnEnable()
    {
        blockUpdate = false;
    }

    void Update()
    {
        if (Input.anyKeyDown) ChangeFace();
        if (transform.position.x != destination.x) MoveCharacter();
    }

    void OnDisable()
    {
        blockUpdate = true;
    }

    void SetPosition(float posX)
    {
        transform.position = new Vector3(posX, transform.position.y, transform.position.z);
    }

    public bool GetActive()
    {
        return gameObject.activeSelf;
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

    public void ResetPosition(MoveDirection direction)
    {
        switch (direction)
        {
            case MoveDirection.left:
                SetPosition(-(spriteHalfWidth + sceneWidth));
                break;
            case MoveDirection.right:
              SetPosition(spriteHalfWidth + sceneWidth);
                break;
        }
    }

    public void SetDestination(float toX)
    {
        destination = new Vector3(toX, transform.position.y, transform.position.z);
    }

    public void SetSceneWidth(float width)
    {
        sceneWidth = width;
    }

    void MoveCharacter()
    {
        if (!blockUpdate) blockUpdate = true;

        Vector3 direction = (destination - transform.position);
        direction.Normalize();
        Vector3 movement = direction * moveSpeed * Time.deltaTime;

        if (Vector3.Distance(transform.position, destination) < 0.01)
        {
            SetPosition(destination.x);
            blockUpdate = false;
        }
        else
        {
            SetPosition((transform.position + movement).x);

            if (transform.position.x < -(spriteHalfWidth + sceneWidth))
            {
                ResetPosition(MoveDirection.left);
                gameObject.SetActive(false);
                blockUpdate = false;
            }
            else if (spriteHalfWidth + sceneWidth < transform.position.x)
            {
                ResetPosition(MoveDirection.right);
                gameObject.SetActive(false);
                blockUpdate = false;
            }
        }
    }

    void ChangeFace()
    {
        if (gameObject.activeSelf && !blockUpdate)
        {
            string inputKeycode = Input.inputString;

            if (!string.IsNullOrEmpty(inputKeycode))
            {
                if (int.TryParse(inputKeycode, out int spriteIndex))
                {
                    spriteIndex--;

                    if (0 <= spriteIndex && spriteIndex < spriteArray.Length)
                    {
                        if (animator.enabled) animator.enabled = false;
                        spriteRenderer.sprite = spriteArray[spriteIndex];
                    }
                    else if (spriteIndex == -1)
                    {
                        spriteRenderer.sprite = spriteArray[0];

                        if (!animator.enabled)
                        {
                            animator.enabled = true;
                            animator.Play("ChangeFace");
                        }
                        else animator.enabled = false;
                    }
                }
            }
        }
    }
}
