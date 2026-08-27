using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    [SerializeField] private Transform[] backGrounds;
    [SerializeField] private float rollSpeed;

    private float bgWidth;

    private void Start()
    {
       
        SpriteRenderer sr = backGrounds[0].GetComponent<SpriteRenderer>();
        bgWidth = sr.bounds.size.x;
    }

    private void Update()
    {
        for (int i = 0; i < backGrounds.Length; i++)
        {
            backGrounds[i].position += Vector3.left * rollSpeed * Time.deltaTime;

           
            if (backGrounds[i].position.x < -bgWidth)
            {
               
                float maxX = float.MinValue;
                for (int j = 0; j < backGrounds.Length; j++)
                {
                    if (backGrounds[j].position.x > maxX)
                    {
                        maxX = backGrounds[j].position.x;
                    }
                }
             
                backGrounds[i].position = new Vector3(maxX + bgWidth, backGrounds[i].position.y, backGrounds[i].position.z);
            }
        }
    }
}