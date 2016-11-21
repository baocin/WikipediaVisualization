using System;
using ProtoBuf;

[ProtoContract]
[Serializable]
public class User
{
	[ProtoMember(1)]
	public string username;
	[ProtoMember(2)]
	public string id;
	[ProtoMember(3)]
	public string status;
	[ProtoMember(4)]
	public string bantime;
	[ProtoMember(5)]
	public string banreason;

	public override string ToString ()
	{
		return string.Format ("[User: username={0}, id={1}, status={2}, bantime={3}, banreason={4}]", username, id, status, bantime, banreason);
	}
	
	
}