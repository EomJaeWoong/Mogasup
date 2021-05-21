package com.ssafy.mogasup.dao;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Repository;

import com.ssafy.mogasup.dto.User;
import com.ssafy.mogasup.mapper.UserMapper;

@Repository
public class UserDaoImpl implements UserDao{
	@Autowired
	UserMapper mapper;
	
	@Override
	public void signup(User user) {
		mapper.signup(user);
	}

	@Override
	public User findByEmail(String email) {
		return mapper.findByEmail(email);
	}

	@Override
	public User login(String email, String password) {
		return mapper.login(email, password);
	}
	
	@Override
	public String findUserByEmail(String email) {
		return mapper.findUserByEmail(email);
	}
	
	@Override
	public List<String> findFamilyByUserId(String user_id) {
		return mapper.findFamilyByUserId(user_id);
	}
}
