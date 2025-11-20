using UnityEngine;

public class QuizStation : MonoBehaviour
{
    public QuizManager quizManager;
    public MathQuestion currentQuestion;   // station keeps track of the active question

    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is a NumberObject
        NumberObject number = other.GetComponent<NumberObject>();
        if (number != null && currentQuestion != null)
        {
            // Prevent double processing for this object
            if (number.hasBeenSubmitted)
            {
                Debug.Log($"NumberObject {number.name} has already been submitted.");
                return;
            }

            number.hasBeenSubmitted = true;

            // Check if the answer is correct
            bool isCorrect = currentQuestion.GetCorrectAnswer() == number.value;
            Debug.Log(isCorrect
                ? "Correct!"
                : $"Incorrect! The correct answer is: {currentQuestion.GetCorrectAnswer()}");

            // Submit the answer to the QuizManager
            quizManager.SubmitAnswer(number.value);

            // Instead of destroying the object immediately, move it out of the scene
            number.transform.position = new Vector3(0, -100, 0); // Move it far away
            number.gameObject.SetActive(false); // Disable the object

            // Schedule destruction after a delay to ensure proper physics updates
            Destroy(number.gameObject, 0.5f);
        }
        else
        {
            Debug.Log("Collision detected, but no valid NumberObject found.");
        }
    }

    // Called by QuizManager whenever a new question is asked
    public void SetCurrentQuestion(MathQuestion question)
    {
        currentQuestion = question;
        Debug.Log("QuizStation has been reset for the new question.");
    }
}