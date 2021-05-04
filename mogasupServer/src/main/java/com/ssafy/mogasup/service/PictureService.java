package com.ssafy.mogasup.service;

import org.springframework.web.multipart.MultipartFile;

public interface PictureService {
	public void init();
	public void saveimage(MultipartFile file, String timeurl);
	public void insertImage(int family_id, String image_path);
}
