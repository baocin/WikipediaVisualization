using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
class Page
{
	public string pagetitle;
	public string pageid;
	public string pagecategories;

	public override string ToString ()
	{
		return string.Format ("[Page: pagetitle={0}, pageid={1}, pagecategories={2}]", pagetitle, pageid, pagecategories);
	}
	
}



