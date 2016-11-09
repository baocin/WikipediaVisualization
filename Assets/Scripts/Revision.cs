using System;

public class Revision
{
	public string rev_id;
	public string rev_timestamp;
	public string rev_user;
	public string rev_user_text;
	public string rev_page;
	public string rev_sha1;
	public string rev_minor_edit;
	public string rev_deleted;
	public string rev_parent_id;
	public string archived;
	public string reverting_id;
	public string reverting_timestamp;
	public string reverting_user;
	public string reverting_user_text;
	public string reverting_page;
	public string reverting_sha1;
	public string reverting_minor_edit;
	public string reverting_deleted;
	public string reverting_parent_id;
	public string reverting_archived;
	public string rev_revert_offset;
	public string revisions_reverted;
	public string reverted_to_rev_id;

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
