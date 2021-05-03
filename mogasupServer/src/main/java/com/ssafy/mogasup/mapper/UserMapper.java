package com.ssafy.mogasup.mapper;

import com.ssafy.mogasup.dto.User;

public interface UserMapper {
	public void signup(User user);
	public User findByEmail(String email);
	public User login(String email, String password);
}
