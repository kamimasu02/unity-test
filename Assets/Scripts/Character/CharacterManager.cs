using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [SerializeField]
    Background background;
    [SerializeField]
    Character selectedCharacter;
    [SerializeField]
    Character[] characters;
    float sceneWidth;
    float sceneHalfWidth;

    void Awake() {

    }

    void Start()
    {
        sceneWidth = background.sceneWidth;
        sceneHalfWidth = sceneWidth / 2;
        selectedCharacter.SetSceneWidth(sceneWidth);
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            HandlePressKey();
        }
    }

    void HandlePressKey()
    {
        if (selectedCharacter.GetActive()) {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (selectedCharacter.transform.position.x + sceneWidth < 0.01)
                {
                    selectedCharacter.SetDestination(-sceneHalfWidth);
                }
                else if (selectedCharacter.transform.position.x + sceneHalfWidth < 0.01)
                {
                    selectedCharacter.SetDestination(0);
                }
                else if (selectedCharacter.transform.position.x - 0 < 0.01)
                {
                    selectedCharacter.SetDestination(sceneHalfWidth);
                }
                else
                {
                    selectedCharacter.SetDestination(selectedCharacter.spriteHalfWidth + sceneWidth * 2);
                }
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (selectedCharacter.transform.position.x + sceneHalfWidth < 0.01)
                {
                    selectedCharacter.SetDestination(-(selectedCharacter.spriteHalfWidth + sceneHalfWidth * 2));
                }
                else if (selectedCharacter.transform.position.x - 0 < 0.01)
                {
                    selectedCharacter.SetDestination(-sceneHalfWidth);
                }
                else if (selectedCharacter.transform.position.x - sceneHalfWidth < 0.01)
                {
                    selectedCharacter.SetDestination(0);
                }
                else
                {
                    selectedCharacter.SetDestination(sceneHalfWidth);
                }
            }
            else if (Input.GetKeyDown("space"))
            {
                selectedCharacter.ResetPosition(Character.MoveDirection.left);
                selectedCharacter.SetActive(false);
            }
        }
        else {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                selectedCharacter.SetActive(true);
                selectedCharacter.ResetPosition(Character.MoveDirection.left);
                selectedCharacter.SetDestination(-sceneHalfWidth);
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                selectedCharacter.SetActive(true);
                selectedCharacter.ResetPosition(Character.MoveDirection.right);
                selectedCharacter.SetDestination(sceneHalfWidth);
            }
        }
    }
}
