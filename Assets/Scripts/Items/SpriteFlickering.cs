using UnityEngine;

public class FlickeringSprite : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    private float minAlpha = 0.1f;
    private float maxAlpha = 1f;
    
    private float flickerRate = 0.5f;
    private float minFlickerRate = 0.05f;
    private float maxFlickerRate = 0.2f;

    // Variables to control the floating effect
    private float floatAmplitude = 0.0005f;
    private float floatFrequency = 0.51f;

    float randomizer = 0f;

    void Start()
    {
        // Get the sprite renderer
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteFlickering: No SpriteRenderer found on this object");
        }

        flickerRate = Random.Range(minFlickerRate, maxFlickerRate);


    }

    void Update()
    {
        // Randomize flicker rate
        if (randomizer == 0f)
        {
            randomizer = Random.Range(0.2f, 1.5f);
        }

        // Get the sprite's current color
        Color spriteColor = spriteRenderer.color;

        // Interpolate alpha value between min and max values
        float noise = Mathf.PerlinNoise(randomizer, Time.time * flickerRate);
         
        //adjust the alpha value linearly
        spriteColor.a = Mathf.Lerp(minAlpha, maxAlpha, noise*1.5f);

        // Apply the new color to the sprite
        spriteRenderer.color = spriteColor;

        //Debug.Log("SpriteFlick: set alpha to be:" + spriteColor.a);


        // Add a tiny floating effect
        Vector3 position = transform.position;
        position.y += floatAmplitude * ( Mathf.PerlinNoise(randomizer, Time.time * floatFrequency) -0.5f);
        transform.position = position;


    }
}
