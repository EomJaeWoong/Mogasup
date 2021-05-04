package com.ssafy.mogasup.dao;

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
}
