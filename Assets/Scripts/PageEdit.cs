using System;
using ProtoBuf;

[ProtoContract]
[Serializable]
public class PageEdit
{
	[ProtoMember(1)]
	public string username;
	[ProtoMember(2)]
	public string pagetitle;
	[ProtoMember(3)]
	public string revid;
	[ProtoMember(4)]
	public string revtime;
	[ProtoMember(5)]
	public bool isReverted;
	[ProtoMember(6)]
	public string revertTime;
	[ProtoMember(7)]
	public int stiki_REP_USER;
	[ProtoMember(8)]
	public int stiki_score;
	[ProtoMember(9)]
	public int cluebotRevert;


	public override string ToString ()
	{
		return string.Format ("[Edit: username={0}, pagetitle={1}, revid={2}, revtime={3}, isReverted={4}, revertTime={5}, stiki_REP_USER={6}, stiki_score={7}, cluebotRevert={8}]", username, pagetitle, revid, revtime, isReverted, revertTime, stiki_REP_USER, stiki_score, cluebotRevert);
	}
	
}

