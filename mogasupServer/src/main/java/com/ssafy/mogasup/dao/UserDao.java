package com.ssafy.mogasup.dao;

import com.ssafy.mogasup.dto.User;

public interface UserDao {
	public void signup(User user);
	public User findByEmail(String email);
	public User login(String email, String password);
}
