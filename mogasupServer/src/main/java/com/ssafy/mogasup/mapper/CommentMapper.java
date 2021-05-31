package com.ssafy.mogasup.mapper;

import java.util.HashMap;
import java.util.List;

public interface CommentMapper {
	public void insertComment(int user_id, int picture_id, String voice_path);
	public void deleteComment(int comment_id);
	public List<HashMap<String, String>> listComment(int picture_id);
	public String readComment(int comment_id);
}
