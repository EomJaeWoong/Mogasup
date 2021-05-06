package com.ssafy.mogasup.service;

import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.util.HashMap;
import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.web.multipart.MultipartFile;

import com.ssafy.mogasup.dao.CommentDao;
import com.ssafy.mogasup.dao.PictureDao;

@Service
public class CommentServiceImpl implements CommentService{
	@Autowired
	CommentDao dao;
	
	private final Path root = Paths.get("C:/Users/multicampus/Desktop");

	@Override
	public void init() {
		try {
			Files.createDirectory(root);
		} catch (IOException e) {
			throw new RuntimeException("Could not initialize folder for upload!");
		}
		
	}

	@Override
	public void saveComment(MultipartFile file, String timeurl) {
		try {
//	    	System.out.println(this.root.resolve(file.getOriginalFilename())); //.\HBD.jpg
			Files.copy(file.getInputStream(), this.root.resolve("picture/"+timeurl+".wav"));
		} catch (Exception e) {
			throw new RuntimeException("Could not store the file. Error: " + e.getMessage());
		}
	}

	@Override
	public void insertComment(int user_id, int picture_id, String voice_path) {
		dao.insertComment(user_id, picture_id, voice_path);
	}

	@Override
	public void deleteComment(int comment_id) {
		dao.deleteComment(comment_id);
	}

	@Override
	public List<HashMap<String, String>> listComment(int picture_id) {
		return dao.listComment(picture_id);
	}

	@Override
	public String readComment(int comment_id) {
		return dao.readComment(comment_id);
	}

	
}
