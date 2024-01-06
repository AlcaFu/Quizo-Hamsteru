using System.Collections;
using UnityEngine;

public class Mole : MonoBehaviour
{
    private Vector2 startPosition = new Vector2(-150f, -200f);
    private Vector2 endPosition = new Vector2(-150f, -385f);

    private float showDuration = 1f;
    private float hideDuration = 1f;
    public float stayDuration = 1f; 
    public float delayBetweenCycles = 2f; 

    private IEnumerator ShowHide(Vector2 start, Vector2 end)
    {
        while (true) // Infinite loop
        {
            // Show
            transform.localPosition = start;

            float elapsed = 0f;
            while (elapsed < showDuration)
            {
                transform.localPosition = Vector2.Lerp(start, end, elapsed / showDuration);
                elapsed += Time.deltaTime;
                yield return null;
            }

            transform.localPosition = end;

            yield return new WaitForSeconds(stayDuration); 

            // Hide
            elapsed = 0f;
            while (elapsed < hideDuration)
            {
                transform.localPosition = Vector2.Lerp(end, start, elapsed / hideDuration);
                elapsed += Time.deltaTime;
                yield return null;
            }

            transform.localPosition = start;

            yield return new WaitForSeconds(delayBetweenCycles); 

            yield return null; 
        }
    }

    private void Start()
    {
        StartCoroutine(ShowHide(startPosition, endPosition));
    }
}
