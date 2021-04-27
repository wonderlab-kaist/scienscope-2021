using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SurfaceExploaration : MonoBehaviour
{
    public float coefficient = 0.05f;

    public Transform display_part;
    public GameObject surface;

    private bool isControllable;

    private Vector3 target_position;

    private float original_scale;
    private float scale = 1.0f;

    private int detachCount = 0;


    void Start()
    {
        this.isControllable = true;
        this.target_position = this.surface.transform.localPosition;
        this.original_scale = surface.transform.parent.localScale.x;
    }

    void Update()
    {
        const float UPDATE_RATIO = 0.4f;
        float MAX_SPEED = 34.2f * coefficient;
        Vector3 movement = (this.target_position - this.surface.transform.localPosition) * UPDATE_RATIO;
        if (movement.magnitude > MAX_SPEED)
        {
            movement = movement.normalized * MAX_SPEED;
        }
        this.surface.transform.localPosition += movement;

        if (!isControllable)
        {
            return;
        }

        if (Application.platform == RuntimePlatform.Android)
        {
            string tmp, delta_position;
            tmp = dataInput.getData();
            if (tmp != null)
            {
                delta_position = tmp.Split(':')[1];

                if (tmp.Split(':')[0] == "M")
                {
                    int dx = -int.Parse(delta_position.Split(',')[1]);
                    int dy = int.Parse(delta_position.Split(',')[0]);
                    this.MovePosition(new Vector3(dx, dy, 0));
                }
                else if (tmp.Split(':')[0] == "D")
                {
                    if (int.Parse(delta_position) >= 65)
                    {
                        detachCount++;
                        if (detachCount > 13)
                        {
                            SceneManager.LoadScene(1);
                        }
                    }
                }
            }

            if (Time.frameCount % 20 == 0 && detachCount > 0)
            {
                detachCount--;
            }
        }
        else
        {
            float moveAmount = Input.GetKey(KeyCode.LeftShift) ? 0.9f : 3.5f;

            if (Input.GetKey(KeyCode.UpArrow))
            {
                this.MovePosition(new Vector3(0, -1, 0) * moveAmount);
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                this.MovePosition(new Vector3(0, 1, 0) * moveAmount);
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                this.MovePosition(new Vector3(1, 0, 0) * moveAmount);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                this.MovePosition(new Vector3(-1, 0, 0) * moveAmount);
            }
        }
    }


    public void SetControllable(bool isControllable)
    {
        this.isControllable = isControllable;
    }

    public void SetPosition(Vector3 position)
    {
        this.surface.transform.localPosition = position;
    }
    public void SetPositionSmooth(Vector3 position)
    {
        this.target_position = position;
    }

    public void MovePosition(Vector3 displacement)
    {
        this.target_position += displacement * coefficient * this.scale;
    }

    public void SetScale(float scale)
    {
        this.scale = scale;
        surface.transform.parent.localScale = new Vector3(1, 1, 1) * original_scale * this.scale;
    }
}
