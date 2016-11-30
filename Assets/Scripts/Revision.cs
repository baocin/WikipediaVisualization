using System;
using ProtoBuf;


[ProtoContract]
[Serializable]
public class Revision
{
	[ProtoMember(1)]
	public string rev_id;
	[ProtoMember(2)]
	public string rev_timestamp;
	[ProtoMember(3)]
	public string rev_user;
	[ProtoMember(4)]
	public string rev_user_text;
	[ProtoMember(5)]
	public string rev_page;
	[ProtoMember(6)]
	public string rev_sha1;
	[ProtoMember(7)]
	public string rev_minor_edit;
	[ProtoMember(8)]
	public string rev_deleted;
	[ProtoMember(9)]
	public string rev_parent_id;
	[ProtoMember(10)]
	public string archived;
	[ProtoMember(11)]
	public string reverting_id;
	[ProtoMember(12)]
	public string reverting_timestamp;
	[ProtoMember(13)]
	public string reverting_user;
	[ProtoMember(14)]
	public string reverting_user_text;
	[ProtoMember(15)]
	public string reverting_page;
	[ProtoMember(16)]
	public string reverting_sha1;
	[ProtoMember(17)]
	public string reverting_minor_edit;
	[ProtoMember(18)]
	public string reverting_deleted;
	[ProtoMember(19)]
	public string reverting_parent_id;
	[ProtoMember(20)]
	public string reverting_archived;
	[ProtoMember(21)]
	public string rev_revert_offset;
	[ProtoMember(22)]
	public string revisions_reverted;
	[ProtoMember(23)]
	public string reverted_to_rev_id;

	public Revision ()
	{
	}
	
	public Revision (string rev_id, string rev_timestamp, string rev_user, string rev_user_text, string rev_page, string rev_sha1, string rev_minor_edit, string rev_deleted, string rev_parent_id, string archived, string reverting_id, string reverting_timestamp, string reverting_user, string reverting_user_text, string reverting_page, string reverting_sha1, string reverting_minor_edit, string reverting_deleted, string reverting_parent_id, string reverting_archived, string rev_revert_offset, string revisions_reverted, string reverted_to_rev_id)
	{
		this.rev_id = rev_id;
		this.rev_timestamp = rev_timestamp;
		this.rev_user = rev_user;
		this.rev_user_text = rev_user_text;
		this.rev_page = rev_page;
		this.rev_sha1 = rev_sha1;
		this.rev_minor_edit = rev_minor_edit;
		this.rev_deleted = rev_deleted;
		this.rev_parent_id = rev_parent_id;
		this.archived = archived;
		this.reverting_id = reverting_id;
		this.reverting_timestamp = reverting_timestamp;
		this.reverting_user = reverting_user;
		this.reverting_user_text = reverting_user_text;
		this.reverting_page = reverting_page;
		this.reverting_sha1 = reverting_sha1;
		this.reverting_minor_edit = reverting_minor_edit;
		this.reverting_deleted = reverting_deleted;
		this.reverting_parent_id = reverting_parent_id;
		this.reverting_archived = reverting_archived;
		this.rev_revert_offset = rev_revert_offset;
		this.revisions_reverted = revisions_reverted;
		this.reverted_to_rev_id = reverted_to_rev_id;
	}
	
	public override string ToString ()
	{
		return string.Format ("[Revision: rev_id={0}, rev_timestamp={1}, rev_user={2}, rev_user_text={3}, rev_page={4}, rev_sha1={5}, rev_minor_edit={6}, rev_deleted={7}, rev_parent_id={8}, archived={9}, reverting_id={10}, reverting_timestamp={11}, reverting_user={12}, reverting_user_text={13}, reverting_page={14}, reverting_sha1={15}, reverting_minor_edit={16}, reverting_deleted={17}, reverting_parent_id={18}, reverting_archived={19}, rev_revert_offset={20}, revisions_reverted={21}, reverted_to_rev_id={22}]", rev_id, rev_timestamp, rev_user, rev_user_text, rev_page, rev_sha1, rev_minor_edit, rev_deleted, rev_parent_id, archived, reverting_id, reverting_timestamp, reverting_user, reverting_user_text, reverting_page, reverting_sha1, reverting_minor_edit, reverting_deleted, reverting_parent_id, reverting_archived, rev_revert_offset, revisions_reverted, reverted_to_rev_id);
	}
	
}
