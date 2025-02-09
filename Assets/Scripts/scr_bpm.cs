using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions;

public class scr_bpm : MonoBehaviour
{
    public int bpm = 60; // Beats per minute
    public float amplitude = 50f; // Height of the spike
    public float speed = 200f; // Speed of wave movement
    public int resolution = 300; // Number of points in the wave
    public float flatlineDuration = 0.5f; // Proportion of time spent flatline between beats

    private UILineRenderer lineRenderer;
    private float timer = 0f;
    private List<Vector2> points = new List<Vector2>();
    private RectTransform rectTransform;

    void Start()
    {
        lineRenderer = GetComponent<UILineRenderer>();
        rectTransform = GetComponent<RectTransform>();

        if (lineRenderer == null)
        {
            Debug.LogError("UILineRenderer component is required!");
            return;
        }

        lineRenderer.Points = new Vector2[0];
        lineRenderer.LineThickness = 2f;
    }

    void Update()
    {
        float deltaTime = Time.deltaTime;

        // Update timer based on BPM
        timer += deltaTime * (bpm / 60f);

        // Generate the cardiogram wave
        GenerateCardiogram();

        // Render the wave
        lineRenderer.Points = points.ToArray();
    }

    private void GenerateCardiogram()
    {
        points.Clear();

        // Get the width and height of the rect
        float width = rectTransform.rect.width;
        float height = rectTransform.rect.height / 2f;

        // Calculate time per beat and time per flatline
        float timePerBeat = 1f / (bpm / 60f);
        float flatlineTime = timePerBeat * flatlineDuration;

        float xStep = width / resolution;
        float time = timer % timePerBeat;

        for (float x = 0; x < width; x += xStep)
        {
            float y = height;

            // Create the cardiogram shape
            if (time < flatlineTime) // Flatline
            {
                y = height;
            }
            else if (time < flatlineTime + 0.1f) // Sharp spike
            {
                float spikeProgress = (time - flatlineTime) / 0.1f;
                y = height + amplitude * Mathf.Sin(spikeProgress * Mathf.PI);
            }
            else if (time < flatlineTime + 0.3f) // Subtle downward wave
            {
                float waveProgress = (time - flatlineTime - 0.1f) / 0.2f;
                y = height - amplitude * 0.5f * Mathf.Sin(waveProgress * Mathf.PI);
            }

            points.Add(new Vector2(x, y));

            // Move time forward based on x step
            time += xStep / speed;
            if (time > timePerBeat)
            {
                time -= timePerBeat;
            }
        }
    }
}
