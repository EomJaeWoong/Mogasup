package com.ssafy.mogasup.dao;

import java.util.HashMap;
import java.util.List;

public interface CommentDao {
	public void insertComment(int user_id, int picture_id, String image_path);
	public void deleteComment(int comment_id);
	public List<HashMap<String, String>> listComment(int picture_id);
	public String readComment(int comment_id);
}
