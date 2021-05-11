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

import com.ssafy.mogasup.dao.PictureDao;

@Service
public class PictureServiceImpl implements PictureService{
	@Autowired
	PictureDao dao;
	
//	private final Path root = Paths.get("C:/Users/multicampus/Desktop");
	private final Path root = Paths.get("/home/ubuntu/backend/");

	@Override
	public void init() {
		try {
			Files.createDirectory(root);
		} catch (IOException e) {
			throw new RuntimeException("Could not initialize folder for upload!");
		}
		
	}

	@Override
	public void saveimage(MultipartFile file, String timeurl) {
		try {
//	    	System.out.println(this.root.resolve(file.getOriginalFilename())); //.\HBD.jpg
			Files.copy(file.getInputStream(), this.root.resolve("picture/"+timeurl+file.getOriginalFilename()));
		} catch (Exception e) {
			throw new RuntimeException("Could not store the file. Error: " + e.getMessage());
		}
	}

	@Override
	public void insertImage(int family_id, String image_path) {
		dao.insertImage(family_id, image_path);
	}

	@Override
	public void deleteImage(int picture_id) {
		dao.deleteImage(picture_id);
	}

	@Override
	public List<HashMap<String, String>> listImage(int family_id) {
		return dao.listImage(family_id);
	}

	@Override
	public String readImage(int picture_id) {
		return dao.readImage(picture_id);
	}
	
}
