package com.ssafy.mogasup.dao;

import java.util.List;

import com.ssafy.mogasup.dto.User;

public interface UserDao {
	public void signup(User user);
	public User findByEmail(String email);
	public User login(String email, String password);
	public String findUserByEmail(String email);
	public List<String> findFamilyByUserId(String user_id);
}
