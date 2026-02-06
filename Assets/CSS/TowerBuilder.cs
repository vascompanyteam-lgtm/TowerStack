using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBuilder : MonoBehaviour
{
    public GameObject p;
    public GameObject p2;
    public FInishMan finish;
    public GameObject blockPrefab;
    public float moveSpeed = 5f;
    public float moveRange = 5f;
    public float returnSpeed = 2f;
    public float cameraMoveSpeed = 2f;
    public float followDistance = 2f;
    public float towerBuilderRiseStep = 1f;
    public Sprite[] sprites;
    public SpriteRenderer blockSprite;
    public int currentBlockInt;

    private GameObject currentBlock;
    private List<GameObject> spawnedBlocks = new List<GameObject>();
    private bool isMoving = false;
    private float startX;
    private Vector3 startPosition;
    private int blocknum;
    public ScoringOverseer sc;
    private void OnEnable()
    {
        sc.UpdateAllVisuals();
    }
    private void Start()
    {
        currentBlockInt = UnityEngine.Random.Range(0, sprites.Length);
        blockSprite.sprite = sprites[currentBlockInt];
        startPosition = transform.position;
        blocknum = 0;
    }

    private void Update()
    {
        // разрешаем клики только в нижних 80% экрана
        bool isClickAllowed = Input.mousePosition.y <= Screen.height * 0.8f;

        if (currentBlock == null)
        {
            if (Input.GetMouseButtonDown(0)
                && isClickAllowed
                && !p.activeInHierarchy
                && !p2.activeInHierarchy)
            {
                finish.pl.pl.Play();

                if (!isMoving)
                    StartMoving();
                else
                    StopMoving();
            }
        }

        if (isMoving)
        {
            Move();
        }
    }

    private void StartMoving()
    {
        startX = transform.position.x;
        isMoving = true;
    }

    private void StopMoving()
    {
        isMoving = false;
        PlaceBlock();
        RaiseTowerBuilder();
    }

    private void Move()
    {
        float movement = Mathf.Sin(Time.time * moveSpeed) * moveRange;
        transform.position = new Vector3(startX + movement, transform.position.y, transform.position.z);
    }

    private void PlaceBlock()
    {
        Vector3 spawnPosition = transform.position;
        currentBlock = Instantiate(blockPrefab, spawnPosition, Quaternion.identity);

        spawnedBlocks.Add(currentBlock);

        currentBlock.GetComponentInChildren<SpriteRenderer>().sprite = sprites[currentBlockInt];

        currentBlockInt = UnityEngine.Random.Range(0, sprites.Length);
        blockSprite.sprite = sprites[currentBlockInt];

        Rigidbody rb = currentBlock.AddComponent<Rigidbody>();
        currentBlock.GetComponent<BlockDoor>().rb = rb;
        currentBlock.GetComponent<BlockDoor>().index = blocknum;
        blocknum++;

        rb.mass = 999;
        rb.angularDamping = 0;
        rb.linearDamping = 0;
        rb.constraints = RigidbodyConstraints.FreezeRotationX;
        rb.isKinematic = false;
        rb.useGravity = true;

        StartCoroutine(MoveToCenter());
    }

    private void RaiseTowerBuilder()
    {
        transform.position = new Vector3(
            transform.position.x,
            transform.position.y + towerBuilderRiseStep,
            transform.position.z
        );
    }

    private IEnumerator MoveToCenter()
    {
        Vector3 targetPosition = new Vector3(0, transform.position.y, transform.position.z);

        while (Mathf.Abs(transform.position.x - targetPosition.x) > 0.01f)
        {
            transform.position = new Vector3(
                Mathf.MoveTowards(transform.position.x, targetPosition.x, returnSpeed * Time.deltaTime),
                transform.position.y,
                transform.position.z
            );
            yield return null;
        }

        currentBlock = null;
    }

    public void Restart()
    {
        finish.CloseAll();
        sc.NullifyPoints();
        foreach (GameObject block in spawnedBlocks)
        {
            Destroy(block);
        }

        spawnedBlocks.Clear();
        transform.position = startPosition;
        currentBlock = null;
        isMoving = false;
        blocknum = 0;
    }
}
