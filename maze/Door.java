package maze;

public class Door {

	private Question question;
	private Boolean isLocked;
	
	public Door(Question question, Boolean isLocked)
	{
		this.question = question;
		this.isLocked = isLocked;
	}
}
