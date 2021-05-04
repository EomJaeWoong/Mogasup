package com.ssafy.mogasup.dao;

import java.util.HashMap;
import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Repository;

import com.ssafy.mogasup.mapper.PictureMapper;

@Repository
public class PictureDaoImpl implements PictureDao{
	@Autowired
	PictureMapper mapper;
	
	@Override
	public void insertImage(int family_id, String image_path) {
		mapper.insertImage(family_id, image_path);
	}

	@Override
	public void deleteImage(int picture_id) {
		mapper.deleteImage(picture_id);
	}

	@Override
	public List<HashMap<String, String>> listImage(int family_id) {
		return mapper.listImage(family_id);
	}

	@Override
	public String readImage(int picture_id) {
		return mapper.readImage(picture_id);
	}
}
