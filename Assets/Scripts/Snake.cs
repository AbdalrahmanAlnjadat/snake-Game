using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private Vector2 dir = Vector2.right;
    private List<Transform> _segments = new List<Transform>();

    public Transform SegmentPrefabs;
    public int InitialSize = 4;
    public GameManager gameManager;

    private void Start()
    {
        ResetState();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && dir != Vector2.down)
            dir = Vector2.up;
        else if (Input.GetKeyDown(KeyCode.S) && dir != Vector2.up)
            dir = Vector2.down;
        else if (Input.GetKeyDown(KeyCode.D) && dir != Vector2.left)
            dir = Vector2.right;
        else if (Input.GetKeyDown(KeyCode.A) && dir != Vector2.right)
            dir = Vector2.left;
    }

    private void FixedUpdate()
    {
        for (int i = _segments.Count - 1; i > 0; i--)
        {
            _segments[i].position = _segments[i - 1].position;
        }

        this.transform.position = new Vector3(
            Mathf.Round(this.transform.position.x) + dir.x,
            Mathf.Round(this.transform.position.y) + dir.y,
            0f
        );
    }

    private void Grow()
    {
        Transform segment = Instantiate(this.SegmentPrefabs);
        segment.position = _segments[_segments.Count - 1].position;
        _segments.Add(segment);
    }

    private void ResetState()
    {
        for (int i = 1; i < _segments.Count; i++)
        {
            Destroy(_segments[i].gameObject);
        }

        _segments.Clear();
        _segments.Add(this.transform);

        for (int i = 1; i < InitialSize; i++)
        {
            _segments.Add(Instantiate(this.SegmentPrefabs));
        }

        this.transform.position = Vector3.zero;
        gameManager.ResetScore();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Food"))
        {
            Grow();
            gameManager.ScoreIncrease();
        }
        else if (collision.gameObject.CompareTag("Obsticals"))
        {
            gameManager.PauseGame(true);
            ResetState();
        }
    }
}