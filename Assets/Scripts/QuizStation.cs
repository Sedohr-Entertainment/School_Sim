using UnityEngine;

public class QuizStation : MonoBehaviour
{
    public QuizManager quizManager;
    public BaseQuestion currentQuestion;

    private void OnTriggerEnter(Collider other)
    {
        // If you’re submitting via number objects, convert to string
        NumberObject number = other.GetComponent<NumberObject>();
        if (number != null && currentQuestion != null)
        {
            if (number.hasBeenSubmitted) return;
            number.hasBeenSubmitted = true;

            quizManager.SubmitAnswer(number.value.ToString());
            number.transform.position = new Vector3(0, -100, 0);
            number.gameObject.SetActive(false);
            Destroy(number.gameObject, 0.5f);
        }
    }

    public void SetCurrentQuestion(BaseQuestion question)
    {
        currentQuestion = question;
        Debug.Log("QuizStation set new question.");
    }

}