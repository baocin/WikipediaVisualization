using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using ProtoBuf;


[ProtoContract]
[Serializable]
class Page
{
	[ProtoMember(1)]
	public string pagetitle;
	[ProtoMember(2)]
	public string pageid;
	[ProtoMember(3)]
	public string pagecategories;

	public override string ToString ()
	{
		return string.Format ("[Page: pagetitle={0}, pageid={1}, pagecategories={2}]", pagetitle, pageid, pagecategories);
	}
	
}



