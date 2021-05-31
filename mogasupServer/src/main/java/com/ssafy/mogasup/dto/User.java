package com.ssafy.mogasup.dto;

public class User {
	private int user_id;
	private String email;
	private String nickname;
	private String password;
	
	public User() {
	}

	public User(int user_id, String email, String nickname, String password) {
		super();
		this.user_id = user_id;
		this.email = email;
		this.nickname = nickname;
		this.password = password;
	}

	public int getUser_id() {
		return user_id;
	}

	public void setUser_id(int user_id) {
		this.user_id = user_id;
	}

	public String getEmail() {
		return email;
	}

	public void setEmail(String email) {
		this.email = email;
	}

	public String getNickname() {
		return nickname;
	}

	public void setNickname(String nickname) {
		this.nickname = nickname;
	}

	public String getPassword() {
		return password;
	}

	public void setPassword(String password) {
		this.password = password;
	}

	
}
