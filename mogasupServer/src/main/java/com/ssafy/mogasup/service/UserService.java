package com.ssafy.mogasup.service;

import com.ssafy.mogasup.dto.User;

public interface UserService {
	public void signup(User user);
	public User findByEmail(String email);
	public User login(String email, String password);
}
