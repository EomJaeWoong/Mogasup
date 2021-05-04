package com.ssafy.mogasup.dao;

import java.util.HashMap;
import java.util.List;

public interface PictureDao {
	public void insertImage(int family_id, String image_path);
	public void deleteImage(int picture_id);
	public List<HashMap<String, String>> listImage(int family_id);
	public String readImage(int picture_id);
}
