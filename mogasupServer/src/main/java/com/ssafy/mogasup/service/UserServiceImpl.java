package com.ssafy.mogasup.service;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.ssafy.mogasup.dao.UserDao;
import com.ssafy.mogasup.dto.User;

@Service
public class UserServiceImpl implements UserService{
	@Autowired
	UserDao dao;
	
	@Override
	public void signup(User user) {
		dao.signup(user);
	}

	@Override
	public User findByEmail(String email) {
		return dao.findByEmail(email);
	}

	@Override
	public User login(String email, String password) {
		return dao.login(email, password);
	}
	
	@Override
	public String findUserByEmail(String email) {
		return dao.findUserByEmail(email);
	}
	
	@Override
	public List<String> findFamilyByUserId(String user_id) {
		return dao.findFamilyByUserId(user_id);
	}
}
