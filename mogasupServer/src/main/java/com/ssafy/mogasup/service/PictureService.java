package com.ssafy.mogasup.service;

import java.util.HashMap;
import java.util.List;

import org.springframework.web.multipart.MultipartFile;

public interface PictureService {
	public void init();
	public void saveimage(MultipartFile file, String timeurl);
	public void insertImage(int family_id, String image_path);
	public void deleteImage(int picture_id);
	public List<HashMap<String, String>> listImage(int family_id);
	public String readImage(int picture_id);
}
