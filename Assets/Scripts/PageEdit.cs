using System;

[Serializable]
public class PageEdit
{
	public string username;
	public string pagetitle;
	public string revid;
	public string revtime;
	public bool isReverted;
	public string revertTime;
	public int stiki_REP_USER;
	public int stiki_score;
	public int cluebotRevert;


	public override string ToString ()
	{
		return string.Format ("[Edit: username={0}, pagetitle={1}, revid={2}, revtime={3}, isReverted={4}, revertTime={5}, stiki_REP_USER={6}, stiki_score={7}, cluebotRevert={8}]", username, pagetitle, revid, revtime, isReverted, revertTime, stiki_REP_USER, stiki_score, cluebotRevert);
	}
	
}

