using System;

[Serializable]
public class User
{
	public string username;
	public string id;
	public string status;
	public string bantime;
	public string banreason;

	public override string ToString ()
	{
		return string.Format ("[User: username={0}, id={1}, status={2}, bantime={3}, banreason={4}]", username, id, status, bantime, banreason);
	}
	
	
}