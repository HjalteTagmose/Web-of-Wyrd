public class GameController : Singleton<GameController>
{
	private void Start()
	{
		DialogueSystem.Instance.StartDialogue("Intro",2);
	}
}