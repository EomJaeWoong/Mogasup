package com.ssafy.mogasup.service;

import java.util.HashMap;
import java.util.List;

import org.springframework.web.multipart.MultipartFile;

public interface CommentService {
	public void init();
	public void saveComment(MultipartFile file, String timeurl);
	public void insertComment(int user_id, int picture_id, String voice_path);
	public void deleteComment(int comment_id);
	public List<HashMap<String, String>> listComment(int picture_id);
	public String readComment(int comment_id);
}
