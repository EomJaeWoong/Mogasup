package com.ssafy.mogasup.dao;

import java.util.HashMap;
import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Repository;

import com.ssafy.mogasup.mapper.CommentMapper;
import com.ssafy.mogasup.mapper.PictureMapper;

@Repository
public class CommentDaoImpl implements CommentDao{
	@Autowired
	CommentMapper mapper;

	@Override
	public void insertComment(int user_id, int picture_id, String voice_path) {
		mapper.insertComment(user_id, picture_id, voice_path);
	}

	@Override
	public void deleteComment(int comment_id) {
		mapper.deleteComment(comment_id);
	}

	@Override
	public List<HashMap<String, String>> listComment(int picture_id) {
		return mapper.listComment(picture_id);
	}

	@Override
	public String readComment(int comment_id) {
		return mapper.readComment(comment_id);
	}
}
